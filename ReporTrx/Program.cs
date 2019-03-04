namespace ReporTrx
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml.Serialization;

    using HtmlTags;

    using Serilog;

    internal class Program
    {
        private static TestRun tr;
        private static HtmlDocument doc;
        private static readonly ConcurrentDictionary<string, List<(string Namespace, string Feature, string TestName, string Scenario, string Owner)>> TestsPerAssembly = new ConcurrentDictionary<string, List<(string Namespace, string Feature, string TestName, string Scenario, string Owner)>>();

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

            Console.WriteLine(string.Empty);
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
            CreateDoc();
            var results = tr.Results.ToList();
            var defs = tr.TestDefinitions.ToList();
            var classes = results.Select(r =>
            {
                var def = GetTestDefMatch(r, defs);
                var owner = GetOwner(def.TestMethod.codeBase, r.testName);
                (string Assembly, string Class, string Name, string Outcome, string Error, string Trace, TimeSpan Duration, string Owner) item = (def.TestMethod.codeBase, def.TestMethod.className, r.testName, r.outcome, r.Output?.ErrorInfo?.Message, r.Output?.ErrorInfo?.StackTrace, r.duration.TimeOfDay, owner);
                return item;
            });

            var groupedClasses = classes.GroupBy(x => x.Class).OrderBy(x => x.Key).ToList();
            PopulateTables(results, defs, groupedClasses);
        }

        private static void CreateDoc()
        {
            doc = new HtmlDocument();
            doc.Head.Add("link").Attr(Constants.HRef, Constants.DataTablesCssLink).Attr("rel", "stylesheet").Attr("type", Constants.TypeCss);
            doc.AddStyle(Constants.Style);
            doc.Head.Add("style").Attr("class", "init").Attr("type", Constants.TypeCss);
            doc.Head.Add(Constants.Script).Attr(Constants.Language, Constants.JavaScript).Attr(Constants.Src, Constants.JQueryLink).Attr("type", Constants.TypeJavaScript);
            doc.Head.Add(Constants.Script).Attr(Constants.Language, Constants.JavaScript).Attr(Constants.Src, Constants.DataTablesScriptLink).Attr("type", Constants.TypeJavaScript);
            var init = "$(document).ready(function() { $('table." + Constants.TableStyle.Split(' ').FirstOrDefault() + "').DataTable({ \"lengthMenu\": [[-1, 50, 25, 10], [\"All\", 50, 25, 10]] }); } );";
            doc.AddJavaScript(init).Attr("class", "init");
        }

        private static void PopulateTables(List<TestRunUnitTestResult> results, List<TestRunUnitTest> defs, List<IGrouping<string, (string Assembly, string Class, string Name, string Outcome, string Error, string Trace, TimeSpan Duration, string Owner)>> classes)
        {
            PopulateSummaryTable(results, defs);
            PopulateOverviewTable(classes);
            PopulateErrorsTable(results);
            PopulateSlowestTable(results, defs);
            PopulateRedundantTable(results);
            PopulateAllResultsTable(classes);
        }
        private static void PopulateSummaryTable(List<TestRunUnitTestResult> results, List<TestRunUnitTest> defs)
        {
            AddTag(Constants.H2, "SUMMARY");
            var summaryTable = AddTag(Constants.Table).ToDataTable("summaryTable");
            summaryTable.AddRow(new List<object> { "KEY", "VALUE" }, true);
            var summaryBody = summaryTable.Add(Constants.TBody);
            summaryBody.AddRow(new List<object> { "Name", tr.name });
            summaryBody.AddRow(new List<object> { "ID", tr.id });
            summaryBody.AddRow(new List<object> { "User", tr.runUser });
            summaryBody.AddRow(new List<object> { "Outcome", tr.ResultSummary.outcome });
            summaryBody.AddRow(new List<object> { "Start", tr.Times.start });
            summaryBody.AddRow(new List<object> { "End", tr.Times.finish });
            summaryBody.AddRow(new List<object> { "Duration", tr.Times.finish.Subtract(tr.Times.start).ToMinutesString() });
            summaryBody.AddRow(new List<object> { "TestDefinitions", defs.Count });
            summaryBody.AddRow(new List<object> { "TestResults", results.Count });
            summaryBody.AddRow(new List<object> { "Total", tr.ResultSummary.Counters.total });
            summaryBody.AddRow(new List<object> { "Executed", tr.ResultSummary.Counters.executed });
            summaryBody.AddRow(new List<object> { "NotExecuted", tr.ResultSummary.Counters.notExecuted == 0 ? tr.ResultSummary.Counters.total - (tr.ResultSummary.Counters.passed + tr.ResultSummary.Counters.failed) : tr.ResultSummary.Counters.notExecuted });
            summaryBody.AddRow(new List<object> { Constants.Passed, tr.ResultSummary.Counters.passed });
            summaryBody.AddRow(new List<object> { Constants.Failed, tr.ResultSummary.Counters.failed });
            summaryBody.AddRow(new List<object> { "Inconclusive", tr.ResultSummary.Counters.inconclusive });
            summaryBody.AddRow(new List<object> { "Pending", tr.ResultSummary.Counters.pending });
            summaryBody.AddRow(new List<object> { "Timeouts", tr.ResultSummary.Counters.timeout });
            summaryBody.AddRow(new List<object> { "Warnings", tr.ResultSummary.Counters.warning });
        }

        private static void PopulateOverviewTable(List<IGrouping<string, (string Assembly, string Class, string Name, string Outcome, string Error, string Trace, TimeSpan Duration, string Owner)>> classes)
        {
            AddTag(Constants.H2, $"OVERVIEW ({classes.Count})");
            var overviewTable = AddTag(Constants.Table).ToDataTable("overviewTable");

            // overviewTable.AddRow(new List<object> { "#", "CLASS", $"TOTAL ({classes.Sum(x => x.Count())})", $"PASSED ({classes.Sum(x => x.Count(y => y.Outcome.Equals("Passed")))})", $"FAILED ({classes.Sum(x => x.Count(y => y.Outcome.Equals("Failed")))})", "PASS %" }, true);
            overviewTable.AddRow(new List<object> { "#", "CLASS", "TOTAL", "PASSED", "FAILED", "PASS %", "DURATION", "OWNER" }, true);
            var overviewBody = overviewTable.Add(Constants.TBody);
            var i = 0;
            foreach (var c in classes)
            {
                i++;
                overviewBody.AddRow(new List<object> { i, c.Key, c.Count(), c.Count(x => x.Outcome.Equals(Constants.Passed)), c.Count(x => x.Outcome.Equals(Constants.Failed)), $"{(100 * c.Count(x => x.Outcome.Equals(Constants.Passed))) / c.Count()}%", c.Sum(x => x.Duration.TotalMinutes).ToMinutesString(), string.Join(Constants.Space, c.Select(o => o.Owner).Distinct()) }, anchors: new Dictionary<int, string> { { 1, c.Key } });
            }
        }

        private static void PopulateErrorsTable(List<TestRunUnitTestResult> results)
        {
            var errors = results.Select(r => r.Output?.ErrorInfo?.Message).GroupBy(x => x).Where(x => !string.IsNullOrWhiteSpace(x.Key) && x.Count() > 1).OrderByDescending(z => z.Count());
            AddTag(Constants.H2, "TOP ERRORS");
            var errorsTable = AddTag(Constants.Table).ToDataTable("errorsTable");
            errorsTable.AddRow(new List<object> { "#", "ERROR", "OCCURENCES" }, true);
            var errorsBody = errorsTable.Add(Constants.TBody);
            var i = 0;
            foreach (var error in errors)
            {
                i++;
                errorsBody.AddRow(new List<object> { i, error.Key, error.Count() });
            }
        }

        private static void PopulateSlowestTable(List<TestRunUnitTestResult> results, List<TestRunUnitTest> defs)
        {
            var slowest = results.OrderByDescending(r => r.duration.TimeOfDay.TotalMinutes).Where(s => s.duration.TimeOfDay.TotalMinutes > Constants.TopSlowestThresholdInMins);
            AddTag(Constants.H2, "TOP SLOWEST");
            var slowestTable = AddTag(Constants.Table).ToDataTable("slowestTable");
            slowestTable.AddRow(new List<object> { "#", "TEST", "DURATION", "CLASS" }, true);
            var slowestBody = slowestTable.Add(Constants.TBody);
            var i = 0;
            foreach (var slow in slowest)
            {
                i++;
                var def = GetTestDefMatch(slow, defs);
                slowestBody.AddRow(new List<object> { i, slow.testName, slow.duration.TimeOfDay.TotalMinutes.ToMinutesString(), def.TestMethod.className }, anchors: new Dictionary<int, string> { { 1, slow.testName }, { 3, def.TestMethod.className } });
            }
        }

        private static void PopulateRedundantTable(List<TestRunUnitTestResult> results)
        {
            var redundantTests = results.GroupBy(r => r.testName).Where(x => x.Count() > 1).OrderByDescending(z => z.Count());
            AddTag(Constants.H2, "REDUNDANT RESULTS");
            var redundantsTable = AddTag(Constants.Table).ToDataTable("redundantsTable");
            redundantsTable.AddRow(new List<object> { "#", "TEST", "COUNT" }, true);
            var i = 0;
            var redundantsBody = redundantsTable.Add(Constants.TBody);
            foreach (var redundant in redundantTests)
            {
                i++;
                redundantsBody.AddRow(new List<object> { i, redundant.Key, redundant.Count() }, anchors: new Dictionary<int, string> { { 1, redundant.Key } });
            }
        }

        private static void PopulateAllResultsTable(List<IGrouping<string, (string Assembly, string Class, string Name, string Outcome, string Error, string Trace, TimeSpan Duration, string Owner)>> classes)
        {
            AddTag(Constants.H2, "ALL RESULTS");
            var j = 0;
            foreach (var c in classes)
            {
                j++;
                var tag = AddTag(Constants.H3, $"{j}. {c.Key} ({(100 * c.Count(x => x.Outcome.Equals(Constants.Passed))) / c.Count()}% Pass)");
                tag.Id(c.Key);
                var resultsTable = AddTag(Constants.Table).ToDataTable($"resultsTable_{c.Key}");

                // resultsTable.Id(nameof(resultsTable) + "_" + c.Key);
                resultsTable.AddRow(new List<object> { "#", "NAME", "OUTCOME", "DURATION", "OWNER", "ERROR", "TRACE" }, true);
                var i = 0;
                var resultsBody = resultsTable.Add(Constants.TBody);
                foreach (var item in c)
                {
                    i++;
                    resultsBody.AddRow(new List<object> { i, item.Name, item.Outcome, item.Duration.ToMinutesString(), item.Owner, item.Error, item.Trace }, ids: new Dictionary<int, string> { { 1, item.Name } });
                }
            }
        }

        private static TestRunUnitTest GetTestDefMatch(TestRunUnitTestResult r, List<TestRunUnitTest> defs)
        {
            var def = defs.Where(x => r.testName.Equals(x.name))?.ToList();

            // Debug.Assert(def.Count == 1, $"Multiple definitions found for {r.testName}");
            if (def.Count > 1)
            {
                Console.WriteLine(string.Empty);
                Log.Warning($"Multiple ({def.Count}) definitions found for {r.testName}");
                def.ForEach(d => Log.Warning($"\t- {d.TestMethod.className}"));
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

        private static string GetOwner(string testAssemblyPath, string testName)
        {
            const string ownerPrefix = "owner=";
            var tests = TestsPerAssembly.GetOrAdd(testAssemblyPath, k =>
            {
                var testAssembly = Assembly.LoadFrom(testAssemblyPath);
                var types = testAssembly.GetTypes().Where(x => x.Name.EndsWith("Feature", StringComparison.OrdinalIgnoreCase));

                // var methods = types.SelectMany(x => x.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)).Where(m => m.DeclaringType != typeof(object) && !m.IsSpecialName && m.CustomAttributes.Any(z => z.AttributeType.Name.Contains("DescriptionAttribute")));
                var methods = types.Select(x =>
                new
                {
                    Owner = x.CustomAttributes?.FirstOrDefault(y => y.ConstructorArguments?.FirstOrDefault().Value?.ToString()?.StartsWith(ownerPrefix, StringComparison.OrdinalIgnoreCase) == true)?.ConstructorArguments?.FirstOrDefault().Value?.ToString().Replace(ownerPrefix, string.Empty),
                    Methods = x.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).Where(m => m.DeclaringType != typeof(object) && !m.IsSpecialName && m.CustomAttributes.Any(z => z.AttributeType.Name.Contains("DescriptionAttribute")))
                });

                var cad = new List<(string Namespace, string Feature, string TestName, string Scenario, string Owner)>();
                foreach (var m in methods)
                {
                    cad.AddRange(m.Methods.Select(x =>
                    {
                        var desc = x.CustomAttributes?.FirstOrDefault(y => y?.AttributeType.Name.Contains("DescriptionAttribute") == true);
                        var cat = x.CustomAttributes?.FirstOrDefault(y => y?.ConstructorArguments?.FirstOrDefault().Value?.ToString()?.StartsWith(ownerPrefix, StringComparison.OrdinalIgnoreCase) == true);
                        var ownr = cat?.ConstructorArguments?.FirstOrDefault().Value?.ToString()?.Replace(ownerPrefix, string.Empty) ?? m.Owner;
                        var item = (Namespace: x.ReflectedType.Namespace, Feature: x.ReflectedType.Name, TestName: x.Name, Scenario: desc?.ConstructorArguments?.FirstOrDefault().Value?.ToString(), Owner: ownr);
                        return item;
                    }).ToList());
                }

                return cad;
            });

            var owners = tests.Where(t => t.TestName.Equals(testName.Split('(').FirstOrDefault())).Select(x => x.Owner).Distinct().ToList();
            if (owners.Count > 1)
            {
                Console.WriteLine(string.Empty);
                Log.Warning($"Multiple ({owners.Count}) owners found for {testName}");
                owners.ForEach(o => Log.Warning($"\t- {o}"));
            }

            return string.Join(Constants.Space, owners);
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
