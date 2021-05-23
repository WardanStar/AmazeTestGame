using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
	public class AddElementsToFieldManager
	{
		public GameObject StartPoint { get; private set; }
		
		private readonly CraftEditor _craftEditor;
		private readonly Arm _arm;
		private readonly string _pathToIllusionCube;
		private readonly string _pathToBarrier;
		private readonly string _pathToStartPoint;
		private GameObject _illusionCube;
		private Vector3 _startScaleIllusionCube;

		public AddElementsToFieldManager(CraftEditor craftEditor, Arm arm, string pathToIllusionCube, string pathToBarrier, string pathToStartPoint)
		{
			_craftEditor = craftEditor;
			_arm = arm;
			_pathToIllusionCube = pathToIllusionCube;
			_pathToBarrier = pathToBarrier;
			_pathToStartPoint = pathToStartPoint;
		}
		
		public void CallIllusionCube(Vector3 position)
		{
			if (ReferenceEquals(_illusionCube, null))
			{
				_illusionCube = _arm.GetObjectManager.GetObject<Transform>(_pathToIllusionCube, position, 
					Quaternion.identity).gameObject;
				_startScaleIllusionCube = _illusionCube.transform.localScale;
				
				_illusionCube.transform
                .DOScale(new Vector3(0.5f, 0.5f, 0.5f), 1f)
                .SetEase(Ease.InOutSine)
                .SetLoops(10000, LoopType.Yoyo)
                .SetLink(_illusionCube, LinkBehaviour.KillOnDisable);
			}
			
			_illusionCube.transform.position = position;
		}

		public void CallBarrier()
		{
			Vector3 illusionCubePosition = _illusionCube.transform.position;
			FindOutOfThePositionOnTheField(illusionCubePosition, out int height, out int width);
			var cubeInfo = _craftEditor.ListCubes[height][width];
			cubeInfo.IsBarrier = true;
			cubeInfo.Cube?.SetActive(false);
			cubeInfo.Cube = _arm.GetObjectManager
				.GetObject<Transform>(_pathToBarrier, illusionCubePosition, Quaternion.identity).gameObject;
		}

		public void CallStartPoint()
		{
			Vector3 illusionCubePosition = _illusionCube.transform.position;
			FindOutOfThePositionOnTheField(illusionCubePosition, out int height, out int width);
			var cubeInfo = _craftEditor.ListCubes[height][width];
			StartPoint?.SetActive(false);
			StartPoint = _arm.GetObjectManager.GetObject<Transform>(_pathToStartPoint, cubeInfo.Cube.transform.position + Vector3.up,
				Quaternion.identity).gameObject;
		}

		public void FindOutOfThePositionOnTheField(Vector3 positionObject, out int height, out int width)
		{
			height = (int) (positionObject.z + Mathf.Abs(_craftEditor.AngularPoint.z));
			width = (int) (positionObject.x + Mathf.Abs(_craftEditor.AngularPoint.x));
		}

		public void ClearField()
		{
			StartPoint.SetActive(false);
			_illusionCube.SetActive(false);
			StartPoint = null;
			_illusionCube = null;
		}
	}
}