using SeleniumTest.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.PageObjects;

namespace  SeleniumTest
{
    
    public static class  PagesIntialize
    {
        public  static void InitialPage()
        {
            var homePage = new Homepage();
            Homepage home= PageFactory.InitElements(SeleniumDemo.driver, homePage);
        }
    }
}
