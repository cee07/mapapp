using System;
namespace mapapp.ViewModels {
	public class MainPageViewModel : BaseDataViewModel {

		public bool IsLoggedIn {
			get { return Xamarin.Essentials.Preferences.Get("IsLoggedIn", false); }

		}

	}
}
