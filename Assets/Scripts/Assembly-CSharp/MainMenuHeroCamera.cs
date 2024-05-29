using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MainMenuHeroCamera : MonoBehaviour
{
	[CompilerGenerated]
	private bool bool_0;

	private float Single_0
	{
		get
		{
			return -88f;
		}
	}

	public bool Boolean_0
	{
		[CompilerGenerated]
		get
		{
			return bool_0;
		}
		[CompilerGenerated]
		private set
		{
			bool_0 = value;
		}
	}

	public void Start()
	{
		Vector3 eulerAngles = base.transform.rotation.eulerAngles;
		base.transform.rotation = Quaternion.Euler(new Vector3(eulerAngles.x, Single_0, eulerAngles.z));
	}

	public void OnMainMenuOpenOptions()
	{
		PlayAnim(-72f);
	}

	public void OnMainMenuCloseOptions()
	{
		PlayAnim(Single_0);
	}

	public void OnMainMenuOpenLeaderboards()
	{
		PlayAnim(0f);
	}

	public void OnMainMenuCloseLeaderboards()
	{
		PlayAnim(6f);
	}

	private void PlayAnim(float float_0)
	{
		StopAllCoroutines();
		StartCoroutine(PlayAnimCoroutine(float_0));
	}

	private IEnumerator PlayAnimCoroutine(float float_0)
	{
		Boolean_0 = true;
		Transform transform = base.transform;
		Quaternion rotation = transform.rotation;
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		Quaternion from = rotation;
		Quaternion quaternion = Quaternion.Euler(rotation.eulerAngles.x, float_0, rotation.eulerAngles.z);
		while (Time.realtimeSinceStartup - realtimeSinceStartup <= 1f)
		{
			float num = Time.realtimeSinceStartup - realtimeSinceStartup;
			if (num <= 0.1f)
			{
				yield return null;
			}
			transform.rotation = Quaternion.Lerp(from, quaternion, (num - 0.1f) / 0.9f);
			yield return null;
		}
		transform.rotation = quaternion;
		Boolean_0 = false;
	}
}
