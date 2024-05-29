using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using BestHTTP.JSON;
using BestHTTP.SocketIO.JsonEncoders;

namespace BestHTTP.SocketIO
{
	public sealed class Packet
	{
		private enum PayloadTypes : byte
		{
			Textual = 0,
			Binary = 1
		}

		private const string string_0 = "_placeholder";

		private List<byte[]> list_0;

		[CompilerGenerated]
		private TransportEventTypes transportEventTypes_0;

		[CompilerGenerated]
		private SocketIOEventTypes socketIOEventTypes_0;

		[CompilerGenerated]
		private int int_0;

		[CompilerGenerated]
		private int int_1;

		[CompilerGenerated]
		private string string_1;

		[CompilerGenerated]
		private string string_2;

		[CompilerGenerated]
		private string string_3;

		[CompilerGenerated]
		private bool bool_0;

		[CompilerGenerated]
		private object[] object_0;

		public TransportEventTypes TransportEventTypes_0
		{
			[CompilerGenerated]
			get
			{
				return transportEventTypes_0;
			}
			[CompilerGenerated]
			private set
			{
				transportEventTypes_0 = value;
			}
		}

		public SocketIOEventTypes SocketIOEventTypes_0
		{
			[CompilerGenerated]
			get
			{
				return socketIOEventTypes_0;
			}
			[CompilerGenerated]
			private set
			{
				socketIOEventTypes_0 = value;
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

		public int Int32_1
		{
			[CompilerGenerated]
			get
			{
				return int_1;
			}
			[CompilerGenerated]
			private set
			{
				int_1 = value;
			}
		}

		public string String_0
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

		public string String_1
		{
			[CompilerGenerated]
			get
			{
				return string_2;
			}
			[CompilerGenerated]
			private set
			{
				string_2 = value;
			}
		}

		public string String_2
		{
			[CompilerGenerated]
			get
			{
				return string_3;
			}
			[CompilerGenerated]
			private set
			{
				string_3 = value;
			}
		}

		public List<byte[]> List_0
		{
			get
			{
				return list_0;
			}
			set
			{
				list_0 = value;
				Int32_0 = ((list_0 != null) ? list_0.Count : 0);
			}
		}

		public bool Boolean_0
		{
			get
			{
				return List_0 != null && List_0.Count == Int32_0;
			}
		}

		public bool Boolean_1
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

		public object[] Object_0
		{
			[CompilerGenerated]
			get
			{
				return object_0;
			}
			[CompilerGenerated]
			private set
			{
				object_0 = value;
			}
		}

		internal Packet()
		{
			TransportEventTypes_0 = TransportEventTypes.Unknown;
			SocketIOEventTypes_0 = SocketIOEventTypes.Unknown;
			String_1 = string.Empty;
		}

		internal Packet(string string_4)
		{
			Parse(string_4);
		}

		internal Packet(TransportEventTypes transportEventTypes_1, SocketIOEventTypes socketIOEventTypes_1, string string_4, string string_5, int int_2 = 0, int int_3 = 0)
		{
			TransportEventTypes_0 = transportEventTypes_1;
			SocketIOEventTypes_0 = socketIOEventTypes_1;
			String_0 = string_4;
			String_1 = string_5;
			Int32_0 = int_2;
			Int32_1 = int_3;
		}

		public object[] Decode(IJsonEncoder ijsonEncoder_0)
		{
			if (!Boolean_1 && ijsonEncoder_0 != null)
			{
				Boolean_1 = true;
				if (string.IsNullOrEmpty(String_1))
				{
					return Object_0;
				}
				List<object> list = ijsonEncoder_0.Decode(String_1);
				if (list != null && list.Count > 0)
				{
					list.RemoveAt(0);
					Object_0 = list.ToArray();
				}
				return Object_0;
			}
			return Object_0;
		}

		public string DecodeEventName()
		{
			if (!string.IsNullOrEmpty(String_2))
			{
				return String_2;
			}
			if (string.IsNullOrEmpty(String_1))
			{
				return string.Empty;
			}
			if (String_1[0] != '[')
			{
				return string.Empty;
			}
			int i;
			for (i = 1; String_1.Length > i && String_1[i] != '"' && String_1[i] != '\''; i++)
			{
			}
			if (String_1.Length <= i)
			{
				return string.Empty;
			}
			int num = ++i;
			for (; String_1.Length > i && String_1[i] != '"' && String_1[i] != '\''; i++)
			{
			}
			if (String_1.Length <= i)
			{
				return string.Empty;
			}
			return String_2 = String_1.Substring(num, i - num);
		}

		public string RemoveEventName(bool bool_1)
		{
			if (string.IsNullOrEmpty(String_1))
			{
				return string.Empty;
			}
			if (String_1[0] != '[')
			{
				return string.Empty;
			}
			int i;
			for (i = 1; String_1.Length > i && String_1[i] != '"' && String_1[i] != '\''; i++)
			{
			}
			if (String_1.Length <= i)
			{
				return string.Empty;
			}
			int num = i;
			for (; String_1.Length > i && String_1[i] != ',' && String_1[i] != ']'; i++)
			{
			}
			if (String_1.Length <= ++i)
			{
				return string.Empty;
			}
			string text = String_1.Remove(num, i - num);
			if (bool_1)
			{
				text = text.Substring(1, text.Length - 2);
			}
			return text;
		}

		public bool ReconstructAttachmentAsIndex()
		{
			return PlaceholderReplacer(delegate(string string_4, Dictionary<string, object> dictionary_0)
			{
				int num = Convert.ToInt32(dictionary_0["num"]);
				String_1 = String_1.Replace(string_4, num.ToString());
				Boolean_1 = false;
			});
		}

		public bool ReconstructAttachmentAsBase64()
		{
			if (!Boolean_0)
			{
				return false;
			}
			return PlaceholderReplacer(delegate(string string_4, Dictionary<string, object> dictionary_0)
			{
				int index = Convert.ToInt32(dictionary_0["num"]);
				String_1 = String_1.Replace(string_4, string.Format("\"{0}\"", Convert.ToBase64String(List_0[index])));
				Boolean_1 = false;
			});
		}

		internal void Parse(string string_4)
		{
			int num = 0;
			num = 1;
			TransportEventTypes_0 = (TransportEventTypes)char.GetNumericValue(string_4, 0);
			if (string_4.Length > 1 && char.GetNumericValue(string_4, num) >= 0.0)
			{
				SocketIOEventTypes_0 = (SocketIOEventTypes)char.GetNumericValue(string_4, num++);
			}
			else
			{
				SocketIOEventTypes_0 = SocketIOEventTypes.Unknown;
			}
			if (SocketIOEventTypes_0 == SocketIOEventTypes.BinaryEvent || SocketIOEventTypes_0 == SocketIOEventTypes.BinaryAck)
			{
				int num2 = string_4.IndexOf('-', num);
				if (num2 == -1)
				{
					num2 = string_4.Length;
				}
				int result = 0;
				int.TryParse(string_4.Substring(num, num2 - num), out result);
				Int32_0 = result;
				num = num2 + 1;
			}
			if (string_4.Length > num && string_4[num] == '/')
			{
				int num3 = string_4.IndexOf(',', num);
				if (num3 == -1)
				{
					num3 = string_4.Length;
				}
				String_0 = string_4.Substring(num, num3 - num);
				num = num3 + 1;
			}
			else
			{
				String_0 = "/";
			}
			if (string_4.Length > num && char.GetNumericValue(string_4[num]) >= 0.0)
			{
				int num4 = num++;
				for (; string_4.Length > num && !(char.GetNumericValue(string_4[num]) < 0.0); num++)
				{
				}
				int result2 = 0;
				int.TryParse(string_4.Substring(num4, num - num4), out result2);
				Int32_1 = result2;
			}
			if (string_4.Length > num)
			{
				String_1 = string_4.Substring(num);
			}
			else
			{
				String_1 = string.Empty;
			}
		}

		internal string Encode()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (TransportEventTypes_0 == TransportEventTypes.Unknown && Int32_0 > 0)
			{
				TransportEventTypes_0 = TransportEventTypes.Message;
			}
			if (TransportEventTypes_0 != TransportEventTypes.Unknown)
			{
				stringBuilder.Append(((int)TransportEventTypes_0).ToString());
			}
			if (SocketIOEventTypes_0 == SocketIOEventTypes.Unknown && Int32_0 > 0)
			{
				SocketIOEventTypes_0 = SocketIOEventTypes.BinaryEvent;
			}
			if (SocketIOEventTypes_0 != SocketIOEventTypes.Unknown)
			{
				stringBuilder.Append(((int)SocketIOEventTypes_0).ToString());
			}
			if (SocketIOEventTypes_0 == SocketIOEventTypes.BinaryEvent || SocketIOEventTypes_0 == SocketIOEventTypes.BinaryAck)
			{
				stringBuilder.Append(Int32_0.ToString());
				stringBuilder.Append("-");
			}
			bool flag = false;
			if (String_0 != "/")
			{
				stringBuilder.Append(String_0);
				flag = true;
			}
			if (Int32_1 != 0)
			{
				if (flag)
				{
					stringBuilder.Append(",");
					flag = false;
				}
				stringBuilder.Append(Int32_1.ToString());
			}
			if (!string.IsNullOrEmpty(String_1))
			{
				if (flag)
				{
					stringBuilder.Append(",");
					flag = false;
				}
				stringBuilder.Append(String_1);
			}
			return stringBuilder.ToString();
		}

		internal byte[] EncodeBinary()
		{
			if (Int32_0 != 0 || (List_0 != null && List_0.Count != 0))
			{
				if (List_0 == null)
				{
					throw new ArgumentException("packet.Attachments are null!");
				}
				if (Int32_0 != List_0.Count)
				{
					throw new ArgumentException("packet.AttachmentCount != packet.Attachments.Count. Use the packet.AddAttachment function to add data to a packet!");
				}
			}
			string s = Encode();
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			byte[] array = EncodeData(bytes, PayloadTypes.Textual, null);
			if (Int32_0 != 0)
			{
				int num = array.Length;
				List<byte[]> list = new List<byte[]>(Int32_0);
				int num2 = 0;
				for (int i = 0; i < Int32_0; i++)
				{
					byte[] array2 = EncodeData(List_0[i], PayloadTypes.Binary, new byte[1] { 4 });
					list.Add(array2);
					num2 += array2.Length;
				}
				Array.Resize(ref array, array.Length + num2);
				for (int j = 0; j < Int32_0; j++)
				{
					byte[] array3 = list[j];
					Array.Copy(array3, 0, array, num, array3.Length);
					num += array3.Length;
				}
			}
			return array;
		}

		internal void AddAttachmentFromServer(byte[] byte_0, bool bool_1)
		{
			if (byte_0 != null && byte_0.Length != 0)
			{
				if (list_0 == null)
				{
					list_0 = new List<byte[]>(Int32_0);
				}
				if (bool_1)
				{
					List_0.Add(byte_0);
					return;
				}
				byte[] array = new byte[byte_0.Length - 1];
				Array.Copy(byte_0, 1, array, 0, byte_0.Length - 1);
				List_0.Add(array);
			}
		}

		private byte[] EncodeData(byte[] byte_0, PayloadTypes payloadTypes_0, byte[] byte_1)
		{
			int num = ((byte_1 != null) ? byte_1.Length : 0);
			string text = (byte_0.Length + num).ToString();
			byte[] array = new byte[text.Length];
			for (int i = 0; i < text.Length; i++)
			{
				array[i] = (byte)char.GetNumericValue(text[i]);
			}
			byte[] array2 = new byte[byte_0.Length + array.Length + 2 + num];
			array2[0] = (byte)payloadTypes_0;
			for (int j = 0; j < array.Length; j++)
			{
				array2[1 + j] = array[j];
			}
			int num2 = 1 + array.Length;
			array2[num2++] = byte.MaxValue;
			if (byte_1 != null && byte_1.Length > 0)
			{
				Array.Copy(byte_1, 0, array2, num2, byte_1.Length);
				num2 += byte_1.Length;
			}
			Array.Copy(byte_0, 0, array2, num2, byte_0.Length);
			return array2;
		}

		private bool PlaceholderReplacer(Action<string, Dictionary<string, object>> action_0)
		{
			if (string.IsNullOrEmpty(String_1))
			{
				return false;
			}
			int num = String_1.IndexOf("_placeholder");
			while (true)
			{
				if (num >= 0)
				{
					int num2 = num;
					while (String_1[num2] != '{')
					{
						num2--;
					}
					int i;
					for (i = num; String_1.Length > i && String_1[i] != '}'; i++)
					{
					}
					if (String_1.Length <= i)
					{
						break;
					}
					string arg = String_1.Substring(num2, i - num2 + 1);
					bool flag = false;
					Dictionary<string, object> dictionary = Json.Decode(arg, ref flag) as Dictionary<string, object>;
					if (flag)
					{
						object value;
						if (dictionary.TryGetValue("_placeholder", out value) && (bool)value)
						{
							if (dictionary.TryGetValue("num", out value))
							{
								action_0(arg, dictionary);
								num = String_1.IndexOf("_placeholder");
								continue;
							}
							return false;
						}
						return false;
					}
					return false;
				}
				return true;
			}
			return false;
		}

		public override string ToString()
		{
			return String_1;
		}

		internal Packet Clone()
		{
			Packet packet = new Packet(TransportEventTypes_0, SocketIOEventTypes_0, String_0, String_1, 0, Int32_1);
			packet.String_2 = String_2;
			packet.Int32_0 = Int32_0;
			packet.list_0 = list_0;
			return packet;
		}
	}
}
