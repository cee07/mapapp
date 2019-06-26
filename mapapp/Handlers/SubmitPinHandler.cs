using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace mapapp.Handlers {
	public class SubmitPinHandler : BaseDataHandler {

		public EventHandler OnPinRequested;

		private JsonWebRequest<BaseDataModel[]> request; 

		public async Task SubmitPin (string email, string establishmentName,
		                            string address, string contact,
		                            string category, string info) {
			APIForm aPIForm = new APIForm();
			aPIForm.AddField("email", email);
			aPIForm.AddField("establishment", establishmentName);
			aPIForm.AddField("address", address);
			aPIForm.AddField("contact", contact);
			aPIForm.AddField("category", category);
			aPIForm.AddField("add_info", info);
			request = JsonWebRequest<BaseDataModel[]>.CreateRequest(HttpMethod.POST, ApiUrl.API.SUBMIT_PIN, aPIForm);
			request.OnAPICallSuccessful += OnAPICallSuccessful;
			request.HasError += OnErrorOccured;
			request.HasTimedOut += OnTimedOut;
			await request.GetData();
		}

		protected override async Task OnAPICallSuccessful () {
			request.OnAPICallSuccessful -= OnAPICallSuccessful;
			OnPinRequested?.Invoke(null, null);
		}

		protected override async Task OnErrorOccured () {
			request.HasError -= OnErrorOccured;
		}

		protected override void OnTimedOut () {
			request.HasTimedOut -= OnTimedOut;
		}
	}
}
