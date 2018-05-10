using System;
using System.Reactive.Concurrency;
using System.Threading;

namespace NMosby
{
	public interface IBaseView
	{
		SynchronizationContext MainThread { get; set; }
		IScheduler BackgroundScheduler { get; set; }
	}
}
