namespace InitializeSystem
{
	public interface IWeaponController
	{ 
		void Shot();
	}

	public interface ITickableUpdate
	{
		void TickUpdate();
		bool disabled { get; set; }
	}
	public interface ITickableLateUpdate
	{
		void TickLateUpdate();
		bool disabled { get; set; }
	}
	public interface ITickableFixedUpdate
	{
		void TickFixedUpdate();
		bool disabled { get; set; }
	}
}