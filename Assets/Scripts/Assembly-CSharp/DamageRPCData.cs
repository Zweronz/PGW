using ExitGames.Client.Photon;

public class DamageRPCData
{
	public int int_0;

	public float float_0;

	public float float_1;

	public float float_2;

	public byte byte_0;

	public byte byte_1;

	public int int_1;

	public int int_2;

	public bool bool_0;

	public bool bool_1;

	public bool bool_2;

	public float float_3;

	public float float_4;

	public float float_5;

	public static byte[] SerializePhoton(object object_0)
	{
		DamageRPCData damageRPCData = (DamageRPCData)object_0;
		byte[] array = new byte[41];
		int targetOffset = 0;
		Protocol.Serialize(damageRPCData.int_0, array, ref targetOffset);
		Protocol.Serialize(damageRPCData.float_0, array, ref targetOffset);
		Protocol.Serialize(damageRPCData.float_1, array, ref targetOffset);
		Protocol.Serialize(damageRPCData.float_2, array, ref targetOffset);
		Protocol.Serialize(damageRPCData.int_1, array, ref targetOffset);
		Protocol.Serialize(damageRPCData.int_2, array, ref targetOffset);
		Protocol.Serialize(damageRPCData.float_3, array, ref targetOffset);
		Protocol.Serialize(damageRPCData.float_4, array, ref targetOffset);
		Protocol.Serialize(damageRPCData.float_5, array, ref targetOffset);
		int num = (damageRPCData.bool_2 ? 1 : 0);
		num = damageRPCData.byte_0 << 24;
		num |= damageRPCData.byte_1 << 16;
		num |= (damageRPCData.bool_0 ? 1 : 0) << 8;
		num |= (damageRPCData.bool_1 ? 1 : 0);
		Protocol.Serialize(num, array, ref targetOffset);
		return array;
	}

	public static object DeserializePhoton(byte[] byte_2)
	{
		DamageRPCData damageRPCData = new DamageRPCData();
		int offset = 0;
		Protocol.Deserialize(out damageRPCData.int_0, byte_2, ref offset);
		Protocol.Deserialize(out damageRPCData.float_0, byte_2, ref offset);
		Protocol.Deserialize(out damageRPCData.float_1, byte_2, ref offset);
		Protocol.Deserialize(out damageRPCData.float_2, byte_2, ref offset);
		Protocol.Deserialize(out damageRPCData.int_1, byte_2, ref offset);
		Protocol.Deserialize(out damageRPCData.int_2, byte_2, ref offset);
		Protocol.Deserialize(out damageRPCData.float_3, byte_2, ref offset);
		Protocol.Deserialize(out damageRPCData.float_4, byte_2, ref offset);
		Protocol.Deserialize(out damageRPCData.float_5, byte_2, ref offset);
		int value = 0;
		Protocol.Deserialize(out value, byte_2, ref offset);
		damageRPCData.bool_2 = (value & 0xFF) == 1;
		damageRPCData.byte_0 = (byte)(value >> 24);
		damageRPCData.byte_1 = (byte)((uint)(value >> 16) & 0xFFu);
		damageRPCData.bool_0 = ((value >> 8) & 0xFF) == 1;
		damageRPCData.bool_1 = (value & 0xFF) == 1;
		return damageRPCData;
	}
}
