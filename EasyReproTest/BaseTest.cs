using System.Drawing.Imaging;
using System.Reflection;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using EasyReproTest.ReportsLib;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using WebDriverManager;

namespace EasyReproTest;

[TestClass]
public class BaseTest
{
    
    public static ExtentReports Extent;
    public static ExtentTest TestParent;
    protected static ExtentTest Test;
    protected static IWebDriver webDriver;
    public TestContext TestContext { get; set; }
    
    [AssemblyInitialize]
    public static void AssemblyInitialize(TestContext context)
    {
        ExtentTestManager.CreateParentTest("SampleTEST");

    }
    
    [TestInitialize]
    public void TestInitialize()
    {
        Console.WriteLine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        ExtentTestManager.CreateTest(TestContext.TestName);
         webDriver = new ChromeDriver("/Users/umag/Projects/EasyReproTest/EasyReproTest/Reports/");
    }
    
    [TestCleanup]
    public void TestCleanup()
    {
        String screenShotPath, fileName;
        DateTime time = DateTime.Now;
        fileName = "Screenshot_" + time.ToString("h_mm_ss") + TestContext.TestName + ".png";

        switch (TestContext.CurrentTestOutcome)
        {
            case UnitTestOutcome.Error:
                ReportLog.Fail("Test Failed - System Error");
                AddScreenShot(webDriver, "testfailed");
                break;
            case UnitTestOutcome.Passed:
                ReportLog.Pass("Test Passed");
                break;
            case UnitTestOutcome.Failed:
                var mediaEntity = CaptureScreenShot(webDriver, fileName);
                ReportLog.Fail("<p><b>Test FAILED!</b></p>", mediaEntity);
                break;
            case UnitTestOutcome.Inconclusive:
                ReportLog.Fail("Test Failed - Inconclusive");
                break;
            case UnitTestOutcome.Timeout:
                ReportLog.Fail("Test Failed - Timeout");
                break;
            case UnitTestOutcome.Aborted:
                ReportLog.Skip("Test Failed - Aborted / Not Runnable");
                break;
            default:
                ReportLog.Fail("Test Failed - Unknown");
                break;
        }
        ReportLog.Info("Execution completed suceesfully");
    }
 
    [ClassCleanup]
    public static void ClassCleanup()
    {
        Console.WriteLine("Inside ClassCleanup");
    }
    
    [AssemblyCleanup]
    public static void AssemblyCleanup()
    {
        Console.WriteLine("Issue here");
        webDriver.Quit();
        ExtentService.GetExtent().Flush();
    }
    
    
    public string AddScreenShot(IWebDriver driver, string screenShotName)
    {
        ITakesScreenshot ts = (ITakesScreenshot)driver;
        Screenshot screenshot = ts.GetScreenshot();
        string pth = Assembly.GetCallingAssembly().CodeBase;
        string finalpth = pth.Substring(0, pth.LastIndexOf("bin")) + "Reports//" + screenShotName + ".png";
        string localpath = new Uri(finalpth).LocalPath;
        Console.WriteLine(localpath);
        screenshot.SaveAsFile(localpath, ScreenshotImageFormat.Png);
        return localpath;
    }
    
    public static string Capture(IWebDriver driver, String screenShotName)
    {
        ITakesScreenshot ts = (ITakesScreenshot)driver;
        Screenshot screenshot = ts.GetScreenshot();
        var pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
        var actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
        var reportPath = new Uri(actualPath).LocalPath;
        Directory.CreateDirectory(reportPath + "Reports//" + "Screenshots");
        var finalpth = pth.Substring(0, pth.LastIndexOf("bin")) + "Reports//Screenshots//" + screenShotName;
        var localpath = new Uri(finalpth).LocalPath;
        screenshot.SaveAsFile(localpath, ScreenshotImageFormat.Png);
        return reportPath;
    }
 
    /* This implementation remains unchanged, MediaEntityBuilder is used for capturing screenshots */
    public MediaEntityModelProvider CaptureScreenShot(IWebDriver driver, String screenShotName)
    {
        ITakesScreenshot ts = (ITakesScreenshot)driver;
        var screenshot = ts.GetScreenshot().AsBase64EncodedString;
 
        return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();
    }


}