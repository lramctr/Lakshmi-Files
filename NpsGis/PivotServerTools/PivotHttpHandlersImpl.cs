using Nps.Gis.PivotServerTools.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Nps.Gis.PivotServerTools
{
    /// <summary>
    /// The instance implementation of the handlers.
    /// </summary>
    class PivotHttpHandlersImpl
    {
        public PivotHttpHandlersImpl()
        {
            m_factories = new CollectionFactories();
            m_collectionCache = new CollectionCache();
        }

        // Public Methods
        //======================================================================

        public void AddFactoriesFromFolder(string folderPath)
        {
            m_factories.AddFromFolder(folderPath);
        }

        /// <summary>
        /// Create an HTML fragment listing the available factories.
        /// </summary>
        //TODO: Also return these as a Pivot collection.
        public string CollectionInfoHtml()
        {
            AddDefaultFactoryLocationIfNone();

            StringBuilder text = new StringBuilder();
            text.Append("<div class='PivotFactories'>");

            var sortedFactoriesByName = m_factories.EnumerateFactories().OrderBy(factory => factory.Name);
            foreach (CollectionFactoryBase factory in sortedFactoriesByName)
            {
                text.Append("<div class='PivotFactory'>");
                text.AppendFormat("<div class='PivotFactoryName'>{0}</div>", factory.Name);
                if (!string.IsNullOrEmpty(factory.Summary))
                {
                    // Convert any new-lines into break tags.
                    string htmlSummary = HttpUtility.HtmlEncode(factory.Summary);
                    htmlSummary = htmlSummary.Replace("\n", "<br/>");

                    //TODO: Convert URLs into hyperlinks.

                    text.AppendFormat("<div class='PivotFactorySummary'>{0}</div>", htmlSummary);
                }

                text.Append("<div class='PivotFactorySampleQueries'>");

                string url = factory.Name + ".cxml";
                text.AppendFormat("<div class='PivotFactorySampleQuery'><a href='{0}'>{0}</a></div>", url);
                
                text.Append("</div>");
                text.Append("</div>");
            }
            text.Append("</div>");

            return text.ToString();
        }

        /// <summary>
        /// Return the list of URLs provided by the collection factories.
        /// </summary>
        //TODO: Also return these as a Pivot collection.
        public IEnumerable<string> CollectionUrls()
        {
            AddDefaultFactoryLocationIfNone();

            var sortedFactoriesByName = m_factories.EnumerateFactories()
                .OrderBy(factory => factory.Name);

            foreach (CollectionFactoryBase factory in sortedFactoriesByName)
            {
                string url = factory.Name + ".cxml";
                yield return url;
            }
        }

        public void ServeCxml(HttpContext context)
        {
            AddDefaultFactoryLocationIfNone();

            string collectionFileName = GetUrlFileBody(context.Request.Url);
            CollectionFactoryBase factory = m_factories.Get(collectionFileName);
            if (null == factory)
            {
                //The requested resource doesn't exist. Return HTTP status code 404.
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.StatusDescription = "Pivot Collection not found.";
                return;
            }

            Collection collection = factory.MakeCollection(
                new CollectionRequestContext(context.Request.QueryString, context.Request.Url.AbsolutePath));
            if (null == collection)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.StatusDescription = "Collection is empty.";
                return;
            }

            ServeCxml(context, collection, collectionFileName);
        }

        public void ServeCxml(HttpContext context, Collection collection, string collectionFileName)
        {
            string collectionKey = collection.SetDynamicDzc(collectionFileName);
            m_collectionCache.Add(collectionKey, collection);

            context.Response.ContentType = "text/xml";
            collection.ToCxml(context.Response.Output);
        }

        public void ServeDzc(HttpContext context)
        {
            string key = GetUrlFileBody(context.Request.Url);
            Collection collection = m_collectionCache.Get(key);
            if (null == collection)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.StatusDescription = "Pivot image data not found. Cache may have expired.";
                return;
            }

            context.Response.ContentType = "text/xml";
            collection.ToDzc(context.Response.Output);
        }

        public void ServeImageTile(HttpContext context)
        {
            ImageRequest request = new ImageRequest(context.Request.Url);

            Collection collection = m_collectionCache.Get(request.DzcName);
            if (null == collection)
            {
                //TODO: Draw this message onto an image tile so it can be seen in Pivot.

                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.StatusDescription = "Pivot image not found. Cache may have expired.";
                return;
            }

            CollectionImageTileBuilder builder = new CollectionImageTileBuilder(collection, request,
                DzcSerializer.DefaultMaxLevel, DzcSerializer.DefaultTileDimension);
            builder.Write(context.Response);
        }

        public void ServeDzi(HttpContext context)
        {
            DziRequest request = new DziRequest(context.Request.Url);
            Collection collection = m_collectionCache.Get(request.CollectionKey);
            if (null == collection)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.StatusDescription = "Pivot image data not found. Cache may have expired.";
                return;
            }

            CollectionItem item = collection.Items[request.ItemId];
            ImageProviderBase image = item.ImageProvider;

            context.Response.ContentType = "text/xml";
            DziSerializer.Serialize(context.Response.Output, image.Size);
        }

        public void ServeDeepZoomImage(HttpContext context)
        {
            DeepZoomImageRequest request = new DeepZoomImageRequest(context.Request.Url);

            Collection collection = m_collectionCache.Get(request.CollectionKey);
            if (null == collection)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.StatusDescription = "Pivot image data not found. Cache may have expired.";
                return;
            }

            CollectionItem item = collection.Items[request.ItemId];
            ImageProviderBase image = item.ImageProvider;

            DeepZoomImageTile imageTile = new DeepZoomImageTile(image, request, DziSerializer.DefaultTileSize,
                DziSerializer.DefaultOverlap, DziSerializer.DefaultFormat);
            imageTile.Write(context.Response);
        }


        // Private Methods
        //======================================================================

        private void AddDefaultFactoryLocationIfNone()
        {
            if (0 == m_factories.Count)
            {
                string defaultAssemblyFolder = HttpRuntime.BinDirectory;
                AddFactoriesFromFolder(defaultAssemblyFolder);
            }
        }

        private string GetUrlFileBody(Uri url)
        {
            string[] pathSegments = url.Segments;
            string fileName = pathSegments[pathSegments.Length - 1];

            //Chop off the extension
            string fileBody = fileName.Substring(0, fileName.LastIndexOf('.'));
            return fileBody;
        }

        // Private Fields
        //======================================================================

        CollectionFactories m_factories;
        CollectionCache m_collectionCache;
    }
}
