using System.Collections.Generic;
using UnityEngine;

namespace ToolsSystem.Action.Additional
{
	public class RestorationOfTheOriginalState : MonoBehaviour
	{
		private readonly List<Transform> _listTransform = new List<Transform>();
		private readonly List<Vector3> _listPosition = new List<Vector3>();
		private readonly List<Vector3> _listEulerAngle = new List<Vector3>();
		
		private void Start()
		{
			foreach (var t in GetComponentsInChildren<Transform>())
			{
				_listTransform.Add(t);
				_listPosition.Add(t.position);
				_listEulerAngle.Add(t.eulerAngles);
			}
		}

		private void OnDisable()
		{
			for (int i = 0; i < _listTransform.Count; i++)
			{
				Transform transforms = _listTransform[i];
				transforms.position = _listPosition[i];
				transforms.eulerAngles = _listEulerAngle[i];
			}
		}
	}
}