using UnityEngine;

public class SetPosInArmor : MonoBehaviour
{
	public Transform target;

	private Transform transform_0;

	public void SetPosition()
	{
		if (target != null)
		{
			base.transform.position = target.position;
			base.transform.rotation = target.rotation;
		}
	}

	private void Start()
	{
		transform_0 = base.transform;
	}

	private void Update()
	{
		if (target != null)
		{
			SetPosition();
		}
		else if (transform_0.root.GetComponent<SkinName>() != null && transform_0.root.GetComponent<SkinName>().Player_move_c_0.transform.childCount > 0 && transform_0.root.GetComponent<SkinName>().Player_move_c_0.transform.GetChild(0).GetComponent<WeaponSounds>() != null)
		{
			if (base.gameObject.name.Equals("Armor_Arm_Left"))
			{
				target = transform_0.root.GetComponent<SkinName>().Player_move_c_0.transform.GetChild(0).GetComponent<WeaponSounds>().Transform_0;
			}
			else
			{
				target = transform_0.root.GetComponent<SkinName>().Player_move_c_0.transform.GetChild(0).GetComponent<WeaponSounds>().Transform_1;
			}
		}
	}
}
