using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Studiotaiha.Toolkit.Collections
{
	public enum ProxyDispatchMode
	{
		None,
		OneWay,
		OneWayToSource,
		TwoWay,
	}
	public class ProxyObservableCollection<TSourceItem, TItem> : ObservableCollection<TItem>, IDisposable
	{
		public IDispatcher Dispatcher { get; }
		ObservableCollection<TSourceItem> MasterCollection { get; }
		Func<TSourceItem, TItem> ProxyItemCreator { get; }
		Func<TItem, TSourceItem> ReverseItemCreator { get; }
		public ProxyDispatchMode DispatchMode { get; set; }

		bool isProxyChanging_ = false;
		bool isMasterChanging_ = false;

		public ProxyObservableCollection(
			ObservableCollection<TSourceItem> masterCollection,
			Func<TSourceItem, TItem> proxyItemCreator,
			Func<TItem, TSourceItem> reverseItemCreator = null,
			IDispatcher dispatcher = null,
			ProxyDispatchMode dispatchMode = ProxyDispatchMode.OneWay)
			: base(masterCollection.Select(proxyItemCreator))
		{
			MasterCollection = masterCollection ?? throw new ArgumentNullException(nameof(masterCollection));
			ProxyItemCreator = proxyItemCreator ?? throw new ArgumentNullException(nameof(proxyItemCreator));

			ReverseItemCreator = reverseItemCreator;
			ProxyItemCreator = proxyItemCreator;
			Dispatcher = dispatcher;
			DispatchMode = dispatchMode;

			MasterCollection.CollectionChanged += MasterCollection_CollectionChanged;
			CollectionChanged += ProxyObservableCollection_CollectionChanged;
		}

		private void ProxyObservableCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (isProxyChanging_) {
				return;
			}

			isMasterChanging_ = true;

			try {
				var action = (Action)(() => {
					if (e.Action == NotifyCollectionChangedAction.Reset) {
						MasterCollection.Clear();
						return;
					}

					if (e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Replace || e.Action == NotifyCollectionChangedAction.Move) {
						for (var i = 0; i < e.OldItems.Count; i++) {
							var index = e.OldStartingIndex + i;
							MasterCollection.RemoveAt(index);
						}
					}

					if (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Replace || e.Action == NotifyCollectionChangedAction.Move) {
						var index = e.NewStartingIndex;
						foreach (var item in e.NewItems) {
							var source = (TItem)item;
							if (ReverseItemCreator == null) {
								throw new InvalidOperationException("ReverseItemCreator is not set.");
							}

							var newItem = ReverseItemCreator.Invoke(source);
							if (index >= MasterCollection.Count) {
								MasterCollection.Add(newItem);
							}
							else {
								MasterCollection.Insert(index, newItem);
							}

							index++;
						}
					}
				});

				if ((DispatchMode == ProxyDispatchMode.OneWayToSource || DispatchMode == ProxyDispatchMode.TwoWay) && Dispatcher != null) {
					Dispatcher.Dispatch(action);
				}
				else {
					action.Invoke();

				}
			}
			finally {
				isMasterChanging_ = false;
			}
		}

		private void MasterCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (isMasterChanging_) {
				return;
			}

			isProxyChanging_ = true;
			try {

				var action = (Action)(() => {
					if (e.Action == NotifyCollectionChangedAction.Reset) {
						Clear();
						return;
					}

					if (e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Replace || e.Action == NotifyCollectionChangedAction.Move) {
						for (var i = 0; i < e.OldItems.Count; i++) {
							var index = e.OldStartingIndex + i;
							RemoveAt(index);
						}
					}

					if (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Replace || e.Action == NotifyCollectionChangedAction.Move) {
						var index = e.NewStartingIndex;
						foreach (var item in e.NewItems) {
							var source = (TSourceItem)item;
							var newItem = ProxyItemCreator.Invoke(source);
							if (index >= Count) {
								Add(newItem);
							}
							else {
								Insert(index, newItem);
							}

							index++;
						}
					}
				});

				if ((DispatchMode == ProxyDispatchMode.OneWay || DispatchMode == ProxyDispatchMode.TwoWay) && Dispatcher != null) {
					Dispatcher.Dispatch(action);
				}
				else {
					action.Invoke();

				}
			}
			finally {
				isProxyChanging_ = false;
			}
		}

		#region IDisposable

		bool isDisposed_ = false;
		virtual protected void Dispose(bool disposing)
		{
			if (isDisposed_) { return; }
			if (disposing) {
				MasterCollection.CollectionChanged -= MasterCollection_CollectionChanged;
				CollectionChanged -= ProxyObservableCollection_CollectionChanged;
			}
			isDisposed_ = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion
	}
}
