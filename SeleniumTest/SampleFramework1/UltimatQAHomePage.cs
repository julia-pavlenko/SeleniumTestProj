using System;
using OpenQA.Selenium;

namespace SampleFramework1
{
    internal class UltimatQAHomePage : BasePage
    {
        public bool IsVisible => Driver.Title.Contains("Home - Ultimate QA");

        public UltimatQAHomePage(IWebDriver driver) : base(driver) { }
    }
}