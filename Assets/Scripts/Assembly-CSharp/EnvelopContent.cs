using UnityEngine;

[RequireComponent(typeof(UIWidget))]
public class EnvelopContent : MonoBehaviour
{
	public Transform targetRoot;

	public int padLeft;

	public int padRight;

	public int padBottom;

	public int padTop;

	private bool bool_0;

	private void Start()
	{
		bool_0 = true;
		Execute();
	}

	private void OnEnable()
	{
		if (bool_0)
		{
			Execute();
		}
	}

	[ContextMenu("Execute")]
	public void Execute()
	{
		if (targetRoot == base.transform)
		{
			Debug.LogError("Target Root object cannot be the same object that has Envelop Content. Make it a sibling instead.", this);
			return;
		}
		if (NGUITools.IsChild(targetRoot, base.transform))
		{
			Debug.LogError("Target Root object should not be a parent of Envelop Content. Make it a sibling instead.", this);
			return;
		}
		Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(base.transform.parent, targetRoot, false);
		float num = bounds.min.x + (float)padLeft;
		float num2 = bounds.min.y + (float)padBottom;
		float num3 = bounds.max.x + (float)padRight;
		float num4 = bounds.max.y + (float)padTop;
		UIWidget component = GetComponent<UIWidget>();
		component.SetRect(num, num2, num3 - num, num4 - num2);
		BroadcastMessage("UpdateAnchors", SendMessageOptions.DontRequireReceiver);
	}
}
