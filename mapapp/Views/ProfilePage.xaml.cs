using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace mapapp.Views {
	public partial class ProfilePage : ContentPage {

		private SubscriptionView subscriptionView;
		private BadgesView badgesView;

		public ProfilePage () {
			subscriptionView = new SubscriptionView();
			badgesView = new BadgesView();
			InitializeComponent();
			AddViews();
			EnableView(ProfilePageState.Subscription);
		}

		void AddViews() {
			profileViews.Children.Add(subscriptionView);
			profileViews.Children.Add(badgesView);
		}

		void EnableView(ProfilePageState profilePageState) {
			ResetViews();
			switch (profilePageState) {
				case ProfilePageState.Subscription:
					subscriptionView.IsVisible = true;
					break;
				case ProfilePageState.Badges:
					badgesView.IsVisible = true;
					break;
			}
		}

		void ResetViews() {
			subscriptionView.IsVisible = false;
			badgesView.IsVisible = false;
		}

		void OnClickPageState (object sender, System.EventArgs e) {
			var pageStateButton = (Button) sender;
			EnableView((ProfilePageState) pageStateButton.CommandParameter);
		}

		enum ProfilePageState {
			Subscription,
			Badges
		}
	}
}
