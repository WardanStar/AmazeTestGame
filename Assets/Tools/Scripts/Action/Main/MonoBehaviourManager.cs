using System.Collections.Generic;
using InitializeSystem;
using UnityEngine;

namespace ToolsSystem.Action.Main
{
	public class MonoBehaviourManager : MonoBehaviour
	{
		private List<ITickableUpdate> _tickableUpdates = new List<ITickableUpdate>();
		private List<ITickableLateUpdate> _tickableLateUpdates = new List<ITickableLateUpdate>();
		private List<ITickableFixedUpdate> _tickableFixedUpdates = new List<ITickableFixedUpdate>();

        public GameObject InstantiateGameObject(GameObject go)
        {
        	return Instantiate(go);
        }
        public TComponent InstantiateGameObjectByComponent<TComponent>(TComponent component) where TComponent : Component
        {
        	return Instantiate(component);
        }
        
        public void AddTickableUpdate(ITickableUpdate action)
        {
        	_tickableUpdates.Add(action);
        }
        public void AddTickableLateUpdate(ITickableLateUpdate action)
        {
        	_tickableLateUpdates.Add(action);
        }
        public void AddTickableFixedUpdate(ITickableFixedUpdate action)
        {
        	_tickableFixedUpdates.Add(action);
        }
		
		private void Update()
		{
			foreach (ITickableUpdate tickableUpdate in _tickableUpdates)
			{
				if (tickableUpdate.disabled) continue;
				
				tickableUpdate.TickUpdate();
			}
		}

		private void LateUpdate()
		{
			foreach (ITickableLateUpdate tickableLateUpdate in _tickableLateUpdates)
			{
				if (tickableLateUpdate.disabled) continue;
				
				tickableLateUpdate.TickLateUpdate();
			}
		}
		
		private void FixedUpdate()
		{
			foreach (ITickableFixedUpdate tickableFixedUpdate in _tickableFixedUpdates)
			{
				if (tickableFixedUpdate.disabled) continue;
				
				tickableFixedUpdate.TickFixedUpdate();
			}
		}
	}
}