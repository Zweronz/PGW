using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace BestHTTP.Caching
{
	public static class HTTPCacheService
	{
		private const int int_0 = 2;

		private static bool bool_0;

		private static bool bool_1;

		private static Dictionary<Uri, HTTPCacheFileInfo> dictionary_0;

		private static Dictionary<ulong, HTTPCacheFileInfo> dictionary_1;

		private static bool bool_2;

		private static bool bool_3;

		private static ulong ulong_0;

		[CompilerGenerated]
		private static string string_0;

		[CompilerGenerated]
		private static string string_1;

		[CompilerGenerated]
		private static Predicate<string> predicate_0;

		[CompilerGenerated]
		private static Predicate<string> predicate_1;

		public static bool Boolean_0
		{
			get
			{
				if (bool_1)
				{
					return bool_0;
				}
				try
				{
					File.Exists(HTTPManager.GetRootCacheFolder());
					bool_0 = true;
				}
				catch
				{
					bool_0 = false;
					HTTPManager.ILogger_0.Warning("HTTPCacheService", "Cache Service Disabled!");
				}
				finally
				{
					bool_1 = true;
				}
				return bool_0;
			}
		}

		private static Dictionary<Uri, HTTPCacheFileInfo> Dictionary_0
		{
			get
			{
				LoadLibrary();
				return dictionary_0;
			}
		}

		internal static string String_0
		{
			[CompilerGenerated]
			get
			{
				return string_0;
			}
			[CompilerGenerated]
			private set
			{
				string_0 = value;
			}
		}

		private static string String_1
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

		static HTTPCacheService()
		{
			dictionary_1 = new Dictionary<ulong, HTTPCacheFileInfo>();
			ulong_0 = 1uL;
		}

		internal static void CheckSetup()
		{
			if (!Boolean_0)
			{
				return;
			}
			try
			{
				SetupCacheFolder();
				LoadLibrary();
			}
			catch
			{
			}
		}

		internal static void SetupCacheFolder()
		{
			if (!Boolean_0)
			{
				return;
			}
			try
			{
				if (string.IsNullOrEmpty(String_0) || string.IsNullOrEmpty(String_1))
				{
					String_0 = Path.Combine(HTTPManager.GetRootCacheFolder(), "HTTPCache");
					if (!Directory.Exists(String_0))
					{
						Directory.CreateDirectory(String_0);
					}
					String_1 = Path.Combine(HTTPManager.GetRootCacheFolder(), "Library");
				}
			}
			catch
			{
			}
		}

		internal static ulong GetNameIdx()
		{
			lock (Dictionary_0)
			{
				ulong result = ulong_0;
				do
				{
					ulong_0 = ++ulong_0 % ulong.MaxValue;
				}
				while (dictionary_1.ContainsKey(ulong_0));
				return result;
			}
		}

		internal static bool HasEntity(Uri uri_0)
		{
			if (!Boolean_0)
			{
				return false;
			}
			lock (Dictionary_0)
			{
				return Dictionary_0.ContainsKey(uri_0);
			}
		}

		internal static bool DeleteEntity(Uri uri_0, bool bool_4 = true)
		{
			if (!Boolean_0)
			{
				return false;
			}
			object obj = HTTPCacheFileLock.Acquire(uri_0);
			lock (obj)
			{
				try
				{
					lock (Dictionary_0)
					{
						bool flag;
						HTTPCacheFileInfo value;
						if (flag = Dictionary_0.TryGetValue(uri_0, out value))
						{
							value.Delete();
						}
						if (flag && bool_4)
						{
							Dictionary_0.Remove(uri_0);
							dictionary_1.Remove(value.UInt64_0);
						}
						return true;
					}
				}
				finally
				{
				}
			}
		}

		internal static bool IsCachedEntityExpiresInTheFuture(HTTPRequest httprequest_0)
		{
			if (!Boolean_0)
			{
				return false;
			}
			lock (Dictionary_0)
			{
				HTTPCacheFileInfo value;
				if (Dictionary_0.TryGetValue(httprequest_0.Uri_2, out value))
				{
					return value.WillExpireInTheFuture();
				}
			}
			return false;
		}

		internal static void SetHeaders(HTTPRequest httprequest_0)
		{
			if (!Boolean_0)
			{
				return;
			}
			lock (Dictionary_0)
			{
				HTTPCacheFileInfo value;
				if (Dictionary_0.TryGetValue(httprequest_0.Uri_2, out value))
				{
					value.SetUpRevalidationHeaders(httprequest_0);
				}
			}
		}

		internal static Stream GetBody(Uri uri_0, out int int_1)
		{
			int_1 = 0;
			if (!Boolean_0)
			{
				return null;
			}
			lock (Dictionary_0)
			{
				HTTPCacheFileInfo value;
				if (Dictionary_0.TryGetValue(uri_0, out value))
				{
					return value.GetBodyStream(out int_1);
				}
			}
			return null;
		}

		internal static HTTPResponse GetFullResponse(HTTPRequest httprequest_0)
		{
			if (!Boolean_0)
			{
				return null;
			}
			lock (Dictionary_0)
			{
				HTTPCacheFileInfo value;
				if (Dictionary_0.TryGetValue(httprequest_0.Uri_2, out value))
				{
					return value.ReadResponseTo(httprequest_0);
				}
			}
			return null;
		}

		internal static bool IsCacheble(Uri uri_0, HTTPMethods httpmethods_0, HTTPResponse httpresponse_0)
		{
			if (!Boolean_0)
			{
				return false;
			}
			if (httpmethods_0 != 0)
			{
				return false;
			}
			if (httpresponse_0 == null)
			{
				return false;
			}
			if (httpresponse_0.Int32_2 == 304)
			{
				return false;
			}
			if (httpresponse_0.Int32_2 >= 200 && httpresponse_0.Int32_2 < 400)
			{
				List<string> headerValues = httpresponse_0.GetHeaderValues("cache-control");
				if (headerValues != null && headerValues.Exists(delegate(string string_2)
				{
					string text2 = string_2.ToLower();
					return text2.Contains("no-store") || text2.Contains("no-cache");
				}))
				{
					return false;
				}
				List<string> headerValues2 = httpresponse_0.GetHeaderValues("pragma");
				if (headerValues2 != null && headerValues2.Exists(delegate(string string_2)
				{
					string text = string_2.ToLower();
					return text.Contains("no-store") || text.Contains("no-cache");
				}))
				{
					return false;
				}
				List<string> headerValues3 = httpresponse_0.GetHeaderValues("content-range");
				if (headerValues3 != null)
				{
					return false;
				}
				return true;
			}
			return false;
		}

		internal static HTTPCacheFileInfo Store(Uri uri_0, HTTPMethods httpmethods_0, HTTPResponse httpresponse_0)
		{
			if (httpresponse_0 != null && httpresponse_0.Byte_0 != null && httpresponse_0.Byte_0.Length != 0)
			{
				if (!Boolean_0)
				{
					return null;
				}
				HTTPCacheFileInfo value = null;
				lock (Dictionary_0)
				{
					if (!Dictionary_0.TryGetValue(uri_0, out value))
					{
						Dictionary_0.Add(uri_0, value = new HTTPCacheFileInfo(uri_0));
						dictionary_1.Add(value.UInt64_0, value);
					}
					try
					{
						value.Store(httpresponse_0);
						return value;
					}
					catch
					{
						DeleteEntity(uri_0);
						throw;
					}
				}
			}
			return null;
		}

		internal static Stream PrepareStreamed(Uri uri_0, HTTPResponse httpresponse_0)
		{
			if (!Boolean_0)
			{
				return null;
			}
			lock (Dictionary_0)
			{
				HTTPCacheFileInfo value;
				if (!Dictionary_0.TryGetValue(uri_0, out value))
				{
					Dictionary_0.Add(uri_0, value = new HTTPCacheFileInfo(uri_0));
					dictionary_1.Add(value.UInt64_0, value);
				}
				try
				{
					return value.GetSaveStream(httpresponse_0);
				}
				catch
				{
					DeleteEntity(uri_0);
					throw;
				}
			}
		}

		public static void BeginClear()
		{
			if (Boolean_0 && !bool_2)
			{
				bool_2 = true;
				SetupCacheFolder();
				new Thread(ClearImpl).Start();
			}
		}

		private static void ClearImpl(object object_0)
		{
			if (!Boolean_0)
			{
				return;
			}
			try
			{
				string[] files = Directory.GetFiles(String_0);
				for (int i = 0; i < files.Length; i++)
				{
					try
					{
						File.Delete(files[i]);
					}
					catch
					{
					}
				}
			}
			finally
			{
				dictionary_1.Clear();
				dictionary_0.Clear();
				ulong_0 = 1uL;
				SaveLibrary();
				bool_2 = false;
			}
		}

		public static void BeginMaintainence(HTTPCacheMaintananceParams httpcacheMaintananceParams_0)
		{
			if (httpcacheMaintananceParams_0 == null)
			{
				throw new ArgumentNullException("maintananceParams == null");
			}
			if (!Boolean_0 || bool_3)
			{
				return;
			}
			bool_3 = true;
			SetupCacheFolder();
			new Thread((ParameterizedThreadStart)delegate
			{
				try
				{
					lock (Dictionary_0)
					{
						DateTime dateTime = DateTime.UtcNow - httpcacheMaintananceParams_0.TimeSpan_0;
						List<HTTPCacheFileInfo> list = new List<HTTPCacheFileInfo>();
						foreach (KeyValuePair<Uri, HTTPCacheFileInfo> item in Dictionary_0)
						{
							if (item.Value.DateTime_0 < dateTime && DeleteEntity(item.Key, false))
							{
								list.Add(item.Value);
							}
						}
						for (int i = 0; i < list.Count; i++)
						{
							Dictionary_0.Remove(list[i].Uri_0);
							dictionary_1.Remove(list[i].UInt64_0);
						}
						list.Clear();
						ulong num = GetCacheSize();
						if (num > httpcacheMaintananceParams_0.UInt64_0)
						{
							List<HTTPCacheFileInfo> list2 = new List<HTTPCacheFileInfo>(dictionary_0.Count);
							foreach (KeyValuePair<Uri, HTTPCacheFileInfo> item2 in dictionary_0)
							{
								list2.Add(item2.Value);
							}
							list2.Sort();
							int num2 = 0;
							while (num >= httpcacheMaintananceParams_0.UInt64_0 && num2 < list2.Count)
							{
								try
								{
									HTTPCacheFileInfo hTTPCacheFileInfo = list2[num2];
									ulong num3 = (ulong)hTTPCacheFileInfo.Int32_0;
									DeleteEntity(hTTPCacheFileInfo.Uri_0);
									num -= num3;
								}
								catch
								{
								}
								finally
								{
									num2++;
								}
							}
						}
					}
				}
				finally
				{
					SaveLibrary();
					bool_3 = false;
				}
			}).Start();
		}

		public static int GetCacheEntityCount()
		{
			if (!Boolean_0)
			{
				return 0;
			}
			CheckSetup();
			lock (Dictionary_0)
			{
				return Dictionary_0.Count;
			}
		}

		public static ulong GetCacheSize()
		{
			ulong num = 0uL;
			if (!Boolean_0)
			{
				return num;
			}
			CheckSetup();
			lock (Dictionary_0)
			{
				foreach (KeyValuePair<Uri, HTTPCacheFileInfo> item in Dictionary_0)
				{
					if (item.Value.Int32_0 > 0)
					{
						num += (ulong)item.Value.Int32_0;
					}
				}
				return num;
			}
		}

		private static void LoadLibrary()
		{
			if (dictionary_0 != null || !Boolean_0)
			{
				return;
			}
			dictionary_0 = new Dictionary<Uri, HTTPCacheFileInfo>();
			if (!File.Exists(String_1))
			{
				DeleteUnusedFiles();
				return;
			}
			try
			{
				int num;
				lock (dictionary_0)
				{
					using (FileStream input = new FileStream(String_1, FileMode.Open))
					{
						using (BinaryReader binaryReader = new BinaryReader(input))
						{
							num = binaryReader.ReadInt32();
							if (num > 1)
							{
								ulong_0 = binaryReader.ReadUInt64();
							}
							int num2 = binaryReader.ReadInt32();
							for (int i = 0; i < num2; i++)
							{
								Uri uri = new Uri(binaryReader.ReadString());
								HTTPCacheFileInfo hTTPCacheFileInfo = new HTTPCacheFileInfo(uri, binaryReader, num);
								if (hTTPCacheFileInfo.IsExists())
								{
									dictionary_0.Add(uri, hTTPCacheFileInfo);
									if (num > 1)
									{
										dictionary_1.Add(hTTPCacheFileInfo.UInt64_0, hTTPCacheFileInfo);
									}
								}
							}
						}
					}
				}
				if (num == 1)
				{
					BeginClear();
				}
				else
				{
					DeleteUnusedFiles();
				}
			}
			catch
			{
			}
		}

		internal static void SaveLibrary()
		{
			if (dictionary_0 == null || !Boolean_0)
			{
				return;
			}
			try
			{
				lock (Dictionary_0)
				{
					using (FileStream output = new FileStream(String_1, FileMode.Create))
					{
						using (BinaryWriter binaryWriter = new BinaryWriter(output))
						{
							binaryWriter.Write(2);
							binaryWriter.Write(ulong_0);
							binaryWriter.Write(Dictionary_0.Count);
							foreach (KeyValuePair<Uri, HTTPCacheFileInfo> item in Dictionary_0)
							{
								binaryWriter.Write(item.Key.ToString());
								item.Value.SaveTo(binaryWriter);
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		internal static void SetBodyLength(Uri uri_0, int int_1)
		{
			if (!Boolean_0)
			{
				return;
			}
			lock (Dictionary_0)
			{
				HTTPCacheFileInfo value;
				if (Dictionary_0.TryGetValue(uri_0, out value))
				{
					value.Int32_0 = int_1;
					return;
				}
				Dictionary_0.Add(uri_0, value = new HTTPCacheFileInfo(uri_0, DateTime.UtcNow, int_1));
				dictionary_1.Add(value.UInt64_0, value);
			}
		}

		private static void DeleteUnusedFiles()
		{
			if (!Boolean_0)
			{
				return;
			}
			CheckSetup();
			string[] files = Directory.GetFiles(String_0);
			for (int i = 0; i < files.Length; i++)
			{
				try
				{
					string fileName = Path.GetFileName(files[i]);
					ulong result = 0uL;
					bool flag = false;
					if (ulong.TryParse(fileName, out result))
					{
						lock (Dictionary_0)
						{
							flag = !dictionary_1.ContainsKey(result);
						}
					}
					else
					{
						flag = true;
					}
					if (flag)
					{
						File.Delete(files[i]);
					}
				}
				catch
				{
				}
			}
		}
	}
}
