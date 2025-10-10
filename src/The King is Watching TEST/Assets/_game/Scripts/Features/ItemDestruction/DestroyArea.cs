using System;
using TetrisFields.Items;
using UniRx;
using UnityEngine;

namespace ItemDestruction
{
	public sealed class DestroyArea : MonoBehaviour, IDestroyArea,
		IDestructionItemService, IDisposable
	{
		Subject<Unit> _itemDestroyed = new();
		public IObservable<Unit> ItemDestroyed => _itemDestroyed;

		public void DestroyItem(IItem item)
		{
			item.Destroy();
			_itemDestroyed.OnNext(Unit.Default);
		}

		public void Dispose()
		{
			_itemDestroyed.OnCompleted();
		}
	}
}