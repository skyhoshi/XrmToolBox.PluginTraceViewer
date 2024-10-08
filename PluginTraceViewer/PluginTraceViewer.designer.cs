﻿namespace Cinteros.XTB.PluginTraceViewer
{
    partial class PluginTraceViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginTraceViewer));
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.buttonRetrieveLogs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonOpen = new System.Windows.Forms.ToolStripDropDownButton();
            this.buttonOpenFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonOpenFXB = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonOpenLogRecord = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonOpenLogTrace = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonOpenLogException = new System.Windows.Forms.ToolStripMenuItem();
            this.dropdownSave = new System.Windows.Forms.ToolStripDropDownButton();
            this.buttonSaveFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonSaveLogs = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonSaveQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.comboRefreshMode = new System.Windows.Forms.ToolStripComboBox();
            this.buttonRefreshLogs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.comboLogSetting = new System.Windows.Forms.ToolStripComboBox();
            this.tsddTraces = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiViewQuickFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLocalTimes = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiFullyQualifiedPluginNames = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHidePluginFromStep = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHideEntityFromStep = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiHighlight = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiResetColumns = new System.Windows.Forms.ToolStripMenuItem();
            this.tsddLayout = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiHighlightGuids = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiIdentifyRecords = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiWordWrap = new System.Windows.Forms.ToolStripMenuItem();
            this.tsddWindows = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiViewFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiViewStatistics = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiViewLog = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiViewException = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPluginStats = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiResetWindows = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiRefreshFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiSuppressLogSettingWarning = new System.Windows.Forms.ToolStripMenuItem();
            this.tslAbout = new System.Windows.Forms.ToolStripLabel();
            this.tsbSupporting = new System.Windows.Forms.ToolStripButton();
            this.vS2005Theme1 = new WeifenLuo.WinFormsUI.Docking.VS2005Theme();
            this.dockContainer = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMain
            // 
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonRetrieveLogs,
            this.toolStripSeparator3,
            this.buttonOpen,
            this.dropdownSave,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.comboRefreshMode,
            this.buttonRefreshLogs,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.comboLogSetting,
            this.tsddTraces,
            this.tsddLayout,
            this.tsddWindows,
            this.tslAbout,
            this.tsbSupporting});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripMain.Size = new System.Drawing.Size(1249, 39);
            this.toolStripMain.TabIndex = 24;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // buttonRetrieveLogs
            // 
            this.buttonRetrieveLogs.Enabled = false;
            this.buttonRetrieveLogs.Image = ((System.Drawing.Image)(resources.GetObject("buttonRetrieveLogs.Image")));
            this.buttonRetrieveLogs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonRetrieveLogs.Name = "buttonRetrieveLogs";
            this.buttonRetrieveLogs.Size = new System.Drawing.Size(136, 36);
            this.buttonRetrieveLogs.Text = "Retrieve Logs (F5)";
            this.buttonRetrieveLogs.ToolTipText = "Retrieve Logs <F5>";
            this.buttonRetrieveLogs.Click += new System.EventHandler(this.buttonRetrieveLogs_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // buttonOpen
            // 
            this.buttonOpen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonOpenFilter,
            this.buttonOpenFXB,
            this.toolStripSeparator5,
            this.buttonOpenLogRecord,
            this.buttonOpenLogTrace,
            this.buttonOpenLogException});
            this.buttonOpen.Image = ((System.Drawing.Image)(resources.GetObject("buttonOpen.Image")));
            this.buttonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(81, 36);
            this.buttonOpen.Text = "Open";
            // 
            // buttonOpenFilter
            // 
            this.buttonOpenFilter.Name = "buttonOpenFilter";
            this.buttonOpenFilter.Size = new System.Drawing.Size(239, 22);
            this.buttonOpenFilter.Text = "Filters...";
            this.buttonOpenFilter.Click += new System.EventHandler(this.buttonOpenFilter_Click);
            // 
            // buttonOpenFXB
            // 
            this.buttonOpenFXB.Name = "buttonOpenFXB";
            this.buttonOpenFXB.Size = new System.Drawing.Size(239, 22);
            this.buttonOpenFXB.Text = "Query in FetchXML Builder...";
            this.buttonOpenFXB.Click += new System.EventHandler(this.buttonOpenFXB_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(236, 6);
            // 
            // buttonOpenLogRecord
            // 
            this.buttonOpenLogRecord.Enabled = false;
            this.buttonOpenLogRecord.Name = "buttonOpenLogRecord";
            this.buttonOpenLogRecord.Size = new System.Drawing.Size(239, 22);
            this.buttonOpenLogRecord.Text = "Selected log record in CRM...";
            this.buttonOpenLogRecord.Click += new System.EventHandler(this.buttonOpenLogRecord_Click);
            // 
            // buttonOpenLogTrace
            // 
            this.buttonOpenLogTrace.Enabled = false;
            this.buttonOpenLogTrace.Name = "buttonOpenLogTrace";
            this.buttonOpenLogTrace.Size = new System.Drawing.Size(239, 22);
            this.buttonOpenLogTrace.Text = "Selected log record Trace...";
            this.buttonOpenLogTrace.Click += new System.EventHandler(this.buttonOpenLogTrace_Click);
            // 
            // buttonOpenLogException
            // 
            this.buttonOpenLogException.Enabled = false;
            this.buttonOpenLogException.Name = "buttonOpenLogException";
            this.buttonOpenLogException.Size = new System.Drawing.Size(239, 22);
            this.buttonOpenLogException.Text = "Selected log record Exception...";
            this.buttonOpenLogException.Click += new System.EventHandler(this.buttonOpenLogException_Click);
            // 
            // dropdownSave
            // 
            this.dropdownSave.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonSaveFilter,
            this.buttonSaveLogs,
            this.buttonSaveQuery});
            this.dropdownSave.Image = ((System.Drawing.Image)(resources.GetObject("dropdownSave.Image")));
            this.dropdownSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dropdownSave.Name = "dropdownSave";
            this.dropdownSave.Size = new System.Drawing.Size(76, 36);
            this.dropdownSave.Text = "Save";
            // 
            // buttonSaveFilter
            // 
            this.buttonSaveFilter.Name = "buttonSaveFilter";
            this.buttonSaveFilter.Size = new System.Drawing.Size(115, 22);
            this.buttonSaveFilter.Text = "Filter...";
            this.buttonSaveFilter.Click += new System.EventHandler(this.buttonSaveFilter_Click);
            // 
            // buttonSaveLogs
            // 
            this.buttonSaveLogs.Name = "buttonSaveLogs";
            this.buttonSaveLogs.Size = new System.Drawing.Size(115, 22);
            this.buttonSaveLogs.Text = "Logs...";
            this.buttonSaveLogs.Click += new System.EventHandler(this.buttonSaveLogs_Click);
            // 
            // buttonSaveQuery
            // 
            this.buttonSaveQuery.Name = "buttonSaveQuery";
            this.buttonSaveQuery.Size = new System.Drawing.Size(115, 22);
            this.buttonSaveQuery.Text = "Query...";
            this.buttonSaveQuery.Click += new System.EventHandler(this.buttonSaveQuery_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(46, 36);
            this.toolStripLabel2.Text = "Refresh";
            // 
            // comboRefreshMode
            // 
            this.comboRefreshMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboRefreshMode.DropDownWidth = 100;
            this.comboRefreshMode.Items.AddRange(new object[] {
            "Off",
            "Notify",
            "Auto"});
            this.comboRefreshMode.Name = "comboRefreshMode";
            this.comboRefreshMode.Size = new System.Drawing.Size(75, 39);
            this.comboRefreshMode.SelectedIndexChanged += new System.EventHandler(this.comboRefreshMode_SelectedIndexChanged);
            // 
            // buttonRefreshLogs
            // 
            this.buttonRefreshLogs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonRefreshLogs.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefreshLogs.Image")));
            this.buttonRefreshLogs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonRefreshLogs.Name = "buttonRefreshLogs";
            this.buttonRefreshLogs.Size = new System.Drawing.Size(42, 36);
            this.buttonRefreshLogs.Text = "0 new";
            this.buttonRefreshLogs.Visible = false;
            this.buttonRefreshLogs.Click += new System.EventHandler(this.buttonRefreshLogs_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(44, 36);
            this.toolStripLabel1.Text = "Setting";
            // 
            // comboLogSetting
            // 
            this.comboLogSetting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLogSetting.DropDownWidth = 100;
            this.comboLogSetting.Items.AddRange(new object[] {
            "Off",
            "Exception",
            "All"});
            this.comboLogSetting.Name = "comboLogSetting";
            this.comboLogSetting.Size = new System.Drawing.Size(75, 39);
            this.comboLogSetting.SelectedIndexChanged += new System.EventHandler(this.comboLogSetting_SelectedIndexChanged);
            // 
            // tsddTraces
            // 
            this.tsddTraces.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiViewQuickFilter,
            this.tsmiLocalTimes,
            this.toolStripMenuItem1,
            this.tsmiFullyQualifiedPluginNames,
            this.tsmiHidePluginFromStep,
            this.tsmiHideEntityFromStep,
            this.toolStripMenuItem2,
            this.tsmiHighlight,
            this.toolStripMenuItem6,
            this.tsmiResetColumns});
            this.tsddTraces.Image = ((System.Drawing.Image)(resources.GetObject("tsddTraces.Image")));
            this.tsddTraces.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddTraces.Name = "tsddTraces";
            this.tsddTraces.Size = new System.Drawing.Size(84, 36);
            this.tsddTraces.Text = "Traces";
            // 
            // tsmiViewQuickFilter
            // 
            this.tsmiViewQuickFilter.CheckOnClick = true;
            this.tsmiViewQuickFilter.Image = ((System.Drawing.Image)(resources.GetObject("tsmiViewQuickFilter.Image")));
            this.tsmiViewQuickFilter.Name = "tsmiViewQuickFilter";
            this.tsmiViewQuickFilter.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.tsmiViewQuickFilter.Size = new System.Drawing.Size(253, 22);
            this.tsmiViewQuickFilter.Text = "Quick Filter";
            this.tsmiViewQuickFilter.Click += new System.EventHandler(this.tsmiViewQuickFilter_Click);
            // 
            // tsmiLocalTimes
            // 
            this.tsmiLocalTimes.CheckOnClick = true;
            this.tsmiLocalTimes.Image = ((System.Drawing.Image)(resources.GetObject("tsmiLocalTimes.Image")));
            this.tsmiLocalTimes.Name = "tsmiLocalTimes";
            this.tsmiLocalTimes.Size = new System.Drawing.Size(253, 22);
            this.tsmiLocalTimes.Text = "Local Times";
            this.tsmiLocalTimes.Click += new System.EventHandler(this.tsmiLocalTimes_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(250, 6);
            // 
            // tsmiFullyQualifiedPluginNames
            // 
            this.tsmiFullyQualifiedPluginNames.CheckOnClick = true;
            this.tsmiFullyQualifiedPluginNames.Image = ((System.Drawing.Image)(resources.GetObject("tsmiFullyQualifiedPluginNames.Image")));
            this.tsmiFullyQualifiedPluginNames.Name = "tsmiFullyQualifiedPluginNames";
            this.tsmiFullyQualifiedPluginNames.Size = new System.Drawing.Size(253, 22);
            this.tsmiFullyQualifiedPluginNames.Text = "Show fully qualified Plugin names";
            this.tsmiFullyQualifiedPluginNames.Click += new System.EventHandler(this.tsmiFullyQualifiedPluginNames_Click);
            // 
            // tsmiHidePluginFromStep
            // 
            this.tsmiHidePluginFromStep.CheckOnClick = true;
            this.tsmiHidePluginFromStep.Image = ((System.Drawing.Image)(resources.GetObject("tsmiHidePluginFromStep.Image")));
            this.tsmiHidePluginFromStep.Name = "tsmiHidePluginFromStep";
            this.tsmiHidePluginFromStep.Size = new System.Drawing.Size(253, 22);
            this.tsmiHidePluginFromStep.Text = "Hide Plugin names from Steps";
            this.tsmiHidePluginFromStep.Click += new System.EventHandler(this.tsmiHidePluginFromStep_Click);
            // 
            // tsmiHideEntityFromStep
            // 
            this.tsmiHideEntityFromStep.CheckOnClick = true;
            this.tsmiHideEntityFromStep.Image = ((System.Drawing.Image)(resources.GetObject("tsmiHideEntityFromStep.Image")));
            this.tsmiHideEntityFromStep.Name = "tsmiHideEntityFromStep";
            this.tsmiHideEntityFromStep.Size = new System.Drawing.Size(253, 22);
            this.tsmiHideEntityFromStep.Text = "Hide Entity names from Steps";
            this.tsmiHideEntityFromStep.Click += new System.EventHandler(this.tsmiHideEntityFromStep_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(250, 6);
            // 
            // tsmiHighlight
            // 
            this.tsmiHighlight.Checked = true;
            this.tsmiHighlight.CheckOnClick = true;
            this.tsmiHighlight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiHighlight.Image = ((System.Drawing.Image)(resources.GetObject("tsmiHighlight.Image")));
            this.tsmiHighlight.Name = "tsmiHighlight";
            this.tsmiHighlight.Size = new System.Drawing.Size(253, 22);
            this.tsmiHighlight.Text = "Highlight identical cells";
            this.tsmiHighlight.Click += new System.EventHandler(this.tsmiHighlight_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(250, 6);
            // 
            // tsmiResetColumns
            // 
            this.tsmiResetColumns.Image = ((System.Drawing.Image)(resources.GetObject("tsmiResetColumns.Image")));
            this.tsmiResetColumns.Name = "tsmiResetColumns";
            this.tsmiResetColumns.Size = new System.Drawing.Size(253, 22);
            this.tsmiResetColumns.Text = "Reset all columns";
            this.tsmiResetColumns.Click += new System.EventHandler(this.tsmiResetColumns_Click);
            // 
            // tsddLayout
            // 
            this.tsddLayout.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiHighlightGuids,
            this.tsmiIdentifyRecords,
            this.toolStripMenuItem7,
            this.tsmiWordWrap});
            this.tsddLayout.Image = ((System.Drawing.Image)(resources.GetObject("tsddLayout.Image")));
            this.tsddLayout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddLayout.Name = "tsddLayout";
            this.tsddLayout.Size = new System.Drawing.Size(77, 36);
            this.tsddLayout.Text = "Logs";
            // 
            // tsmiHighlightGuids
            // 
            this.tsmiHighlightGuids.CheckOnClick = true;
            this.tsmiHighlightGuids.Image = ((System.Drawing.Image)(resources.GetObject("tsmiHighlightGuids.Image")));
            this.tsmiHighlightGuids.Name = "tsmiHighlightGuids";
            this.tsmiHighlightGuids.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.G)));
            this.tsmiHighlightGuids.Size = new System.Drawing.Size(240, 22);
            this.tsmiHighlightGuids.Text = "Highlight guids";
            this.tsmiHighlightGuids.Click += new System.EventHandler(this.tsmiIdentifyRecords_Click);
            // 
            // tsmiIdentifyRecords
            // 
            this.tsmiIdentifyRecords.CheckOnClick = true;
            this.tsmiIdentifyRecords.Image = ((System.Drawing.Image)(resources.GetObject("tsmiIdentifyRecords.Image")));
            this.tsmiIdentifyRecords.Name = "tsmiIdentifyRecords";
            this.tsmiIdentifyRecords.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.I)));
            this.tsmiIdentifyRecords.Size = new System.Drawing.Size(240, 22);
            this.tsmiIdentifyRecords.Text = "Identify records";
            this.tsmiIdentifyRecords.Click += new System.EventHandler(this.tsmiIdentifyRecords_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(237, 6);
            // 
            // tsmiWordWrap
            // 
            this.tsmiWordWrap.CheckOnClick = true;
            this.tsmiWordWrap.Image = ((System.Drawing.Image)(resources.GetObject("tsmiWordWrap.Image")));
            this.tsmiWordWrap.Name = "tsmiWordWrap";
            this.tsmiWordWrap.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Z)));
            this.tsmiWordWrap.Size = new System.Drawing.Size(240, 22);
            this.tsmiWordWrap.Text = "Wrap text in log window";
            this.tsmiWordWrap.CheckedChanged += new System.EventHandler(this.tsmiWordWrap_CheckedChanged);
            // 
            // tsddWindows
            // 
            this.tsddWindows.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiViewFilter,
            this.tsmiViewStatistics,
            this.tsmiViewLog,
            this.tsmiViewException,
            this.tsmiPluginStats,
            this.toolStripMenuItem3,
            this.tsmiResetWindows,
            this.toolStripMenuItem4,
            this.tsmiRefreshFilter,
            this.toolStripMenuItem5,
            this.tsmiSuppressLogSettingWarning});
            this.tsddWindows.Image = ((System.Drawing.Image)(resources.GetObject("tsddWindows.Image")));
            this.tsddWindows.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddWindows.Name = "tsddWindows";
            this.tsddWindows.Size = new System.Drawing.Size(101, 36);
            this.tsddWindows.Text = "Windows";
            // 
            // tsmiViewFilter
            // 
            this.tsmiViewFilter.Image = ((System.Drawing.Image)(resources.GetObject("tsmiViewFilter.Image")));
            this.tsmiViewFilter.Name = "tsmiViewFilter";
            this.tsmiViewFilter.Size = new System.Drawing.Size(226, 22);
            this.tsmiViewFilter.Text = "Filter";
            this.tsmiViewFilter.Click += new System.EventHandler(this.tsmiViewFilter_Click);
            // 
            // tsmiViewStatistics
            // 
            this.tsmiViewStatistics.Image = ((System.Drawing.Image)(resources.GetObject("tsmiViewStatistics.Image")));
            this.tsmiViewStatistics.Name = "tsmiViewStatistics";
            this.tsmiViewStatistics.Size = new System.Drawing.Size(226, 22);
            this.tsmiViewStatistics.Text = "Statistics";
            this.tsmiViewStatistics.Visible = false;
            this.tsmiViewStatistics.Click += new System.EventHandler(this.tsmiViewStatistics_Click);
            // 
            // tsmiViewLog
            // 
            this.tsmiViewLog.Image = ((System.Drawing.Image)(resources.GetObject("tsmiViewLog.Image")));
            this.tsmiViewLog.Name = "tsmiViewLog";
            this.tsmiViewLog.Size = new System.Drawing.Size(226, 22);
            this.tsmiViewLog.Text = "Trace Message Log";
            this.tsmiViewLog.Click += new System.EventHandler(this.tsmiViewLog_Click);
            // 
            // tsmiViewException
            // 
            this.tsmiViewException.Image = ((System.Drawing.Image)(resources.GetObject("tsmiViewException.Image")));
            this.tsmiViewException.Name = "tsmiViewException";
            this.tsmiViewException.Size = new System.Drawing.Size(226, 22);
            this.tsmiViewException.Text = "Exception";
            this.tsmiViewException.Click += new System.EventHandler(this.tsmiViewException_Click);
            // 
            // tsmiPluginStats
            // 
            this.tsmiPluginStats.Name = "tsmiPluginStats";
            this.tsmiPluginStats.Size = new System.Drawing.Size(226, 22);
            this.tsmiPluginStats.Text = "Show all Plugin Statistics...";
            this.tsmiPluginStats.Visible = false;
            this.tsmiPluginStats.Click += new System.EventHandler(this.tsmiPluginStats_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(223, 6);
            // 
            // tsmiResetWindows
            // 
            this.tsmiResetWindows.Image = ((System.Drawing.Image)(resources.GetObject("tsmiResetWindows.Image")));
            this.tsmiResetWindows.Name = "tsmiResetWindows";
            this.tsmiResetWindows.Size = new System.Drawing.Size(226, 22);
            this.tsmiResetWindows.Text = "Reset window layout";
            this.tsmiResetWindows.Click += new System.EventHandler(this.tsmiResetWindows_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(223, 6);
            // 
            // tsmiRefreshFilter
            // 
            this.tsmiRefreshFilter.Enabled = false;
            this.tsmiRefreshFilter.Image = ((System.Drawing.Image)(resources.GetObject("tsmiRefreshFilter.Image")));
            this.tsmiRefreshFilter.Name = "tsmiRefreshFilter";
            this.tsmiRefreshFilter.Size = new System.Drawing.Size(226, 22);
            this.tsmiRefreshFilter.Text = "Refresh Filter Options";
            this.tsmiRefreshFilter.Click += new System.EventHandler(this.tsmiRefreshFilter_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(223, 6);
            // 
            // tsmiSuppressLogSettingWarning
            // 
            this.tsmiSuppressLogSettingWarning.CheckOnClick = true;
            this.tsmiSuppressLogSettingWarning.Image = ((System.Drawing.Image)(resources.GetObject("tsmiSuppressLogSettingWarning.Image")));
            this.tsmiSuppressLogSettingWarning.Name = "tsmiSuppressLogSettingWarning";
            this.tsmiSuppressLogSettingWarning.Size = new System.Drawing.Size(226, 22);
            this.tsmiSuppressLogSettingWarning.Text = "Suppress log setting warning";
            this.tsmiSuppressLogSettingWarning.Click += new System.EventHandler(this.tsmiSuppressLogSettingWarning_Click);
            // 
            // tslAbout
            // 
            this.tslAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tslAbout.Image = ((System.Drawing.Image)(resources.GetObject("tslAbout.Image")));
            this.tslAbout.IsLink = true;
            this.tslAbout.Name = "tslAbout";
            this.tslAbout.Size = new System.Drawing.Size(114, 36);
            this.tslAbout.Text = "by Jonas Rapp";
            this.tslAbout.Click += new System.EventHandler(this.tslAbout_Click);
            // 
            // tsbSupporting
            // 
            this.tsbSupporting.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbSupporting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSupporting.Image = global::Cinteros.XTB.PluginTraceViewer.Properties.Resources.Supporting_icon;
            this.tsbSupporting.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSupporting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSupporting.Name = "tsbSupporting";
            this.tsbSupporting.Size = new System.Drawing.Size(56, 36);
            this.tsbSupporting.ToolTipText = "We all support these free, open-source tools - either\r\nas a company, personally, " +
    "or by contribution.";
            this.tsbSupporting.Click += new System.EventHandler(this.tsbSupporting_Click);
            // 
            // dockContainer
            // 
            this.dockContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dockContainer.DefaultFloatWindowSize = new System.Drawing.Size(600, 400);
            this.dockContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockContainer.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.dockContainer.Location = new System.Drawing.Point(0, 39);
            this.dockContainer.Name = "dockContainer";
            this.dockContainer.Size = new System.Drawing.Size(1249, 689);
            this.dockContainer.TabIndex = 32;
            // 
            // timerRefresh
            // 
            this.timerRefresh.Interval = 1000;
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
            // 
            // PluginTraceViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.dockContainer);
            this.Controls.Add(this.toolStripMain);
            this.Name = "PluginTraceViewer";
            this.PluginIcon = ((System.Drawing.Icon)(resources.GetObject("$this.PluginIcon")));
            this.Size = new System.Drawing.Size(1249, 728);
            this.TabIcon = ((System.Drawing.Image)(resources.GetObject("$this.TabIcon")));
            this.ConnectionUpdated += new XrmToolBox.Extensibility.PluginControlBase.ConnectionUpdatedHandler(this.PluginTraceViewer_ConnectionUpdated);
            this.Load += new System.EventHandler(this.PluginTraceViewer_Load);
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton buttonRetrieveLogs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton dropdownSave;
        private System.Windows.Forms.ToolStripMenuItem buttonSaveFilter;
        private System.Windows.Forms.ToolStripMenuItem buttonSaveLogs;
        private System.Windows.Forms.ToolStripDropDownButton buttonOpen;
        private System.Windows.Forms.ToolStripMenuItem buttonOpenFilter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem buttonOpenLogRecord;
        private System.Windows.Forms.ToolStripMenuItem buttonOpenFXB;
        private System.Windows.Forms.ToolStripMenuItem buttonSaveQuery;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox comboLogSetting;
        private System.Windows.Forms.ToolStripDropDownButton tsddLayout;
        private System.Windows.Forms.ToolStripMenuItem tsmiViewFilter;
        private System.Windows.Forms.ToolStripMenuItem tsmiViewStatistics;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem tsmiWordWrap;
        private System.Windows.Forms.ToolStripMenuItem tsmiRefreshFilter;
        private System.Windows.Forms.ToolStripMenuItem tsmiPluginStats;
        private WeifenLuo.WinFormsUI.Docking.VS2005Theme vS2005Theme1;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockContainer;
        private System.Windows.Forms.ToolStripMenuItem tsmiViewLog;
        private System.Windows.Forms.ToolStripMenuItem tsmiViewException;
        internal System.Windows.Forms.ToolStripMenuItem tsmiHighlight;
        internal System.Windows.Forms.ToolStripMenuItem tsmiLocalTimes;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem tsmiResetWindows;
        private System.Windows.Forms.Timer timerRefresh;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox comboRefreshMode;
        private System.Windows.Forms.ToolStripButton buttonRefreshLogs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel tslAbout;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        internal System.Windows.Forms.ToolStripMenuItem tsmiSuppressLogSettingWarning;
        private System.Windows.Forms.ToolStripMenuItem tsmiViewQuickFilter;
        private System.Windows.Forms.ToolStripMenuItem buttonOpenLogTrace;
        private System.Windows.Forms.ToolStripMenuItem buttonOpenLogException;
        internal System.Windows.Forms.ToolStripMenuItem tsmiFullyQualifiedPluginNames;
        internal System.Windows.Forms.ToolStripMenuItem tsmiHidePluginFromStep;
        internal System.Windows.Forms.ToolStripMenuItem tsmiHideEntityFromStep;
        internal System.Windows.Forms.ToolStripMenuItem tsmiIdentifyRecords;
        private System.Windows.Forms.ToolStripDropDownButton tsddWindows;
        private System.Windows.Forms.ToolStripDropDownButton tsddTraces;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem tsmiResetColumns;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        internal System.Windows.Forms.ToolStripMenuItem tsmiHighlightGuids;
        private System.Windows.Forms.ToolStripButton tsbSupporting;
    }
}
