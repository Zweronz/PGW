using System.Collections.Generic;
using UnityEngine;

public class UIItemStorage : MonoBehaviour
{
	public int maxItemCount = 8;

	public int maxRows = 4;

	public int maxColumns = 4;

	public GameObject template;

	public UIWidget background;

	public int spacing = 128;

	public int padding = 10;

	private List<InvGameItem> list_0 = new List<InvGameItem>();

	public List<InvGameItem> List_0
	{
		get
		{
			while (list_0.Count < maxItemCount)
			{
				list_0.Add(null);
			}
			return list_0;
		}
	}

	public InvGameItem GetItem(int int_0)
	{
		return (int_0 >= List_0.Count) ? null : list_0[int_0];
	}

	public InvGameItem Replace(int int_0, InvGameItem invGameItem_0)
	{
		if (int_0 < maxItemCount)
		{
			InvGameItem result = List_0[int_0];
			list_0[int_0] = invGameItem_0;
			return result;
		}
		return invGameItem_0;
	}

	private void Start()
	{
		if (!(template != null))
		{
			return;
		}
		int num = 0;
		Bounds bounds = default(Bounds);
		int num2 = 0;
		while (true)
		{
			if (num2 < maxRows)
			{
				for (int i = 0; i < maxColumns; i++)
				{
					GameObject gameObject = NGUITools.AddChild(base.gameObject, template);
					Transform transform = gameObject.transform;
					transform.localPosition = new Vector3((float)padding + ((float)i + 0.5f) * (float)spacing, (float)(-padding) - ((float)num2 + 0.5f) * (float)spacing, 0f);
					UIStorageSlot component = gameObject.GetComponent<UIStorageSlot>();
					if (component != null)
					{
						component.uiitemStorage_0 = this;
						component.int_0 = num;
					}
					bounds.Encapsulate(new Vector3((float)padding * 2f + (float)((i + 1) * spacing), (float)(-padding) * 2f - (float)((num2 + 1) * spacing), 0f));
					if (++num >= maxItemCount)
					{
						if (background != null)
						{
							background.transform.localScale = bounds.size;
						}
						return;
					}
				}
				num2++;
				continue;
			}
			if (background != null)
			{
				background.transform.localScale = bounds.size;
			}
			break;
		}
	}
}
