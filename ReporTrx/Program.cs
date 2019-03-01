namespace ReporTrx
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    using HtmlTags;

    using Serilog;

    internal class Program
    {
        private const string Passed = "Passed";
        private const string Failed = "Failed";
        private const string Table = "table";
        private const string H2 = "h2";
        private const string H3 = "h3";
        private const string N2 = "N2";
        private const string Mins = " mins";
        private const string TBody = "tbody";

        private static TestRun tr;
        private static HtmlDocument doc;
        private static readonly string Style = ConfigurationManager.AppSettings[nameof(Style)];
        private static readonly int TopSlowestThresholdInMins = int.Parse(ConfigurationManager.AppSettings[nameof(TopSlowestThresholdInMins)]);

        private static void Main(string[] args)
        {
            SetLogger();
            try
            {
                var path = args[0];
                Log.Information($"Input: {path}");
                var files = Directory.GetFiles(Path.GetDirectoryName(path), Path.GetFileName(path));
                foreach (var file in files)
                {
                    ParseTestRun(file);
                    GenerateHtml();
                    Save(file);
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.ToString());
            }

            Log.Information("Done!");
            Log.CloseAndFlush();
            Console.ReadKey();
        }

        private static void ParseTestRun(string file)
        {
            Log.Information($"Parsing: {file}");
            var ser = new XmlSerializer(typeof(TestRun));
            using (var stream = File.Open(file, FileMode.Open))
            {
                tr = (TestRun)ser.Deserialize(stream);
            }
        }

        private static void GenerateHtml()
        {
            doc = new HtmlDocument();
            doc.AddStyle(Style);
            var results = tr.Results.ToList();
            var defs = tr.TestDefinitions.ToList();
            var classes = results.Select(r =>
            {
                var def = GetTestDefMatch(r, defs);
                (string Assembly, string Class, string Name, string Outcome, string Error, string Trace, TimeSpan Duration) item = (def.TestMethod.codeBase, def.TestMethod.className, r.testName, r.outcome, r.Output?.ErrorInfo?.Message, r.Output?.ErrorInfo?.StackTrace, r.duration.TimeOfDay);
                return item;
            }).GroupBy(x => x.Class).OrderBy(x => x.Key);

            AddTag(H2, "SUMMARY");
            var headerTable = AddTag(Table);
            headerTable.AddRow(new List<object> { "Name", tr.name });
            headerTable.AddRow(new List<object> { "ID", tr.id });
            headerTable.AddRow(new List<object> { "Outcome", tr.ResultSummary.outcome });
            headerTable.AddRow(new List<object> { "Start", tr.Times.start });
            headerTable.AddRow(new List<object> { "End", tr.Times.finish });
            headerTable.AddRow(new List<object> { "Duration", tr.Times.finish.Subtract(tr.Times.start).TotalMinutes.ToString(N2) + Mins });
            headerTable.AddRow(new List<object> { "TestDefinitions", defs.Count });
            headerTable.AddRow(new List<object> { "TestResults", results.Count });
            headerTable.AddRow(new List<object> { "Total", tr.ResultSummary.Counters.total });
            headerTable.AddRow(new List<object> { "Executed", tr.ResultSummary.Counters.executed });
            headerTable.AddRow(new List<object> { "NotExecuted", tr.ResultSummary.Counters.notExecuted == 0 ? tr.ResultSummary.Counters.total - (tr.ResultSummary.Counters.passed + tr.ResultSummary.Counters.failed) : tr.ResultSummary.Counters.notExecuted });
            headerTable.AddRow(new List<object> { Passed, tr.ResultSummary.Counters.passed });
            headerTable.AddRow(new List<object> { Failed, tr.ResultSummary.Counters.failed });
            headerTable.AddRow(new List<object> { "Inconclusive", tr.ResultSummary.Counters.inconclusive });
            headerTable.AddRow(new List<object> { "Pending", tr.ResultSummary.Counters.pending });
            headerTable.AddRow(new List<object> { "Timeouts", tr.ResultSummary.Counters.timeout });
            headerTable.AddRow(new List<object> { "Warnings", tr.ResultSummary.Counters.warning });

            AddTag(H2, "OVERVIEW");
            var overviewTable = AddTag(Table);
            overviewTable.Id(nameof(overviewTable));

            // overviewTable.AddRow(new List<object> { "#", "CLASS", $"TOTAL ({classes.Sum(x => x.Count())})", $"PASSED ({classes.Sum(x => x.Count(y => y.Outcome.Equals("Passed")))})", $"FAILED ({classes.Sum(x => x.Count(y => y.Outcome.Equals("Failed")))})", "PASS %" }, true);
            overviewTable.AddRow(new List<object> { "#", "CLASS", "TOTAL", "PASSED", "FAILED", "PASS %", "DURATION" }, true);
            var overviewBody = overviewTable.Add(TBody);
            var i = 0;
            foreach (var c in classes)
            {
                i++;
                overviewBody.AddRow(new List<object> { i, c.Key, c.Count(), c.Count(x => x.Outcome.Equals(Passed)), c.Count(x => x.Outcome.Equals(Failed)), $"{(100 * c.Count(x => x.Outcome.Equals(Passed))) / c.Count()}%", c.Sum(x => x.Duration.TotalMinutes).ToString(N2) + Mins}, anchors: new Dictionary<int, string> { { 1, c.Key } });
            }

            var errors = results.Select(r => r.Output?.ErrorInfo?.Message).GroupBy(x => x).Where(x => !string.IsNullOrWhiteSpace(x.Key) && x.Count() > 1).OrderByDescending(z => z.Count());
            AddTag(H2, "TOP ERRORS");
            var errorsTable = AddTag(Table);
            errorsTable.Id(nameof(errorsTable));
            errorsTable.AddRow(new List<object> { "#", "ERROR", "OCCURENCES" }, true);
            var errorsBody = errorsTable.Add(TBody);
            i = 0;
            foreach (var error in errors)
            {
                i++;
                errorsBody.AddRow(new List<object> { i, error.Key, error.Count() });
            }

            var slowest = results.OrderByDescending(r => r.duration.TimeOfDay.TotalMinutes).Where(s => s.duration.TimeOfDay.TotalMinutes > TopSlowestThresholdInMins);
            AddTag(H2, "TOP SLOWEST");
            var slowestTable = AddTag(Table);
            slowestTable.Id(nameof(slowestTable));
            slowestTable.AddRow(new List<object> { "#", "TEST", "DURATION", "CLASS" }, true);
            var slowestBody = slowestTable.Add(TBody);
            i = 0;
            foreach (var slow in slowest)
            {
                i++;
                var def = GetTestDefMatch(slow, defs);
                slowestBody.AddRow(new List<object> { i, slow.testName, slow.duration.TimeOfDay.TotalMinutes.ToString(N2) + Mins, def.TestMethod.className }, anchors: new Dictionary<int, string> { { 1, slow.testName }, { 3, def.TestMethod.className } });
            }

            var redundantTests = results.GroupBy(r => r.testName).Where(x => x.Count() > 1).OrderByDescending(z => z.Count());
            AddTag(H2, "REDUNDANT RESULTS");
            var redundantsTable = AddTag(Table);
            redundantsTable.Id(nameof(redundantsTable));
            redundantsTable.AddRow(new List<object> { "#", "TEST", "COUNT" }, true);
            i = 0;
            var redundantsBody = redundantsTable.Add(TBody);
            foreach (var redundant in redundantTests)
            {
                i++;
                redundantsBody.AddRow(new List<object> { i, redundant.Key, redundant.Count() }, anchors: new Dictionary<int, string> { { 1, redundant.Key } });
            }

            AddTag(H2, "ALL RESULTS");
            var j = 0;
            foreach (var c in classes)
            {
                j++;
                var tag = AddTag(H3, $"{j}. {c.Key} ({(100 * c.Count(x => x.Outcome.Equals(Passed))) / c.Count()}% Pass)");
                tag.Id(c.Key);
                var resultsTable = AddTag(Table);

                // resultsTable.Id(nameof(resultsTable) + "_" + c.Key);
                resultsTable.AddRow(new List<object> { "#", "NAME", "OUTCOME", "DURATION", "ERROR", "TRACE" }, true);
                i = 0;
                var resultsBody = resultsTable.Add(TBody);
                foreach (var item in c)
                {
                    i++;
                    resultsBody.AddRow(new List<object> { i, item.Name, item.Outcome, item.Duration.TotalMinutes.ToString(N2) + Mins, item.Error, item.Trace }, ids: new Dictionary<int, string> { { 1, item.Name } });
                }
            }
        }

        private static TestRunUnitTest GetTestDefMatch(TestRunUnitTestResult r, List<TestRunUnitTest> defs)
        {
            var def = defs.Where(x => r.testName.Equals(x.name))?.ToList();

            // Debug.Assert(def.Count == 1, $"Multiple definitions found for {r.testName}");
            if (def.Count > 1)
            {
                Log.Warning($"Multiple definitions found for {r.testName}");
            }

            return def.FirstOrDefault();
        }

        public static HtmlTag AddTag(string tag = "div", string text = "", HtmlTag parent = null)
        {
            var node = new HtmlTag(tag);
            if (!string.IsNullOrWhiteSpace(text))
            {
                node.Text(text);
            }

            if (parent == null)
            {
                doc.Add(node);
            }
            else
            {
                parent.Children.Add(node);
            }

            return node;
        }

        private static void Save(string file)
        {
            doc.WriteToFile($"{file}.html");
        }

        private static void SetLogger()
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console(outputTemplate: "[{Level:u3}] {Message}{NewLine}").Enrich.FromLogContext().CreateLogger();
        }
    }
}
