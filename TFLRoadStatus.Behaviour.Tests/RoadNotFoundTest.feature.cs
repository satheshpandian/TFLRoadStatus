﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.4.0.0
//      SpecFlow Generator Version:2.4.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------

#region Designer generated code

using TechTalk.SpecFlow;

#pragma warning disable
namespace TFLRoadStatus.Behaviour.Tests
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.4.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [TechTalk.SpecRun.FeatureAttribute("RoadNotFoundTest And ExitCode Test", Description =
        "\tWhen an invalid road ID is passed\r\n\tThe application has to return HttpStatusCode" +
        " as 404", SourceFile = "RoadNotFoundTest.feature", SourceLine = 0)]
    public partial class RoadNotFoundTestAndExitCodeTestFeature
    {
        private TechTalk.SpecFlow.ITestRunner testRunner;

        [TechTalk.SpecRun.FeatureInitialize()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(
                new System.Globalization.CultureInfo("en-US"), "RoadNotFoundTest And ExitCode Test",
                "\tWhen an invalid road ID is passed\r\n\tThe application has to return HttpStatusCode" +
                " as 404", ProgrammingLanguage.CSharp, ((string[]) (null)));
            testRunner.OnFeatureStart(featureInfo);
        }

        [TechTalk.SpecRun.FeatureCleanup()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }

        public virtual void TestInitialize()
        {
        }

        [TechTalk.SpecRun.ScenarioCleanup()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }

        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
        }

        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }

        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }

        [TechTalk.SpecRun.ScenarioAttribute("HttpErrorCode 404", new string[]
        {
            "errorCode404"
        }, SourceLine = 5)]
        public virtual void HttpErrorCode404()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("HttpErrorCode 404", null,
                new string[]
                {
                    "errorCode404"
                });
#line 6
            this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 7
            testRunner.Given("an invalid road ID \'A223\' is passed", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 8
            testRunner.When("the client is run", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 9
            testRunner.Then("the application should return an informative error", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [TechTalk.SpecRun.ScenarioAttribute("Non Zero Exit code", new string[]
        {
            "error"
        }, SourceLine = 11)]
        public virtual void NonZeroExitCode()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Non Zero Exit code", null,
                new string[]
                {
                    "error"
                });
#line 12
            this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 13
            testRunner.Given("an invalid road ID \'A223\' is passed", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 14
            testRunner.When("the client is run", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 15
            testRunner.Then("the application should exit with a non-zero system Error code", ((string) (null)),
                ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [TechTalk.SpecRun.TestRunCleanup()]
        public virtual void TestRunCleanup()
        {
            TechTalk.SpecFlow.TestRunnerManager.GetTestRunner().OnTestRunEnd();
        }
    }
}
#pragma warning restore

#endregion