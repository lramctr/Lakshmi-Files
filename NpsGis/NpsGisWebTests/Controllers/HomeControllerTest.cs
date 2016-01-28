using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nps.Gis.Web.Controllers;
using System.Web.Mvc;

namespace Nps.Gis.Web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
