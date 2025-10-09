using Extensions;
using UnityEngine;
using Zenject;

namespace TetrisField
{
	public sealed class TetrisFieldFactory : ITetrisFieldFactory
	{
		[Inject]
		DiContainer _diContainer;

		public ITetrisField CreateField(Transform parent, Vector2Int size)
		{
			var field = _diContainer.Resolve<Field>();
			field.transform.SetParent(parent);
			field.Init(size);
			
			foreach (var gridPos in field.ItemsGrid)
			{
				var fieldCell = _diContainer.Resolve<FieldCell>();
				fieldCell.transform.SetParent(field.transform);
				fieldCell.transform.localPosition = gridPos.AsVector3();
				fieldCell.FieldPos = gridPos;
				field.SetFieldCell(fieldCell, gridPos);
			}
			
			return field;
		}
	}
}