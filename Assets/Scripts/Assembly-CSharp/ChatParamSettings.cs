using System.Reflection;
using ProtoBuf;

[Obfuscation(Exclude = true)]
[ProtoContract]
public class ChatParamSettings
{
	[ProtoMember(1)]
	public int numberOfCheckMessage;

	[ProtoMember(2)]
	public float checkMessageTime;

	public static ChatParamSettings Get { get; private set; }
}
