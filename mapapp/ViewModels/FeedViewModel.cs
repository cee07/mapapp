using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using mapapp.Handlers;
using mapapp.Models;
using MvvmHelpers;
using Xamarin.Forms;

namespace mapapp.ViewModels {
	public class FeedViewModel : BaseDataViewModel {

		private FeedHandler feedHandler;

		public ObservableRangeCollection<FeedModel> FeedList { get; private set; }

		public FeedViewModel () {
			feedHandler = new FeedHandler();
			feedHandler.OnFeedRequested += OnFeedRequested;
			FeedList = new ObservableRangeCollection<FeedModel>();
			RequestFeedCommand = new Command(async () => await ExecuteRequestFeedCommand());
		}

		void OnFeedRequested(List<FeedModel> feeds) {
			if (feeds.Count > 0) {
				FeedList.Clear();
				FeedList.AddRange(feeds);
			} else
				Application.Current.MainPage.DisplayAlert("Error",
				                                          "There is no available feed.",
				                                          "OK");
		}

		public ICommand RequestFeedCommand { get; private set; }

		private async Task ExecuteRequestFeedCommand() {
			try {
				IsBusy = true;
				await feedHandler.RequestFeed();
			} finally {
				IsBusy = false;
			}
		}
	}
}
