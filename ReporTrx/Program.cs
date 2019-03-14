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
        private static HtmlDocument doc;
        private static TestRun tr;
        private static List<TestResult> TestResults;
        private static List<IGrouping<string, TestResult>> TestResultsByClass;
        private static readonly ConcurrentDictionary<string, List<TestResult>> TestsPerAssembly = new ConcurrentDictionary<string, List<TestResult>>();

        private static void Main(string[] args)
        {
            SetLogger();
            try
            {
                var path = args[0];
                Console.WriteLine();
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
            Console.WriteLine();
            Log.Information($"Parsing: {file}");
            Console.WriteLine();
            var testAssemblyPathOverride = Constants.TestAssemblyFolderOverride;
            var ser = new XmlSerializer(typeof(TestRun));
            using (var stream = File.Open(file, FileMode.Open))
            {
                tr = (TestRun)ser.Deserialize(stream);
                var defNotFound = 1;
                TestResults = tr.TestEntries.Select(e =>
                {
                    var result = tr.Results.SingleOrDefault(x => e.testId.Equals(x.testId) && tr.TestDefinitions.Any(y => x.executionId.Equals(y.Execution.id)));
                    var def = tr.TestDefinitions.SingleOrDefault(x => e.executionId.Equals(x.Execution.id));
                    var assembly = string.IsNullOrWhiteSpace(testAssemblyPathOverride) ? def?.TestMethod?.codeBase : Path.Combine(testAssemblyPathOverride, Path.GetFileName(def?.TestMethod?.codeBase));
                    var item = new TestResult(assembly, def?.TestMethod?.className, result.testName, result.outcome, result.Output?.ErrorInfo?.Message, result.Output?.ErrorInfo?.StackTrace, result.duration.TimeOfDay);
                    if (def != null)
                    {
                        SetOwner(item);
                    }
                    else
                    {
                        Log.Warning($"{defNotFound++}. TestDefinition not found for: {result.testName} (testId={result.testId}, executionId={result.executionId})");
                        item = null;
                    }

                    return item;
                }).Where(x => x != null).ToList();

                TestResultsByClass = TestResults.GroupBy(x => x.ClassName).OrderBy(x => x.Key).ToList();
            }
        }

        private static void GenerateHtml()
        {
            CreateDoc();
            PopulateTables();
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

        private static void PopulateTables()
        {
            PopulateSummaryTable();
            PopulateOverviewTable();
            PopulateOwnersTable();
            PopulateErrorsTable();
            PopulateSlowestTable();
            PopulateRedundantTable();
            PopulateAllResultsTable();
        }
        private static void PopulateSummaryTable()
        {
            AddTag(Constants.H2, "SUMMARY");
            var summaryTable = AddTag(Constants.Table).ToDataTable("summaryTable");
            summaryTable.AddRow(new List<object> { "#", "KEY", "VALUE" }, true);
            var summaryBody = summaryTable.Add(Constants.TBody);
            var items = new List<List<object>>()
            {
                new List<object> { "Name", tr.name },
                new List<object> { "ID", tr.id },
                new List<object> { "User", tr.runUser },
                new List<object> { "Outcome", tr.ResultSummary.outcome },
                new List<object> { "Start", tr.Times.start },
                new List<object> { "End", tr.Times.finish },
                new List<object> { "Duration", tr.Times.finish.Subtract(tr.Times.start).ToMinutesString() },
                new List<object> { "TestDefinitions", tr.TestDefinitions.Count() },
                new List<object> { "TestResults", tr.Results.Count() },
                new List<object> { "ResultsWithDefitions", TestResults.Count },
                new List<object> { "Total", tr.ResultSummary.Counters.total },
                new List<object> { "Executed", tr.ResultSummary.Counters.executed },
                new List<object> { "NotExecuted", tr.ResultSummary.Counters.notExecuted == 0 ? tr.ResultSummary.Counters.total - (tr.ResultSummary.Counters.passed + tr.ResultSummary.Counters.failed) : tr.ResultSummary.Counters.notExecuted },
                new List<object> { Constants.Passed, tr.ResultSummary.Counters.passed },
                new List<object> { Constants.Failed, tr.ResultSummary.Counters.failed },
                new List<object> { "Inconclusive", tr.ResultSummary.Counters.inconclusive },
                new List<object> { "Pending", tr.ResultSummary.Counters.pending },
                new List<object> { "Timeouts", tr.ResultSummary.Counters.timeout },
                new List<object> { "Warnings", tr.ResultSummary.Counters.warning }
            };

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                summaryBody.AddRow(new List<object> {i + 1, item[0], item[1] });
            }
        }

        private static void PopulateOverviewTable()
        {
            AddTag(Constants.H2, $"OVERVIEW ({TestResultsByClass.Count})");
            var overviewTable = AddTag(Constants.Table).ToDataTable("overviewTable");

            // overviewTable.AddRow(new List<object> { "#", "CLASS", $"TOTAL ({classes.Sum(x => x.Count())})", $"PASSED ({classes.Sum(x => x.Count(y => y.Outcome.Equals("Passed")))})", $"FAILED ({classes.Sum(x => x.Count(y => y.Outcome.Equals("Failed")))})", "PASS %" }, true);
            overviewTable.AddRow(new List<object> { "#", "CLASS", "TOTAL", "PASSED", "FAILED", "PASS %", "DURATION", "OWNER" }, true);
            var overviewBody = overviewTable.Add(Constants.TBody);
            var i = 0;
            foreach (var c in TestResultsByClass)
            {
                i++;
                overviewBody.AddRow(new List<object> { i, c.Key, c.Count(), c.Count(x => x.Outcome.Equals(Constants.Passed)), c.Count(x => x.Outcome.Equals(Constants.Failed)), $"{(100 * c.Count(x => x.Outcome.Equals(Constants.Passed))) / c.Count()}%", c.Sum(x => x.Duration.TotalMinutes()).ToMinutesString(), string.Join(Constants.Space, c.Select(o => o.Owner).Distinct()) }, anchors: new Dictionary<int, string> { { 1, c.Key } });
            }
        }

        private static void PopulateOwnersTable()
        {
            var owners = TestResults.GroupBy(x => x.Owner)?.OrderBy(x => x.Key)?.ToList();
            if (owners?.Count > 0)
            {
                AddTag(Constants.H2, $"OWNERS ({owners.Count})");
                var ownersTable = AddTag(Constants.Table).ToDataTable("ownersTable");
                ownersTable.AddRow(new List<object> { "#", "OWNER", "TOTAL", "PASSED", "FAILED", "PASS %" }, true);
                var ownersBody = ownersTable.Add(Constants.TBody);
                var i = 0;
                foreach (var o in owners)
                {
                    i++;
                    ownersBody.AddRow(new List<object> { i, o.Key, o.Count(), o.Count(x => x.Outcome.Equals(Constants.Passed)), o.Count(x => x.Outcome.Equals(Constants.Failed)), $"{(100 * o.Count(x => x.Outcome.Equals(Constants.Passed))) / o.Count()}%" });
                }
            }
        }

        private static void PopulateErrorsTable()
        {
            var errors = TestResults.Select(r => r.Error).GroupBy(x => x).Where(x => !string.IsNullOrWhiteSpace(x.Key) && x.Count() > 1).OrderByDescending(z => z.Count()).ToList();
            AddTag(Constants.H2, $"TOP ERRORS ({errors.Count})");
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

        private static void PopulateSlowestTable()
        {
            var slowest = TestResults.OrderByDescending(r => r.Duration.TotalMinutes()).Where(s => s.Duration.TotalMinutes() > Constants.TopSlowestThresholdInMins).ToList();
            AddTag(Constants.H2, $"TOP SLOWEST ({slowest.Count} > {Constants.TopSlowestThresholdInMins} MINS)");
            var slowestTable = AddTag(Constants.Table).ToDataTable("slowestTable");
            slowestTable.AddRow(new List<object> { "#", "TEST", "DURATION", "CLASS" }, true);
            var slowestBody = slowestTable.Add(Constants.TBody);
            var i = 0;
            foreach (var slow in slowest)
            {
                i++;
                slowestBody.AddRow(new List<object> { i, slow.TestName, slow.Duration.TotalMinutes().ToMinutesString(), slow.ClassName }, anchors: new Dictionary<int, string> { { 1, slow.TestName }, { 3, slow.ClassName } });
            }
        }

        private static void PopulateRedundantTable()
        {
            var redundantTests = TestResults.GroupBy(r => r.TestName).Where(x => x.Count() > 1).OrderByDescending(z => z.Count()).ToList();
            AddTag(Constants.H2, $"REDUNDANT RESULTS ({redundantTests.Count})");
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

        private static void PopulateAllResultsTable()
        {
            AddTag(Constants.H2, $"ALL RESULTS ({TestResultsByClass.Sum(x => x.Count())} / {TestResultsByClass.Count} CLASSES)");
            var j = 0;
            foreach (var c in TestResultsByClass)
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
                    resultsBody.AddRow(new List<object> { i, item.TestName, item.Outcome, item.Duration.TotalMinutes().ToMinutesString(), item.Owner, item.Error, item.Trace }, ids: new Dictionary<int, string> { { 1, item.TestName } });
                }
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

        private static void SetOwner(TestResult testResult)
        {
            const string ownerPrefix = "owner=";
            var tests = TestsPerAssembly.GetOrAdd(testResult.Assembly, k =>
            {
                if (File.Exists(k))
                {
                    var testAssembly = Assembly.LoadFrom(k);
                    var types = testAssembly.GetTypes().Where(x => x.Name.EndsWith("Feature", StringComparison.OrdinalIgnoreCase));

                    // var methods = types.SelectMany(x => x.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)).Where(m => m.DeclaringType != typeof(object) && !m.IsSpecialName && m.CustomAttributes.Any(z => z.AttributeType.Name.Contains("DescriptionAttribute")));
                    var methods = types.Select(x =>
                    new
                    {
                        Owner = x.CustomAttributes?.FirstOrDefault(y => y.ConstructorArguments?.FirstOrDefault().Value?.ToString()?.StartsWith(ownerPrefix, StringComparison.OrdinalIgnoreCase) == true)?.ConstructorArguments?.FirstOrDefault().Value?.ToString().Replace(ownerPrefix, string.Empty),
                        Methods = x.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).Where(m => m.DeclaringType != typeof(object) && !m.IsSpecialName && m.CustomAttributes.Any(z => z.AttributeType.Name.Contains("DescriptionAttribute")))
                    });

                    var cad = new List<TestResult>();
                    foreach (var m in methods)
                    {
                        cad.AddRange(m.Methods.Select(x =>
                        {
                            var desc = x.CustomAttributes?.FirstOrDefault(y => y?.AttributeType.Name.Contains("DescriptionAttribute") == true);
                            var cat = x.CustomAttributes?.FirstOrDefault(y => y?.ConstructorArguments?.FirstOrDefault().Value?.ToString()?.StartsWith(ownerPrefix, StringComparison.OrdinalIgnoreCase) == true);
                            var owner = cat?.ConstructorArguments?.FirstOrDefault().Value?.ToString()?.Replace(ownerPrefix, string.Empty) ?? m.Owner;
                            var item = new TestResult(testResult.Assembly, x.ReflectedType.FullName, x.Name, owner: owner);
                            return item;
                        }).ToList());
                    }

                    return cad;
                }
                else
                {
                    Log.Warning($"Assembly not found (override the value of 'TestAssemblyFolderOverride' in the config if this is expected): {k}");
                    return null;
                }
            });

            var owners = tests?.Where(t => t.TestName.Equals(testResult.TestName.Split('(').FirstOrDefault()) && t.ClassName.Equals(testResult.ClassName))?.Select(x => x.Owner?.Trim())?.Where(x => !string.IsNullOrWhiteSpace(x))?.Distinct()?.ToList();
            if (owners != null)
            {
                testResult.Owner = string.Join(Constants.Space, owners).Trim();
            }
            ////if (owners.Count > 1)
            ////{
            ////    Console.WriteLine(string.Empty);
            ////    Log.Warning($"Multiple ({owners.Count}) owners found for {testResult.TestName}");
            ////    owners.ForEach(o => Log.Warning($"\t- {o}"));
            ////}
        }

        private static void Save(string file)
        {
            doc.WriteToFile($"{file}.html");
            Console.WriteLine();
            Log.Information($"File saved to: {file}.html");
        }

        private static void SetLogger()
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console(outputTemplate: "[{Level:u3}] {Message}{NewLine}").Enrich.FromLogContext().CreateLogger();
        }
    }
}
