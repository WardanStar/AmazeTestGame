using System;
using System.Collections.Generic;
using Settings;
using UnityEngine;

namespace DefaultNamespace
{
	public class CubeManager
	{
		public event Action DoVisitedAllCube;
		
		private List<List<CubeInfo>> _listCubes;
		private DisplayManager _displayManager;
		private int _notVisitedCube;
		
		public CubeManager(Arm arm)
		{
			var pathToObjectSettings = Resources.Load<PathToObjectSettings>("Settings/PathToObjectSettings");
			_displayManager = new DisplayManager(arm, pathToObjectSettings.PathToNotVisitedCube, pathToObjectSettings.PathToVisitedCube, pathToObjectSettings.PathToBarrier);
		}

		public void LoadField(List<List<CubeInfo>> list)
		{
			if (_listCubes != null)
				_displayManager.ClearField(_listCubes);
			
			_listCubes = list;
           
            foreach (var cubeInfos in _listCubes)
            {
            	foreach (var cubeInfo in cubeInfos)
            	{
            		if (cubeInfo.IsBarrier)
            			continue;

            		_notVisitedCube += 1;
            	}
            }
            
            _displayManager.ContentDisplay(_listCubes, out _);
		}
		
		public CubeInfo SearchCubeInfo(int height, int width)
		{
			return _listCubes[height][width];
		}
		
		public bool CheckBarrier(int height, int width)
		{
			return _listCubes[height][width].IsBarrier;
		}

		public void VisitedCube(CubeInfo cubeInfo)
		{
			if (cubeInfo.IsVisited)
				return;
			
			cubeInfo.IsVisited = true;
			
			_displayManager.PointOverCube(cubeInfo);
			
			_notVisitedCube -= 1;
			if (_notVisitedCube != 0)
				return;
			
			_displayManager.DiceRotation(_listCubes);
			DoVisitedAllCube?.Invoke();
		}
	}
}