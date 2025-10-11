using UniRx;

namespace GameResources
{
	public interface IResourceStorage
	{
		ResourceType Type { get; }
		IReactiveProperty<int> Value { get; }
	}
}