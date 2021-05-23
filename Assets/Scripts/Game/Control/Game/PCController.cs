using System;
using AbstractSystem;
using InitializeSystem;
using ToolsSystem.Action.Main;
using UnityEngine;

namespace DefaultNamespace
{
	public class PCController : IControl, ITickableUpdate
	{
		public event Action<float> DoHorizontalChange;
		public event Action<float> DoVerticalChange;
		public bool disabled { get; set; }

		public PCController(MonoBehaviourManager monoBehaviourManager) =>
			monoBehaviourManager.AddTickableUpdate(this);
		
		public void TickUpdate()
		{
			var horizontal = Input.GetAxis("Horizontal");
			var vertical = Input.GetAxis("Vertical");
			
			if (!Mathf.Approximately(horizontal, 0f))
				DoHorizontalChange?.Invoke(horizontal);
			if (!Mathf.Approximately(vertical, 0f))
				DoVerticalChange?.Invoke(vertical);
		}
	}
}