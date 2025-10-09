using TetrisField;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;
using Zenject;

namespace Infrastructure
{
	public sealed class GameInstaller : MonoInstaller
	{
		[Header("Tetris field")]
		[SerializeField] Transform _fieldParent;
		[SerializeField] Field _tetrisFieldPrefab;
		[SerializeField] FieldCell _tetrisFieldCellPrefab;

		public override void InstallBindings()
		{
			BindDiContainer();
			BindTetrisField();
			BindGameFieldParent();
			BindTetrisFieldFactory();
		}

		void BindGameFieldParent()
		{
			Container
				.BindInstance(_fieldParent)
				.WithId(InjectId.GameFieldParent);
		}

		void BindDiContainer()
		{
			Container
				.Bind<DiContainer>()
				.FromInstance(Container)
				.AsSingle();
		}

		void BindTetrisField()
		{
			Assert.IsNotNull(_tetrisFieldPrefab);
			Assert.IsNotNull(_tetrisFieldCellPrefab);

			Container
				.Bind<Field>()
				.FromComponentInNewPrefab(_tetrisFieldPrefab)
				.AsTransient();

			Container.Bind<FieldCell>()
				.FromComponentInNewPrefab(_tetrisFieldCellPrefab)
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