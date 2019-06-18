using System;
using System.Collections.Generic;
using mapapp.Models;
using MvvmHelpers;

namespace mapapp.ViewModels {
	public class EmergencyViewModel : BaseDataViewModel {

		public ObservableRangeCollection<EmergencyModel> EmergencyList { get; private set; }

		public EmergencyViewModel () {
			EmergencyList = new ObservableRangeCollection<EmergencyModel>();

			EmergencyModel pnp = new EmergencyModel() {
				ContactName = "Philippine National Police (PNP) Hotline Patrol",
				Contact = "117",
				Icon = "emergency_police.png"
			};

			EmergencyModel nddrmc = new EmergencyModel() {
				ContactName = "National Disaster and Risk Reduction and Management Council (NDRRMC)",
				Contact = "(02) 911-1406",
				Icon = "emergency_ndrmmc.png"
			};

			EmergencyModel bofp = new EmergencyModel() {
				ContactName = "Bureau of Fire Protection (NCR)",
				Contact = "(02) 729-5166",
				Icon = "emergency_fire.png"
			};

			EmergencyModel dotc = new EmergencyModel() {
				ContactName = "Department of Transportation and Communications (DOTC)",
				Contact = "7890",
				Icon = "emergency_dotc.png"
			};

			EmergencyModel mmda = new EmergencyModel() {
				ContactName = "Metro Manila Development Authority (MMDA) Metrobase",
				Contact = "136",
				Icon = "emergency_mmda.png"
			};

			EmergencyModel dpwh = new EmergencyModel() {
				ContactName = "Department of Public Works and Highways",
				Contact = "(02) 304-3713",
				Icon = "emergency_dpwh.png"
			};

			EmergencyModel redcross = new EmergencyModel() {
				ContactName = "Red Cross",
				Contact = "(02) 911-1876",
				Icon = "emergency_redcross.png"
			};

			EmergencyList.Add(pnp);
			EmergencyList.Add(nddrmc);
			EmergencyList.Add(bofp);
			EmergencyList.Add(dotc);
			EmergencyList.Add(mmda);
			EmergencyList.Add(dpwh);
			EmergencyList.Add(redcross);
		}
	}
}
