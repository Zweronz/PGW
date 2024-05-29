using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ByteReader
{
	private byte[] byte_0;

	private int int_0;

	private static BetterList<string> betterList_0 = new BetterList<string>();

	public bool Boolean_0
	{
		get
		{
			return byte_0 != null && int_0 < byte_0.Length;
		}
	}

	public ByteReader(byte[] byte_1)
	{
		byte_0 = byte_1;
	}

	public ByteReader(TextAsset textAsset_0)
	{
		byte_0 = textAsset_0.bytes;
	}

	public static ByteReader Open(string string_0)
	{
		FileStream fileStream = File.OpenRead(string_0);
		if (fileStream != null)
		{
			fileStream.Seek(0L, SeekOrigin.End);
			byte[] array = new byte[fileStream.Position];
			fileStream.Seek(0L, SeekOrigin.Begin);
			fileStream.Read(array, 0, array.Length);
			fileStream.Close();
			return new ByteReader(array);
		}
		return null;
	}

	private static string ReadLine(byte[] byte_1, int int_1, int int_2)
	{
		return Encoding.UTF8.GetString(byte_1, int_1, int_2);
	}

	public string ReadLine()
	{
		return ReadLine(true);
	}

	public string ReadLine(bool bool_0)
	{
		int num = byte_0.Length;
		if (bool_0)
		{
			while (int_0 < num && byte_0[int_0] < 32)
			{
				int_0++;
			}
		}
		int num2 = int_0;
		if (num2 < num)
		{
			int num3 = 0;
			do
			{
				if (num2 < num)
				{
					num3 = byte_0[num2++];
					continue;
				}
				num2++;
				break;
			}
			while (num3 != 10 && num3 != 13);
			string result = ReadLine(byte_0, int_0, num2 - int_0 - 1);
			int_0 = num2;
			return result;
		}
		int_0 = num;
		return null;
	}

	public Dictionary<string, string> ReadDictionary()
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		char[] separator = new char[1] { '=' };
		while (Boolean_0)
		{
			string text = ReadLine();
			if (text == null)
			{
				break;
			}
			if (!text.StartsWith("//"))
			{
				string[] array = text.Split(separator, 2, StringSplitOptions.RemoveEmptyEntries);
				if (array.Length == 2)
				{
					string key = array[0].Trim();
					string value = array[1].Trim().Replace("\\n", "\n");
					dictionary[key] = value;
				}
			}
		}
		return dictionary;
	}

	public BetterList<string> ReadCSV()
	{
		betterList_0.Clear();
		string text = string.Empty;
		bool flag = false;
		int num = 0;
		while (true)
		{
			if (Boolean_0)
			{
				if (flag)
				{
					string text2 = ReadLine(false);
					if (text2 == null)
					{
						return null;
					}
					text2 = text2.Replace("\\n", "\n");
					text = text + "\n" + text2;
					num++;
				}
				else
				{
					text = ReadLine(true);
					if (text == null)
					{
						break;
					}
					text = text.Replace("\\n", "\n");
					num = 0;
				}
				int i = num;
				for (int length = text.Length; i < length; i++)
				{
					switch (text[i])
					{
					case ',':
						if (!flag)
						{
							betterList_0.Add(text.Substring(num, i - num));
							num = i + 1;
						}
						break;
					case '"':
						if (flag)
						{
							if (i + 1 >= length)
							{
								betterList_0.Add(text.Substring(num, i - num).Replace("\"\"", "\""));
								return betterList_0;
							}
							if (text[i + 1] != '"')
							{
								betterList_0.Add(text.Substring(num, i - num).Replace("\"\"", "\""));
								flag = false;
								if (text[i + 1] == ',')
								{
									i++;
									num = i + 1;
								}
							}
							else
							{
								i++;
							}
						}
						else
						{
							num = i + 1;
							flag = true;
						}
						break;
					}
				}
				if (num < text.Length)
				{
					if (flag)
					{
						continue;
					}
					betterList_0.Add(text.Substring(num, text.Length - num));
				}
				return betterList_0;
			}
			return null;
		}
		return null;
	}
}
