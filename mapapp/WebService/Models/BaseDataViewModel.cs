using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class BaseDataViewModel : INotifyPropertyChanged {

	private const string IS_BUSY_PROPERTY = "IsBusy";

	public event PropertyChangedEventHandler PropertyChanged;

	private bool isBusy = false;

	public bool IsBusy {
		get {
			return isBusy;
		} set {
			SetProperty (ref isBusy,value);
		}
	}

	protected bool SetProperty<T> (ref T backingStore,T value,
	                               [CallerMemberName]string propertyName = "",
	                               Action onChanged = null) {
		if (EqualityComparer<T>.Default.Equals (backingStore,value))
			return false;

		backingStore = value;
		onChanged?.Invoke ();
		OnPropertyChanged (propertyName);

		return true;
	}

	protected void OnPropertyChanged(string propertyName) {
		var handler = PropertyChanged;
		if (handler != null)
			handler (this, new PropertyChangedEventArgs (propertyName));
	}
}

