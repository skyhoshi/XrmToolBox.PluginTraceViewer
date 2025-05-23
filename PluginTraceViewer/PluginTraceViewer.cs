﻿using Cinteros.XTB.PluginTraceViewer.Const;
using Cinteros.XTB.PluginTraceViewer.Controls;
using Cinteros.XTB.PluginTraceViewer.Properties;
using McTools.Xrm.Connection;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Rappen.XRM.Helpers.Serialization;
using Rappen.XTB;
using Rappen.XTB.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;
using XrmToolBox.Extensibility.Interfaces;

namespace Cinteros.XTB.PluginTraceViewer
{
    public partial class PluginTraceViewer : PluginControlBase, IGitHubPlugin, IMessageBusHost, IHelpPlugin, IPayPalPlugin, IStatusBarMessenger, IShortcutReceiver, IAboutPlugin
    {
        private const string aiEndpoint = "https://dc.services.visualstudio.com/v2/track";

        //private const string aiKey = "cc7cb081-b489-421d-bb61-2ee53495c336";    // jonas@rappen.net tenant, TestAI
        private const string aiKey1 = "eed73022-2444-45fd-928b-5eebd8fa46a6";    // jonas@rappen.net tenant, XrmToolBox

        private const string aiKey2 = "d46e9c12-ee8b-4b28-9643-dae62ae7d3d4";    // jonas@jonasr.app, XrmToolBoxTools

        private readonly AppInsights ai1;
        private readonly AppInsights ai2;

        internal GridControl gridControl;
        internal FilterControl filterControl;
        private StatsControl statsControl;
        private TraceControl traceControl;
        private ExceptionControl exceptionControl;
        internal RecordList recordlist;
        private Entity lasttracerecord;
        private List<string> defaultcolumns;

        public PluginTraceViewer()
        {
            InitializeComponent();
            UrlUtils.TOOL_NAME = "PluginTraceViewer";
            tslAbout.ToolTipText = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";
            ai1 = new AppInsights(aiEndpoint, aiKey1, Assembly.GetExecutingAssembly(), "Plugin Trace Viewer");
            ai2 = new AppInsights(aiEndpoint, aiKey2, Assembly.GetExecutingAssembly(), "Plugin Trace Viewer");
            var theme = new VS2015LightTheme();
            dockContainer.Theme = theme;
            gridControl = new GridControl(this);
            filterControl = new FilterControl(this);
            statsControl = new StatsControl(this);
            traceControl = new TraceControl(this);
            exceptionControl = new ExceptionControl(this);
        }

        private void SetupDockControls()
        {
            string dockFile = GetDockFileName();
            if (File.Exists(dockFile))
            {
                try
                {
                    dockContainer.LoadFromXml(dockFile, dockDeSerialization);
                    return;
                }
                catch (InvalidOperationException)
                {
                    // Restore from file failed
                }
            }
            ResetDockLayout();
        }

        private void ResetDockLayout()
        {
            gridControl.Show(dockContainer, DockState.Document);
            filterControl.Show(dockContainer, DockState.DockTop);
            traceControl.Show(dockContainer, DockState.DockRight);
            exceptionControl.Show(traceControl.Pane, DockAlignment.Bottom, 0.3);
            dockContainer.DockTopPortion = filterControl.originalSize.Height;
            statsControl.Hide();
        }

        private static string GetDockFileName()
        {
            return Path.Combine(Paths.SettingsPath, "Cinteros.XTB.PluginTraceViewer_DockPanels.xml");
        }

        private IDockContent dockDeSerialization(string persistString)
        {
            switch (persistString)
            {
                case "Cinteros.XTB.PluginTraceViewer.Controls.GridControl":
                    return gridControl;

                case "Cinteros.XTB.PluginTraceViewer.Controls.FilterControl":
                    return filterControl;

                case "Cinteros.XTB.PluginTraceViewer.Controls.StatsControl":
                    return statsControl;

                case "Cinteros.XTB.PluginTraceViewer.Controls.TraceControl":
                    return traceControl;

                case "Cinteros.XTB.PluginTraceViewer.Controls.ExceptionControl":
                    return exceptionControl;

                default:
                    return null;
            }
        }

        public string HelpUrl => "https://jonasr.app/PTV";

        public string RepositoryName => "PluginTraceViewer";

        public string UserName => "rappen";

        public string DonationDescription => "Plugin Trace Viewer Fan Club";

        public string EmailAccount => "jonas@rappen.net";

        public event EventHandler<MessageBusEventArgs> OnOutgoingMessage;

        public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;

        public void OnIncomingMessage(MessageBusEventArgs message)
        {
            if (message.TargetArgument is string strarg)
            {
                if (message.SourcePlugin == "FetchXML Builder" &&
                    strarg.ToLowerInvariant().Trim().StartsWith("<fetch"))
                {
                    FetchUpdated(strarg);
                }
                else if (ParseFilterArgs(strarg) is PTVFilter filter)
                {
                    filterControl?.ApplyFilter(filter);
                    RefreshTraces(GetQuery(false));
                }
            }
        }

