using System;
using TetrisFields.Items;
using UniRx;

namespace ItemDestruction
{
	public interface IDestructionItemService
	{
		IObservable<Unit> ItemDestroyed { get; }
		void DestroyItem(IItem item);
	}
}