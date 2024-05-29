using System.Collections.Generic;
using UnityEngine;
using engine.helpers;

public sealed class LocalMonsterData
{
	private const string string_0 = "Prefabs/Monsters/";

	private const string string_1 = "Audio/Monsters/";

	private const string string_2 = "SkinsTextures/MonstersSkins/";

	private static List<int> list_0 = new List<int>();

	private static Dictionary<int, GameObject> dictionary_0 = new Dictionary<int, GameObject>();

	private static Dictionary<string, AudioClip> dictionary_1 = new Dictionary<string, AudioClip>();

	private static Dictionary<string, Texture2D> dictionary_2 = new Dictionary<string, Texture2D>();

	public static int Int32_0
	{
		get
		{
			return list_0.Count;
		}
	}

	public static List<int> List_0
	{
		get
		{
			return list_0;
		}
	}

	public static void LoadMonsterData(int int_0)
	{
		MonsterData objectByKey = MonsterStorage.Get.Storage.GetObjectByKey(int_0);
		if (objectByKey != null)
		{
			InitMonster(objectByKey);
			InitAudio(objectByKey);
		}
	}

	public static void LoadMonsterData(CustomMonsterData customMonsterData_0)
	{
		if (customMonsterData_0 != null)
		{
			LoadMonsterData(customMonsterData_0.Int32_0);
			InitSkins(customMonsterData_0.String_0);
			list_0.Add(customMonsterData_0.Int32_0);
		}
	}

	private static void InitMonster(MonsterData monsterData_0)
	{
		if (!HaveMonster(monsterData_0.Int32_0))
		{
			GameObject gameObject = Resources.Load<GameObject>(monsterData_0.String_0);
			if (gameObject == null)
			{
				gameObject = Resources.Load<GameObject>("Prefabs/Monsters/" + monsterData_0.String_0);
			}
			if (gameObject == null)
			{
				Log.AddLine("[MonstersController::addMonster. Add prefab in cache error! Prefab name]: " + monsterData_0.String_0, LogType.Warning);
			}
			else
			{
				AddMonster(monsterData_0.Int32_0, gameObject);
			}
		}
	}

	private static void InitAudio(MonsterData monsterData_0)
	{
		List<string> list = new List<string>();
		if (!string.IsNullOrEmpty(monsterData_0.String_1))
		{
			list.Add(monsterData_0.String_1);
		}
		if (!string.IsNullOrEmpty(monsterData_0.String_2))
		{
			list.Add(monsterData_0.String_2);
		}
		if (!string.IsNullOrEmpty(monsterData_0.String_4))
		{
			list.Add(monsterData_0.String_4);
		}
		if (!string.IsNullOrEmpty(monsterData_0.String_3))
		{
			list.Add(monsterData_0.String_3);
		}
		for (int i = 0; i < list.Count; i++)
		{
			string text = list[i];
			if (!HaveAudio(text))
			{
				AudioClip audioClip = Resources.Load<AudioClip>(text);
				if (audioClip == null)
				{
					audioClip = Resources.Load<AudioClip>("Audio/Monsters/" + text);
				}
				if (audioClip != null)
				{
					AddAudio(text, audioClip);
				}
				else
				{
					Log.AddLine(string.Format("[MonstersController::addMonster] warning no AudioClip for name = {0} monsterId = {1}", text, monsterData_0.Int32_0), Log.LogLevel.WARNING);
				}
			}
		}
	}

	private static void InitSkins(string string_3)
	{
		if (!string.IsNullOrEmpty(string_3) && !HaveSkin(string_3))
		{
			Texture2D texture2D = Resources.Load(string_3) as Texture2D;
			if (texture2D == null)
			{
				texture2D = Resources.Load("SkinsTextures/MonstersSkins/" + string_3) as Texture2D;
			}
			if (texture2D == null)
			{
				Log.AddLine(string.Format("[MonstersController::addSkin] warning no skinTexture for name = {0}", string_3), Log.LogLevel.WARNING);
			}
			else
			{
				AddSkin(string_3, texture2D);
			}
		}
	}

	public static void Clear()
	{
		list_0.Clear();
		dictionary_0.Clear();
		dictionary_1.Clear();
		dictionary_2.Clear();
	}

	public static GameObject GetMosnter(int int_0)
	{
		if (dictionary_0.ContainsKey(int_0))
		{
			return dictionary_0[int_0];
		}
		return null;
	}

	public static AudioClip GetAudio(string string_3)
	{
		if (dictionary_1.ContainsKey(string_3))
		{
			return dictionary_1[string_3];
		}
		return null;
	}

	public static Texture2D GetSkin(string string_3)
	{
		if (dictionary_2.ContainsKey(string_3))
		{
			return dictionary_2[string_3];
		}
		return null;
	}

	private static void AddMonster(int int_0, GameObject gameObject_0)
	{
		if (!(gameObject_0 == null) && int_0 != 0)
		{
			if (dictionary_0.ContainsKey(int_0))
			{
				dictionary_0[int_0] = gameObject_0;
			}
			else
			{
				dictionary_0.Add(int_0, gameObject_0);
			}
		}
	}

	private static bool HaveMonster(int int_0)
	{
		return dictionary_0.ContainsKey(int_0);
	}

	private static void AddAudio(string string_3, AudioClip audioClip_0)
	{
		if (!(audioClip_0 == null) && !string.IsNullOrEmpty(string_3))
		{
			if (dictionary_1.ContainsKey(string_3))
			{
				dictionary_1[string_3] = audioClip_0;
			}
			else
			{
				dictionary_1.Add(string_3, audioClip_0);
			}
		}
	}

	private static bool HaveAudio(string string_3)
	{
		return dictionary_1.ContainsKey(string_3);
	}

	private static void AddSkin(string string_3, Texture2D texture2D_0)
	{
		if (!(texture2D_0 == null) && !string.IsNullOrEmpty(string_3))
		{
			if (dictionary_2.ContainsKey(string_3))
			{
				dictionary_2[string_3] = texture2D_0;
			}
			else
			{
				dictionary_2.Add(string_3, texture2D_0);
			}
		}
	}

	public static bool HaveSkin(string string_3)
	{
		return dictionary_2.ContainsKey(string_3);
	}
}
