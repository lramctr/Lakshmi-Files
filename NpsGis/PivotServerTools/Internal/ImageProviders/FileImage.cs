// Copyright (c) Microsoft Corporation. All rights reserved.

using System.Drawing;

namespace Nps.Gis.PivotServerTools.Internal
{
    /// <summary>
    /// Create an item image by loading it from a file.
    /// </summary>
    internal class FileImage : ImageBase
    {
        // Constructors, Finalizer and Dispose
        //======================================================================

        public FileImage(string filePath)
        {
            m_filePath = filePath;
        }

        // Protected Methods
        //======================================================================

        protected override Image MakeImage()
        {
            return Image.FromFile(m_filePath);
        }

        // Private Fields
        //======================================================================

        string m_filePath;
    }
}
