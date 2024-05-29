using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class PBitStream
{
	private List<byte> list_0;

	private int int_0;

	private int int_1;

	[CompilerGenerated]
	private int int_2;

	public int Int32_0
	{
		get
		{
			return BytesForBits(int_1);
		}
	}

	public int Int32_1
	{
		get
		{
			return int_1;
		}
		private set
		{
			int_1 = value;
		}
	}

	public int Int32_2
	{
		[CompilerGenerated]
		get
		{
			return int_2;
		}
		[CompilerGenerated]
		set
		{
			int_2 = value;
		}
	}

	public PBitStream()
	{
		list_0 = new List<byte>(1);
	}

	public PBitStream(int int_3)
	{
		list_0 = new List<byte>(BytesForBits(int_3));
	}

	public PBitStream(IEnumerable<byte> ienumerable_0, int int_3)
	{
		list_0 = new List<byte>(ienumerable_0);
		Int32_1 = int_3;
	}

	public static int BytesForBits(int int_3)
	{
		if (int_3 <= 0)
		{
			return 0;
		}
		return (int_3 - 1) / 8 + 1;
	}

	public void Add(bool bool_0)
	{
		int num = int_1 / 8;
		if (num > list_0.Count - 1 || int_1 == 0)
		{
			list_0.Add(0);
		}
		if (bool_0)
		{
			int num2 = 7 - int_1 % 8;
			List<byte> list;
			List<byte> list2 = (list = list_0);
			int index;
			int index2 = (index = num);
			byte b = list[index];
			list2[index2] = (byte)(b | (byte)(1 << num2));
		}
		int_1++;
	}

	public byte[] ToBytes()
	{
		return list_0.ToArray();
	}

	public bool GetNext()
	{
		if (Int32_2 > int_1)
		{
			throw new Exception("End of PBitStream reached. Can't read more.");
		}
		return Get(Int32_2++);
	}

	public bool Get(int int_3)
	{
		int index = int_3 / 8;
		int num = 7 - int_3 % 8;
		return (list_0[index] & (byte)(1 << num)) > 0;
	}

	public void Set(int int_3, bool bool_0)
	{
		int num = int_3 / 8;
		int num2 = 7 - int_3 % 8;
		List<byte> list;
		List<byte> list2 = (list = list_0);
		int index;
		int index2 = (index = num);
		byte b = list[index];
		list2[index2] = (byte)(b | (byte)(1 << num2));
	}
}
