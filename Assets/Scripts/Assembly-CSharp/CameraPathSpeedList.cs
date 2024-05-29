using UnityEngine;

public class CameraPathSpeedList : CameraPathPointList
{
	public enum Interpolation
	{
		None = 0,
		Linear = 1,
		SmoothStep = 2
	}

	public Interpolation interpolation_0 = Interpolation.SmoothStep;

	[SerializeField]
	private bool bool_1 = true;

	public new CameraPathSpeed this[int index]
	{
		get
		{
			return (CameraPathSpeed)base[index];
		}
	}

	public bool Boolean_0
	{
		get
		{
			return bool_1 && base.Int32_1 > 0;
		}
		set
		{
			bool_1 = value;
		}
	}

	private void OnEnable()
	{
		base.hideFlags = HideFlags.HideInInspector;
	}

	public override void Init(CameraPath cameraPath_1)
	{
		string_0 = "Speed";
		base.Init(cameraPath_1);
	}

	public void AddSpeedPoint(CameraPathControlPoint cameraPathControlPoint_0)
	{
		CameraPathSpeed cameraPathSpeed = base.gameObject.AddComponent<CameraPathSpeed>();
		cameraPathSpeed.hideFlags = HideFlags.HideInInspector;
		AddPoint(cameraPathSpeed, cameraPathControlPoint_0);
		RecalculatePoints();
	}

	public CameraPathSpeed AddSpeedPoint(CameraPathControlPoint cameraPathControlPoint_0, CameraPathControlPoint cameraPathControlPoint_1, float float_0)
	{
		CameraPathSpeed cameraPathSpeed = base.gameObject.AddComponent<CameraPathSpeed>();
		cameraPathSpeed.hideFlags = HideFlags.HideInInspector;
		AddPoint(cameraPathSpeed, cameraPathControlPoint_0, cameraPathControlPoint_1, Mathf.Clamp01(float_0));
		RecalculatePoints();
		return cameraPathSpeed;
	}

	public float GetSpeed(float float_0)
	{
		if (base.Int32_1 < 2)
		{
			if (base.Int32_1 == 1)
			{
				return this[0].Single_3;
			}
			Debug.Log("Not enough points to define a speed");
			return 0f;
		}
		if (float_0 >= 1f)
		{
			return ((CameraPathSpeed)GetPoint(base.Int32_1 - 1)).Single_3;
		}
		float_0 = Mathf.Clamp(float_0, 0f, 0.999f);
		switch (interpolation_0)
		{
		default:
			return LinearInterpolation(float_0);
		case Interpolation.None:
		{
			CameraPathSpeed cameraPathSpeed = (CameraPathSpeed)GetPoint(GetNextPointIndex(float_0));
			return cameraPathSpeed.Single_3;
		}
		case Interpolation.Linear:
			return LinearInterpolation(float_0);
		case Interpolation.SmoothStep:
			return SmoothStepInterpolation(float_0);
		}
	}

	private float LinearInterpolation(float float_0)
	{
		int lastPointIndex = GetLastPointIndex(float_0);
		CameraPathSpeed cameraPathSpeed = (CameraPathSpeed)GetPoint(lastPointIndex);
		CameraPathSpeed cameraPathSpeed2 = (CameraPathSpeed)GetPoint(lastPointIndex + 1);
		if (float_0 < cameraPathSpeed.Single_0)
		{
			return cameraPathSpeed.Single_3;
		}
		if (float_0 > cameraPathSpeed2.Single_0)
		{
			return cameraPathSpeed2.Single_3;
		}
		float single_ = cameraPathSpeed.Single_0;
		float num = cameraPathSpeed2.Single_0;
		if (single_ > num)
		{
			num += 1f;
		}
		float num2 = num - single_;
		float num3 = float_0 - single_;
		float t = num3 / num2;
		return Mathf.Lerp(cameraPathSpeed.Single_3, cameraPathSpeed2.Single_3, t);
	}

	private float SmoothStepInterpolation(float float_0)
	{
		int lastPointIndex = GetLastPointIndex(float_0);
		CameraPathSpeed cameraPathSpeed = (CameraPathSpeed)GetPoint(lastPointIndex);
		CameraPathSpeed cameraPathSpeed2 = (CameraPathSpeed)GetPoint(lastPointIndex + 1);
		if (float_0 < cameraPathSpeed.Single_0)
		{
			return cameraPathSpeed.Single_3;
		}
		if (float_0 > cameraPathSpeed2.Single_0)
		{
			return cameraPathSpeed2.Single_3;
		}
		float single_ = cameraPathSpeed.Single_0;
		float num = cameraPathSpeed2.Single_0;
		if (single_ > num)
		{
			num += 1f;
		}
		float num2 = num - single_;
		float num3 = float_0 - single_;
		float float_ = num3 / num2;
		return Mathf.Lerp(cameraPathSpeed.Single_3, cameraPathSpeed2.Single_3, CPMath.SmoothStep(float_));
	}
}
