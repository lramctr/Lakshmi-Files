using System;
using System.Reflection;

namespace Nps.Gis.Web.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// IModel Documentation Provider
    /// </summary>
    public interface IModelDocumentationProvider
    {
        /// <summary>
        /// Get Documentation
        /// </summary>
        /// <returns>Documentation</returns>
        string GetDocumentation(MemberInfo member);

        /// <summary>
        /// Get Documentation
        /// </summary>
        /// <returns>Documentation</returns>
        string GetDocumentation(Type type);
    }
}