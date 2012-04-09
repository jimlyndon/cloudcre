using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcRouteUnitTester;

namespace Cloudcre.Web.Tests
{
    [TestClass]
    public class RouteTests
    {
       [TestMethod]
       public void TestIncomingRoutes()
       {
          // Arrange
          var tester = new RouteTester<MvcApplication>();

          // Assert
          tester.WithIncomingRequest("/").ShouldMatchRoute("Search", "Index");
          tester.WithIncomingRequest("/queue").ShouldMatchRoute("Queue", "Index");
          tester.WithIncomingRequest("/office/summary").ShouldMatchRoute("Office", "Summary");
          tester.WithIncomingRequest("/office/create").ShouldMatchRoute("Office", "Create");
          tester.WithIncomingRequest("/office/edit").ShouldMatchRoute("Office", "Edit");
          tester.WithIncomingRequest("/office/search").ShouldMatchRoute("Office", "Search");
          tester.WithIncomingRequest("/office/model").ShouldMatchRoute("Office", "Model");
          //tester.WithIncomingRequest("/multiplefamily/report/edit/f8de7dcf-5bc2-42d3-ae7e-9fe601038ac9/berkshire-by-the-sea").ShouldMatchRoute("Multiplefamily", "Edit");


          //tester.WithIncomingRequest("/Foo/Index").ShouldMatchRoute("Foo", "Index");
          //tester.WithIncomingRequest("/Foo/Bar").ShouldMatchRoute("Foo", "Bar");
          //tester.WithIncomingRequest("/Foo/Bar/5").ShouldMatchRoute("Foo", "Bar", new { id = 5 });

          //tester.WithIncomingRequest("/Foo/Bar/5/Baz").ShouldMatchNoRoute();

          //tester.WithIncomingRequest("/handler.axd/pathInfo").ShouldBeIgnored();
       }

       [TestMethod]
       public void TestOutgoingRoutes()
       {
           // Arrange
           var tester = new RouteTester<MvcApplication>();

           // Assert
           tester.WithRouteInfo("Office", "Edit").ShouldGenerateUrl("/office/edit");
           tester.WithRouteInfo("Office", "Summary").ShouldGenerateUrl("/office/summary");
           //tester.WithRouteInfo("Home", "About").ShouldGenerateUrl("/Home/About");
           //tester.WithRouteInfo("Home", "About", new { id = 5 }).ShouldGenerateUrl("/Home/About/5");
           //tester.WithRouteInfo("Home", "About", new { id = 5, someKey = "someValue" }).ShouldGenerateUrl("/Home/About/5?someKey=someValue");
       }
    }
    
    
    ///// <summary>
    /////This is a test class for MvcApplicationTest and is intended
    /////to contain all MvcApplicationTest Unit Tests
    /////</summary>
    //[TestClass()]
    //public class MvcApplicationTest
    //{


    //    private TestContext testContextInstance;

    //    /// <summary>
    //    ///Gets or sets the test context which provides
    //    ///information about and functionality for the current test run.
    //    ///</summary>
    //    public TestContext TestContext
    //    {
    //        get
    //        {
    //            return testContextInstance;
    //        }
    //        set
    //        {
    //            testContextInstance = value;
    //        }
    //    }

    //    #region Additional test attributes
    //    // 
    //    //You can use the following additional attributes as you write your tests:
    //    //
    //    //Use ClassInitialize to run code before running the first test in the class
    //    //[ClassInitialize()]
    //    //public static void MyClassInitialize(TestContext testContext)
    //    //{
    //    //}
    //    //
    //    //Use ClassCleanup to run code after all tests in a class have run
    //    //[ClassCleanup()]
    //    //public static void MyClassCleanup()
    //    //{
    //    //}
    //    //
    //    //Use TestInitialize to run code before running each test
    //    //[TestInitialize()]
    //    //public void MyTestInitialize()
    //    //{
    //    //}
    //    //
    //    //Use TestCleanup to run code after each test has run
    //    //[TestCleanup()]
    //    //public void MyTestCleanup()
    //    //{
    //    //}
    //    //
    //    #endregion


    //    /// <summary>
    //    ///A test for MvcApplication Constructor
    //    ///</summary>
    //    // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
    //    // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
    //    // whether you are testing a page, web service, or a WCF service.
    //    [TestMethod()]
    //    [HostType("ASP.NET")]
    //    [AspNetDevelopmentServerHost("C:\\cloudcre\\Cloudcre.Web", "/")]
    //    [UrlToTest("http://localhost:62348/")]
    //    public void MvcApplicationConstructorTest()
    //    {
    //        MvcApplication target = new MvcApplication();
    //        Assert.Inconclusive("TODO: Implement code to verify target");
    //    }

    //    /// <summary>
    //    ///A test for Application_Start
    //    ///</summary>
    //    // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
    //    // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
    //    // whether you are testing a page, web service, or a WCF service.
    //    [TestMethod()]
    //    [HostType("ASP.NET")]
    //    [AspNetDevelopmentServerHost("C:\\cloudcre\\Cloudcre.Web", "/")]
    //    [UrlToTest("http://localhost:62348/")]
    //    [DeploymentItem("Cloudcre.Web.dll")]
    //    public void Application_StartTest()
    //    {
    //        MvcApplication_Accessor target = new MvcApplication_Accessor(); // TODO: Initialize to an appropriate value
    //        target.Application_Start();
    //        Assert.Inconclusive("A method that does not return a value cannot be verified.");
    //    }

    //    /// <summary>
    //    ///A test for RegisterGlobalFilters
    //    ///</summary>
    //    // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
    //    // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
    //    // whether you are testing a page, web service, or a WCF service.
    //    [TestMethod()]
    //    [HostType("ASP.NET")]
    //    [AspNetDevelopmentServerHost("C:\\cloudcre\\Cloudcre.Web", "/")]
    //    [UrlToTest("http://localhost:62348/")]
    //    public void RegisterGlobalFiltersTest()
    //    {
    //        GlobalFilterCollection filters = null; // TODO: Initialize to an appropriate value
    //        MvcApplication.RegisterGlobalFilters(filters);
    //        Assert.Inconclusive("A method that does not return a value cannot be verified.");
    //    }

    //    /// <summary>
    //    ///A test for RegisterRoutes
    //    ///</summary>
    //    // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
    //    // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
    //    // whether you are testing a page, web service, or a WCF service.
    //    [TestMethod()]
    //    [HostType("ASP.NET")]
    //    [AspNetDevelopmentServerHost("C:\\cloudcre\\Cloudcre.Web", "/")]
    //    [UrlToTest("http://localhost:62348/")]
    //    public void RegisterRoutesTest()
    //    {
    //        RouteCollection routes = null; // TODO: Initialize to an appropriate value
    //        MvcApplication.RegisterRoutes(routes);
    //        Assert.Inconclusive("A method that does not return a value cannot be verified.");
    //    }
    //}
}
