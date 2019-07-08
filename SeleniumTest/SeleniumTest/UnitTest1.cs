using System;
using System.IO;
using System.Reflection;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using OpenQA.Selenium.Interactions;

namespace SeleniumTest
{
    [TestClass]
    public class UnitTest1
    {
        IWebDriver driver;
        
        [TestMethod]
        public void SelectorsQuiz()
        {
            driver = GetChromeDriver();
            driver.Navigate().GoToUrl("https://www.ultimateqa.com/simple-html-elements-for-automation/");
            HighliteElementUsingJavaScript(By.Id("simpleElementsLink"));
            HighliteElementUsingJavaScript(By.CssSelector("#simpleElementsLink"));
            HighliteElementUsingJavaScript(By.XPath("//a[@id='simpleElementsLink']"));
            HighliteElementUsingJavaScript(By.LinkText("Click this link"));
            HighliteElementUsingJavaScript(By.PartialLinkText("Click this l"));
            HighliteElementUsingJavaScript(By.Name("clickableLink"));
            driver.Close();
        }

        [TestMethod]
        public void XPathQuiz()
        {
            driver = GetChromeDriver();
            driver.Navigate().GoToUrl("https://www.ultimateqa.com/simple-html-elements-for-automation/");

            driver.FindElement(By.XPath("//*[@name='gender'][@value='male']")).Click();

            driver.FindElement(By.XPath("//*[@type='checkbox'][@value='Bike']")).Click();

            driver.FindElement(By.XPath("//*[@value='volvo']/..")).Click();
            driver.FindElement(By.XPath("//*[@value='saab']")).Click();

            driver.FindElement(By.XPath("//*[@class='et_pb_tab_1']/a")).Click();
            var element = driver.FindElement(By.XPath("//*[@class='et_pb_tab et_pb_tab_1 clearfix et-pb-active-slide']/div"));
            Assert.AreEqual(element.Text, "Tab 2 content");

            HighliteElementUsingJavaScript(By.XPath("//*[@id='htmlTableId']//td[contains(text(),'$150,000+')]"));

            var elementInSecondTable = driver.FindElements(By.XPath("//table//td[contains(text(),'$150,000+')]"))[1];
            HighliteElementUsingJavaScript(elementInSecondTable);

            driver.Close();
        }

        [TestMethod]
        public void Navigation()
        {
            driver = GetChromeDriver();
            driver.Navigate().GoToUrl("https://www.ultimateqa.com/simple-html-elements-for-automation/");
            driver.Navigate().Back();
            driver.Navigate().Forward();
            driver.Navigate().Refresh();
        }

        [TestMethod]
        public void NavigationQuiz()
        {
            driver = GetChromeDriver();
            driver.Navigate().GoToUrl("https://www.ultimateqa.com");
            Assert.AreEqual("Home - Ultimate QA", driver.Title);
            
            driver.Navigate().GoToUrl("https://www.ultimateqa.com/automation/");
            Assert.AreEqual("Automation Practice - Ultimate QA", driver.Title);

            driver.Navigate().Refresh();

            driver.FindElement(By.XPath("//a[@href='../complicated-page']")).Click();
            Assert.AreEqual("Complicated Page - Ultimate QA", driver.Title);
            driver.Navigate().Back();
            Assert.AreEqual("Automation Practice - Ultimate QA", driver.Title);
        }

        [TestMethod]
        public void ElementsManipulationQuiz()
        {
            driver = GetChromeDriver();
            driver.Navigate().GoToUrl("https://www.ultimateqa.com/filling-out-forms/");

            var nameField = driver.FindElement(By.Id("et_pb_contact_name_1"));
            nameField.Clear();
            nameField.SendKeys("Julia");

            var messageField = driver.FindElement(By.Id("et_pb_contact_message_1"));
            messageField.Clear();
            messageField.SendKeys("Any text");

            var captcha = driver.FindElement(By.XPath("//*[@class='input et_pb_contact_captcha']"));
            int firstInt  = Convert.ToInt32(captcha.GetAttribute("data-first_digit"));
            int secondInt = Convert.ToInt32(captcha.GetAttribute("data-second_digit"));
            captcha.Clear();
            int result = firstInt + secondInt;
            captcha.SendKeys(result.ToString());

            driver.FindElements(By.XPath("//*[@class='et_pb_contact_submit et_pb_button']"))[1].Click();
        }

        [TestMethod]
        public void WindowInterrogation()
        {
            driver = GetChromeDriver();
            driver.Navigate().GoToUrl("https://www.ultimateqa.com/simple-html-elements-for-automation/");
            var uRL = driver.Url;
            var title = driver.Title;
            var currentWindowHandle = driver.CurrentWindowHandle;
            var windowHandles = driver.WindowHandles;
        }

        [TestMethod]
        public void ElementsInterrogation()
        {
            driver = GetChromeDriver();
            driver.Navigate().GoToUrl("https://www.ultimateqa.com/simple-html-elements-for-automation/");
            driver.Manage().Window.Maximize();

            var element = driver.FindElement(By.Id("button1"));
            Assert.AreEqual("submit", element.GetAttribute("type"));
            Assert.AreEqual("normal", element.GetCssValue("letter-spacing"));
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            Assert.IsFalse(element.Selected);
            Assert.AreEqual("Click Me!", element.Text);
            Assert.AreEqual("button", element.TagName);
            Assert.AreEqual(24, element.Size.Height);
            Assert.AreEqual(341, element.Location.X);
            Assert.AreEqual(262, element.Location.Y);
        }

