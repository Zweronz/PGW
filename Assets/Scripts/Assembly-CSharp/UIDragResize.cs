using UnityEngine;

public class UIDragResize : MonoBehaviour
{
	public UIWidget target;

	public UIWidget.Pivot pivot = UIWidget.Pivot.BottomRight;

	public int minWidth = 100;

	public int minHeight = 100;

	public int maxWidth = 100000;

	public int maxHeight = 100000;

	private Plane plane_0;

	private Vector3 vector3_0;

	private Vector3 vector3_1;

	private int int_0;

	private int int_1;

	private bool bool_0;

	private void OnDragStart()
	{
		if (target != null)
		{
			Vector3[] vector3_ = target.Vector3_3;
			plane_0 = new Plane(vector3_[0], vector3_[1], vector3_[3]);
			Ray ray_ = UICamera.Ray_0;
			float enter;
			if (plane_0.Raycast(ray_, out enter))
			{
				vector3_0 = ray_.GetPoint(enter);
				vector3_1 = target.Transform_0.localPosition;
				int_0 = target.Int32_0;
				int_1 = target.Int32_1;
				bool_0 = true;
			}
		}
	}

	private void OnDrag(Vector2 vector2_0)
	{
		if (bool_0 && target != null)
		{
			Ray ray_ = UICamera.Ray_0;
			float enter;
			if (plane_0.Raycast(ray_, out enter))
			{
				Transform transform_ = target.Transform_0;
				transform_.localPosition = vector3_1;
				target.Int32_0 = int_0;
				target.Int32_1 = int_1;
				Vector3 vector = ray_.GetPoint(enter) - vector3_0;
				transform_.position += vector;
				Vector3 vector2 = Quaternion.Inverse(transform_.localRotation) * (transform_.localPosition - vector3_1);
				transform_.localPosition = vector3_1;
				NGUIMath.ResizeWidget(target, pivot, vector2.x, vector2.y, minWidth, minHeight, maxWidth, maxHeight);
			}
		}
	}

	private void OnDragEnd()
	{
		bool_0 = false;
	}
}
