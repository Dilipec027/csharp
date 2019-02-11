using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumTest.Pages;

namespace SeleniumTest.Pages
{
     class Homepage
    {
        [FindsBy(How = How.Id, Using = "username")]
        public  IWebElement UserName { get; set; }

        [FindsBy(How = How.Id, Using = "pwd")]
        public  IWebElement Password { get; set; }

        [FindsBy(How = How.Id, Using = "login")]
        public  IWebElement Submit { get; set; }

        public Homepage()
        {
            PageFactory.InitElements(this, new RetryingElementLocator(SeleniumDemo.driver, TimeSpan.FromSeconds(20)));
        }

        public void login(String username)
        {
            UserName.SendKeys(username);
        }

    }
}
