using Nps.Cds.DataModels.NpsCds;
using Nps.Gis.PivotServerTools;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Nps.Gis.CollectionFactories
{
    public class CrashCollection : CollectionFactoryBase
    {
        // Constructors, Finalizer and Dispose
        //======================================================================

        public CrashCollection()
        {
            this.Name = "Crashes";
            this.Summary = "A collection of crashes in National Park Service.";
        }

        // Public Methods
        //======================================================================

        public override Collection MakeCollection(CollectionRequestContext context)
        {
            try
            {
                var crashes = db.Crashes
                    .Include(c => c.Light)
                    .Include(c => c.Weather)
                    .Include(c => c.CrashLocation)
                    .Include(c => c.SurfCondition)
                    .Include(c => c.CrashClass)
                    .Include(c => c.VehCollision)
                    .Include(c => c.ObjectStruck)
                    .Include(c => c.RoadCharacter)
                    .Include(c => c.ContFactor1)
                    .Include(c => c.ContFactor2)
                    .Include(c => c.CrashCategory);

                Collection collection = new Collection();
                collection.Name = "NPS Crashes";

                foreach (var crash in crashes.Take(maxItems))
                {
                    ItemImage image = null;

                    collection.AddItem(crash.IncidentNo, null, null, image, GetFacets(crash));
                }

                return collection;
            }
            catch (Exception ex)
            {

                return ErrorCollection.FromException(ex);
            }
        }

        // Private Fields
        //======================================================================

        private Facet[] GetFacets(Crash crash)
        {
            List<Facet> facets = new List<Facet>();
            facets.Add(new Facet("Park Alpha", crash.ParkAlpha));
            facets.Add(new Facet("State", crash.StateCode));
            facets.Add(new Facet("Crash Year", crash.CrashYear));
            facets.Add(new Facet("Route ID", crash.RouteIdent));
            facets.Add(new Facet("Crash MP", (double)crash.Milepost.Value));
            facets.Add(new Facet("Light Condition", crash.Light.LightValue));
            facets.Add(new Facet("Weather Condition", crash.Weather.WeatherValue));
            facets.Add(new Facet("Crash Location", crash.CrashLocation.CrashLocValue));
            facets.Add(new Facet("Surface Condition", crash.SurfCondition.SurfCondValue));
            facets.Add(new Facet("Crash Class", crash.CrashClass.CrashClassValue));
            facets.Add(new Facet("Vehicle Collision", crash.VehCollision.VehCollValue));
            facets.Add(new Facet("Object Struck", crash.ObjectStruck.ObjectStruckValue));
            facets.Add(new Facet("Road Character", crash.RoadCharacter.RoadCharValue));
            facets.Add(new Facet("Contributing Factor 1", crash.ContFactor1.ConFactValue));
            facets.Add(new Facet("Contributing Factor 2", crash.ContFactor2.ConFactValue));
            facets.Add(new Facet("Category", crash.CrashCategory.Category));
            facets.Add(new Facet("Pedestrian", crash.Pedestrian.Value.ToString()));
            facets.Add(new Facet("Spatial Location", crash.SpatialLoc.ToString()));


            return facets.ToArray();
        }

        // Private Fields
        //======================================================================

        const int maxItems = 2500;
        private NpsCdsContext db = new NpsCdsContext();
    }
}