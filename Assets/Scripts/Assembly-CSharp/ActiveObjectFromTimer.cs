using UnityEngine;

public class ActiveObjectFromTimer : MonoBehaviour
{
	public float Period;

	public bool IsDestroy;

	public GameObject ActivateObject;

	private float float_0;

	protected virtual void Awake()
	{
		if (ActivateObject == null)
		{
			ActivateObject = base.gameObject;
		}
		float_0 = Period;
	}

	protected virtual void Update()
	{
		if (Period <= 0f)
		{
			return;
		}
		float_0 -= Time.deltaTime;
		if (!(float_0 > 0f))
		{
			if (IsDestroy)
			{
				Period = 0f;
				DestroyObject();
			}
			else
			{
				float_0 = Period;
				SetActiveObject(!ActivateObject.activeSelf);
			}
		}
	}

	protected virtual void DestroyObject()
	{
		Object.Destroy(ActivateObject);
	}

	protected virtual void SetActiveObject(bool bool_0)
	{
		ActivateObject.SetActive(bool_0);
	}
}
