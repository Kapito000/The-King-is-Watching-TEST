using TetrisField;
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
		[SerializeField] TetrisField.TetrisField tetrisTetrisFieldPrefab;
		[SerializeField] Transform _gameFieldParent;
		[SerializeField] FieldCell _tetrisFieldCellPrefab;

		public override void InstallBindings()
		{
			BindTetrisField();
			BindGameFieldsParents();
			BindTetrisFieldFactory();
		}

		void BindGameFieldsParents()
		{
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

			Container
				.Bind<TetrisField.TetrisField>()
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