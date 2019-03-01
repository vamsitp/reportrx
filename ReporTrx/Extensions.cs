namespace ReporTrx
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;

    using HtmlTags;

    public static class Extensions
    {
        private const string Tr = "tr";
        private const string Th = "th";
        private const string Td = "td";
        private const string Font = "font";
        private const string Anchor = "a";
        private const string THead = "thead";
        private const string HRef = "href";
        private const string DataTable = " $(\"#{0}\").DataTable({{ \"lengthMenu\": [[-1, 50, 25], [\"All\", 50, 25]] }});";
        private const string DisplayCompact = "display compact";
        private const string Width = "width";
        private const string Cent = "100%";

        private static readonly bool ColorEntireRow = bool.Parse(ConfigurationManager.AppSettings[nameof(ColorEntireRow)]);
        private static readonly Dictionary<string, string> OutputColors = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Passed", "green" },
            { "Failed", "red" },
            { "NotExecuted", "gray" },
            { "Inconclusive", "gray" },
            { "Pending", "gray" },
            { "Warning", "orange" },
            { "Timeout", "orange" },
            { Cent, "green" },
            { "0%", "red" }
        };

        public static void AddRow(this HtmlTag table, IEnumerable<object> cols, bool header = false, Dictionary<int, string> ids = null, Dictionary<int, string> anchors = null)
        {
            var row = new HtmlTag(Tr);
            if (header)
            {
                var thead = table.Add(THead);
                thead.Children.Add(row);
            }
            else
            {
                table.Children.Add(row);
            }

            for (var i = 0; i < cols.Count(); i++)
            {
                var c = cols.ElementAt(i);
                var val = c?.ToString();
                var col = row.Add(header ? Th : Td);
                col.AddClass("cell expand-maximum-on-hover"); //.AddStyle("word-break", "break-all");
                if (anchors?.ContainsKey(i) == true)
                {
                    var anchor = new HtmlTag(Anchor);
                    anchor.Attr(HRef, $"#{anchors[i]}");
                    anchor.Text(val);
                    col.Children.Add(anchor);
                }
                else
                {
                    col.ColoredText(val ?? string.Empty, ColorEntireRow ? cols : null);
                }

                if (ids?.ContainsKey(i) == true)
                {
                    col.Id(ids[i]);
                }
            }
        }

        public static HtmlTag ToDataTable(this HtmlTag table, string id, StringBuilder initScript)
        {
            id = id.Replace(".", "_");
            initScript.AppendFormat(DataTable, id);
            table.AddClass(DisplayCompact).AddStyle(Width, Cent); // .AddStyle("table-layout", "fixed").AddStyle("word-wrap", "break-word");
            return table.Id(id);
        }

        public static IList<HtmlTag> AddStyle(this HtmlTag tag, Dictionary<string, string> styles)
        {
            var results = new List<HtmlTag>();
            foreach (var style in styles)
            {
                results.Add(tag.AddStyle(style.Key, style.Value));
            }

            return results;
        }

        public static HtmlTag AddStyle(this HtmlTag tag, string key, string value)
        {
            return tag.Style(key, value);
        }

        public static void ColoredText(this HtmlTag tag, string text, IEnumerable<object> siblings = null)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                string color = null;
                if (siblings != null)
                {
                    var sibling = siblings?.SingleOrDefault(x => x != null && OutputColors.ContainsKey(x.ToString()));
                    if (sibling != null)
                    {
                        color = OutputColors[sibling.ToString()];
                    }
                }

                if (color == null && OutputColors.ContainsKey(text))
                {
                    color = OutputColors[text];
                }

                if (color != null)
                {
                    var font = new HtmlTag(Font);
                    font.Attr(nameof(color), color);
                    font.Text(text);
                    tag.Children.Add(font);
                }
                else
                {
                    tag.Text(text);
                }
            }
        }
    }
}
