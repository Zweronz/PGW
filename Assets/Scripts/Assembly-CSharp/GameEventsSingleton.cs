public abstract class GameEventsSingleton<T> where T : class, new()
{
	private static T _instance;

	public static T Prop_0
	{
		get
		{
			if (_instance == null)
			{
				_instance = new T();
			}
			return _instance;
		}
	}
}
