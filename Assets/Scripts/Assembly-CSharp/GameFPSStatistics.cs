using System.Collections.Generic;
using System.Text;
using ProtoBuf;

[ProtoContract]
public sealed class GameFPSStatistics
{
	[ProtoMember(1)]
	public Dictionary<int, int> dictionary_0 = new Dictionary<int, int>();

	public void Reset()
	{
		dictionary_0.Clear();
	}

	public void SetFPS(int int_0)
	{
		if (!dictionary_0.ContainsKey(int_0))
		{
			dictionary_0.Add(int_0, 0);
		}
		Dictionary<int, int> dictionary;
		Dictionary<int, int> dictionary2 = (dictionary = dictionary_0);
		int key;
		int key2 = (key = int_0);
		key = dictionary[key];
		dictionary2[key2] = key + 1;
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("[FPS statistics]:");
		foreach (KeyValuePair<int, int> item in dictionary_0)
		{
			stringBuilder.AppendFormat("----[fps: {0}, seconds: {1}]\n", item.Key, item.Value);
		}
		return stringBuilder.ToString();
	}
}
