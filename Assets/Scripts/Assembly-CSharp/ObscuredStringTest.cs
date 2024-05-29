using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

public class ObscuredStringTest : MonoBehaviour
{
	internal string string_0;

	internal ObscuredString obscuredString_0;

	internal bool bool_0;

	private void Start()
	{
		Debug.Log("===== ObscuredStringTest =====\n");
		ObscuredString.SetNewCryptoKey("I LOVE MY GIRL");
		string_0 = "Try Goscurry! Or better buy it!";
		Debug.Log("Original string:\n" + string_0);
		obscuredString_0 = string_0;
		Debug.Log("How your string is stored in memory when obscured:\n" + obscuredString_0.GetEncrypted());
		obscuredString_0 = (string_0 = string.Empty);
	}

	public void UseRegular()
	{
		bool_0 = true;
		string_0 = "Hey, you can easily change me in memory!";
		obscuredString_0 = string.Empty;
		Debug.Log("Try to change this string in memory:\n" + string_0);
	}

	public void UseObscured()
	{
		bool_0 = false;
		obscuredString_0 = "Hey, you can't change me in memory!";
		string_0 = string.Empty;
		Debug.Log("Try to change this string in memory:\n" + obscuredString_0);
	}
}
