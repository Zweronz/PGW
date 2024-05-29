using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

internal sealed class HighAssetsLoader : MonoBehaviour
{
	public static readonly string string_0 = "Lightmap";

	public static readonly string string_1 = "High";

	public static readonly string string_2 = "Atlas";

	[CompilerGenerated]
	private static Comparison<Texture2D> comparison_0;

	[CompilerGenerated]
	private static Comparison<Material> comparison_1;

	[CompilerGenerated]
	private static Comparison<Texture2D> comparison_2;

	private void Start()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	private void OnLevelWasLoaded(int int_0)
	{
		BotHealth.bool_0 = false;
		BotMovement.bool_0 = false;
		ZombiUpravlenie.bool_0 = false;
		string path = ResPath.Combine(ResPath.Combine(string_0, string_1), Application.loadedLevelName);
		string path2 = ResPath.Combine(ResPath.Combine(string_2, string_1), Application.loadedLevelName);
		Texture2D[] array = Resources.LoadAll<Texture2D>(path);
		if (array != null && array.Length > 0)
		{
			List<Texture2D> list = new List<Texture2D>();
			Texture2D[] array2 = array;
			foreach (Texture2D item in array2)
			{
				list.Add(item);
			}
			list.Sort((Texture2D texture2D_0, Texture2D texture2D_1) => texture2D_0.name.CompareTo(texture2D_1.name));
			LightmapData lightmapData = new LightmapData();
			lightmapData.lightmapColor = list[0];
			List<LightmapData> list2 = new List<LightmapData>();
			list2.Add(lightmapData);
			LightmapSettings.lightmaps = list2.ToArray();
		}
		Texture2D[] array3 = Resources.LoadAll<Texture2D>(path2);
		string value = Application.loadedLevelName + "_Atlas";
		if (array3 == null || array3.Length <= 0)
		{
			return;
		}
		UnityEngine.Object[] array4 = UnityEngine.Object.FindObjectsOfType(typeof(Renderer));
		List<Material> list3 = new List<Material>();
		UnityEngine.Object[] array5 = array4;
		for (int j = 0; j < array5.Length; j++)
		{
			Renderer renderer = (Renderer)array5[j];
			if (renderer != null && renderer.sharedMaterial != null && renderer.sharedMaterial.name != null && renderer.sharedMaterial.name.Contains(value) && !list3.Contains(renderer.sharedMaterial))
			{
				list3.Add(renderer.sharedMaterial);
			}
		}
		List<Texture2D> list4 = new List<Texture2D>();
		Texture2D[] array6 = array3;
		foreach (Texture2D item2 in array6)
		{
			list4.Add(item2);
		}
		list3.Sort((Material material_0, Material material_1) => material_0.name.CompareTo(material_1.name));
		list4.Sort((Texture2D texture2D_0, Texture2D texture2D_1) => texture2D_0.name.CompareTo(texture2D_1.name));
		for (int l = 0; l < Mathf.Min(list3.Count, list4.Count); l++)
		{
			list3[l].mainTexture = list4[l];
		}
	}
}
