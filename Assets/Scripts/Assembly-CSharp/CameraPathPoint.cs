using UnityEngine;

public class CameraPathPoint : MonoBehaviour
{
	public enum PositionModes
	{
		Free = 0,
		FixedToPoint = 1,
		FixedToPercent = 2
	}

	public PositionModes positionModes;

	public string givenName = string.Empty;

	public string customName = string.Empty;

	public string fullName = string.Empty;

	[SerializeField]
	protected float float_0;

	[SerializeField]
	protected float float_1;

	public CameraPathControlPoint point;

	public int index;

	public CameraPathControlPoint cpointA;

	public CameraPathControlPoint cpointB;

	public float curvePercentage;

	public Vector3 worldPosition;

	public bool lockPoint;

	public float Single_0
	{
		get
		{
			switch (positionModes)
			{
			default:
				return float_0;
			case PositionModes.Free:
				return float_0;
			case PositionModes.FixedToPoint:
				return point.percentage;
			case PositionModes.FixedToPercent:
				return float_0;
			}
		}
		set
		{
			float_0 = value;
		}
	}

	public float Single_1
	{
		get
		{
			return float_0;
		}
	}

	public float Single_2
	{
		get
		{
			switch (positionModes)
			{
			default:
				return float_0;
			case PositionModes.Free:
				return float_1;
			case PositionModes.FixedToPoint:
				return point.normalisedPercentage;
			case PositionModes.FixedToPercent:
				return float_1;
			}
		}
		set
		{
			float_1 = value;
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
}
