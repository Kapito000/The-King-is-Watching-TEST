using GameResources;
using GameResources.StaticData;
using Input;
using ItemDestruction;
using ItemSpawners;
using TetrisFields;
using TetrisFields.Items;
using TetrisFields.Items.StaticData;
using UI.ResourcesView;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Infrastructure
{
	public sealed class GameInstaller : MonoInstaller
	{
		[SerializeField] Hand _hand;
		[SerializeField] Camera _mainCamera;
		[SerializeField] DestroyArea _destroyArea;
		[SerializeField] BootItemSpawner _bootItemSpawner;
		[SerializeField] PlayerResources _playerResources;
		[Header("Free items field")]
		[SerializeField] Transform _freeItemsFieldParent;
		[Header("Game field")]
		[SerializeField] Transform _gameFieldParent;
		[SerializeField] FieldCell _tetrisFieldCellPrefab;
		[SerializeField] TetrisField _tetrisTetrisFieldPrefab;
		[Header("Items")]
		[SerializeField] Item _itemPrefab;
		[SerializeField] ItemCell _itemCellPrefab;
		[SerializeField] Transform _itemsParent;
		[Header("UI")]
		[SerializeField] ResourcesStorageViewPanel[] _resourcesStorageViewPanels;
		[Header("Static data")]
		[SerializeField] ItemDataCollection _itemDataCollection;
		[SerializeField] ResourceCellDataCollection _resourceCellDataCollection;

		public override void InstallBindings()
		{
			BindHand();
			BindGameData();
			BindMainCamera();
			BindItemPrefab();
			BindTetrisField();
			BindItemFactory();
			BindItemsParent();
			BindInputService();
			BindPlayerResources();
			BindFreeItemSpawner();
			BindBootItemSpawner();
			BindItemSpawnService();
			BindGameFieldsParents();
			BindTetrisFieldFactory();
			BindItemDataCollection();
			BindDestructionItemService();
			BindResourcesStorageViewPanel();
			BindResourceCellDataCollection();
			BindItemSpawnerAfterDestruction();
		}

		void BindResourcesStorageViewPanel()
		{
			foreach (var panel in _resourcesStorageViewPanels)
			{
				Container
					.BindInterfacesTo<ResourcesStorageViewPanel>()
					.FromInstance(panel)
					.AsCached();
			}
		}

		void BindResourceCellDataCollection()
		{
			Assert.IsNotNull(_resourceCellDataCollection);

			Container
				.BindInterfacesTo<ResourceCellDataCollection>()
				.FromInstance(_resourceCellDataCollection)
				.AsSingle();
		}

		void BindPlayerResources()
		{
			Assert.IsNotNull(_playerResources);

			Container
				.BindInterfacesTo<PlayerResources>()
				.FromInstance(_playerResources);
		}

		void BindGameData()
		{
			Container
				.BindInterfacesTo<GameData>()
				.AsSingle();
		}

		void BindItemSpawnerAfterDestruction()
		{
			Container
				.BindInterfacesAndSelfTo<ItemSpawnerAfterDestruction>()
				.AsSingle();
		}

		void BindFreeItemSpawner()
		{
			Container
				.BindInterfacesAndSelfTo<FreeItemSpawner>()
				.AsSingle();
		}

		void BindItemSpawnService()
		{
			Container
				.Bind<IItemSpawnService>()
				.To<ItemSpawnService>()
				.AsSingle();
		}

		void BindBootItemSpawner()
		{
			Assert.IsNotNull(_bootItemSpawner);

			Container
				.BindInterfacesAndSelfTo<BootItemSpawner>()
				.FromInstance(_bootItemSpawner)
				.AsSingle();
		}

		void BindDestructionItemService()
		{
			Assert.IsNotNull(_destroyArea);

			Container
				.BindInterfacesAndSelfTo<DestroyArea>()
				.FromInstance(_destroyArea)
				.AsSingle();
		}

		void BindMainCamera()
		{
			Assert.IsNotNull(_mainCamera);

			Container
				.BindInstance(_mainCamera)
				.WithId(InjectId.MainCamera)
				.AsSingle();
		}

		void BindHand()
		{
			Assert.IsNotNull(_hand);

			Container
				.BindInterfacesAndSelfTo<Hand>()
				.FromInstance(_hand)
				.AsSingle()
				.OnInstantiated<Hand>((_, x) => x.Init())
				.NonLazy();
		}

		void BindInputService()
		{
			Container
				.Bind<InputActions>()
				.AsSingle();

			Container
				.BindInterfacesAndSelfTo<InputService>()
				.AsSingle()
				.OnInstantiated<InputService>((_, x) => x.Init());
		}

		void BindItemsParent()
		{
			Assert.IsNotNull(_itemsParent);

			Container
				.BindInstance(_itemsParent)
				.WithId(InjectId.ItemsParent);
		}

		void BindItemDataCollection()
		{
			Assert.IsNotNull(_itemDataCollection);

			Container
				.Bind<IItemDataCollection>()
				.FromInstance(_itemDataCollection)
				.AsSingle();
		}

		void BindItemFactory()
		{
			Container
				.Bind<IItemFactory>()
				.To<ItemFactory>()
				.AsSingle();
		}

		void BindItemPrefab()
		{
			Assert.IsNotNull(_itemPrefab);
			Assert.IsNotNull(_itemCellPrefab);

			Container
				.BindInstance(_itemPrefab)
				.AsSingle();
			Container
				.BindInstance(_itemCellPrefab)
				.AsSingle();
		}

		void BindGameFieldsParents()
		{
			Assert.IsNotNull(_gameFieldParent);
			Assert.IsNotNull(_freeItemsFieldParent);

			Container
				.BindInstance(_gameFieldParent)
				.WithId(InjectId.GameFieldParent);
			Container
				.BindInstance(_freeItemsFieldParent)
				.WithId(InjectId.FreeItemsFieldParent);
		}

		void BindTetrisField()
		{
			Assert.IsNotNull(_tetrisFieldCellPrefab);
			Assert.IsNotNull(_tetrisTetrisFieldPrefab);

			Container
				.Bind<TetrisField>()
				.FromInstance(_tetrisTetrisFieldPrefab)
				.AsSingle();

			Container.Bind<FieldCell>()
				.FromInstance(_tetrisFieldCellPrefab)
				.AsSingle();
		}

		void BindTetrisFieldFactory()
		{
			Container
				.Bind<ITetrisFieldFactory>()
				.To<TetrisFieldFactory>()
				.AsSingle();
		}
	}
}