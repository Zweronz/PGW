using UnityEngine;

public class SkinEditorToolsPanel : MonoBehaviour
{
	public SkinEditorToolItem[] tools;

	private void Start()
	{
		SkinEditorToolItem[] array = tools;
		foreach (SkinEditorToolItem skinEditorToolItem in array)
		{
			skinEditorToolItem.SetOnSelectCallback(OnSelectedTool);
		}
		tools[0].Invoke("OnClick", 0.01f);
	}

	private void UnselectAll()
	{
		SkinEditorToolItem[] array = tools;
		foreach (SkinEditorToolItem skinEditorToolItem in array)
		{
			skinEditorToolItem.Boolean_0 = false;
		}
	}

	private void OnSelectedTool(SkinEditorToolItem.ToolType toolType_0)
	{
		SkinEditController.SkinEditController_0.toolType_0 = toolType_0;
		UnselectAll();
	}
}
