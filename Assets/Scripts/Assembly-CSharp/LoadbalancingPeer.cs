using System.Collections.Generic;
using ExitGames.Client.Photon;

internal class LoadbalancingPeer : PhotonPeer
{
	public LoadbalancingPeer(IPhotonPeerListener iphotonPeerListener_0, ConnectionProtocol connectionProtocol_0)
		: base(iphotonPeerListener_0, connectionProtocol_0)
	{
	}

	public virtual bool OpGetRegions(string string_0)
	{
		Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
		dictionary[224] = string_0;
		return OpCustom(220, dictionary, true, 0, true);
	}

	public virtual bool OpJoinLobby(TypedLobby typedLobby_0)
	{
		if ((int)base.DebugOut >= 3)
		{
			base.Listener.DebugReturn(DebugLevel.INFO, "OpJoinLobby()");
		}
		Dictionary<byte, object> dictionary = null;
		if (typedLobby_0 != null && !typedLobby_0.Boolean_0)
		{
			dictionary = new Dictionary<byte, object>();
			dictionary[213] = typedLobby_0.string_0;
			dictionary[212] = (byte)typedLobby_0.lobbyType_0;
		}
		return OpCustom(229, dictionary, true);
	}

	public virtual bool OpLeaveLobby()
	{
		if ((int)base.DebugOut >= 3)
		{
			base.Listener.DebugReturn(DebugLevel.INFO, "OpLeaveLobby()");
		}
		return OpCustom(228, null, true);
	}

	public virtual bool OpCreateRoom(string string_0, RoomOptions roomOptions_0, TypedLobby typedLobby_0, Hashtable hashtable_0, bool bool_0)
	{
		if ((int)base.DebugOut >= 3)
		{
			base.Listener.DebugReturn(DebugLevel.INFO, "OpCreateRoom()");
		}
		Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
		if (!string.IsNullOrEmpty(string_0))
		{
			dictionary[byte.MaxValue] = string_0;
		}
		if (typedLobby_0 != null)
		{
			dictionary[213] = typedLobby_0.string_0;
			dictionary[212] = (byte)typedLobby_0.lobbyType_0;
		}
		if (bool_0)
		{
			if (hashtable_0 != null && hashtable_0.Count > 0)
			{
				dictionary[249] = hashtable_0;
				dictionary[250] = true;
			}
			if (roomOptions_0 == null)
			{
				roomOptions_0 = new RoomOptions();
			}
			Hashtable hashtable2 = (Hashtable)(dictionary[248] = new Hashtable());
			hashtable2.MergeStringKeys(roomOptions_0.hashtable_0);
			hashtable2[(byte)253] = roomOptions_0.bool_1;
			hashtable2[(byte)254] = roomOptions_0.bool_0;
			hashtable2[(byte)250] = roomOptions_0.string_0;
			if (roomOptions_0.int_0 > 0)
			{
				hashtable2[byte.MaxValue] = roomOptions_0.int_0;
			}
			if (roomOptions_0.bool_2)
			{
				dictionary[241] = true;
				hashtable2[(byte)249] = true;
			}
		}
		return OpCustom(227, dictionary, true);
	}

	public virtual bool OpJoinRoom(string string_0, RoomOptions roomOptions_0, TypedLobby typedLobby_0, bool bool_0, Hashtable hashtable_0, bool bool_1)
	{
		Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
		if (!string.IsNullOrEmpty(string_0))
		{
			dictionary[byte.MaxValue] = string_0;
		}
		if (bool_0)
		{
			dictionary[215] = true;
			if (typedLobby_0 != null)
			{
				dictionary[213] = typedLobby_0.string_0;
				dictionary[212] = (byte)typedLobby_0.lobbyType_0;
			}
		}
		if (bool_1)
		{
			if (hashtable_0 != null && hashtable_0.Count > 0)
			{
				dictionary[249] = hashtable_0;
				dictionary[250] = true;
			}
			if (bool_0)
			{
				if (roomOptions_0 == null)
				{
					roomOptions_0 = new RoomOptions();
				}
				Hashtable hashtable2 = (Hashtable)(dictionary[248] = new Hashtable());
				hashtable2.MergeStringKeys(roomOptions_0.hashtable_0);
				hashtable2[(byte)253] = roomOptions_0.bool_1;
				hashtable2[(byte)254] = roomOptions_0.bool_0;
				hashtable2[(byte)250] = roomOptions_0.string_0;
				if (roomOptions_0.int_0 > 0)
				{
					hashtable2[byte.MaxValue] = roomOptions_0.int_0;
				}
				if (roomOptions_0.bool_2)
				{
					dictionary[241] = true;
					hashtable2[(byte)249] = true;
				}
			}
		}
		return OpCustom(226, dictionary, true);
	}

