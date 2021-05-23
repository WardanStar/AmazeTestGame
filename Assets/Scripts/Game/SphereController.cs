using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
	public class SphereController
	{
		public readonly Transform SphereTransform;
		
        private readonly CubeManager _cubeManager;
        private readonly float _speed;
        private bool isMoving;
        private int _height;
        private int _width;

        public SphereController(CubeManager cubeManager, Transform sphereTransform, float speed)
        {
	        _cubeManager = cubeManager;
	        SphereTransform = sphereTransform;
	        _speed = speed;
        }

        public void Move(int durationHeight, int durationWidth)
        {
	        if (isMoving || _cubeManager.CheckBarrier(_height + durationHeight, _width + durationWidth))
		        return;
	        
	        int quantityStep = 0;
	        
	        while (!_cubeManager.CheckBarrier(_height + durationHeight, _width + durationWidth))
	        {
		        quantityStep++;
		        CubeInfo cubeInfo = _cubeManager.SearchCubeInfo(_height + durationHeight, _width + durationWidth);
		        DOVirtual.DelayedCall(quantityStep * _speed, () => _cubeManager.VisitedCube(cubeInfo));
		        _height += durationHeight;
		        _width += durationWidth;
	        }
	        
	        SphereTransform.DOMove(
		        _cubeManager.SearchCubeInfo(
            					_height, _width).Cube.transform.position + Vector3.up, quantityStep * _speed).
		        OnComplete((() => isMoving = false));
	        
	        isMoving = true;
        }

        public void ChengePositionToField(int height, int width)
        {
	        _height = height;
	        _width = width;
        }
	}
}