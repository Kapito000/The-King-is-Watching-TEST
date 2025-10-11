namespace GameResources
{
	public interface IResourceStorage
	{
		ResourceType Type { get; }
		int Value { get; set; }
	}
}