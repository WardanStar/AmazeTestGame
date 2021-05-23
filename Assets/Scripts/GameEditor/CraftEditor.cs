using System.Collections.Generic;
using AbstractSystem;
using Settings;
using ToolsSystem.Action.Main;
using UnityEngine;

namespace DefaultNamespace
{
	public class CraftEditor : MonoBehaviour
	{
		public List<List<CubeInfo>> ListCubes { get; private set; } = new List<List<CubeInfo>>(20);
		public AddElementsToFieldManager AddElementsToFieldManager { get; private set; }
		public Vector3 AngularPoint { get; private set; }
		
		[SerializeField] private Camera _camera3d;
		
		private ChangeOfSizeField _changeOfSizeField;
		private CheckInputNumbers _checkInputNumbers;
		private DisplayManager _displayManager;
		private ResourcesLevelManager _resourcesLevelManager;
		private IControlEditor _controlEditor;

		public void ChangeOfSize(int height, int width)
        {
        	if (_checkInputNumbers.CheckNumber(height) || _checkInputNumbers.CheckNumber(width))
        		return;
        		
        	_displayManager.ClearField(ListCubes);

        	ListCubes = _changeOfSizeField.ChangeOfSize(ListCubes, height, width);
        	
        	_displayManager.ContentDisplay(ListCubes, out Vector3 angularPoint);
            AngularPoint = angularPoint;
            
            AddElementsToFieldManager.CallIllusionCube(angularPoint);
        }
		
		public void Save(int level)
		{
			_displayManager.ClearField(ListCubes);
			AddElementsToFieldManager.FindOutOfThePositionOnTheField(AddElementsToFieldManager.StartPoint.transform.position, out int height, out int width);
			AddElementsToFieldManager.ClearField();
			_resourcesLevelManager.AddNewLevel(level, new LevelInfo(ListCubes, height, width));
			_resourcesLevelManager.Save();
		}
		
		private void Awake()
		{
			var pathTojObjectSettings = Resources.Load<PathToObjectSettings>("Settings/PathToObjectSettings");
			var monoBehaviourManager = new GameObject("MonoBehaviourManager").AddComponent<MonoBehaviourManager>();
			var arm = new Arm(monoBehaviourManager);
			_changeOfSizeField = new ChangeOfSizeField();
			_checkInputNumbers = new CheckInputNumbers();
			_displayManager = new DisplayManager(arm, pathTojObjectSettings.PathToNotVisitedCube, pathTojObjectSettings.PathToVisitedCube, pathTojObjectSettings.PathToBarrier);
			_controlEditor = new PCControllerEditor(monoBehaviourManager);
			AddElementsToFieldManager = new AddElementsToFieldManager(
				this, arm, pathTojObjectSettings.PathToIllusionCube, pathTojObjectSettings.PathToBarrier, pathTojObjectSettings.PathToStartPoint);
			_resourcesLevelManager = new ResourcesLevelManager("levels.dat");
			_controlEditor.DoClick += OnControlEditorDoClick;
			
		}

		private void OnControlEditorDoClick()
		{
			Vector3 cursorPosition = _controlEditor.CursorPosition;

			Ray ray = _camera3d.ScreenPointToRay(cursorPosition);

			if (Physics.Raycast(ray, out RaycastHit hit, 100f))
			{
				Collider hitCollider = hit.collider;
				if (hitCollider.CompareTag("Selector"))
					return;
				
				Vector3 hitColliderPosition = hitCollider.transform.position;
				Vector3 shift = Vector3.up;
				
				if (hitCollider.CompareTag("Barrier"))
					shift = Vector3.zero;
				
				AddElementsToFieldManager.CallIllusionCube(hitColliderPosition + shift);
			}
		}

		private void OnDisable()
		{
			_resourcesLevelManager.Save();
		}
	}
}