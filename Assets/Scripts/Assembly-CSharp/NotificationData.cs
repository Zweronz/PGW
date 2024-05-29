using ProtoBuf;
using engine.data;

[StorageDataKey(typeof(NotificationType))]
[ProtoContract]
public class NotificationData
{
	[ProtoMember(1, IsRequired = true)]
	public NotificationType notificationType_0;

	[ProtoMember(2, IsRequired = true)]
	public string string_0;

	[ProtoMember(3, IsRequired = true)]
	public int int_0;

	[ProtoMember(4, IsRequired = true)]
	public bool bool_0;

	[ProtoMember(5, IsRequired = true)]
	public int int_1;
}
