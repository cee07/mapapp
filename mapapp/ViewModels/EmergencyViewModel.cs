using System;
using System.Collections.Generic;
using mapapp.Models;
using MvvmHelpers;

namespace mapapp.ViewModels {
	public class EmergencyViewModel : BaseDataViewModel {

		public ObservableRangeCollection<EmergencyModel> EmergencyList { get; private set; }

		public EmergencyViewModel () {
			EmergencyList = new ObservableRangeCollection<EmergencyModel>();

			EmergencyModel emergencyModel = new EmergencyModel() {
				ContactName = "Red Cross",
				ContactDescription = "Add Description Here",
				ContactNumbers = new List<string>() { "(02) 527-8385", "(02) 527-8386" }
			};

			EmergencyList.Add(emergencyModel);
			EmergencyList.Add(emergencyModel);
			EmergencyList.Add(emergencyModel);
		}
	}
}
