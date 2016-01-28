using Nps.Cds.DataModels.NpsCds;
using Nps.Gis.PivotServerTools;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Nps.Gis.Web.Models.CollectionFactories
{
    /// <summary>
    /// Crash Collection
    /// </summary>
    public class CrashCollection : CollectionFactoryBase
    {
        // Constructors, Finalizer and Dispose
        //======================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public CrashCollection()
        {
            this.Name = "Crashes";
            this.Summary = "A collection of crashes in National Park Service.";
        }

        // Public Methods
        //======================================================================

        /// <summary>
        /// Make new Collection
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
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
                    .Include(c => c.CrashCategory)
                    .Where(c => c.LightCode == null);

                Collection collection = new Collection();
                collection.Name = "NPS Crashes";

                foreach (var crash in crashes.Take(maxItems))
                {
                    string imgFolder = HttpRuntime.BinDirectory.Substring(0, HttpRuntime.BinDirectory.Length - 5);
                    string imgName = GetImageName(crash.Category);
                    ItemImage image = null;
                    if (imgName != null)
                    {
                        image = new ItemImage(string.Format(@"{0}\Images\{1}", imgFolder, imgName));
                    }
                    collection.AddItem(crash.IncidentNo, null, null, image, GetFacets(crash));
                }

                // Set facet display properties
                collection.SetFacetDisplay("Park Alpha", true, true, true);
                collection.SetFacetDisplay("State", true, true, false);
                collection.SetFacetDisplay("Crash Year", true, true, false);
                collection.SetFacetDisplay("Crash Date", false, true, false);
                collection.SetFacetDisplay("Crash Time", false, true, false);
                collection.SetFacetDisplay("Route ID", true, true, true);
                collection.SetFacetDisplay("Road Name", false, true, true);
                collection.SetFacetDisplay("Node Distance (Ft)", false, true, false);
                collection.SetFacetDisplay("Node Distance (Mi)", false, true, false);
                collection.SetFacetDisplay("Node Direction", false, true, false);
                collection.SetFacetDisplay("Node Number", false, true, false);
                collection.SetFacetDisplay("Node MP", false, true, false);
                collection.SetFacetDisplay("Crash MP", true, true, false);
                collection.SetFacetDisplay("Light Condition", true, true, false);
                collection.SetFacetDisplay("Weather Condition", true, true, false);
                collection.SetFacetDisplay("Crash Location", true, true, false);
                collection.SetFacetDisplay("Surface Condition", true, true, false);
                collection.SetFacetDisplay("Crash Class", true, true, false);
                collection.SetFacetDisplay("Vehicle Collision", true, true, false);
                collection.SetFacetDisplay("Fixed Object Struck", true, true, false);
                collection.SetFacetDisplay("Road Character", true, true, false);
                collection.SetFacetDisplay("Contributing Factor 1", true, true, false);
                collection.SetFacetDisplay("Contributing Factor 2", true, true, false);
                collection.SetFacetDisplay("Contributing Factor 3", false, true, false);
                collection.SetFacetDisplay("Contributing Factor 4", false, true, false);
                collection.SetFacetDisplay("Contributing Factor 5", false, true, false);
                collection.SetFacetDisplay("Contributing Factor 6", false, true, false);
                collection.SetFacetDisplay("Category", true, true, false);
                collection.SetFacetDisplay("Hit & Run", false, true, false);
                collection.SetFacetDisplay("Fatal", false, true, false);
                collection.SetFacetDisplay("Injured", false, true, false);
                collection.SetFacetDisplay("Pedestrian Fatal", false, true, false);
                collection.SetFacetDisplay("Pedestrian Injured", false, true, false);
                collection.SetFacetDisplay("Bikers Fatal", false, true, false);
                collection.SetFacetDisplay("Bikers Injured", false, true, false);
                collection.SetFacetDisplay("Pedestrian", true, true, false);
                collection.SetFacetDisplay("Comments", false, true, true);
                collection.SetFacetDisplay("Spatial Location", true, true, false);

                //collection.SetFacetDisplay("Case Number", false, true, false);
                //collection.SetFacetDisplay("Location", false, true, false);
                //collection.SetFacetDisplay("USPP/NPS Veh. Inv.", false, true, false);
                //collection.SetFacetDisplay("Park Property Dest.", false, true, false);
                //collection.SetFacetDisplay("Data Source", false, true, false);
                //collection.SetFacetDisplay("Latitude", false, true, false);
                //collection.SetFacetDisplay("Longitude", false, true, false);
                //collection.SetFacetDisplay("Save Date", false, true, false);
                //collection.SetFacetDisplay("RIP Cycle", false, true, false);

                // Set facet formats
                //collection.SetFacetFormat("List price", "$#,0.00");

                return collection;
            }
            catch (Exception ex)
            {

                return ErrorCollection.FromException(ex);
            }
        }

        // Private Methods
        //======================================================================

        private Facet[] GetFacets(Crash crash)
        {
            List<Facet> facets = new List<Facet>();

            facets.Add(new Facet("Park Alpha", StringValue(crash.ParkAlpha)));
            facets.Add(new Facet("State", StringValue(crash.StateCode)));
            facets.Add(new Facet("Crash Year", StringValue(crash.CrashYear)));
            facets.Add(new Facet("Crash Date", crash.CrashDate));
            facets.Add(new Facet("Crash Time", crash.CrashTime));
            facets.Add(new Facet("Route ID", StringValue(crash.RouteIdent)));
            facets.Add(new Facet("Road Name", StringValue(crash.RouteName)));
            facets.Add(new Facet("Node Distance (Ft)", DecimalValue(crash.NodeDistFt)));
            facets.Add(new Facet("Node Distance (Mi)", DecimalValue(crash.NodeDistMi)));
            facets.Add(new Facet("Node Direction", StringValue(crash.NodeDir)));
            facets.Add(new Facet("Node Number", StringValue(crash.NodeNum)));
            facets.Add(new Facet("Node MP", DecimalValue(crash.MpNode)));
            facets.Add(new Facet("Crash MP", DecimalValue(crash.Milepost)));
            facets.Add(new Facet("Light Condition", string.IsNullOrEmpty(crash.LightCode) ? string.Empty : crash.Light.LightValue));
            facets.Add(new Facet("Weather Condition", string.IsNullOrEmpty(crash.WeatherCode) ? string.Empty : crash.Weather.WeatherValue));
            facets.Add(new Facet("Crash Location", string.IsNullOrEmpty(crash.CrashLoc) ? string.Empty : crash.CrashLocation.CrashLocValue));
            facets.Add(new Facet("Surface Condition", string.IsNullOrEmpty(crash.SurfaceCond) ? string.Empty : crash.SurfCondition.SurfCondValue));
            facets.Add(new Facet("Crash Class", string.IsNullOrEmpty(crash.CrashClassification) ? string.Empty : crash.CrashClass.CrashClassValue));
            facets.Add(new Facet("Vehicle Collision", string.IsNullOrEmpty(crash.VehColl) ? string.Empty : crash.VehCollision.VehCollValue));
            facets.Add(new Facet("Fixed Object Struck", string.IsNullOrEmpty(crash.ObjStruck) ? string.Empty : crash.ObjectStruck.ObjectStruckValue));
            facets.Add(new Facet("Road Character", string.IsNullOrEmpty(crash.RoadChar) ? string.Empty : crash.RoadCharacter.RoadCharValue));
            facets.Add(new Facet("Contributing Factor 1", string.IsNullOrEmpty(crash.ConFact1) ? string.Empty : crash.ContFactor1.ConFactValue));
            facets.Add(new Facet("Contributing Factor 2", string.IsNullOrEmpty(crash.ConFact2) ? string.Empty : crash.ContFactor2.ConFactValue));
            facets.Add(new Facet("Contributing Factor 3", string.IsNullOrEmpty(crash.ConFact3) ? string.Empty : crash.ContFactor3.ConFactValue));
            facets.Add(new Facet("Contributing Factor 4", string.IsNullOrEmpty(crash.ConFact4) ? string.Empty : crash.ContFactor4.ConFactValue));
            facets.Add(new Facet("Contributing Factor 5", string.IsNullOrEmpty(crash.ConFact5) ? string.Empty : crash.ContFactor5.ConFactValue));
            facets.Add(new Facet("Contributing Factor 6", string.IsNullOrEmpty(crash.ConFact6) ? string.Empty : crash.ContFactor6.ConFactValue));
            facets.Add(new Facet("Category", string.IsNullOrEmpty(crash.Category) ? string.Empty : crash.CrashCategory.Category));
            facets.Add(new Facet("Hit & Run", BoolValue(crash.HitRun)));
            facets.Add(new Facet("Fatal", ShortValue(crash.Fatals)));
            facets.Add(new Facet("Injured", ShortValue(crash.Injured)));
            facets.Add(new Facet("Pedestrian Fatal", ShortValue(crash.PedFatility)));
            facets.Add(new Facet("Pedestrian Injured", ShortValue(crash.PedInjury)));
            facets.Add(new Facet("Bikers Fatal", ShortValue(crash.BikeFatilty)));
            facets.Add(new Facet("Bikers Injured", ShortValue(crash.BikeInjury)));
            facets.Add(new Facet("Pedestrian", BoolValue(crash.Pedestrian)));
            facets.Add(new Facet("Comments", StringValue(crash.Comments)));
            facets.Add(new Facet("Spatial Location", BoolValue(crash.SpatialLoc)));

            // facets.Add(new Facet("Case Number", StringValue(crash.CaseNum)));
            //facets.Add(new Facet("Location", StringValue(crash.Location)));
            //facets.Add(new Facet("USPP/NPS Veh. Inv.", BoolValue(crash.UsppNpsNehInv)));
            //facets.Add(new Facet("Park Property Dest.", BoolValue(crash.ParkPropertyDest)));
            //facets.Add(new Facet("Data Source", StringValue(crash.DataSrc)));
            //facets.Add(new Facet("Latitude", DecimalValue(crash.Latitude)));
            //facets.Add(new Facet("Longitude", DecimalValue(crash.Longitude)));
            //facets.Add(new Facet("Save Date", crash.SaveDate));
            //facets.Add(new Facet("RIP Cycle", StringValue(crash.RipCycle)));

            return facets.ToArray();
        }


        private string StringValue(string data)
        {
            return string.IsNullOrEmpty(data) ? string.Empty : data;
        }

        private string BoolValue(bool? data)
        {
            return data.HasValue ? data.Value.ToString() : string.Empty;
        }

        private double DecimalValue(decimal? data)
        {
            return data.HasValue ? (double)data.Value : -1;
        }

        private double ShortValue(short? data)
        {
            return data.HasValue ? (double)data.Value : -1;
        }

        private string GetImageName(string category)
        {
            string imgName = "PdOnly.jpg";
            switch (category)
            {
                case "FATAL":
                    imgName = "Fatal.jpg";
                    break;
                case "INJURY":
                    imgName = "Injury.jpg";
                    break;
                case "PD ONLY":
                    imgName = "PdOnly.jpg";
                    break;
            }

            return imgName;
        }

        // Private Fields
        //======================================================================

        const int maxItems = 150;
        private NpsCdsContext db = new NpsCdsContext();
    }
}