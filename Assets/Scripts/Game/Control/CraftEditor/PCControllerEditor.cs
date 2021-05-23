using System;
using AbstractSystem;
using InitializeSystem;
using ToolsSystem.Action.Main;
using UnityEngine;

namespace DefaultNamespace
{
	public class PCControllerEditor : IControlEditor, ITickableUpdate
	{
		public event Action DoClick;
		public Vector3 CursorPosition => Input.mousePosition;
		public bool disabled { get; set; }

		public PCControllerEditor(MonoBehaviourManager monoBehaviourManager) =>
        			monoBehaviourManager.AddTickableUpdate(this);
		
		public void TickUpdate()
		{
			if (Input.GetMouseButton(0))
				DoClick?.Invoke();
		}
	}
}