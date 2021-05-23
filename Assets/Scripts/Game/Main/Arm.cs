using ResourcesLoadSystem;
using ToolsSystem.Action.Main;

namespace DefaultNamespace
{
	public class Arm
	{
		public GetObjectManager GetObjectManager { get; }
		
		public Arm(MonoBehaviourManager monoBehaviourManager)
		{
			GetObjectManager = new GetObjectManager(new ResourceLoadManager(),
				new PoolManager(), monoBehaviourManager);
		}
	}
}