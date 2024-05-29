using System.Reflection;
using ProtoBuf;

[Obfuscation(Exclude = true)]
[ProtoContract]
public class CommonParamsSettings
{
	[ProtoMember(1)]
	public int MaxLevel;

	[ProtoMember(2)]
	public int BonusAfterKillPlayerProb;

	[ProtoMember(3)]
	public int MaxBulletInRow;

	public static CommonParamsSettings Get { get; private set; }
}
