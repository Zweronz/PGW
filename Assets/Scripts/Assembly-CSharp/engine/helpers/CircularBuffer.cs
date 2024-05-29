using System;
using System.Collections;
using System.Collections.Generic;

namespace engine.helpers
{
	public class CircularBuffer<T> : IEnumerable<T>, IEnumerable, ICollection<T>, IList<T>
	{
		private List<T> items_ = new List<T>();

		public List<T> List_0
		{
			get
			{
				return items_;
			}
		}

		public uint UInt32_0 { get; private set; }

		public T this[int index]
		{
			get
			{
				return items_[index];
			}
			set
			{
				items_[index] = value;
			}
		}

		public int Count
		{
			get
			{
				return items_.Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		public CircularBuffer(uint uint_0)
		{
			if (uint_0 == 0)
			{
				throw new ArgumentException();
			}
			UInt32_0 = uint_0;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return items_.GetEnumerator();
		}

		public IEnumerator<T> GetEnumerator()
		{
			return items_.GetEnumerator();
		}

		public int IndexOf(T item)
		{
			return items_.IndexOf(item);
		}

		public void Insert(int index, T item)
		{
			items_.Insert(index, item);
		}

		public void RemoveAt(int index)
		{
			items_.RemoveAt(index);
		}

		public void Add(T item)
		{
			if (items_.Count >= UInt32_0)
			{
				items_.RemoveAt(0);
			}
			items_.Add(item);
		}

		public void Clear()
		{
			items_.Clear();
		}

		public bool Contains(T item)
		{
			return items_.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			items_.CopyTo(array, arrayIndex);
		}

		public bool Remove(T item)
		{
			return items_.Remove(item);
		}
	}
}
