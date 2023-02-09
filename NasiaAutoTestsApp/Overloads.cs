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
        public void Click(By element)
        {
            Thread.Sleep(500);
            IWebElement clickElement = Form1.Driver.FindElement(element);
            if (clickElement.Enabled && clickElement.Displayed)
            {
                Form1.Driver.FindElement(element).Click();
            }
            
        }
        public void Click(By element,int index)
        {
            Thread.Sleep(500);
            Form1.Driver.FindElements(element)[index].Click();
        }

        public void SendKeys(By element,string text)
        {
            Form1.Driver.FindElement(element).SendKeys(text);
        }
        public void SendKeys(By element,int index,string text)
        {
            Form1.Driver.FindElements(element)[index].SendKeys(text);
        }

        public void Clear(By element)
        {
            Form1.Driver.FindElement(element).Clear();
        }

        public void AClick(By element)
        {
            Actions action = new Actions(Form1.Driver);
            action.Click(Form1.Driver.FindElement(element)).Perform();
        }

        public void JSClick(By element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Form1.Driver;
            js.ExecuteScript("arguments[0].click();", Form1.Driver.FindElement(element));
        }
        public void Scroll(By element)
        {
            Actions action = new Actions(Form1.Driver);
            action.MoveToElement(Form1.Driver.FindElement(element)).Build().Perform();
        }

        public void Scroll(int vertical, int horizontal)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Form1.Driver;
            js.ExecuteScript($"window.scrollBy({vertical},{horizontal})");
        }
    }
}