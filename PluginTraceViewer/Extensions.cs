using Cinteros.XTB.PluginTraceViewer.Const;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Cinteros.XTB.PluginTraceViewer
{
    public static class Extensions
    {
        public static List<T> AllChildren<T>(this Control control) where T : Control
        {
            var result = new List<T>();
            if (control is T)
            {
                result.Add((T)control);
            }
            foreach (Control child in control.Controls)
            {
                result.AddRange(child.AllChildren<T>());
            }
            return result;
        }

        public static bool Contains(this EntityCollection entities, Guid id)
        {
            return entities.Entities.Any(e => e.Id.Equals(id));
        }

        public static void Remove(this EntityCollection entities, Guid id)
        {
            var i = 0;
            while (i < entities.Entities.Count)
            {
                if (entities.Entities[i].Id.Equals(id))
                {
                    entities.Entities.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }

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

        public static void HighlightFilter(this RichTextBox textbox, string text, Color bg, Color fr)
        {
            if (textbox == null)
            {
                throw new ArgumentNullException(nameof(textbox));
            }
            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }

            // Preserve user selection/caret.
            var originalSelectionStart = textbox.SelectionStart;
            var originalSelectionLength = textbox.SelectionLength;

            var length = text.Length;
            var start = 0;

            // IMPORTANT:
            // RichTextBox.Find() does not necessarily move the SelectionStart.
            // If we don’t explicitly select the found match, SelectionStart can stay the same,
            // and subsequent Find() calls can return the same position, causing an infinite loop.
            while (start <= textbox.TextLength - length)
            {
                var pos = textbox.Find(text, start, RichTextBoxFinds.None);
                if (pos < 0)
                {
                    break;
                }

                textbox.Select(pos, length);
                textbox.SelectionBackColor = bg;
                textbox.SelectionColor = fr;

                // Ensure forward progress even if something odd happens.
                start = pos + Math.Max(1, length);
            }

            textbox.Select(originalSelectionStart, originalSelectionLength);
        }
    }
}