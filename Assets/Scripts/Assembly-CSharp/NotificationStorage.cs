using System.Reflection;
using engine.data;

[Obfuscation(Exclude = true)]
public sealed class NotificationStorage : BaseStorage<NotificationType, NotificationData>
{
	private static NotificationStorage _instance;

	public static NotificationStorage Get
	{
		get
		{
			if (_instance == null)
			{
				_instance = new NotificationStorage();
			}
			return _instance;
		}
	}

	private NotificationStorage()
	{
	}
}
