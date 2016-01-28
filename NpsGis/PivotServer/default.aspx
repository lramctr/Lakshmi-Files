<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="PivotServer._default" %>

<!DOCTYPE html>

<html>
<head>
    <title>Pivot Collection Server</title>
    <link rel="stylesheet" type="text/css" href="default.css" />
    <meta name="description" content="Pivot Collection Server" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h3>Pivot Collection Server</h3>
    <div style="margin-bottom:1em;">
        <a href="SilverlightPivotViewer.aspx">Click here to view the samples in a Silverlight application using the PivotViewer control.</a>
    </div>
    <div id="content"><% Response.Write(this.HtmlFragmentListPivotCollectionFactories()); %></div>
    </div>
    </form>
</body>
</html>
