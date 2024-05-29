using System.Reflection;
using ProtoBuf;

[Obfuscation(Exclude = true)]
[ProtoContract]
public class FlagParamsSettings
{
	[ProtoMember(1)]
	public int FlagScore1;

	[ProtoMember(2)]
	public int FlagScore2;

	[ProtoMember(3)]
	public int FlagScore3;

	public static FlagParamsSettings Get { get; private set; }
}
