using System.Collections.Generic;
using UnityEngine;
using engine.helpers;

public class SkinEditorCanvas : MonoBehaviour
{
	public UITexture canvasTexture;

	public UISprite grid;

	public int scale = 32;

	private Camera camera_0;

	private float float_0;

	private float float_1;

	private float float_2;

	private float float_3;

	private Texture2D texture2D_0;

	private bool bool_0;

	private Color color_0;

	private Color color_1;

	private readonly Dictionary<SkinEditorToolItem.ToolType, KeyValuePair<int, int>> dictionary_0 = new Dictionary<SkinEditorToolItem.ToolType, KeyValuePair<int, int>>
	{
		{
			SkinEditorToolItem.ToolType.PENCIL,
			new KeyValuePair<int, int>(0, 34)
		},
		{
			SkinEditorToolItem.ToolType.BRUSH,
			new KeyValuePair<int, int>(0, 39)
		},
		{
			SkinEditorToolItem.ToolType.POURING,
			new KeyValuePair<int, int>(0, 36)
		},
		{
			SkinEditorToolItem.ToolType.DROPPER,
			new KeyValuePair<int, int>(0, 38)
		},
		{
			SkinEditorToolItem.ToolType.ERASER,
			new KeyValuePair<int, int>(0, 40)
		}
	};

	public void Start()
	{
		camera_0 = NGUITools.FindCameraForLayer(canvasTexture.gameObject.layer);
		OnGridToggleChanged();
		SkinEditController.SkinEditController_0.Subscribe(OnGridToggleChanged, SkinEditController.SkinEditorEvent.GRID_CHANGED);
	}

	private void OnDestroy()
	{
		SkinEditController.SkinEditController_0.Unsubscribe(OnGridToggleChanged, SkinEditController.SkinEditorEvent.GRID_CHANGED);
	}

	public void SetCanvas(Texture texture_0)
	{
		canvasTexture.Texture_0 = texture_0;
		canvasTexture.Int32_0 = texture_0.width * scale;
		canvasTexture.Int32_1 = texture_0.height * scale;
		texture2D_0 = (Texture2D)canvasTexture.Texture_0;
		UpdateParams();
		UpdateGrid();
	}

	public void Clear()
	{
		FillCanvas(Color.white, true);
	}

	private void UpdateParams()
	{
		float_0 = canvasTexture.Vector2_4.x / 2f;
		float_1 = canvasTexture.Vector2_4.y / 2f;
		float_2 = (float)canvasTexture.Int32_0 / (float)canvasTexture.Texture_0.width;
		float_3 = (float)canvasTexture.Int32_1 / (float)canvasTexture.Texture_0.height;
	}

	private void UpdateGrid()
	{
		grid.Int32_0 = canvasTexture.Int32_0;
		grid.Int32_1 = canvasTexture.Int32_1;
		grid.transform.localPosition = Vector3.zero;
	}

	private void OnPress(bool bool_1)
	{
		if (bool_1)
		{
			bool_0 = false;
			EditProcess();
		}
		else if (bool_0)
		{
			SkinEditController.SkinEditController_0.AddToHistory(Utility.CopyTexture(texture2D_0));
		}
	}

	private void OnDrag(Vector2 vector2_0)
	{
		if (SkinEditController.SkinEditController_0.toolType_0 != SkinEditorToolItem.ToolType.POURING)
		{
			EditProcess();
		}
	}

	private void EditProcess()
	{
		Vector3 v = camera_0.ScreenToWorldPoint(Input.mousePosition);
		Vector3 vector = canvasTexture.transform.worldToLocalMatrix.MultiplyPoint3x4(v);
		vector.x += float_0;
		vector.y += float_1;
		vector.x += dictionary_0[SkinEditController.SkinEditController_0.toolType_0].Key;
		vector.y -= dictionary_0[SkinEditController.SkinEditController_0.toolType_0].Value;
		int int_ = Mathf.FloorToInt(vector.x / float_2);
		int int_2 = Mathf.FloorToInt(vector.y / float_3);
		if (IsCollisionCanvas(int_, int_2))
		{
			EditCanvas(int_, int_2);
		}
	}

	private bool IsCollisionCanvas(int int_0, int int_1)
	{
		if (int_0 >= 0 && int_0 <= canvasTexture.Texture_0.width - 1 && int_1 >= 0 && int_1 <= canvasTexture.Texture_0.height - 1)
		{
			return true;
		}
		return false;
	}

