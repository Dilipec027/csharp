using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System.Diagnostics;
using System.IO;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using System.Threading;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumTest.Pages;

namespace SeleniumTest
{
    class SeleniumDemo
    {
        public static IWebDriver driver;
        public static ExtentReports extent;
        public static ExtentTest test;
        String dir;
        public static PagesIntialize page;



        [OneTimeSetUp]
        public void BeforeClass()
        { 
            try
            {
                //To create report directory and add HTML report into it

                extent = new ExtentReports();
                dir = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "");
                DirectoryInfo di = Directory.CreateDirectory(dir + "\\Test_Execution_Reports");
                var htmlReporter = new ExtentHtmlReporter(dir + "\\Test_Execution_Reports" + "\\Automation_Report" + ".html");
                extent.AddSystemInfo("Environment", "Journey of Quality");
                extent.AddSystemInfo("User Name", "Dilip");
                extent.AttachReporter(htmlReporter);
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        [SetUp]
        public void startBrowser()
        {
            //System.Diagnostics.Debug.WriteLine("my string");
            //Console.WriteLine("C:\\Users\\DMUHBR\\Downloads\\chromedriver_win32\\chromedriver");
            //string startupPath = Environment.CurrentDirectory;
            //string currentDir = Environment.CurrentDirectory;
            //System.Diagnostics.Debug.WriteLine(currentDir);
            //DirectoryInfo directory = NewMethod(currentDir);
            driver = new ChromeDriver(dir);
            try
            {
                test = extent.CreateTest(TestContext.CurrentContext.Test.Name);                      
            }
            catch (Exception e)
            {
                throw (e);
            }


        }

        //private static DirectoryInfo NewMethod(string currentDir)
        //{
        //    return new DirectoryInfo(
        //        Path.GetFullPath(Path.Combine(currentDir, @"..\..\" )));
        //}

        [Test]
        public void urlLaunch()
        {
            driver.Url = "https://wami.scania.com/";
            //driver.FindElement(By.Id("username")).SendKeys("abcd");
            System.Threading.Thread.Sleep(1000);
            Assert.IsTrue(true);
            //    Console.WriteLine("Hello how are you");
            //    Console.Read();
            System.Diagnostics.Debug.WriteLine("hello1");          
            //    Trace.WriteLine("This line will show on Output window");
            //    Trace.Flush();
        }

        [Test]
        public void login()
        {
            driver.Url = "https://wami.scania.com/";
            Homepage homepage = new Homepage();
            homepage.login("abcd");
            System.Threading.Thread.Sleep(1000);
            Assert.IsTrue(true);
        }

        //[TearDown]
        //public void closeBrowser()
        //{
        //    driver.Close();
        //}
        [TearDown]
        public void AfterTest()
        {
            try
            {

                driver.Close();
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var stacktrace = "" + TestContext.CurrentContext.Result.StackTrace + "";
                var errorMessage = TestContext.CurrentContext.Result.Message;
                Status logstatus;
                switch (status)
                {
                    case TestStatus.Failed:
                        logstatus = Status.Fail;
                        string screenShotPath = Capture(driver, TestContext.CurrentContext.Test.Name);
                        test.Log(logstatus, "Test ended with " + logstatus + " – " + errorMessage);
                        test.Log(logstatus, "Snapshot below: " + test.AddScreenCaptureFromPath(screenShotPath));
                        break;
                    case TestStatus.Skipped:
                        logstatus = Status.Skip;
                        test.Log(logstatus, "Test ended with " + logstatus);
                        break;
                    default:
                        logstatus = Status.Pass;
                        test.Log(logstatus, "Test ended with " + logstatus);
                        break;
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        [OneTimeTearDown]
        public void AfterClass()
        {
            try
            {
                extent.Flush();
            }
            catch (Exception e)
            {
                throw (e);
            }
            driver.Quit();
        }


        private string Capture(IWebDriver driver, string screenShotName)
        {
            string localpath = "";
            try
            {
                Thread.Sleep(4000);
                ITakesScreenshot ts = (ITakesScreenshot)driver;
                Screenshot screenshot = ts.GetScreenshot();
                string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                var dir = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "");
                DirectoryInfo di = Directory.CreateDirectory(dir + "\\Defect_Screenshots\\");
                string finalpth = pth.Substring(0, pth.LastIndexOf("bin")) + "\\Defect_Screenshots\\" + screenShotName + ".png";
                localpath = new Uri(finalpth).LocalPath;
                screenshot.SaveAsFile(localpath);
            }
            catch (Exception e)
            {
                throw (e);
            }
            return localpath;
        }
    }
}
    


