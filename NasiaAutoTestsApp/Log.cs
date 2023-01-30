using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace NasiaAutoTestsApp
{
    public class Log
    {
        public string text;
        public List<string> LogMessage;
        public List<string> TimeList;
        
        public void AddLogs(string message)
        {
            TimeList.Add(DateTime.Now.ToString("h:mm:ss tt"));
            LogMessage.Add(message);
        }
    }
    
}