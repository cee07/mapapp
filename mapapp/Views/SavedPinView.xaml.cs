﻿using System;
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
			savedPinsParent.Children.Clear();
			for (int index = 0 ; index < savedPinViewModel.SavedPins.Count ; index++) {
				var savedPin = savedPinViewModel.SavedPins[index];
				if (savedPin != null) {
					if (index > 0) {
						BoxView boxView = new BoxView() { Color = Color.FromHex("#77A3B1"), HeightRequest = 2, Margin = new Thickness(20,0,20,0)};
						savedPinsParent.Children.Add(boxView);
					}
					PinInfoView pinInfoView = new PinInfoView(savedPin);
					savedPinsParent.Children.Add(pinInfoView);
					var tapGestureRecognizer = new TapGestureRecognizer();
					tapGestureRecognizer.Tapped += async (object sender, EventArgs e) => {
						var pinView = (PinInfoView) sender;
						//NavigationPage newPage = new NavigationPage(new PinDetailPage(pinView.PinModel)) {
						//	BarBackgroundColor = Color.FromHex("#C54F4E")
						//};
						//await Navigation.PushAsync(newPage);
						await Navigation.PushAsync(new PinDetailPage(pinView.PinModel) {
							Title = pinView.PinModel.EstablishmentName
						});
					};
					pinInfoView.GestureRecognizers.Add(tapGestureRecognizer);
				}
			}
		}
	}
}