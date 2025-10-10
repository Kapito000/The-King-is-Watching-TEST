namespace TetrisFields.Items
{
	public interface IItemCell
	{
		IItem Item { get; }

		void SetLayer(string layer);
	}
}