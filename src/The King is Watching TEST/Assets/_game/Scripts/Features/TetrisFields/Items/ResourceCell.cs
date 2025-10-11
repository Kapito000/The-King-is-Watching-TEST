using UniRx;

namespace TetrisFields.Items
{
	public class ResourceCell : ItemCell
	{
		public void Init(IItemCell itemCell)
		{
			itemCell.RenderLayerChanged
				.Subscribe(SetRenderLayer)
				.AddTo(this);
		}
	}
}