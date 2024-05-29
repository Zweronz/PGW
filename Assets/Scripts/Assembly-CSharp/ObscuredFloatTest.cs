using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

public class ObscuredFloatTest : MonoBehaviour
{
	internal float float_0 = 11.4f;

	internal ObscuredFloat obscuredFloat_0 = 11.4f;

	internal bool bool_0 = true;

	private void Start()
	{
		Debug.Log("===== ObscuredFloatTest =====\n");
		ObscuredFloat.SetNewCryptoKey(404);
		float_0 = 99.9f;
		Debug.Log("Original health bar:\n" + float_0);
		obscuredFloat_0 = float_0;
		Debug.Log("How your health bar is stored in memory when obscured:\n" + obscuredFloat_0.GetEncrypted());
		float num = 100f;
		ObscuredFloat obscuredFloat = 60.3f;
		ObscuredFloat.SetNewCryptoKey(666);
		++obscuredFloat;
		obscuredFloat = (float)obscuredFloat - 2f;
		--obscuredFloat;
		obscuredFloat = num - (float)obscuredFloat;
		obscuredFloat_0 = (float_0 = (obscuredFloat = 0f));
	}

	public void UseRegular()
	{
		bool_0 = true;
		float_0 += Random.Range(-10f, 50f);
		obscuredFloat_0 = 11f;
		Debug.Log("Try to change this float in memory:\n" + float_0);
	}

	public void UseObscured()
	{
		bool_0 = false;
		obscuredFloat_0 = (float)obscuredFloat_0 + Random.Range(-10f, 50f);
		float_0 = 11f;
		Debug.Log("Try to change this float in memory:\n" + obscuredFloat_0);
	}
}
