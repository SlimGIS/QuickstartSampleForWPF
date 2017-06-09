# Quickstart Guide for SlimGIS MapKit WPF
SlimGIS MapKit for WPF is a .net WPF component to help you to easily build up a Map based WPF application. It contains full function of SlimGIS MapKit Core which comes with Geometry, GeoFunction, Symbology, Renderer, DataSource, Layer etc. In this guide, we are going to focus on the WPF component introduction and create the first application with it.

[Get the source code](https://github.com/SlimGIS/QuickstartSampleForWPF).

When you are reading this guide, I assume that you have installed SlimGIS Setup on your machine (if not ready, please visit [this page](http://www.slimgis.com/developers/installation) and make it ready for few steps).

In this guide, I will introduce the following items:

1. Scenario description.
2. Add assembly references and init the map control.
3. Add OpenStreetMap as base map.
4. Build-in Touch and Mouse operation.
5. Add a Shapefile and set styles.
5. Add build-in controls: zoom bar, scale bar etc.
7. Identify a feature and highlight it.

> This project already includes the runtime license. If your trial license is expired, it still can run the project by "ctrl + F5". Or [try executable](https://github.com/SlimGIS/QuickstartSampleForWPF/releases). 

All right, I think we can stop here. Not too much content. It is only parts of this WPF component. View [this page](#) for the full feature list.

## Scenario description.
What a basic map contains? I'm sure everyone has a different vision of it. Let's open the web browser and visit [maps.google.com](https://www.google.com/maps). It is the most popular map currently.  
![quickstart-guide-google](http://p1.bqimg.com/567571/baf5b2a702cd22b2.png)
A basic map application usually contains a map of course; a serial buttons to controll the map viewport (In the screenshot above, it adorns on the bottom right corner, see the little "+" and "-" button). A scale bar belows the buttons on the right of the very bottom. We can add more controls to make it convenient to use. Like displaying current mouse coordinate. This is what all for a common maps have. That's not all for this guide. You know everyone loves Google Maps, but compare with a component, we can do more as we like. In the following part, we will load our own Shapefile, set a nice style and put a lable on it; then interact with it. Isn't it cool? Let's get start.

## Add assembly references and init the map control.
To start the demo, every developers know how to create a project with Visual Studio. In this guide, I will use Visual Studio 2015 Community version. Here are few steps, I will try to make it simple. Open VS2015 -> Create a WPF Application and name it `QuickstartSampleForWpf`.  
At this step, there are several options to continue:

1. Drag the map component from *ToolBox* in Visual Studio.
2. Reference assembly from installed folder.
3. Reference package from NuGet.org.

We will go with option one, as it is already installed on your machine at *C:\Program Files (x86)\SlimGIS\SDK\3.0.0\Wpf*. Let's drag *SGMapKit.Wpf.dll* into the toolbox; Visual Studio will automatically detect the custom controls inside this assembly and prepare the controls for you. See the screenshot below.  
![quickstart-guide-wpf-toolbox](http://p1.bpimg.com/567571/89bfc2a2bcfda73d.png)  
MapControl is what we are going to add to the *MainWindow.xaml* design-time. Drag this control into the default *MainWindow.xaml* that is created by WPF application template and set a proper size. I'd like to make it like following size.
![quickstart-guide-wpf-set-a-proper-size](http://p1.bpimg.com/567571/8dc84d9cce26dcc8.png)  
Now we will add a *Loaded* event on the *MapControl*. Here will do a little trick on Visual Studio. Select map control to make it focus -> Press `F4` button to open the *Properties Explorer*. First we set the control's name to `Map1` and click the *Event handlers for the selected element* button. ![quickstart-guide-wpf-set-map-name](http://p1.bpimg.com/567571/3ee8c36d76dbb734.png)  Scroll down and look for the *Loaded* event, then double click on the textbox. Visual Studio will create the event handler method `Map1_Loaded` for you and auto switch to the source code. It is very convenient. This method is the major place where we will code at.

## Add OpenStreetMap as base map.
First, we set the very first and important setting on map, the `MapUnit`. `MapUnit` defines the primary coordinate unit which is used to caclulate the vectors or raster position or measuring etc. on the map. In SlimGIS products, we supports linear and degree (longitude & latitude) coordiante system.
```csharp
Map1.MapUnit = GeoUnit.Meter;
```
We used linear coordinate unit `Meter` here, because as we described just now, we are going to add `OpenStreetMap` as base map. It is a 3rd part map imagery service that is provided by [OpenStreetMap](http://www.openstreetmap.org). While OpenStreetMap is using linear coordinate unit `Meter`, so we have to follow the base map's primary coordinate unit and use it for the further caculation.  
Now we are going to add OpenStreetMap as base map and zoom the location to the full bound of it.
```csharp
Map1.UseOpenStreetMapAsBaseMap();
Map1.ZoomToFullBound();
```
That's all to add an OpenStreetMap as base map now. Vert straight forward right? Let's press `F5` to see the effect.
![quickstart-guide-wpf-osm](http://i1.piimg.com/567571/343ef1d7c3eab3b0.png)

## Build-in Touch and Mouse operation.
Now you could feel free to do the operations below. 

1. Pan the map around with dragging the map and move your mouse.
2. Zoom in/out by mouse wheel up/down.
3. Zoom current map viewport to the next level by double click.  
4. Press `Shift` key on keyboard and hold -> use mouse to draw a rectangle to zoom the drawn area.

If you have a tablet device with OS Win8 above, that is a fantasy part, we support mouse and `touch` at the same time.

1. Pan the map around by one finger touches on the screen and move it.
2. Pinch the map to zoom in/out with two fingers.
3. Rotate the map with two fingers and rotate.
4. Double tap on the map to zoom in to the next level.  

## Add a Shapefile and set styles.
We prepared a contrys ShapeFile data in Spherical Mercator in the sample. If you are following this guide to create this sample step by step, please copy the *AppData* folder from the project folder to your's. Let's take a look at the code below:
```csharp
ShapefileLayer shapefileLayer = new ShapefileLayer("../../AppData/cntry02-900913.shp");
shapefileLayer.UseRandomStyle(120);
Map1.AddLayers("Dynamic Layers", shapefileLayer);
```
*Note: `shapefileLayer.UseRandomStyle(120)` means we give it a random style with 120 alpha component for the fill color. So the screenshots below might have different fill color.*  

Now, our map becomes this:
![quickstart-guide-wpf-osm-shp](http://i1.piimg.com/567571/d1bcd2c7bd2d7f2f.png)

## Add build-in controls: zoom bar, scale bar etc.
We have a build-in pan-zoom bar which allows we to zoom the map with. We could add it by XAML or programmically in the code behind. I guess most XAML developers like to write code in the XAML part. We will go this way. We are going to add a zoom bar to the upper right corner, a scale bar on the lower left corner and text to display current viewport's information on the lower right corner. See the code below:  
```xml
<!--  ZoomBar  -->
<Wpf:ZoomBar Margin="20"
             HorizontalAlignment="Right"
             VerticalAlignment="Top"
             Map="{Binding ElementName=Map1}" />

<!--  ScaleBar  -->
<Wpf:ScaleBar Margin="20"
              HorizontalAlignment="Left"
              VerticalAlignment="Bottom"
              Map="{Binding ElementName=Map1}" />

<!--  LocationInfoText  -->
<Wpf:LocationInfoText Margin="20"
                      HorizontalAlignment="Right"
                      VerticalAlignment="Bottom"
                      Map="{Binding ElementName=Map1}" />
```
*Note: one important setting is the `Map` property. we have to bind the defined map to the control.*  

Now, our map becomes this:
![quickstart-guide-wpf-controls](http://p1.bqimg.com/567571/0c7145c9a188e320.png)

## Identify a feature and highlight it.
At current step, our map is static map. Although we have build-in function like guesture, mouse interaction etc. But that is not enough for a map control.  
In this section, we are going to do some custom interaction with map. Like the normal map that allows to identify a feature and show some information of the feature on the map.

1. add a click event on the map. We will add it in XAML like we used to do.  
![quickstart-guide-wpf-click-event](http://i1.piimg.com/567571/66896b73bbb7b557.png)
2. Implement the event as following.
```csharp
private void Map1_MapSingleClick(object sender, MapSingleClickEventArgs e)
{
    // We added a ShapefileLayer in the Loaded event, 
    // it's default name is the name of the shapefile.
    // so here, we could find the layer back by the shapefile name without extension. 
    FeatureLayer featureLayer = Map1.FindLayer<FeatureLayer>("cntry02-900913");
    Feature identifiedFeature = IdentifyHelper.Identify(featureLayer, e.WorldCoordinate, Map1.CurrentScale, Map1.MapUnit)
        .FirstOrDefault();

    Map1.Placements.Clear();
    if (identifiedFeature != null)
    {
        Popup popup = new Popup();
        popup.Location = e.WorldCoordinate;
        popup.Content = new Label { Content = identifiedFeature.FieldValues["LONG_NAME"] };
        Map1.Placements.Add(popup);
    }
}
```
Done, press `F5` to run your first fantasy map application. It's pretty simple. 
![quickstart-guide-wpf-final](http://i1.piimg.com/567571/13216e41ca9ae34b.png)

[Get the source code](https://github.com/SlimGIS/QuickstartSampleForWPF).

I'm sure you have more ideas for this guide. Please feel free to create a pull request, we are glad to take suggestions from you. Also, let us know how you think by dev@slimgis.com.
