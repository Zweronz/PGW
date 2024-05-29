using System;
using System.Collections;
using Rilisoft;
using UnityEngine;

public class EditorTextures : MonoBehaviour
{
	private Color color_0 = new Color(1f, 1f, 1f, 1f);

	public UITexture canvasTexture;

	private bool bool_0;

	private Vector2 vector2_0 = new Vector2(-1f, -1f);

	private bool bool_1;

	public UITexture fonCanvas;

	public ButtonHandler prevHistoryButton;

	public ButtonHandler nextHistoryButton;

	private UIButton uibutton_0;

	private UIButton uibutton_1;

	public ArrayList arrHistory = new ArrayList();

	public int currentHistoryIndex;

	private bool bool_2;

	public GameObject saveFrame;

	private void Start()
	{
		if (prevHistoryButton != null)
		{
			prevHistoryButton.Clicked += HandlePrevHistoryButtonClicked;
			uibutton_0 = prevHistoryButton.gameObject.GetComponent<UIButton>();
		}
		if (nextHistoryButton != null)
		{
			nextHistoryButton.Clicked += HandleNextHistoryButtonClicked;
			uibutton_1 = nextHistoryButton.gameObject.GetComponent<UIButton>();
		}
	}

	public void SetStartCanvas(Texture2D texture2D_0)
	{
		canvasTexture.Texture_0 = CreateCopyTexture(CreateCopyTexture(texture2D_0));
		float num = 400f / (float)canvasTexture.Texture_0.width;
		float num2 = 400f / (float)canvasTexture.Texture_0.height;
		int num3 = ((!(num < num2)) ? Mathf.RoundToInt(num2) : Mathf.RoundToInt(num));
		canvasTexture.Int32_0 = canvasTexture.Texture_0.width * num3;
		canvasTexture.Int32_1 = canvasTexture.Texture_0.height * num3;
		UpdateFonCanvas();
		arrHistory.Clear();
		AddCanvasTextureInHistory();
	}

	private void HandlePrevHistoryButtonClicked(object sender, EventArgs e)
	{
		if (currentHistoryIndex > 0)
		{
			currentHistoryIndex--;
		}
		UpdateTextureFromHistory();
	}

	private void HandleNextHistoryButtonClicked(object sender, EventArgs e)
	{
		if (currentHistoryIndex < arrHistory.Count - 1)
		{
			currentHistoryIndex++;
		}
		UpdateTextureFromHistory();
	}

	private void UpdateTextureFromHistory()
	{
		canvasTexture.Texture_0 = CreateCopyTexture((Texture2D)arrHistory[currentHistoryIndex]);
	}

	public void AddCanvasTextureInHistory()
	{
		while (currentHistoryIndex < arrHistory.Count - 1)
		{
			arrHistory.RemoveAt(arrHistory.Count - 1);
		}
		arrHistory.Add(CreateCopyTexture((Texture2D)canvasTexture.Texture_0));
		if (arrHistory.Count > 30)
		{
			arrHistory.RemoveAt(0);
		}
		currentHistoryIndex = arrHistory.Count - 1;
	}

