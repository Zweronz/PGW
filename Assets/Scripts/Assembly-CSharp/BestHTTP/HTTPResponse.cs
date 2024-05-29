using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using BestHTTP.Caching;
using BestHTTP.Cookies;
using BestHTTP.Decompression.Zlib;
using BestHTTP.Extensions;
using UnityEngine;

namespace BestHTTP
{
	public class HTTPResponse : IDisposable
	{
		internal const byte byte_0 = 13;

		internal const byte byte_1 = 10;

		public const int int_0 = 4096;

		protected string string_0;

		protected Texture2D texture2D_0;

		internal HTTPRequest httprequest_0;

		protected Stream stream_0;

		protected List<byte[]> list_0;

		protected object object_0 = new object();

		protected byte[] byte_2;

		protected int int_1;

		protected Stream stream_1;

		protected int int_2;

		[CompilerGenerated]
		private int int_3;

		[CompilerGenerated]
		private int int_4;

		[CompilerGenerated]
		private int int_5;

		[CompilerGenerated]
		private string string_1;

		[CompilerGenerated]
		private bool bool_0;

		[CompilerGenerated]
		private bool bool_1;

		[CompilerGenerated]
		private bool bool_2;

		[CompilerGenerated]
		private Dictionary<string, List<string>> dictionary_0;

		[CompilerGenerated]
		private byte[] byte_3;

		[CompilerGenerated]
		private bool bool_3;

		[CompilerGenerated]
		private List<Cookie> list_1;

		[CompilerGenerated]
		private bool bool_4;

		[CompilerGenerated]
		private static Dictionary<string, int> dictionary_1;

		public int Int32_0
		{
			[CompilerGenerated]
			get
			{
				return int_3;
			}
			[CompilerGenerated]
			protected set
			{
				int_3 = value;
			}
		}

		public int Int32_1
		{
			[CompilerGenerated]
			get
			{
				return int_4;
			}
			[CompilerGenerated]
			protected set
			{
				int_4 = value;
			}
		}

		public int Int32_2
		{
			[CompilerGenerated]
			get
			{
				return int_5;
			}
			[CompilerGenerated]
			protected set
			{
				int_5 = value;
			}
		}

