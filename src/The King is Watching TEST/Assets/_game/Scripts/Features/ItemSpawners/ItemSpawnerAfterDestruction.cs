using System;
using ItemDestruction;
using UniRx;
using Zenject;

namespace ItemSpawners
{
	public sealed class ItemSpawnerAfterDestruction : IItemSpawnerByDestruction,
		IInitializable, IDisposable
	{
		IDisposable _disposable;

		[Inject] IFreeItemSpawner _freeItemSpawner;
		[Inject] IDestructionItemService _destructionItemService;

		public void Initialize()
		{
			_disposable = _destructionItemService.ItemDestroyed
				.Subscribe(_ => OnItemDestroyed());
		}

		void OnItemDestroyed()
		{
			_freeItemSpawner.SpawnItems();
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}