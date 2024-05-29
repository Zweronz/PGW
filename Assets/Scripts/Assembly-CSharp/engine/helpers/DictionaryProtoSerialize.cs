using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using ProtoBuf;
using engine.protobuf;

namespace engine.helpers
{
	public class DictionaryProtoSerialize
	{
		[ProtoContract]
		internal class DictionaryProtoData
		{
			[ProtoMember(1)]
			public Dictionary<string, WrapObjForProtobuf> dictionary_0 = new Dictionary<string, WrapObjForProtobuf>();
		}

		internal DictionaryProtoData dictionaryProtoData_0 = new DictionaryProtoData();

		private MethodInfo methodInfo_0;

		public WrapObjForProtobuf this[int key]
		{
			get
			{
				return this[key.ToString()];
			}
			set
			{
				AddRfl(key.ToString(), value);
			}
		}

		public WrapObjForProtobuf this[string key]
		{
			get
			{
				if (dictionaryProtoData_0.dictionary_0.ContainsKey(key))
				{
					return dictionaryProtoData_0.dictionary_0[key];
				}
				return null;
			}
			set
			{
				AddRfl(key, value);
			}
		}

		public DictionaryProtoSerialize()
		{
			methodInfo_0 = GetType().GetMethod("Add");
		}

		public static DictionaryProtoSerialize Create(byte[] byte_0)
		{
			DictionaryProtoSerialize dictionaryProtoSerialize = new DictionaryProtoSerialize();
			if (byte_0 != null && byte_0.Length != 0)
			{
				dictionaryProtoSerialize.Deserialize(byte_0);
			}
			return dictionaryProtoSerialize;
		}

		public void Add<T>(string string_0, WrapObjForProtobuf<T> wrapObjForProtobuf_0)
		{
			if (dictionaryProtoData_0.dictionary_0.ContainsKey(string_0))
			{
				dictionaryProtoData_0.dictionary_0[string_0] = wrapObjForProtobuf_0;
			}
			else
			{
				dictionaryProtoData_0.dictionary_0.Add(string_0, wrapObjForProtobuf_0);
			}
		}

		public void Remove(string string_0)
		{
			dictionaryProtoData_0.dictionary_0.Remove(string_0);
		}

		public string UrlEncoding()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, WrapObjForProtobuf> item in dictionaryProtoData_0.dictionary_0)
			{
				stringBuilder.Append(string.Concat(item.Key, "=", item.Value.Object_0, "&"));
			}
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
			return stringBuilder.ToString();
		}

		public byte[] ToUrlBytes()
		{
			string s = UrlEncoding();
			return Encoding.UTF8.GetBytes(s);
		}

		public byte[] Serialize()
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Serializer.Serialize(memoryStream, dictionaryProtoData_0.dictionary_0);
				return memoryStream.ToArray();
			}
		}

		public void Deserialize(byte[] byte_0)
		{
			dictionaryProtoData_0 = Serializer.Deserialize<DictionaryProtoData>(new MemoryStream(byte_0));
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Indexer has ");
			stringBuilder.Append(dictionaryProtoData_0.dictionary_0.Count.ToString());
			stringBuilder.AppendLine(" entries:");
			foreach (KeyValuePair<string, WrapObjForProtobuf> item in dictionaryProtoData_0.dictionary_0)
			{
				stringBuilder.Append(item.Key);
				stringBuilder.Append(": ");
				stringBuilder.AppendLine(item.Value.Object_0.ToString());
			}
			return stringBuilder.ToString();
		}

		public void Clear()
		{
			dictionaryProtoData_0.dictionary_0.Clear();
		}

		private void AddRfl(string string_0, WrapObjForProtobuf wrapObjForProtobuf_0)
		{
			Type type = wrapObjForProtobuf_0.GetType();
			if (!type.IsGenericType)
			{
				throw new ArgumentException("value argument must be generic");
			}
			Type[] genericArguments = type.GetGenericArguments();
			MethodInfo methodInfo = methodInfo_0.MakeGenericMethod(genericArguments[0]);
			methodInfo.Invoke(this, new object[2] { string_0, wrapObjForProtobuf_0 });
		}
	}
}
