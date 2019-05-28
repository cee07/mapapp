using System;
using System.Threading.Tasks;

namespace mapapp.Handlers {
	public class RateHandler : BaseDataHandler {

		public System.Action<string> OnRateRequested;

		private JsonWebRequest<BaseDataModel> request;
 
		public async Task Rate(string email, string crt, string establishmentID, string rating) {
			APIForm apiForm = new APIForm();
			apiForm.AddField("email", email);
			apiForm.AddField("crt", crt);
			apiForm.AddField("eid", establishmentID);
			apiForm.AddField("rate", rating);
			request = JsonWebRequest<BaseDataModel>.CreateRequest(HttpMethod.POST, ApiUrl.API.RATE, apiForm);
			request.OnAPICallSuccessful += OnAPICallSuccessful;
			request.HasError += OnErrorOccured;
			request.HasTimedOut += OnTimedOut;
			await request.GetData();
		}

		protected override async Task OnAPICallSuccessful () {
			request.OnAPICallSuccessful -= OnAPICallSuccessful;
			OnRateRequested?.Invoke(request.Data.Status);
		}

		protected override async Task OnErrorOccured () {
			request.HasError -= OnErrorOccured;
		}

		protected override void OnTimedOut () {
			request.HasTimedOut -= OnTimedOut;
		}
	}
}
