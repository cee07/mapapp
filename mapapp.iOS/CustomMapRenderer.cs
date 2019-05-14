using System;
using System.Collections.Generic;
using CoreGraphics;
using mapapp.Helpers;
using MapKit;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

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
				annotationView.Image = UIImage.FromFile("pin.png");
				annotationView.CalloutOffset = new CGPoint(0, 0);
				((CustomMKAnnotationView) annotationView).ID = customPin.PinType.ToString();
			}
			annotationView.CanShowCallout = true;

			return annotationView;
		}

		//private UIImage GetImageAsset (PinType pinType) {
		//	string assetName = null;
		//	switch (pinType) {
		//		case PinType.Hospital:
		//			assetName = "pin_gray.png";
		//			break;
		//		case PinType.Clinic:
		//			assetName = "pin_blue.png";
		//			break;
		//		case PinType.Dental:
		//			assetName = "pin_orange.png";
		//			break;
		//		default:
		//			assetName = "pin_green.png";
		//			break;
		//	}
		//	return UIImage.FromFile(assetName);
		//}

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
