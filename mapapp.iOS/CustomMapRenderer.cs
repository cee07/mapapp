using System;
using System.Collections.Generic;
using CoreGraphics;
using mapapp.Helpers;
using mapapp.iOS;
using MapKit;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace mapapp.iOS {
	public class CustomMapRenderer : MapRenderer {

		private UIView customPinView;
		private List<CustomPin> customPins;

		protected override void OnElementChanged (ElementChangedEventArgs<View> e) {
			base.OnElementChanged(e);

			if (e.OldElement != null) {
				var nativeMap = Control as MKMapView;
				nativeMap.GetViewForAnnotation = null;
			}

			if (e.NewElement != null) {
				var formsMap = (CustomMap) e.NewElement;
				var nativeMap = Control as MKMapView;
				customPins = formsMap.CustomPins;

				nativeMap.GetViewForAnnotation = GetViewForAnnotation;
			}
		}

		protected override MKAnnotationView GetViewForAnnotation (MKMapView mapView, IMKAnnotation annotation) {
			MKAnnotationView annotationView = null;

			if (annotation is MKUserLocation)
				return null;

			var customPin = GetCustomPin(annotation as MKPointAnnotation);

			if (customPin == null)
				return null;

			annotationView = mapView.DequeueReusableAnnotation(customPin.PinType.ToString());
			if (annotationView == null) {
				annotationView = new CustomMKAnnotationView(annotation, customPin.Id.ToString());
				annotationView.Image = GetImageAsset(customPin.CouponCount);
				annotationView.CalloutOffset = new CGPoint(0, 0);
				((CustomMKAnnotationView) annotationView).ID = customPin.PinType.ToString();
			}
			annotationView.CanShowCallout = true;

			return annotationView;
		}

		private UIImage GetImageAsset (int couponCount) {
			return UIImage.FromFile((couponCount > 0) ? "marker_coupon.png" : "marker.png" );
		}

		void OnDidDeselectAnnotationView (object sender, MKAnnotationViewEventArgs e) {
			if (!e.View.Selected) {
				customPinView.RemoveFromSuperview();
				customPinView.Dispose();
				customPinView = null;
			}
		}

		private CustomPin GetCustomPin (MKPointAnnotation annotation) {
			if (annotation != null) {
				var position = new Position(annotation.Coordinate.Latitude, annotation.Coordinate.Longitude);

				foreach (var pin in customPins) {
					if (pin.Position == position)
						return pin;
				}
			}

			return null;
		}

	}
}
