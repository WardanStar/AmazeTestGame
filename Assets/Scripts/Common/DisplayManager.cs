using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
	public class DisplayManager
	{
		private readonly Arm _arm;
		private readonly string _pathToNotVisitedCube;
		private readonly string _pathToVisitedCube;
		private readonly string _pathToBarrier;

		public DisplayManager(Arm arm, string pathToNotVisitedCube, string pathToVisitedCube, string pathToBarrier)
		{
			_arm = arm;
			_pathToNotVisitedCube = pathToNotVisitedCube;
			_pathToVisitedCube = pathToVisitedCube;
			_pathToBarrier = pathToBarrier;
		}

		public void ContentDisplay(List<List<CubeInfo>> list, out Vector3 angularPoint)
		{
			int numberOfHeightShifts = (list.Count - 1) / 2;
			int numberOfWidthShifts = (list[0].Count - 1) / 2;
			
			angularPoint = new Vector3(0f - numberOfWidthShifts, 0f, 0f - numberOfHeightShifts);
			
			var scaleCube = _arm.GetObjectManager.GetInfoObject<GameObject>(_pathToBarrier).transform.localScale;
			
			var point = angularPoint;
			
			foreach (var cubeInfos in list)
			{
				FillingListCube(point, scaleCube, cubeInfos);
				point += new Vector3(0f, 0f, scaleCube.z);
			}
		}

		public void ClearField(List<List<CubeInfo>> list)
		{
			foreach (var cubeInfos in list)
			{
				foreach (var cubeInfo in cubeInfos)
				{
					if (cubeInfo.Cube == null) continue;
					
					cubeInfo.Cube.SetActive(false);
					cubeInfo.Cube = null;
					cubeInfo.IsVisited = false;
				}
			}
		}

		public void PointOverCube(CubeInfo cubeInfo)
		{
			cubeInfo.Cube.SetActive(false);
			Vector3 positionCube = cubeInfo.Cube.transform.position;
			cubeInfo.Cube = _arm.GetObjectManager
				.GetObject<Transform>(_pathToVisitedCube, positionCube, Quaternion.identity).gameObject;
		}

		public void DiceRotation(List<List<CubeInfo>> list)
		{
			foreach (var cubeInfos in list)
			{
				foreach (var cubeInfo in cubeInfos)
				{
					if (cubeInfo.IsBarrier)
						continue;

					cubeInfo.Cube.transform.DORotate(new Vector3(0f,0f,90f), Random.Range(0.3f, 1f));
				}
			}
		}
		
		private void FillingListCube(Vector3 startingPoint, Vector3 cubeScale, List<CubeInfo> list)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].Cube != null)
					continue;

				if (list[i].IsBarrier)
				{
					list[i].Cube =
						_arm.GetObjectManager.GetObject<Transform>(
							_pathToBarrier, startingPoint + Vector3.up, Quaternion.identity).gameObject;
				}
				else
				{
					list[i].Cube =
						_arm.GetObjectManager.GetObject<Transform>(
							_pathToNotVisitedCube, startingPoint, Quaternion.identity).gameObject;
				}
				
				startingPoint += new Vector3(cubeScale.x, 0f,0f);
			}
		}
	}
}