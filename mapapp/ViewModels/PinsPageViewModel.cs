using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace mapapp.ViewModels {
	public class PinsPageViewModel : BaseDataViewModel {

		public EventHandler OnClickAddItem;

		public PinsPageViewModel ()  {
			AddItemCommand = new Command(ExecuteAddItemCommand);
		}

		void ExecuteAddItemCommand () {
			OnClickAddItem?.Invoke(null, null);
		} 

		public ICommand AddItemCommand { get; private set; }
	}
}
