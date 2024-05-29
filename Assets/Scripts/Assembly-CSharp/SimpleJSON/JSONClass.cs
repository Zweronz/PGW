using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SimpleJSON
{
	public class JSONClass : JSONNode, IEnumerable
	{
		private Dictionary<string, JSONNode> dictionary_0 = new Dictionary<string, JSONNode>();

		public override JSONNode this[string aKey]
		{
			get
			{
				if (dictionary_0.ContainsKey(aKey))
				{
					return dictionary_0[aKey];
				}
				return new JSONLazyCreator(this, aKey);
			}
			set
			{
				if (dictionary_0.ContainsKey(aKey))
				{
					dictionary_0[aKey] = value;
				}
				else
				{
					dictionary_0.Add(aKey, value);
				}
			}
		}

		public override JSONNode this[int aIndex]
		{
			get
			{
				if (aIndex >= 0 && aIndex < dictionary_0.Count)
				{
					return dictionary_0.ElementAt(aIndex).Value;
				}
				return null;
			}
			set
			{
				if (aIndex >= 0 && aIndex < dictionary_0.Count)
				{
					string key = dictionary_0.ElementAt(aIndex).Key;
					dictionary_0[key] = value;
				}
			}
		}

		public override int Int32_0
		{
			get
			{
				return dictionary_0.Count;
			}
		}

		public override IEnumerable<JSONNode> Prop_0
		{
			get
			{
				Dictionary<string, JSONNode>.Enumerator enumerator = dictionary_0.GetEnumerator();
				/*Error near IL_003c: Could not find block for branch target IL_004a*/;
				yield break;
			}
		}

		public override void Add(string string_0, JSONNode jsonnode_0)
		{
			if (!string.IsNullOrEmpty(string_0))
			{
				if (dictionary_0.ContainsKey(string_0))
				{
					dictionary_0[string_0] = jsonnode_0;
				}
				else
				{
					dictionary_0.Add(string_0, jsonnode_0);
				}
			}
			else
			{
				dictionary_0.Add(Guid.NewGuid().ToString(), jsonnode_0);
			}
		}

		public override JSONNode Remove(string string_0)
		{
			if (!dictionary_0.ContainsKey(string_0))
			{
				return null;
			}
			JSONNode result = dictionary_0[string_0];
			dictionary_0.Remove(string_0);
			return result;
		}

		public override JSONNode Remove(int int_0)
		{
			if (int_0 >= 0 && int_0 < dictionary_0.Count)
			{
				KeyValuePair<string, JSONNode> keyValuePair = dictionary_0.ElementAt(int_0);
				dictionary_0.Remove(keyValuePair.Key);
				return keyValuePair.Value;
			}
			return null;
		}

		public override JSONNode Remove(JSONNode jsonnode_0)
		{
			try
			{
				KeyValuePair<string, JSONNode> keyValuePair = dictionary_0.Where((KeyValuePair<string, JSONNode> keyValuePair_0) => keyValuePair_0.Value == jsonnode_0).First();
				dictionary_0.Remove(keyValuePair.Key);
				return jsonnode_0;
			}
			catch
			{
				return null;
			}
		}

		public IEnumerator GetEnumerator()
		{
			Dictionary<string, JSONNode>.Enumerator enumerator = dictionary_0.GetEnumerator();
			/*Error near IL_003c: Could not find block for branch target IL_004a*/;
			yield break;
		}

		public override string ToString()
		{
			string text = "{";
			foreach (KeyValuePair<string, JSONNode> item in dictionary_0)
			{
				if (text.Length > 2)
				{
					text += ", ";
				}
				string text2 = text;
				text = text2 + "\"" + JSONNode.Escape(item.Key) + "\":" + item.Value.ToString();
			}
			return text + "}";
		}

		public override string ToString(string string_0)
		{
			string text = "{ ";
			foreach (KeyValuePair<string, JSONNode> item in dictionary_0)
			{
				if (text.Length > 3)
				{
					text += ", ";
				}
				text = text + "\n" + string_0 + "   ";
				string text2 = text;
				text = text2 + "\"" + JSONNode.Escape(item.Key) + "\" : " + item.Value.ToString(string_0 + "   ");
			}
			return text + "\n" + string_0 + "}";
		}

		public override void Serialize(BinaryWriter binaryWriter_0)
		{
			binaryWriter_0.Write((byte)2);
			binaryWriter_0.Write(dictionary_0.Count);
			foreach (string key in dictionary_0.Keys)
			{
				binaryWriter_0.Write(key);
				dictionary_0[key].Serialize(binaryWriter_0);
			}
		}
	}
}
