using System;
using mapapp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mapapp.Views {
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : TabbedPage {

		private readonly MainPageViewModel mainPageViewModel;

		public MainPage () {
			InitializeComponent();
			BindingContext = mainPageViewModel = new MainPageViewModel();

			this.Children.Add(new NavigationPage(new MapPage()) { Icon = "menu_home.png", Title = "Home" });
			if (mainPageViewModel.IsLoggedIn) {
				this.Children.Add(new NavigationPage(new PinPage()) { Icon = "menu_pins.png", Title = "Pins" });
				this.Children.Add(new NavigationPage(new ProfilePage()) { Icon = "menu_me.png", Title = "Me" });
			}
			this.Children.Add(new NavigationPage(new FeedPage()) { Icon = "menu_feeds.png", Title = "Feeds" });
			this.Children.Add(new NavigationPage(new EmergencyContactPage()) { Icon = "menu_emergency.png", Title = "Emergency" });
		}

	}
}