	public virtual bool OpJoinRandomRoom(Hashtable hashtable_0, byte byte_0, Hashtable hashtable_1, MatchmakingMode matchmakingMode_0, TypedLobby typedLobby_0, string string_0)
	{
		if ((int)base.DebugOut >= 3)
		{
			base.Listener.DebugReturn(DebugLevel.INFO, "OpJoinRandomRoom()");
		}
		Hashtable hashtable = new Hashtable();
		hashtable.MergeStringKeys(hashtable_0);
		if (byte_0 > 0)
		{
			hashtable[byte.MaxValue] = byte_0;
		}
		Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
		if (hashtable.Count > 0)
		{
			dictionary[248] = hashtable;
		}
		if (hashtable_1 != null && hashtable_1.Count > 0)
		{
			dictionary[249] = hashtable_1;
		}
		if (matchmakingMode_0 != 0)
		{
			dictionary[223] = (byte)matchmakingMode_0;
		}
		if (typedLobby_0 != null)
		{
			dictionary[213] = typedLobby_0.string_0;
			dictionary[212] = (byte)typedLobby_0.lobbyType_0;
		}
		if (!string.IsNullOrEmpty(string_0))
		{
			dictionary[245] = string_0;
		}
		return OpCustom(225, dictionary, true);
	}

	public virtual bool OpFindFriends(string[] string_0)
	{
		Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
		if (string_0 != null && string_0.Length > 0)
		{
			dictionary[1] = string_0;
		}
		return OpCustom(222, dictionary, true);
	}

	public bool OpSetCustomPropertiesOfActor(int int_0, Hashtable hashtable_0, bool bool_0, byte byte_0)
	{
		return OpSetPropertiesOfActor(int_0, hashtable_0.StripToStringKeys(), bool_0, byte_0);
	}

	protected bool OpSetPropertiesOfActor(int int_0, Hashtable hashtable_0, bool bool_0, byte byte_0)
	{
		if ((int)base.DebugOut >= 3)
		{
			base.Listener.DebugReturn(DebugLevel.INFO, "OpSetPropertiesOfActor()");
		}
		if (int_0 > 0 && hashtable_0 != null)
		{
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			dictionary.Add(251, hashtable_0);
			dictionary.Add(254, int_0);
			if (bool_0)
			{
				dictionary.Add(250, bool_0);
			}
			return OpCustom(252, dictionary, bool_0, byte_0);
		}
		if ((int)base.DebugOut >= 3)
		{
			base.Listener.DebugReturn(DebugLevel.INFO, "OpSetPropertiesOfActor not sent. ActorNr must be > 0 and actorProperties != null.");
		}
		return false;
	}

	protected void OpSetPropertyOfRoom(byte byte_0, object object_0)
	{
		Hashtable hashtable = new Hashtable();
		hashtable[byte_0] = object_0;
		OpSetPropertiesOfRoom(hashtable, true, 0);
	}

	public bool OpSetCustomPropertiesOfRoom(Hashtable hashtable_0, bool bool_0, byte byte_0)
	{
		return OpSetPropertiesOfRoom(hashtable_0.StripToStringKeys(), bool_0, byte_0);
	}

