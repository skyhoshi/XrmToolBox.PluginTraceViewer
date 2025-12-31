using Cinteros.XTB.PluginTraceViewer.Const;
using Microsoft.Xrm.Sdk;
using System;

namespace Cinteros.XTB.PluginTraceViewer
{
    public static class Extensions
    {
        public static string TraceLogName(this Entity record)
        {
            if (record == null || record.LogicalName != PluginTraceLog.EntityName)
            {
                return string.Empty;
            }
            var name = record.GetAttributeValue<string>(PluginTraceLog.MessageName) + " " +
                record.GetAttributeValue<string>(PluginTraceLog.PrimaryEntity) + " " +
                record.GetAttributeValue<DateTime>(PluginTraceLog.PerformanceExecutionStarttime).ToString("yyyyMMddHHmmssfff");
            return name;
        }
    }
}