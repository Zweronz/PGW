using System;
using UnityEngine;

namespace Rilisoft
{
	internal sealed class LoadingProgress
	{
		private readonly GUIStyle guistyle_0;

		private readonly Texture2D texture2D_0;

		private readonly Texture2D texture2D_1;

		private static LoadingProgress loadingProgress_0;

		public static LoadingProgress LoadingProgress_0
		{
			get
			{
				if (loadingProgress_0 == null)
				{
					loadingProgress_0 = new LoadingProgress();
				}
				return loadingProgress_0;
			}
		}

		private LoadingProgress()
		{
			texture2D_0 = Resources.Load<Texture2D>("line_shadow");
			texture2D_1 = Resources.Load<Texture2D>("line");
			guistyle_0 = new GUIStyle(GUI.skin.label)
			{
				alignment = TextAnchor.MiddleCenter,
				font = Resources.Load<Font>("04B_03"),
				fontSize = Convert.ToInt32(22f * Defs.Single_0),
				normal = new GUIStyleState
				{
					textColor = Color.black
				}
			};
		}

		public static void Unload()
		{
			if (loadingProgress_0 != null)
			{
				Resources.UnloadAsset(loadingProgress_0.texture2D_0);
				Resources.UnloadAsset(loadingProgress_0.texture2D_1);
				loadingProgress_0 = null;
			}
		}

		public void Draw(float float_0)
		{
			float num = Mathf.Clamp01(float_0);
			if (texture2D_0 != null)
			{
				float num2 = 1.8f * (float)texture2D_0.width * Defs.Single_0;
				float num3 = 1.8f * (float)texture2D_0.height * Defs.Single_0;
				Rect rect = new Rect(0.5f * ((float)Screen.width - num2), (float)Screen.height - (21f * Defs.Single_0 + num3), num2, num3);
				float num4 = num2 - 7.2f * Defs.Single_0;
				float width = num4 * num;
				float height = num3 - 7.2f * Defs.Single_0;
				if (texture2D_1 != null)
				{
					Rect position = new Rect(rect.xMin + 3.6f * Defs.Single_0, rect.yMin + 3.6f * Defs.Single_0, width, height);
					GUI.DrawTexture(position, texture2D_1);
				}
				GUI.DrawTexture(rect, texture2D_0);
				Rect position2 = rect;
				position2.yMin -= 1.8f * Defs.Single_0;
				position2.y += 1.8f * Defs.Single_0;
				int num5 = Mathf.RoundToInt(num * 100f);
				string text = string.Format("{0}%", num5);
				GUI.Label(position2, text, guistyle_0);
			}
		}
	}
}
