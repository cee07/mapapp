using System;
using System.Net.Http;
using System.Threading.Tasks;
using mapapp.Models;
using Newtonsoft.Json;

namespace mapapp.Services {
	public class FacebookServices {

		public async Task<FacebookProfileModel> GetFacebookProfileAsync (string accessToken) {
			var requestUrl =
				"https://graph.facebook.com/v3.3/me/?fields=name,email&access_token="
				+ accessToken;

			var httpClient = new HttpClient();
			var userJson = await httpClient.GetStringAsync(requestUrl);
			var facebookProfile = JsonConvert.DeserializeObject<FacebookProfileModel>(userJson);

			return facebookProfile;
		}

	}
}
