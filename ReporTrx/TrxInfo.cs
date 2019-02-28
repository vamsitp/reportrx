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
            get
            {
                return this.timesField;
            }
            set
            {
                this.timesField = value;
            }
        }

        public TestRunTestSettings TestSettings
        {
            get
            {
                return this.testSettingsField;
            }
            set
            {
                this.testSettingsField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("UnitTestResult", IsNullable = false)]
        public TestRunUnitTestResult[] Results
        {
            get
            {
                return this.resultsField;
            }
            set
            {
                this.resultsField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("UnitTest", IsNullable = false)]
        public TestRunUnitTest[] TestDefinitions
        {
            get
            {
                return this.testDefinitionsField;
            }
            set
            {
                this.testDefinitionsField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("TestEntry", IsNullable = false)]
        public TestRunTestEntry[] TestEntries
        {
            get
            {
                return this.testEntriesField;
            }
            set
            {
                this.testEntriesField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("TestList", IsNullable = false)]
        public TestRunTestList[] TestLists
        {
            get
            {
                return this.testListsField;
            }
            set
            {
                this.testListsField = value;
            }
        }

        public TestRunResultSummary ResultSummary
        {
            get
            {
                return this.resultSummaryField;
            }
            set
            {
                this.resultSummaryField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string runUser
        {
            get
            {
                return this.runUserField;
            }
            set
            {
                this.runUserField = value;
            }
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
            get
            {
                return this.creationField;
            }
            set
            {
                this.creationField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime queuing
        {
            get
            {
                return this.queuingField;
            }
            set
            {
                this.queuingField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime start
        {
            get
            {
                return this.startField;
            }
            set
            {
                this.startField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime finish
        {
            get
            {
                return this.finishField;
            }
            set
            {
                this.finishField = value;
            }
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
            get
            {
                return this.deploymentField;
            }
            set
            {
                this.deploymentField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
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
            get
            {
                return this.runDeploymentRootField;
            }
            set
            {
                this.runDeploymentRootField = value;
            }
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
            get
            {
                return this.outputField;
            }
            set
            {
                this.outputField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string executionId
        {
            get
            {
                return this.executionIdField;
            }
            set
            {
                this.executionIdField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string testId
        {
            get
            {
                return this.testIdField;
            }
            set
            {
                this.testIdField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string testName
        {
            get
            {
                return this.testNameField;
            }
            set
            {
                this.testNameField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string computerName
        {
            get
            {
                return this.computerNameField;
            }
            set
            {
                this.computerNameField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "time")]
        public System.DateTime duration
        {
            get
            {
                return this.durationField;
            }
            set
            {
                this.durationField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime startTime
        {
            get
            {
                return this.startTimeField;
            }
            set
            {
                this.startTimeField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime endTime
        {
            get
            {
                return this.endTimeField;
            }
            set
            {
                this.endTimeField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string testType
        {
            get
            {
                return this.testTypeField;
            }
            set
            {
                this.testTypeField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string outcome
        {
            get
            {
                return this.outcomeField;
            }
            set
            {
                this.outcomeField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string testListId
        {
            get
            {
                return this.testListIdField;
            }
            set
            {
                this.testListIdField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string relativeResultsDirectory
        {
            get
            {
                return this.relativeResultsDirectoryField;
            }
            set
            {
                this.relativeResultsDirectoryField = value;
            }
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
            get
            {
                return this.stdOutField;
            }
            set
            {
                this.stdOutField = value;
            }
        }

        public TestRunUnitTestResultOutputErrorInfo ErrorInfo
        {
            get
            {
                return this.errorInfoField;
            }
            set
            {
                this.errorInfoField = value;
            }
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
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }

        public string StackTrace
        {
            get
            {
                return this.stackTraceField;
            }
            set
            {
                this.stackTraceField = value;
            }
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
            get
            {
                return this.executionField;
            }
            set
            {
                this.executionField = value;
            }
        }

        public TestRunUnitTestTestMethod TestMethod
        {
            get
            {
                return this.testMethodField;
            }
            set
            {
                this.testMethodField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string storage
        {
            get
            {
                return this.storageField;
            }
            set
            {
                this.storageField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
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
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
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
            get
            {
                return this.codeBaseField;
            }
            set
            {
                this.codeBaseField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string adapterTypeName
        {
            get
            {
                return this.adapterTypeNameField;
            }
            set
            {
                this.adapterTypeNameField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string className
        {
            get
            {
                return this.classNameField;
            }
            set
            {
                this.classNameField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
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
            get
            {
                return this.testIdField;
            }
            set
            {
                this.testIdField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string executionId
        {
            get
            {
                return this.executionIdField;
            }
            set
            {
                this.executionIdField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string testListId
        {
            get
            {
                return this.testListIdField;
            }
            set
            {
                this.testListIdField = value;
            }
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
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
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
            get
            {
                return this.countersField;
            }
            set
            {
                this.countersField = value;
            }
        }

        public TestRunResultSummaryOutput Output
        {
            get
            {
                return this.outputField;
            }
            set
            {
                this.outputField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("RunInfo", IsNullable = false)]
        public TestRunResultSummaryRunInfo[] RunInfos
        {
            get
            {
                return this.runInfosField;
            }
            set
            {
                this.runInfosField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string outcome
        {
            get
            {
                return this.outcomeField;
            }
            set
            {
                this.outcomeField = value;
            }
        }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunResultSummaryCounters
    {

        private ushort totalField;

        private ushort executedField;

        private byte passedField;

        private byte failedField;

        private byte errorField;

        private byte timeoutField;

        private byte abortedField;

        private byte inconclusiveField;

        private byte passedButRunAbortedField;

        private byte notRunnableField;

        private byte notExecutedField;

        private byte disconnectedField;

        private byte warningField;

        private byte completedField;

        private byte inProgressField;

        private byte pendingField;

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort total
        {
            get
            {
                return this.totalField;
            }
            set
            {
                this.totalField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort executed
        {
            get
            {
                return this.executedField;
            }
            set
            {
                this.executedField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte passed
        {
            get
            {
                return this.passedField;
            }
            set
            {
                this.passedField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte failed
        {
            get
            {
                return this.failedField;
            }
            set
            {
                this.failedField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte error
        {
            get
            {
                return this.errorField;
            }
            set
            {
                this.errorField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte timeout
        {
            get
            {
                return this.timeoutField;
            }
            set
            {
                this.timeoutField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte aborted
        {
            get
            {
                return this.abortedField;
            }
            set
            {
                this.abortedField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte inconclusive
        {
            get
            {
                return this.inconclusiveField;
            }
            set
            {
                this.inconclusiveField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte passedButRunAborted
        {
            get
            {
                return this.passedButRunAbortedField;
            }
            set
            {
                this.passedButRunAbortedField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte notRunnable
        {
            get
            {
                return this.notRunnableField;
            }
            set
            {
                this.notRunnableField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte notExecuted
        {
            get
            {
                return this.notExecutedField;
            }
            set
            {
                this.notExecutedField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte disconnected
        {
            get
            {
                return this.disconnectedField;
            }
            set
            {
                this.disconnectedField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte warning
        {
            get
            {
                return this.warningField;
            }
            set
            {
                this.warningField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte completed
        {
            get
            {
                return this.completedField;
            }
            set
            {
                this.completedField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte inProgress
        {
            get
            {
                return this.inProgressField;
            }
            set
            {
                this.inProgressField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte pending
        {
            get
            {
                return this.pendingField;
            }
            set
            {
                this.pendingField = value;
            }
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
            get
            {
                return this.stdOutField;
            }
            set
            {
                this.stdOutField = value;
            }
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
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string computerName
        {
            get
            {
                return this.computerNameField;
            }
            set
            {
                this.computerNameField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string outcome
        {
            get
            {
                return this.outcomeField;
            }
            set
            {
                this.outcomeField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime timestamp
        {
            get
            {
                return this.timestampField;
            }
            set
            {
                this.timestampField = value;
            }
        }
    }


}
