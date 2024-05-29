using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraPathEventList : CameraPathPointList
{
	public delegate void CameraPathEventPointHandler(string string_0);

	private float float_0;

	private CameraPathEventPointHandler cameraPathEventPointHandler_0;

	public new CameraPathEvent this[int index]
	{
		get
		{
			return (CameraPathEvent)base[index];
		}
	}

	public event CameraPathEventPointHandler CameraPathEventPoint
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			cameraPathEventPointHandler_0 = (CameraPathEventPointHandler)Delegate.Combine(cameraPathEventPointHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			cameraPathEventPointHandler_0 = (CameraPathEventPointHandler)Delegate.Remove(cameraPathEventPointHandler_0, value);
		}
	}

	private void OnEnable()
	{
		base.hideFlags = HideFlags.HideInInspector;
	}

	public override void Init(CameraPath cameraPath_1)
	{
		string_0 = "Event";
		base.Init(cameraPath_1);
	}

	public void AddEvent(CameraPathControlPoint cameraPathControlPoint_0)
	{
		CameraPathEvent cameraPathEvent = base.gameObject.AddComponent<CameraPathEvent>();
		cameraPathEvent.hideFlags = HideFlags.HideInInspector;
		AddPoint(cameraPathEvent, cameraPathControlPoint_0);
		RecalculatePoints();
	}

	public CameraPathEvent AddEvent(CameraPathControlPoint cameraPathControlPoint_0, CameraPathControlPoint cameraPathControlPoint_1, float float_1)
	{
		CameraPathEvent cameraPathEvent = base.gameObject.AddComponent<CameraPathEvent>();
		cameraPathEvent.hideFlags = HideFlags.HideInInspector;
		AddPoint(cameraPathEvent, cameraPathControlPoint_0, cameraPathControlPoint_1, float_1);
		RecalculatePoints();
		return cameraPathEvent;
	}

	public void OnAnimationStart(float float_1)
	{
		float_0 = float_1;
	}

	public void CheckEvents(float float_1)
	{
		if (Mathf.Abs(float_1 - float_0) > 0.5f)
		{
			float_0 = float_1;
			return;
		}
		for (int i = 0; i < base.Int32_1; i++)
		{
			CameraPathEvent cameraPathEvent = this[i];
			if ((cameraPathEvent.Single_0 >= float_0 && !(cameraPathEvent.Single_0 > float_1)) || (cameraPathEvent.Single_0 >= float_1 && cameraPathEvent.Single_0 <= float_0))
			{
				switch (cameraPathEvent.types_0)
				{
				case CameraPathEvent.Types.Call:
					Call(cameraPathEvent);
					break;
				case CameraPathEvent.Types.Broadcast:
					BroadCast(cameraPathEvent);
					break;
				}
			}
		}
		float_0 = float_1;
	}

	public void BroadCast(CameraPathEvent cameraPathEvent_0)
	{
		if (cameraPathEventPointHandler_0 != null)
		{
			cameraPathEventPointHandler_0(cameraPathEvent_0.string_0);
		}
	}

	public void Call(CameraPathEvent cameraPathEvent_0)
	{
		if (cameraPathEvent_0.gameObject_0 == null)
		{
			Debug.LogError("Camera Path Event Error: There is an event call without a specified target. Please check " + cameraPathEvent_0.String_0, cameraPath_0);
			return;
		}
		switch (cameraPathEvent_0.argumentTypes_0)
		{
		case CameraPathEvent.ArgumentTypes.None:
			cameraPathEvent_0.gameObject_0.SendMessage(cameraPathEvent_0.string_1, SendMessageOptions.DontRequireReceiver);
			break;
		case CameraPathEvent.ArgumentTypes.Float:
		{
			float num = float.Parse(cameraPathEvent_0.string_2);
			if (float.IsNaN(num))
			{
				Debug.LogError("Camera Path Aniamtor: The argument specified is not a float");
			}
			cameraPathEvent_0.gameObject_0.SendMessage(cameraPathEvent_0.string_1, num, SendMessageOptions.DontRequireReceiver);
			break;
		}
		case CameraPathEvent.ArgumentTypes.Int:
		{
			int result;
			if (int.TryParse(cameraPathEvent_0.string_2, out result))
			{
				cameraPathEvent_0.gameObject_0.SendMessage(cameraPathEvent_0.string_1, result, SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				Debug.LogError("Camera Path Aniamtor: The argument specified is not an integer");
			}
			break;
		}
		case CameraPathEvent.ArgumentTypes.String:
		{
			string string_ = cameraPathEvent_0.string_2;
			cameraPathEvent_0.gameObject_0.SendMessage(cameraPathEvent_0.string_1, string_, SendMessageOptions.DontRequireReceiver);
			break;
		}
		}
	}
}
