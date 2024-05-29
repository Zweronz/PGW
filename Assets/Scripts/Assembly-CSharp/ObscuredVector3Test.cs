using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

public class ObscuredVector3Test : MonoBehaviour
{
	internal Vector3 vector3_0 = new Vector3(10.5f, 11.5f, 12.5f);

	internal ObscuredVector3 obscuredVector3_0 = new Vector3(10.5f, 11.5f, 12.5f);

	internal bool bool_0 = true;

	private void Start()
	{
		Debug.Log("===== ObscuredVector3Test =====\n");
		ObscuredVector3.SetNewCryptoKey(404);
		vector3_0 = new Vector3(54.1f, 64.3f, 63.2f);
		Debug.Log("Original position:\n" + vector3_0);
		obscuredVector3_0 = vector3_0;
		ObscuredVector3.RawEncryptedVector3 encrypted = obscuredVector3_0.GetEncrypted();
		Debug.Log("How your position is stored in memory when obscured:\n(" + encrypted.x + ", " + encrypted.y + ", " + encrypted.z + ")");
	}

	public void UseRegular()
	{
		bool_0 = true;
		vector3_0 += new Vector3(Random.Range(-10f, 50f), Random.Range(-10f, 50f), Random.Range(-10f, 50f));
		obscuredVector3_0 = new Vector3(10.5f, 11.5f, 12.5f);
		Debug.Log("Try to change this Vector3 in memory:\n" + vector3_0);
	}

	public void UseObscured()
	{
		bool_0 = false;
		obscuredVector3_0 += new Vector3(Random.Range(-10f, 50f), Random.Range(-10f, 50f), Random.Range(-10f, 50f));
		vector3_0 = new Vector3(10.5f, 11.5f, 12.5f);
		Debug.Log("Try to change this Vector3 in memory:\n" + obscuredVector3_0);
	}
}
