using System.Collections.Generic;
using System.Text;
using Rilisoft.MiniJson;

namespace engine.helpers
{
	public class DictionarySerialize
	{
		public Dictionary<string, object> dictionary_0 = new Dictionary<string, object>();

		public object this[int key]
		{
			get
			{
				return this[key.ToString()];
			}
			set
			{
				Add(key.ToString(), value);
			}
		}

		public object this[string key]
		{
			get
			{
				if (dictionary_0 == null)
				{
					Log.AddLine("[DictionarySerialize::this[string key]. Data is null???]");
					return null;
				}
				if (dictionary_0.ContainsKey(key))
				{
					return dictionary_0[key];
				}
				return null;
			}
			set
			{
				Add(key, value);
			}
		}

		public void Add(string string_0, object object_0)
		{
			if (dictionary_0 == null)
			{
				Log.AddLine("[DictionarySerialize::Add. Data is null???]");
			}
			else if (dictionary_0.ContainsKey(string_0))
			{
				dictionary_0[string_0] = object_0;
			}
			else
			{
				dictionary_0.Add(string_0, object_0);
			}
		}

		public void Remove(string string_0)
		{
			if (dictionary_0 == null)
			{
				Log.AddLine("[DictionarySerialize::Remove. Data is null???]");
			}
			else
			{
				dictionary_0.Remove(string_0);
			}
		}

		public string UrlEncoding()
		{
			if (dictionary_0 == null)
			{
				Log.AddLine("[DictionarySerialize::UrlEncoding. Data is null???]");
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, object> item in dictionary_0)
			{
				stringBuilder.Append(string.Concat(item.Key, "=", item.Value, "&"));
			}
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
			return stringBuilder.ToString();
		}

		public void ParseJSON(string string_0)
		{
			if (!string.IsNullOrEmpty(string_0))
			{
				dictionary_0 = Json.Deserialize(string_0) as Dictionary<string, object>;
			}
		}

		public string ToJSON()
		{
			if (dictionary_0 == null)
			{
				Log.AddLine("[DictionarySerialize::ToJSON. Data is null???]");
				return string.Empty;
			}
			return Json.Serialize(dictionary_0);
		}

		public byte[] ToUrlBytes()
		{
			string s = UrlEncoding();
			return Encoding.UTF8.GetBytes(s);
		}

		public override string ToString()
		{
			if (dictionary_0 == null)
			{
				Log.AddLine("[DictionarySerialize::ToString. Data is null???]");
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Indexer has ");
			stringBuilder.Append(dictionary_0.Count.ToString());
			stringBuilder.AppendLine(" entries:");
			foreach (KeyValuePair<string, object> item in dictionary_0)
			{
				stringBuilder.Append(item.Key);
				stringBuilder.Append(": ");
				stringBuilder.AppendLine(item.Value.ToString());
			}
			return stringBuilder.ToString();
		}

		public void Clear()
		{
			if (dictionary_0 == null)
			{
				Log.AddLine("[DictionarySerialize::Clear. Data is null???]");
			}
			else
			{
				dictionary_0.Clear();
			}
		}
	}
}
