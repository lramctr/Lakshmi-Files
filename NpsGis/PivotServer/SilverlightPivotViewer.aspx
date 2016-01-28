<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<title>Silverlight PivotViewer</title>
	<style type="text/css">
	html, body {
		height: 100%;
		overflow: auto;
	}
	body {
		padding: 0;
		margin: 0;
	}
	#silverlightControlHost {
		height: 100%;
		text-align:center;
	}
	</style>
	<script type="text/javascript" src="Silverlight.js"></script>
	<script type="text/javascript">
		function onSilverlightError(sender, args) {
			var appSource = "";
			if (sender != null && sender != 0) {
			  appSource = sender.getHost().Source;
			}
			
			var errorType = args.ErrorType;
			var iErrorCode = args.ErrorCode;

			if (errorType == "ImageError" || errorType == "MediaError") {
			  return;
			}

			var errMsg = "Unhandled Error in Silverlight Application " +  appSource + "\n" ;

			errMsg += "Code: "+ iErrorCode + "    \n";
			errMsg += "Category: " + errorType + "       \n";
			errMsg += "Message: " + args.ErrorMessage + "     \n";

			if (errorType == "ParserError") {
				errMsg += "File: " + args.xamlFile + "     \n";
				errMsg += "Line: " + args.lineNumber + "     \n";
				errMsg += "Position: " + args.charPosition + "     \n";
			}
			else if (errorType == "RuntimeError") {           
				if (args.lineNumber != 0) {
					errMsg += "Line: " + args.lineNumber + "     \n";
					errMsg += "Position: " +  args.charPosition + "     \n";
				}
				errMsg += "MethodName: " + args.methodName + "     \n";
			}

			throw new Error(errMsg);
		}

		function loadCollection(collectionUrl) {
			if (collectionUrl == null || collectionUrl == "") 
				return;

			var pivot = document.getElementById("pivotViewer");
			pivot.Content.pivotViewer.LoadCollection(collectionUrl);
		}

		function getSelectedCollection() {
			var selectControl = document.getElementById("selectCollection");
			var indexSamples = selectControl.selectedIndex;
			return selectControl.options[indexSamples].value;
		}

		function onSelectCollection() {
			var textBox = document.getElementById("textUrl");
			var collection = getSelectedCollection();
			textBox.value = collection;
			loadCollection(collection);
		}

		function onLoad() {
			var textBox = document.getElementById("textUrl");
			var collectionUrl = textBox.value;
			loadCollection(collectionUrl);
		}
	</script>
</head>
<body>
	<div>
		<select id="selectCollection" onchange="onSelectCollection();">
			<option value="">&lt;Choose a sample collection&gt;</option>
			<% 
				foreach (string sample in Nps.Gis.PivotServerTools.PivotHttpHandlers.CollectionUrls())
				{
					string optionHtml = string.Format("<option value='{0}'>{1}</option>", sample, HttpUtility.HtmlEncode(sample));
					Response.Write(optionHtml);
				}
			%>
		</select>
		<input type="text" id="textUrl" value="" />
		<input type="button" value="Load" onclick="onLoad();" />
		</div>

	<form id="form1" runat="server" style="height:100%">
	<div id="silverlightControlHost">
		<object id="pivotViewer" data="data:application/x-silverlight-2," type="application/x-silverlight-2" width="100%" height="100%">
		  <param name="source" value="ClientBin/SilverlightPivotViewer.xap"/>
		  <param name="onError" value="onSilverlightError" />
		  <param name="background" value="white" />
		  <param name="minRuntimeVersion" value="4.0.50401.0" />
		  <param name="autoUpgrade" value="true" />
<!--
		  <param name="initParams" value="cxml=samples.cxml" />
-->
		  <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=4.0.50401.0" style="text-decoration:none">
			  <img src="http://go.microsoft.com/fwlink/?LinkId=161376" alt="Get Microsoft Silverlight" style="border-style:none"/>
		  </a>
		</object><iframe id="_sl_historyFrame" style="visibility:hidden;height:0;width:0;border:0"></iframe></div>
	</form>
</body>
</html>
