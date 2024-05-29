using UnityEngine;

public class CameraPathFOVList : CameraPathPointList
{
	public enum Interpolation
	{
		None = 0,
		Linear = 1,
		SmoothStep = 2
	}

	private const float float_0 = 60f;

	public Interpolation interpolation_0 = Interpolation.SmoothStep;

	public bool bool_1 = true;

	public new CameraPathFOV this[int index]
	{
		get
		{
			return (CameraPathFOV)base[index];
		}
	}

	private float Single_0
	{
		get
		{
			if ((bool)Camera.current)
			{
				return Camera.current.fieldOfView;
			}
			Camera[] allCameras = Camera.allCameras;
			if (allCameras.Length > 0)
			{
				return allCameras[0].fieldOfView;
			}
			return 60f;
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
			cameraPath_0.PathPointAddedEvent += AddFOV;
			string_0 = "FOV";
			bool_0 = true;
		}
	}

	public override void CleanUp()
	{
		base.CleanUp();
		cameraPath_0.PathPointAddedEvent -= AddFOV;
		bool_0 = false;
	}

	public void AddFOV(CameraPathControlPoint cameraPathControlPoint_0)
	{
		CameraPathFOV cameraPathFOV = base.gameObject.AddComponent<CameraPathFOV>();
		cameraPathFOV.float_2 = Single_0;
		cameraPathFOV.hideFlags = HideFlags.HideInInspector;
		AddPoint(cameraPathFOV, cameraPathControlPoint_0);
		RecalculatePoints();
	}

	public CameraPathFOV AddFOV(CameraPathControlPoint cameraPathControlPoint_0, CameraPathControlPoint cameraPathControlPoint_1, float float_1, float float_2)
	{
		CameraPathFOV cameraPathFOV = base.gameObject.AddComponent<CameraPathFOV>();
		cameraPathFOV.hideFlags = HideFlags.HideInInspector;
		cameraPathFOV.float_2 = float_2;
		AddPoint(cameraPathFOV, cameraPathControlPoint_0, cameraPathControlPoint_1, float_1);
		RecalculatePoints();
		return cameraPathFOV;
	}

	public float GetFOV(float float_1)
	{
		if (base.Int32_1 < 2)
		{
			if (base.Int32_1 == 1)
			{
				return this[0].float_2;
			}
			return 60f;
		}
		float_1 = Mathf.Clamp(float_1, 0f, 1f);
		switch (interpolation_0)
		{
		default:
			return LinearInterpolation(float_1);
		case Interpolation.SmoothStep:
			return SmoothStepInterpolation(float_1);
		case Interpolation.Linear:
			return LinearInterpolation(float_1);
		}
	}

	private float LinearInterpolation(float float_1)
	{
		int lastPointIndex = GetLastPointIndex(float_1);
		CameraPathFOV cameraPathFOV = (CameraPathFOV)GetPoint(lastPointIndex);
		CameraPathFOV cameraPathFOV2 = (CameraPathFOV)GetPoint(lastPointIndex + 1);
		float single_ = cameraPathFOV.Single_0;
		float num = cameraPathFOV2.Single_0;
		if (single_ > num)
		{
			num += 1f;
		}
		float num2 = num - single_;
		float num3 = float_1 - single_;
		float t = num3 / num2;
		return Mathf.Lerp(cameraPathFOV.float_2, cameraPathFOV2.float_2, t);
	}

	private float SmoothStepInterpolation(float float_1)
	{
		int lastPointIndex = GetLastPointIndex(float_1);
		CameraPathFOV cameraPathFOV = (CameraPathFOV)GetPoint(lastPointIndex);
		CameraPathFOV cameraPathFOV2 = (CameraPathFOV)GetPoint(lastPointIndex + 1);
		float single_ = cameraPathFOV.Single_0;
		float num = cameraPathFOV2.Single_0;
		if (single_ > num)
		{
			num += 1f;
		}
		float num2 = num - single_;
		float num3 = float_1 - single_;
		float num4 = num3 / num2;
		return Mathf.Lerp(cameraPathFOV.float_2, cameraPathFOV2.float_2, CPMath.SmoothStep(num4));
	}
}
