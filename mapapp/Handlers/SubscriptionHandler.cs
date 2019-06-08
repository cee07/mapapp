using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using mapapp.Models;

namespace mapapp.Handlers {
	public class SubscriptionHandler : BaseDataHandler {

		public System.Action<List<SubscriptionModel>> OnSubsRequested;

		private JsonWebRequest<List<SubscriptionModel>> request;

		public async Task RequestSubscriptions(string email) {
			APIForm apiForm = new APIForm();
			apiForm.AddField("email", email);
			request = JsonWebRequest<List<SubscriptionModel>>.CreateRequest(HttpMethod.POST, ApiUrl.API.SUBSCRIPTION_LIST, apiForm);
			request.OnAPICallSuccessful += OnAPICallSuccessful;
			request.HasError += OnErrorOccured;
			request.HasTimedOut += OnTimedOut;
			await request.GetData();
		}

		protected override async Task OnAPICallSuccessful () {
			request.OnAPICallSuccessful -= OnAPICallSuccessful;
			OnSubsRequested?.Invoke(request.Data);
		}

		protected override async Task OnErrorOccured () {
			request.HasError -= OnErrorOccured;
		}

		protected override void OnTimedOut () {
			request.HasTimedOut -= OnTimedOut;
		}
	}
}
