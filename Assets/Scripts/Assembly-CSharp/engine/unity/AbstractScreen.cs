namespace engine.unity
{
	public abstract class AbstractScreen
	{
		public bool Boolean_0
		{
			get
			{
				AbstractScreen abstractScreen = ((ScreenController.ScreenController_0 == null) ? null : ScreenController.ScreenController_0.AbstractScreen_0);
				return abstractScreen != null && abstractScreen == this;
			}
		}

		public abstract void Init();

		public abstract void Release();
	}
}
