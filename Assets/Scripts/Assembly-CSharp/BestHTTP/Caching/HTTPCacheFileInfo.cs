using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using BestHTTP.Extensions;

namespace BestHTTP.Caching
{
	internal class HTTPCacheFileInfo : IComparable<HTTPCacheFileInfo>
	{
		[CompilerGenerated]
		private Uri uri_0;

		[CompilerGenerated]
		private DateTime dateTime_0;

		[CompilerGenerated]
		private int int_0;

		[CompilerGenerated]
		private string string_0;

		[CompilerGenerated]
		private string string_1;

		[CompilerGenerated]
		private DateTime dateTime_1;

		[CompilerGenerated]
		private long long_0;

		[CompilerGenerated]
		private long long_1;

		[CompilerGenerated]
		private DateTime dateTime_2;

		[CompilerGenerated]
		private bool bool_0;

		[CompilerGenerated]
		private DateTime dateTime_3;

		[CompilerGenerated]
		private string string_2;

		[CompilerGenerated]
		private ulong ulong_0;

		internal Uri Uri_0
		{
			[CompilerGenerated]
			get
			{
				return uri_0;
			}
			[CompilerGenerated]
			set
			{
				uri_0 = value;
			}
		}

		internal DateTime DateTime_0
		{
			[CompilerGenerated]
			get
			{
				return dateTime_0;
			}
			[CompilerGenerated]
			set
			{
				dateTime_0 = value;
			}
		}

		internal int Int32_0
		{
			[CompilerGenerated]
			get
			{
				return int_0;
			}
			[CompilerGenerated]
			set
			{
				int_0 = value;
			}
		}

		private string String_0
		{
			[CompilerGenerated]
			get
			{
				return string_0;
			}
			[CompilerGenerated]
			set
			{
				string_0 = value;
			}
		}

		private string String_1
		{
			[CompilerGenerated]
			get
			{
				return string_1;
			}
			[CompilerGenerated]
			set
			{
				string_1 = value;
			}
		}

		private DateTime DateTime_1
		{
			[CompilerGenerated]
			get
			{
				return dateTime_1;
			}
			[CompilerGenerated]
			set
			{
				dateTime_1 = value;
			}
		}

		private long Int64_0
		{
			[CompilerGenerated]
			get
			{
				return long_0;
			}
			[CompilerGenerated]
			set
			{
				long_0 = value;
			}
		}

		private long Int64_1
		{
			[CompilerGenerated]
			get
			{
				return long_1;
			}
			[CompilerGenerated]
			set
			{
				long_1 = value;
			}
		}

		private DateTime DateTime_2
		{
			[CompilerGenerated]
			get
			{
				return dateTime_2;
			}
			[CompilerGenerated]
			set
			{
				dateTime_2 = value;
			}
		}

		private bool Boolean_0
		{
			[CompilerGenerated]
			get
			{
				return bool_0;
			}
			[CompilerGenerated]
			set
			{
				bool_0 = value;
			}
		}

		private DateTime DateTime_3
		{
			[CompilerGenerated]
			get
			{
				return dateTime_3;
			}
			[CompilerGenerated]
			set
			{
				dateTime_3 = value;
			}
		}

		private string String_2
		{
			[CompilerGenerated]
			get
			{
				return string_2;
			}
			[CompilerGenerated]
			set
			{
				string_2 = value;
			}
		}

		internal ulong UInt64_0
		{
			[CompilerGenerated]
			get
			{
				return ulong_0;
			}
			[CompilerGenerated]
			set
			{
				ulong_0 = value;
			}
		}

		internal HTTPCacheFileInfo(Uri uri_1)
			: this(uri_1, DateTime.UtcNow, -1)
		{
		}

		internal HTTPCacheFileInfo(Uri uri_1, DateTime dateTime_4, int int_1)
		{
			Uri_0 = uri_1;
			DateTime_0 = dateTime_4;
			Int32_0 = int_1;
			Int64_1 = -1L;
			UInt64_0 = HTTPCacheService.GetNameIdx();
		}

