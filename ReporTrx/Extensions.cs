namespace ReporTrx
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    using HtmlTags;

    public static class Extensions
    {
        private const string Tr = "tr";
        private const string Th = "th";
        private const string Td = "td";
        private const string Font = "font";

        private static readonly bool ColorEntireRow = bool.Parse(ConfigurationManager.AppSettings[nameof(ColorEntireRow)]);
        private static readonly Dictionary<string, string> OutputColors = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) { { "Passed", "green" }, { "Failed", "red" }, { "NotExecuted", "gray" }, { "Inconclusive", "gray" }, { "Pending", "gray" }, { "Warning", "orange" }, { "Timeout", "orange" } };

        public static void AddRow(this HtmlTag table, IEnumerable<object> cols, bool header = false)
        {
            var row = new HtmlTag(Tr);
            table.Children.Add(row);
            foreach (var c in cols)
            {
                var col = row.Add(header ? Th : Td);
                col.ColoredText(c?.ToString() ?? string.Empty, ColorEntireRow ? cols : null);
            }
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
