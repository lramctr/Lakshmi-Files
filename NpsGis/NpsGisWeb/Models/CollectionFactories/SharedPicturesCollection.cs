// Copyright (c) Microsoft Corporation. All rights reserved.

using Nps.Gis.PivotServerTools;
using System.IO;

namespace Nps.Gis.Web.Models.CollectionFactories
{
    /// <summary>
    /// Create a collection from the Shared Pictures folder.
    /// </summary>
    public class SharedPicturesCollection : CollectionFactoryBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SharedPicturesCollection()
        {
            this.Name = "Pictures";
            this.Summary = "A collection of JPG and PNG images from the Windows shared pictures folder on this server.";
        }

        /// <summary>
        /// Calls static methods for creating Pivot Viewer collection
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Collection MakeCollection(CollectionRequestContext context)
        {
            return MakeCollection();
        }

        /// <summary>
        /// Creates Collection for Pivot Viewer
        /// </summary>
        /// <returns></returns>
        public static Collection MakeCollection()
        {
            string folder = @"C:\Users\Public\Pictures\Sample Pictures";
            string[] files = Directory.GetFiles(folder);

            Collection coll = new Collection();
            coll.Name = "Sample Pictures";

            bool anyItems = false;
            foreach (string path in files)
            {
                string extension = Path.GetExtension(path);
                bool isJpeg = (0 == string.Compare(".jpg", extension, true));
                bool isPng = (0 == string.Compare(".png", extension, true));

                if (isJpeg || isPng)
                {
                    anyItems = true;

                    FileInfo info = new FileInfo(path);
                    coll.AddItem(Path.GetFileNameWithoutExtension(path), path, null,
                        new ItemImage(path)
                        , new Facet("File name", Path.GetFileName(path)
                            , isJpeg ? "*.jpg" : null
                            , isPng ? "*.png" : null
                            )
                        , new Facet("File size", info.Length / 1000)
                        , new Facet("Creation time", info.CreationTime)
                        , new Facet("Link:", new FacetHyperlink("click to view image", path))
                        );
                }
            }

            if (anyItems)
            {
                coll.SetFacetDisplay("Creation time", false, true, false);
                coll.SetFacetFormat("File size", "#,#0 kb");
            }
            else
            {
                coll.AddItem("No pictures", null,
                    string.Format("The folder \"{0}\" does not contain any JPEG or PNG files.", folder),
                    null);
            }

            return coll;
        }
    }
}
