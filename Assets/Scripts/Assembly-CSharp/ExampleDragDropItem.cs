using UnityEngine;

public class ExampleDragDropItem : UIDragDropItem
{
	public GameObject gameObject_0;

	protected override void OnDragDropRelease(GameObject gameObject_1)
	{
		if (gameObject_1 != null)
		{
			ExampleDragDropSurface component = gameObject_1.GetComponent<ExampleDragDropSurface>();
			if (component != null)
			{
				GameObject gameObject = NGUITools.AddChild(component.gameObject, gameObject_0);
				gameObject.transform.localScale = component.transform.localScale;
				Transform transform = gameObject.transform;
				transform.position = UICamera.vector3_0;
				if (component.rotatePlacedObject)
				{
					transform.rotation = Quaternion.LookRotation(UICamera.raycastHit_0.normal) * Quaternion.Euler(90f, 0f, 0f);
				}
				NGUITools.Destroy(base.gameObject);
				return;
			}
		}
		base.OnDragDropRelease(gameObject_1);
	}
}
