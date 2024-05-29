using System;
using System.Collections.Generic;

namespace BestHTTP.Authentication
{
	internal static class DigestStore
	{
		private static Dictionary<string, Digest> dictionary_0 = new Dictionary<string, Digest>();

		private static object object_0 = new object();

		private static string[] string_0 = new string[2] { "digest", "basic" };

		public static Digest Get(Uri uri_0)
		{
			lock (object_0)
			{
				Digest value = null;
				if (dictionary_0.TryGetValue(uri_0.Host, out value) && !value.IsUriProtected(uri_0))
				{
					return null;
				}
				return value;
			}
		}

		public static Digest GetOrCreate(Uri uri_0)
		{
			lock (object_0)
			{
				Digest value = null;
				if (!dictionary_0.TryGetValue(uri_0.Host, out value))
				{
					dictionary_0.Add(uri_0.Host, value = new Digest(uri_0));
				}
				return value;
			}
		}

		public static void Remove(Uri uri_0)
		{
			lock (object_0)
			{
				dictionary_0.Remove(uri_0.Host);
			}
		}

		public static string FindBest(List<string> list_0)
		{
			if (list_0 != null && list_0.Count != 0)
			{
				List<string> list = new List<string>(list_0.Count);
				for (int i = 0; i < list_0.Count; i++)
				{
					list.Add(list_0[i].ToLower());
				}
				int int_0 = 0;
				int num;
				while (true)
				{
					if (int_0 < string_0.Length)
					{
						num = list.FindIndex((string string_0) => string_0.StartsWith(DigestStore.string_0[int_0]));
						if (num != -1)
						{
							break;
						}
						int_0++;
						continue;
					}
					return string.Empty;
				}
				return list_0[num];
			}
			return string.Empty;
		}
	}
}
