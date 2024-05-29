using System.IO;
using UnityEngine;

internal class Tutorial3DArrowCreator
{
	private static Tutorial3DArrowCreator tutorial3DArrowCreator_0;

	public static Tutorial3DArrowCreator Tutorial3DArrowCreator_0
	{
		get
		{
			if (tutorial3DArrowCreator_0 == null)
			{
				tutorial3DArrowCreator_0 = new Tutorial3DArrowCreator();
			}
			return tutorial3DArrowCreator_0;
		}
	}

	public GameObject CreateArrow(string string_0, GameObject gameObject_0, Vector3 vector3_0)
	{
		string fileName = Path.GetFileName(string_0);
		GameObject gameObject = Resources.Load("Materials/Tutorial/" + fileName) as GameObject;
		if (gameObject == null)
		{
			return null;
		}
		Vector3 position = vector3_0;
		if ((bool)gameObject_0 && (bool)gameObject_0.gameObject.GetComponent<Renderer>())
		{
			position.y += gameObject_0.gameObject.GetComponent<Renderer>().bounds.size.y;
		}
		if (string_0.Equals("TargetZombieHead"))
		{
			position.y += 2f;
		}
		GameObject gameObject2 = (GameObject)Object.Instantiate(gameObject, position, Quaternion.identity);
		gameObject2.transform.localScale = new Vector3(1f, 1f, 1f);
		return gameObject2;
	}
}
