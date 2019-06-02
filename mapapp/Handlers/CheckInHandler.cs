using System;
using System.Threading.Tasks;

namespace mapapp.Handlers {
	public class CheckInHandler : BaseDataHandler {

		public System.Action<string> OnCheckInRequested;

		private JsonWebRequest<BaseDataModel> request;

		public async Task CheckIn (string email, string ctr, string establishmentID, string category) {
			APIForm apiForm = new APIForm();
			apiForm.AddField("email", email);
			apiForm.AddField("ctr", ctr);
			apiForm.AddField("eid", establishmentID);
			apiForm.AddField("category", category);
			request = JsonWebRequest<BaseDataModel>.CreateRequest(HttpMethod.POST, ApiUrl.API.CHECKIN, apiForm);
			request.OnAPICallSuccessful += OnAPICallSuccessful;
			request.HasError += OnErrorOccured;
			request.HasTimedOut += OnTimedOut;
			await request.GetData();
		}

		protected override async Task OnAPICallSuccessful () {
			request.OnAPICallSuccessful -= OnAPICallSuccessful;
			OnCheckInRequested?.Invoke(request.Data.Status);
		}

		protected override async Task OnErrorOccured () {
			request.HasError -= OnErrorOccured;
		}

		protected override void OnTimedOut () {
			request.HasTimedOut -= OnTimedOut;
		}

	}
}
