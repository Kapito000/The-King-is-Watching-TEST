using Input;
using TetrisFields;
using TetrisFields.Items;
using TetrisFields.Items.StaticData;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Infrastructure
{
	public sealed class GameInstaller : MonoInstaller
	{
		[Header("Free items field")]
		[SerializeField] Transform _freeItemsFieldParent;
		[Header("Game field")]
		[SerializeField] Transform _gameFieldParent;
		[SerializeField] FieldCell _tetrisFieldCellPrefab;
		[SerializeField] TetrisField tetrisTetrisFieldPrefab;
		[Header("Items")]
		[SerializeField] Item _itemPrefab;
		[SerializeField] ItemCell _itemCellPrefab;
		[SerializeField] Transform _itemsParent;
		[Header("Static data")]
		[SerializeField] ItemDataCollection _itemDataCollection;

		public override void InstallBindings()
		{
			BindItemPrefab();
			BindTetrisField();
			BindItemFactory();
			BindItemsParent();
			BindInputService();
			BindGameFieldsParents();
			BindTetrisFieldFactory();
			BindItemDataCollection();
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
				.AsTransient();
			Container
				.BindInstance(_itemCellPrefab)
				.AsTransient();
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
			Assert.IsNotNull(tetrisTetrisFieldPrefab);

			Container
				.Bind<TetrisField>()
				.FromInstance(tetrisTetrisFieldPrefab)
				.AsTransient();

			Container.Bind<FieldCell>()
				.FromInstance(_tetrisFieldCellPrefab)
				.AsTransient();
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