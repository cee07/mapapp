using System;
namespace merchantapp.Utils.Interfaces {
	public interface IAppVersionAndBuild {
		string GetVersionNumber ();
		string GetBuildNumber ();
	}
}