		public bool Boolean_0
		{
			get
			{
				return (Int32_2 >= 200 && Int32_2 < 300) || Int32_2 == 304;
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
			protected set
			{
				string_1 = value;
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
			protected set
			{
				bool_0 = value;
			}
		}

		public bool Boolean_2
		{
			[CompilerGenerated]
			get
			{
				return bool_1;
			}
			[CompilerGenerated]
			internal set
			{
				bool_1 = value;
			}
		}

		public bool Boolean_3
		{
			[CompilerGenerated]
			get
			{
				return bool_2;
			}
			[CompilerGenerated]
			internal set
			{
				bool_2 = value;
			}
		}

		public Dictionary<string, List<string>> Dictionary_0
		{
			[CompilerGenerated]
			get
			{
				return dictionary_0;
			}
			[CompilerGenerated]
			protected set
			{
				dictionary_0 = value;
			}
		}

		public byte[] Byte_0
		{
			[CompilerGenerated]
			get
			{
				return byte_3;
			}
			[CompilerGenerated]
			internal set
			{
				byte_3 = value;
			}
		}

		public bool Boolean_4
		{
			[CompilerGenerated]
			get
			{
				return bool_3;
			}
			[CompilerGenerated]
			protected set
			{
				bool_3 = value;
			}
		}

		public List<Cookie> List_0
		{
			[CompilerGenerated]
			get
			{
				return list_1;
			}
			[CompilerGenerated]
			internal set
			{
				list_1 = value;
			}
		}

		public string String_1
		{
			get
			{
				if (Byte_0 == null)
				{
					return string.Empty;
				}
				if (!string.IsNullOrEmpty(string_0))
				{
					return string_0;
				}
				return string_0 = Encoding.UTF8.GetString(Byte_0, 0, Byte_0.Length);
			}
		}

		public Texture2D Texture2D_0
		{
			get
			{
				if (Byte_0 == null)
				{
					return null;
				}
				if (texture2D_0 != null)
				{
					return texture2D_0;
				}
				texture2D_0 = new Texture2D(0, 0);
				texture2D_0.LoadImage(Byte_0);
				return texture2D_0;
			}
		}

		public bool Boolean_5
		{
			[CompilerGenerated]
			get
			{
				return bool_4;
			}
			[CompilerGenerated]
			protected set
			{
				bool_4 = value;
			}
		}

		internal HTTPResponse(HTTPRequest httprequest_1, Stream stream_2, bool bool_5, bool bool_6)
		{
			httprequest_0 = httprequest_1;
			stream_0 = stream_2;
			Boolean_1 = bool_5;
			Boolean_3 = bool_6;
			Boolean_5 = false;
		}

		internal virtual bool Receive(int int_6 = -1, bool bool_5 = true)
		{
			string empty = string.Empty;
			try
			{
				empty = ReadTo(stream_0, 32);
			}
			catch
			{
				if (httprequest_0.Boolean_5)
				{
					throw;
				}
				return false;
			}
			if (!httprequest_0.Boolean_5 && string.IsNullOrEmpty(empty))
			{
				return false;
			}
			string[] array = empty.Split('/', '.');
			Int32_0 = int.Parse(array[1]);
			Int32_1 = int.Parse(array[2]);
			string text = NoTrimReadTo(stream_0, 32, 10);
			int result;
			if (httprequest_0.Boolean_5)
			{
				result = int.Parse(text);
			}
			else if (!int.TryParse(text, out result))
			{
				return false;
			}
			Int32_2 = result;
			if (text.Length > 0 && (byte)text[text.Length - 1] != 10 && (byte)text[text.Length - 1] != 13)
			{
				String_0 = ReadTo(stream_0, 10);
			}
			else
			{
				String_0 = string.Empty;
			}
			ReadHeaders(stream_0);
			Boolean_4 = Int32_2 == 101 && (HasHeaderWithValue("connection", "upgrade") || HasHeader("upgrade"));
			if (!bool_5)
			{
				return true;
			}
			return ReadPayload(int_6);
		}

		protected bool ReadPayload(int int_6)
		{
			if (int_6 != -1)
			{
				Boolean_3 = true;
				ReadRaw(stream_0, int_6);
				return true;
			}
			if ((Int32_2 < 100 || Int32_2 >= 200) && Int32_2 != 204 && Int32_2 != 304 && httprequest_0.HTTPMethods_0 != HTTPMethods.Head)
			{
				if (HasHeaderWithValue("transfer-encoding", "chunked"))
				{
					ReadChunked(stream_0);
				}
				else
				{
					List<string> headerValues = GetHeaderValues("content-length");
					List<string> headerValues2 = GetHeaderValues("content-range");
					if (headerValues != null && headerValues2 == null)
					{
						ReadRaw(stream_0, int.Parse(headerValues[0]));
					}
					else if (headerValues2 != null)
					{
						HTTPRange range = GetRange();
						ReadRaw(stream_0, range.Int32_1 - range.Int32_0 + 1);
					}
					else
					{
						ReadUnknownSize(stream_0);
					}
				}
				return true;
			}
			return true;
		}

		protected void ReadHeaders(Stream stream_2)
		{
			string text = ReadTo(stream_2, 58, 10).Trim();
			while (text != string.Empty)
			{
				string string_ = ReadTo(stream_2, 10);
				AddHeader(text, string_);
				text = ReadTo(stream_2, 58, 10);
			}
		}

		protected void AddHeader(string string_2, string string_3)
		{
			string_2 = string_2.ToLower();
			if (Dictionary_0 == null)
			{
				Dictionary_0 = new Dictionary<string, List<string>>();
			}
			List<string> value;
			if (!Dictionary_0.TryGetValue(string_2, out value))
			{
				Dictionary_0.Add(string_2, value = new List<string>(1));
			}
			value.Add(string_3);
		}

		public List<string> GetHeaderValues(string string_2)
		{
			if (Dictionary_0 == null)
			{
				return null;
			}
			string_2 = string_2.ToLower();
			List<string> value;
			if (Dictionary_0.TryGetValue(string_2, out value) && value.Count != 0)
			{
				return value;
			}
			return null;
		}

		public string GetFirstHeaderValue(string string_2)
		{
			if (Dictionary_0 == null)
			{
				return null;
			}
			string_2 = string_2.ToLower();
			List<string> value;
			if (Dictionary_0.TryGetValue(string_2, out value) && value.Count != 0)
			{
				return value[0];
			}
			return null;
		}

		public bool HasHeaderWithValue(string string_2, string string_3)
		{
			List<string> headerValues = GetHeaderValues(string_2);
			if (headerValues == null)
			{
				return false;
			}
			int num = 0;
			while (true)
			{
				if (num < headerValues.Count)
				{
					if (string.Compare(headerValues[num], string_3, StringComparison.OrdinalIgnoreCase) == 0)
					{
						break;
					}
					num++;
					continue;
				}
				return false;
			}
			return true;
		}

		public bool HasHeader(string string_2)
		{
			List<string> headerValues = GetHeaderValues(string_2);
			if (headerValues == null)
			{
				return false;
			}
			return true;
		}

		public HTTPRange GetRange()
		{
			List<string> headerValues = GetHeaderValues("content-range");
			if (headerValues == null)
			{
				return null;
			}
			string[] array = headerValues[0].Split(new char[3] { ' ', '-', '/' }, StringSplitOptions.RemoveEmptyEntries);
			if (array[1] == "*")
			{
				return new HTTPRange(int.Parse(array[2]));
			}
			return new HTTPRange(int.Parse(array[1]), int.Parse(array[2]), (!(array[3] != "*")) ? (-1) : int.Parse(array[3]));
		}

		public static string ReadTo(Stream stream_2, byte byte_4)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				int num = stream_2.ReadByte();
				while (num != byte_4 && num != -1)
				{
					memoryStream.WriteByte((byte)num);
					num = stream_2.ReadByte();
				}
				return memoryStream.ToArray().AsciiToString().Trim();
			}
		}

