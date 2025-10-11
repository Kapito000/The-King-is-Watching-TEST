using GameResources;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace UI.ResourcesView
{
	public sealed class ResourcesStorageViewPanel : MonoBehaviour,
		IResourcesStorageViewPanel
	{
		[SerializeField] ResourceType _resourceType;

		[SerializeField] TMP_Text _valueText;

		[Inject] IPlayerResources _playerResources;

		void Awake()
		{
			Assert.IsNotNull(_valueText);
		}

		public void Init()
		{
			if (_playerResources
				    .TryGetResourceStorage(_resourceType, out var storage) == false)
			{
				Debug.LogError($"Can't find resource storage: \"{_resourceType}\"");
				return;
			}

			storage.Value
				.Subscribe(OnResourceValueChanged)
				.AddTo(this);
		}

		void OnResourceValueChanged(int newResourceValue)
		{
			_valueText.text = newResourceValue.ToString();
		}
	}
}