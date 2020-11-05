using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Globalization;
using System.Threading;

namespace Exercise_1___Automation_C
{
    [TestClass]
    public class UnitTest1
    {
        IWebDriver driver;
        #region Web elements
        IWebElement label;
        string locator1 = "//h2[contains(text(),'Connect with friends and the world around you on F')]";
        IWebElement Newact;
        string locator2 = "//a[contains(text(),'Create New Account')]";
        IWebElement Firstname;
        string locator3 = "u_1_b";
        IWebElement Lastname;
        string locator4 = "u_1_d";
        IWebElement Mobile;
        string locator5 = "u_1_g";

        string fakeElement = "u_1_fake";

        #endregion

        public IWebElement finder(string path)
        {
            return driver.FindElement(By.XPath(path));
        }

        public IWebElement finder(string path, string method)
        {
            switch (method)
            {
                case "Id":
                return driver.FindElement(By.Id(path));
                    

                case "Name":
                    return driver.FindElement(By.Name(path));
                    

                case "CSS":
                    return driver.FindElement(By.CssSelector(path));


                default:
                    return null;
            }
            
        }

        public void waitelement(string locator)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(locator)));
        }

        public void waitelement(string locator, string method)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            switch (method)
            {
                case "CSS":
                    wait.Until(ExpectedConditions.ElementExists(By.CssSelector(locator)));
                    break;

                case "Name":
                    wait.Until(ExpectedConditions.ElementExists(By.Name(locator)));
                    break;

                case "Id":
                    wait.Until(ExpectedConditions.ElementExists(By.Id(locator)));
                    break;
            }  
        }

        //NoSuchElementException
        public void waitelement(string locator, int flag)
        {
            if (flag == 1)
            {
                try
                {
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
                    wait.Until(ExpectedConditions.ElementExists(By.Id(locator)));
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n\nThe element was not found: NoSuchElementException");
                }
            }
            else
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.ElementExists(By.Id(locator)));
            }
            
            
            
        }

        public void entertext(IWebElement element, string keys)
        {
            element.SendKeys(keys);
            Console.WriteLine("The {0} was filled out with {1}", element.GetAttribute("Name"), keys);
        }

        public void doclick(IWebElement element)
        {
            element.Click();
        }

        
        public void myassert(string actualtext, string expectedtext)
        {
            if (actualtext.Equals(expectedtext))
                Console.WriteLine("The Text validation was success");
        }


        [TestInitialize]
        public void BrowserFactory()
        {
            string browser = "Chrome";
            switch (browser)
            {
                case "Chrome":
                    driver = new ChromeDriver();
                    break;

                case "Firefox":
                    driver = new FirefoxDriver();
                    break;

                default:
                    Console.WriteLine("No a browser");
                    break;
            }
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.facebook.com/");
        }

        [TestMethod]
        public void TestMethod1()
        {
            
            
            waitelement(locator1);
            label = finder(locator1);
            string text = "Connect with friends and the world around you on Facebook.";
            Assert.AreEqual(label.Text, text);
            myassert(label.Text, text);
            
            waitelement(locator2);
            Newact = finder(locator2);
            doclick(Newact);
            Thread.Sleep(1000);
            
            
            waitelement(locator3,"Id");
            Firstname = finder(locator3,"Id");
            entertext(Firstname, "Norberto");
            Thread.Sleep(1000);
            
            
            waitelement(locator4, "Id");
            Lastname = finder(locator4,"Id");
            entertext(Lastname, "Cabrera");
            Thread.Sleep(1000);
            
            
            waitelement(locator5, "Id");
            Mobile = finder(locator5,"Id");
            entertext(Mobile, "4771390913");
            Thread.Sleep(1000);

            waitelement(fakeElement, 1);
        }

        [TestCleanup()]
        public void testcleanp()
        {
            driver.Quit();
        }
    }
}
