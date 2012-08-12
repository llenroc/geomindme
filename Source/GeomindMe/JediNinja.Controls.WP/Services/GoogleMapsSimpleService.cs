using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace JediNinja.Controls.WP.Services
{
	class RequestInfo
	{
		public RequestInfo()
		{
			Buffer = new byte[BUFFER_SIZE];
			Data = new StringBuilder();
		}

		public HttpWebRequest Request { get; set; }
		public HttpWebResponse Response { get; set; }
		public StringBuilder Data { get; set; }
		public Stream ResponseStream { get; set; }
		public byte[] Buffer { get; set; }
		public const int BUFFER_SIZE = 1024;
		public Action<string> OnDataReadCompleted { get; set; }
	}



	public class GoogleMapsSimpleService
	{
		#region AddressByGeoLocation
		Action<string> _onAddressByGeoLocationCallback;
		public void GetAddressByGeoLocation(GeoLocation geoLocation, Action<string> addressByGeoLocationCallback)
		{
			_onAddressByGeoLocationCallback = addressByGeoLocationCallback;

			string formattedGeoLocation = Uri.EscapeUriString(string.Format("{0},{1}", geoLocation.Latitude, geoLocation.Longitude));
			string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=true", formattedGeoLocation);

			DownloadString(requestUri,
				(data) =>
				{
					string address = ExtractAddressFromGeocodeData(data);
					//GeoLocation geoLocation = new GeoLocation(42.642364f, 23.337971f);//some test data
					if (_onAddressByGeoLocationCallback != null)
					{
						_onAddressByGeoLocationCallback(address);
						_onAddressByGeoLocationCallback = null;
					}
				});
		}

		private string ExtractAddressFromGeocodeData(string data)
		{
			var geocodeResponse = GeoLocationPicker.Helpers.Serializer<GeocodeResponse>.DeserializeFromXMLString(data);
			string address = geocodeResponse.result[0].address_component[0].long_name;
			return address;
		}

		#endregion

		#region GeoLocationByAddress
		Action<GeoLocation> _onGeoLocationByAddressCallback;
		public void GetGeoLocationByAddress(string address, Action<GeoLocation> geoLocationByAddressCallback)
		{
			_onGeoLocationByAddressCallback = geoLocationByAddressCallback;

			string escapedAddress = Uri.EscapeUriString(address);
			string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=true", escapedAddress);

			DownloadString(requestUri, 
				(data) =>
				{
					GeoLocation geoLocation = ExtractGeoLocationFromGeoCodeData(data);
					//GeoLocation geoLocation = new GeoLocation(42.642364f, 23.337971f);//some test data
					if (_onGeoLocationByAddressCallback != null)
					{
						_onGeoLocationByAddressCallback(geoLocation);
						_onGeoLocationByAddressCallback = null;
					}
				});
		}
		private GeoLocation ExtractGeoLocationFromGeoCodeData(string data)
		{
			var geocodeResponse = GeoLocationPicker.Helpers.Serializer<GeocodeResponse>.DeserializeFromXMLString(data);

			double lat = 0.0f;
			string latStr = geocodeResponse.result[0].geometry[0].location[0].lat;
			if (!double.TryParse(latStr, out lat))
			{
				lat = 0.0f;
			}

			double lon = 0.0f;
			string lonStr = geocodeResponse.result[0].geometry[0].location[0].lng;
			if (!double.TryParse(lonStr, out lon))
			{
				lon = 0.0f;
			}

			var geoLocation = new GeoLocation(lat, lon);

			return geoLocation;
		}
		#endregion
		#region HttpDownloadString
		private void DownloadString(string requestUri, Action<string> ondDownloadCompleted)
		{
			var request = (HttpWebRequest)WebRequest.Create(requestUri);
			RequestInfo requestInfo = new RequestInfo();
			requestInfo.Request = request;
			requestInfo.OnDataReadCompleted = ondDownloadCompleted;
			var asyncResult = (IAsyncResult)request.BeginGetResponse(new AsyncCallback(ResponseCallback), requestInfo);
		}

		public void ResponseCallback(IAsyncResult asyncResult)
		{
			try
			{
				var requestInfo = asyncResult.AsyncState as RequestInfo;
				var request = requestInfo.Request;

				var response = (HttpWebResponse)request.EndGetResponse(asyncResult);
				requestInfo.Response = response;

				var stream = response.GetResponseStream();
				requestInfo.ResponseStream = stream;

				var buffer = requestInfo.Buffer;
				stream.BeginRead(buffer, 0, buffer.Length, ReadCallback, requestInfo);
			}
			catch (WebException we)
			{
				Deployment.Current.Dispatcher.BeginInvoke(
						() =>
						{
							throw we;
						}
				);
			}
		}

		public void ReadCallback(IAsyncResult asyncResult)
		{
			var requestInfo = asyncResult.AsyncState as RequestInfo;
			var responseStream = requestInfo.ResponseStream;

			int readBytesCount = responseStream.EndRead(asyncResult);
			if (readBytesCount > 0)
			{
				var buffer = requestInfo.Buffer;
				string readData = Encoding.UTF8.GetString(buffer, 0, readBytesCount);
				var data = requestInfo.Data;
				data.Append(readData);

				IAsyncResult asyncReadResult = (IAsyncResult)responseStream.BeginRead(buffer, 0, buffer.Length, ReadCallback, requestInfo);
			}
			else
			{
				var onDataReadCompleted = requestInfo.OnDataReadCompleted;
				if (onDataReadCompleted != null)
				{
					var data = requestInfo.Data.ToString();
					onDataReadCompleted(data);
				}

				responseStream.Close();
			}
		}
		#endregion
	}
}
