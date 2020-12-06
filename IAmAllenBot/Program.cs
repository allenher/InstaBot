using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace IAmAllenBot
{
    public class Program
    {
        #region 
        const string username = "iamallenbot@gmail.com";
        const string password = "12345spaceballs";
        #endregion
        private const string UploadInstagramImageFilePath = "C:/Users/allen/OneDrive/Documents/CodingProjects/InstaBot/uploadImageForInstagram";
        private const string DeleteInstagramImageFilePath = "C:/Users/allen/OneDrive/Documents/CodingProjects/InstaBot/deleteImageForInstagram";
        static void Main(string[] args)
        {
            //ChromeDriver driver = SetUpDriver();
            string url = "www.reddit.com";
            try
            {
                WebScraper(url);
                //Login(driver);
                //ByPassInitialSetup(driver);
                //UploadToInstagram(driver);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Page did not load");
            }

            //driver.Quit();
        }
        public static ChromeDriver SetUpDriver()
        { 
            ChromeDriver driver;
            ChromeOptions chromeCapabilities = new ChromeOptions();
            chromeCapabilities.EnableMobileEmulation("Pixel 2");
            driver = new ChromeDriver(chromeCapabilities);
            driver.Navigate().GoToUrl("https://www.instagram.com/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(8);

            return driver;
        }

        public static void Login(ChromeDriver driver)
        {
            Actions action = new Actions(driver);

            //'Inital Login button
            var loginButton = driver.FindElementByXPath("/html/body/div[1]/section/main/article/div/div/div/div[2]/button");
            action.MoveToElement(loginButton).Click().Build().Perform();
            action = new Actions(driver);

            //'Username;
            var usernameTextField = driver.FindElementByXPath("/html/body/div[1]/section/main/article/div/div/div/form/div[1]/div[3]/div/label/input");
            action.MoveToElement(usernameTextField).Click();
            action.Perform();
            action.SendKeys(username);
            action.Perform();
            action = new Actions(driver);

            //'Password
            var passwordTextField = driver.FindElementByXPath("/html/body/div[1]/section/main/article/div/div/div/form/div[1]/div[4]/div/label/input");
            action.MoveToElement(passwordTextField).Click();
            action.Perform();
            action.SendKeys(password);
            action.Perform();
            action = new Actions(driver);

            //'Login button
            loginButton = driver.FindElementByXPath("/html/body/div[1]/section/main/article/div/div/div/form/div[1]/div[6]/button");
            action.MoveToElement(loginButton).Click().Build().Perform();
        }

        public static void ByPassInitialSetup(ChromeDriver driver)
        {
            Actions action = new Actions(driver);

            Thread.Sleep(2000);

            //' Exit out of login info
            var saveLoginInfo = IsElementPresent(driver,"/html/body/div[1]/section/main/div/div/div/button");
            if (saveLoginInfo == null)
            {
                return;
            }
            action.MoveToElement(saveLoginInfo).Click();
            action.Perform();
            action = new Actions(driver);

            Thread.Sleep(1000);

            //' Exit out of home screen setup
            var homeScreenInfo = IsElementPresent(driver,"/html/body/div[4]/div/div/div/div[3]/button[2]");
            if (homeScreenInfo == null)
            {
                return;
            }
            action.MoveToElement(homeScreenInfo).Click();
            action.Perform();
            action = new Actions(driver);

            Thread.Sleep(1000);

            //' turn off notifications
            var notifactionSettings = IsElementPresent(driver,"/html/body/div[4]/div/div/div/div[3]/button[2]");
            if (notifactionSettings == null)
            {
                return;
            }
            action.MoveToElement(notifactionSettings).Click();
            action.Perform();
            action = new Actions(driver);

            Thread.Sleep(1000);
        }

        public static void UploadToInstagram(ChromeDriver driver)
        {
            Actions action = new Actions(driver);

            //'TODO: Need to grab all photo names
            List<string> photoNames = new List<string>()
            {
                "woah.png"
            };

            string path = FilePath + photoNames[0];

            var uploadButton = IsElementPresent(driver, "/html/body/div[1]/section/nav[2]/div/div/div[2]/div/div/div[3]");
            if (uploadButton == null)
            {
                return;
            }
            action.MoveToElement(uploadButton).Click();
            action.Perform();
            action = new Actions(driver);

            Thread.Sleep(500);

            Process process = new Process();
            process.StartInfo.FileName = UploadInstagramImageFilePath;
            process.Start();
            Thread.Sleep(1000);

            var editPageNext = IsElementPresent(driver, "/html/body/div[1]/section/div[1]/header/div/div[2]/button");
            action.MoveToElement(editPageNext).Click();
            action.Perform();
            action = new Actions(driver);

            Thread.Sleep(500);

            var shareButton = IsElementPresent(driver, "/html/body/div[1]/section/div[1]/header/div/div[2]/button");
            action.MoveToElement(shareButton).Click();
            action.Perform();
            action = new Actions(driver);

            //' delete picture uploaded
            process.StartInfo.FileName = DeleteInstagramImageFilePath;
            process.Start();
            Thread.Sleep(10000);
        }

        private static IWebElement IsElementPresent(ChromeDriver driver, string xPath)
        {
            try
            {
                return driver.FindElementByXPath(xPath);
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        private static List<string> WebScraper(string url)
        {
            var options = new ChromeOptions();
            options.AddArgument("--disable.gpu");
            ChromeDriver driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("https://www.reddit.com");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(8);

            Thread.Sleep(500);

            var titles = driver.FindElementsByClassName("_2_tDEnGMLxpM6uOa2kaDB3");

            foreach(var title in titles)
            {
                Console.WriteLine(title.Text);
            }

            driver.Quit();
            return new List<string>();
        }
    }
}










