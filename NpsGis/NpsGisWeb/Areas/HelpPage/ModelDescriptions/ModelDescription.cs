using System;

namespace Nps.Gis.Web.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// Describes a type model.
    /// </summary>
    public abstract class ModelDescription
    {
        /// <summary>
        /// Documentation
        /// </summary>
        public string Documentation { get; set; }

        /// <summary>
        /// Model Type
        /// </summary>
        public Type ModelType { get; set; }

        /// <summary>
        /// Model Name
        /// </summary>
        public string Name { get; set; }
    }
}