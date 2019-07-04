using System;
using System.Collections.Generic;
using mapapp.Models;
using mapapp.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace mapapp.Views {
	public partial class SubscriptionView : ContentView {

		private SubscriptionViewModel subscriptionViewModel;

		private bool isBusy = false;

		public SubscriptionView () {
			InitializeComponent();
			BindingContext = subscriptionViewModel = new SubscriptionViewModel();
			subscriptionViewModel.OnSubscriptionsRequested += OnInitializedSubscriptions;
		}

		public void InitializeSubscriptions () {
			subscriptionViewModel.RequestSubscriptionsCommand.Execute(null);
		}

		void OnInitializedSubscriptions(List<SubscriptionModel> obj) {
			if(obj != null) {
				if (string.IsNullOrEmpty(obj[0].Status)) {
					subscriptionsPanel.Children.Clear();
					for (int index = 0 ; index < obj.Count ; index++) {
						var subscriptionModel = obj[index];
						if (subscriptionModel != null) {
							if (index > 0) {
								BoxView boxView = new BoxView() { Color = Color.FromHex("#77A3B1"), HeightRequest = 2, Margin = new Thickness(20, 0, 20, 0) };
								subscriptionsPanel.Children.Add(boxView);
							}
							SubscriptionTicketsView subView = new SubscriptionTicketsView(subscriptionModel);
							subscriptionsPanel.Children.Add(subView);
							var tapGestureRecognizer = new TapGestureRecognizer();
							tapGestureRecognizer.Tapped += (object sender, EventArgs e) => {
								string html = string.Format("{0}?email={1}", subscriptionModel.URL, Preferences.Get("email", null));
								Device.BeginInvokeOnMainThread(async () => {
									await Navigation.PushAsync(new CouponPage(html));
								});
							};
							subView.GestureRecognizers.Add(tapGestureRecognizer);
						}
					}
				}
			}
		}


	}
}
