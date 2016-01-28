namespace Nps.Gis.Web.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// Key Value Pair Model Description
    /// </summary>
    public class KeyValuePairModelDescription : ModelDescription
    {
        /// <summary>
        /// Key Model Description
        /// </summary>
        public ModelDescription KeyModelDescription { get; set; }

        /// <summary>
        /// Value Model Description
        /// </summary>
        public ModelDescription ValueModelDescription { get; set; }
    }
}