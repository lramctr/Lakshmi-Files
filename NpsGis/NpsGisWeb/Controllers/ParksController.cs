using Nps.Cds.DataModels.NpsCds;
using Nps.Gis.Web.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;

namespace Nps.Gis.Web.Controllers
{
    /// <summary>
    /// Web API controller for parks
    /// </summary>
    public class ParksController : ApiController
    {
        private NpsCdsContext db = new NpsCdsContext();

        // GET: api/Parks?query=5
        /// <summary>
        /// Gets list of top 20 parks containing query string
        /// </summary>
        /// <returns>List of parks</returns>
        public List<Park> GetParks(string query)
        {
            var parkAlphas = from park in db.NpsParks
                             where (park.ParkAlpha.Contains(query))
                             select new Park
                             {
                                 Label = park.ParkAlpha,
                                 Value = park.ParkAlpha
                             };

            var parkNames = from park in db.NpsParks
                            where (park.ParkName.Contains(query))
                            select new Park
                            {
                                Label = park.ParkName,
                                Value = park.ParkAlpha
                            };

            var parks = parkAlphas.Union(parkNames);
            return parks.Take(20).ToList();
        }

        /// <summary>
        /// Disposes the class
        /// </summary>
        /// <param name="disposing">Boolean</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}