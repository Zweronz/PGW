using UnityEngine;
using engine.events;

public class TutorialWayPointArrow
{
	private static TutorialWayPointArrow tutorialWayPointArrow_0;

	private GameObject gameObject_0;

	private GameObject gameObject_1;

	private bool bool_0;

	public static TutorialWayPointArrow TutorialWayPointArrow_0
	{
		get
		{
			if (tutorialWayPointArrow_0 == null)
			{
				tutorialWayPointArrow_0 = new TutorialWayPointArrow();
			}
			return tutorialWayPointArrow_0;
		}
	}

	public void InitArrow()
	{
		GameObject original = Resources.Load<GameObject>("Materials/Tutorial/ArrowNavigate");
		gameObject_0 = Object.Instantiate(original, Vector3.zero, Quaternion.identity) as GameObject;
		gameObject_0.GetComponent<Renderer>().material.SetColor("_ColorRili", new Color(0f, 0f, 0f, 0f));
		gameObject_0.SetActive(false);
	}

	public void ReInitArrow()
	{
		InitArrow();
		SetArrowActive(true);
	}

	public void SetArrowActive(bool bool_1)
	{
		if (gameObject_0 == null)
		{
			InitArrow();
		}
		gameObject_0.SetActive(bool_1);
		bool_0 = bool_1;
		if (!bool_1)
		{
			gameObject_0.transform.parent = null;
			if (!DependSceneEvent<MainUpdate>.Contains(Update))
			{
				DependSceneEvent<MainUpdate>.GlobalUnsubscribe(Update);
			}
			return;
		}
		gameObject_0.transform.parent = GameObject.FindGameObjectWithTag("MainCamera").transform;
		gameObject_0.transform.localPosition = new Vector3(0f, 0.18f, 0.51f);
		if (!DependSceneEvent<MainUpdate>.Contains(Update))
		{
			DependSceneEvent<MainUpdate>.GlobalSubscribe(Update, true);
		}
	}

	public void SetTarget(GameObject gameObject_2)
	{
		gameObject_1 = gameObject_2;
	}

	private void Update()
	{
		if (gameObject_0 == null)
		{
			ReInitArrow();
		}
		Quaternion quaternion = Quaternion.FromToRotation(Camera.main.ScreenPointToRay(new Vector3((float)Screen.width * 0.5f, (float)Screen.height * 0.5f, 0f)).direction, gameObject_0.transform.forward);
		float num = 0f;
		GameObject gameObject = GameObject.FindGameObjectWithTag("TutorialArrow");
		if (gameObject == null)
		{
			gameObject = GameObject.FindGameObjectWithTag("Portal");
		}
		if (gameObject == null)
		{
			gameObject = GameObject.FindGameObjectWithTag("Enemy");
		}
		if (gameObject != null)
		{
			num = Vector3.Distance(gameObject.transform.position, WeaponManager.weaponManager_0.myPlayer.transform.position);
		}
		if (gameObject_0 != null && gameObject_1 != null)
		{
			Vector3 position = gameObject_1.transform.position;
			if (WeaponManager.weaponManager_0.myPlayer.transform.position.y >= gameObject_1.transform.position.y)
			{
				position.y += 1f;
				if (num <= 2.5f)
				{
					position.y += Mathf.Min(2.5f - num, 0.5f);
				}
			}
			gameObject_0.transform.LookAt(position);
		}
		if ((quaternion.y > -0.3f && quaternion.y < 0.3f && quaternion.z > -0.3f && quaternion.z < 0.3f) || num <= 2.5f)
		{
			gameObject_0.GetComponent<Renderer>().material.SetColor("_ColorRili", new Color(0f, 1f, 0f, 1f));
		}
		else
		{
			gameObject_0.GetComponent<Renderer>().material.SetColor("_ColorRili", new Color(1f, 0f, 0f, 1f));
		}
	}

	public void DestroyArrow()
	{
		Object.Destroy(gameObject_0);
		gameObject_0 = null;
	}
}
