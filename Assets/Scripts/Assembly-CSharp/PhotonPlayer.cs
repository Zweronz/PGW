using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;
using UnityEngine;

public class PhotonPlayer
{
	private int int_0 = -1;

	private string string_0 = string.Empty;

	public readonly bool bool_0;

	public object object_0;

	[CompilerGenerated]
	private Hashtable hashtable_0;

	public int Int32_0
	{
		get
		{
			return int_0;
		}
	}

	public string String_0
	{
		get
		{
			return string_0;
		}
		set
		{
			if (!bool_0)
			{
				Debug.LogError("Error: Cannot change the name of a remote player!");
			}
			else
			{
				string_0 = value;
			}
		}
	}

	public bool Boolean_0
	{
		get
		{
			return PhotonNetwork.networkingPeer_0.photonPlayer_2 == this;
		}
	}

	public Hashtable Hashtable_0
	{
		[CompilerGenerated]
		get
		{
			return hashtable_0;
		}
		[CompilerGenerated]
		private set
		{
			hashtable_0 = value;
		}
	}

	public Hashtable Hashtable_1
	{
		get
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Merge(Hashtable_0);
			hashtable[byte.MaxValue] = String_0;
			return hashtable;
		}
	}

	public PhotonPlayer(bool bool_1, int int_1, string string_1)
	{
		Hashtable_0 = new Hashtable();
		bool_0 = bool_1;
		int_0 = int_1;
		string_0 = string_1;
	}

	protected internal PhotonPlayer(bool bool_1, int int_1, Hashtable hashtable_1)
	{
		Hashtable_0 = new Hashtable();
		bool_0 = bool_1;
		int_0 = int_1;
		InternalCacheProperties(hashtable_1);
	}

	public override bool Equals(object obj)
	{
		PhotonPlayer photonPlayer = obj as PhotonPlayer;
		return photonPlayer != null && GetHashCode() == photonPlayer.GetHashCode();
	}

	public override int GetHashCode()
	{
		return Int32_0;
	}

	internal void InternalChangeLocalID(int int_1)
	{
		if (!bool_0)
		{
			Debug.LogError("ERROR You should never change PhotonPlayer IDs!");
		}
		else
		{
			int_0 = int_1;
		}
	}

	internal void InternalCacheProperties(Hashtable hashtable_1)
	{
		if (hashtable_1 != null && hashtable_1.Count != 0 && !Hashtable_0.Equals(hashtable_1))
		{
			if (hashtable_1.ContainsKey(byte.MaxValue))
			{
				string_0 = (string)hashtable_1[byte.MaxValue];
			}
			Hashtable_0.MergeStringKeys(hashtable_1);
			Hashtable_0.StripKeysWithNullValues();
		}
	}

	public void SetCustomProperties(Hashtable hashtable_1)
	{
		if (hashtable_1 != null)
		{
			Hashtable_0.MergeStringKeys(hashtable_1);
			Hashtable_0.StripKeysWithNullValues();
			Hashtable hashtable = hashtable_1.StripToStringKeys();
			if (int_0 > 0 && !PhotonNetwork.Boolean_3)
			{
				PhotonNetwork.networkingPeer_0.OpSetCustomPropertiesOfActor(int_0, hashtable, true, 0);
			}
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerPropertiesChanged, this, hashtable_1);
		}
	}

	public static PhotonPlayer Find(int int_1)
	{
		int num = 0;
		PhotonPlayer photonPlayer;
		while (true)
		{
			if (num < PhotonNetwork.PhotonPlayer_2.Length)
			{
				photonPlayer = PhotonNetwork.PhotonPlayer_2[num];
				if (photonPlayer.Int32_0 == int_1)
				{
					break;
				}
				num++;
				continue;
			}
			return null;
		}
		return photonPlayer;
	}

	public PhotonPlayer Get(int int_1)
	{
		return Find(int_1);
	}

	public PhotonPlayer GetNext()
	{
		return GetNextFor(Int32_0);
	}

	public PhotonPlayer GetNextFor(PhotonPlayer photonPlayer_0)
	{
		if (photonPlayer_0 == null)
		{
			return null;
		}
		return GetNextFor(photonPlayer_0.Int32_0);
	}

	public PhotonPlayer GetNextFor(int int_1)
	{
		if (PhotonNetwork.networkingPeer_0 != null && PhotonNetwork.networkingPeer_0.dictionary_0 != null && PhotonNetwork.networkingPeer_0.dictionary_0.Count >= 2)
		{
			Dictionary<int, PhotonPlayer> dictionary_ = PhotonNetwork.networkingPeer_0.dictionary_0;
			int num = int.MaxValue;
			int num2 = int_1;
			foreach (int key in dictionary_.Keys)
			{
				if (key < num2)
				{
					num2 = key;
				}
				else if (key > int_1 && key < num)
				{
					num = key;
				}
			}
			return (num == int.MaxValue) ? dictionary_[num2] : dictionary_[num];
		}
		return null;
	}

	public override string ToString()
	{
		if (string.IsNullOrEmpty(String_0))
		{
			return string.Format("#{0:00}{1}", Int32_0, (!Boolean_0) ? string.Empty : "(master)");
		}
		return string.Format("'{0}'{1}", String_0, (!Boolean_0) ? string.Empty : "(master)");
	}

	public string ToStringFull()
	{
		return string.Format("#{0:00} '{1}' {2}", Int32_0, String_0, Hashtable_0.ToStringFull());
	}
}
