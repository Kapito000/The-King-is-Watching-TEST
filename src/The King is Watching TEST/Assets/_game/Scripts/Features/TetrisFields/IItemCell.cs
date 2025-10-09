namespace TetrisFields
{
	public interface IItemCell
	{
		IItem Item { get; }

		void SetLayer(string layer);
		void EnableCollider(bool enable);
	}
}