using UnityEngine;

public class CameraPathTiltList : CameraPathPointList
{
	public enum Interpolation
	{
		None = 0,
		Linear = 1,
		SmoothStep = 2
	}

	public Interpolation interpolation_0 = Interpolation.SmoothStep;

	public bool bool_1 = true;

	public float float_0 = 1f;

	public new CameraPathTilt this[int index]
	{
		get
		{
			return (CameraPathTilt)base[index];
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
			base.Init(cameraPath_1);
			cameraPath_0.PathPointAddedEvent += AddTilt;
			string_0 = "Tilt";
			bool_0 = true;
		}
	}

	public override void CleanUp()
	{
		base.CleanUp();
		cameraPath_0.PathPointAddedEvent -= AddTilt;
		bool_0 = false;
	}

	public void AddTilt(CameraPathControlPoint cameraPathControlPoint_0)
	{
		CameraPathTilt cameraPathTilt = base.gameObject.AddComponent<CameraPathTilt>();
		cameraPathTilt.float_2 = 0f;
		cameraPathTilt.hideFlags = HideFlags.HideInInspector;
		AddPoint(cameraPathTilt, cameraPathControlPoint_0);
		RecalculatePoints();
	}

	public CameraPathTilt AddTilt(CameraPathControlPoint cameraPathControlPoint_0, CameraPathControlPoint cameraPathControlPoint_1, float float_1, float float_2)
	{
		CameraPathTilt cameraPathTilt = base.gameObject.AddComponent<CameraPathTilt>();
		cameraPathTilt.float_2 = float_2;
		cameraPathTilt.hideFlags = HideFlags.HideInInspector;
		AddPoint(cameraPathTilt, cameraPathControlPoint_0, cameraPathControlPoint_1, float_1);
		RecalculatePoints();
		return cameraPathTilt;
	}

	public float GetTilt(float float_1)
	{
		if (base.Int32_1 < 2)
		{
			if (base.Int32_1 == 1)
			{
				return this[0].float_2;
			}
			return 0f;
		}
		float_1 = Mathf.Clamp(float_1, 0f, 1f);
		switch (interpolation_0)
		{
		default:
			return LinearInterpolation(float_1);
		case Interpolation.None:
		{
			CameraPathTilt cameraPathTilt = (CameraPathTilt)GetPoint(GetNextPointIndex(float_1));
			return cameraPathTilt.float_2;
		}
		case Interpolation.Linear:
			return LinearInterpolation(float_1);
		case Interpolation.SmoothStep:
			return SmoothStepInterpolation(float_1);
		}
	}

	public void AutoSetTilts()
	{
		for (int i = 0; i < base.Int32_1; i++)
		{
			AutoSetTilt(this[i]);
		}
	}

	public void AutoSetTilt(CameraPathTilt cameraPathTilt_0)
	{
		float single_ = cameraPathTilt_0.Single_0;
		Vector3 pathPosition = cameraPath_0.GetPathPosition(single_ - 0.1f);
		Vector3 pathPosition2 = cameraPath_0.GetPathPosition(single_);
		Vector3 pathPosition3 = cameraPath_0.GetPathPosition(single_ + 0.1f);
		Vector3 vector = pathPosition2 - pathPosition;
		Vector3 vector2 = pathPosition3 - pathPosition2;
		Quaternion quaternion = Quaternion.LookRotation(-cameraPath_0.GetPathDirection(cameraPathTilt_0.Single_0));
		Vector3 vector3 = quaternion * (vector2 - vector).normalized;
		float num = Vector2.Angle(Vector2.up, new Vector2(vector3.x, vector3.y));
		float num2 = Mathf.Min(Mathf.Abs(vector3.x) + Mathf.Abs(vector3.y) / Mathf.Abs(vector3.z), 1f);
		cameraPathTilt_0.float_2 = (0f - num) * float_0 * num2;
	}

	private float LinearInterpolation(float float_1)
	{
		int lastPointIndex = GetLastPointIndex(float_1);
		CameraPathTilt cameraPathTilt = (CameraPathTilt)GetPoint(lastPointIndex);
		CameraPathTilt cameraPathTilt2 = (CameraPathTilt)GetPoint(lastPointIndex + 1);
		float single_ = cameraPathTilt.Single_0;
		float num = cameraPathTilt2.Single_0;
		if (single_ > num)
		{
			num += 1f;
		}
		float num2 = num - single_;
		float num3 = float_1 - single_;
		float t = num3 / num2;
		return Mathf.Lerp(cameraPathTilt.float_2, cameraPathTilt2.float_2, t);
	}

	private float SmoothStepInterpolation(float float_1)
	{
		int lastPointIndex = GetLastPointIndex(float_1);
		CameraPathTilt cameraPathTilt = (CameraPathTilt)GetPoint(lastPointIndex);
		CameraPathTilt cameraPathTilt2 = (CameraPathTilt)GetPoint(lastPointIndex + 1);
		float single_ = cameraPathTilt.Single_0;
		float num = cameraPathTilt2.Single_0;
		if (single_ > num)
		{
			num += 1f;
		}
		float num2 = num - single_;
		float num3 = float_1 - single_;
		float num4 = num3 / num2;
		return Mathf.Lerp(cameraPathTilt.float_2, cameraPathTilt2.float_2, CPMath.SmoothStep(num4));
	}
}
