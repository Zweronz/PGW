using System.Collections.Generic;
using System.Threading;

namespace System.Collections.Concurrent
{
	public class ConcurrentStack<T> : IEnumerable<T>, IProducerConsumerCollection<T>, ICollection, IEnumerable
	{
		private class Node
		{
			public T Value = default(T);

			public Node Next;
		}

		private Node head;

		private int count;

		private object syncRoot = new object();

		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		object ICollection.SyncRoot
		{
			get
			{
				return syncRoot;
			}
		}

		public int Count
		{
			get
			{
				return count;
			}
		}

		public bool Boolean_0
		{
			get
			{
				return count == 0;
			}
		}

		public ConcurrentStack()
		{
		}

		public ConcurrentStack(IEnumerable<T> ienumerable_0)
		{
			foreach (T item in ienumerable_0)
			{
				Push(item);
			}
		}

		bool IProducerConsumerCollection<T>.TryAdd(T item)
		{
			Push(item);
			return true;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return InternalGetEnumerator();
		}

		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank > 1)
			{
				throw new ArgumentException("The array can't be multidimensional");
			}
			if (array.GetLowerBound(0) != 0)
			{
				throw new ArgumentException("The array needs to be 0-based");
			}
			T[] array2 = array as T[];
			if (array2 == null)
			{
				throw new ArgumentException("The array cannot be cast to the collection element type", "array");
			}
			CopyTo(array2, index);
		}

		bool IProducerConsumerCollection<T>.TryTake(out T item)
		{
			return TryPop(out item);
		}

		public void Push(T gparam_0)
		{
			Node node = new Node();
			node.Value = gparam_0;
			do
			{
				node.Next = head;
			}
			while (Interlocked.CompareExchange(ref head, node, node.Next) != node.Next);
			Interlocked.Increment(ref count);
		}

		public void PushRange(T[] gparam_0)
		{
			PushRange(gparam_0, 0, gparam_0.Length);
		}

		public void PushRange(T[] gparam_0, int int_0, int int_1)
		{
			Node node = null;
			Node node2 = null;
			for (int i = int_0; i < int_1; i++)
			{
				Node node3 = new Node();
				node3.Value = gparam_0[i];
				node3.Next = node;
				node = node3;
				if (node2 == null)
				{
					node2 = node3;
				}
			}
			do
			{
				node2.Next = head;
			}
			while (Interlocked.CompareExchange(ref head, node, node2.Next) != node2.Next);
			Interlocked.Add(ref int_1, int_1);
		}

		public bool TryPop(out T gparam_0)
		{
			while (true)
			{
				Node node = head;
				if (node == null)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref head, node.Next, node) == node)
				{
					Interlocked.Decrement(ref count);
					gparam_0 = node.Value;
					return true;
				}
			}
			gparam_0 = default(T);
			return false;
		}

		public int TryPopRange(T[] gparam_0)
		{
			if (gparam_0 == null)
			{
				throw new ArgumentNullException("items");
			}
			return TryPopRange(gparam_0, 0, gparam_0.Length);
		}

		public int TryPopRange(T[] gparam_0, int int_0, int int_1)
		{
			if (gparam_0 == null)
			{
				throw new ArgumentNullException("items");
			}
			if (int_0 >= 0 && int_0 < gparam_0.Length)
			{
				if (int_1 < 0)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (int_0 + int_1 > gparam_0.Length)
				{
					throw new ArgumentException("startIndex + count is greater than the length of items.");
				}
				Node next;
				Node node = null;
				do
				{
					next = head;
					if (next != null)
					{
						node = next;
						for (int i = 0; i < int_1; i++)
						{
							node = node.Next;
							if (node == null)
							{
								break;
							}
						}
						continue;
					}
					return -1;
				}
				while (Interlocked.CompareExchange(ref head, node, next) != next);
				int j;
				for (j = int_0; j < int_0 + int_1; j++)
				{
					if (next == null)
					{
						break;
					}
					gparam_0[j] = next.Value;
					node = next;
					next = next.Next;
				}
				Interlocked.Add(ref count, -(j - int_0));
				return j - int_0;
			}
			throw new ArgumentOutOfRangeException("startIndex");
		}

		public bool TryPeek(out T gparam_0)
		{
			Node node = head;
			if (node == null)
			{
				gparam_0 = default(T);
				return false;
			}
			gparam_0 = node.Value;
			return true;
		}

		public void Clear()
		{
			count = 0;
			head = null;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return InternalGetEnumerator();
		}

		private IEnumerator<T> InternalGetEnumerator()
		{
			Node my_head = head;
			if (my_head != null)
			{
				Node next;
				do
				{
					yield return my_head.Value;
					my_head = (next = my_head.Next);
				}
				while (next != null);
			}
		}

		public void CopyTo(T[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (index >= array.Length)
			{
				throw new ArgumentException("index is equals or greather than array length", "index");
			}
			IEnumerator<T> enumerator = InternalGetEnumerator();
			int num = index;
			while (true)
			{
				if (enumerator.MoveNext())
				{
					if (num == array.Length - index)
					{
						break;
					}
					array[num++] = enumerator.Current;
					continue;
				}
				return;
			}
			throw new ArgumentException("The number of elememts in the collection exceeds the capacity of array", "array");
		}

		public T[] ToArray()
		{
			return new List<T>(this).ToArray();
		}
	}
}
