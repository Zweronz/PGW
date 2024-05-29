using System.Collections.Generic;
using UnityEngine;

public class InvDatabase : MonoBehaviour
{
	private static InvDatabase[] invDatabase_0;

	private static bool bool_0 = true;

	public int databaseID;

	public List<InvBaseItem> items = new List<InvBaseItem>();

	public UIAtlas iconAtlas;

	public static InvDatabase[] InvDatabase_0
	{
		get
		{
			if (bool_0)
			{
				bool_0 = false;
				invDatabase_0 = NGUITools.FindActive<InvDatabase>();
			}
			return invDatabase_0;
		}
	}

	private void OnEnable()
	{
		bool_0 = true;
	}

	private void OnDisable()
	{
		bool_0 = true;
	}

	private InvBaseItem GetItem(int int_0)
	{
		int num = 0;
		int count = items.Count;
		InvBaseItem invBaseItem;
		while (true)
		{
			if (num < count)
			{
				invBaseItem = items[num];
				if (invBaseItem.id16 == int_0)
				{
					break;
				}
				num++;
				continue;
			}
			return null;
		}
		return invBaseItem;
	}

	private static InvDatabase GetDatabase(int int_0)
	{
		int num = 0;
		int num2 = InvDatabase_0.Length;
		InvDatabase invDatabase;
		while (true)
		{
			if (num < num2)
			{
				invDatabase = InvDatabase_0[num];
				if (invDatabase.databaseID == int_0)
				{
					break;
				}
				num++;
				continue;
			}
			return null;
		}
		return invDatabase;
	}

	public static InvBaseItem FindByID(int int_0)
	{
		InvDatabase database = GetDatabase(int_0 >> 16);
		return (!(database != null)) ? null : database.GetItem(int_0 & 0xFFFF);
	}

	public static InvBaseItem FindByName(string string_0)
	{
		int i = 0;
		for (int num = InvDatabase_0.Length; i < num; i++)
		{
			InvDatabase invDatabase = InvDatabase_0[i];
			int j = 0;
			for (int count = invDatabase.items.Count; j < count; j++)
			{
				InvBaseItem invBaseItem = invDatabase.items[j];
				if (invBaseItem.name == string_0)
				{
					return invBaseItem;
				}
			}
		}
		return null;
	}

	public static int FindItemID(InvBaseItem invBaseItem_0)
	{
		int num = 0;
		int num2 = InvDatabase_0.Length;
		InvDatabase invDatabase;
		while (true)
		{
			if (num < num2)
			{
				invDatabase = InvDatabase_0[num];
				if (invDatabase.items.Contains(invBaseItem_0))
				{
					break;
				}
				num++;
				continue;
			}
			return -1;
		}
		return (invDatabase.databaseID << 16) | invBaseItem_0.id16;
	}
}