	private void Update()
	{
		if (uibutton_0 != null && uibutton_1 != null)
		{
			if (uibutton_0.Boolean_0 != (currentHistoryIndex != 0))
			{
				uibutton_0.Boolean_0 = currentHistoryIndex != 0;
			}
			if (uibutton_1.Boolean_0 != currentHistoryIndex < arrHistory.Count - 1)
			{
				uibutton_1.Boolean_0 = currentHistoryIndex < arrHistory.Count - 1;
			}
		}
		if (bool_0 && ((Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)) || Input.GetMouseButtonUp(0)))
		{
			bool_0 = false;
			vector2_0 = new Vector2(-1f, -1f);
			AddCanvasTextureInHistory();
		}
		if (bool_1)
		{
			bool_1 = false;
			UpdateFonCanvas();
		}
		if ((Input.touchCount <= 0 || Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled) && !bool_0 && !Input.GetMouseButtonDown(0))
		{
			return;
		}
		Vector2 vector2_ = ((Input.touchCount <= 0) ? new Vector2(Input.mousePosition.x, Input.mousePosition.y) : new Vector2(Input.touches[0].position.x, Input.touches[0].position.y));
		if (IsCanvasConteinPosition(vector2_))
		{
			bool_0 = true;
			Vector2 editPixelPos = GetEditPixelPos(vector2_);
			if (!editPixelPos.Equals(vector2_0))
			{
				vector2_0 = editPixelPos;
				EditCanvas(editPixelPos);
			}
		}
	}

	private void EditCanvas(Vector2 vector2_1)
	{
		if (saveFrame != null && saveFrame.activeSelf)
		{
			return;
		}
		if (SkinEditorController.brashMode_0 == SkinEditorController.BrashMode.Pipette)
		{
			if (SkinEditorController.skinEditorController_0 != null)
			{
				SkinEditorController.skinEditorController_0.newColor.Color_0 = ((Texture2D)canvasTexture.Texture_0).GetPixel(Mathf.RoundToInt(vector2_1.x), Mathf.RoundToInt(vector2_1.y));
				SkinEditorController.skinEditorController_0.HandleSetColorClicked(null, null);
			}
			return;
		}
		SkinEditorController.bool_0 = true;
		Texture2D texture2D = CreateCopyTexture(canvasTexture.Texture_0 as Texture2D);
		if (SkinEditorController.brashMode_0 == SkinEditorController.BrashMode.Pencil)
		{
			texture2D.SetPixel(Mathf.RoundToInt(vector2_1.x), Mathf.RoundToInt(vector2_1.y), SkinEditorController.color_0);
		}
		if (SkinEditorController.brashMode_0 == SkinEditorController.BrashMode.Brash)
		{
			texture2D.SetPixel(Mathf.RoundToInt(vector2_1.x), Mathf.RoundToInt(vector2_1.y), SkinEditorController.color_0);
			if (Mathf.RoundToInt(vector2_1.x) > 0)
			{
				texture2D.SetPixel(Mathf.RoundToInt(vector2_1.x) - 1, Mathf.RoundToInt(vector2_1.y), SkinEditorController.color_0);
			}
			if (Mathf.RoundToInt(vector2_1.x) < texture2D.width - 1)
			{
				texture2D.SetPixel(Mathf.RoundToInt(vector2_1.x) + 1, Mathf.RoundToInt(vector2_1.y), SkinEditorController.color_0);
			}
			if (Mathf.RoundToInt(vector2_1.y) > 0)
			{
				texture2D.SetPixel(Mathf.RoundToInt(vector2_1.x), Mathf.RoundToInt(vector2_1.y) - 1, SkinEditorController.color_0);
			}
			if (Mathf.RoundToInt(vector2_1.y) < texture2D.height - 1)
			{
				texture2D.SetPixel(Mathf.RoundToInt(vector2_1.x), Mathf.RoundToInt(vector2_1.y) + 1, SkinEditorController.color_0);
			}
		}
		if (SkinEditorController.brashMode_0 == SkinEditorController.BrashMode.Eraser)
		{
			texture2D.SetPixel(Mathf.RoundToInt(vector2_1.x), Mathf.RoundToInt(vector2_1.y), color_0);
		}
		if (SkinEditorController.brashMode_0 == SkinEditorController.BrashMode.Fill)
		{
			for (int i = 0; i < texture2D.width; i++)
			{
				for (int j = 0; j < texture2D.height; j++)
				{
					texture2D.SetPixel(i, j, SkinEditorController.color_0);
				}
			}
		}
		texture2D.Apply();
		bool_1 = true;
		canvasTexture.Texture_0 = texture2D;
	}

	private bool IsCanvasConteinPosition(Vector2 vector2_1)
	{
		float num = (float)Screen.height / 768f;
		Vector2 vector = new Vector2(((float)Screen.width - num * (float)canvasTexture.Int32_0) * 0.5f, ((float)Screen.height + num * (float)canvasTexture.Int32_1) * 0.5f);
		Vector2 vector2 = new Vector2(((float)Screen.width + num * (float)canvasTexture.Int32_0) * 0.5f, ((float)Screen.height - num * (float)canvasTexture.Int32_1) * 0.5f);
		if (vector2_1.x > vector.x && vector2_1.x < vector2.x && vector2_1.y < vector.y && vector2_1.y > vector2.y)
		{
			return true;
		}
		return false;
	}

	private Vector2 GetEditPixelPos(Vector2 vector2_1)
	{
		float num = (float)Screen.height / 768f;
		return new Vector2(Mathf.FloorToInt((vector2_1.x - ((float)Screen.width - (float)canvasTexture.Int32_0 * num) * 0.5f) / ((float)canvasTexture.Int32_0 * num) * (float)canvasTexture.Texture_0.width), Mathf.FloorToInt((vector2_1.y - ((float)Screen.height - (float)canvasTexture.Int32_1 * num) * 0.5f) / ((float)canvasTexture.Int32_1 * num) * (float)canvasTexture.Texture_0.height));
	}

	public static Texture2D CreateCopyTexture(Texture texture_0)
	{
		return CreateCopyTexture((Texture2D)texture_0);
	}

	public static Texture2D CreateCopyTexture(Texture2D texture2D_0)
	{
		Texture2D texture2D = new Texture2D(texture2D_0.width, texture2D_0.height, TextureFormat.RGBA32, false);
		texture2D.SetPixels(texture2D_0.GetPixels());
		texture2D.filterMode = FilterMode.Point;
		texture2D.Apply();
		return texture2D;
	}

	public void UpdateFonCanvas()
	{
		fonCanvas.Int32_0 = canvasTexture.Int32_0;
		fonCanvas.Int32_1 = canvasTexture.Int32_1;
		fonCanvas.Texture_0 = canvasTexture.Texture_0;
	}
}
