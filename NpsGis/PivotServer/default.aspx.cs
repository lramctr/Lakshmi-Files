using System;
using System.Web.UI;
using Nps.Gis.PivotServerTools;

namespace PivotServer
{
    public partial class _default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public string HtmlFragmentListPivotCollectionFactories()
        {
            string htmlFragment = PivotHttpHandlers.CollectionInfoHtml();
            return htmlFragment;
        }
    }
}
