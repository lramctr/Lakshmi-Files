// Copyright (c) Microsoft Corporation. All rights reserved.


namespace Nps.Gis.PivotServerTools
{
    /// <summary>
    /// Your collection factory must derive from this class.
    /// Derived instances will be detected and loaded automatically,
    ///   so must have a public constructor that takes no parameters.
    /// </summary>
    public abstract class CollectionFactoryBase
    {
        // Constructors, Finalizer and Dispose
        //======================================================================

        protected CollectionFactoryBase()
        {
        }


        // Public Properties
        //======================================================================

        /// <summary>
        /// The file body used in the URL to refer to this collection.
        /// If null, the class name is used.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A summary of the collection, including URL query parameters that it accepts.
        /// </summary>
        public string Summary { get; set; }

        // Public Methods
        //======================================================================

        /// <summary>
        /// Override this method to provide a Collection object for the request.
        /// </summary>
        public abstract Collection MakeCollection(CollectionRequestContext context);
    }
}
