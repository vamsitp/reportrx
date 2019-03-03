namespace ReporTrx
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    public static class Constants
    {
        public const string Anchor = "a";

        public const string Failed = "Failed";

        public const string Font = "font";

        public const string H2 = "h2";

        public const string H3 = "h3";

        public const string HRef = "href";

        public const string JavaScript = "javascript";

        public const string Language = "language";

        public const string Mins = " mins";

        public const string N2 = "N2";

        public const string Passed = "Passed";

        public const string Script = "script";

        public const string Src = "src";

        public const string Table = "table";

        public const string TBody = "tbody";

        public const string Td = "td";

        public const string Th = "th";

        public const string THead = "thead";

        public const string Tr = "tr";

        public const string TypeCss = "text/css";

        public const string TypeJavaScript = "text/javascript";

        public static readonly string CellStyle = ConfigurationManager.AppSettings[nameof(CellStyle)];

        public static readonly bool ColorEntireRow = bool.Parse(ConfigurationManager.AppSettings[nameof(ColorEntireRow)]);

        public static readonly string DataTablesCssLink = ConfigurationManager.AppSettings[nameof(DataTablesCssLink)];

        public static readonly string DataTablesScriptLink = ConfigurationManager.AppSettings[nameof(DataTablesScriptLink)];

        public static readonly string JQueryLink = ConfigurationManager.AppSettings[nameof(JQueryLink)];

        public static readonly string Style = ConfigurationManager.AppSettings[nameof(Style)];

        public static readonly string TableStyle = ConfigurationManager.AppSettings[nameof(TableStyle)];

        public static readonly int TopSlowestThresholdInMins = int.Parse(ConfigurationManager.AppSettings[nameof(TopSlowestThresholdInMins)]);

        public static readonly Dictionary<string, string> OutputColors = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Passed", "green" },
            { "Failed", "red" },
            { "NotExecuted", "gray" },
            { "Inconclusive", "gray" },
            { "Pending", "gray" },
            { "Warning", "orange" },
            { "Timeout", "orange" },
            { "100%", "green" },
            { "0%", "red" }
        };
    }
}
