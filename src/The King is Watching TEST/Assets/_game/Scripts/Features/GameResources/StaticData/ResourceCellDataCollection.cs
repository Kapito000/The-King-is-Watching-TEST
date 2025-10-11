using Constant;
using UnityEngine;
using Zenject;

namespace GameResources.StaticData
{
	[CreateAssetMenu(menuName =
		CreateAssetMenu.MenuName.StaticData + nameof(ResourceCellDataCollection))]
	public sealed class ResourceCellDataCollection : ScriptableObject,
		IResourceCellDataCollection
	{
		[SerializeField] ResourcesData[] _data;
		
		public ResourcesData[] Data => _data;

		public bool TryGet(ResourceType resourceType, out ResourcesData outData)
		{
			foreach (var data in _data)
			{
				if (data.Type == resourceType)
				{
					outData = data;
					return true;
				}
			}

			outData = default;
			return false;
		}
	}
}