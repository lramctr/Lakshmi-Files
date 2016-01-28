// Global variables
var debug = false;
var map;
var mapPanel = "#mapPanel";
var legendPanel = "#legendPanel";
var legendLink = "#toggleLegend";
var basemapsPanel = "#basemapsPanel";
var basemapsLink = "#toggleBasemaps";
var navPanel = "#navPanel";
var navLink = "#toggleNav"
var pivotPanel = "#pivotPanel";
var pivotLink = "#togglePivot"
var aboutLink = "#btnAbout"
var contactLink = "#btnContact"

var legendVisible = false;
var basemapsVisible = false;
var navVisible = false;
var pivotVisible = false;
var pivotInitialized = false;

$(function () {
    // http://api.jqueryui.com/autocomplete/
    // http://stackoverflow.com/questions/12370614/jquery-ui-autocomplete-with-json-from-url
    $("#searchPark").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/NpsGis/api/Parks",
                data: { query: request.term },
                success: function (data) {
                    var transformed = $.map(data, function (park) {
                        return {
                            label: park.Label,
                            value: park.Value
                        };
                    });
                    response(transformed);
                },
                error: function () {
                    response([]);
                }
            });
        }
    });
});

function hideAllPanels() {
    $(mapPanel).removeClass("col-md-10");
    $(mapPanel).removeClass("col-md-6");

    $(legendPanel).removeClass("show").addClass("hidden");
    $(basemapsPanel).removeClass("show").addClass("hidden");
    $(navPanel).removeClass("show").addClass("hidden");
    $(pivotPanel).removeClass("show").addClass("hidden");

    $(legendLink).parent().removeClass("active");
    $(basemapsLink).parent().removeClass("active");
    $(navLink).parent().removeClass("active");
    $(pivotLink).parent().removeClass("active");

    legendVisible = false;
    basemapsVisible = false;
    navVisible = false;
    pivotVisible = false;
}

// Legend Panel
$(legendLink).click(function (event) {
    event.preventDefault();
    if (legendVisible) { // Hide TOC
        hideAllPanels();
    } else { // Show TOC
        hideAllPanels();
        $(mapPanel).addClass("col-md-10");
        $(legendPanel).removeClass("hidden").addClass("show");
        $(legendLink).parent().addClass("active");
        legendVisible = true;
    }
});

// Basemaps Panel
$(basemapsLink).click(function (event) {
    event.preventDefault();
    if (basemapsVisible) { // Hide Basemaps Panel
        hideAllPanels();
    } else { // Show Basemap Gallery
        hideAllPanels();
        $(mapPanel).addClass("col-md-10");
        $(basemapsPanel).removeClass("hidden").addClass("show");
        $(basemapsLink).parent().addClass("active");
        basemapsVisible = true;
    }
});

// Navigator Panel
$(navLink).click(function (event) {
    event.preventDefault();
    if (navVisible) { // Hide Navigator
        hideAllPanels();
    } else { // Show Navigator
        hideAllPanels();
        $(mapPanel).addClass("col-md-6");
        $(navPanel).removeClass("hidden").addClass("show");
        $(navLink).parent().addClass("active");
        navVisible = true;
    }
});

// Pivot Viewer Panel
$(pivotLink).click(function (event) {
    event.preventDefault();
    if (pivotVisible) { // Hide Pivot Viewer
        hideAllPanels();
    } else { // Show Pivot Viewer
        hideAllPanels();
        $(mapPanel).addClass("col-md-6");
        $(pivotPanel).removeClass("hidden").addClass("show");

        if (jQuery.isReady && !pivotInitialized) {
            $(document).ready(function () {
                $(pivotPanel).PivotViewer({
                    Loader: new PivotViewer.Models.Loaders.CXMLLoader("/NpsGis/Crashes.cxml")
                });
            });

            pivotInitialized = true;
        }

        $(pivotLink).parent().addClass("active");
        pivotVisible = true;
    }
});

// Map
require(["esri/map",
    "esri/layers/ArcGISDynamicMapServiceLayer",
    "esri/layers/ImageParameters",
    "esri/dijit/BasemapGallery",
    "esri/dijit/Legend",
    "esri/tasks/QueryTask",
    "esri/tasks/query",
    "esri/graphicsUtils",
    "dojo/_base/array",
    "dojo/parser",
    "dojo/dom",
    "dojo/on",
    "dojo/domReady!"
], function (Map, ArcGISDynamicMapServiceLayer, ImageParameters, BasemapGallery, Legend, QueryTask, Query,
    graphicsUtils, arrayUtils, parser, dom, on) {

    parser.parse();
    var startExtent = new esri.geometry.Extent(-126, 30, -100, 55,
          new esri.SpatialReference({ wkid: 4326 }));

    map = new Map("mapPanel", {
        basemap: "streets",
        extent: startExtent
    });

    //Use the ImageParameters to set map service layer definitions and map service visible layers before adding to the client map.
    var imageParameters = new ImageParameters();
    imageParameters.format = "png24"; //set the image type to PNG24, note default is PNG8

    // Set the layers you want to be visible
    imageParameters.layerIds = [4, 5, 7, 8];
    imageParameters.layerOption = ImageParameters.LAYER_OPTION_SHOW;
    imageParameters.transparent = true;

    //Takes a URL to a non cached map service.
    var dynamicMapServiceLayer = new ArcGISDynamicMapServiceLayer(
        "https://fhfl15gisweb.flhd.fhwa.dot.gov/arcgis/rest/services/NPS/NPS_C5/MapServer", {
        "imageParameters": imageParameters
    });

    //add the legend
    map.on("layers-add-result", function (evt) {
        var layerInfo = arrayUtils.map(evt.layers, function (layer, index) {
            return { layer: layer.layer, title: layer.layer.name };
        });
        if (layerInfo.length > 0) {
            var legendDijit = new Legend({
                map: map,
                layerInfos: layerInfo
            }, "legend");
            legendDijit.startup();
        }
    });

    map.addLayers([dynamicMapServiceLayer]);

    //add the basemap gallery, in this case we'll display maps from ArcGIS.com including bing maps
    var basemapGallery = new BasemapGallery({
        showArcGISBasemaps: true,
        map: map
    }, "basemapGallery");
    basemapGallery.startup();

    basemapGallery.on("error", function (msg) {
        console.log("basemap gallery error:  ", msg);
    });

    //initialize query task
    queryTask = new QueryTask("https://fhfl15gisweb.flhd.fhwa.dot.gov/arcgis/rest/services/NPS/NPS_C5/MapServer/8");

    //initialize query
    query = new Query();
    query.returnGeometry = true;

    $("#searchPark").autocomplete({
        select: function (event, ui) {
            //set query based on what user typed in for population;
            query.where = "BOUNDARIES.PARK_ALPHA = '" + ui.item.value + "'";
            //execute query
            queryTask.execute(query, function (featureSet) {
                map.setExtent(graphicsUtils.graphicsExtent(featureSet.features));
            });
        }
    });
});
