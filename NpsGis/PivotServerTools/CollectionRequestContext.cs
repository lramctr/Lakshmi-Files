using System.Collections.Specialized;

namespace Nps.Gis.PivotServerTools
{
    /// <summary>
    /// Contains data about the CXML query, passed to overrides of CollectionFactoryBase.MakeCollection().
    /// </summary>
    public class CollectionRequestContext
    {
        internal CollectionRequestContext(NameValueCollection query, string collectionUrl)
        {
            this.Query = query;
            this.Url = collectionUrl;
        }

        /// <summary>
        /// The parameters passed in the URL
        /// </summary>
        public NameValueCollection Query { get; private set; }

        /// <summary>
        /// The base URL to the collection, without the query parameters.
        /// </summary>
        public string Url { get; private set; }

        //public void OutputTrace(string message);
    }
}
