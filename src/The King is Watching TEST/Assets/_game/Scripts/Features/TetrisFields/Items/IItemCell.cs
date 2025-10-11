using System;

namespace TetrisFields.Items
{
	public interface IItemCell
	{
		void SetRenderLayer(string layer);
		IObservable<string> RenderLayerChanged { get; }
	}
}