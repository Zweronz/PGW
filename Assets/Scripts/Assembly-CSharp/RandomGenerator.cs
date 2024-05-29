public class RandomGenerator
{
	private const uint uint_0 = 1842502087u;

	private const uint uint_1 = 1357980759u;

	private const uint uint_2 = 273326509u;

	private static uint uint_3;

	private uint uint_4;

	private uint uint_5;

	private uint uint_6;

	private uint uint_7;

	public RandomGenerator(uint uint_8)
	{
		SetSeed(uint_8);
	}

	public RandomGenerator()
	{
		SetSeed(uint_3++);
	}

	public uint GenerateUint()
	{
		uint num = uint_4 ^ (uint_4 << 11);
		uint_4 = uint_5;
		uint_5 = uint_6;
		uint_6 = uint_7;
		return uint_7 = uint_7 ^ (uint_7 >> 19) ^ (num ^ (num >> 8));
	}

	public int Range(int int_0)
	{
		return (int)(GenerateUint() % int_0);
	}

	public int Range(int int_0, int int_1)
	{
		return int_0 + (int)(GenerateUint() % (int_1 - int_0));
	}

	public float GenerateFloat()
	{
		return 2.3283064E-10f * (float)GenerateUint();
	}

	public float GenerateRangeFloat()
	{
		uint num = GenerateUint();
		return 4.656613E-10f * (float)(int)num;
	}

	public double GenerateDouble()
	{
		return 2.3283064370807974E-10 * (double)GenerateUint();
	}

	public double GenerateRangeDouble()
	{
		uint num = GenerateUint();
		return 4.656612874161595E-10 * (double)(int)num;
	}

	public void SetSeed(uint uint_8)
	{
		uint_4 = uint_8;
		uint_5 = uint_8 ^ 0x6DD259C7u;
		uint_6 = (uint_8 >> 5) ^ 0x50F12457u;
		uint_7 = (uint_8 >> 7) ^ 0x104AA1ADu;
		for (uint num = 0u; num < 4; num++)
		{
			uint_4 = GenerateUint();
		}
	}
}
