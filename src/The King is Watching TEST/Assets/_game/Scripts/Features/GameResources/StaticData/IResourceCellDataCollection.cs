namespace GameResources.StaticData
{
	public interface IResourceCellDataCollection
	{
		bool TryGet(ResourceType resourceType, out ResourcesData outData);
		ResourcesData[] Data { get; }
	}
}