using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using mapapp.Models;
using mapapp.ViewModels;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Plugin.Connectivity;

namespace mapapp.Views {
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : Xamarin.Forms.TabbedPage {

		private readonly MainPageViewModel mainPageViewModel;

		private NavigationPage mapPage;

		public MainPage () {
			InitializeComponent();
			BindingContext = mainPageViewModel = new MainPageViewModel();
			mapPage = new NavigationPage(new MapPage()) { Icon = "menu_home.png", Title = "Home", BarBackgroundColor = Color.FromHex("#539EB3") };
			this.Children.Add(mapPage);
			if (mainPageViewModel.IsLoggedIn) {
				this.Children.Add(new NavigationPage(new PinPage()) { Icon = "menu_pins.png", Title = "Pins", BarBackgroundColor = Color.FromHex("#539EB3") });
				this.Children.Add(new NavigationPage(new ProfilePage()) { Icon = "menu_me.png", Title = "Me", BarBackgroundColor = Color.FromHex("#539EB3") });
			}
			this.Children.Add(new NavigationPage(new FeedPage()) { Icon = "menu_feeds.png", Title = "Feed", BarBackgroundColor = Color.FromHex("#539EB3") });
			this.Children.Add(new NavigationPage(new EmergencyContactPage()) { Icon = "menu_emergency.png", Title = "Emergency", BarBackgroundColor = Color.FromHex("#C54F4E") });
		}

		public void ShowPinDetailPage (PinModel pinModel) {
			if (CrossConnectivity.Current.IsConnected) {
				string recentPinsData = Preferences.Get("RecentPins", null);
				string pinData = null;
				if (!string.IsNullOrEmpty(recentPinsData)) {
					var savedPins = JsonConvert.DeserializeObject<List<PinModel>>(recentPinsData);
					bool hasSaved = savedPins.Exists(x => x.EstablishmentName == pinModel.EstablishmentName &&
													 x.Address == pinModel.Address);
					if (hasSaved) {
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

				Preferences.Set("RecentPins", pinData);
				Device.BeginInvokeOnMainThread(async () => {
					await this.CurrentPage.Navigation.PushAsync(new PinDetailPage(pinModel) { Title = pinModel.EstablishmentName });
				});
			} else {
				Xamarin.Forms.Application.Current.MainPage.DisplayAlert("No Internet Access", "Please check your internet connection.", "OK");
			}
		}

	}
}