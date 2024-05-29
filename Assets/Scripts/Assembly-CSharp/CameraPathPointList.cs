using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraPathPointList : MonoBehaviour
{
	[SerializeField]
	private List<CameraPathPoint> list_0 = new List<CameraPathPoint>();

	[SerializeField]
	protected CameraPath cameraPath_0;

	protected string string_0 = "point";

	[NonSerialized]
	protected bool bool_0;

	[CompilerGenerated]
	private static Dictionary<string, int> dictionary_0;

	public CameraPathPoint this[int index]
	{
		get
		{
			if (cameraPath_0.Boolean_0 && index > list_0.Count - 1)
			{
				index %= list_0.Count;
			}
			if (index < 0)
			{
				Debug.LogError("Index can't be minus");
			}
			if (index >= list_0.Count)
			{
				Debug.LogError("Index out of range");
			}
			return list_0[index];
		}
	}

	public int Int32_0
	{
		get
		{
			if (list_0.Count == 0)
			{
				return 0;
			}
			return (!cameraPath_0.Boolean_0) ? list_0.Count : (list_0.Count + 1);
		}
	}

	public int Int32_1
	{
		get
		{
			return list_0.Count;
		}
	}

	private void OnEnable()
	{
		base.hideFlags = HideFlags.HideInInspector;
	}

	public virtual void Init(CameraPath cameraPath_1)
	{
		if (!bool_0)
		{
			base.hideFlags = HideFlags.HideInInspector;
			cameraPath_0 = cameraPath_1;
			cameraPath_0.CleanUpListsEvent += CleanUp;
			cameraPath_0.RecalculateCurvesEvent += RecalculatePoints;
			cameraPath_0.PathPointRemovedEvent += PathPointRemovedEvent;
			cameraPath_0.CheckStartPointCullEvent += CheckPointCullEventFromStart;
			cameraPath_0.CheckEndPointCullEvent += CheckPointCullEventFromEnd;
			bool_0 = true;
		}
	}

	public virtual void CleanUp()
	{
		cameraPath_0.CleanUpListsEvent -= CleanUp;
		cameraPath_0.RecalculateCurvesEvent -= RecalculatePoints;
		cameraPath_0.PathPointRemovedEvent -= PathPointRemovedEvent;
		cameraPath_0.CheckStartPointCullEvent -= CheckPointCullEventFromStart;
		cameraPath_0.CheckEndPointCullEvent -= CheckPointCullEventFromEnd;
		bool_0 = false;
	}

	public int IndexOf(CameraPathPoint cameraPathPoint_0)
	{
		return list_0.IndexOf(cameraPathPoint_0);
	}

	public void AddPoint(CameraPathPoint cameraPathPoint_0, CameraPathControlPoint cameraPathControlPoint_0, CameraPathControlPoint cameraPathControlPoint_1, float float_0)
	{
		cameraPathPoint_0.positionModes = CameraPathPoint.PositionModes.Free;
		cameraPathPoint_0.cpointA = cameraPathControlPoint_0;
		cameraPathPoint_0.cpointB = cameraPathControlPoint_1;
		cameraPathPoint_0.curvePercentage = float_0;
		list_0.Add(cameraPathPoint_0);
		RecalculatePoints();
	}

	public void AddPoint(CameraPathPoint cameraPathPoint_0, float float_0)
	{
		cameraPathPoint_0.positionModes = CameraPathPoint.PositionModes.FixedToPercent;
		cameraPathPoint_0.Single_0 = float_0;
		list_0.Add(cameraPathPoint_0);
		RecalculatePoints();
	}

	public void AddPoint(CameraPathPoint cameraPathPoint_0, CameraPathControlPoint cameraPathControlPoint_0)
	{
		cameraPathPoint_0.positionModes = CameraPathPoint.PositionModes.FixedToPoint;
		cameraPathPoint_0.point = cameraPathControlPoint_0;
		list_0.Add(cameraPathPoint_0);
		RecalculatePoints();
	}

	public void RemovePoint(CameraPathPoint cameraPathPoint_0)
	{
		list_0.Remove(cameraPathPoint_0);
		RecalculatePoints();
	}

	public void PathPointAddedEvent(CameraPathControlPoint cameraPathControlPoint_0)
	{
		float percentage = cameraPathControlPoint_0.percentage;
		for (int i = 0; i < Int32_1; i++)
		{
			CameraPathPoint cameraPathPoint = list_0[i];
			if (cameraPathPoint.positionModes != 0)
			{
				continue;
			}
			float percentage2 = cameraPathPoint.cpointA.percentage;
			float percentage3 = cameraPathPoint.cpointB.percentage;
			if (percentage > percentage2 && percentage < percentage3)
			{
				if (percentage < cameraPathPoint.Single_0)
				{
					cameraPathPoint.cpointA = cameraPathControlPoint_0;
				}
				else
				{
					cameraPathPoint.cpointB = cameraPathControlPoint_0;
				}
				cameraPath_0.GetCurvePercentage(cameraPathPoint);
			}
		}
	}

	public void PathPointRemovedEvent(CameraPathControlPoint cameraPathControlPoint_0)
	{
		for (int i = 0; i < Int32_1; i++)
		{
			CameraPathPoint cameraPathPoint = list_0[i];
			switch (cameraPathPoint.positionModes)
			{
			case CameraPathPoint.PositionModes.Free:
				if (cameraPathPoint.cpointA == cameraPathControlPoint_0)
				{
					CameraPathControlPoint point = cameraPath_0.GetPoint(cameraPathControlPoint_0.index - 1);
					cameraPathPoint.cpointA = point;
					cameraPath_0.GetCurvePercentage(cameraPathPoint);
				}
				if (cameraPathPoint.cpointB == cameraPathControlPoint_0)
				{
					CameraPathControlPoint point2 = cameraPath_0.GetPoint(cameraPathControlPoint_0.index + 1);
					cameraPathPoint.cpointB = point2;
					cameraPath_0.GetCurvePercentage(cameraPathPoint);
				}
				break;
			case CameraPathPoint.PositionModes.FixedToPoint:
				if (cameraPathPoint.point == cameraPathControlPoint_0)
				{
					list_0.Remove(cameraPathPoint);
					i--;
				}
				break;
			}
		}
		RecalculatePoints();
	}

	public void CheckPointCullEventFromStart(float float_0)
	{
		int num = list_0.Count;
		for (int i = 0; i < num; i++)
		{
			CameraPathPoint cameraPathPoint = list_0[i];
			if (cameraPathPoint.positionModes != CameraPathPoint.PositionModes.FixedToPercent && cameraPathPoint.Single_0 < float_0)
			{
				list_0.Remove(cameraPathPoint);
				i--;
				num--;
			}
		}
		RecalculatePoints();
	}

	public void CheckPointCullEventFromEnd(float float_0)
	{
		int num = list_0.Count;
		for (int i = 0; i < num; i++)
		{
			CameraPathPoint cameraPathPoint = list_0[i];
			if (cameraPathPoint.positionModes != CameraPathPoint.PositionModes.FixedToPercent && cameraPathPoint.Single_0 > float_0)
			{
				list_0.Remove(cameraPathPoint);
				i--;
				num--;
			}
		}
		RecalculatePoints();
	}

	protected int GetNextPointIndex(float float_0)
	{
		if (Int32_1 == 0)
		{
			Debug.LogError("No points to draw from");
		}
		if (float_0 == 0f)
		{
			return 1;
		}
		if (float_0 == 1f)
		{
			return list_0.Count - 1;
		}
		int count = list_0.Count;
		int num = 0;
		int num2 = 1;
		while (true)
		{
			if (num2 < count)
			{
				if (!(list_0[num2].Single_0 <= float_0))
				{
					break;
				}
				num = num2;
				num2++;
				continue;
			}
			return num;
		}
		return num + 1;
	}

	protected int GetLastPointIndex(float float_0)
	{
		if (Int32_1 == 0)
		{
			Debug.LogError("No points to draw from");
		}
		if (float_0 == 0f)
		{
			return 0;
		}
		if (float_0 == 1f)
		{
			return (cameraPath_0.Boolean_0 || cameraPath_0.Boolean_3) ? (list_0.Count - 1) : (list_0.Count - 2);
		}
		int count = list_0.Count;
		int result = 0;
		int num = 1;
		while (true)
		{
			if (num < count)
			{
				if (!(list_0[num].Single_0 <= float_0))
				{
					break;
				}
				result = num;
				num++;
				continue;
			}
			return result;
		}
		return result;
	}

	public CameraPathPoint GetPoint(int int_0)
	{
		int count = list_0.Count;
		if (count == 0)
		{
			return null;
		}
		CameraPathPointList cameraPathPointList = this;
		if (cameraPath_0.Boolean_3)
		{
			switch (string_0)
			{
			case "Orientation":
				cameraPathPointList = cameraPath_0.CameraPath_0.CameraPathOrientationList_0;
				break;
			case "FOV":
				cameraPathPointList = cameraPath_0.CameraPath_0.CameraPathFOVList_0;
				break;
			case "Tilt":
				cameraPathPointList = cameraPath_0.CameraPath_0.CameraPathTiltList_0;
				break;
			}
		}
		if (cameraPathPointList == this)
		{
			if (!cameraPath_0.Boolean_0)
			{
				return list_0[Mathf.Clamp(int_0, 0, count - 1)];
			}
			if (int_0 >= count)
			{
				int_0 -= count;
			}
			if (int_0 < 0)
			{
				int_0 += count;
			}
		}
		else if (cameraPath_0.Boolean_0)
		{
			if (int_0 == count)
			{
				int_0 = 0;
				cameraPathPointList = null;
			}
			else if (int_0 > count)
			{
				int_0 = Mathf.Clamp(int_0, 0, cameraPathPointList.Int32_1 - 1);
			}
			else if (int_0 < 0)
			{
				int_0 += count;
				cameraPathPointList = null;
			}
			else
			{
				cameraPathPointList = null;
			}
		}
		else if (int_0 > count - 1)
		{
			int_0 = Mathf.Clamp(int_0 - count, 0, cameraPathPointList.Int32_1 - 1);
		}
		else if (int_0 < 0)
		{
			int_0 = 0;
			cameraPathPointList = null;
		}
		else
		{
			int_0 = Mathf.Clamp(int_0, 0, count - 1);
			cameraPathPointList = null;
		}
		if (cameraPathPointList != null)
		{
			return cameraPathPointList[int_0];
		}
		return list_0[int_0];
	}

	public CameraPathPoint GetPoint(CameraPathControlPoint cameraPathControlPoint_0)
	{
		if (list_0.Count == 0)
		{
			return null;
		}
		foreach (CameraPathPoint item in list_0)
		{
			if (item.positionModes == CameraPathPoint.PositionModes.FixedToPoint && item.point == cameraPathControlPoint_0)
			{
				return item;
			}
		}
		return null;
	}

	public void Clear()
	{
		list_0.Clear();
	}

	public CameraPathPoint DuplicatePointCheck()
	{
		foreach (CameraPathPoint item in list_0)
		{
			foreach (CameraPathPoint item2 in list_0)
			{
				if (item != item2 && item.Single_0 == item2.Single_0)
				{
					return item;
				}
			}
		}
		return null;
	}

	protected virtual void RecalculatePoints()
	{
		if (cameraPath_0 == null)
		{
			Debug.LogError("Camera Path Point List was not initialised - run Init();");
			return;
		}
		int count = list_0.Count;
		if (count == 0)
		{
			return;
		}
		List<CameraPathPoint> list = new List<CameraPathPoint>();
		for (int i = 0; i < count; i++)
		{
			if (list_0[i] == null)
			{
				continue;
			}
			CameraPathPoint cameraPathPoint = list_0[i];
			if (i == 0)
			{
				list.Add(cameraPathPoint);
				continue;
			}
			bool flag = false;
			foreach (CameraPathPoint item in list)
			{
				if (!(item.Single_0 <= cameraPathPoint.Single_0))
				{
					list.Insert(list.IndexOf(item), cameraPathPoint);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				list.Add(cameraPathPoint);
			}
		}
		count = list.Count;
		list_0 = list;
		for (int j = 0; j < count; j++)
		{
			CameraPathPoint cameraPathPoint2 = list_0[j];
			cameraPathPoint2.givenName = string_0 + " Point " + j;
			cameraPathPoint2.fullName = cameraPath_0.name + " " + string_0 + " Point " + j;
			cameraPathPoint2.index = j;
			if (cameraPath_0.Int32_1 < 2)
			{
				continue;
			}
			switch (cameraPathPoint2.positionModes)
			{
			case CameraPathPoint.PositionModes.Free:
				if (!(cameraPathPoint2.cpointA == cameraPathPoint2.cpointB))
				{
					cameraPathPoint2.Single_0 = cameraPath_0.GetPathPercentage(cameraPathPoint2.cpointA, cameraPathPoint2.cpointB, cameraPathPoint2.curvePercentage);
					cameraPathPoint2.Single_2 = ((!cameraPath_0.Boolean_1) ? cameraPathPoint2.Single_0 : cameraPath_0.CalculateNormalisedPercentage(cameraPathPoint2.Single_0));
					cameraPathPoint2.worldPosition = cameraPath_0.GetPathPosition(cameraPathPoint2.Single_0, true);
					break;
				}
				cameraPathPoint2.positionModes = CameraPathPoint.PositionModes.FixedToPoint;
				cameraPathPoint2.point = cameraPathPoint2.cpointA;
				cameraPathPoint2.cpointA = null;
				cameraPathPoint2.cpointB = null;
				cameraPathPoint2.Single_0 = cameraPathPoint2.point.percentage;
				cameraPathPoint2.Single_2 = ((!cameraPath_0.Boolean_1) ? cameraPathPoint2.point.percentage : cameraPathPoint2.point.normalisedPercentage);
				cameraPathPoint2.worldPosition = cameraPathPoint2.point.Vector3_1;
				return;
			case CameraPathPoint.PositionModes.FixedToPoint:
				if (cameraPathPoint2.point == null)
				{
					cameraPathPoint2.point = cameraPath_0[cameraPath_0.GetNearestPointIndex(cameraPathPoint2.Single_1)];
				}
				cameraPathPoint2.Single_0 = cameraPathPoint2.point.percentage;
				cameraPathPoint2.Single_2 = ((!cameraPath_0.Boolean_1) ? cameraPathPoint2.point.percentage : cameraPathPoint2.point.normalisedPercentage);
				cameraPathPoint2.worldPosition = cameraPathPoint2.point.Vector3_1;
				break;
			case CameraPathPoint.PositionModes.FixedToPercent:
				cameraPathPoint2.worldPosition = cameraPath_0.GetPathPosition(cameraPathPoint2.Single_0, true);
				cameraPathPoint2.Single_2 = ((!cameraPath_0.Boolean_1) ? cameraPathPoint2.Single_0 : cameraPath_0.CalculateNormalisedPercentage(cameraPathPoint2.Single_0));
				break;
			}
		}
	}

	public void ReassignCP(CameraPathControlPoint cameraPathControlPoint_0, CameraPathControlPoint cameraPathControlPoint_1)
	{
		foreach (CameraPathPoint item in list_0)
		{
			if (item.point == cameraPathControlPoint_0)
			{
				item.point = cameraPathControlPoint_1;
			}
			if (item.cpointA == cameraPathControlPoint_0)
			{
				item.cpointA = cameraPathControlPoint_1;
			}
			if (item.cpointB == cameraPathControlPoint_0)
			{
				item.cpointB = cameraPathControlPoint_1;
			}
		}
	}
}
