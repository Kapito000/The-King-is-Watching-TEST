using UnityEngine;

namespace GameResources
{
	public class ResourceStorage : MonoBehaviour, IResourceStorage
	{
		[field: SerializeField] public ResourceType Type { get; private set; }
		[field: SerializeField] public int Value { get; set; }
	}
}