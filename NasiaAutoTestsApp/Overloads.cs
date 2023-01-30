using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using Cookie = OpenQA.Selenium.Cookie;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace NasiaAutoTestsApp
{
    public class Overloads
    {
        private IWebDriver _driver;
        private Log _log;

        public Overloads(IWebDriver driver,Log log)
        {
            _driver = driver;
            _log = log;
        }

        public void Click(By element)
        {
            Thread.Sleep(700);
            IWebElement clickElement = _driver.FindElement(element);
            if (clickElement.Enabled && clickElement.Displayed)
            {
                _driver.FindElement(element).Click();
                _log.AddLogs($"element {element} is clicked");
            }
            
        }
        public void Click(By element,int index)
        {
            Thread.Sleep(700);
            _driver.FindElements(element)[index].Click();
            _log.AddLogs($"element {element} is clicked");
        }

        public void SendKeys(By element,string text)
        {
            _driver.FindElement(element).SendKeys(text);
            _log.AddLogs($"text:'{text}' is insented in field element {element}");
        }
        public void SendKeys(By element,int index,string text)
        {
            _driver.FindElements(element)[index].SendKeys(text);
            _log.AddLogs($"text:'{text}' is insented in field element {element}");
        }

        public void Clear(By element)
        {
            _driver.FindElement(element).Clear();
            _log.AddLogs($"element {element} is cleared");
        }

        
        
    }
}