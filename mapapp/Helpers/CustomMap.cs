using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace mapapp.Helpers {
	public class CustomMap : Map {

		public static readonly BindableProperty CustomPinsProperty =
			BindableProperty.Create(
				"CustomPins",
				typeof(List<CustomPin>),
				typeof(CustomMap),
				null,
				propertyChanged: CustomPinsPropertyChanged);

		private static void CustomPinsPropertyChanged (BindableObject bindable, object oldValue, object newValue) {
			var control = (CustomMap) bindable;
			if (control.CustomPins != null) {
				control.Pins.Clear();
				foreach (CustomPin customPin in control.CustomPins)
					control.Pins.Add((Pin) customPin);
			}
		}

		public List<CustomPin> CustomPins {
			get {
				return (List<CustomPin>) GetValue(CustomPinsProperty);
			}
			set {
				SetValue(CustomPinsProperty, value);
			}
		}

		public static readonly BindableProperty OriginProperty =
			BindableProperty.Create(
				"Origin",
				typeof(Position),
				typeof(CustomMap),
				new Position(0, 0),
				propertyChanged: OriginPropertyChanged);

		private static void OriginPropertyChanged (BindableObject bindable, object oldValue, object newValue) {
			var control = (CustomMap) bindable;
			var position = (Position) newValue;
			control.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(1)));
		}

		public Position Origin {
			get {
				return (Position) GetValue(OriginProperty);
			}
			set {
				SetValue(OriginProperty, value);
			}
		}

	}
}