		public static string ReadTo(Stream stream_2, byte byte_4, byte byte_5)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				int num = stream_2.ReadByte();
				while (num != byte_4 && num != byte_5 && num != -1)
				{
					memoryStream.WriteByte((byte)num);
					num = stream_2.ReadByte();
				}
				return memoryStream.ToArray().AsciiToString().Trim();
			}
		}

		public static string NoTrimReadTo(Stream stream_2, byte byte_4, byte byte_5)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				int num = stream_2.ReadByte();
				while (num != byte_4 && num != byte_5 && num != -1)
				{
					memoryStream.WriteByte((byte)num);
					num = stream_2.ReadByte();
				}
				return memoryStream.ToArray().AsciiToString();
			}
		}

		protected int ReadChunkLength(Stream stream_2)
		{
			string text = ReadTo(stream_2, 10);
			string[] array = text.Split(';');
			string text2 = array[0];
			int result;
			if (!int.TryParse(text2, NumberStyles.AllowHexSpecifier, null, out result))
			{
				throw new Exception(string.Format("Can't parse '{0}' as a hex number!", text2));
			}
			return result;
		}

		protected void ReadChunked(Stream stream_2)
		{
			BeginReceiveStreamFragments();
			using (MemoryStream memoryStream = new MemoryStream())
			{
				int num = ReadChunkLength(stream_2);
				byte[] array = new byte[num];
				int num2 = 0;
				httprequest_0.Int32_5 = num;
				httprequest_0.Boolean_11 = Boolean_0 || Boolean_3;
				while (num != 0)
				{
					if (array.Length < num)
					{
						Array.Resize(ref array, num);
					}
					int num3 = 0;
					WaitWhileHasFragments();
					do
					{
						int num4 = stream_2.Read(array, num3, num - num3);
						if (num4 != 0)
						{
							num3 += num4;
							continue;
						}
						throw new Exception("The remote server closed the connection unexpectedly!");
					}
					while (num3 < num);
					if (httprequest_0.Boolean_4)
					{
						FeedStreamFragment(array, 0, num3);
					}
					else
					{
						memoryStream.Write(array, 0, num3);
					}
					ReadTo(stream_2, 10);
					num2 += num3;
					num = ReadChunkLength(stream_2);
					httprequest_0.Int32_5 += num;
					httprequest_0.Int32_4 = num2;
					httprequest_0.Boolean_11 = Boolean_0 || Boolean_3;
				}
				if (httprequest_0.Boolean_4)
				{
					FlushRemainingFragmentBuffer();
				}
				ReadHeaders(stream_2);
				if (!httprequest_0.Boolean_4)
				{
					Byte_0 = DecodeStream(memoryStream);
				}
			}
		}

		internal void ReadRaw(Stream stream_2, int int_6)
		{
			BeginReceiveStreamFragments();
			httprequest_0.Int32_5 = int_6;
			httprequest_0.Boolean_11 = Boolean_0 || Boolean_3;
			using (MemoryStream memoryStream = new MemoryStream((!httprequest_0.Boolean_4) ? int_6 : 0))
			{
				byte[] array = new byte[Math.Max(httprequest_0.Int32_0, 4096)];
				int num = 0;
				while (int_6 > 0)
				{
					num = 0;
					WaitWhileHasFragments();
					do
					{
						int num2 = stream_2.Read(array, num, Math.Min(int_6, array.Length - num));
						if (num2 != 0)
						{
							num += num2;
							int_6 -= num2;
							httprequest_0.Int32_4 += num2;
							httprequest_0.Boolean_11 = Boolean_0 || Boolean_3;
							continue;
						}
						throw new Exception("The remote server closed the connection unexpectedly!");
					}
					while (num < array.Length && int_6 > 0);
					if (httprequest_0.Boolean_4)
					{
						FeedStreamFragment(array, 0, num);
					}
					else
					{
						memoryStream.Write(array, 0, num);
					}
				}
				if (httprequest_0.Boolean_4)
				{
					FlushRemainingFragmentBuffer();
				}
				if (!httprequest_0.Boolean_4)
				{
					Byte_0 = DecodeStream(memoryStream);
				}
			}
		}

		protected void ReadUnknownSize(Stream stream_2)
		{
			NetworkStream networkStream = stream_2 as NetworkStream;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				byte[] array = new byte[Math.Max(httprequest_0.Int32_0, 4096)];
				int num = 0;
				int num2 = 0;
				do
				{
					num = 0;
					do
					{
						num2 = 0;
						if (networkStream == null)
						{
							num2 = stream_2.Read(array, num, array.Length - num);
						}
						else
						{
							for (int i = num; i < array.Length; i++)
							{
								if (!networkStream.DataAvailable)
								{
									break;
								}
								int num3 = stream_2.ReadByte();
								if (num3 < 0)
								{
									break;
								}
								array[i] = (byte)num3;
								num2++;
							}
						}
						num += num2;
						httprequest_0.Int32_4 += num2;
						httprequest_0.Int32_5 = httprequest_0.Int32_4;
						httprequest_0.Boolean_11 = Boolean_0 || Boolean_3;
					}
					while (num < array.Length && num2 > 0);
					if (httprequest_0.Boolean_4)
					{
						FeedStreamFragment(array, 0, num);
					}
					else
					{
						memoryStream.Write(array, 0, num);
					}
				}
				while (num2 > 0);
				if (httprequest_0.Boolean_4)
				{
					FlushRemainingFragmentBuffer();
				}
				if (!httprequest_0.Boolean_4)
				{
					Byte_0 = DecodeStream(memoryStream);
				}
			}
		}

		protected byte[] DecodeStream(Stream stream_2)
		{
			stream_2.Seek(0L, SeekOrigin.Begin);
			List<string> list = ((!Boolean_3) ? GetHeaderValues("content-encoding") : null);
			Stream stream = null;
			if (list == null)
			{
				stream = stream_2;
			}
			else
			{
				switch (list[0])
				{
				default:
				{
					int num = 0;
					stream = ((num != 1) ? stream_2 : new DeflateStream(stream_2, CompressionMode.Decompress));
					break;
				}
				case "gzip":
					stream = new GZipStream(stream_2, CompressionMode.Decompress);
					break;
				}
			}
			using (MemoryStream memoryStream = new MemoryStream((int)stream_2.Length))
			{
				byte[] array = new byte[1024];
				int num2 = 0;
				while ((num2 = stream.Read(array, 0, array.Length)) > 0)
				{
					memoryStream.Write(array, 0, num2);
				}
				return memoryStream.ToArray();
			}
		}

		protected void BeginReceiveStreamFragments()
		{
			if (!httprequest_0.Boolean_3 && httprequest_0.Boolean_4 && !Boolean_3 && HTTPCacheService.IsCacheble(httprequest_0.Uri_2, httprequest_0.HTTPMethods_0, this))
			{
				stream_1 = HTTPCacheService.PrepareStreamed(httprequest_0.Uri_2, this);
			}
			int_2 = 0;
		}

		protected void FeedStreamFragment(byte[] byte_4, int int_6, int int_7)
		{
			if (byte_2 == null)
			{
				byte_2 = new byte[httprequest_0.Int32_0];
				int_1 = 0;
			}
			if (int_1 + int_7 <= httprequest_0.Int32_0)
			{
				Array.Copy(byte_4, int_6, byte_2, int_1, int_7);
				int_1 += int_7;
				if (int_1 == httprequest_0.Int32_0)
				{
					AddStreamedFragment(byte_2);
					byte_2 = null;
					int_1 = 0;
				}
			}
			else
			{
				int num = httprequest_0.Int32_0 - int_1;
				FeedStreamFragment(byte_4, int_6, num);
				FeedStreamFragment(byte_4, int_6 + num, int_7 - num);
			}
		}

		protected void FlushRemainingFragmentBuffer()
		{
			if (byte_2 != null)
			{
				Array.Resize(ref byte_2, int_1);
				AddStreamedFragment(byte_2);
				byte_2 = null;
				int_1 = 0;
			}
			if (stream_1 != null)
			{
				stream_1.Dispose();
				stream_1 = null;
				HTTPCacheService.SetBodyLength(httprequest_0.Uri_2, int_2);
			}
		}

		protected void AddStreamedFragment(byte[] byte_4)
		{
			lock (object_0)
			{
				if (list_0 == null)
				{
					list_0 = new List<byte[]>();
				}
				list_0.Add(byte_4);
				if (stream_1 != null)
				{
					stream_1.Write(byte_4, 0, byte_4.Length);
					int_2 += byte_4.Length;
				}
			}
		}

		protected void WaitWhileHasFragments()
		{
		}

		public List<byte[]> GetStreamedFragments()
		{
			lock (object_0)
			{
				if (list_0 != null && list_0.Count != 0)
				{
					List<byte[]> result = new List<byte[]>(list_0);
					list_0.Clear();
					return result;
				}
				return null;
			}
		}

		internal bool HasStreamedFragments()
		{
			lock (object_0)
			{
				return list_0 != null && list_0.Count > 0;
			}
		}

		internal void FinishStreaming()
		{
			Boolean_2 = true;
			Dispose();
		}

		public void Dispose()
		{
			if (stream_1 != null)
			{
				stream_1.Dispose();
				stream_1 = null;
			}
		}
	}
}
