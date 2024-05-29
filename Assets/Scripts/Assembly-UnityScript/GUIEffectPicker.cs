using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class GUIEffectPicker : MonoBehaviour
{
	public Transform[] FX;

	public virtual void OnGUI()
	{
		for (int i = 0; i < FX.Length; i++)
		{
			if (GUI.Button(new Rect(120 * i, 0f, 120f, 80f), FX[i].gameObject.name))
			{
				(gameObject.GetComponent("ExplosionAtPoint") as ExplosionAtPoint).explosionPrefab = FX[i];
			}
		}
		Time.timeScale = GUI.HorizontalSlider(new Rect(0f, 130f, Screen.width, 10f), Time.timeScale, 0f, 15f);
	}

	public virtual void Main()
	{
	}
}
