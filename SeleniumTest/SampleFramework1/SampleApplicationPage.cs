using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace SampleFramework1
{
    internal class SampleApplicationPage : BasePage
    {
        public SampleApplicationPage(IWebDriver driver) : base(driver) { }

        public bool IsVisible {
            get
            {
                return Driver.Title.Contains("Sample Application Lifecycle - Sprint 4 - Ultimate QA");
            }
            set { }
        }

        public IWebElement FirstNameField => Driver.FindElement(By.Name("firstname"));

        public IWebElement LastNameField => Driver.FindElement(By.Name("lastname"));

        public IWebElement SubmitButton => Driver.FindElement(By.XPath("//*[@type='submit']"));

        public IWebElement MaleGenderButton => Driver.FindElement(By.XPath("//*[@value='male']"));

        public IWebElement FemaleGenderButton => Driver.FindElement(By.XPath("//*[@value='female']"));

        public IWebElement OtherGenderButton => Driver.FindElement(By.XPath("//*[@value='other']"));

        public IWebElement EmergencyFirstNameField => Driver.FindElement(By.Id("f2"));
        public IWebElement EmergencyLastNameField => Driver.FindElement(By.Id("l2"));

        public IWebElement EmergencyMaleGenderButton => Driver.FindElement(By.Id("radio2-m"));
        public IWebElement EmergencyFemaleGenderButton => Driver.FindElement(By.Id("radio2-f"));
        public IWebElement EmergencyOtherGenderButton => Driver.FindElement(By.Id("radio2-0"));

        internal void Open()
        {
            Driver.Navigate().GoToUrl("https://www.ultimateqa.com/sample-application-lifecycle-sprint-4/");
            Assert.IsTrue(IsVisible);
        }

        internal UltimatQAHomePage FillOutPrimaryContactFormAndSubmit(TestUser testUser)
        {
            FirstNameField.SendKeys(testUser.FirstName);
            LastNameField.SendKeys(testUser.LastName);
            SetGender(testUser.Gender);
            SubmitButton.Click();
            return new UltimatQAHomePage(Driver);
        }

        internal UltimatQAHomePage FillOutEmergencyForm(TestUser testUser)
        {
            EmergencyFirstNameField.SendKeys(testUser.FirstName);
            EmergencyLastNameField.SendKeys(testUser.LastName);
            SetEmergencyGender(testUser.Gender);
            return new UltimatQAHomePage(Driver);
        }

        private void SetEmergencyGender(UserGender gender)
        {
            switch (gender)
            {
                case UserGender.Male:
                    {
                        EmergencyMaleGenderButton.Click();
                        break;
                    }
                case UserGender.Female:
                    {
                        EmergencyFemaleGenderButton.Click();
                        break;
                    }
                case UserGender.Other:
                    {
                        EmergencyOtherGenderButton.Click();
                        break;
                    }
                default: break;
            }
        }

        private void SetGender(UserGender gender)
        {
            switch (gender)
            {
                case UserGender.Male:
                    {
                        MaleGenderButton.Click();
                        break;
                    }
                case UserGender.Female:
                    {
                        FemaleGenderButton.Click();
                        break;
                    }
                case UserGender.Other:
                    {
                        OtherGenderButton.Click();
                        break;
                    }
                default: break;
            }

        }
    }
}