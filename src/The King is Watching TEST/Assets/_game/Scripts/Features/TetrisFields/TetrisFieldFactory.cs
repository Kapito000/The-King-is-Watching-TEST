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

			CreateCells(field);

			return field;
		}

		void CreateCells(TetrisField field)
		{
			foreach (var gridPos in field.ItemsGrid)
			{
				var cell = CreateCell(field, gridPos);
				InitCell(field, cell, gridPos);
			}
		}

		void InitCell(TetrisField field, FieldCell cell, Vector2Int gridPos)
		{
			cell.FieldPos = gridPos;
			field.SetFieldCell(cell, gridPos);

			InitTetrisFieldReference(field, cell);
		}

		FieldCell CreateCell(TetrisField field, Vector2Int gridPos)
		{
			var pos = field.transform.position + gridPos.AsVector3();
			var cell = _instantiator
				.InstantiatePrefabForComponent<FieldCell>(_cellPrefab, pos,
					Quaternion.identity, field.transform);
			return cell;
		}

		void InitTetrisFieldReference(TetrisField field, FieldCell cell)
		{
			if (cell.TryGetComponent<ITetrisFieldRef>(out var fieldRef) == false)
			{
				Debug.LogError($"The cell has no {nameof(ITetrisFieldRef)} component.");
				return;
			}

			fieldRef.Field = field;
		}
	}
}