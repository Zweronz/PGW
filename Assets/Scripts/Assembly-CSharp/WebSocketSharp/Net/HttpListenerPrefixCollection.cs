using System;
using System.Collections;
using System.Collections.Generic;

namespace WebSocketSharp.Net
{
	public class HttpListenerPrefixCollection : ICollection<string>, IEnumerable, IEnumerable<string>
	{
		private HttpListener httpListener_0;

		private List<string> list_0;

		public int Count
		{
			get
			{
				return list_0.Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		public bool Boolean_0
		{
			get
			{
				return false;
			}
		}

		internal HttpListenerPrefixCollection(HttpListener httpListener_1)
		{
			httpListener_0 = httpListener_1;
			list_0 = new List<string>();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return list_0.GetEnumerator();
		}

		public void Add(string item)
		{
			httpListener_0.CheckDisposed();
			HttpListenerPrefix.CheckPrefix(item);
			if (!list_0.Contains(item))
			{
				list_0.Add(item);
				if (httpListener_0.Boolean_3)
				{
					EndPointManager.AddPrefix(item, httpListener_0);
				}
			}
		}

		public void Clear()
		{
			httpListener_0.CheckDisposed();
			list_0.Clear();
			if (httpListener_0.Boolean_3)
			{
				EndPointManager.RemoveListener(httpListener_0);
			}
		}

		public bool Contains(string item)
		{
			httpListener_0.CheckDisposed();
			if (item == null)
			{
				throw new ArgumentNullException("uriPrefix");
			}
			return list_0.Contains(item);
		}

		public void CopyTo(Array array_0, int int_0)
		{
			httpListener_0.CheckDisposed();
			((ICollection)list_0).CopyTo(array_0, int_0);
		}

		public void CopyTo(string[] array, int arrayIndex)
		{
			httpListener_0.CheckDisposed();
			list_0.CopyTo(array, arrayIndex);
		}

		public IEnumerator<string> GetEnumerator()
		{
			return list_0.GetEnumerator();
		}

		public bool Remove(string item)
		{
			httpListener_0.CheckDisposed();
			if (item == null)
			{
				throw new ArgumentNullException("uriPrefix");
			}
			bool result;
			if ((result = list_0.Remove(item)) && httpListener_0.Boolean_3)
			{
				EndPointManager.RemovePrefix(item, httpListener_0);
			}
			return result;
		}
	}
}
