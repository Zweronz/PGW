using System.Runtime.CompilerServices;
using UnityEngine;
using engine.helpers;
using engine.protobuf;

namespace engine.filesystem
{
	public class CacheResources
	{
		private DictionaryProtoSerialize dictionaryProtoSerialize_0;

		[CompilerGenerated]
		private bool bool_0;

		public bool Boolean_0
		{
			[CompilerGenerated]
			get
			{
				return bool_0;
			}
			[CompilerGenerated]
			private set
			{
				bool_0 = value;
			}
		}

		public byte[] Byte_0
		{
			get
			{
				return dictionaryProtoSerialize_0.Serialize();
			}
		}

		public object this[string key]
		{
			get
			{
				return dictionaryProtoSerialize_0[key];
			}
		}

		public CacheResources(byte[] byte_0, bool bool_1)
		{
			Boolean_0 = bool_1;
			dictionaryProtoSerialize_0 = DictionaryProtoSerialize.Create(byte_0);
			if (Application.isPlaying)
			{
				Log.AddLine(ToString());
			}
			else
			{
				Debug.Log(ToString());
			}
		}

		public bool Contains(FileName fileName_0)
		{
			return dictionaryProtoSerialize_0[fileName_0.String_1] != null && dictionaryProtoSerialize_0[fileName_0.String_1].Object_0.ToString() == fileName_0.String_0;
		}

		public bool Contains(string string_0)
		{
			return dictionaryProtoSerialize_0[string_0] != null;
		}

		public void Add(FileName fileName_0)
		{
			dictionaryProtoSerialize_0[fileName_0.String_1] = WrapObjForProtobuf.Create(fileName_0.String_0);
		}

		public override string ToString()
		{
			return string.Format("Cache: \n in resources: {0}, \n data: {1}", Boolean_0, ObjectParser.ObjectToString(dictionaryProtoSerialize_0));
		}

		public void Clear()
		{
			dictionaryProtoSerialize_0.Clear();
		}
	}
}
