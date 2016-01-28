// Copyright (c) Microsoft Corporation. All rights reserved.

using Nps.Gis.PivotServerTools;
using System;

namespace Nps.Gis.CollectionFactories
{
    public static class ErrorCollection
    {
        /// <summary>
        /// Create a single item collection that displays the error message from an exception.
        /// </summary>
        public static Collection FromException(Exception ex)
        {
            Collection collection = new Collection();
            collection.Name = "Error";

            string title = ex.Message;
            string summary = (null == ex.InnerException) ? null : ex.InnerException.Message;

            collection.AddItem(title, null, summary, null);
            return collection;
        }
    }
}
