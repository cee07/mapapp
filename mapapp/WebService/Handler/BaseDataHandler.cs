using System;
using System.Threading.Tasks;

public abstract class BaseDataHandler {
	public Func<Task> OnRequestSuccessful;
	public Action OnTimeOut;
	public Func<Task> OnError;

	protected abstract Task OnAPICallSuccessful();
	protected abstract void OnTimedOut();
	protected abstract Task OnErrorOccured();
}