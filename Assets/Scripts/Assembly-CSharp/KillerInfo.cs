using UnityEngine;

public class KillerInfo
{
	public bool bool_0;

	public bool bool_1;

	public bool bool_2;

	public bool bool_3;

	public bool bool_4;

	public int int_0;

	public int int_1;

	public int int_2;

	public string string_0;

	public Texture texture_0;

	public string string_1;

	public Texture texture_1;

	public int int_3;

	public int int_4;

	public int int_5;

	public Texture texture_2;

	public int int_6;

	public Transform transform_0;

	public int int_7;

	public int int_8;

	public int int_9;

	public int int_10;

	public bool bool_5;

	public int int_11;

	public int int_12;

	public void CopyTo(KillerInfo killerInfo_0)
	{
		killerInfo_0.bool_0 = bool_0;
		killerInfo_0.bool_1 = bool_1;
		killerInfo_0.bool_2 = bool_2;
		killerInfo_0.bool_3 = bool_3;
		killerInfo_0.int_0 = int_0;
		killerInfo_0.bool_4 = bool_4;
		killerInfo_0.string_0 = string_0;
		killerInfo_0.texture_0 = texture_0;
		killerInfo_0.string_1 = string_1;
		killerInfo_0.texture_1 = texture_1;
		killerInfo_0.int_3 = int_3;
		killerInfo_0.int_4 = int_4;
		killerInfo_0.int_5 = int_5;
		killerInfo_0.texture_2 = texture_2;
		killerInfo_0.int_6 = int_6;
		killerInfo_0.transform_0 = transform_0;
		killerInfo_0.int_8 = int_8;
		killerInfo_0.int_7 = int_7;
		killerInfo_0.int_10 = int_10;
		killerInfo_0.int_9 = int_9;
		killerInfo_0.int_1 = int_1;
		killerInfo_0.bool_5 = bool_5;
		killerInfo_0.int_2 = int_2;
		killerInfo_0.int_11 = int_11;
		killerInfo_0.int_12 = int_12;
	}

	public void Reset()
	{
		bool_0 = false;
		bool_1 = false;
		bool_2 = false;
		bool_3 = false;
		bool_4 = false;
		int_1 = 0;
		int_0 = 0;
		bool_5 = false;
		int_2 = 0;
		string_0 = string.Empty;
		texture_0 = null;
		string_1 = string.Empty;
		texture_1 = null;
		int_3 = 0;
		int_4 = 0;
		int_5 = 0;
		texture_2 = null;
		int_6 = 0;
		transform_0 = null;
		int_8 = 0;
		int_7 = 0;
		int_10 = 0;
		int_9 = 0;
		int_11 = 1;
		int_12 = 0;
	}
}
