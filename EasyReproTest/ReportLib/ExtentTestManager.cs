using System.Runtime.CompilerServices;
using AventStack.ExtentReports;

namespace EasyReproTest.ReportsLib;

public class ExtentTestManager
{
    [ThreadStatic]
    private static ExtentTest _ParentTest; 
    
    [ThreadStatic]
    private static ExtentTest _childTest;

    [MethodImpl(MethodImplOptions.Synchronized)]
    public static ExtentTest CreateParentTest(string testName, string description = null)
    {
        _ParentTest = ExtentService.GetExtent().CreateTest(testName, description);
        return _ParentTest;
    }
    
    [MethodImpl(MethodImplOptions.Synchronized)]
    public static ExtentTest CreateTest(string testName, string description = null)
    {
        _childTest = _ParentTest.CreateNode(testName, description);
        return _childTest;
    }
    
    [MethodImpl(MethodImplOptions.Synchronized)]
    public static ExtentTest GetTest()
    {
        return _childTest;
    }


    
}