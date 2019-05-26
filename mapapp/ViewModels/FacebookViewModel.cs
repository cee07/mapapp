using System;
using System.Threading.Tasks;
using mapapp.Models;
using mapapp.Services;

namespace mapapp.ViewModels {
	public class FacebookViewModel : BaseDataViewModel {

		public async Task<FacebookProfileModel> RequestFacebookProfileAsync (string accessToken) {
			var facebookServices = new FacebookServices();
			var facebookProfile = await facebookServices.GetFacebookProfileAsync(accessToken);
			return facebookProfile;
		}
	}
}
