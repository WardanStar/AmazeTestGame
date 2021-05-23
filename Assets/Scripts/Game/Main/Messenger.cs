using System;

namespace DefaultNamespace
{
	public class Messenger
	{
		public event Action DoVisitedAllCube;

		public void DoVisitedAllCubeMailing() => DoVisitedAllCube?.Invoke();
	}
}