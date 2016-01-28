// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;

namespace SilverlightPivotViewer
{
    [ScriptableType]
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Register this object in the page so that ScriptableMember methods can be called from JavaScript.
            HtmlPage.RegisterScriptableObject("pivotViewer", this);
        }

        [ScriptableMember]
        public void LoadCollection(string cxmlName)
        {
            string pageUrl = HtmlPage.Document.DocumentUri.AbsoluteUri;
            string rootUrl = pageUrl.Substring(0, pageUrl.LastIndexOf('/') + 1);

            //Create a URL to the desired CXML (and query if specified) on this JIT collection server.
            // Note, this assumes this webpage hosting the Silverlight control is at the root of the JIT collection server.
            string collectionUrl = rootUrl + cxmlName;

            PivotViewer.LoadCollection(collectionUrl, string.Empty);
        }

        private void PivotViewer_Loaded(object sender, RoutedEventArgs e)
        {
            App app = (App)App.Current;
            string cxmlName = app.InitParamsCxml;
            if (!string.IsNullOrWhiteSpace(cxmlName))
            {
                LoadCollection(cxmlName);
            }
        }

        private void PivotViewer_LinkClicked(object sender, System.Windows.Pivot.LinkEventArgs e)
        {
            NavigateTo(e.Link);
        }

        private void PivotViewer_ItemDoubleClicked(object sender, System.Windows.Pivot.ItemEventArgs e)
        {
        }

        private void NavigateTo(Uri targetUrl)
        {
            if (targetUrl.LocalPath.EndsWith(".cxml", StringComparison.InvariantCultureIgnoreCase))
            {
                //If the link is to CXML, show it in this PivotViewer.
                PivotViewer.LoadCollection(targetUrl.AbsoluteUri, string.Empty);
            }
            else
            {
                //Otherwise open the URL in a new window
                HtmlPage.Window.Navigate(targetUrl, "_blank");
            }
        }
    }
}
