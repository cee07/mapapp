using System;
using System.Collections.Generic;
using mapapp.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace mapapp.Views {
	public partial class ProfilePage : ContentPage {

		private SubscriptionView subscriptionView;
		private BadgesView badgesView;

		private ToolbarItem logoutToolbarItem;

		private ProfileViewModel profileViewModel;

		public ProfilePage () {
			subscriptionView = new SubscriptionView();
			badgesView = new BadgesView();
			profileViewModel = new ProfileViewModel();
			profileViewModel.SettingsClicked += OnClickedSettings;
			profileViewModel.LogoutClicked += OnClickedLogout;
			InitializeComponent();
			AddViews();
			EnableView(ProfilePageState.Subscription);
			Title = Preferences.Get("name", null);
			//nameLabel.Text = Preferences.Get("name", null);
			string pic = Preferences.Get("picture", null);
			if (!string.IsNullOrEmpty(pic)) 
				image.Source = ImageSource.FromUri(new Uri(pic));
			var settings = new ToolbarItem() {
				Text = "Settings",
				Order = ToolbarItemOrder.Primary,
				Priority = 0,
				Command = profileViewModel.SettingsClickedCommand
			};
			logoutToolbarItem = new ToolbarItem() {
				Text = "Logout",
				Order = ToolbarItemOrder.Secondary,
				Priority = 1,
				Command = profileViewModel.LogoutClickedCommand
			};
			ToolbarItems.Add(settings);
		}

		void OnClickedSettings(object sender, System.EventArgs e) {
			if (ToolbarItems.Contains(logoutToolbarItem))
				ToolbarItems.Remove(logoutToolbarItem);
			else
				ToolbarItems.Add(logoutToolbarItem);
		}

		void OnClickedLogout(object sender, System.EventArgs e) {
			Preferences.Set("email", null);
			App.GoToLogin();
		}

		void AddViews() {
			profileViews.Children.Add(subscriptionView);
			profileViews.Children.Add(badgesView);
		}

		void EnableView(ProfilePageState profilePageState) {
			ResetViews();
			switch (profilePageState) {
				case ProfilePageState.Subscription:
					meSubs.IsVisible = true;
					subscriptionView.IsVisible = true;
					subscriptionView.InitializeSubscriptions();
					break;
				case ProfilePageState.Badges:
					meBande.IsVisible = true;
					badgesView.IsVisible = true;
					badgesView.Initialize();
					break;
			}
		}

		void ResetViews() {
			subscriptionView.IsVisible = false;
			badgesView.IsVisible = false;
			meSubs.IsVisible = false;
			meBande.IsVisible = false;
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
