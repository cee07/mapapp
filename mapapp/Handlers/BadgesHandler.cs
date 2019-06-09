using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using mapapp.Models;

namespace mapapp.Handlers {
	public class BadgesHandler : BaseDataHandler {

		public System.Action<List<BadgeModel>> OnBadgesReceived;

		private JsonWebRequest<List<BadgeModel>> request;

		public async Task RequestBadges(string email) {
			APIForm apiForm = new APIForm();
			apiForm.AddField("email", email);
			request = JsonWebRequest<List<BadgeModel>>.CreateRequest(HttpMethod.POST, ApiUrl.API.BADGE_LIST, apiForm);
			request.OnAPICallSuccessful += OnAPICallSuccessful;
			request.HasError += OnErrorOccured;
			request.HasTimedOut += OnTimedOut;
			await request.GetData();
		}

		protected override async Task OnAPICallSuccessful () {
			request.OnAPICallSuccessful -= OnAPICallSuccessful;
			OnBadgesReceived?.Invoke(request.Data);
		}

		protected override async Task OnErrorOccured () {
			request.HasError -= OnErrorOccured;
		}

		protected override void OnTimedOut () {
			request.HasTimedOut -= OnTimedOut;
		}
	}
}
