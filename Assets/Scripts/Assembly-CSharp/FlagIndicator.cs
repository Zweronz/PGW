using System;
using UnityEngine;
using engine.unity;

public class FlagIndicator : MonoBehaviour
{
	public Transform target;

	public bool isBaza;

	public FlagController flagController;

	private Camera camera_0;

	private Transform transform_0;

	private Vector3 vector3_0;

	private Vector3 vector3_1;

	private Vector2 vector2_0;

	private float float_0;

	private void Start()
	{
		transform_0 = base.transform;
		float_0 = ScreenController.ScreenController_0.Single_0;
		vector2_0 = new Vector2((float)Screen.width * float_0, (float)Screen.height * float_0);
		vector3_0 = new Vector3(vector2_0.x * 0.5f, vector2_0.y * 0.5f);
		vector3_1.x = vector3_0.x * 0.96f;
		vector3_1.y = vector3_0.y * 0.93f;
	}

	private void LateUpdate()
	{
		camera_0 = NickLabelController.camera_0;
		if (!(target == null) && !(camera_0 == null))
		{
			Vector3 localPosition = camera_0.WorldToScreenPoint(target.position);
			localPosition *= float_0;
			bool flag = localPosition.z > 0f && localPosition.x > 0f && localPosition.x < vector2_0.x && localPosition.y > 0f && localPosition.y < vector2_0.y;
			localPosition -= vector3_0;
			if (!flag)
			{
				if (localPosition.z < 0f)
				{
					localPosition *= -1f;
				}
				float num = Mathf.Atan2(localPosition.y, localPosition.x);
				num -= (float)Math.PI / 2f;
				float num2 = Mathf.Cos(num);
				float num3 = 0f - Mathf.Sin(num);
				float num4 = num2 / num3;
				if (num2 > 0f)
				{
					localPosition.x = vector3_1.y / num4;
					localPosition.y = vector3_1.y;
				}
				else
				{
					localPosition.x = (0f - vector3_1.y) / num4;
					localPosition.y = 0f - vector3_1.y;
				}
				if (localPosition.x > vector3_1.x)
				{
					localPosition.x = vector3_1.x;
					localPosition.y = vector3_1.x * num4;
				}
				else if (localPosition.x < 0f - vector3_1.x)
				{
					localPosition.x = 0f - vector3_1.x;
					localPosition.y = (0f - vector3_1.x) * num4;
				}
			}
			transform_0.localPosition = localPosition;
			bool flag2 = flagController.GameObject_0 != null && flagController.GameObject_0.activeInHierarchy;
			bool flag3;
			if (!(flag3 = isBaza && flagController.Boolean_1 && flag2))
			{
				flag3 = !isBaza && !flag2;
			}
			if (!flag3 && WeaponManager.weaponManager_0 != null && WeaponManager.weaponManager_0.myPlayerMoveC != null && WeaponManager.weaponManager_0.myPlayerMoveC.Boolean_15 && WeaponManager.weaponManager_0.myPlayerMoveC.FlagController_3.Equals(flagController))
			{
				flag3 = true;
			}
			if (flag3)
			{
				MoveToBlackHole();
			}
		}
		else
		{
			MoveToBlackHole();
		}
	}

	private void MoveToBlackHole()
	{
		transform_0.position = new Vector3(-1000f, -1000f, -1000f);
	}
}
