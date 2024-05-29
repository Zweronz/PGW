using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;

public class RoomInfo
{
	private Hashtable hashtable_0 = new Hashtable();

	protected byte byte_0;

	protected bool bool_0 = true;

	protected bool bool_1 = true;

	protected bool bool_2 = PhotonNetwork.Boolean_5;

	protected string string_0;

	[CompilerGenerated]
	private bool bool_3;

	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private bool bool_4;

	public bool Boolean_0
	{
		[CompilerGenerated]
		get
		{
			return bool_3;
		}
		[CompilerGenerated]
		internal set
		{
			bool_3 = value;
		}
	}

	public Hashtable Hashtable_0
	{
		get
		{
			return hashtable_0;
		}
	}

	public string String_0
	{
		get
		{
			return string_0;
		}
	}

	public int Int32_0
	{
		[CompilerGenerated]
		get
		{
			return int_0;
		}
		[CompilerGenerated]
		private set
		{
			int_0 = value;
		}
	}

	public bool Boolean_1
	{
		[CompilerGenerated]
		get
		{
			return bool_4;
		}
		[CompilerGenerated]
		set
		{
			bool_4 = value;
		}
	}

	public byte Byte_0
	{
		get
		{
			return byte_0;
		}
	}

	public bool Boolean_2
	{
		get
		{
			return bool_0;
		}
	}

	public bool Boolean_3
	{
		get
		{
			return bool_1;
		}
	}

	protected internal RoomInfo(string string_1, Hashtable hashtable_1)
	{
		CacheProperties(hashtable_1);
		string_0 = string_1;
	}

	public override bool Equals(object obj)
	{
		Room room = obj as Room;
		return room != null && string_0.Equals(room.string_0);
	}

	public override int GetHashCode()
	{
		return string_0.GetHashCode();
	}

	public override string ToString()
	{
		return string.Format("Room: '{0}' {1},{2} {4}/{3} players.", string_0, (!bool_1) ? "hidden" : "visible", (!bool_0) ? "closed" : "open", byte_0, Int32_0);
	}

	public string ToStringFull()
	{
		return string.Format("Room: '{0}' {1},{2} {4}/{3} players.\ncustomProps: {5}", string_0, (!bool_1) ? "hidden" : "visible", (!bool_0) ? "closed" : "open", byte_0, Int32_0, hashtable_0.ToStringFull());
	}

	protected internal void CacheProperties(Hashtable hashtable_1)
	{
		if (hashtable_1 == null || hashtable_1.Count == 0 || hashtable_0.Equals(hashtable_1))
		{
			return;
		}
		if (hashtable_1.ContainsKey((byte)251))
		{
			Boolean_0 = (bool)hashtable_1[(byte)251];
			if (Boolean_0)
			{
				return;
			}
		}
		if (hashtable_1.ContainsKey(byte.MaxValue))
		{
			byte_0 = (byte)hashtable_1[byte.MaxValue];
		}
		if (hashtable_1.ContainsKey((byte)253))
		{
			bool_0 = (bool)hashtable_1[(byte)253];
		}
		if (hashtable_1.ContainsKey((byte)254))
		{
			bool_1 = (bool)hashtable_1[(byte)254];
		}
		if (hashtable_1.ContainsKey((byte)252))
		{
			Int32_0 = (byte)hashtable_1[(byte)252];
		}
		if (hashtable_1.ContainsKey((byte)249))
		{
			bool_2 = (bool)hashtable_1[(byte)249];
		}
		hashtable_0.MergeStringKeys(hashtable_1);
	}
}
