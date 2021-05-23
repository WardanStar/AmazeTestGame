using UnityEngine;

namespace Settings
{
	[CreateAssetMenu()]
	public class GameSettings : ScriptableObject
	{
		public float SpeedSphere => _speedSphere;

		[SerializeField] private float _speedSphere;
	}
}