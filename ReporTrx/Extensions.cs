namespace ReporTrx
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using HtmlTags;

    public static class Extensions
    {
        public static void AddRow(this HtmlTag table, IEnumerable<object> cols, bool header = false, Dictionary<int, string> ids = null, Dictionary<int, string> anchors = null)
        {
            var row = new HtmlTag(Constants.Tr);
            if (header)
            {
                var thead = table.Add(Constants.THead);
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
                var col = row.Add(header ? Constants.Th : Constants.Td);
                col.AddClass(Constants.CellStyle);
                if (anchors?.ContainsKey(i) == true)
                {
                    var anchor = new HtmlTag(Constants.Anchor);
                    anchor.Attr(Constants.HRef, $"#{anchors[i]}");
                    anchor.Text(val);
                    col.Children.Add(anchor);
                }
                else
                {
                    col.ColoredText(val ?? string.Empty, Constants.ColorEntireRow ? cols : null);
                }

                if (ids?.ContainsKey(i) == true)
                {
                    col.Id(ids[i]);
                }
            }
        }

        public static HtmlTag ToDataTable(this HtmlTag table, string id)
        {
            id = id.Replace(".", "_");
            table.AddClass(Constants.TableStyle);
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
                    var sibling = siblings?.SingleOrDefault(x => x != null && Constants.OutputColors.ContainsKey(x.ToString()));
                    if (sibling != null)
                    {
                        color = Constants.OutputColors[sibling.ToString()];
                    }
                }

                if (color == null && Constants.OutputColors.ContainsKey(text))
                {
                    color = Constants.OutputColors[text];
                }

                if (color != null)
                {
                    var font = new HtmlTag(Constants.Font);
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

        public static string ToMinutesString(this TimeSpan duration)
        {
            return duration.TotalMinutes.ToMinutesString();
        }

        public static string ToMinutesString(this double duration)
        {
            return duration.ToString(Constants.N2) + Constants.Mins;
        }
    }
}
