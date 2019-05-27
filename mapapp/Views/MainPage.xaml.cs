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
		}

	}
}