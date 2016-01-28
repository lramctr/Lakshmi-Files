// Copyright (c) Microsoft Corporation. All rights reserved.

using System.Web;
using Nps.Gis.PivotServerTools;

namespace PivotServer
{
    /// <summary>
    /// Handle a request for any CXML file. See the associated entry in web.config
    /// This handler finds all implementations of CollectionFactoryBase in any assembly in the bin folder.
    /// To add your own collection using this method, add a class that implements CollectionFactoryBase
    ///  into the CollectionFactories assembly.
    /// </summary>
    public class CxmlHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            PivotHttpHandlers.ServeCxml(context);
        }

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
    //  <add name="MyCXML" verb="GET" path="my.cxml" type="PivotServer.MyCxmlHandler"/>
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

    public class DzcHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            PivotHttpHandlers.ServeDzc(context);
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }

    public class ImageTileHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            PivotHttpHandlers.ServeImageTile(context);
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }

    public class DziHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            PivotHttpHandlers.ServeDzi(context);
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }

    public class DeepZoomImageHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            PivotHttpHandlers.ServeDeepZoomImage(context);
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
