namespace GameResources
{
	public interface IPlayerResources
	{
		bool TryGetResourceStorage(
			ResourceType resourceType,
			out ResourceStorage resourceStorage);
	}
}