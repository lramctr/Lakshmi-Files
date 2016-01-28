using System.Collections.ObjectModel;

namespace Nps.Gis.Web.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// Complex Type Model Description
    /// </summary>
    public class ComplexTypeModelDescription : ModelDescription
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ComplexTypeModelDescription()
        {
            Properties = new Collection<ParameterDescription>();
        }

        /// <summary>
        /// Collection of Parameter Description
        /// </summary>
        public Collection<ParameterDescription> Properties { get; private set; }
    }
}