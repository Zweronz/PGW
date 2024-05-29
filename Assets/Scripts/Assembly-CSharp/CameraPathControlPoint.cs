using UnityEngine;

public class CameraPathControlPoint : MonoBehaviour
{
	public string givenName = string.Empty;

	public string customName = string.Empty;

	public string fullName = string.Empty;

	[SerializeField]
	private Vector3 vector3_0;

	[SerializeField]
	private bool bool_0;

	[SerializeField]
	private Vector3 vector3_1;

	[SerializeField]
	private Vector3 vector3_2;

	[SerializeField]
	private Vector3 vector3_3 = Vector3.forward;

	public int index;

	public float percentage;

	public float normalisedPercentage;

	public Vector3 Vector3_0
	{
		get
		{
			return base.transform.rotation * vector3_0;
		}
		set
		{
			Vector3 vector = value;
			vector = Quaternion.Inverse(base.transform.rotation) * vector;
			vector3_0 = vector;
		}
	}

	public Vector3 Vector3_1
	{
		get
		{
			return base.transform.rotation * vector3_0 + base.transform.position;
		}
		set
		{
			Vector3 vector = value - base.transform.position;
			vector = Quaternion.Inverse(base.transform.rotation) * vector;
			vector3_0 = vector;
		}
	}

	public Vector3 Vector3_2
	{
		get
		{
			return Vector3_3 + base.transform.position;
		}
		set
		{
			Vector3_3 = value - base.transform.position;
		}
	}

	public Vector3 Vector3_3
	{
		get
		{
			return base.transform.rotation * (vector3_1 + vector3_0);
		}
		set
		{
			Vector3 vector = value;
			vector = Quaternion.Inverse(base.transform.rotation) * vector;
			vector += -vector3_0;
			vector3_1 = vector;
		}
	}

	public Vector3 Vector3_4
	{
		get
		{
			return base.transform.rotation * vector3_1;
		}
		set
		{
			Vector3 vector = value;
			vector = Quaternion.Inverse(base.transform.rotation) * vector;
			vector3_1 = vector;
		}
	}

	public Vector3 Vector3_5
	{
		get
		{
			return Vector3_6 + base.transform.position;
		}
		set
		{
			Vector3_6 = value - base.transform.position;
		}
	}

	public Vector3 Vector3_6
	{
		get
		{
			Vector3 vector = ((!bool_0) ? (-vector3_1) : vector3_2);
			return base.transform.rotation * (vector + vector3_0);
		}
		set
		{
			Vector3 vector = value;
			vector = Quaternion.Inverse(base.transform.rotation) * vector;
			vector += -vector3_0;
			if (bool_0)
			{
				vector3_2 = vector;
			}
			else
			{
				vector3_1 = -vector;
			}
		}
	}

	public bool Boolean_0
	{
		get
		{
			return bool_0;
		}
		set
		{
			if (value != bool_0)
			{
				vector3_2 = -vector3_1;
			}
			bool_0 = value;
		}
	}

	public Vector3 Vector3_7
	{
		get
		{
			return vector3_3;
		}
		set
		{
			if (!(value == Vector3.zero))
			{
				vector3_3 = value.normalized;
			}
		}
	}

	public string String_0
	{
		get
		{
			if (customName != string.Empty)
			{
				return customName;
			}
			return givenName;
		}
	}

	private void OnEnable()
	{
		base.hideFlags = HideFlags.HideInInspector;
	}

	public void CopyData(CameraPathControlPoint cameraPathControlPoint_0)
	{
		cameraPathControlPoint_0.customName = customName;
		cameraPathControlPoint_0.index = index;
		cameraPathControlPoint_0.percentage = percentage;
		cameraPathControlPoint_0.normalisedPercentage = normalisedPercentage;
		cameraPathControlPoint_0.Vector3_1 = Vector3_1;
		cameraPathControlPoint_0.Boolean_0 = bool_0;
		cameraPathControlPoint_0.Vector3_3 = vector3_1;
		cameraPathControlPoint_0.Vector3_6 = vector3_2;
	}
}
