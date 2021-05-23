using System;
using UnityEngine;

namespace AbstractSystem
{
	public interface IControl
	{
		public event Action<float> DoHorizontalChange;
		public event Action<float> DoVerticalChange;
	}

	public interface IControlEditor
	{
		public event Action DoClick;
		public Vector3 CursorPosition { get; }
	}
}