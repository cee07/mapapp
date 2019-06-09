using System;
using System.Collections.Generic;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Views;
using mapapp.Droid;
using mapapp.Helpers;
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

			if (e.NewElement != null) {
				var formsMap = (CustomMap) e.NewElement;
				customPins = formsMap.CustomPins;
				Control.GetMapAsync(this);
			}
		}

		protected override MarkerOptions CreateMarker (Pin pin) {
			var marker = new MarkerOptions();
			marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
			marker.SetTitle(pin.Label);
			marker.SetSnippet(pin.Address);
			marker.SetIcon(BitmapDescriptorFactory.FromResource(GetResourceId(pin)));
			return marker;
		}

		private int GetResourceId (Pin pin) {
			CustomPin customPin = GetCustomPin(pin);
			int pinResource = (customPin.CouponCount == 0) ? Resource.Drawable.marker_coupon : Resource.Drawable.marker;
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

		public Android.Views.View GetInfoContents (Marker marker) {
			return null;
		}

		public Android.Views.View GetInfoWindow (Marker marker) {
			return null;
		}
	}
}
