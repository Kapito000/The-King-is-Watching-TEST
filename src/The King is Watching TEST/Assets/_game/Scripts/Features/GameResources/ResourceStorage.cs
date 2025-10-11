using System;
using UniRx;
using UnityEngine;

namespace GameResources
{
	public class ResourceStorage : MonoBehaviour, IResourceStorage
	{
		[field: SerializeField] public ResourceType Type { get; private set; }
		[field: SerializeField] IntReactiveProperty _value { get; set; } = new();
		
		public IObservable<int> Value => _value;
	}
}