        private PTVFilter ParseFilterArgs(string strarg)
        {
            var result = new PTVFilter();
            foreach (var filterpair in strarg.Split('\n').Select(f => f.Trim()).Where(f => !string.IsNullOrEmpty(f)))
            {
                if (filterpair.Contains("="))
                {
                    var key = filterpair.Split('=')[0];
                    var value = filterpair.Split('=')[1];
                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        switch (key.ToLowerInvariant().Trim())
                        {
                            case "plugin":
                                result.Plugin = value;
                                result.FilterPlugin = true;
                                break;

                            case "message":
                                result.Message = value;
                                result.FilterMessage = true;
                                break;

                            case "entity":
                                result.Entity = value;
                                result.FilterEntity = true;
                                break;

                            case "correlationid":
                                result.CorrelationId = value;
                                result.FilterCorr = true;
                                break;

                            case PluginTraceLog.RequestId:
                                result.RequestId = value;
                                break;

                            case "exceptionsonly":
                                if (bool.TryParse(value, out bool exceptions))
                                {
                                    result.Exceptions = exceptions;
                                }
                                break;

                            case "plugins":
                                if (bool.TryParse(value, out bool plugins))
                                {
                                    result.Exceptions = plugins;
                                }
                                break;

                            case "workflows":
                                if (bool.TryParse(value, out bool workflows))
                                {
                                    result.Exceptions = workflows;
                                }
                                break;

                            case "sync":
                                if (bool.TryParse(value, out bool sync))
                                {
                                    result.Exceptions = sync;
                                }
                                break;

                            case "async":
                                if (bool.TryParse(value, out bool async))
                                {
                                    result.Exceptions = async;
                                }
                                break;

                            case "preval":
                                if (bool.TryParse(value, out bool preval))
                                {
                                    result.Exceptions = preval;
                                }
                                break;

                            case "preop":
                                if (bool.TryParse(value, out bool preop))
                                {
                                    result.Exceptions = preop;
                                }
                                break;

                            case "postop":
                                if (bool.TryParse(value, out bool postop))
                                {
                                    result.Exceptions = postop;
                                }
                                break;

                            case "durationmin":
                                if (int.TryParse(value, out int durationmin))
                                {
                                    result.MinDuration = durationmin;
                                }
                                break;

                            case "durationmax":
                                if (int.TryParse(value, out int durationmax))
                                {
                                    result.MinDuration = durationmax;
                                }
                                break;

                            case "records":
                                if (int.TryParse(value, out int records))
                                {
                                    result.MinDuration = records;
                                }
                                break;
                        }
                    }
                }
            }
            return result;
        }

