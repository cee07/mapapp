﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using mapapp.Models;
using mapapp.ViewModels;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mapapp.Views {
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : TabbedPage {

		private readonly MainPageViewModel mainPageViewModel;

		private NavigationPage mapPage;

		public MainPage () {
			InitializeComponent();
			BindingContext = mainPageViewModel = new MainPageViewModel();
			mapPage = new NavigationPage(new MapPage()) {  Icon = "menu_home.png", Title = "Home", BarBackgroundColor = Color.FromHex("#539EB3")};
			this.Children.Add(mapPage);
			if (mainPageViewModel.IsLoggedIn) {
				this.Children.Add(new NavigationPage(new PinPage()) { Icon = "menu_pins.png", Title = "Pins", BarBackgroundColor = Color.FromHex("#539EB3") });
				this.Children.Add(new NavigationPage(new ProfilePage()) { Icon = "menu_me.png", Title = "Me", BarBackgroundColor = Color.FromHex("#539EB3") });
			}
			this.Children.Add(new NavigationPage(new FeedPage()) { Icon = "menu_feeds.png", Title = "Feeds", BarBackgroundColor = Color.FromHex("#539EB3") });
			this.Children.Add(new NavigationPage(new EmergencyContactPage()) { Icon = "menu_emergency.png", Title = "Emergency", BarBackgroundColor = Color.FromHex("#C54F4E") });
		}

		public void ShowPinDetailPage(PinModel pinModel) {
			string recentPinsData = Preferences.Get("RecentPins", null);
			string pinData = null;
			if (!string.IsNullOrEmpty(recentPinsData)) {
				var savedPins = JsonConvert.DeserializeObject<List<PinModel>>(recentPinsData);
				if (savedPins.Contains(pinModel)) {
					savedPins.Remove(pinModel);
					savedPins.Add(pinModel);
				} else {
					savedPins.Add(pinModel);
				}
				pinData = JsonConvert.SerializeObject(savedPins);
			} else {
				var pinList = new List<PinModel>() { pinModel };
				pinData = JsonConvert.SerializeObject(pinList);
			}
			NavigationPage newPage = new NavigationPage(new PinDetailPage(pinModel)) {
				BarBackgroundColor = Color.FromHex("#C54F4E")
			};
			Preferences.Set("RecentPins", pinData);
			mapPage.PushAsync(newPage);
		}

	}
}