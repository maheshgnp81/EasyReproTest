using System;
using System.Net;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;


namespace EasyReproTest;

[TestClass]
public class SmallTest : BaseTest
{
    
    [TestMethod]
    public void Test_2()
    {
       webDriver.Url = "http://www.google.co.in";
       string title = webDriver.Title;
       AddScreenShot(webDriver, "test");
       Assert.AreEqual(title, "Testing");
    }
  

}