using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraPathDelayList : CameraPathPointList
{
	public delegate void CameraPathDelayEventHandler(float float_0);

	public float float_0 = 0.01f;

	private float float_1;

	[SerializeField]
	private CameraPathDelay cameraPathDelay_0;

	[SerializeField]
	private CameraPathDelay cameraPathDelay_1;

	[SerializeField]
	private bool bool_1;

	private CameraPathDelayEventHandler cameraPathDelayEventHandler_0;

	public new CameraPathDelay this[int index]
	{
		get
		{
			return (CameraPathDelay)base[index];
		}
	}

	public CameraPathDelay CameraPathDelay_0
	{
		get
		{
			return cameraPathDelay_0;
		}
	}

	public CameraPathDelay CameraPathDelay_1
	{
		get
		{
			return cameraPathDelay_1;
		}
	}

	public event CameraPathDelayEventHandler CameraPathDelayEvent
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			cameraPathDelayEventHandler_0 = (CameraPathDelayEventHandler)Delegate.Combine(cameraPathDelayEventHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			cameraPathDelayEventHandler_0 = (CameraPathDelayEventHandler)Delegate.Remove(cameraPathDelayEventHandler_0, value);
		}
	}

	private void OnEnable()
	{
		base.hideFlags = HideFlags.HideInInspector;
	}

	public override void Init(CameraPath cameraPath_1)
	{
		base.Init(cameraPath_1);
		if (!bool_1)
		{
			cameraPathDelay_0 = base.gameObject.AddComponent<CameraPathDelay>();
			cameraPathDelay_0.customName = "Start Point";
			cameraPathDelay_0.hideFlags = HideFlags.HideInInspector;
			AddPoint(CameraPathDelay_0, 0f);
			cameraPathDelay_1 = base.gameObject.AddComponent<CameraPathDelay>();
			cameraPathDelay_1.customName = "End Point";
			cameraPathDelay_1.hideFlags = HideFlags.HideInInspector;
			AddPoint(CameraPathDelay_1, 1f);
			RecalculatePoints();
			bool_1 = true;
		}
		string_0 = "Delay";
	}

	public void AddDelayPoint(CameraPathControlPoint cameraPathControlPoint_0)
	{
		CameraPathDelay cameraPathDelay = base.gameObject.AddComponent<CameraPathDelay>();
		cameraPathDelay.hideFlags = HideFlags.HideInInspector;
		AddPoint(cameraPathDelay, cameraPathControlPoint_0);
		RecalculatePoints();
	}

	public CameraPathDelay AddDelayPoint(CameraPathControlPoint cameraPathControlPoint_0, CameraPathControlPoint cameraPathControlPoint_1, float float_2)
	{
		CameraPathDelay cameraPathDelay = base.gameObject.AddComponent<CameraPathDelay>();
		cameraPathDelay.hideFlags = HideFlags.HideInInspector;
		AddPoint(cameraPathDelay, cameraPathControlPoint_0, cameraPathControlPoint_1, float_2);
		RecalculatePoints();
		return cameraPathDelay;
	}

	public void OnAnimationStart(float float_2)
	{
		float_1 = float_2;
	}

	public void CheckEvents(float float_2)
	{
		if (Mathf.Abs(float_2 - float_1) > 0.1f)
		{
			float_1 = float_2;
		}
		else
		{
			if (float_1 == float_2)
			{
				return;
			}
			for (int i = 0; i < base.Int32_1; i++)
			{
				CameraPathDelay cameraPathDelay = this[i];
				if (cameraPathDelay == CameraPathDelay_1)
				{
					continue;
				}
				if (cameraPathDelay.Single_0 >= float_1 && cameraPathDelay.Single_0 <= float_2)
				{
					if (cameraPathDelay != CameraPathDelay_0)
					{
						FireDelay(cameraPathDelay);
					}
					else if (cameraPathDelay.float_2 > 0f)
					{
						FireDelay(cameraPathDelay);
					}
				}
				else if (cameraPathDelay.Single_0 >= float_2 && cameraPathDelay.Single_0 <= float_1)
				{
					if (cameraPathDelay != CameraPathDelay_0)
					{
						FireDelay(cameraPathDelay);
					}
					else if (cameraPathDelay.float_2 > 0f)
					{
						FireDelay(cameraPathDelay);
					}
				}
			}
			float_1 = float_2;
		}
	}

	public float CheckEase(float float_2)
	{
		float val = 1f;
		for (int i = 0; i < base.Int32_1; i++)
		{
			CameraPathDelay cameraPathDelay = this[i];
			if (cameraPathDelay != CameraPathDelay_0)
			{
				CameraPathDelay cameraPathDelay2 = (CameraPathDelay)GetPoint(i - 1);
				float pathPercentage = cameraPath_0.GetPathPercentage(cameraPathDelay2.Single_0, cameraPathDelay.Single_0, 1f - cameraPathDelay.float_3);
				if (pathPercentage < float_2 && cameraPathDelay.Single_0 > float_2)
				{
					float time = (float_2 - pathPercentage) / (cameraPathDelay.Single_0 - pathPercentage);
					val = cameraPathDelay.animationCurve_0.Evaluate(time);
				}
			}
			if (cameraPathDelay != CameraPathDelay_1)
			{
				CameraPathDelay cameraPathDelay3 = (CameraPathDelay)GetPoint(i + 1);
				float pathPercentage2 = cameraPath_0.GetPathPercentage(cameraPathDelay.Single_0, cameraPathDelay3.Single_0, cameraPathDelay.float_4);
				if (cameraPathDelay.Single_0 < float_2 && pathPercentage2 > float_2)
				{
					float time2 = (float_2 - cameraPathDelay.Single_0) / (pathPercentage2 - cameraPathDelay.Single_0);
					val = cameraPathDelay.animationCurve_1.Evaluate(time2);
				}
			}
		}
		return Math.Max(val, float_0);
	}

	public void FireDelay(CameraPathDelay cameraPathDelay_2)
	{
		if (cameraPathDelayEventHandler_0 != null)
		{
			cameraPathDelayEventHandler_0(cameraPathDelay_2.float_2);
		}
	}
}
