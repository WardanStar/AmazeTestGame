namespace InitializeSystem
{
	public interface IInitialize
	{
		void Initialize();
	}
	public interface IInitialize<TInit>
	{
		void Assign(TInit init);
	}
	public interface IInitialize<TInit1, TInit2>
	{
		void Assign(TInit1 init1, TInit2 init2);
	}
	public interface IInitialize<TInit1, TInit2, TInit3>
	{
		void Assign(TInit1 init1, TInit2 init2, TInit3 init3);
	}
	public interface IInitialize<TInit1, TInit2, TInit3, TInit4>
	{
		void Assign(TInit1 model, TInit2 init2, TInit3 init3, TInit4 init4);
	}
	public interface IInitialize<TInit1, TInit2, TInit3, TInit4, TInit5>
	{
		void Assign(TInit1 init1, TInit2 init2, TInit3 init3, TInit4 init4, TInit5 init5);
	}
}