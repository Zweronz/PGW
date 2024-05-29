using System.Collections.Generic;
using UnityEngine;

public class PhotonStream
{
	private bool bool_0;

	internal List<object> list_0;

	private byte byte_0;

	public bool Boolean_0
	{
		get
		{
			return bool_0;
		}
	}

	public bool Boolean_1
	{
		get
		{
			return !bool_0;
		}
	}

	public int Int32_0
	{
		get
		{
			return list_0.Count;
		}
	}

	public PhotonStream(bool bool_1, object[] object_0)
	{
		bool_0 = bool_1;
		if (object_0 == null)
		{
			list_0 = new List<object>();
		}
		else
		{
			list_0 = new List<object>(object_0);
		}
	}

	public object ReceiveNext()
	{
		if (bool_0)
		{
			Debug.LogError("Error: you cannot read this stream that you are writing!");
			return null;
		}
		object result = list_0[byte_0];
		byte_0++;
		return result;
	}

	public void SendNext(object object_0)
	{
		if (!bool_0)
		{
			Debug.LogError("Error: you cannot write/send to this stream that you are reading!");
		}
		else
		{
			list_0.Add(object_0);
		}
	}

	public object[] ToArray()
	{
		return list_0.ToArray();
	}

	public void Serialize(ref bool bool_1)
	{
		if (bool_0)
		{
			list_0.Add(bool_1);
		}
		else if (list_0.Count > byte_0)
		{
			bool_1 = (bool)list_0[byte_0];
			byte_0++;
		}
	}

	public void Serialize(ref int int_0)
	{
		if (bool_0)
		{
			list_0.Add(int_0);
		}
		else if (list_0.Count > byte_0)
		{
			int_0 = (int)list_0[byte_0];
			byte_0++;
		}
	}

	public void Serialize(ref string string_0)
	{
		if (bool_0)
		{
			list_0.Add(string_0);
		}
		else if (list_0.Count > byte_0)
		{
			string_0 = (string)list_0[byte_0];
			byte_0++;
		}
	}

	public void Serialize(ref char char_0)
	{
		if (bool_0)
		{
			list_0.Add(char_0);
		}
		else if (list_0.Count > byte_0)
		{
			char_0 = (char)list_0[byte_0];
			byte_0++;
		}
	}

	public void Serialize(ref short short_0)
	{
		if (bool_0)
		{
			list_0.Add(short_0);
		}
		else if (list_0.Count > byte_0)
		{
			short_0 = (short)list_0[byte_0];
			byte_0++;
		}
	}

	public void Serialize(ref float float_0)
	{
		if (bool_0)
		{
			list_0.Add(float_0);
		}
		else if (list_0.Count > byte_0)
		{
			float_0 = (float)list_0[byte_0];
			byte_0++;
		}
	}

	public void Serialize(ref PhotonPlayer photonPlayer_0)
	{
		if (bool_0)
		{
			list_0.Add(photonPlayer_0);
		}
		else if (list_0.Count > byte_0)
		{
			photonPlayer_0 = (PhotonPlayer)list_0[byte_0];
			byte_0++;
		}
	}

	public void Serialize(ref Vector3 vector3_0)
	{
		if (bool_0)
		{
			list_0.Add(vector3_0);
		}
		else if (list_0.Count > byte_0)
		{
			vector3_0 = (Vector3)list_0[byte_0];
			byte_0++;
		}
	}

	public void Serialize(ref Vector2 vector2_0)
	{
		if (bool_0)
		{
			list_0.Add(vector2_0);
		}
		else if (list_0.Count > byte_0)
		{
			vector2_0 = (Vector2)list_0[byte_0];
			byte_0++;
		}
	}

	public void Serialize(ref Quaternion quaternion_0)
	{
		if (bool_0)
		{
			list_0.Add(quaternion_0);
		}
		else if (list_0.Count > byte_0)
		{
			quaternion_0 = (Quaternion)list_0[byte_0];
			byte_0++;
		}
	}
}
