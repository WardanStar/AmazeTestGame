using UnityEngine;

namespace ToolsSystem.Action.Additional
{
	public class Timer
	{
		public float InterpolationTime
		{
			get
			{
				if(!_enabled) 
					return 0;

				return (Time.time - _startTime) / _endTime;
			}
		}

		public float ResidualTime 
		{
			get 
			{
				if(!_enabled) 
					return 0;

				return _endTime - (Time.time - _startTime); 
			}
		}
		
		private float _startTime;
		private float _endTime;
		private bool _enabled;

		public Timer(){}

		public Timer(float endTime)
		{
			UpdateTimer(endTime);
		}
		
		public void UpdateTimer(float endTime)
		{
			_endTime = endTime;
		}
		
		public void StartTimer()
		{
			if (Mathf.Approximately(_endTime, 0f))
			{
				Debug.Log("Not Exposed EndTime To Timer");
				return;
			}
			
			_startTime = Time.time;
			_enabled = true;
		}

		public bool UpdateAndCheckEndedToTimer()
		{
			if (!_enabled || Time.time - _startTime < _endTime)
				return false;

			_enabled = false;
			return true;
		}
	}
}