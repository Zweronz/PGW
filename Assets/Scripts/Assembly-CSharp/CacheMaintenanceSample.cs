using System;
using BestHTTP.Caching;
using UnityEngine;

public sealed class CacheMaintenanceSample : MonoBehaviour
{
	private enum DeleteOlderTypes
	{
		Days = 0,
		Hours = 1,
		Mins = 2,
		Secs = 3
	}

	private DeleteOlderTypes deleteOlderTypes_0 = DeleteOlderTypes.Secs;

	private int int_0 = 10;

	private int int_1 = 5242880;

	private void OnGUI()
	{
		GUIHelper.DrawArea(GUIHelper.rect_0, true, delegate
		{
			GUILayout.BeginHorizontal();
			GUILayout.Label("Delete cached entities older then");
			GUILayout.Label(int_0.ToString(), GUILayout.MinWidth(50f));
			int_0 = (int)GUILayout.HorizontalSlider(int_0, 1f, 60f, GUILayout.MinWidth(100f));
			GUILayout.Space(10f);
			deleteOlderTypes_0 = (DeleteOlderTypes)GUILayout.SelectionGrid((int)deleteOlderTypes_0, new string[4] { "Days", "Hours", "Mins", "Secs" }, 4);
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.Space(10f);
			GUILayout.BeginHorizontal();
			GUILayout.Label("Max Cache Size (bytes): ", GUILayout.Width(150f));
			GUILayout.Label(int_1.ToString("N0"), GUILayout.Width(70f));
			int_1 = (int)GUILayout.HorizontalSlider(int_1, 1024f, 10485760f);
			GUILayout.EndHorizontal();
			GUILayout.Space(10f);
			if (GUILayout.Button("Maintenance"))
			{
				TimeSpan timeSpan_ = TimeSpan.FromDays(14.0);
				switch (deleteOlderTypes_0)
				{
				case DeleteOlderTypes.Days:
					timeSpan_ = TimeSpan.FromDays(int_0);
					break;
				case DeleteOlderTypes.Hours:
					timeSpan_ = TimeSpan.FromHours(int_0);
					break;
				case DeleteOlderTypes.Mins:
					timeSpan_ = TimeSpan.FromMinutes(int_0);
					break;
				case DeleteOlderTypes.Secs:
					timeSpan_ = TimeSpan.FromSeconds(int_0);
					break;
				}
				HTTPCacheService.BeginMaintainence(new HTTPCacheMaintananceParams(timeSpan_, (ulong)int_1));
			}
		});
	}
}
