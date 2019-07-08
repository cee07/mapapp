using System;
using System.Collections.Generic;
using System.IO;
using mapapp.Utils.Interfaces;
using Xamarin.Forms;

namespace mapapp.Views {
	public partial class OfflinePage : ContentPage {
		public OfflinePage () {
			InitializeComponent();
			var source = new HtmlWebViewSource();
			source.Html = @"<html>
<body background=bg1.jpg>
<table align=center>
	<tr align=center>
		<td><br><br><br><br><br><br><font face=verdana color=white size=7>Oops!</font> <br><br> <font face=verdana color=white size=4>Experiencing slow connection?</font><br><br><font face=verdana color=white size=4>	Click next to view our available offline list! </font>
		<br><br><a href=""offline2.html""><img src=next.png></a><br>
		</td>
	</ tr >
</ table >
</ body >
</ html > ";
			source.BaseUrl = DependencyService.Get<IBaseUrl>().Get();
			offlineContent.Source = source;

		}
	}
}
