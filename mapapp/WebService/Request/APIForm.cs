using System;
using System.Collections.Generic;
using System.Net.Http;

public class APIForm {

	private List<KeyValuePair<string, string>> content;

	public APIForm() {
		content = new List<KeyValuePair<string, string>> ();
	}

	public void AddField(string node, string data) {
		content.Add (new KeyValuePair<string, string> (node, data));
	}

	public KeyValuePair<string,string>[] FormContent {
		get { return content.ToArray (); }
	}

}