using System.Collections;
using System.Collections.Generic;

namespace LitJson
{
	internal class OrderedDictionaryEnumerator : IEnumerator, IDictionaryEnumerator
	{
		private IEnumerator<KeyValuePair<string, JsonData>> ienumerator_0;

		public object Current
		{
			get
			{
				return Entry;
			}
		}

		public DictionaryEntry Entry
		{
			get
			{
				KeyValuePair<string, JsonData> current = ienumerator_0.Current;
				return new DictionaryEntry(current.Key, current.Value);
			}
		}

		public object Key
		{
			get
			{
				return ienumerator_0.Current.Key;
			}
		}

		public object Value
		{
			get
			{
				return ienumerator_0.Current.Value;
			}
		}

		public OrderedDictionaryEnumerator(IEnumerator<KeyValuePair<string, JsonData>> ienumerator_1)
		{
			ienumerator_0 = ienumerator_1;
		}

		public bool MoveNext()
		{
			return ienumerator_0.MoveNext();
		}

		public void Reset()
		{
			ienumerator_0.Reset();
		}
	}
}