        public void ReceiveKeyDownShortcut(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5 && buttonRetrieveLogs.Enabled)
            {
                buttonRetrieveLogs_Click(null, null);
            }
            else if (e.Shift && e.KeyCode == Keys.C && gridControl.tsmiCorrelationSelectThis.Enabled && gridControl.crmGridView.Focused)
            {
                gridControl.SelectCurrentCorrelation();
            }
        }

        public void ReceiveKeyPressShortcut(KeyPressEventArgs e)
        {
            // Don't handle
        }

        public void ReceiveKeyUpShortcut(KeyEventArgs e)
        {
            // Don't handle
        }

        public void ReceivePreviewKeyDownShortcut(PreviewKeyDownEventArgs e)
        {
            // Don't handle
        }

        public void ShowAboutDialog()
        {
            tslAbout_Click(null, null);
        }

        internal void UpdateHighlighting()
        {
            traceControl?.ShowMessageTextAsync(tsmiHighlightGuids.Checked, tsmiIdentifyRecords.Checked, null, filterControl.FreeTextFilter(), gridControl.crmGridView.Filtering.Text);
            exceptionControl?.ShowMessageTextAsync(tsmiHighlightGuids.Checked, tsmiIdentifyRecords.Checked, filterControl.FreeTextFilter(), gridControl.crmGridView.Filtering.Text);
        }

        private void tslAbout_Click(object sender, EventArgs e)
        {
            LogUse("OpenAbout");
            var about = new About(this);
            about.StartPosition = FormStartPosition.CenterParent;
            about.lblVersion.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            about.ShowDialog();
        }

        private void PluginTraceViewer_Load(object sender, EventArgs e)
        {
            SetupDockControls();
            defaultcolumns = gridControl.Columns;
            LoadSettings();
            LogUse("Load", ai2: true);
            Supporting.ShowIf(this, false, true, ai2);
            if (Supporting.IsEnabled(this))
            {
                tsbSupporting.Visible = true;
                var supptype = Supporting.IsSupporting(this);
                switch (supptype)
                {
                    case SupportType.Company:
                        tsbSupporting.Image = Resources.We_Support_icon;
                        break;

                    case SupportType.Personal:
                        tsbSupporting.Image = Resources.I_Support_icon;
                        break;

                    case SupportType.Contribute:
                        tsbSupporting.Image = Resources.I_Contribute_icon;
                        break;
                }
            }
            else
            {
                tsbSupporting.Visible = false;
            }
        }

        private void PluginTraceViewer_ConnectionUpdated(object sender, ConnectionUpdatedEventArgs e)
        {
            LoadFilter();
            var orgver = new Version(e.ConnectionDetail.OrganizationVersion);
            LogInfo("Connected CRM version: {0}", orgver);
            recordlist = new RecordList(e.ConnectionDetail, Service);
            ClearControls();
            var orgok = orgver >= new Version(7, 1);
            gridControl.SetOrgService(orgok ? e.Service : null);
            buttonRetrieveLogs.Enabled = orgok;
            tsmiRefreshFilter.Enabled = orgok;
            if (orgok)
            {
                filterControl.LoadConstraints();
                LoadLogSetting();
            }
            else
            {
                LogError("CRM version too old for Plugin Trace Viewer");
                MessageBox.Show("Plug-in Trace Log was introduced in\nMicrosoft Dynamics CRM 2015 Update 1 (7.1.0.0)\n\nPlease connect to a newer organization to use this cool tool.", "Organization too old", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public override void ClosingPlugin(PluginCloseInfo info)
        {
            ShowLogSettingWarning(info);
            SaveSettings();
            SaveDockPanels();
            LogUse("Close", ai2: true);
        }

        private void ShowLogSettingWarning(PluginCloseInfo info)
        {
            if (comboLogSetting.SelectedIndex == 2 && !tsmiSuppressLogSettingWarning.Checked)
            {
                switch (MessageBox.Show($"Trace Log Setting is currently set to 'All'\nin environment '{ConnectionDetail.ConnectionName}'.\n\n" +
                    "This setting can cause lots of trace log records, which eventually can slow down the system and increase data consumption.\n\n" +
                    "Change setting to 'Off'?", "Exit Plugin Trace Viewer", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
                {
                    case DialogResult.Yes:
                        UpdateLogSetting(0);
                        break;

                    case DialogResult.No:
                        switch (MessageBox.Show("Continue showing this warning for this organization?", "Trace Log Setting", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        {
                            case DialogResult.No:
                                tsmiSuppressLogSettingWarning.Checked = true;
                                break;

                            case DialogResult.Cancel:
                                info.Cancel = true;
                                break;
                        }
                        break;

                    case DialogResult.Cancel:
                        info.Cancel = true;
                        break;
                }
            }
        }

        private void SaveDockPanels()
        {
            var dockFile = GetDockFileName();
            dockContainer.SaveAsXml(dockFile);
        }

        internal void SendStatusMessage(string message)
        {
            Invoke(new Action(() =>
            {
                SendMessageToStatusBar(this, new StatusBarMessageEventArgs(message));
            }));
        }

        private void LoadFilter()
        {
            if (SettingsManager.Instance.TryLoad(typeof(PluginTraceViewer), out PTVFilter settings, ConnectionDetail?.ConnectionName))
            {
                filterControl.ApplyFilter(settings);
            }
        }

        private void LoadSettings()
        {
            if (!SettingsManager.Instance.TryLoad(typeof(PluginTraceViewer), out Settings settings, "Settings"))
            {
                settings = new Settings();
            }
            ApplySettings(settings);
        }

        private void LoadLogSetting()
        {
            comboLogSetting.Enabled = false;
            GetLogSetting((id, setting) =>
            {
                comboLogSetting.Tag = id;
                comboLogSetting.SelectedIndex = setting;
                comboLogSetting.Enabled = true;
            });
        }

        private void UpdateLogSetting(int setting)
        {
            LogUse("UpdateSetting-" + setting);
            var orgsetting = new Entity(Organization.EntityName);
            orgsetting.Id = (Guid)comboLogSetting.Tag;
            orgsetting[Organization.PluginTraceLogsetting] = new OptionSetValue(setting);
            SendStatusMessage($"Updating setting to {setting}");
            LogInfo($"Updating setting for org {orgsetting.Id} to {setting}");
            Enabled = false;
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Updating trace setting",
                Work = (a, e) =>
                {
                    Service.Update(orgsetting);
                },
                PostWorkCallBack = (e) =>
                {
                    Enabled = true;
                    if (e.Error != null)
                    {
                        ShowErrorDialog(e.Error, "Update Setting");
                    }
                    else if (IsDisposed == false)
                    {
                        comboLogSetting.SelectedIndex = setting;
                        SendStatusMessage("");
                        MessageBox.Show("Updated trace setting!");
                    }
                    gridControl.Focus();
                }
            });
        }

        private void GetLogSetting(Action<Guid, int> callback)
        {
            LogInfo("GetLogSetting");
            var qx = new QueryExpression(Const.Organization.EntityName);
            qx.ColumnSet.AddColumns(Const.Organization.PrimaryName, Const.Organization.PluginTraceLogsetting);
            var asyncinfo = new WorkAsyncInfo()
            {
                Message = "Loading organization settings",
                Work = (a, args) =>
                {
                    args.Result = Service.RetrieveMultiple(qx);
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        ShowErrorDialog(args.Error, "Load Settings");
                    }
                    else if (args.Result is EntityCollection)
                    {
                        var entity = ((EntityCollection)args.Result).Entities.FirstOrDefault();
                        if (entity != null)
                        {
                            var id = entity.Id;
                            var name = entity.Contains(Const.Organization.PrimaryName) ? entity[Const.Organization.PrimaryName] : "?";
                            var setting = entity.Contains(Const.Organization.PluginTraceLogsetting) ? ((OptionSetValue)entity[Const.Organization.PluginTraceLogsetting]).Value : 0;
                            LogInfo("Found org: {0} with setting {1}", name, setting);
                            callback(id, setting);
                        }
                        else
                        {
                            LogError("No organization data found!");
                        }
                    }
                }
            };
            WorkAsync(asyncinfo);
        }

        private void FetchUpdated(string fetchxml)
        {
            var req = Service.Execute(new FetchXmlToQueryExpressionRequest { FetchXml = fetchxml }) as FetchXmlToQueryExpressionResponse;
            RefreshTraces(req.Query);
        }

        private void RefreshTraces(QueryExpression query)
        {
            if (query == null)
            {
                return;
            }
            if (Service == null)
            {
                return;
            }
            StopRefreshTimer();
            ClearControls();
            LogUse("RetrieveLogs");
            var asyncinfo = new WorkAsyncInfo()
            {
                Message = "Loading trace log records",
                Work = (a, args) =>
                {
                    LogInfo("Loading logs");
                    UpdateUI(() => { Enabled = false; });
                    args.Result = Service.RetrieveMultiple(query);
                },
                PostWorkCallBack = (args) =>
                {
                    UpdateUI(() => { Enabled = true; });
                    if (args.Error != null)
                    {
                        ShowErrorDialog(args.Error, "Load Traces");
                    }
                    else if (args.Result is EntityCollection results)
                    {
                        var entities = results.Entities;
                        FixingLogRecords(entities);
                        gridControl.PopulateGrid(entities);
                        StartRefreshTimer(false);
                    }
                }
            };
            WorkAsync(asyncinfo);
        }

        private void RefreshNewTraces(bool forcerefresh)
        {
            StopRefreshTimer();
            if (gridControl.crmGridView.DataSource == null || comboRefreshMode.SelectedIndex == 0)
            {
                return;
            }
            try
            {
                var newlogs = Service.RetrieveMultiple(GetQuery(true));
                if (newlogs.Entities.Count == 0)
                {
                    UpdateRefreshButton(0);
                }
                else
                {
                    if (gridControl.crmGridView.DataSource is DataCollection<Entity> logs)
                    {
                        foreach (var log in logs)
                        {
                            newlogs.Remove(log.Id);
                        }
                        UpdateRefreshButton(newlogs.Entities.Count);

                        if ((forcerefresh || comboRefreshMode.SelectedIndex == 2) && newlogs.Entities.Count > 0)
                        {
                            foreach (var log in newlogs.Entities.Reverse())
                            {
                                logs.Insert(0, log);
                            }
                            FixingLogRecords(logs);
                            gridControl.crmGridView.Refresh();
                            UpdateRefreshButton(0);
                        }
                    }
                }
                StartRefreshTimer(false);
            }
            catch
            {   // If something went wrong, don't check that often
                StartRefreshTimer(true);
            }
        }

        private void FixingLogRecords(DataCollection<Entity> logs)
        {
            FriendlyfyCorrelationIds(logs);
            SplittingStartDateTime(logs);
            SimplifyPluginTypes(logs);
            HidePluginsFromSteps(logs);
            HideEntitiesFromSteps(logs);
            SetTraceSizes(logs);
            ExtractExceptionSummaries(logs);
        }

        private void UpdateRefreshButton(int count)
        {
            var text = $"{count} new";
            var style = count > 0 ? ToolStripItemDisplayStyle.ImageAndText : ToolStripItemDisplayStyle.Text;
            if (buttonRefreshLogs.Text != text)
            {
                buttonRefreshLogs.Text = text;
            }
            if (buttonRefreshLogs.DisplayStyle != style)
            {
                buttonRefreshLogs.DisplayStyle = style;
            }
        }

        internal void StartRefreshTimer(bool increaseinterval)
        {
            if (comboLogSetting.IsDisposed != true && (comboRefreshMode.SelectedIndex == 1 || comboRefreshMode.SelectedIndex == 2))
            {
                if (increaseinterval)
                {   // Something probably went wrong, increase interval until next try
                    if (timerRefresh.Interval < 30000)
                    {   // Never slower than 30 sec
                        timerRefresh.Interval = timerRefresh.Interval * 2;
                    }
                }
                else if (timerRefresh.Tag is int defaultinterval && defaultinterval != timerRefresh.Interval)
                {   // Reset interval to default
                    timerRefresh.Interval = defaultinterval;
                }
                timerRefresh.Start();
            }
        }

        internal void StopRefreshTimer()
        {
            timerRefresh.Stop();
        }

        private void ClearControls()
        {
            traceControl.Clear();
            exceptionControl.Clear();
            statsControl.Clear();
        }

        private void SimplifyPluginTypes(IEnumerable<Entity> entities)
        {
            if (tsmiFullyQualifiedPluginNames.Checked)
            {
                return;
            }
            foreach (var entity in entities)
            {
                if (entity.TryGetAttributeValue(PluginTraceLog.PrimaryName, out string plugin))
                {
                    if (plugin.Contains(",") && plugin.Contains("Version="))
                    {   // Looks like new fully qualified format of the plugintype, break before first comma and add wildcard
                        entity[PluginTraceLog.PrimaryName] = plugin.Split(',')[0];
                    }
                }
            }
        }

        private void HidePluginsFromSteps(DataCollection<Entity> entities)
        {
            if (!tsmiHidePluginFromStep.Checked)
            {
                return;
            }
            foreach (var entity in entities)
            {
                if (entity.TryGetAttributeValue(PluginTraceLog.PrimaryName, out string plugin))
                {
                    var step = GetStepName(entity);
                    if (step.StartsWith(plugin))
                    {
                        entity["step.name"] = step.Replace(plugin, "").Trim(':').Trim();
                    }
                    else if (step.Contains(":"))
                    {
                        var stepplugin = step.Split(':')[0];
                        if (plugin.StartsWith(stepplugin))
                        {
                            entity["step.name"] = step.Replace(stepplugin, "").Trim(':').Trim();
                        }
                    }
                }
            }
        }

        private void HideEntitiesFromSteps(DataCollection<Entity> entities)
        {
            if (!tsmiHideEntityFromStep.Checked)
            {
                return;
            }
            foreach (var entity in entities)
            {
                var step = GetStepName(entity);
                if (!string.IsNullOrWhiteSpace(step) &&
                    entity.TryGetAttributeValue(PluginTraceLog.PrimaryEntity, out string triggerentity) &&
                    !string.IsNullOrWhiteSpace(triggerentity) &&
                    step.EndsWith($"{triggerentity}"))
                {
                    entity["step.name"] = step.Replace(triggerentity, "").Replace(" of ", "").Trim();
                }
            }
        }

        private static string GetStepName(Entity entity)
        {
            if (entity.TryGetAttributeValue(PluginTraceLog.PrimaryName, out string plugin) &&
                entity.Contains("step.name"))
            {
                if (entity["step.name"] is AliasedValue stepalias && stepalias.Value is string stepname)
                {
                    return stepname;
                }
                else if (entity["step.name"] is string steptext)
                {
                    return steptext;
                }
            }
            return string.Empty;
        }

        private void FriendlyfyCorrelationIds(IEnumerable<Entity> entities)
        {
            var allCorrelationIds = new List<Guid>();
            var correlationIds = new List<Guid>();
            foreach (var entity in entities)
            {   // First determine which correlation ids that occur more than once, we don't want to show corr for single occurences
                if (entity.Contains(PluginTraceLog.CorrelationId))
                {
                    var corrId = (Guid)entity[PluginTraceLog.CorrelationId];
                    if (allCorrelationIds.Contains(corrId))
                    {   // id occurs more than once
                        if (!correlationIds.Contains(corrId))
                        {
                            correlationIds.Add(corrId);
                        }
                    }
                    else
                    {
                        allCorrelationIds.Add(corrId);
                    }
                }
            }
            foreach (var entity in entities)
            {
                if (entity.Contains(PluginTraceLog.CorrelationId))
                {
                    var corrId = (Guid)entity[PluginTraceLog.CorrelationId];
                    if (correlationIds.Contains(corrId))
                    {
                        var index = correlationIds.IndexOf(corrId);
                        var friendlyCorr = string.Empty;
                        do
                        {
                            var curr = (index > 25 ? index / 26 - 1 : index) + 65;
                            friendlyCorr += (char)curr;
                            index = index > 25 ? index % 26 : -1;
                        }
                        while (index >= 0);
                        if (entity.Contains("correlation"))
                        {
                            entity["correlation"] = friendlyCorr;
                        }
                        else
                        {
                            entity.Attributes.Add("correlation", friendlyCorr);
                        }
                    }
                }
            }
        }

        private void SplittingStartDateTime(IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {   // First determine which correlation ids that occur more than once, we don't want to show corr for single occurences
                if (entity.Contains(PluginTraceLog.PerformanceExecutionStarttime))
                {
                    var start = (DateTime)entity[PluginTraceLog.PerformanceExecutionStarttime];
                    entity["startdate"] = start;
                }
            }
        }

        private void ExtractExceptionSummaries(IEnumerable<Entity> entities)
        {
            const string fault = "(Fault Detail is equal to Microsoft.Xrm.Sdk.OrganizationServiceFault).: ";
            const string fault2 = "System.ServiceModel.FaultException`1[Microsoft.Xrm.Sdk.OrganizationServiceFault]";
            const string faultdetails = "(Fault Detail is equal to Exception details";
            const string unhandled = "Unhandled Exception: ";
            var cnt = 0;
            foreach (var entity in entities)
            {
                if (!entity.Contains("exceptionsummary") &&
                    entity.Contains(PluginTraceLog.ExceptionDetails) &&
                    !string.IsNullOrWhiteSpace(entity[PluginTraceLog.ExceptionDetails].ToString()))
                {
                    var summary = entity[PluginTraceLog.ExceptionDetails].ToString();
                    if (summary.Contains("<Message>") && summary.Contains("</Message>"))
                    {
                        summary = summary.Substring(summary.IndexOf("<Message>") + 9);
                        summary = summary.Substring(0, summary.IndexOf("</Message>"));
                        while (summary.Contains(fault))
                        {
                            summary = summary.Substring(summary.IndexOf(fault) + fault.Length);
                        }
                    }
                    else if (summary.Contains(fault2))
                    {
                        summary = summary.Replace(fault2, "").Replace("  ", " ").Trim().Trim(':').Trim().Trim(':').Trim().Trim(':').Trim();
                        var faultdetat = summary.IndexOf(faultdetails);
                        var messageat = summary.IndexOf("\r\nMessage: ");
                        if (faultdetat > 0 && faultdetat < messageat)
                        {
                            summary = summary.Substring(0, faultdetat).Trim().Trim(':').Trim();
                        }
                        else if (summary.Contains("\r\nMessage: "))
                        {
                            summary = summary.Substring(summary.IndexOf("\r\nMessage: ") + 11).Trim();
                        }
                        if (summary.Contains("\r\nTimeStamp:"))
                        {
                            summary = summary.Substring(0, summary.IndexOf("\r\nTimeStamp:")).Trim();
                        }
                        if (summary.Contains("\r\nErrorCode:"))
                        {
                            summary = summary.Substring(summary.IndexOf("\r\nErrorCode:")).Trim();
                        }
                        if (string.IsNullOrWhiteSpace(summary))
                        {
                            summary = entity[PluginTraceLog.ExceptionDetails].ToString();
                            if (summary.Contains("\r\nErrorCode: "))
                            {
                                summary = summary.Substring(summary.IndexOf("\r\nErrorCode: ") + 13).Trim();
                            }
                            if (summary.Contains("\r\n"))
                            {
                                summary = summary.Substring(0, summary.IndexOf("\r\n")).Trim();
                            }
                        }
                        if (string.IsNullOrWhiteSpace(summary))
                        {
                            summary = "OrganizationServiceFault with no message";
                        }
                    }
                    else if (summary.StartsWith(unhandled))
                    {
                        summary = summary.Substring(summary.IndexOf(unhandled) + unhandled.Length);
                        if (summary.Contains(":"))
                        {
                            summary = summary.Split(':')[0];
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(summary))
                    {
                        entity.Attributes.Add("exceptionsummary", summary);
                        cnt++;
                    }
                }
            }
            LogInfo("Extracted exception summary from {0} logs", cnt);
        }

        private void SetTraceSizes(IEnumerable<Entity> entities)
        {
            var cnt = 0;
            foreach (var entity in entities)
            {
                if (!entity.Contains("tracesize") &&
                    entity.Contains(PluginTraceLog.MessageBlock) &&
                    !string.IsNullOrWhiteSpace(entity[PluginTraceLog.MessageBlock].ToString()))
                {
                    var trace = entity[PluginTraceLog.MessageBlock].ToString();
                    entity.Attributes.Add("tracesize", trace.Length);
                    cnt++;
                }
            }
            LogInfo("Set trace sizes for {0} logs", cnt);
        }

        private QueryExpression GetQuery(bool refreshOnly)
        {
            var QEplugintracelog = new QueryExpression(PluginTraceLog.EntityName);
            QEplugintracelog.ColumnSet.AddColumns(
                            PluginTraceLog.CorrelationId,
                            PluginTraceLog.PerformanceExecutionStarttime,
                            PluginTraceLog.OperationType,
                            PluginTraceLog.MessageName,
                            PluginTraceLog.PrimaryKey,
                            PluginTraceLog.PrimaryEntity,
                            PluginTraceLog.ExceptionDetails,
                            PluginTraceLog.MessageBlock,
                            PluginTraceLog.PerformanceExecutionDuration,
                            PluginTraceLog.CreatedOn,
                            PluginTraceLog.PrimaryName,
                            PluginTraceLog.Depth,
                            PluginTraceLog.Mode,
                            PluginTraceLog.RequestId);
            var LEstep = QEplugintracelog.AddLink(SdkMessageProcessingStep.EntityName, PluginTraceLog.PluginStepId, SdkMessageProcessingStep.PrimaryKey, JoinOperator.LeftOuter);
            LEstep.EntityAlias = "step";
            LEstep.Columns.AddColumns(SdkMessageProcessingStep.PrimaryName, SdkMessageProcessingStep.Rank, SdkMessageProcessingStep.Stage);
            filterControl.GetQueryFilter(QEplugintracelog, refreshOnly);
            QEplugintracelog.AddOrder(PluginTraceLog.PerformanceExecutionStarttime, OrderType.Descending);
            QEplugintracelog.AddOrder(PluginTraceLog.CorrelationId, OrderType.Ascending);    // This just to group threads together when starting the same second
            QEplugintracelog.AddOrder(PluginTraceLog.Depth, OrderType.Descending);           // This to try to compensate for executionstarttime only accurate to the second
            return QEplugintracelog;
        }

        private void buttonRetrieveLogs_Click(object sender, EventArgs e)
        {
            RefreshTraces(GetQuery(false));
        }

        internal void GridRecordEnter(Entity record)
        {
            if (record == lasttracerecord)
            {
                return;
            }
            buttonOpenLogRecord.Enabled = record != null;
            buttonOpenLogTrace.Enabled = record != null && !string.IsNullOrWhiteSpace(record.GetAttributeValue<string>(PluginTraceLog.MessageBlock));
            buttonOpenLogException.Enabled = record != null && !string.IsNullOrWhiteSpace(record.GetAttributeValue<string>(PluginTraceLog.ExceptionDetails));
            traceControl.SetLogText(record != null && record.Contains(PluginTraceLog.MessageBlock) ? record[PluginTraceLog.MessageBlock].ToString() : "", record, filterControl.FreeTextFilter(), gridControl.crmGridView.Filtering.Text);
            exceptionControl.SetException(record != null && record.Contains(PluginTraceLog.ExceptionDetails) ? record[PluginTraceLog.ExceptionDetails].ToString() : "",
                "Exception" + (record.Contains("exceptionsummary") ? ": " + record["exceptionsummary"].ToString().Replace("\r\n", " ") : ""), filterControl.FreeTextFilter(), gridControl.crmGridView.Filtering.Text);
            statsControl.ShowStatistics(record);
            lasttracerecord = record;
        }

        internal void OpenLogRecord(Entity record)
        {
            if (record != null)
            {
                var url = GetEntityReferenceUrl(record.ToEntityReference());
                if (!string.IsNullOrEmpty(url))
                {
                    LogUse("Open log record");
                    LogInfo("Opening record: {0}", url);
                    Process.Start(url);
                }
            }
        }

        private string GetEntityReferenceUrl(EntityReference entref)
        {
            if (!string.IsNullOrEmpty(entref.LogicalName) && !entref.Id.Equals(Guid.Empty))
            {
                var url = ConnectionDetail.WebApplicationUrl;
                if (string.IsNullOrEmpty(url))
                {
                    url = string.Concat(ConnectionDetail.ServerName, "/", ConnectionDetail.Organization);
                    if (!url.ToLower().StartsWith("http"))
                    {
                        url = string.Concat("http://", url);
                    }
                }
                url = string.Concat(url.TrimEnd('/'),
                    "/main.aspx?etn=",
                    entref.LogicalName,
                    "&pagetype=entityrecord&id=",
                    entref.Id.ToString());
                return url;
            }
            return string.Empty;
        }

        private void tsmiWordWrap_CheckedChanged(object sender, EventArgs e)
        {
            traceControl.textMessage.WordWrap = tsmiWordWrap.Checked;
            exceptionControl.textException.WordWrap = tsmiWordWrap.Checked;
        }

        private void tsmiRefreshFilter_Click(object sender, EventArgs e)
        {
            LoadLogSetting();
            filterControl.LoadConstraints();
            LogUse("LoadConstraints");
        }

        private void SaveLogs()
        {
            var sfd = new SaveFileDialog
            {
                Title = "Select a location to save the logs",
                Filter = "XML file (*.xml)|*.xml"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var newfile = sfd.FileName;
                if (!string.IsNullOrEmpty(newfile))
                {
                    var serialized = EntityCollectionSerializer.Serialize(gridControl.Entities, SerializationStyle.Explicit);
                    serialized.Save(newfile);
                    MessageBox.Show(this, "Logs saved!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        internal void UpdateUI(Action action)
        {
            MethodInvoker mi = delegate
            {
                action();
            };
            if (InvokeRequired)
            {
                Invoke(mi);
            }
            else
            {
                mi();
            }
        }

        private void buttonSaveLogs_Click(object sender, EventArgs e)
        {
            SaveLogs();
        }

        private void buttonSaveFilter_Click(object sender, EventArgs e)
        {
            SaveFilter();
        }

        private void SaveFilter()
        {
            var currentfilter = filterControl.GetFilter();
            var sfd = new SaveFileDialog
            {
                Title = "Select a location to save the filter",
                Filter = "XML file (*.xml)|*.xml"
            };
            if (sfd.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(sfd.FileName))
            {
                XmlSerializerHelper.SerializeToFile(currentfilter, sfd.FileName);
                MessageBox.Show(this, "Filter saved!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private Settings GetSettings()
        {
            var ass = Assembly.GetExecutingAssembly().GetName();
            var version = ass.Version.ToString();
            return new Settings
            {
                WordWrap = tsmiWordWrap.Checked,
                Version = version,
                LocalTime = tsmiLocalTimes.Checked,
                FullyQualifiedPluginNames = tsmiFullyQualifiedPluginNames.Checked,
                HidePluginFromStep = tsmiHidePluginFromStep.Checked,
                HideEntityFromStep = tsmiHideEntityFromStep.Checked,
                HighlightIdentical = tsmiHighlight.Checked,
                HighlightColor = ColorTranslator.ToHtml(gridControl.highlightColor),
                Columns = gridControl?.Columns,
                RefreshMode = comboRefreshMode.SelectedIndex,
                ShowQuickFilter = tsmiViewQuickFilter.Checked,
                IdentifyRecords = tsmiIdentifyRecords.Checked,
                HighlightGuids = tsmiHighlightGuids.Checked
            };
        }

        private void buttonOpenFilter_Click(object sender, EventArgs e)
        {
            OpenFilter();
        }

        private void OpenFilter()
        {
            var ofd = new OpenFileDialog
            {
                Title = "Select filter file",
                Filter = "XML file (*.xml)|*.xml"
            };
            if (ofd.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(ofd.FileName))
            {
                var document = new XmlDocument();
                try
                {
                    document.Load(ofd.FileName);
                    var currentfilter = (PTVFilter)XmlSerializerHelper.Deserialize(document.OuterXml, typeof(PTVFilter));
                    filterControl.ApplyFilter(currentfilter);
                    MessageBox.Show(this, "Filter loaded!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    LogError("Fatal error at OpenFilter:\n{0}", ex.Message);
                }
            }
        }

        private void ApplySettings(Settings settings)
        {
            comboRefreshMode.Enabled = false;
            var ass = Assembly.GetExecutingAssembly().GetName();
            var version = ass.Version.ToString();
            if (!version.Equals(settings.Version))
            {
                // Reset some settings when new version is deployed
                settings.Version = version;
                settings.Columns = defaultcolumns;
                SettingsManager.Instance.Save(typeof(PluginTraceViewer), settings, "Settings");
                LogUse("ShowWelcome", ai2: true);
                Process.Start($"https://jonasr.app/PTV/releases/#{version}");
            }
            tsmiWordWrap.Checked = settings.WordWrap;
            tsmiLocalTimes.Checked = settings.LocalTime;
            tsmiFullyQualifiedPluginNames.Checked = settings.FullyQualifiedPluginNames;
            tsmiHidePluginFromStep.Checked = settings.HidePluginFromStep;
            tsmiHideEntityFromStep.Checked = settings.HideEntityFromStep;
            tsmiHighlight.Checked = settings.HighlightIdentical;
            comboRefreshMode.SelectedIndex = settings.RefreshMode;
            timerRefresh.Interval = settings.RefreshInterval;
            timerRefresh.Tag = settings.RefreshInterval;
            tsmiIdentifyRecords.Checked = settings.IdentifyRecords;
            tsmiHighlightGuids.Checked = settings.HighlightGuids;
            RefreshModeUpdated();
            try
            {
                gridControl.highlightColor = ColorTranslator.FromHtml(settings.HighlightColor);
            }
            catch
            {
                gridControl.highlightColor = ColorTranslator.FromHtml("#FFD0D0");
            }
            gridControl.crmGridView.ShowLocalTimes = settings.LocalTime;
            gridControl.Columns = settings.Columns;
            gridControl.UpdateColumnsLayout();
            gridControl.UpdateMenuChecks();
            filterControl.ShowTZInfo(settings.LocalTime);
            tsmiViewQuickFilter.Checked = settings.ShowQuickFilter;
            gridControl.panQuickFilter.Visible = settings.ShowQuickFilter;
            comboRefreshMode.Enabled = true;
        }

        private void RefreshModeUpdated()
        {
            StopRefreshTimer();
            buttonRefreshLogs.Text = "0 new";
            buttonRefreshLogs.Visible = comboRefreshMode.SelectedIndex == 1;
            StartRefreshTimer(false);
        }

        private void buttonOpenLogRecord_Click(object sender, EventArgs e)
        {
            OpenLogRecord(gridControl.crmGridView.SelectedCellRecords.FirstOrDefault());
        }

        private void SaveSettings()
        {
            var settings = GetSettings();
            SettingsManager.Instance.Save(typeof(PluginTraceViewer), settings, "Settings");
            var currentfilter = filterControl.GetFilter();
            SettingsManager.Instance.Save(typeof(PluginTraceViewer), currentfilter, ConnectionDetail?.ConnectionName);
        }

        private void buttonOpenFXB_Click(object sender, EventArgs e)
        {
            var fetchxml = GetQueryFetchXML();
            if (string.IsNullOrEmpty(fetchxml))
            {
                return;
            }
            var messageBusEventArgs = new MessageBusEventArgs("FetchXML Builder")
            {
                TargetArgument = fetchxml
            };
            OnOutgoingMessage(this, messageBusEventArgs);
        }

        private string GetQueryFetchXML()
        {
            QueryExpression query = GetQuery(false);
            if (query == null)
            {
                return string.Empty;
            }
            var resp = Service.Execute(new QueryExpressionToFetchXmlRequest() { Query = query }) as QueryExpressionToFetchXmlResponse;
            return resp.FetchXml;
        }

        private void buttonSaveQuery_Click(object sender, EventArgs e)
        {
            SaveQuery();
        }

        private void SaveQuery()
        {
            var sfd = new SaveFileDialog
            {
                Title = "Select a location to save the trace log query",
                Filter = "XML file (*.xml)|*.xml"
            };
            if (sfd.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(sfd.FileName))
            {
                var fetchxml = GetQueryFetchXML();
                if (string.IsNullOrEmpty(fetchxml))
                {
                    return;
                }
                var fetchdoc = new XmlDocument();
                fetchdoc.LoadXml(fetchxml);
                fetchdoc.Save(sfd.FileName);
                MessageBox.Show(this, "Query saved!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        internal void LogUse(string action, bool forceLog = false, double? count = null, double? duration = null, bool ai1 = true, bool ai2 = false)
        {
            if (ai1)
            {
                this.ai1.WriteEvent(action, count, duration, HandleAIResult);
            }
            if (ai2)
            {
                this.ai2.WriteEvent(action, count, duration, HandleAIResult);
            }
        }

        private void HandleAIResult(string result)
        {
            if (!string.IsNullOrEmpty(result))
            {
                LogError("Failed to write to Application Insights:\n{0}", result);
            }
        }

        internal void LogInfo(string message, params object[] args)
        {
            base.LogInfo(message, args);
        }

        internal void LogError(string message, params object[] args)
        {
            base.LogError(message, args);
        }

        private void comboLogSetting_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboLogSetting.Enabled)
            {
                return;
            }
            if (DialogResult.OK == MessageBox.Show($"Update the organization wide setting for plug-in trace log to\n\n  \"{comboLogSetting.Text}\" ?", "Confirm",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation))
            {
                UpdateLogSetting(comboLogSetting.SelectedIndex);
            }
            else
            {
                LoadLogSetting();
            }
        }

        private void tsmiPluginStats_Click(object sender, EventArgs e)
        {
            var stats = new PluginStatistics(Service);
            var result = stats.ShowDialog(this);
            if (result == DialogResult.OK && stats.SelectedPlugins != null && stats.SelectedPlugins.Count > 0)
            {
                filterControl.AddPluginFilter(stats.SelectedPlugins, true);
            }
        }

        private void tsmiFullyQualifiedPluginNames_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Setting changed. Retrieve logs again to see results.\n\nRetrieve logs now?", "Plugin Names", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                RefreshTraces(GetQuery(false));
            }
        }

        private void tsmiHidePluginFromStep_Click(object sender, EventArgs e)
        {
            RefreshTraces(GetQuery(false));
        }

        private void tsmiHideEntityFromStep_Click(object sender, EventArgs e)
        {
            RefreshTraces(GetQuery(false));
        }

        private void tsmiLocalTimes_Click(object sender, EventArgs e)
        {
            gridControl.crmGridView.ShowLocalTimes = tsmiLocalTimes.Checked;
            filterControl.ShowTZInfo(tsmiLocalTimes.Checked);
        }

        private void tsmiHighlight_Click(object sender, EventArgs e)
        {
            gridControl.Refresh();
        }

        private void tsmiViewStatistics_Click(object sender, EventArgs e)
        {
            if (filterControl.Visible)
            {
                statsControl.Show(filterControl.Pane, DockAlignment.Right, 0.4);
            }
            else
            {
                statsControl.Show(dockContainer);
            }
        }

        private void tsmiViewFilter_Click(object sender, EventArgs e)
        {
            filterControl.Show(dockContainer);
        }

        private void tsmiViewLog_Click(object sender, EventArgs e)
        {
            traceControl.Show(dockContainer);
        }

        private void tsmiViewException_Click(object sender, EventArgs e)
        {
            exceptionControl.Show(dockContainer);
        }

        private void tsmiResetWindows_Click(object sender, EventArgs e)
        {
            ResetDockLayout();
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            RefreshNewTraces(false);
        }

        private void comboRefreshMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboRefreshMode.Enabled)
            {
                return;
            }
            LogUse("RefreshMode-" + comboRefreshMode.SelectedItem);
            RefreshModeUpdated();
        }

        private void buttonRefreshLogs_Click(object sender, EventArgs e)
        {
            RefreshNewTraces(true);
        }

        private void tsmiSuppressLogSettingWarning_Click(object sender, EventArgs e)
        {
            if (tsmiSuppressLogSettingWarning.Checked)
            {
                if (MessageBox.Show("This will supress warning about log setting being active when closing Plugin Trace Viewer.\n" +
                    "This supression only applies to currently connected organization.", "Trace Log Setting",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                {
                    tsmiSuppressLogSettingWarning.Checked = false;
                }
            }
        }

        private void tsmiViewQuickFilter_Click(object sender, EventArgs e)
        {
            gridControl.panQuickFilter.Visible = tsmiViewQuickFilter.Checked;
        }

        private void buttonOpenLogException_Click(object sender, EventArgs e)
        {
            OpenLogException(gridControl.crmGridView.SelectedCellRecords.FirstOrDefault());
        }

        private void buttonOpenLogTrace_Click(object sender, EventArgs e)
        {
            OpenLogTrace(gridControl.crmGridView.SelectedCellRecords.FirstOrDefault());
        }

        private void OpenLogException(Entity record)
        {
            if (record == null || string.IsNullOrWhiteSpace(record.GetAttributeValue<string>(PluginTraceLog.ExceptionDetails)))
            {
                return;
            }
            var filename = record.TraceLogName() + ".exception.txt";
            filename = Path.Combine(Paths.LogsPath, filename);
            File.WriteAllText(filename, record.GetAttributeValue<string>(PluginTraceLog.ExceptionDetails));
            Process.Start(filename);
        }

        private static void OpenLogTrace(Entity record)
        {
            if (record == null)
            {
                return;
            }
            var filename = record.TraceLogName() + ".txt";
            filename = Path.Combine(Paths.LogsPath, filename);
            File.WriteAllText(filename, record.GetAttributeValue<string>(PluginTraceLog.MessageBlock));
            Process.Start(filename);
        }

        private void tsmiIdentifyRecords_Click(object sender, EventArgs e)
        {
            UpdateHighlighting();
        }

        private void tsmiResetColumns_Click(object sender, EventArgs e)
        {
            gridControl.Columns = defaultcolumns;
            gridControl.UpdateColumnsLayout();
            gridControl.UpdateMenuChecks();
        }

        private void tsbSupporting_Click(object sender, EventArgs e)
        {
            Supporting.ShowIf(this, true, false, ai2);
        }
    }
}