	private void EditCanvas(int int_0, int int_1)
	{
		Color pixel = texture2D_0.GetPixel(int_0, int_1);
		switch (SkinEditController.SkinEditController_0.toolType_0)
		{
		case SkinEditorToolItem.ToolType.PENCIL:
			if (!pixel.Equals(SkinEditController.SkinEditController_0.color_0))
			{
				texture2D_0.SetPixel(int_0, int_1, SkinEditController.SkinEditController_0.color_0);
				bool_0 = true;
			}
			break;
		case SkinEditorToolItem.ToolType.BRUSH:
			texture2D_0.SetPixel(int_0, int_1, SkinEditController.SkinEditController_0.color_0);
			if (int_0 > 0)
			{
				texture2D_0.SetPixel(int_0 - 1, int_1, SkinEditController.SkinEditController_0.color_0);
			}
			if (int_0 < texture2D_0.width - 1)
			{
				texture2D_0.SetPixel(int_0 + 1, int_1, SkinEditController.SkinEditController_0.color_0);
			}
			if (int_1 > 0)
			{
				texture2D_0.SetPixel(int_0, int_1 - 1, SkinEditController.SkinEditController_0.color_0);
			}
			if (int_1 < texture2D_0.height - 1)
			{
				texture2D_0.SetPixel(int_0, int_1 + 1, SkinEditController.SkinEditController_0.color_0);
			}
			bool_0 = true;
			break;
		case SkinEditorToolItem.ToolType.POURING:
			if (!pixel.Equals(SkinEditController.SkinEditController_0.color_0))
			{
				FillArea(int_0, int_1);
				bool_0 = true;
			}
			break;
		case SkinEditorToolItem.ToolType.DROPPER:
			SkinEditController.SkinEditController_0.color_0 = texture2D_0.GetPixel(int_0, int_1);
			SkinEditController.SkinEditController_0.Dispatch(0, SkinEditController.SkinEditorEvent.DROPPER_USED);
			break;
		case SkinEditorToolItem.ToolType.ERASER:
			if (!pixel.Equals(Color.white))
			{
				texture2D_0.SetPixel(int_0, int_1, Color.white);
				bool_0 = true;
			}
			break;
		}
		texture2D_0.Apply();
	}

	private void FillArea(int int_0, int int_1)
	{
		color_1 = texture2D_0.GetPixel(int_0, int_1);
		color_0 = SkinEditController.SkinEditController_0.color_0;
		FillPixel(int_0, int_1);
	}

	private void FillPixel(int int_0, int int_1)
	{
		int num = int_0;
		int num2 = int_0;
		int num3 = int_0;
		while (true)
		{
			texture2D_0.SetPixel(num3, int_1, color_0);
			num3--;
			if (num3 < 0)
			{
				break;
			}
			Color pixel = texture2D_0.GetPixel(num3, int_1);
			if (!pixel.Equals(color_1) || pixel.Equals(color_0))
			{
				break;
			}
			num = num3;
		}
		num3 = int_0;
		while (true)
		{
			texture2D_0.SetPixel(num3, int_1, color_0);
			num3++;
			if (num3 > texture2D_0.width - 1)
			{
				break;
			}
			Color pixel2 = texture2D_0.GetPixel(num3, int_1);
			if (!pixel2.Equals(color_1) || pixel2.Equals(color_0))
			{
				break;
			}
			num2 = num3;
		}
		for (int i = num; i <= num2; i++)
		{
			if (int_1 < texture2D_0.height - 1 && texture2D_0.GetPixel(i, int_1 + 1).Equals(color_1))
			{
				FillPixel(i, int_1 + 1);
			}
			if (int_1 > 0 && texture2D_0.GetPixel(i, int_1 - 1).Equals(color_1))
			{
				FillPixel(i, int_1 - 1);
			}
		}
	}

	private void FillCanvas(Color color_2, bool bool_1)
	{
		for (int i = 0; i < texture2D_0.width; i++)
		{
			for (int j = 0; j < texture2D_0.height; j++)
			{
				texture2D_0.SetPixel(i, j, color_2);
			}
		}
		if (bool_1)
		{
			texture2D_0.Apply();
		}
	}

	private void OnGridToggleChanged(int int_0 = 0)
	{
		NGUITools.SetActive(grid.gameObject, SkinEditController.SkinEditController_0.Boolean_0);
	}
}
