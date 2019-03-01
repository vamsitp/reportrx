namespace ReporTrx
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    using HtmlTags;

    internal class Program
    {
        private static TestRun tr;
        private static HtmlDocument doc;
        private static readonly string Style = ConfigurationManager.AppSettings[nameof(Style)];
        private static readonly int TopSlowestThresholdInMins = int.Parse(ConfigurationManager.AppSettings[nameof(TopSlowestThresholdInMins)]);

        private static void Main(string[] args)
        {
            try
            {
                var path = args[0];
                Console.WriteLine($"Input: {path}");
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
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("Done");
            Console.ReadKey();
        }

        private static void ParseTestRun(string file)
        {
            Console.WriteLine($"Parsing: {file}");
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

            AddTag("h2", "SUMMARY");
            var headerTable = AddTag("table");

            headerTable.AddRow(new List<object> { "Name", tr.name });
            headerTable.AddRow(new List<object> { "ID", tr.id });
            headerTable.AddRow(new List<object> { "Outcome", tr.ResultSummary.outcome });
            headerTable.AddRow(new List<object> { "Start", tr.Times.start });
            headerTable.AddRow(new List<object> { "End", tr.Times.finish });
            headerTable.AddRow(new List<object> { "Duration", tr.Times.finish.Subtract(tr.Times.start).TotalMinutes.ToString("N2") + " mins" });
            headerTable.AddRow(new List<object> { "TestDefinitions", defs.Count });
            headerTable.AddRow(new List<object> { "TestResults", results.Count });
            headerTable.AddRow(new List<object> { "Total", tr.ResultSummary.Counters.total });
            headerTable.AddRow(new List<object> { "Executed", tr.ResultSummary.Counters.executed });
            headerTable.AddRow(new List<object> { "NotExecuted", tr.ResultSummary.Counters.notExecuted });
            headerTable.AddRow(new List<object> { "Passed", tr.ResultSummary.Counters.passed});
            headerTable.AddRow(new List<object> { "Failed", tr.ResultSummary.Counters.failed });
            headerTable.AddRow(new List<object> { "Inconclusive", tr.ResultSummary.Counters.inconclusive });
            headerTable.AddRow(new List<object> { "Pending", tr.ResultSummary.Counters.pending });
            headerTable.AddRow(new List<object> { "Timeouts", tr.ResultSummary.Counters.timeout });
            headerTable.AddRow(new List<object> { "Warnings", tr.ResultSummary.Counters.warning });

            var redundantTests = results.GroupBy(r => r.testName).Where(x => x.Count() > 1).OrderByDescending(z => z.Count());
            AddTag("h2", "REDUNDANT RESULTS");
            var redundantsTable = AddTag("table");
            var i = 0;
            foreach (var redundant in redundantTests)
            {
                i++;
                redundantsTable.AddRow(new List<object> { i, redundant.Key, redundant.Count() });
            }

            var errors = results.Select(r => r.Output?.ErrorInfo?.Message).GroupBy(x => x).Where(x => !string.IsNullOrWhiteSpace(x.Key) && x.Count() > 1).OrderByDescending(z => z.Count());
            AddTag("h2", "TOP ERRORS");
            var errorsTable = AddTag("table");
            i = 0;
            foreach (var error in errors)
            {
                i++;
                errorsTable.AddRow(new List<object> { i, error.Key, error.Count() });
            }

            var slowest = results.OrderByDescending(r => r.duration.TimeOfDay.TotalMinutes).Where(s => s.duration.TimeOfDay.TotalMinutes > TopSlowestThresholdInMins);
            AddTag("h2", "TOP SLOWEST");
            var slowestTable = AddTag("table");
            i = 0;
            foreach (var slow in slowest)
            {
                i++;
                slowestTable.AddRow(new List<object> { i, slow.testName, slow.duration.TimeOfDay.TotalMinutes.ToString("N2") + " mins"});
            }

            AddTag("h2", "ALL RESULTS");
            var resultsTable = AddTag("table");
            resultsTable.AddRow(new List<object> { "#", "NAME", "OUTCOME", "ERROR", "TRACE" }, true);
            i = 0;
            foreach (var res in results)
            {
                i++;
                var def = defs.SingleOrDefault(d => res.testName.Equals(d.name));
                resultsTable.AddRow(new List<object> { i, res.testName, res.outcome, res.Output?.ErrorInfo?.Message, res.Output?.ErrorInfo?.StackTrace });
            }
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
    }
}
