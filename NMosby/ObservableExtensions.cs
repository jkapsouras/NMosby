using System;
using System.Reactive.Disposables;

namespace NMosby
{
	public static class ObservableExtensions
	{
		public static TDisposable DisposeWith<TDisposable>(this TDisposable observable, CompositeDisposable disposables) where TDisposable : class, IDisposable
		{
			if (observable != null)
				disposables.Add(observable);

			return observable;
		}
	}
}
