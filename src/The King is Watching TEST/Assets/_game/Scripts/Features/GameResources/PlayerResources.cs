using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GameResources
{
	public sealed class PlayerResources : MonoBehaviour, IPlayerResources,
		IInitializable
	{
		[SerializeField] List<ResourceStorage> _resources = new();

		Dictionary<ResourceType, ResourceStorage> _resourcesDictionary = new();

		public void Initialize()
		{
			foreach (var storage in _resources)
			{
				AddToDictionary(storage);
			}
		}

		public bool TryGetResource(
			ResourceType resourceType,
			out ResourceStorage resource)
		{
			return _resourcesDictionary.TryGetValue(resourceType, out resource);
		}

		void AddToDictionary(ResourceStorage storage)
		{
			if (_resourcesDictionary.TryAdd(storage.Type, storage))
				return;

			Debug.LogError(
				$"Failed to add resource to dictionary: \"{storage.Type}\".");
		}
	}
}