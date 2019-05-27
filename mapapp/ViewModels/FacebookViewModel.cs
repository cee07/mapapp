using System;
using System.Threading.Tasks;
using mapapp.Handlers;
using mapapp.Models;
using mapapp.Services;

namespace mapapp.ViewModels {
	public class FacebookViewModel : BaseDataViewModel {

		public System.Action OnRegistrationRequestFinished;

		private RegistrationHandler registrationHandler;

		public FacebookViewModel() {
			registrationHandler = new RegistrationHandler();
			registrationHandler.OnRequestFinished += OnRegisterFinished;
		}

		public async Task<FacebookProfileModel> RequestFacebookProfileAsync (string accessToken) {
			var facebookServices = new FacebookServices();
			var facebookProfile = await facebookServices.GetFacebookProfileAsync(accessToken);
			return facebookProfile;
		}

		public async Task Register (string email) {
			await registrationHandler.Register(email);
		}

		void OnRegisterFinished() {
			OnRegistrationRequestFinished?.Invoke();
		}
	}
}
