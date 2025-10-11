using UnityEngine;
using UnityEngine.Assertions;

namespace ProductionCells
{
	public sealed class ProductionCell : MonoBehaviour, IProductionCell
	{
		[SerializeField] SpriteRenderer _spriteRenderer;

		public int ProductionDataId { get; private set; }

		void Awake()
		{
			Assert.IsNotNull(_spriteRenderer);
		}

		public void Init(int productionDataId, Color color)
		{
			ProductionDataId = productionDataId;
			_spriteRenderer.color = color;
		}
	}
}