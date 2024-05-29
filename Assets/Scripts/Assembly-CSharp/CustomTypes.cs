using ExitGames.Client.Photon;
using UnityEngine;

internal static class CustomTypes
{
	internal static void Register()
	{
		PhotonPeer.RegisterType(typeof(Vector2), 87, SerializeVector2, DeserializeVector2);
		PhotonPeer.RegisterType(typeof(Vector3), 86, SerializeVector3, DeserializeVector3);
		PhotonPeer.RegisterType(typeof(Quaternion), 81, SerializeQuaternion, DeserializeQuaternion);
		PhotonPeer.RegisterType(typeof(PhotonPlayer), 80, SerializePhotonPlayer, DeserializePhotonPlayer);
	}

	private static byte[] SerializeVector3(object object_0)
	{
		Vector3 vector = (Vector3)object_0;
		int targetOffset = 0;
		byte[] array = new byte[12];
		Protocol.Serialize(vector.x, array, ref targetOffset);
		Protocol.Serialize(vector.y, array, ref targetOffset);
		Protocol.Serialize(vector.z, array, ref targetOffset);
		return array;
	}

	private static object DeserializeVector3(byte[] byte_0)
	{
		Vector3 vector = default(Vector3);
		int offset = 0;
		Protocol.Deserialize(out vector.x, byte_0, ref offset);
		Protocol.Deserialize(out vector.y, byte_0, ref offset);
		Protocol.Deserialize(out vector.z, byte_0, ref offset);
		return vector;
	}

	private static byte[] SerializeVector2(object object_0)
	{
		Vector2 vector = (Vector2)object_0;
		byte[] array = new byte[8];
		int targetOffset = 0;
		Protocol.Serialize(vector.x, array, ref targetOffset);
		Protocol.Serialize(vector.y, array, ref targetOffset);
		return array;
	}

	private static object DeserializeVector2(byte[] byte_0)
	{
		Vector2 vector = default(Vector2);
		int offset = 0;
		Protocol.Deserialize(out vector.x, byte_0, ref offset);
		Protocol.Deserialize(out vector.y, byte_0, ref offset);
		return vector;
	}

	private static byte[] SerializeQuaternion(object object_0)
	{
		Quaternion quaternion = (Quaternion)object_0;
		byte[] array = new byte[16];
		int targetOffset = 0;
		Protocol.Serialize(quaternion.w, array, ref targetOffset);
		Protocol.Serialize(quaternion.x, array, ref targetOffset);
		Protocol.Serialize(quaternion.y, array, ref targetOffset);
		Protocol.Serialize(quaternion.z, array, ref targetOffset);
		return array;
	}

	private static object DeserializeQuaternion(byte[] byte_0)
	{
		Quaternion quaternion = default(Quaternion);
		int offset = 0;
		Protocol.Deserialize(out quaternion.w, byte_0, ref offset);
		Protocol.Deserialize(out quaternion.x, byte_0, ref offset);
		Protocol.Deserialize(out quaternion.y, byte_0, ref offset);
		Protocol.Deserialize(out quaternion.z, byte_0, ref offset);
		return quaternion;
	}

	private static byte[] SerializePhotonPlayer(object object_0)
	{
		int int32_ = ((PhotonPlayer)object_0).Int32_0;
		byte[] array = new byte[4];
		int targetOffset = 0;
		Protocol.Serialize(int32_, array, ref targetOffset);
		return array;
	}

	private static object DeserializePhotonPlayer(byte[] byte_0)
	{
		int offset = 0;
		int value;
		Protocol.Deserialize(out value, byte_0, ref offset);
		if (PhotonNetwork.networkingPeer_0.dictionary_0.ContainsKey(value))
		{
			return PhotonNetwork.networkingPeer_0.dictionary_0[value];
		}
		return null;
	}
}
