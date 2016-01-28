using System.Web.Mvc;

namespace Nps.Gis.Web.Controllers
{
    /// <summary>
    /// Home Controller
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Index page
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// test page
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Test()
        {
            return View();
        }
    }
}