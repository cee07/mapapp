using System;
using System.Threading.Tasks;

namespace mapapp.Handlers {
	public class RateHandler : BaseDataHandler {

		private JsonWebRequest<BaseDataModel> request;
 
		public async Task Rate(string email, string crt, string establishmentID, string rating) {
			APIForm apiForm = new APIForm();
			apiForm.AddField("email", email);
			apiForm.AddField("crt", crt);
			apiForm.AddField("eid", establishmentID);
			apiForm.AddField("rate", rating);
			//request = JsonWebRequest<BaseDataModel>.CreateRequest(HttpMethod.POST, )
		}

		protected override Task OnAPICallSuccessful () {
			throw new NotImplementedException();
		}

		protected override Task OnErrorOccured () {
			throw new NotImplementedException();
		}

		protected override void OnTimedOut () {
			throw new NotImplementedException();
		}
	}
}
