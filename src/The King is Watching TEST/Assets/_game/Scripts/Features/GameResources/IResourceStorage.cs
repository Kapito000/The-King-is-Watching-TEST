using System;

namespace GameResources
{
	public interface IResourceStorage
	{
		ResourceType Type { get; }
		IObservable<int> Value { get; }
	}
}