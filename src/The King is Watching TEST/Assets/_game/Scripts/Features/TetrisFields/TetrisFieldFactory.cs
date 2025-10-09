using Extensions;
using UnityEngine;
using Zenject;

namespace TetrisFields
{
	public sealed class TetrisFieldFactory : ITetrisFieldFactory
	{
		[Inject] FieldCell _cellPrefab;
		[Inject] TetrisField _fieldPrefab;
		[Inject] IInstantiator _instantiator;

		public ITetrisField CreateField(Transform parent, Vector2Int size)
		{
			var field = _instantiator
				.InstantiatePrefabForComponent<TetrisField>(_fieldPrefab,
					parent.position,
					Quaternion.identity, parent);

			field.transform.SetParent(parent);
			field.Init(size);

			foreach (var gridPos in field.ItemsGrid)
			{
				var pos = field.transform.position + gridPos.AsVector3();
				var cell = _instantiator
					.InstantiatePrefabForComponent<FieldCell>(_cellPrefab, pos,
						Quaternion.identity, field.transform);

				cell.FieldPos = gridPos;
				field.SetFieldCell(cell, gridPos);
			}

			return field;
		}
	}
}