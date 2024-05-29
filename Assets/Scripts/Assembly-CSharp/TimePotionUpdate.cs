using System;
using UnityEngine;

public class TimePotionUpdate : MonoBehaviour
{
	public UILabel myLabel;

	public SlotType slot;

	private float float_0 = -1f;

	private void Start()
	{
	}

	private void Update()
	{
		if (myLabel.enabled)
		{
			float_0 -= Time.deltaTime;
			if (float_0 <= 0f)
			{
				float_0 = 1f;
				SetTimeForLabel();
			}
		}
	}

	private void SetTimeForLabel()
	{
		int timeForSlot = ConsumablesController.ConsumablesController_0.GetTimeForSlot(slot);
		TimeSpan timeSpan = TimeSpan.FromSeconds(timeForSlot);
		myLabel.String_0 = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
		if (timeForSlot <= 5)
		{
			myLabel.Color_0 = new Color(1f, 0f, 0f);
		}
		else
		{
			myLabel.Color_0 = new Color(1f, 1f, 1f);
		}
	}

	private void OnEnable()
	{
		float_0 = -1f;
	}

	private void OnDisable()
	{
		myLabel.String_0 = string.Empty;
	}
}
