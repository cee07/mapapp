using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using mapapp.Models;
using Xamarin.Forms;

namespace mapapp.Handlers {
	public class FeedHandler : BaseDataHandler {

		public System.Action<List<FeedModel>> OnFeedRequested;

		private JsonWebRequest<List<FeedModel>> request;

		public async Task RequestFeed() {
			request = JsonWebRequest<List<FeedModel>>.CreateRequest(HttpMethod.GET, ApiUrl.API.FEED);
			request.OnAPICallSuccessful += OnAPICallSuccessful;
			request.HasError += OnErrorOccured;
			request.HasTimedOut += OnTimedOut;
			await request.GetData();
		}

		protected override async Task OnAPICallSuccessful () {
			request.OnAPICallSuccessful -= OnAPICallSuccessful;
			OnFeedRequested?.Invoke(request.Data);
		}

		protected override async Task OnErrorOccured () {
			request.HasError -= OnErrorOccured;
			await Application.Current.MainPage.DisplayAlert("Error",
			                                                "There is no available feed.",
			                                                "OK");
		}

		protected override void OnTimedOut () {
			request.HasTimedOut -= OnTimedOut;
		}
	}
}