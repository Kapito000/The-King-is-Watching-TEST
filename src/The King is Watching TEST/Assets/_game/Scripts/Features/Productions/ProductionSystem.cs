using System.Collections.Generic;
using System.Linq;
using GameResources;
using ProductionCells;
using ProductionCells.StaticData;
using TetrisFields;
using UniRx;
using UnityEngine;
using Zenject;

namespace Productions
{
	public sealed class ProductionSystem : MonoBehaviour, IProductionSystem
	{
		[Inject] IPlayerResources _playerResources;
		[Inject] IProductionCellDataCollection _productionDatas;

		ITetrisField _field;
		List<IProductionTimer> _productions = new();

		void FixedUpdate()
		{
			foreach (var production in _productions)
			{
				if (Time.time <= production.TimeMoment)
					continue;

				production.TimeMoment = Time.time + production.TimeSpan;
				foreach (var resource in production.ResourceProductions)
				{
					if (_playerResources.TryGetResourceStorage(resource.Key,
						    out var storage) == false)
						continue;

					storage.Value.Value += (int)resource.Value;
				}
			}
		}

		public void Init(ITetrisField field)
		{
			_field = field;
			field.FieldChanged
				.Subscribe(_ => OnFieldChanged())
				.AddTo(this);

			for (var i = 0; i < _productionDatas.ProductionData.Length; i++)
			{
				var productionData = _productionDatas.ProductionData[i];
				if (productionData.ProductionModifier == 0)
					continue;

				var item = new ProductionTimer(i, new[]
				{
					ResourceType.Iron,
					ResourceType.Wood,
					ResourceType.Wheat
				});

				item.TimeMoment = Time.time + productionData.ProductionTimer;
				item.TimeSpan = productionData.ProductionTimer;

				_productions.Add(item);
			}
		}

		void OnFieldChanged()
		{
			ResetProductionValues();

			var productionItemsCells = _field.ProductionItemCellGrid
				.WithValues()
				.Where(x => x.value != null);

			foreach (var itemPair in productionItemsCells)
			{
				IProductionCell productionCell =
					_field.ProductionCellsGrid[itemPair.cell.x, itemPair.cell.y];

				foreach (var productionTimer in _productions)
				{
					var modifier = Modifier(productionCell.ProductionDataId);
					productionTimer
							.ResourceProductions[itemPair.value.Type] +=
						1 * modifier;
				}
			}
		}

		float Modifier(int dataId) =>
			_productionDatas.ProductionData[dataId].ProductionModifier;

		void ResetProductionValues()
		{
			foreach (var production in _productions)
			{
				var keys = production.ResourceProductions.Keys.ToArray();
				foreach (var key in keys)
					production.ResourceProductions[key] = 0;
			}
		}
	}
}