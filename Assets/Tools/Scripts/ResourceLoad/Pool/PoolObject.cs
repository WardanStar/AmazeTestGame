using InitializeSystem;
using UnityEngine;

namespace ResourcesLoadSystem
{
	public class PoolObject : MonoBehaviour, IInitialize<PoolManager, Component, string>
	{
		private PoolManager _poolManager;
		private Component _component;
		private string _resourcePath;

		public void Assign(PoolManager poolManager, Component component, string resourcePath)
		{
			_poolManager = poolManager;
			_resourcePath = resourcePath;
			_component = component;
		}

		private void OnDisable()
		{
			_poolManager.ReturnToPool(_component, _resourcePath);
		}
	}
}