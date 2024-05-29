using UnityEngine;

public class BestScoresStat : MonoBehaviour
{
	private void Start()
	{
		GetComponent<UILabel>().String_0 = Storager.GetInt(Defs.String_7).ToString();
	}
}
