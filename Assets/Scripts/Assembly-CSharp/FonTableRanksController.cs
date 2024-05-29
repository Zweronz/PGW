using UnityEngine;
using engine.unity;

public class FonTableRanksController : MonoBehaviour
{
	public GameObject scoreHead;

	public GameObject countKillsHead;

	private void Start()
	{
		if (MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0 == ModeType.FLAG_CAPTURE)
		{
			float x = countKillsHead.transform.position.x;
			countKillsHead.transform.position = new Vector3(scoreHead.transform.position.x, countKillsHead.transform.position.y, countKillsHead.transform.position.z);
			scoreHead.transform.position = new Vector3(x, scoreHead.transform.position.y, scoreHead.transform.position.z);
			countKillsHead.GetComponent<UILabel>().String_0 = LocalizationStore.Get("Key_1006");
		}
		if (MonoSingleton<FightController>.Prop_0.ModeData_0.ModeType_0 == ModeType.TIME_BATTLE)
		{
			scoreHead.transform.position = new Vector3(countKillsHead.transform.position.x, scoreHead.transform.position.y, scoreHead.transform.position.z);
			countKillsHead.SetActive(false);
		}
	}
}
