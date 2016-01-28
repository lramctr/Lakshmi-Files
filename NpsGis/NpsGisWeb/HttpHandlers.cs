// Copyright (c) Microsoft Corporation. All rights reserved.

using Nps.Gis.PivotServerTools;
using System.Web;

namespace Nps.Gis.Web
{
    /// <summary>
    /// Handle a request for any CXML file. See the associated entry in web.config
    /// This handler finds all implementations of CollectionFactoryBase in any assembly in the bin folder.
    /// To add your own collection using this method, add a class that implements CollectionFactoryBase
    ///  into the CollectionFactories assembly.
    /// </summary>
    public class CxmlHandler : IHttpHandler
    {
        /// <summary>
        /// Process Request
        /// </summary>
        /// <param name="context">HttpContext</param>
        public void ProcessRequest(HttpContext context)
        {
            PivotHttpHandlers.ServeCxml(context);
        }

        /// <summary>
        /// Is Reusable
        /// </summary>
        public bool IsReusable
        {
            get { return true; }
        }
    }

    /*
    //You may use the steps above to create your own collections using the provided generic
    // CXML handler. Alternatively, if you want to directly implement your own specific CXML
    // handler, uncomment this sample implementation and add a corresponding entry in the
    // handlers section of web.config to use this handler. E.g.
    //  <add name="MyCXML" verb="GET" path="my.cxml" type="Nps.Gis.Web.MyCxmlHandler"/>
    public class MyCxmlHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            Collection collection = new Collection();
            collection.Name = "My specific collection";
            for (int i = 0; i < 10; ++i)
            {
                collection.AddItem(i.ToString(), null, null, null);
            }

            PivotHttpHandlers.ServeCxml(context, collection);
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
    */

    /// <summary>
    /// Dzc Handler
    /// </summary>
    public class DzcHandler : IHttpHandler
    {
        /// <summary>
        /// Process Request
        /// </summary>
        /// <param name="context">HttpContext</param>
        public void ProcessRequest(HttpContext context)
        {
            PivotHttpHandlers.ServeDzc(context);
        }

        /// <summary>
        /// Is Reusable
        /// </summary>
        public bool IsReusable
        {
            get { return true; }
        }
    }

    /// <summary>
    /// Image Tile Handler
    /// </summary>
    public class ImageTileHandler : IHttpHandler
    {
        /// <summary>
        /// Process Request
        /// </summary>
        /// <param name="context">HttpContext</param>
        public void ProcessRequest(HttpContext context)
        {
            PivotHttpHandlers.ServeImageTile(context);
        }

        /// <summary>
        /// Is Reusable
        /// </summary>
        public bool IsReusable
        {
            get { return true; }
        }
    }

    /// <summary>
    /// Dzi Handler
    /// </summary>
    public class DziHandler : IHttpHandler
    {
        /// <summary>
        /// Process Request
        /// </summary>
        /// <param name="context">HttpContext</param>
        public void ProcessRequest(HttpContext context)
        {
            PivotHttpHandlers.ServeDzi(context);
        }

        /// <summary>
        /// Is Reusable
        /// </summary>
        public bool IsReusable
        {
            get { return true; }
        }
    }

    /// <summary>
    /// Deep Zoom Image Handler
    /// </summary>
    public class DeepZoomImageHandler : IHttpHandler
    {
        /// <summary>
        /// Process Request
        /// </summary>
        /// <param name="context">HttpContext</param>
        public void ProcessRequest(HttpContext context)
        {
            PivotHttpHandlers.ServeDeepZoomImage(context);
        }

        /// <summary>
        /// Is Reusable
        /// </summary>
        public bool IsReusable
        {
            get { return true; }
        }
    }
}
