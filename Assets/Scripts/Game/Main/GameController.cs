using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AbstractSystem;
using Newtonsoft.Json;
using Settings;
using ToolsSystem.Action.Main;
using UnityEngine;

namespace DefaultNamespace
{
	public class GameController : MonoBehaviour
	{
		public event Action<int> DoWin;
		public int Level => _currenLevel;

		[SerializeField] private ParticleSystem _firework;
		
		private Arm _arm;
		private IControl _control;
		private SphereController _sphereController;
		private CubeManager _cubeManager;
		private ResourcesLevelManager _resourcesLevelManager;
		private string _pathToSphere;
		private int _currenLevel;

		private void Awake()
		{
			var pathToObjectSettings = Resources.Load<PathToObjectSettings>("Settings/PathToObjectSettings");
			var monoBehaviourManager = new GameObject("MonoBehaviourManager").AddComponent<MonoBehaviourManager>();
			_arm = new Arm(monoBehaviourManager);
			_control = new PCController(monoBehaviourManager);
			_cubeManager = new CubeManager(_arm);
			_resourcesLevelManager = new ResourcesLevelManager("levels.dat");
			_pathToSphere = pathToObjectSettings.PathToSphere;
			_control.DoHorizontalChange += OnControlDoHorizontalChange;
			_control.DoVerticalChange += OnControlDoVerticalChange;
			_cubeManager.DoVisitedAllCube += OnCubeManagerDoVisitedAllCube;
		}

		private void Start()
		{
			_currenLevel = 1;
			var gameSettings = Resources.Load<GameSettings>("Settings/GameSettings");
            var sphereTransform =
                            _arm.GetObjectManager.GetObject<Transform>(_pathToSphere);
            
            _sphereController = new SphereController(_cubeManager, sphereTransform, gameSettings.SpeedSphere);
            
			LoadLevel();
		}

		public void LoadLevel()
		{
			var levelInfo = _resourcesLevelManager.GetLevel(ref _currenLevel);
			
            _cubeManager.LoadField(levelInfo.List);
            
            var startPosition = _cubeManager.SearchCubeInfo(levelInfo.HeightStartPoint, levelInfo.WidthStartPoint).Cube.transform.position + Vector3.up;
            _sphereController.SphereTransform.position = startPosition;
            _sphereController.ChengePositionToField(levelInfo.HeightStartPoint, levelInfo.WidthStartPoint);
            
            _cubeManager.VisitedCube(_cubeManager.SearchCubeInfo(levelInfo.HeightStartPoint, levelInfo.WidthStartPoint));
		}
		
		private void OnCubeManagerDoVisitedAllCube()
		{
			_firework.Play();
			DoWin?.Invoke(Level);
			_currenLevel++;
		}

		private void OnControlDoVerticalChange(float y)
        {
	        if (y > 0)
	        {
		        _sphereController.Move(1,0);
	        }
	        else
	        {
		        _sphereController.Move(-1,0);
	        }
        }

        private void OnControlDoHorizontalChange(float x)
        {
	        if (x > 0)
	        {
		        _sphereController.Move(0,1);
	        }
	        else
	        {
		        _sphereController.Move(0,-1);
	        }
        }
	}
}