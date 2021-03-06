﻿using System;
using System.Collections.Generic;
using mapapp.ViewModels;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace mapapp.Views {
	public partial class SubmitPinsListView : ContentView {

		private SubmitPinsListViewModel submitPinsListViewModel;

		public SubmitPinsListView () {
			InitializeComponent();
			BindingContext = submitPinsListViewModel = new SubmitPinsListViewModel();
			submitPinsListViewModel.OnPinsRequested += OnPinsRequested;
		}

		public void InitializeSubmittedPins () {
			submitPinsListViewModel.RequestSubmittedPins.Execute(null);
		}

		void OnPinsRequested() {
			submittedPinsParent.Children.Clear();
			if (submitPinsListViewModel.PinList.Count > 0) {
				for (int index = 0 ; index < submitPinsListViewModel.PinList.Count ; index++) {
					var pin = submitPinsListViewModel.PinList[index];
					if (pin != null) {
						if (index > 0) {
							BoxView boxView = new BoxView() { Color = Color.FromHex("#529eb6"), HeightRequest = 2, Margin = new Thickness(20, 0, 20, 0) };
							submittedPinsParent.Children.Add(boxView);
						}
						PinInfoView pinInfoView = new PinInfoView(pin);
						submittedPinsParent.Children.Add(pinInfoView);
						if (pin.Active.Equals("1")) {
							var tapGestureRecognizer = new TapGestureRecognizer();
							tapGestureRecognizer.Tapped += async (object sender, EventArgs e) => {
								if (CrossConnectivity.Current.IsConnected) {
									var pinView = (PinInfoView) sender;
									PinDetailPage pinDetailPage = new PinDetailPage(pinView.PinModel) { Title = pinView.PinModel.EstablishmentName };
									await Navigation.PushAsync(pinDetailPage);
								} else {
									await Application.Current.MainPage.DisplayAlert("No Internet Access", "Please check your internet connection.", "OK");
								}
							};
							pinInfoView.GestureRecognizers.Add(tapGestureRecognizer);
						}
					}
				}
			} 
		}
	}
}
