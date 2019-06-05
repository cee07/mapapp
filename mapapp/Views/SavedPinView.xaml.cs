using System;
using System.Collections.Generic;
using mapapp.ViewModels;
using Xamarin.Forms;

namespace mapapp.Views {
	public partial class SavedPinView : ContentView {

		private SavedPinViewModel savedPinViewModel;

		public SavedPinView () {
			InitializeComponent();
		}

		public void InitializeSavedPins() {
			BindingContext = savedPinViewModel = new SavedPinViewModel();
			for (int index = 0 ; index < savedPinViewModel.SavedPins.Count ; index++) {
				var savedPin = savedPinViewModel.SavedPins[index];
				if (savedPin != null) {
					if (index > 0) {
						BoxView boxView = new BoxView() { Color = Color.Teal, HeightRequest = 2, Margin = new Thickness(20,0,20,0)};
						savedPinsParent.Children.Add(boxView);
					}
					PinInfoView pinInfoView = new PinInfoView(savedPin);
					savedPinsParent.Children.Add(pinInfoView);
				}
			}
		}

	}
}