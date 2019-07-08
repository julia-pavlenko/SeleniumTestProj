using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SampleFramework1
{
    [TestClass]
    [TestCategory("SampleApplication1")]
    public class SampleApplicationOneTests
    {
        private IWebDriver Driver { get; set; }

        [TestInitialize]
        public void DriverSetUp()
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            this.Driver = new ChromeDriver(outPutDirectory);
        }

        [TestMethod]
        public void Test1()
        {
            TestUser testUser = GetTestUser();

            var sampleApplicationPage = new SampleApplicationPage(Driver);
            sampleApplicationPage.Open();
            sampleApplicationPage.FillOutEmergencyForm(testUser);
            var ultimatQAHomePage = sampleApplicationPage.FillOutPrimaryContactFormAndSubmit(testUser);

            Assert.IsTrue(ultimatQAHomePage.IsVisible);
        }

        private static TestUser GetTestUser()
        {
            return new TestUser
            {
                FirstName = "Julia",
                LastName = "Pavlenko",
                Gender = UserGender.Female
            };
        }

        [TestCleanup]
        public void AfterRun()
        {
            Driver.Close();
            Driver.Quit();
        }
    }
}
