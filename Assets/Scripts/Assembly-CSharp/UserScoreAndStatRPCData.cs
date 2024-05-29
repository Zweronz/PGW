using ExitGames.Client.Photon;

public class UserScoreAndStatRPCData
{
	public int int_0;

	public int int_1;

	public int int_2;

	public float float_0;

	public short short_0;

	public int int_3;

	public short short_1;

	public static byte[] SerializePhoton(object object_0)
	{
		UserScoreAndStatRPCData userScoreAndStatRPCData = (UserScoreAndStatRPCData)object_0;
		byte[] array = new byte[24];
		int targetOffset = 0;
		Protocol.Serialize(userScoreAndStatRPCData.int_0, array, ref targetOffset);
		Protocol.Serialize(userScoreAndStatRPCData.int_1, array, ref targetOffset);
		Protocol.Serialize(userScoreAndStatRPCData.int_2, array, ref targetOffset);
		Protocol.Serialize(userScoreAndStatRPCData.float_0, array, ref targetOffset);
		Protocol.Serialize(userScoreAndStatRPCData.short_0, array, ref targetOffset);
		Protocol.Serialize(userScoreAndStatRPCData.int_3, array, ref targetOffset);
		Protocol.Serialize(userScoreAndStatRPCData.short_1, array, ref targetOffset);
		return array;
	}

	public static object DeserializePhoton(byte[] byte_0)
	{
		UserScoreAndStatRPCData userScoreAndStatRPCData = new UserScoreAndStatRPCData();
		int offset = 0;
		Protocol.Deserialize(out userScoreAndStatRPCData.int_0, byte_0, ref offset);
		Protocol.Deserialize(out userScoreAndStatRPCData.int_1, byte_0, ref offset);
		Protocol.Deserialize(out userScoreAndStatRPCData.int_2, byte_0, ref offset);
		Protocol.Deserialize(out userScoreAndStatRPCData.float_0, byte_0, ref offset);
		Protocol.Deserialize(out userScoreAndStatRPCData.short_0, byte_0, ref offset);
		Protocol.Deserialize(out userScoreAndStatRPCData.int_3, byte_0, ref offset);
		Protocol.Deserialize(out userScoreAndStatRPCData.short_1, byte_0, ref offset);
		return userScoreAndStatRPCData;
	}
}
