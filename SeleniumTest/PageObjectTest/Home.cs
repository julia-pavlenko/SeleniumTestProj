using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectTest
{
    class Home
    {
        private IWebDriver _driver;

        public Home(IWebDriver driver)
        {
            _driver = driver;
        }

        public Home OpenHomePage()
        {
            _driver.Navigate().GoToUrl("https://www.ultimateqa.com/");
            return this;
        }

        public Home Search(string value)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("et_search_icon"))).Click();


            _driver.FindElement(By.Id("et_search_icon")).Click();
            IWebElement searchField = _driver.FindElement(By.ClassName("et-search-field"));
            searchField.Click();
            searchField.Clear();
            searchField.SendKeys(value);
            Actions builder = new Actions(_driver);
            builder.SendKeys(Keys.Enter);
            return this;
        }


    }
}
