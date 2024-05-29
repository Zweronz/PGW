namespace engine.operations
{
	public abstract class TheradOperation : Operation
	{
		public virtual void Update()
		{
		}

		public override void Complete()
		{
			base.Boolean_0 = true;
		}
	}
}
