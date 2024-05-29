using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace engine.helpers
{
	public static class CollectionExtensions
	{
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> ienumerable_0)
		{
			Random rnd = new Random();
			return ienumerable_0.OrderBy((T gparam_0) => rnd.Next());
		}

		public static Dictionary<TKey, List<TValue>> GroupListBy<TKey, TValue>(this IEnumerable<TValue> ienumerable_0, Func<TValue, TKey> func_0)
		{
			Dictionary<TKey, List<TValue>> dictionary = new Dictionary<TKey, List<TValue>>();
			foreach (TValue item in ienumerable_0)
			{
				TKey key = func_0(item);
				List<TValue> value;
				if (!dictionary.TryGetValue(key, out value))
				{
					value = (dictionary[key] = new List<TValue>());
				}
				value.Add(item);
			}
			return dictionary;
		}

		public static HashSet<T> ToHashSet<T>(this IEnumerable<T> ienumerable_0)
		{
			return new HashSet<T>(ienumerable_0);
		}

		public static IEnumerable<T> Concat<T>(this IEnumerable<IEnumerable<T>> ienumerable_0)
		{
			IEnumerator<IEnumerable<T>> enumerator = ienumerable_0.GetEnumerator();
			/*Error near IL_0037: Could not find block for branch target IL_00d0*/;
			yield break;
		}

		public static IEnumerable<T> Slice<T>(this IEnumerable<T> ienumerable_0, int int_0, int int_1)
		{
			int index = 0;
			int count2 = 0;
			if (ienumerable_0 == null)
			{
				throw new ArgumentNullException("collection");
			}
			count2 = ((ienumerable_0 is ICollection<T>) ? ((ICollection<T>)ienumerable_0).Count : ((!(ienumerable_0 is ICollection)) ? ienumerable_0.Count() : ((ICollection)ienumerable_0).Count));
			if (int_0 < 0)
			{
				int_0 += count2;
			}
			if (int_1 < 0)
			{
				int_1 += count2;
			}
			IEnumerator<T> enumerator = ienumerable_0.GetEnumerator();
			/*Error near IL_00eb: Could not find block for branch target IL_013a*/;
			yield break;
		}

		public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> ienumerable_0, Dictionary<TKey, TValue> dictionary_0)
		{
			if (dictionary_0 == null)
			{
				return ienumerable_0.ToDictionary();
			}
			foreach (KeyValuePair<TKey, TValue> item in ienumerable_0)
			{
				dictionary_0.Add(item.Key, item.Value);
			}
			return dictionary_0;
		}

		public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> ienumerable_0)
		{
			Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
			foreach (KeyValuePair<TKey, TValue> item in ienumerable_0)
			{
				dictionary.Add(item.Key, item.Value);
			}
			return dictionary;
		}

		public static IEnumerable<KeyValuePair<int, TValue>> AsIndexedEnumerable<TValue>(this IList<TValue> ilist_0)
		{
			for (int i = 0; i < ilist_0.Count; i++)
			{
				yield return new KeyValuePair<int, TValue>(i, ilist_0[i]);
			}
		}

		public static IEnumerable<KeyValuePair<string, TValue>> AsStrIndexedEnumerable<TValue>(this IList<TValue> ilist_0)
		{
			for (int i = 0; i < ilist_0.Count; i++)
			{
				yield return new KeyValuePair<string, TValue>(i.ToString(), ilist_0[i]);
			}
		}

		public static bool IsEmpty<T>(this IEnumerable<T> ienumerable_0)
		{
			return !ienumerable_0.GetEnumerator().MoveNext();
		}
	}
}