		internal HTTPCacheFileInfo(Uri uri_1, BinaryReader binaryReader_0, int int_1)
		{
			Uri_0 = uri_1;
			DateTime_0 = DateTime.FromBinary(binaryReader_0.ReadInt64());
			Int32_0 = binaryReader_0.ReadInt32();
			switch (int_1)
			{
			default:
				return;
			case 2:
				UInt64_0 = binaryReader_0.ReadUInt64();
				break;
			case 1:
				break;
			}
			String_0 = binaryReader_0.ReadString();
			String_1 = binaryReader_0.ReadString();
			DateTime_1 = DateTime.FromBinary(binaryReader_0.ReadInt64());
			Int64_0 = binaryReader_0.ReadInt64();
			Int64_1 = binaryReader_0.ReadInt64();
			DateTime_2 = DateTime.FromBinary(binaryReader_0.ReadInt64());
			Boolean_0 = binaryReader_0.ReadBoolean();
			DateTime_3 = DateTime.FromBinary(binaryReader_0.ReadInt64());
		}

		internal void SaveTo(BinaryWriter binaryWriter_0)
		{
			binaryWriter_0.Write(DateTime_0.ToBinary());
			binaryWriter_0.Write(Int32_0);
			binaryWriter_0.Write(UInt64_0);
			binaryWriter_0.Write(String_0);
			binaryWriter_0.Write(String_1);
			binaryWriter_0.Write(DateTime_1.ToBinary());
			binaryWriter_0.Write(Int64_0);
			binaryWriter_0.Write(Int64_1);
			binaryWriter_0.Write(DateTime_2.ToBinary());
			binaryWriter_0.Write(Boolean_0);
			binaryWriter_0.Write(DateTime_3.ToBinary());
		}

		private string GetPath()
		{
			if (String_2 != null)
			{
				return String_2;
			}
			return String_2 = Path.Combine(HTTPCacheService.String_0, UInt64_0.ToString("X"));
		}

		internal bool IsExists()
		{
			if (!HTTPCacheService.Boolean_0)
			{
				return false;
			}
			return File.Exists(GetPath());
		}

		internal void Delete()
		{
			if (!HTTPCacheService.Boolean_0)
			{
				return;
			}
			string path = GetPath();
			try
			{
				File.Delete(path);
			}
			catch
			{
			}
			finally
			{
				Reset();
			}
		}

		private void Reset()
		{
			UInt64_0 = 0uL;
			Int32_0 = -1;
			String_0 = string.Empty;
			DateTime_1 = DateTime.FromBinary(0L);
			String_1 = string.Empty;
			Int64_0 = 0L;
			Int64_1 = -1L;
			DateTime_2 = DateTime.FromBinary(0L);
			Boolean_0 = false;
			DateTime_3 = DateTime.FromBinary(0L);
		}

		private void SetUpCachingValues(HTTPResponse httpresponse_0)
		{
			String_0 = httpresponse_0.GetFirstHeaderValue("ETag").ToStrOrEmpty();
			DateTime_1 = httpresponse_0.GetFirstHeaderValue("Expires").ToDateTime(DateTime.FromBinary(0L));
			String_1 = httpresponse_0.GetFirstHeaderValue("Last-Modified").ToStrOrEmpty();
			Int64_0 = httpresponse_0.GetFirstHeaderValue("Age").ToInt64();
			DateTime_2 = httpresponse_0.GetFirstHeaderValue("Date").ToDateTime(DateTime.FromBinary(0L));
			string firstHeaderValue = httpresponse_0.GetFirstHeaderValue("cache-control");
			if (!string.IsNullOrEmpty(firstHeaderValue))
			{
				string[] array = firstHeaderValue.FindOption("Max-Age");
				double result;
				if (array != null && double.TryParse(array[1], out result))
				{
					Int64_1 = (int)result;
				}
				Boolean_0 = firstHeaderValue.ToLower().Contains("must-revalidate");
			}
			DateTime_3 = DateTime.UtcNow;
		}

		internal bool WillExpireInTheFuture()
		{
			if (!IsExists())
			{
				return false;
			}
			if (Boolean_0)
			{
				return false;
			}
			if (Int64_1 != -1L)
			{
				long val = Math.Max(0L, (long)(DateTime_3 - DateTime_2).TotalSeconds);
				long num = Math.Max(val, Int64_0);
				long num2 = (long)(DateTime.UtcNow - DateTime_2).TotalSeconds;
				long num3 = num + num2;
				return num3 < Int64_1;
			}
			return DateTime_1 > DateTime.UtcNow;
		}

