using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Subjects;

namespace NMosby
{
	public abstract class BasePresenter<V, VS>
		where V : class, IBaseView
		where VS : class
	{
		public CompositeDisposable Disposables { get; set; }
		private WeakReference<V> _viewRef;
		ReplaySubject<VS> _viewState = new ReplaySubject<VS>(1);

		protected V View
		{
			get
			{
				if (_viewRef != null && _viewRef.TryGetTarget(out V target))
					return target;
				else
					return null;
			}
		}

		public BasePresenter()
		{
			Disposables = new CompositeDisposable();
		}

		public void AttachView(V view)
		{
			_viewRef = new WeakReference<V>(view);
			BindIntents();
		}

		public bool IsViewAttached()
		{
			return _viewRef != null && _viewRef.TryGetTarget(out V target);
		}

		public void DetachView()
		{
			_viewRef = null;
			Disposables.Clear();
			Disposables.Dispose();
		}

		public abstract void BindIntents();

		public void SubscribeViewState(IObservable<VS> state, Func<VS, Unit> render)
		{
			state.Subscribe(st => _viewState.OnNext(st)).DisposeWith(Disposables);
			_viewState.Subscribe(st => render(st)).DisposeWith(Disposables);
		}
	}
}
