using UnityEngine;

public class CameraPathOrientationList : CameraPathPointList
{
	public enum Interpolation
	{
		None = 0,
		Linear = 1,
		SmoothStep = 2,
		Hermite = 3,
		Cubic = 4
	}

	public Interpolation interpolation_0 = Interpolation.Cubic;

	public new CameraPathOrientation this[int index]
	{
		get
		{
			return (CameraPathOrientation)base[index];
		}
	}

	private void OnEnable()
	{
		base.hideFlags = HideFlags.HideInInspector;
	}

	public override void Init(CameraPath cameraPath_1)
	{
		if (!bool_0)
		{
			string_0 = "Orientation";
			base.Init(cameraPath_1);
			cameraPath_0.PathPointAddedEvent += AddOrientation;
			bool_0 = true;
		}
	}

	public override void CleanUp()
	{
		base.CleanUp();
		cameraPath_0.PathPointAddedEvent -= AddOrientation;
		bool_0 = false;
	}

	public void AddOrientation(CameraPathControlPoint cameraPathControlPoint_0)
	{
		CameraPathOrientation cameraPathOrientation = base.gameObject.AddComponent<CameraPathOrientation>();
		if (cameraPathControlPoint_0.Vector3_3 != Vector3.zero)
		{
			cameraPathOrientation.quaternion_0 = Quaternion.LookRotation(cameraPathControlPoint_0.Vector3_3);
		}
		else
		{
			cameraPathOrientation.quaternion_0 = Quaternion.LookRotation(cameraPath_0.GetPathDirection(cameraPathControlPoint_0.percentage));
		}
		cameraPathOrientation.hideFlags = HideFlags.HideInInspector;
		AddPoint(cameraPathOrientation, cameraPathControlPoint_0);
		RecalculatePoints();
	}

	public CameraPathOrientation AddOrientation(CameraPathControlPoint cameraPathControlPoint_0, CameraPathControlPoint cameraPathControlPoint_1, float float_0, Quaternion quaternion_0)
	{
		CameraPathOrientation cameraPathOrientation = base.gameObject.AddComponent<CameraPathOrientation>();
		cameraPathOrientation.quaternion_0 = quaternion_0;
		cameraPathOrientation.hideFlags = HideFlags.HideInInspector;
		AddPoint(cameraPathOrientation, cameraPathControlPoint_0, cameraPathControlPoint_1, float_0);
		RecalculatePoints();
		return cameraPathOrientation;
	}

	public void RemovePoint(CameraPathOrientation cameraPathOrientation_0)
	{
		RemovePoint((CameraPathPoint)cameraPathOrientation_0);
		RecalculatePoints();
	}

	public Quaternion GetOrientation(float float_0)
	{
		if (base.Int32_1 < 2)
		{
			if (base.Int32_1 == 1)
			{
				return this[0].quaternion_0;
			}
			return Quaternion.identity;
		}
		if (float.IsNaN(float_0))
		{
			float_0 = 0f;
		}
		float_0 = Mathf.Clamp(float_0, 0f, 1f);
		Quaternion identity = Quaternion.identity;
		switch (interpolation_0)
		{
		default:
			identity = Quaternion.LookRotation(Vector3.forward);
			break;
		case Interpolation.None:
		{
			CameraPathOrientation cameraPathOrientation = (CameraPathOrientation)GetPoint(GetNextPointIndex(float_0));
			identity = cameraPathOrientation.quaternion_0;
			break;
		}
		case Interpolation.Linear:
			identity = LinearInterpolation(float_0);
			break;
		case Interpolation.SmoothStep:
			identity = SmootStepInterpolation(float_0);
			break;
		case Interpolation.Hermite:
			identity = CubicInterpolation(float_0);
			break;
		case Interpolation.Cubic:
			identity = CubicInterpolation(float_0);
			break;
		}
		if (float.IsNaN(identity.x))
		{
			return Quaternion.identity;
		}
		return identity;
	}

	private Quaternion LinearInterpolation(float float_0)
	{
		int lastPointIndex = GetLastPointIndex(float_0);
		CameraPathOrientation cameraPathOrientation = (CameraPathOrientation)GetPoint(lastPointIndex);
		CameraPathOrientation cameraPathOrientation2 = (CameraPathOrientation)GetPoint(lastPointIndex + 1);
		float single_ = cameraPathOrientation.Single_0;
		float num = cameraPathOrientation2.Single_0;
		if (single_ > num)
		{
			num += 1f;
		}
		float num2 = num - single_;
		float num3 = float_0 - single_;
		float t = num3 / num2;
		return Quaternion.Lerp(cameraPathOrientation.quaternion_0, cameraPathOrientation2.quaternion_0, t);
	}

	private Quaternion SmootStepInterpolation(float float_0)
	{
		int lastPointIndex = GetLastPointIndex(float_0);
		CameraPathOrientation cameraPathOrientation = (CameraPathOrientation)GetPoint(lastPointIndex);
		CameraPathOrientation cameraPathOrientation2 = (CameraPathOrientation)GetPoint(lastPointIndex + 1);
		float single_ = cameraPathOrientation.Single_0;
		float num = cameraPathOrientation2.Single_0;
		if (single_ > num)
		{
			num += 1f;
		}
		float num2 = num - single_;
		float num3 = float_0 - single_;
		float float_ = num3 / num2;
		return Quaternion.Lerp(cameraPathOrientation.quaternion_0, cameraPathOrientation2.quaternion_0, CPMath.SmoothStep(float_));
	}

	private Quaternion CubicInterpolation(float float_0)
	{
		int lastPointIndex = GetLastPointIndex(float_0);
		CameraPathOrientation cameraPathOrientation = (CameraPathOrientation)GetPoint(lastPointIndex);
		CameraPathOrientation cameraPathOrientation2 = (CameraPathOrientation)GetPoint(lastPointIndex + 1);
		CameraPathOrientation cameraPathOrientation3 = (CameraPathOrientation)GetPoint(lastPointIndex - 1);
		CameraPathOrientation cameraPathOrientation4 = (CameraPathOrientation)GetPoint(lastPointIndex + 2);
		float single_ = cameraPathOrientation.Single_0;
		float num = cameraPathOrientation2.Single_0;
		if (single_ > num)
		{
			num += 1f;
		}
		float num2 = num - single_;
		float num3 = float_0 - single_;
		float float_ = num3 / num2;
		Quaternion result = CPMath.CalculateCubic(cameraPathOrientation.quaternion_0, cameraPathOrientation3.quaternion_0, cameraPathOrientation4.quaternion_0, cameraPathOrientation2.quaternion_0, float_);
		if (float.IsNaN(result.x))
		{
			Debug.Log(float_0 + " " + cameraPathOrientation.fullName + " " + cameraPathOrientation2.fullName + " " + cameraPathOrientation3.fullName + " " + cameraPathOrientation4.fullName);
		}
		return result;
	}

	protected override void RecalculatePoints()
	{
		base.RecalculatePoints();
		for (int i = 0; i < base.Int32_1; i++)
		{
			CameraPathOrientation cameraPathOrientation = this[i];
			if (cameraPathOrientation.transform_0 != null)
			{
				cameraPathOrientation.quaternion_0 = Quaternion.LookRotation(cameraPathOrientation.transform_0.transform.position - cameraPathOrientation.worldPosition);
			}
		}
	}
}
