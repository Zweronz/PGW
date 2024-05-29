using UnityEngine;

public static class ImageLoader
{
	private static readonly string string_0 = "UI/Levels/Level{0}.pngts=1";

	private static readonly string[] string_1 = new string[5] { "LevelLoadingsPreview/", "LevelLoadingsPreview/Hi/", "LevelLoadings/Hi/", "LevelLoadings/", "LevelLoadingsSmall/" };

	public static Texture LoadArtikulTexture(int int_0)
	{
		ArtikulData artikul = ArtikulController.ArtikulController_0.GetArtikul(int_0);
		if (artikul == null)
		{
			return null;
		}
		return LoadArtikulTexture(artikul);
	}

	public static Texture LoadArtikulTexture(ArtikulData artikulData_0)
	{
		if (artikulData_0 == null)
		{
			return null;
		}
		return Resources.Load<Texture>("OfferIcons/" + artikulData_0.String_2);
	}

	public static Texture LoadMapPreviewTexture(MapData mapData_0)
	{
		if (mapData_0 == null)
		{
			return null;
		}
		Texture texture = Resources.Load<Texture>(mapData_0.String_2);
		if (texture != null)
		{
			return texture;
		}
		int num = 0;
		while (texture == null && num < string_1.Length)
		{
			string path = string.Format("{0}Loading_{1}", string_1[num], mapData_0.String_0);
			texture = Resources.Load<Texture>(path);
			num++;
		}
		return texture;
	}

	public static Texture LoadTexture(string string_2)
	{
		return Resources.Load<Texture>(string_2);
	}
}