	public bool OpSetPropertiesOfRoom(Hashtable hashtable_0, bool bool_0, byte byte_0)
	{
		if ((int)base.DebugOut >= 3)
		{
			base.Listener.DebugReturn(DebugLevel.INFO, "OpSetPropertiesOfRoom()");
		}
		Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
		dictionary.Add(251, hashtable_0);
		if (bool_0)
		{
			dictionary.Add(250, true);
		}
		return OpCustom(252, dictionary, bool_0, byte_0);
	}

	public virtual bool OpAuthenticate(string string_0, string string_1, string string_2, AuthenticationValues authenticationValues_0, string string_3)
	{
		if ((int)base.DebugOut >= 3)
		{
			base.Listener.DebugReturn(DebugLevel.INFO, "OpAuthenticate()");
		}
		Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
		if (authenticationValues_0 != null && authenticationValues_0.string_1 != null)
		{
			dictionary[221] = authenticationValues_0.string_1;
			return OpCustom(230, dictionary, true, 0, false);
		}
		dictionary[220] = string_1;
		dictionary[224] = string_0;
		if (!string.IsNullOrEmpty(string_3))
		{
			dictionary[210] = string_3;
		}
		if (!string.IsNullOrEmpty(string_2))
		{
			dictionary[225] = string_2;
		}
		if (authenticationValues_0 != null && authenticationValues_0.customAuthenticationType_0 != CustomAuthenticationType.None)
		{
			if (!base.IsEncryptionAvailable)
			{
				base.Listener.DebugReturn(DebugLevel.ERROR, "OpAuthenticate() failed. When you want Custom Authentication encryption is mandatory.");
				return false;
			}
			dictionary[217] = (byte)authenticationValues_0.customAuthenticationType_0;
			if (!string.IsNullOrEmpty(authenticationValues_0.string_1))
			{
				dictionary[221] = authenticationValues_0.string_1;
			}
			if (!string.IsNullOrEmpty(authenticationValues_0.string_0))
			{
				dictionary[216] = authenticationValues_0.string_0;
			}
			if (authenticationValues_0.Object_0 != null)
			{
				dictionary[214] = authenticationValues_0.Object_0;
			}
		}
		bool result;
		if (!(result = OpCustom(230, dictionary, true, 0, base.IsEncryptionAvailable)))
		{
			base.Listener.DebugReturn(DebugLevel.ERROR, "Error calling OpAuthenticate! Did not work. Check log output, CustomAuthenticationValues and if you're connected.");
		}
		return result;
	}

	public virtual bool OpChangeGroups(byte[] byte_0, byte[] byte_1)
	{
		if ((int)base.DebugOut >= 5)
		{
			base.Listener.DebugReturn(DebugLevel.ALL, "OpChangeGroups()");
		}
		Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
		if (byte_0 != null)
		{
			dictionary[239] = byte_0;
		}
		if (byte_1 != null)
		{
			dictionary[238] = byte_1;
		}
		return OpCustom(248, dictionary, true, 0);
	}

	public virtual bool OpRaiseEvent(byte byte_0, object object_0, bool bool_0, RaiseEventOptions raiseEventOptions_0)
	{
		Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
		dictionary[244] = byte_0;
		if (object_0 != null)
		{
			dictionary[245] = object_0;
		}
		if (raiseEventOptions_0 == null)
		{
			raiseEventOptions_0 = RaiseEventOptions.raiseEventOptions_0;
		}
		else
		{
			if (raiseEventOptions_0.eventCaching_0 != 0)
			{
				dictionary[247] = (byte)raiseEventOptions_0.eventCaching_0;
			}
			if (raiseEventOptions_0.receiverGroup_0 != 0)
			{
				dictionary[246] = (byte)raiseEventOptions_0.receiverGroup_0;
			}
			if (raiseEventOptions_0.byte_0 != 0)
			{
				dictionary[240] = raiseEventOptions_0.byte_0;
			}
			if (raiseEventOptions_0.int_0 != null)
			{
				dictionary[252] = raiseEventOptions_0.int_0;
			}
			if (raiseEventOptions_0.bool_0)
			{
				dictionary[234] = true;
			}
		}
		return OpCustom(253, dictionary, bool_0, raiseEventOptions_0.byte_1, false);
	}
}
