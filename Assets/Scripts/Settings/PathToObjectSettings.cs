using UnityEngine;

namespace Settings
{
	[CreateAssetMenu()]
	public class PathToObjectSettings : ScriptableObject
	{
		public string PathToBarrier => _pathToBarrier;
		public string PathToNotVisitedCube => _pathToNotVisitedCube;
		public string PathToVisitedCube => _pathToVisitedCube;
		public string PathToIllusionCube => _pathToIllusionCube;
		public string PathToSphere => _pathToSphere;
		public string PathToStartPoint => _pathToStartPoint;

		[SerializeField] private string _pathToBarrier;
		[SerializeField] private string _pathToNotVisitedCube;
		[SerializeField] private string _pathToVisitedCube;
		[SerializeField] private string _pathToIllusionCube;
		[SerializeField] private string _pathToSphere;
		[SerializeField] private string _pathToStartPoint;
	}
}