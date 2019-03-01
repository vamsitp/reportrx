namespace ReporTrx
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010", IsNullable = false)]
    public partial class TestRun
    {
        private TestRunTimes timesField;

        private TestRunTestSettings testSettingsField;

        private TestRunUnitTestResult[] resultsField;

        private TestRunUnitTest[] testDefinitionsField;

        private TestRunTestEntry[] testEntriesField;

        private TestRunTestList[] testListsField;

        private TestRunResultSummary resultSummaryField;

        private string idField;

        private string nameField;

        private string runUserField;

        public TestRunTimes Times
        {
            get => this.timesField;
            set => this.timesField = value;
        }

        public TestRunTestSettings TestSettings
        {
            get => this.testSettingsField;
            set => this.testSettingsField = value;
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("UnitTestResult", IsNullable = false)]
        public TestRunUnitTestResult[] Results
        {
            get => this.resultsField;
            set => this.resultsField = value;
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("UnitTest", IsNullable = false)]
        public TestRunUnitTest[] TestDefinitions
        {
            get => this.testDefinitionsField;
            set => this.testDefinitionsField = value;
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("TestEntry", IsNullable = false)]
        public TestRunTestEntry[] TestEntries
        {
            get => this.testEntriesField;
            set => this.testEntriesField = value;
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("TestList", IsNullable = false)]
        public TestRunTestList[] TestLists
        {
            get => this.testListsField;
            set => this.testListsField = value;
        }

        public TestRunResultSummary ResultSummary
        {
            get => this.resultSummaryField;
            set => this.resultSummaryField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get => this.idField;
            set => this.idField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get => this.nameField;
            set => this.nameField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string runUser
        {
            get => this.runUserField;
            set => this.runUserField = value;
        }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunTimes
    {
        private System.DateTime creationField;

        private System.DateTime queuingField;

        private System.DateTime startField;

        private System.DateTime finishField;

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime creation
        {
            get => this.creationField;
            set => this.creationField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime queuing
        {
            get => this.queuingField;
            set => this.queuingField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime start
        {
            get => this.startField;
            set => this.startField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime finish
        {
            get => this.finishField;
            set => this.finishField = value;
        }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunTestSettings
    {
        private TestRunTestSettingsDeployment deploymentField;

        private string nameField;

        private string idField;

        public TestRunTestSettingsDeployment Deployment
        {
            get => this.deploymentField;
            set => this.deploymentField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get => this.nameField;
            set => this.nameField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get => this.idField;
            set => this.idField = value;
        }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunTestSettingsDeployment
    {
        private string runDeploymentRootField;

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string runDeploymentRoot
        {
            get => this.runDeploymentRootField;
            set => this.runDeploymentRootField = value;
        }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunUnitTestResult
    {
        private TestRunUnitTestResultOutput outputField;

        private string executionIdField;

        private string testIdField;

        private string testNameField;

        private string computerNameField;

        private System.DateTime durationField;

        private System.DateTime startTimeField;

        private System.DateTime endTimeField;

        private string testTypeField;

        private string outcomeField;

        private string testListIdField;

        private string relativeResultsDirectoryField;

        public TestRunUnitTestResultOutput Output
        {
            get => this.outputField;
            set => this.outputField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string executionId
        {
            get => this.executionIdField;
            set => this.executionIdField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string testId
        {
            get => this.testIdField;
            set => this.testIdField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string testName
        {
            get => this.testNameField;
            set => this.testNameField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string computerName
        {
            get => this.computerNameField;
            set => this.computerNameField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "time")]
        public System.DateTime duration
        {
            get => this.durationField;
            set => this.durationField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime startTime
        {
            get => this.startTimeField;
            set => this.startTimeField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime endTime
        {
            get => this.endTimeField;
            set => this.endTimeField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string testType
        {
            get => this.testTypeField;
            set => this.testTypeField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string outcome
        {
            get => this.outcomeField;
            set => this.outcomeField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string testListId
        {
            get => this.testListIdField;
            set => this.testListIdField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string relativeResultsDirectory
        {
            get => this.relativeResultsDirectoryField;
            set => this.relativeResultsDirectoryField = value;
        }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunUnitTestResultOutput
    {
        private string stdOutField;

        private TestRunUnitTestResultOutputErrorInfo errorInfoField;

        public string StdOut
        {
            get => this.stdOutField;
            set => this.stdOutField = value;
        }

        public TestRunUnitTestResultOutputErrorInfo ErrorInfo
        {
            get => this.errorInfoField;
            set => this.errorInfoField = value;
        }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunUnitTestResultOutputErrorInfo
    {
        private string messageField;

        private string stackTraceField;

        public string Message
        {
            get => this.messageField;
            set => this.messageField = value;
        }

        public string StackTrace
        {
            get => this.stackTraceField;
            set => this.stackTraceField = value;
        }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunUnitTest
    {
        private TestRunUnitTestExecution executionField;

        private TestRunUnitTestTestMethod testMethodField;

        private string nameField;

        private string storageField;

        private string idField;

        public TestRunUnitTestExecution Execution
        {
            get => this.executionField;
            set => this.executionField = value;
        }

        public TestRunUnitTestTestMethod TestMethod
        {
            get => this.testMethodField;
            set => this.testMethodField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get => this.nameField;
            set => this.nameField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string storage
        {
            get => this.storageField;
            set => this.storageField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get => this.idField;
            set => this.idField = value;
        }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunUnitTestExecution
    {
        private string idField;

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get => this.idField;
            set => this.idField = value;
        }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunUnitTestTestMethod
    {
        private string codeBaseField;

        private string adapterTypeNameField;

        private string classNameField;

        private string nameField;

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string codeBase
        {
            get => this.codeBaseField;
            set => this.codeBaseField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string adapterTypeName
        {
            get => this.adapterTypeNameField;
            set => this.adapterTypeNameField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string className
        {
            get => this.classNameField;
            set => this.classNameField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get => this.nameField;
            set => this.nameField = value;
        }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunTestEntry
    {
        private string testIdField;

        private string executionIdField;

        private string testListIdField;

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string testId
        {
            get => this.testIdField;
            set => this.testIdField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string executionId
        {
            get => this.executionIdField;
            set => this.executionIdField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string testListId
        {
            get => this.testListIdField;
            set => this.testListIdField = value;
        }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunTestList
    {
        private string nameField;

        private string idField;

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get => this.nameField;
            set => this.nameField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get => this.idField;
            set => this.idField = value;
        }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunResultSummary
    {
        private TestRunResultSummaryCounters countersField;

        private TestRunResultSummaryOutput outputField;

        private TestRunResultSummaryRunInfo[] runInfosField;

        private string outcomeField;

        public TestRunResultSummaryCounters Counters
        {
            get => this.countersField;
            set => this.countersField = value;
        }

        public TestRunResultSummaryOutput Output
        {
            get => this.outputField;
            set => this.outputField = value;
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("RunInfo", IsNullable = false)]
        public TestRunResultSummaryRunInfo[] RunInfos
        {
            get => this.runInfosField;
            set => this.runInfosField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string outcome
        {
            get => this.outcomeField;
            set => this.outcomeField = value;
        }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunResultSummaryCounters
    {
        private uint totalField;

        private uint executedField;

        private int passedField;

        private int failedField;

        private int errorField;

        private int timeoutField;

        private int abortedField;

        private int inconclusiveField;

        private int passedButRunAbortedField;

        private int notRunnableField;

        private int notExecutedField;

        private int disconnectedField;

        private int warningField;

        private int completedField;

        private int inProgressField;

        private int pendingField;

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint total
        {
            get => this.totalField;
            set => this.totalField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint executed
        {
            get => this.executedField;
            set => this.executedField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int passed
        {
            get => this.passedField;
            set => this.passedField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int failed
        {
            get => this.failedField;
            set => this.failedField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int error
        {
            get => this.errorField;
            set => this.errorField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int timeout
        {
            get => this.timeoutField;
            set => this.timeoutField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int aborted
        {
            get => this.abortedField;
            set => this.abortedField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int inconclusive
        {
            get => this.inconclusiveField;
            set => this.inconclusiveField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int passedButRunAborted
        {
            get => this.passedButRunAbortedField;
            set => this.passedButRunAbortedField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int notRunnable
        {
            get => this.notRunnableField;
            set => this.notRunnableField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int notExecuted
        {
            get => this.notExecutedField;
            set => this.notExecutedField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int disconnected
        {
            get => this.disconnectedField;
            set => this.disconnectedField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int warning
        {
            get => this.warningField;
            set => this.warningField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int completed
        {
            get => this.completedField;
            set => this.completedField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int inProgress
        {
            get => this.inProgressField;
            set => this.inProgressField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int pending
        {
            get => this.pendingField;
            set => this.pendingField = value;
        }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunResultSummaryOutput
    {
        private string stdOutField;

        public string StdOut
        {
            get => this.stdOutField;
            set => this.stdOutField = value;
        }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunResultSummaryRunInfo
    {
        private string textField;

        private string computerNameField;

        private string outcomeField;

        private System.DateTime timestampField;

        public string Text
        {
            get => this.textField;
            set => this.textField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string computerName
        {
            get => this.computerNameField;
            set => this.computerNameField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string outcome
        {
            get => this.outcomeField;
            set => this.outcomeField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime timestamp
        {
            get => this.timestampField;
            set => this.timestampField = value;
        }
    }
}