		internal void SetUpRevalidationHeaders(HTTPRequest httprequest_0)
		{
			if (IsExists())
			{
				if (!string.IsNullOrEmpty(String_0))
				{
					httprequest_0.AddHeader("If-None-Match", String_0);
				}
				if (!string.IsNullOrEmpty(String_1))
				{
					httprequest_0.AddHeader("If-Modified-Since", String_1);
				}
			}
		}

		internal Stream GetBodyStream(out int int_1)
		{
			if (!IsExists())
			{
				int_1 = 0;
				return null;
			}
			int_1 = Int32_0;
			DateTime_0 = DateTime.UtcNow;
			FileStream fileStream = new FileStream(GetPath(), FileMode.Open);
			fileStream.Seek(-int_1, SeekOrigin.End);
			return fileStream;
		}

		internal HTTPResponse ReadResponseTo(HTTPRequest httprequest_0)
		{
			if (!IsExists())
			{
				return null;
			}
			DateTime_0 = DateTime.UtcNow;
			using (FileStream stream_ = new FileStream(GetPath(), FileMode.Open))
			{
				HTTPResponse hTTPResponse = new HTTPResponse(httprequest_0, stream_, httprequest_0.Boolean_4, true);
				hTTPResponse.Receive(Int32_0);
				return hTTPResponse;
			}
		}

		internal void Store(HTTPResponse httpresponse_0)
		{
			if (!HTTPCacheService.Boolean_0)
			{
				return;
			}
			string path = GetPath();
			if (path.Length > HTTPManager.Int32_0)
			{
				return;
			}
			if (File.Exists(path))
			{
				Delete();
			}
			using (FileStream fileStream = new FileStream(path, FileMode.Create))
			{
				fileStream.WriteLine("HTTP/1.1 {0} {1}", httpresponse_0.Int32_2, httpresponse_0.String_0);
				foreach (KeyValuePair<string, List<string>> item in httpresponse_0.Dictionary_0)
				{
					for (int i = 0; i < item.Value.Count; i++)
					{
						fileStream.WriteLine("{0}: {1}", item.Key, item.Value[i]);
					}
				}
				fileStream.WriteLine();
				fileStream.Write(httpresponse_0.Byte_0, 0, httpresponse_0.Byte_0.Length);
			}
			Int32_0 = httpresponse_0.Byte_0.Length;
			DateTime_0 = DateTime.UtcNow;
			SetUpCachingValues(httpresponse_0);
		}

		internal Stream GetSaveStream(HTTPResponse httpresponse_0)
		{
			if (!HTTPCacheService.Boolean_0)
			{
				return null;
			}
			DateTime_0 = DateTime.UtcNow;
			string path = GetPath();
			if (File.Exists(path))
			{
				Delete();
			}
			if (path.Length > HTTPManager.Int32_0)
			{
				return null;
			}
			using (FileStream fileStream_ = new FileStream(path, FileMode.Create))
			{
				fileStream_.WriteLine("HTTP/1.1 {0} {1}", httpresponse_0.Int32_2, httpresponse_0.String_0);
				foreach (KeyValuePair<string, List<string>> item in httpresponse_0.Dictionary_0)
				{
					for (int i = 0; i < item.Value.Count; i++)
					{
						fileStream_.WriteLine("{0}: {1}", item.Key, item.Value[i]);
					}
				}
				fileStream_.WriteLine();
			}
			if (httpresponse_0.Boolean_3 && !httpresponse_0.Dictionary_0.ContainsKey("content-length"))
			{
				httpresponse_0.Dictionary_0.Add("content-length", new List<string> { Int32_0.ToString() });
			}
			SetUpCachingValues(httpresponse_0);
			return new FileStream(GetPath(), FileMode.Append);
		}

		public int CompareTo(HTTPCacheFileInfo a_Other)
		{
			return DateTime_0.CompareTo(a_Other.DateTime_0);
		}
	}
}
