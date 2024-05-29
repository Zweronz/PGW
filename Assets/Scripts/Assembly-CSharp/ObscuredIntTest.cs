using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

public class ObscuredIntTest : MonoBehaviour
{
	internal int int_0 = 11;

	internal ObscuredInt obscuredInt_0 = 11;

	internal bool bool_0;

	private void Start()
	{
		Debug.Log("===== ObscuredIntTest =====\n");
		int_0 = 5;
		Debug.Log("Original lives count:\n" + int_0);
		obscuredInt_0 = int_0;
		Debug.Log("How your lives count is stored in memory when obscured:\n" + obscuredInt_0.GetEncrypted());
		ObscuredInt.SetNewCryptoKey(666);
		ObscuredInt obscuredInt = 100;
		obscuredInt = (int)obscuredInt - 10;
		obscuredInt = (int)obscuredInt + 100;
		obscuredInt = (int)obscuredInt / 10;
		ObscuredInt.SetNewCryptoKey(888);
		++obscuredInt;
		ObscuredInt.SetNewCryptoKey(999);
		++obscuredInt;
		--obscuredInt;
		Debug.Log(string.Concat("Lives count: ", obscuredInt, " (", obscuredInt.ToString("X"), "h)"));
	}

	public void UseRegular()
	{
		bool_0 = true;
		int_0 += Random.Range(-10, 50);
		obscuredInt_0 = 11;
		Debug.Log("Try to change this int in memory:\n" + int_0);
	}

	public void UseObscured()
	{
		bool_0 = false;
		obscuredInt_0 = (int)obscuredInt_0 + Random.Range(-10, 50);
		int_0 = 11;
		Debug.Log("Try to change this int in memory:\n" + obscuredInt_0);
	}
}
