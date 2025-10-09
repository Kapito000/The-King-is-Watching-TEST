using Extensions;
using UnityEngine;
using Zenject;

namespace TetrisFields
{
	public sealed class TetrisFieldFactory : ITetrisFieldFactory
	{
		[Inject] DiContainer _diContainer;
		[Inject] IInstantiator _instantiator;

		public ITetrisField CreateField(Transform parent, Vector2Int size)
		{
			var fieldPrefab = _diContainer.Resolve<TetrisField>();
			var field = _instantiator
				.InstantiatePrefabForComponent<TetrisField>(fieldPrefab,
					parent.position,
					Quaternion.identity, parent);

			field.transform.SetParent(parent);
			field.Init(size);

			foreach (var gridPos in field.ItemsGrid)
			{
				var cellPrefab = _diContainer.Resolve<FieldCell>();
				var pos = field.transform.position + gridPos.AsVector3();
				var cell = _instantiator
					.InstantiatePrefabForComponent<FieldCell>(cellPrefab, pos,
						Quaternion.identity, field.transform);

				cell.FieldPos = gridPos;
				field.SetFieldCell(cell, gridPos);
			}

			return field;
		}
	}
}