using System.Collections.Generic;
using System.Linq;
using GameResources;
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
				if (production.TimeMoment <= Time.time)
					continue;

				production.TimeMoment = production.TimeSpan;
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

			var resourceCells = _field
				.AllItems()
				.Select(item => item.ResourceCell);

			foreach (var resourceCell in resourceCells)
			{
				var productionCell = _field.ProductionCells(resourceCell.Pos);

				foreach (var productionTimer in _productions)
				{
					if (productionTimer.ProductionDataId !=
					    productionCell.ProductionDataId)
					{
						continue;
					}

					var modifier = Modifier(productionCell.ProductionDataId);
					productionTimer.ResourceProductions[resourceCell.Type] +=
						1 * modifier;
				}
			}
		}

		float Modifier(int dataId) =>
			_productionDatas.ProductionData[dataId].ProductionModifier;

		void ResetProductionValues()
		{
			foreach (var production in _productions)
			foreach (var pair in production.ResourceProductions)
				production.ResourceProductions[pair.Key] = 0;
		}
	}
}