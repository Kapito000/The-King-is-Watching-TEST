namespace GameResources
{
	public interface IPlayerResources
	{
		bool TryGetResource(
			ResourceType resourceType,
			out ResourceStorage resource);
	}
}