using System.Collections;
using UnityEngine;

public class BombsCratorNucl : MonoBehaviour
{
	public float time = 120f;

	public GameObject bmb;

	public int oldBombPeriod;

	private IEnumerator Start()
	{
		if (Defs.bool_2)
		{
			oldBombPeriod = (int)PhotonNetwork.Double_0 / (int)time;
			while (true)
			{
				if (oldBombPeriod < (int)PhotonNetwork.Double_0 / (int)time)
				{
					oldBombPeriod = (int)PhotonNetwork.Double_0 / (int)time;
					Object.Instantiate(bmb);
				}
				yield return null;
			}
		}
		while (true)
		{
			yield return new WaitForSeconds(time);
			if (bmb != null)
			{
				Object.Instantiate(bmb);
			}
		}
	}
}
