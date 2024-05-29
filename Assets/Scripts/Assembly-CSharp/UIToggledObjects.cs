using System.Collections.Generic;
using UnityEngine;

public class UIToggledObjects : MonoBehaviour
{
	public List<GameObject> activate;

	public List<GameObject> deactivate;

	[SerializeField]
	private GameObject gameObject_0;

	[SerializeField]
	private bool bool_0;

	private void Awake()
	{
		if (gameObject_0 != null)
		{
			if (activate.Count == 0 && deactivate.Count == 0)
			{
				if (bool_0)
				{
					deactivate.Add(gameObject_0);
				}
				else
				{
					activate.Add(gameObject_0);
				}
			}
			else
			{
				gameObject_0 = null;
			}
		}
		UIToggle component = GetComponent<UIToggle>();
		EventDelegate.Add(component.list_0, Toggle);
	}

	public void Toggle()
	{
		bool boolean_ = UIToggle.uitoggle_0.Boolean_0;
		if (base.enabled)
		{
			for (int i = 0; i < activate.Count; i++)
			{
				Set(activate[i], boolean_);
			}
			for (int j = 0; j < deactivate.Count; j++)
			{
				Set(deactivate[j], !boolean_);
			}
		}
	}

	private void Set(GameObject gameObject_1, bool bool_1)
	{
		if (gameObject_1 != null)
		{
			NGUITools.SetActive(gameObject_1, bool_1);
		}
	}
}
