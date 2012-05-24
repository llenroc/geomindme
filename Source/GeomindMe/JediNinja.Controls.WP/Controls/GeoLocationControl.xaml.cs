using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using JediNinja.Controls.WP.Services;
using JediNinja.Controls.WP;
using System.Windows.Controls.Primitives;
using System.Device.Location;
using Microsoft.Phone.Controls.Maps;

namespace JediNinja.Controls.WP
{
    public partial class GeoLocationControl : UserControl
    {
        public GeoLocationControl()
        {
            InitializeComponent();
        }

        public double Latitude
        {
            get
            {
                double val = 0.0f;
                if (!double.TryParse(LatitudeTextBlock.Text, out val))
                {
                    return 0;
                }

                return val;
            }
            set
            {
                LatitudeTextBlock.Text = string.Format("{0}", value);
            }
        }
        public double Longitude
        {
            get
            {
                double val = 0.0f;
                if (!double.TryParse(LongitudeTextBlock.Text, out val))
                {
                    return 0;
                }

                return val;
            }
            set
            {
                LongitudeTextBlock.Text = string.Format("{0}", value);
            }
        }

        public string Address
        {
            get
            {
                return SearchTextBox.Text;
            }
            set
            {
                SearchTextBox.Text = value;
            }
        }

        public void SetProperties(string address, double latitude, double longitude)
        {
            this.Address = address;
            this.Latitude = latitude;
            this.Longitude = longitude;

            NavigateToPositionOnMap();
        }

        public void NavigateToPositionOnMap()
        {
            GeoCoordinate geoCoordinate = new GeoCoordinate(Latitude, Longitude);

            Pushpin pin = locationPushPin;
            pin.Location = geoCoordinate;
            pin.Content = Address;
            NavigateToMap(geoCoordinate);
        }




        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string address = SearchTextBox.Text;
            if (string.IsNullOrEmpty(address))
            {
                return;
            }

            GpsLocationRetrievalProgress.IsEnabled = true;
            GoogleMapsSimpleService googleMapsService = new GoogleMapsSimpleService();
            googleMapsService.GetGeoLocationByAddress(address, OnGetGeoLocationByAddress);
        }

        public void OnGetGeoLocationByAddress(GeoLocation geoLocation)
        {
            Dispatcher.BeginInvoke(
                () =>
                {
                    GpsLocationRetrievalProgress.IsEnabled = false;
                    //PositionTextBox.Text = geoLocation.ToString();
                    Latitude = geoLocation.Latitude;
                    Longitude = geoLocation.Longitude;

                    GeoCoordinate geoCoordinate = new GeoCoordinate(geoLocation.Latitude, geoLocation.Longitude);
                    var pinPoint = GeoMap.LocationToViewportPoint(geoCoordinate);

                    Pushpin pin = locationPushPin;
                    pin.Location = geoCoordinate;
                    pin.Content = Address;
                    NavigateToMap(geoCoordinate);
                }
            );
        }

        private void NavigateToMap(GeoCoordinate location)
        {
            GeoMap.Center = location;
            GeoMap.ZoomLevel = 14;
        }

        private void ZoomInButton_Click(object sender, RoutedEventArgs e)
        {
            GeoMap.ZoomLevel++;
        }

        private void ZoomOutButton_Click(object sender, RoutedEventArgs e)
        {
            GeoMap.ZoomLevel--;
        }

        private void MeButton_Click(object sender, RoutedEventArgs e)
        {
            GetCurrentGpsPosition();
        }

        private void GetCurrentGpsPosition()
        {
            GeoCoordinateWatcher geoWatcher = new GeoCoordinateWatcher();
            geoWatcher.MovementThreshold = 50;
            geoWatcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(geoWatcher_PositionChanged);
            geoWatcher.TryStart(false, TimeSpan.FromSeconds(30));
        }

        void geoWatcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            var position = e.Position;
            if (position == null)
            {
                return;
            }
            if (position.Location == GeoCoordinate.Unknown)
            {
                return;
            }

            var currentLocation = position.Location;
            this.Latitude = currentLocation.Latitude;
            this.Longitude = currentLocation.Longitude;

            var pushPin = locationPushPin;
            pushPin.Location = currentLocation;
            string content = "Me";
            pushPin.Content = content;
            NavigateToMap(currentLocation);

            var geoWatcher = sender as GeoCoordinateWatcher;
            if (geoWatcher == null)
            {
                return;
            }
            geoWatcher.Stop();
        }

    }
}
