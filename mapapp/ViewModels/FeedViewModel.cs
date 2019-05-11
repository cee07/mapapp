using System;
using mapapp.Models;
using MvvmHelpers;

namespace mapapp.ViewModels {
	public class FeedViewModel {

		public ObservableRangeCollection<FeedModel> FeedList { get; private set; }

		public FeedViewModel () {
			FeedList = new ObservableRangeCollection<FeedModel>();

			FeedModel feedModel = new FeedModel() {
				Title = "Title here",
				Excerpt = "Excerpt here",
				Content = "Content here",
				Author = "Author here",
				Thumbnail = "http://tiny.cc/6k4i6y"
			};

			FeedList.Add(feedModel);
			FeedList.Add(feedModel);
			FeedList.Add(feedModel);
		}

	}
}
