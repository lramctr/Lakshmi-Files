// Copyright (c) Microsoft Corporation. All rights reserved.

using System.Collections.Generic;
using System.Threading;
using System.Web;

namespace Nps.Gis.PivotServerTools
{
    public static class PivotHttpHandlers
    {
        // Constructors, Finalizer and Dispose
        //======================================================================

        static PivotHttpHandlers()
        {
            s_lock = new ReaderWriterLockSlim();
        }

        // Public Methods
        //======================================================================

        public static void ApplicationStart()
        {
            MakeImplementation();
        }

        public static void ApplicationEnd()
        {
            //TODO: How to synchronize this.
            //ClearImplementation();
            if (null != s_lock)
            {
                s_lock.Dispose();
                s_lock = null;
            }
        }

        public static void AddFactoriesFromFolder(string folderPath)
        {
            GetImplementation().AddFactoriesFromFolder(folderPath);
        }

        /// <summary>
        /// Get the collection factory names, descriptions and sample URLs hosted by this server, as an HTML fragment
        /// </summary>
        /// <returns></returns>
        public static string CollectionInfoHtml()
        {
            return GetImplementation().CollectionInfoHtml();
        }

        /// <summary>
        /// Get the list of sample URLs hosted by this server.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> CollectionUrls()
        {
            return GetImplementation().CollectionUrls();
        }

        /// <summary>
        /// Return a CXML response by using the relevant Collection Factory for this request.
        /// </summary>
        public static void ServeCxml(HttpContext context)
        {
            GetImplementation().ServeCxml(context);
        }

        /// <summary>
        /// Return a CXML response using the given Collection object.
        /// </summary>
        public static void ServeCxml(HttpContext context, Collection collection)
        {
            GetImplementation().ServeCxml(context, collection, null);
        }

        public static void ServeDzc(HttpContext context)
        {
            GetImplementation().ServeDzc(context);
        }

        public static void ServeImageTile(HttpContext context)
        {
            GetImplementation().ServeImageTile(context);
        }

        public static void ServeDzi(HttpContext context)
        {
            GetImplementation().ServeDzi(context);
        }

        public static void ServeDeepZoomImage(HttpContext context)
        {
            GetImplementation().ServeDeepZoomImage(context);
        }

        // Private Methods
        //======================================================================

        static void MakeImplementation()
        {
            s_lock.EnterWriteLock();
            try
            {
                //If we're setting this, existing threads will still have their old implementation to use.
                s_impl = new PivotHttpHandlersImpl();
            }
            finally
            {
                s_lock.ExitWriteLock();
            }
        }

        static PivotHttpHandlersImpl GetImplementation()
        {
            s_lock.EnterUpgradeableReadLock();
            try
            {
                if (null == s_impl)
                {
                    MakeImplementation();
                }
                return s_impl;
            }
            finally
            {
                s_lock.ExitUpgradeableReadLock();
            }
        }


        // Private Fields
        //======================================================================

        static ReaderWriterLockSlim s_lock;
        static PivotHttpHandlersImpl s_impl;
    }
}
