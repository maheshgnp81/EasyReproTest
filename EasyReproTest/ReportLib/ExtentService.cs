using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace EasyReproTest.ReportsLib;

public class ExtentService
{
    public static ExtentReports extent;

    public static ExtentReports GetExtent()
    {
        if (extent == null)
        {
            var path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            var actualPath = path.Substring(0, path.LastIndexOf("bin"));
            var projectPath = new Uri(actualPath).LocalPath;
            Directory.CreateDirectory(projectPath.ToString() + "Reports");
            var reportPath = projectPath + "Reports/Index.html";
            Console.WriteLine(reportPath);
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Host Name", "Automation Report");
            extent.AddSystemInfo("Environment", "Test Environment");
            extent.AddSystemInfo("UserName", "Uma Mahesh");
            htmlReporter.LoadConfig(projectPath + "report-config.xml");
        }
        return extent;

    }
}