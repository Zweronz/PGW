using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace WebSocketSharp.Net
{
	[Serializable]
	public class CookieCollection : ICollection, IEnumerable
	{
		private List<Cookie> list_0;

		private object object_0;

		internal IList<Cookie> IList_0
		{
			get
			{
				return list_0;
			}
		}

		internal IEnumerable<Cookie> IEnumerable_0
		{
			get
			{
				List<Cookie> list = new List<Cookie>(list_0);
				if (list.Count > 1)
				{
					list.Sort(compareCookieWithinSorted);
				}
				return list;
			}
		}

		public int Count
		{
			get
			{
				return list_0.Count;
			}
		}

		public bool Boolean_0
		{
			get
			{
				return true;
			}
		}

		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		public Cookie this[int index]
		{
			get
			{
				if (index < 0 || index >= list_0.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return list_0[index];
			}
		}

		public Cookie this[string name]
		{
			get
			{
				if (name == null)
				{
					throw new ArgumentNullException("name");
				}
				foreach (Cookie item in IEnumerable_0)
				{
					if (item.String_2.Equals(name, StringComparison.InvariantCultureIgnoreCase))
					{
						return item;
					}
				}
				return null;
			}
		}

		public object SyncRoot
		{
			get
			{
				return object_0 ?? (object_0 = ((ICollection)list_0).SyncRoot);
			}
		}

		public CookieCollection()
		{
			list_0 = new List<Cookie>();
		}

		private static int compareCookieWithinSort(Cookie cookie_0, Cookie cookie_1)
		{
			return cookie_0.String_2.Length + cookie_0.String_5.Length - (cookie_1.String_2.Length + cookie_1.String_5.Length);
		}

		private static int compareCookieWithinSorted(Cookie cookie_0, Cookie cookie_1)
		{
			int num = 0;
			return ((num = cookie_0.Int32_2 - cookie_1.Int32_2) != 0) ? num : (((num = cookie_0.String_2.CompareTo(cookie_1.String_2)) == 0) ? (cookie_1.String_3.Length - cookie_0.String_3.Length) : num);
		}

		private static CookieCollection parseRequest(string string_0)
		{
			CookieCollection cookieCollection = new CookieCollection();
			Cookie cookie = null;
			int num = 0;
			string[] array = splitCookieHeaderValue(string_0);
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].Trim();
				if (text.Length == 0)
				{
					continue;
				}
				if (text.StartsWith("$version", StringComparison.InvariantCultureIgnoreCase))
				{
					num = int.Parse(text.GetValue('=', true));
					continue;
				}
				if (text.StartsWith("$path", StringComparison.InvariantCultureIgnoreCase))
				{
					if (cookie != null)
					{
						cookie.String_3 = text.GetValue('=');
					}
					continue;
				}
				if (text.StartsWith("$domain", StringComparison.InvariantCultureIgnoreCase))
				{
					if (cookie != null)
					{
						cookie.String_1 = text.GetValue('=');
					}
					continue;
				}
				if (text.StartsWith("$port", StringComparison.InvariantCultureIgnoreCase))
				{
					string string_ = ((!text.Equals("$port", StringComparison.InvariantCultureIgnoreCase)) ? text.GetValue('=') : "\"\"");
					if (cookie != null)
					{
						cookie.String_4 = string_;
					}
					continue;
				}
				if (cookie != null)
				{
					cookieCollection.Add(cookie);
				}
				string string_2 = string.Empty;
				int num2 = text.IndexOf('=');
				string string_3;
				if (num2 == -1)
				{
					string_3 = text;
				}
				else if (num2 == text.Length - 1)
				{
					string_3 = text.Substring(0, num2).TrimEnd(' ');
				}
				else
				{
					string_3 = text.Substring(0, num2).TrimEnd(' ');
					string_2 = text.Substring(num2 + 1).TrimStart(' ');
				}
				cookie = new Cookie(string_3, string_2);
				if (num != 0)
				{
					cookie.Int32_2 = num;
				}
			}
			if (cookie != null)
			{
				cookieCollection.Add(cookie);
			}
			return cookieCollection;
		}

		private static CookieCollection parseResponse(string string_0)
		{
			CookieCollection cookieCollection = new CookieCollection();
			Cookie cookie = null;
			string[] array = splitCookieHeaderValue(string_0);
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].Trim();
				if (text.Length == 0)
				{
					continue;
				}
				if (text.StartsWith("version", StringComparison.InvariantCultureIgnoreCase))
				{
					if (cookie != null)
					{
						cookie.Int32_2 = int.Parse(text.GetValue('=', true));
					}
					continue;
				}
				if (text.StartsWith("expires", StringComparison.InvariantCultureIgnoreCase))
				{
					StringBuilder stringBuilder = new StringBuilder(text.GetValue('='), 32);
					if (i < array.Length - 1)
					{
						stringBuilder.AppendFormat(", {0}", array[++i].Trim());
					}
					DateTime result;
					if (!DateTime.TryParseExact(stringBuilder.ToString(), new string[2] { "ddd, dd'-'MMM'-'yyyy HH':'mm':'ss 'GMT'", "r" }, CultureInfo.CreateSpecificCulture("en-US"), DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out result))
					{
						result = DateTime.Now;
					}
					if (cookie != null && cookie.DateTime_0 == DateTime.MinValue)
					{
						cookie.DateTime_0 = result.ToLocalTime();
					}
					continue;
				}
				if (text.StartsWith("max-age", StringComparison.InvariantCultureIgnoreCase))
				{
					int num = int.Parse(text.GetValue('=', true));
					DateTime dateTime_ = DateTime.Now.AddSeconds(num);
					if (cookie != null)
					{
						cookie.DateTime_0 = dateTime_;
					}
					continue;
				}
				if (text.StartsWith("path", StringComparison.InvariantCultureIgnoreCase))
				{
					if (cookie != null)
					{
						cookie.String_3 = text.GetValue('=');
					}
					continue;
				}
				if (text.StartsWith("domain", StringComparison.InvariantCultureIgnoreCase))
				{
					if (cookie != null)
					{
						cookie.String_1 = text.GetValue('=');
					}
					continue;
				}
				if (text.StartsWith("port", StringComparison.InvariantCultureIgnoreCase))
				{
					string string_ = ((!text.Equals("port", StringComparison.InvariantCultureIgnoreCase)) ? text.GetValue('=') : "\"\"");
					if (cookie != null)
					{
						cookie.String_4 = string_;
					}
					continue;
				}
				if (text.StartsWith("comment", StringComparison.InvariantCultureIgnoreCase))
				{
					if (cookie != null)
					{
						cookie.String_0 = text.GetValue('=').UrlDecode();
					}
					continue;
				}
				if (text.StartsWith("commenturl", StringComparison.InvariantCultureIgnoreCase))
				{
					if (cookie != null)
					{
						cookie.Uri_0 = text.GetValue('=', true).ToUri();
					}
					continue;
				}
				if (text.StartsWith("discard", StringComparison.InvariantCultureIgnoreCase))
				{
					if (cookie != null)
					{
						cookie.Boolean_1 = true;
					}
					continue;
				}
				if (text.StartsWith("secure", StringComparison.InvariantCultureIgnoreCase))
				{
					if (cookie != null)
					{
						cookie.Boolean_4 = true;
					}
					continue;
				}
				if (text.StartsWith("httponly", StringComparison.InvariantCultureIgnoreCase))
				{
					if (cookie != null)
					{
						cookie.Boolean_3 = true;
					}
					continue;
				}
				if (cookie != null)
				{
					cookieCollection.Add(cookie);
				}
				string string_2 = string.Empty;
				int num2 = text.IndexOf('=');
				string string_3;
				if (num2 == -1)
				{
					string_3 = text;
				}
				else if (num2 == text.Length - 1)
				{
					string_3 = text.Substring(0, num2).TrimEnd(' ');
				}
				else
				{
					string_3 = text.Substring(0, num2).TrimEnd(' ');
					string_2 = text.Substring(num2 + 1).TrimStart(' ');
				}
				cookie = new Cookie(string_3, string_2);
			}
			if (cookie != null)
			{
				cookieCollection.Add(cookie);
			}
			return cookieCollection;
		}

		private int searchCookie(Cookie cookie_0)
		{
			string string_ = cookie_0.String_2;
			string string_2 = cookie_0.String_3;
			string string_3 = cookie_0.String_1;
			int int32_ = cookie_0.Int32_2;
			int num = list_0.Count - 1;
			while (true)
			{
				if (num >= 0)
				{
					Cookie cookie = list_0[num];
					if (cookie.String_2.Equals(string_, StringComparison.InvariantCultureIgnoreCase) && cookie.String_3.Equals(string_2, StringComparison.InvariantCulture) && cookie.String_1.Equals(string_3, StringComparison.InvariantCultureIgnoreCase) && cookie.Int32_2 == int32_)
					{
						break;
					}
					num--;
					continue;
				}
				return -1;
			}
			return num;
		}

		private static string[] splitCookieHeaderValue(string string_0)
		{
			return new List<string>(string_0.SplitHeaderValue(',', ';')).ToArray();
		}

		internal static CookieCollection Parse(string string_0, bool bool_0)
		{
			return (!bool_0) ? parseRequest(string_0) : parseResponse(string_0);
		}

		internal void SetOrRemove(Cookie cookie_0)
		{
			int num = searchCookie(cookie_0);
			if (num == -1)
			{
				if (!cookie_0.Boolean_2)
				{
					list_0.Add(cookie_0);
				}
			}
			else if (!cookie_0.Boolean_2)
			{
				list_0[num] = cookie_0;
			}
			else
			{
				list_0.RemoveAt(num);
			}
		}

		internal void SetOrRemove(CookieCollection cookieCollection_0)
		{
			foreach (Cookie item in cookieCollection_0)
			{
				SetOrRemove(item);
			}
		}

		internal void Sort()
		{
			if (list_0.Count > 1)
			{
				list_0.Sort(compareCookieWithinSort);
			}
		}

		public void Add(Cookie cookie_0)
		{
			if (cookie_0 == null)
			{
				throw new ArgumentNullException("cookie");
			}
			int num = searchCookie(cookie_0);
			if (num == -1)
			{
				list_0.Add(cookie_0);
			}
			else
			{
				list_0[num] = cookie_0;
			}
		}

		public void Add(CookieCollection cookieCollection_0)
		{
			if (cookieCollection_0 == null)
			{
				throw new ArgumentNullException("cookies");
			}
			foreach (Cookie item in cookieCollection_0)
			{
				Add(item);
			}
		}

		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Less than zero.");
			}
			if (array.Rank > 1)
			{
				throw new ArgumentException("Multidimensional.", "array");
			}
			if (array.Length - index < list_0.Count)
			{
				throw new ArgumentException("The number of elements in this collection is greater than the available space of the destination array.");
			}
			if (!array.GetType().GetElementType().IsAssignableFrom(typeof(Cookie)))
			{
				throw new InvalidCastException("The elements in this collection cannot be cast automatically to the type of the destination array.");
			}
			((ICollection)list_0).CopyTo(array, index);
		}

		public void CopyTo(Cookie[] cookie_0, int int_0)
		{
			if (cookie_0 == null)
			{
				throw new ArgumentNullException("array");
			}
			if (int_0 < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Less than zero.");
			}
			if (cookie_0.Length - int_0 < list_0.Count)
			{
				throw new ArgumentException("The number of elements in this collection is greater than the available space of the destination array.");
			}
			list_0.CopyTo(cookie_0, int_0);
		}

		public IEnumerator GetEnumerator()
		{
			return list_0.GetEnumerator();
		}
	}
}
