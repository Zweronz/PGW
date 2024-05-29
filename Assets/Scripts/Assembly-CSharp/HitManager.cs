using UnityEngine;

public class HitManager : MonoBehaviour
{
	public string id = "cube1";

	public int hits;

	private TextMesh textMesh_0;

	private void Start()
	{
		textMesh_0 = GetComponentInChildren<TextMesh>();
		if (!CryptoPlayerPrefs.HasKey(getPrefKey()))
		{
			SetHits(0);
		}
		else
		{
			SetHits(CryptoPlayerPrefs.GetInt(getPrefKey()));
		}
	}

	private void OnCollisionEnter(Collision collision_0)
	{
		SetHits(hits + 1);
	}

	private void SetHits(int int_0)
	{
		hits = int_0;
		Save();
		textMesh_0.text = int_0.ToString();
	}

	private void Save()
	{
		CryptoPlayerPrefs.SetInt(getPrefKey(), hits);
		PlayerPrefs.SetInt(getPrefKey(), hits);
		CryptoPlayerPrefs.Save();
	}

	private string getPrefKey()
	{
		return "cpp_test_hits_" + id;
	}
}
