using System;
using System.Collections.Generic;

namespace BestHTTP.Caching
{
	internal sealed class HTTPCacheFileLock
	{
		private static Dictionary<Uri, object> dictionary_0 = new Dictionary<Uri, object>();

		private static object object_0 = new object();

		internal static object Acquire(Uri uri_0)
		{
			lock (object_0)
			{
				object value;
				if (!dictionary_0.TryGetValue(uri_0, out value))
				{
					dictionary_0.Add(uri_0, value = new object());
				}
				return value;
			}
		}

		internal static void Remove(Uri uri_0)
		{
			lock (object_0)
			{
				if (dictionary_0.ContainsKey(uri_0))
				{
					dictionary_0.Remove(uri_0);
				}
			}
		}

		internal static void Clear()
		{
			lock (object_0)
			{
				dictionary_0.Clear();
			}
		}
	}
}
