using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;
using UnityEngine;

public class Room : RoomInfo
{
	[CompilerGenerated]
	private string[] string_1;

	public int Int32_1
	{
		get
		{
			if (PhotonNetwork.PhotonPlayer_2 != null)
			{
				return PhotonNetwork.PhotonPlayer_2.Length;
			}
			return 0;
		}
	}

	public string String_1
	{
		get
		{
			return string_0;
		}
		internal set
		{
			string_0 = value;
		}
	}

	public int Int32_2
	{
		get
		{
			return byte_0;
		}
		set
		{
			if (!Equals(PhotonNetwork.Room_0))
			{
				Debug.LogWarning("Can't set maxPlayers when not in that room.");
			}
			if (value > 255)
			{
				Debug.LogWarning("Can't set Room.maxPlayers to: " + value + ". Using max value: 255.");
				value = 255;
			}
			if (value != byte_0 && !PhotonNetwork.Boolean_3)
			{
				PhotonNetwork.networkingPeer_0.OpSetPropertiesOfRoom(new Hashtable { 
				{
					byte.MaxValue,
					(byte)value
				} }, true, 0);
			}
			byte_0 = (byte)value;
		}
	}

	public bool Boolean_4
	{
		get
		{
			return bool_0;
		}
		set
		{
			if (!Equals(PhotonNetwork.Room_0))
			{
				Debug.LogWarning("Can't set open when not in that room.");
			}
			if (value != bool_0 && !PhotonNetwork.Boolean_3)
			{
				PhotonNetwork.networkingPeer_0.OpSetPropertiesOfRoom(new Hashtable { 
				{
					(byte)253,
					value
				} }, true, 0);
			}
			bool_0 = value;
		}
	}

	public bool Boolean_5
	{
		get
		{
			return bool_1;
		}
		set
		{
			if (!Equals(PhotonNetwork.Room_0))
			{
				Debug.LogWarning("Can't set visible when not in that room.");
			}
			if (value != bool_1 && !PhotonNetwork.Boolean_3)
			{
				PhotonNetwork.networkingPeer_0.OpSetPropertiesOfRoom(new Hashtable { 
				{
					(byte)254,
					value
				} }, true, 0);
			}
			bool_1 = value;
		}
	}

	public string[] String_2
	{
		[CompilerGenerated]
		get
		{
			return string_1;
		}
		[CompilerGenerated]
		private set
		{
			string_1 = value;
		}
	}

	public bool Boolean_6
	{
		get
		{
			return bool_2;
		}
	}

	internal Room(string string_2, RoomOptions roomOptions_0)
		: base(string_2, null)
	{
		if (roomOptions_0 == null)
		{
			roomOptions_0 = new RoomOptions();
		}
		bool_1 = roomOptions_0.bool_0;
		bool_0 = roomOptions_0.bool_1;
		byte_0 = (byte)roomOptions_0.int_0;
		bool_2 = false;
		CacheProperties(roomOptions_0.hashtable_0);
		String_2 = roomOptions_0.string_0;
	}

	public void SetCustomProperties(Hashtable hashtable_1)
	{
		if (hashtable_1 != null)
		{
			base.Hashtable_0.MergeStringKeys(hashtable_1);
			base.Hashtable_0.StripKeysWithNullValues();
			Hashtable hashtable = hashtable_1.StripToStringKeys();
			if (!PhotonNetwork.Boolean_3)
			{
				PhotonNetwork.networkingPeer_0.OpSetCustomPropertiesOfRoom(hashtable, true, 0);
			}
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonCustomRoomPropertiesChanged, hashtable_1);
		}
	}

	public void SetPropertiesListedInLobby(string[] string_2)
	{
		Hashtable hashtable = new Hashtable();
		hashtable[(byte)250] = string_2;
		PhotonNetwork.networkingPeer_0.OpSetPropertiesOfRoom(hashtable, false, 0);
		String_2 = string_2;
	}

	public override string ToString()
	{
		return string.Format("Room: '{0}' {1},{2} {4}/{3} players.", string_0, (!bool_1) ? "hidden" : "visible", (!bool_0) ? "closed" : "open", byte_0, Int32_1);
	}

	public new string ToStringFull()
	{
		return string.Format("Room: '{0}' {1},{2} {4}/{3} players.\ncustomProps: {5}", string_0, (!bool_1) ? "hidden" : "visible", (!bool_0) ? "closed" : "open", byte_0, Int32_1, base.Hashtable_0.ToStringFull());
	}
}
