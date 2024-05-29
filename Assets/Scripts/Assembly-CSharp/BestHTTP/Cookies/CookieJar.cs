using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace BestHTTP.Cookies
{
	public static class CookieJar
	{
		private const int int_0 = 1;

		private static List<Cookie> list_0 = new List<Cookie>();

		private static object object_0 = new object();

		private static bool bool_0;

		private static bool bool_1;

		private static bool bool_2;

		[CompilerGenerated]
		private static string string_0;

		[CompilerGenerated]
		private static string string_1;

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
					HTTPManager.ILogger_0.Warning("CookieJar", "Cookie saving and loading disabled!");
				}
				finally
				{
					bool_1 = true;
				}
				return bool_0;
			}
		}

		private static string String_0
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

		internal static void SetupFolder()
		{
			try
			{
				if (string.IsNullOrEmpty(String_0) || string.IsNullOrEmpty(String_1))
				{
					String_0 = Path.Combine(HTTPManager.GetRootCacheFolder(), "Cookies");
					String_1 = Path.Combine(String_0, "Library");
				}
			}
			catch
			{
			}
		}

		internal static void Set(HTTPResponse httpresponse_0)
		{
			if (httpresponse_0 == null)
			{
				return;
			}
			lock (object_0)
			{
				try
				{
					Maintain();
					List<Cookie> list = new List<Cookie>();
					List<string> headerValues = httpresponse_0.GetHeaderValues("set-cookie");
					if (headerValues == null)
					{
						return;
					}
					foreach (string item in headerValues)
					{
						try
						{
							Cookie cookie = Cookie.Parse(item, httpresponse_0.httprequest_0.Uri_2);
							if (cookie == null)
							{
								continue;
							}
							int int_;
							Cookie cookie2 = Find(cookie, out int_);
							if (!string.IsNullOrEmpty(cookie.String_1) && cookie.WillExpireInTheFuture())
							{
								if (cookie2 == null)
								{
									list_0.Add(cookie);
									list.Add(cookie);
								}
								else
								{
									cookie.DateTime_0 = cookie2.DateTime_0;
									list_0[int_] = cookie;
									list.Add(cookie);
								}
							}
							else if (int_ != -1)
							{
								list_0.RemoveAt(int_);
							}
						}
						catch
						{
						}
					}
					httpresponse_0.List_0 = list;
				}
				catch
				{
				}
			}
		}

		internal static void Maintain()
		{
			lock (object_0)
			{
				try
				{
					uint num = 0u;
					TimeSpan timeSpan = TimeSpan.FromDays(7.0);
					int num2 = 0;
					while (num2 < list_0.Count)
					{
						Cookie cookie = list_0[num2];
						if (cookie.WillExpireInTheFuture() && !(cookie.DateTime_1 + timeSpan < DateTime.UtcNow))
						{
							if (!cookie.Boolean_0)
							{
								num += cookie.GuessSize();
							}
							num2++;
						}
						else
						{
							list_0.RemoveAt(num2);
						}
					}
					if (num > HTTPManager.UInt32_0)
					{
						list_0.Sort();
						while (num > HTTPManager.UInt32_0 && list_0.Count > 0)
						{
							Cookie cookie2 = list_0[0];
							list_0.RemoveAt(0);
							num -= cookie2.GuessSize();
						}
					}
				}
				catch
				{
				}
			}
		}

		internal static void Persist()
		{
			if (!Boolean_0)
			{
				return;
			}
			lock (object_0)
			{
				try
				{
					Maintain();
					if (!Directory.Exists(String_0))
					{
						Directory.CreateDirectory(String_0);
					}
					using (FileStream output = new FileStream(String_1, FileMode.Create))
					{
						using (BinaryWriter binaryWriter = new BinaryWriter(output))
						{
							binaryWriter.Write(1);
							int num = 0;
							foreach (Cookie item in list_0)
							{
								if (!item.Boolean_0)
								{
									num++;
								}
							}
							binaryWriter.Write(num);
							foreach (Cookie item2 in list_0)
							{
								if (!item2.Boolean_0)
								{
									item2.SaveTo(binaryWriter);
								}
							}
						}
					}
				}
				catch
				{
				}
			}
		}

		internal static void Load()
		{
			if (!Boolean_0)
			{
				return;
			}
			lock (object_0)
			{
				if (bool_2)
				{
					return;
				}
				SetupFolder();
				try
				{
					list_0.Clear();
					if (!Directory.Exists(String_0))
					{
						Directory.CreateDirectory(String_0);
					}
					if (!File.Exists(String_1))
					{
						return;
					}
					using (FileStream input = new FileStream(String_1, FileMode.Open))
					{
						using (BinaryReader binaryReader = new BinaryReader(input))
						{
							binaryReader.ReadInt32();
							int num = binaryReader.ReadInt32();
							for (int i = 0; i < num; i++)
							{
								Cookie cookie = new Cookie();
								cookie.LoadFrom(binaryReader);
								if (cookie.WillExpireInTheFuture())
								{
									list_0.Add(cookie);
								}
							}
						}
					}
				}
				catch
				{
					list_0.Clear();
				}
				finally
				{
					bool_2 = true;
				}
			}
		}

		public static List<Cookie> Get(Uri uri_0)
		{
			lock (object_0)
			{
				Load();
				List<Cookie> list = null;
				for (int i = 0; i < list_0.Count; i++)
				{
					Cookie cookie = list_0[i];
					if (cookie.WillExpireInTheFuture() && uri_0.Host.IndexOf(cookie.String_2) != -1 && uri_0.AbsolutePath.StartsWith(cookie.String_3))
					{
						if (list == null)
						{
							list = new List<Cookie>();
						}
						list.Add(cookie);
					}
				}
				return list;
			}
		}

		public static void Set(Uri uri_0, Cookie cookie_0)
		{
			lock (object_0)
			{
				Load();
				Cookie cookie = new Cookie(cookie_0.String_0, cookie_0.String_1, uri_0.AbsolutePath, uri_0.Host);
				int int_;
				Find(cookie, out int_);
				if (int_ >= 0)
				{
					list_0[int_] = cookie;
				}
				else
				{
					list_0.Add(cookie);
				}
			}
		}

		public static List<Cookie> GetAll()
		{
			lock (object_0)
			{
				Load();
				return list_0;
			}
		}

		public static void Clear()
		{
			lock (object_0)
			{
				Load();
				list_0.Clear();
			}
		}

		public static void Clear(TimeSpan timeSpan_0)
		{
			lock (object_0)
			{
				Load();
				int num = 0;
				while (num < list_0.Count)
				{
					Cookie cookie = list_0[num];
					if (cookie.WillExpireInTheFuture() && !(cookie.DateTime_0 + timeSpan_0 < DateTime.UtcNow))
					{
						num++;
					}
					else
					{
						list_0.RemoveAt(num);
					}
				}
			}
		}

		public static void Clear(string string_2)
		{
			lock (object_0)
			{
				Load();
				int num = 0;
				while (num < list_0.Count)
				{
					Cookie cookie = list_0[num];
					if (cookie.WillExpireInTheFuture() && cookie.String_2.IndexOf(string_2) == -1)
					{
						num++;
					}
					else
					{
						list_0.RemoveAt(num);
					}
				}
			}
		}

		public static void Remove(Uri uri_0, string string_2)
		{
			lock (object_0)
			{
				Load();
				int num = 0;
				while (num < list_0.Count)
				{
					Cookie cookie = list_0[num];
					if (cookie.String_0.Equals(string_2, StringComparison.OrdinalIgnoreCase) && uri_0.Host.IndexOf(cookie.String_2) != -1)
					{
						list_0.RemoveAt(num);
					}
					else
					{
						num++;
					}
				}
			}
		}

		private static Cookie Find(Cookie cookie_0, out int int_1)
		{
			int num = 0;
			Cookie cookie;
			while (true)
			{
				if (num < list_0.Count)
				{
					cookie = list_0[num];
					if (cookie.Equals(cookie_0))
					{
						break;
					}
					num++;
					continue;
				}
				int_1 = -1;
				return null;
			}
			int_1 = num;
			return cookie;
		}
	}
}
