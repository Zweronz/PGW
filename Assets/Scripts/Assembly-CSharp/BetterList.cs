using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BetterList<T>
{
	public delegate int CompareFunc(T gparam_0, T gparam_1);

	public T[] buffer;

	public int size;

	[DebuggerHidden]
	public T this[int i]
	{
		get
		{
			return buffer[i];
		}
		set
		{
			buffer[i] = value;
		}
	}

	[DebuggerStepThrough]
	public IEnumerator<T> GetEnumerator()
	{
		if (buffer != null)
		{
			for (int i = 0; i < size; i++)
			{
				yield return buffer[i];
			}
		}
	}

	private void AllocateMore()
	{
		T[] array = ((buffer == null) ? new T[32] : new T[Mathf.Max(buffer.Length << 1, 32)]);
		if (buffer != null && size > 0)
		{
			buffer.CopyTo(array, 0);
		}
		buffer = array;
	}

	private void Trim()
	{
		if (size > 0)
		{
			if (size < buffer.Length)
			{
				T[] array = new T[size];
				for (int i = 0; i < size; i++)
				{
					array[i] = buffer[i];
				}
				buffer = array;
			}
		}
		else
		{
			buffer = null;
		}
	}

	public void Clear()
	{
		size = 0;
	}

	public void Release()
	{
		size = 0;
		buffer = null;
	}

	public void Add(T gparam_0)
	{
		if (buffer == null || size == buffer.Length)
		{
			AllocateMore();
		}
		buffer[size++] = gparam_0;
	}

	public void Insert(int int_0, T gparam_0)
	{
		if (buffer == null || size == buffer.Length)
		{
			AllocateMore();
		}
		if (int_0 > -1 && int_0 < size)
		{
			for (int num = size; num > int_0; num--)
			{
				buffer[num] = buffer[num - 1];
			}
			buffer[int_0] = gparam_0;
			size++;
		}
		else
		{
			Add(gparam_0);
		}
	}

	public bool Contains(T gparam_0)
	{
		if (buffer == null)
		{
			return false;
		}
		int num = 0;
		while (true)
		{
			if (num < size)
			{
				if (buffer[num].Equals(gparam_0))
				{
					break;
				}
				num++;
				continue;
			}
			return false;
		}
		return true;
	}

	public int IndexOf(T gparam_0)
	{
		if (buffer == null)
		{
			return -1;
		}
		int num = 0;
		while (true)
		{
			if (num < size)
			{
				if (buffer[num].Equals(gparam_0))
				{
					break;
				}
				num++;
				continue;
			}
			return -1;
		}
		return num;
	}

	public bool Remove(T gparam_0)
	{
		if (buffer != null)
		{
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			for (int i = 0; i < size; i++)
			{
				if (@default.Equals(buffer[i], gparam_0))
				{
					size--;
					buffer[i] = default(T);
					for (int j = i; j < size; j++)
					{
						buffer[j] = buffer[j + 1];
					}
					buffer[size] = default(T);
					return true;
				}
			}
		}
		return false;
	}

	public void RemoveAt(int int_0)
	{
		if (buffer != null && int_0 > -1 && int_0 < size)
		{
			size--;
			buffer[int_0] = default(T);
			for (int i = int_0; i < size; i++)
			{
				buffer[i] = buffer[i + 1];
			}
			buffer[size] = default(T);
		}
	}

	public T Pop()
	{
		if (buffer != null && size != 0)
		{
			T result = buffer[--size];
			buffer[size] = default(T);
			return result;
		}
		return default(T);
	}

	public T[] ToArray()
	{
		Trim();
		return buffer;
	}

	[DebuggerStepThrough]
	[DebuggerHidden]
	public void Sort(CompareFunc compareFunc_0)
	{
		int num = 0;
		int num2 = size - 1;
		bool flag = true;
		while (flag)
		{
			flag = false;
			for (int i = num; i < num2; i++)
			{
				if (compareFunc_0(buffer[i], buffer[i + 1]) > 0)
				{
					T val = buffer[i];
					buffer[i] = buffer[i + 1];
					buffer[i + 1] = val;
					flag = true;
				}
				else if (!flag)
				{
					num = ((i != 0) ? (i - 1) : 0);
				}
			}
		}
	}
}
