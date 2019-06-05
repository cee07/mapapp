using System;
using System.Collections.Generic;
using mapapp.ViewModels;
using Xamarin.Forms;

namespace mapapp.Views {
	public partial class RecenPinView : ContentView {

		private RecentPinViewModel recentPinViewModel;

		public RecenPinView () {
			InitializeComponent();
		}

		public void InitializeRecentPins () {
			BindingContext = recentPinViewModel = new RecentPinViewModel();
			recentPinsParent.Children.Clear();
			for (int index = 0 ; index < recentPinViewModel.RecentPins.Count ; index++) {
				var savedPin = recentPinViewModel.RecentPins[index];
				if (savedPin != null) {
					if (index > 0) {
						BoxView boxView = new BoxView() { Color = Color.Teal, HeightRequest = 2, Margin = new Thickness(20, 0, 20, 0) };
						recentPinsParent.Children.Add(boxView);
					}
					PinInfoView pinInfoView = new PinInfoView(savedPin);
					recentPinsParent.Children.Add(pinInfoView);
					var tapGestureRecognizer = new TapGestureRecognizer();
					tapGestureRecognizer.Tapped += async (object sender, EventArgs e) => {
						var pinView = (PinInfoView) sender;
						await Navigation.PushAsync(new PinDetailPage(pinView.PinModel));
					};
					pinInfoView.GestureRecognizers.Add(tapGestureRecognizer);
				}
			}
		}
	}
}
