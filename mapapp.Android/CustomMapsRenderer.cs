using System;
using System.Collections.Generic;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Views;
using mapapp.Droid;
using mapapp.Helpers;
using mapapp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapsRenderer))]
namespace mapapp.Droid {
	public class CustomMapsRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter {

		private List<CustomPin> customPins;

		public CustomMapsRenderer (Context context) : base(context) {
		}

		protected override void OnElementChanged (ElementChangedEventArgs<Xamarin.Forms.Maps.Map> e) {
			base.OnElementChanged(e);

			if (e.OldElement != null) 
				NativeMap.InfoWindowClick -= OnInfoWindowClick;

			if (e.NewElement != null) {
				var formsMap = (CustomMap) e.NewElement;
				customPins = formsMap.CustomPins;
				Control.GetMapAsync(this);
			}
		}

		protected override void OnMapReady (GoogleMap map) {
			base.OnMapReady(map);
			NativeMap.InfoWindowClick += OnInfoWindowClick;
			NativeMap.SetInfoWindowAdapter(this);
		}

		void OnInfoWindowClick (object sender, GoogleMap.InfoWindowClickEventArgs e) {
			var customPin = GetCustomPin(e.Marker.Position);
			if (customPin != null)
				((MainPage) Xamarin.Forms.Application.Current.MainPage).ShowPinDetailPage(customPin.Model);
		}

		protected override MarkerOptions CreateMarker (Pin pin) {
			var marker = new MarkerOptions();
			marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
			marker.SetTitle(pin.Label);
			//marker.SetSnippet(pin.Address);
			marker.SetIcon(BitmapDescriptorFactory.FromResource(GetResourceId(pin)));
			return marker;
		}

		private int GetResourceId (Pin pin) {
			CustomPin customPin = GetCustomPin(pin);
			int pinResource = (customPin.CouponCount > 0) ? Resource.Drawable.marker_coupon : Resource.Drawable.marker;
			return pinResource;
		}

		private CustomPin GetCustomPin (Pin pin) {
			var position = new Position(pin.Position.Latitude, pin.Position.Longitude);
			if (customPins != null) {
				foreach (var customPin in customPins) {
					if (customPin.Position == position) {
						return customPin;
					}
				}
			}
			return null;
		}

		private CustomPin GetCustomPin(LatLng pos) {
			foreach (var customPin in customPins) {
				if (customPin.Position == new Position(pos.Latitude, pos.Longitude) ) 
					return customPin;
			}
			return null;
		}

		public Android.Views.View GetInfoContents (Marker marker) {
			return null;
		}

		public Android.Views.View GetInfoWindow (Marker marker) {
			return null;
		}
	}
}