        [TestMethod]
        public void ImplicitWaits()
        {
            driver = GetChromeDriver();
            driver.Navigate().GoToUrl("https://www.ultimateqa.com/simple-html-elements-for-automation/");

            //ImplicitWait tells WebDriver to poll the DOM for a certain amount of time when trying to find an element
            //ImplicitWait doesn't work when element is hidden in DOM
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [TestMethod]
        public void ExplicitWait()
        {
            driver = GetChromeDriver();
            driver.Navigate().GoToUrl("https://www.ultimateqa.com/simple-html-elements-for-automation/");

            //ExplicitWait is the code that you write to wait for a certain condition to occure before proceeding
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            IWebElement element = wait.Until((d) =>
            {
                return d.FindElement(By.Id("success"));
            });
        }

        [TestMethod]
        public void ExplicitWaitWithExpectedCondition()
        {
            driver = GetChromeDriver();
            driver.Navigate().GoToUrl("https://www.ultimateqa.com/simple-html-elements-for-automation/");

            //ExplicitWait is the code that you write to wait for a certain condition to occure before proceeding
            OpenQA.Selenium.Support.UI.WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("success"))).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(By.Id("success"))).Displayed);
        }

        [TestMethod]
        public void DragAndDropExample1()
        {
            driver = GetChromeDriver();
            var actions = new Actions(driver);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(6));

            driver.Navigate().GoToUrl("http://jqueryui.com/droppable/");
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.ClassName("demo-frame")));

            IWebElement targetElement = driver.FindElement(By.Id("droppable"));
            IWebElement sourceElement = driver.FindElement(By.Id("draggable"));
            actions.DragAndDrop(sourceElement, targetElement).Perform();

            //or you can build this first and then perform the action
            //var dragAndDrop = actions.DragAndDrop(sourceElement, targetElement).Build();
            //dragAndDrop.Perform();

            Assert.AreEqual("Dropped!", targetElement.Text);
        }

        [TestMethod]
        public void DragAndDropExample2()
        {
            driver = GetChromeDriver();
            var actions = new Actions(driver);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(6));

            driver.Navigate().GoToUrl("http://jqueryui.com/droppable/");
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.ClassName("demo-frame")));

            IWebElement sourceElement = driver.FindElement(By.Id("draggable"));
            IWebElement targetElement = driver.FindElement(By.Id("droppable"));

            var drag = actions
                .ClickAndHold(sourceElement)
                .MoveToElement(targetElement)
                .Release(targetElement)
                .Build();

            drag.Perform();

            Assert.AreEqual("Dropped!", targetElement.Text);
        }

        [TestMethod]
        public void DragAndDropQuiz()
        {
            driver = GetChromeDriver();
            driver.Navigate().GoToUrl("http://www.pureexample.com/jquery-ui/basic-droppable.html");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.Id("ExampleFrame-94")));

            var actions = new Actions(driver);
            IWebElement sourceElement = driver.FindElement(By.XPath("//div[@class='square ui-draggable']"));
            IWebElement targetElement = driver.FindElement(By.XPath("//div[@class='squaredotted ui-droppable']"));
            actions.DragAndDrop(sourceElement, targetElement).Perform();

            IWebElement infoElement = driver.FindElement(By.Id("info"));
            Assert.AreEqual("dropped!", infoElement.Text);
        }

        [TestMethod]
        public void ResizingExample()
        {
            driver = GetChromeDriver();
            driver.Navigate().GoToUrl("http://jqueryui.com/resizable/");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.ClassName("demo-frame")));

            IWebElement resizeHandle = driver.FindElement(By.XPath("//*[@class='ui-resizable-handle ui-resizable-se ui-icon ui-icon-gripsmall-diagonal-se']"));

            var actions = new Actions(driver);
            actions.ClickAndHold(resizeHandle).MoveByOffset(100, 100).Perform();

            Assert.IsTrue(driver.FindElement(By.XPath("//*[@id='resizable' and @style]")).Displayed);
        }

        [TestMethod]
        public void DragAndDropQuiz2()
        {
            driver = GetChromeDriver();
            driver.Navigate().GoToUrl("http://the-internet.herokuapp.com/drag_and_drop");
            
            IWebElement sourceElement = driver.FindElement(By.Id("column-a"));
            IWebElement targetElement = driver.FindElement(By.Id("column-b"));

            var actions = new Actions(driver);
            actions.ClickAndHold(sourceElement).MoveToElement(targetElement).Perform();
            //actions.DragAndDrop(sourceElement, targetElement).Perform();
        }

        private IWebDriver GetChromeDriver()
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return new ChromeDriver(outPutDirectory);
        }


        private void HighliteElementUsingJavaScript(By locationStrategy, int duration = 2)
        {
            var element = driver.FindElement(locationStrategy);
            HighliteElementUsingJavaScript(element, duration);
        }

        private void HighliteElementUsingJavaScript(IWebElement element, int duration = 2)
        {

            var origineStyle = element.GetAttribute("style");
            IJavaScriptExecutor javaScriptExecutor = driver as IJavaScriptExecutor;
            string highlightJavascript = @"arguments[0].style.cssText = ""border-width: 2px; border-style: solid; border-color: red"";";
            javaScriptExecutor.ExecuteScript(highlightJavascript, new object[] { element });

            if (duration <= 0)
                return;
            Thread.Sleep(TimeSpan.FromSeconds(duration));
            javaScriptExecutor.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2])",
                element,
                "style",
                origineStyle);
        }

        [TestCleanup]
        public void AfterRun()
        {
            driver.Close();
            driver.Quit();
        }
    }
}
