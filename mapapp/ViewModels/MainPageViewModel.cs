using System;
namespace mapapp.ViewModels {
	public class MainPageViewModel : BaseDataViewModel {

		public bool IsLoggedIn {
			get {
				string email = Xamarin.Essentials.Preferences.Get("email", null);
				return !string.IsNullOrEmpty(email); 
			}
		}

	}
}
