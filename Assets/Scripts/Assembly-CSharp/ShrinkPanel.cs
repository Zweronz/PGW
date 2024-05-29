using UnityEngine;

public class ShrinkPanel : MonoBehaviour
{
	private const float float_0 = 0.2f;

	public ShrinkPanelObject[] Objects;

	private bool bool_0;

	private float float_1;

	private float float_2;

	private int int_0 = -1;

	private void Start()
	{
	}

	private void Update()
	{
		if (float_2 != 0f)
		{
			float num = Time.deltaTime / 0.2f * float_1;
			if (Mathf.Abs(num) > Mathf.Abs(float_2))
			{
				num = float_2;
			}
			float_2 -= num;
			Vector3 localPosition = Objects[int_0].body.transform.localPosition;
			Objects[int_0].body.transform.localPosition = new Vector3(localPosition.x, localPosition.y + num, localPosition.z);
			for (int i = int_0 + 1; i < Objects.Length; i++)
			{
				localPosition = Objects[i].panel.transform.localPosition;
				Objects[i].panel.transform.localPosition = new Vector3(localPosition.x, localPosition.y + num, localPosition.z);
			}
			if (float_2 == 0f)
			{
				float_1 = 0f;
				moveEnd();
			}
		}
	}

	private void moveEnd()
	{
		bool_0 = false;
		Objects[int_0].moveEnd();
		for (int i = 0; i < Objects.Length; i++)
		{
			Objects[i].panel.RebuildAllDrawCalls();
			Objects[i].panel.Refresh();
			Objects[i].panel.SetDirty();
		}
		base.gameObject.GetComponent<UIPanel>().RebuildAllDrawCalls();
		base.gameObject.GetComponent<UIPanel>().Refresh();
		base.gameObject.GetComponent<UIPanel>().SetDirty();
		int_0 = -1;
	}

	public void OnOpenButtonClick(ShrinkPanelObject shrinkPanelObject_0)
	{
		if (bool_0)
		{
			return;
		}
		for (int i = 0; i < Objects.Length; i++)
		{
			if (Objects[i].Equals(shrinkPanelObject_0))
			{
				int_0 = i;
				break;
			}
		}
		if (int_0 != -1)
		{
			bool_0 = true;
			float_1 = 0f - Objects[int_0].height;
			float_2 = float_1;
		}
	}

	public void OnCloseButtonClick(ShrinkPanelObject shrinkPanelObject_0)
	{
		if (bool_0)
		{
			return;
		}
		for (int i = 0; i < Objects.Length; i++)
		{
			if (Objects[i].Equals(shrinkPanelObject_0))
			{
				int_0 = i;
				break;
			}
		}
		if (int_0 != -1)
		{
			bool_0 = true;
			float_1 = Objects[int_0].height;
			float_2 = float_1;
		}
	}

	public void ForceOpenAll()
	{
		for (int i = 0; i < Objects.Length; i++)
		{
			if (!Objects[i]._isOpen)
			{
				float num = 0f - Objects[i].height;
				Vector3 localPosition = Objects[i].body.transform.localPosition;
				Objects[i].body.transform.localPosition = new Vector3(localPosition.x, localPosition.y + num, localPosition.z);
				for (int j = i + 1; j < Objects.Length; j++)
				{
					localPosition = Objects[j].panel.transform.localPosition;
					Objects[j].panel.transform.localPosition = new Vector3(localPosition.x, localPosition.y + num, localPosition.z);
				}
				Objects[i]._isOpen = true;
			}
			Objects[i].openButton.gameObject.SetActive(false);
			Objects[i].closeButton.gameObject.SetActive(true);
		}
	}
}
