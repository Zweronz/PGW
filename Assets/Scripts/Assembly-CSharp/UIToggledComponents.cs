using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIToggle))]
public class UIToggledComponents : MonoBehaviour
{
	public List<MonoBehaviour> activate;

	public List<MonoBehaviour> deactivate;

	[SerializeField]
	private MonoBehaviour monoBehaviour_0;

	[SerializeField]
	private bool bool_0;

	private void Awake()
	{
		if (monoBehaviour_0 != null)
		{
			if (activate.Count == 0 && deactivate.Count == 0)
			{
				if (bool_0)
				{
					deactivate.Add(monoBehaviour_0);
				}
				else
				{
					activate.Add(monoBehaviour_0);
				}
			}
			else
			{
				monoBehaviour_0 = null;
			}
		}
		UIToggle component = GetComponent<UIToggle>();
		EventDelegate.Add(component.list_0, Toggle);
	}

	public void Toggle()
	{
		if (base.enabled)
		{
			for (int i = 0; i < activate.Count; i++)
			{
				MonoBehaviour monoBehaviour = activate[i];
				monoBehaviour.enabled = UIToggle.uitoggle_0.Boolean_0;
			}
			for (int j = 0; j < deactivate.Count; j++)
			{
				MonoBehaviour monoBehaviour2 = deactivate[j];
				monoBehaviour2.enabled = !UIToggle.uitoggle_0.Boolean_0;
			}
		}
	}
}
