using SlimGis.MapKit.Geometries;
using SlimGis.MapKit.Layers;
using SlimGis.MapKit.Utilities;
using SlimGis.MapKit.Wpf;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace QuickstartSampleForWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Map1_Loaded(object sender, RoutedEventArgs e)
        {
            Map1.MapUnit = GeoUnit.Meter;

            Map1.UseOpenStreetMapAsBaseMap();

            ShapefileLayer shapefileLayer = new ShapefileLayer("../../AppData/countries-900913.shp");
            shapefileLayer.UseRandomStyle(120);
            Map1.AddStaticLayers("Dynamic Layers", shapefileLayer);

            Map1.ZoomToFullBound();
        }

        private void Map1_MapClick(object sender, MapClickEventArgs e)
        {
            // We added a ShapefileLayer in the Loaded event, 
            // it's default name is the name of the shapefile.
            // so here, we could find the layer back by the shapefile name without extension. 
            FeatureLayer featureLayer = Map1.FindLayer<FeatureLayer>("countries-900913");
            Feature identifiedFeature = IdentifyHelper.Identify(featureLayer, e.WorldCoordinate, Map1.CurrentScale, Map1.MapUnit).FirstOrDefault();

            Map1.Placements.Clear();
            if (identifiedFeature != null)
            {
                Popup popup = new Popup();
                popup.Location = e.WorldCoordinate;
                popup.Content = new Label { Content = identifiedFeature.FieldValues["LONG_NAME"] };
                Map1.Placements.Add(popup);
            }
        }
    }
}
