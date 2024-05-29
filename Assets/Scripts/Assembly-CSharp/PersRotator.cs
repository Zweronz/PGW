using System.Collections;
using Holoville.HOTween;
using UnityEngine;

public class PersRotator : MonoBehaviour
{
	public Transform pers;

	public float coeff = 0.5f;

	public float resetTime = 2f;

	public float animationTime = 1.5f;

	private Quaternion quaternion_0;

	private void Start()
	{
		StartCoroutine(InitPers());
	}

	private IEnumerator InitPers()
	{
		while (pers == null)
		{
			PersController persController = Object.FindObjectOfType<PersController>();
			if (persController != null)
			{
				GameObject gameObject = persController.transform.parent.gameObject;
				if (gameObject != null)
				{
					pers = gameObject.transform;
				}
			}
			yield return null;
		}
		quaternion_0 = pers.transform.localRotation;
	}

	private void OnDrag(Vector2 vector2_0)
	{
		if (pers != null)
		{
			pers.Rotate(new Vector3(0f, (0f - vector2_0.x) * coeff, 0f));
		}
		else
		{
			StartCoroutine(InitPers());
		}
	}

	private void OnPress(bool bool_0)
	{
		if (bool_0)
		{
			HOTween.Kill();
			CancelInvoke("Reset");
		}
		else
		{
			Invoke("Reset", resetTime);
		}
	}

	public void Reset()
	{
		HOTween.To(pers, animationTime, "localRotation", quaternion_0, false);
	}
}
