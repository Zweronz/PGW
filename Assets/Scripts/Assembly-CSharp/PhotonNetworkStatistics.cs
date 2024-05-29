using System.Collections.Generic;
using System.Text;
using ProtoBuf;
using UnityEngine;

[ProtoContract]
public sealed class PhotonNetworkStatistics
{
	[ProtoMember(1)]
	public Dictionary<int, int> dictionary_0 = new Dictionary<int, int>();

	[ProtoMember(2)]
	public int int_0;

	[ProtoMember(3)]
	public int int_1;

	private int int_2;

	public int Int32_0
	{
		get
		{
			return int_2;
		}
	}

	public void Reset()
	{
		dictionary_0.Clear();
		int_0 = 0;
		int_1 = 0;
	}

	public void SetPing(int int_3, int int_4)
	{
		int_2 = PhotonNetwork.GetPing();
		if (!dictionary_0.ContainsKey(int_2))
		{
			dictionary_0.Add(int_2, 0);
		}
		Dictionary<int, int> dictionary;
		Dictionary<int, int> dictionary2 = (dictionary = dictionary_0);
		int key;
		int key2 = (key = int_2);
		key = dictionary[key];
		dictionary2[key2] = key + 1;
		int_0 = Mathf.Max(int_0, int_3);
		int_1 = Mathf.Max(int_1, int_4);
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("[Ping statistics]:");
		foreach (KeyValuePair<int, int> item in dictionary_0)
		{
			stringBuilder.AppendFormat("----[ping: {0}, seconds: {1}]\n", item.Key, item.Value);
		}
		stringBuilder.AppendLine("[LongestDeltaBetweenSending]: " + int_0);
		stringBuilder.AppendLine("[LongestDeltaBetweenDispatching]: " + int_1);
		return stringBuilder.ToString();
	}
}
