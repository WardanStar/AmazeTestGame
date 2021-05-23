using System;

namespace Tools.Scripts.Abstract
{
	public interface IControl
	{
		float X { get;}
		float Y { get;}
	}

	public interface IPlayerControl
	{
		event Action DoMove;
		event Action DoAttack;
	}

	public interface IModule
	{
		public void Update();
	}
	
	public interface IStaticModule : IModule{}
	public interface IActiveModule : IModule{}
}