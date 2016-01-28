using System.Collections.ObjectModel;

namespace Nps.Gis.Web.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// Enumeration Type Model Description
    /// </summary>
    public class EnumTypeModelDescription : ModelDescription
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public EnumTypeModelDescription()
        {
            Values = new Collection<EnumValueDescription>();
        }

        /// <summary>
        /// Collection of Enumeration Value Description
        /// </summary>
        public Collection<EnumValueDescription> Values { get; private set; }
    }
}