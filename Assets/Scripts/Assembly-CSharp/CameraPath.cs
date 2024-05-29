using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraPath : MonoBehaviour
{
	public enum PointModes
	{
		Transform = 0,
		ControlPoints = 1,
		FOV = 2,
		Events = 3,
		Speed = 4,
		Delay = 5,
		Ease = 6,
		Orientations = 7,
		Tilt = 8,
		AddPathPoints = 9,
		RemovePathPoints = 10,
		AddOrientations = 11,
		RemoveOrientations = 12,
		TargetOrientation = 13,
		AddFovs = 14,
		RemoveFovs = 15,
		AddTilts = 16,
		RemoveTilts = 17,
		AddEvents = 18,
		RemoveEvents = 19,
		AddSpeeds = 20,
		RemoveSpeeds = 21,
		AddDelays = 22,
		RemoveDelays = 23,
		Options = 24
	}

	public enum Interpolation
	{
		Linear = 0,
		SmoothStep = 1,
		CatmullRom = 2,
		Hermite = 3,
		Bezier = 4
	}

	public delegate void RecalculateCurvesHandler();

	public delegate void PathPointAddedHandler(CameraPathControlPoint cameraPathControlPoint_0);

	public delegate void PathPointRemovedHandler(CameraPathControlPoint cameraPathControlPoint_0);

	public delegate void CheckStartPointCullHandler(float float_0);

	public delegate void CheckEndPointCullHandler(float float_0);

	public delegate void CleanUpListsHandler();

	private const float float_0 = 0.5f;

	public static float float_1 = 3.2f;

	public float version = float_1;

	[SerializeField]
	private List<CameraPathControlPoint> list_0 = new List<CameraPathControlPoint>();

	[SerializeField]
	private Interpolation interpolation_0 = Interpolation.Bezier;

	[SerializeField]
	private bool bool_0;

	[SerializeField]
	private float float_2;

	[SerializeField]
	private float[] float_3;

	[SerializeField]
	private float[] float_4;

	[SerializeField]
	private Vector3[] vector3_0;

	[SerializeField]
	private float[] float_5;

	[SerializeField]
	private float float_6 = 0.1f;

	[SerializeField]
	private int int_0;

	[SerializeField]
	private Vector3[] vector3_1;

	[SerializeField]
	private CameraPathControlPoint[] cameraPathControlPoint_0;

	[SerializeField]
	private CameraPathControlPoint[] cameraPathControlPoint_1;

	[SerializeField]
	private CameraPathOrientationList cameraPathOrientationList_0;

	[SerializeField]
	private CameraPathFOVList cameraPathFOVList_0;

	[SerializeField]
	private CameraPathTiltList cameraPathTiltList_0;

	[SerializeField]
	private CameraPathSpeedList cameraPathSpeedList_0;

	[SerializeField]
	private CameraPathEventList cameraPathEventList_0;

	[SerializeField]
	private CameraPathDelayList cameraPathDelayList_0;

	[SerializeField]
	private bool bool_1 = true;

	[SerializeField]
	private bool bool_2;

	[SerializeField]
	private bool bool_3 = true;

	[SerializeField]
	private Bounds bounds_0 = default(Bounds);

	public float hermiteTension;

	public float hermiteBias;

	public GameObject editorPreview;

	public int selectedPoint;

	public PointModes pointMode;

	public float addPointAtPercent;

	[SerializeField]
	private CameraPath cameraPath_0;

	[SerializeField]
	private bool bool_4;

	public bool showGizmos = true;

	public Color selectedPathColour = CameraPathColours.color_1;

	public Color unselectedPathColour = CameraPathColours.color_4;

	public Color selectedPointColour = CameraPathColours.color_0;

	public Color unselectedPointColour = CameraPathColours.color_1;

	public bool showOrientationIndicators;

	public float orientationIndicatorUnitLength = 2.5f;

	public Color orientationIndicatorColours = CameraPathColours.color_7;

	public bool autoSetStoedPointRes = true;

	public bool enableUndo = true;

	public bool showPreview = true;

	public bool enablePreviews = true;

	private RecalculateCurvesHandler recalculateCurvesHandler_0;

	private PathPointAddedHandler pathPointAddedHandler_0;

	private PathPointRemovedHandler pathPointRemovedHandler_0;

	private CheckStartPointCullHandler checkStartPointCullHandler_0;

	private CheckEndPointCullHandler checkEndPointCullHandler_0;

	private CleanUpListsHandler cleanUpListsHandler_0;

	public CameraPathControlPoint this[int index]
	{
		get
		{
			int count = list_0.Count;
			if (bool_2)
			{
				if (Boolean_3)
				{
					if (index == count)
					{
						index = 0;
					}
					else
					{
						if (index > count)
						{
							return cameraPath_0[index % count];
						}
						if (index < 0)
						{
							Debug.LogError("Index out of range");
						}
					}
				}
				else
				{
					index %= count;
				}
			}
			else
			{
				if (index < 0)
				{
					Debug.LogError("Index can't be minus");
				}
				if (index >= list_0.Count)
				{
					if (index >= list_0.Count && Boolean_3)
					{
						return CameraPath_0[index % count];
					}
					Debug.LogError("Index out of range");
				}
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
			int num = ((!bool_2) ? list_0.Count : (list_0.Count + 1));
			if (Boolean_3)
			{
				num++;
			}
			return num;
		}
	}

	public int Int32_1
	{
		get
		{
			return list_0.Count;
		}
	}

	public int Int32_2
	{
		get
		{
			if (list_0.Count < 2)
			{
				return 0;
			}
			return Int32_0 - 1;
		}
	}

	public bool Boolean_0
	{
		get
		{
			return bool_2;
		}
		set
		{
			if (bool_2 != value)
			{
				bool_2 = value;
				RecalculateStoredValues();
			}
		}
	}

	public float Single_0
	{
		get
		{
			return float_2;
		}
	}

	public CameraPathOrientationList CameraPathOrientationList_0
	{
		get
		{
			return cameraPathOrientationList_0;
		}
	}

	public CameraPathFOVList CameraPathFOVList_0
	{
		get
		{
			return cameraPathFOVList_0;
		}
	}

	public CameraPathTiltList CameraPathTiltList_0
	{
		get
		{
			return cameraPathTiltList_0;
		}
	}

	public CameraPathSpeedList CameraPathSpeedList_0
	{
		get
		{
			return cameraPathSpeedList_0;
		}
	}

	public CameraPathEventList CameraPathEventList_0
	{
		get
		{
			return cameraPathEventList_0;
		}
	}

	public CameraPathDelayList CameraPathDelayList_0
	{
		get
		{
			return cameraPathDelayList_0;
		}
	}

	public Bounds Bounds_0
	{
		get
		{
			return bounds_0;
		}
	}

	public int Int32_3
	{
		get
		{
			return int_0;
		}
	}

	public CameraPathControlPoint[] CameraPathControlPoint_0
	{
		get
		{
			return cameraPathControlPoint_0;
		}
	}

	public CameraPathControlPoint[] CameraPathControlPoint_1
	{
		get
		{
			return cameraPathControlPoint_1;
		}
	}

	public Vector3[] Vector3_0
	{
		get
		{
			return vector3_0;
		}
	}

	public bool Boolean_1
	{
		get
		{
			return bool_3;
		}
		set
		{
			bool_3 = value;
		}
	}

	public Interpolation Interpolation_0
	{
		get
		{
			return interpolation_0;
		}
		set
		{
			if (value != interpolation_0)
			{
				interpolation_0 = value;
				RecalculateStoredValues();
			}
		}
	}

	public CameraPath CameraPath_0
	{
		get
		{
			return cameraPath_0;
		}
		set
		{
			if (value != cameraPath_0)
			{
				if (value == this)
				{
					Debug.LogError("Do not link a path to itself! The Universe would crumble and it would be your fault!! If you want to loop a path, just toggle the loop option...");
					return;
				}
				cameraPath_0 = value;
				cameraPath_0.GetComponent<CameraPathAnimator>().playOnStart = false;
				RecalculateStoredValues();
			}
		}
	}

	public bool Boolean_2
	{
		get
		{
			return bool_4;
		}
		set
		{
			if (bool_4 != value)
			{
				bool_4 = value;
				RecalculateStoredValues();
			}
		}
	}

	public bool Boolean_3
	{
		get
		{
			return CameraPath_0 != null && Boolean_2;
		}
	}

	public float Single_1
	{
		get
		{
			return float_6;
		}
		set
		{
			float_6 = value;
		}
	}

	public event RecalculateCurvesHandler RecalculateCurvesEvent
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			recalculateCurvesHandler_0 = (RecalculateCurvesHandler)Delegate.Combine(recalculateCurvesHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			recalculateCurvesHandler_0 = (RecalculateCurvesHandler)Delegate.Remove(recalculateCurvesHandler_0, value);
		}
	}

	public event PathPointAddedHandler PathPointAddedEvent
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			pathPointAddedHandler_0 = (PathPointAddedHandler)Delegate.Combine(pathPointAddedHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			pathPointAddedHandler_0 = (PathPointAddedHandler)Delegate.Remove(pathPointAddedHandler_0, value);
		}
	}

	public event PathPointRemovedHandler PathPointRemovedEvent
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			pathPointRemovedHandler_0 = (PathPointRemovedHandler)Delegate.Combine(pathPointRemovedHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			pathPointRemovedHandler_0 = (PathPointRemovedHandler)Delegate.Remove(pathPointRemovedHandler_0, value);
		}
	}

	public event CheckStartPointCullHandler CheckStartPointCullEvent
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			checkStartPointCullHandler_0 = (CheckStartPointCullHandler)Delegate.Combine(checkStartPointCullHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			checkStartPointCullHandler_0 = (CheckStartPointCullHandler)Delegate.Remove(checkStartPointCullHandler_0, value);
		}
	}

	public event CheckEndPointCullHandler CheckEndPointCullEvent
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			checkEndPointCullHandler_0 = (CheckEndPointCullHandler)Delegate.Combine(checkEndPointCullHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			checkEndPointCullHandler_0 = (CheckEndPointCullHandler)Delegate.Remove(checkEndPointCullHandler_0, value);
		}
	}

	public event CleanUpListsHandler CleanUpListsEvent
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			cleanUpListsHandler_0 = (CleanUpListsHandler)Delegate.Combine(cleanUpListsHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			cleanUpListsHandler_0 = (CleanUpListsHandler)Delegate.Remove(cleanUpListsHandler_0, value);
		}
	}

	public float StoredArcLength(int int_1)
	{
		int_1 = ((!bool_2) ? Mathf.Clamp(int_1, 0, Int32_2 - 1) : (int_1 % (Int32_2 - 1)));
		return float_3[int_1];
	}

	public int StoredValueIndex(float float_7)
	{
		int num = Int32_3 - 1;
		return Mathf.Clamp(Mathf.RoundToInt((float)num * float_7), 0, num);
	}

	public CameraPathControlPoint AddPoint(Vector3 vector3_2)
	{
		CameraPathControlPoint cameraPathControlPoint = base.gameObject.AddComponent<CameraPathControlPoint>();
		cameraPathControlPoint.hideFlags = HideFlags.HideInInspector;
		cameraPathControlPoint.Vector3_0 = vector3_2;
		list_0.Add(cameraPathControlPoint);
		if (bool_1)
		{
			CameraPathOrientationList_0.AddOrientation(cameraPathControlPoint);
		}
		RecalculateStoredValues();
		pathPointAddedHandler_0(cameraPathControlPoint);
		return cameraPathControlPoint;
	}

	public void AddPoint(CameraPathControlPoint cameraPathControlPoint_2)
	{
		list_0.Add(cameraPathControlPoint_2);
		RecalculateStoredValues();
		pathPointAddedHandler_0(cameraPathControlPoint_2);
	}

	public void InsertPoint(CameraPathControlPoint cameraPathControlPoint_2, int int_1)
	{
		list_0.Insert(int_1, cameraPathControlPoint_2);
		RecalculateStoredValues();
		pathPointAddedHandler_0(cameraPathControlPoint_2);
	}

	public CameraPathControlPoint InsertPoint(int int_1)
	{
		CameraPathControlPoint cameraPathControlPoint = base.gameObject.AddComponent<CameraPathControlPoint>();
		cameraPathControlPoint.hideFlags = HideFlags.HideInInspector;
		list_0.Insert(int_1, cameraPathControlPoint);
		RecalculateStoredValues();
		pathPointAddedHandler_0(cameraPathControlPoint);
		return cameraPathControlPoint;
	}

	public void RemovePoint(int int_1)
	{
		RemovePoint(this[int_1]);
	}

	public bool RemovePoint(string string_0)
	{
		foreach (CameraPathControlPoint item in list_0)
		{
			if (item.String_0 == string_0)
			{
				RemovePoint(item);
				return true;
			}
		}
		return false;
	}

	public void RemovePoint(Vector3 vector3_2)
	{
		foreach (CameraPathControlPoint item in list_0)
		{
			if (item.Vector3_1 == vector3_2)
			{
				RemovePoint(item);
			}
		}
		float nearestPoint = GetNearestPoint(vector3_2, true);
		RemovePoint(GetNearestPointIndex(nearestPoint));
	}

	public void RemovePoint(CameraPathControlPoint cameraPathControlPoint_2)
	{
		if (list_0.Count < 3)
		{
			Debug.Log("We can't see any point in allowing you to delete any more points so we're not going to do it.");
			return;
		}
		pathPointRemovedHandler_0(cameraPathControlPoint_2);
		int num = list_0.IndexOf(cameraPathControlPoint_2);
		if (num == 0)
		{
			float pathPercentage = GetPathPercentage(1);
			checkStartPointCullHandler_0(pathPercentage);
		}
		if (num == Int32_1 - 1)
		{
			float pathPercentage2 = GetPathPercentage(Int32_1 - 2);
			checkEndPointCullHandler_0(pathPercentage2);
		}
		list_0.Remove(cameraPathControlPoint_2);
		RecalculateStoredValues();
	}

	private float ParsePercentage(float float_7)
	{
		if (float_7 == 0f)
		{
			return 0f;
		}
		if (float_7 == 1f)
		{
			return 1f;
		}
		float_7 = ((!bool_2) ? Mathf.Clamp01(float_7) : (float_7 % 1f));
		if (bool_3)
		{
			int max = Int32_3 - 1;
			float num = 1f / (float)Int32_3;
			int num2 = Mathf.Clamp(Mathf.FloorToInt((float)Int32_3 * float_7), 0, max);
			int num3 = Mathf.Clamp(num2 + 1, 0, max);
			float num4 = (float)num2 * num;
			float num5 = (float)num3 * num;
			float num6 = float_5[num2];
			float num7 = float_5[num3];
			if (num6 == num7)
			{
				return float_7;
			}
			float t = (float_7 - num4) / (num5 - num4);
			float_7 = Mathf.Lerp(num6, num7, t);
		}
		return float_7;
	}

	public float CalculateNormalisedPercentage(float float_7)
	{
		if (Int32_1 < 2)
		{
			return float_7;
		}
		if (float_7 == 0f)
		{
			return 0f;
		}
		if (float_7 == 1f)
		{
			return 1f;
		}
		if (float_2 == 0f)
		{
			return float_7;
		}
		float num = float_7 * float_2;
		int num2 = 0;
		int num3 = Int32_3;
		int num4 = 0;
		while (num2 < num3)
		{
			num4 = num2 + (num3 - num2) / 2;
			if (float_4[num4] < num)
			{
				num2 = num4 + 1;
			}
			else
			{
				num3 = num4;
			}
		}
		if (float_4[num4] > num && num4 > 0)
		{
			num4--;
		}
		float num5 = float_4[num4];
		float result = (float)num4 / (float)Int32_3;
		if (num5 == num)
		{
			return result;
		}
		return ((float)num4 + (num - num5) / (float_4[num4 + 1] - num5)) / (float)Int32_3;
	}

	public float DeNormalisePercentage(float float_7)
	{
		int num = float_5.Length;
		int num2 = 0;
		while (true)
		{
			if (num2 < num)
			{
				if (!(float_5[num2] <= float_7))
				{
					break;
				}
				num2++;
				continue;
			}
			return 1f;
		}
		if (num2 == 0)
		{
			return 0f;
		}
		float from = (float)(num2 - 1) / (float)num;
		float to = (float)num2 / (float)num;
		float num3 = float_5[num2 - 1];
		float num4 = float_5[num2];
		float t = (float_7 - num3) / (num4 - num3);
		return Mathf.Lerp(from, to, t);
	}

	public int GetPointNumber(float float_7)
	{
		float_7 = ParsePercentage(float_7);
		float num = 1f / (float)Int32_2;
		return Mathf.Clamp(Mathf.FloorToInt(float_7 / num), 0, list_0.Count - 1);
	}

	public Vector3 GetPathPosition(float float_7)
	{
		return GetPathPosition(float_7, false);
	}

	public Vector3 GetPathPosition(float float_7, bool bool_5)
	{
		if (Int32_1 < 2)
		{
			Debug.LogError("Not enough points to define a curve");
			return Vector3.zero;
		}
		if (!bool_5)
		{
			float_7 = ParsePercentage(float_7);
		}
		float num = 1f / (float)Int32_2;
		int num2 = Mathf.FloorToInt(float_7 / num);
		float t = Mathf.Clamp01((float_7 - (float)num2 * num) * (float)Int32_2);
		CameraPathControlPoint point = GetPoint(num2);
		CameraPathControlPoint point2 = GetPoint(num2 + 1);
		if (!(point == null) && !(point2 == null))
		{
			switch (Interpolation_0)
			{
			default:
				return Vector3.zero;
			case Interpolation.Linear:
				return Vector3.Lerp(point.Vector3_1, point2.Vector3_1, t);
			case Interpolation.SmoothStep:
				return Vector3.Lerp(point.Vector3_1, point2.Vector3_1, CPMath.SmoothStep(t));
			case Interpolation.CatmullRom:
			{
				CameraPathControlPoint point3 = GetPoint(num2 - 1);
				CameraPathControlPoint point4 = GetPoint(num2 + 2);
				return CPMath.CalculateCatmullRom(point3.Vector3_1, point.Vector3_1, point2.Vector3_1, point4.Vector3_1, t);
			}
			case Interpolation.Hermite:
			{
				CameraPathControlPoint point3 = GetPoint(num2 - 1);
				CameraPathControlPoint point4 = GetPoint(num2 + 2);
				return CPMath.CalculateHermite(point3.Vector3_1, point.Vector3_1, point2.Vector3_1, point4.Vector3_1, t, hermiteTension, hermiteBias);
			}
			case Interpolation.Bezier:
				return CPMath.CalculateBezier(t, point.Vector3_1, point.Vector3_2, point2.Vector3_5, point2.Vector3_1);
			}
		}
		return Vector3.zero;
	}

	public Quaternion GetPathRotation(float float_7, bool bool_5)
	{
		if (!bool_5)
		{
			float_7 = ParsePercentage(float_7);
		}
		return CameraPathOrientationList_0.GetOrientation(float_7);
	}

	public Vector3 GetPathDirection(float float_7)
	{
		return GetPathDirection(float_7, true);
	}

	public Vector3 GetPathDirection(float float_7, bool bool_5)
	{
		if (bool_5)
		{
			float_7 = ParsePercentage(float_7);
		}
		return vector3_1[StoredValueIndex(float_7)];
	}

	public float GetPathTilt(float float_7)
	{
		float_7 = ParsePercentage(float_7);
		return cameraPathTiltList_0.GetTilt(float_7);
	}

	public float GetPathFOV(float float_7)
	{
		float_7 = ParsePercentage(float_7);
		return cameraPathFOVList_0.GetFOV(float_7);
	}

	public float GetPathSpeed(float float_7)
	{
		float_7 = ParsePercentage(float_7);
		float speed = cameraPathSpeedList_0.GetSpeed(float_7);
		return speed * cameraPathDelayList_0.CheckEase(float_7);
	}

	public float GetPathEase(float float_7)
	{
		float_7 = ParsePercentage(float_7);
		return cameraPathDelayList_0.CheckEase(float_7);
	}

	public void CheckEvents(float float_7)
	{
		float_7 = ParsePercentage(float_7);
		cameraPathEventList_0.CheckEvents(float_7);
		cameraPathDelayList_0.CheckEvents(float_7);
	}

	public float GetPathPercentage(CameraPathControlPoint cameraPathControlPoint_2)
	{
		int num = list_0.IndexOf(cameraPathControlPoint_2);
		return (float)num / (float)Int32_2;
	}

	public float GetPathPercentage(int int_1)
	{
		return (float)int_1 / (float)Int32_2;
	}

	public int GetNearestPointIndex(float float_7)
	{
		float_7 = ParsePercentage(float_7);
		return Mathf.RoundToInt((float)Int32_2 * float_7);
	}

	public int GetLastPointIndex(float float_7, bool bool_5)
	{
		if (bool_5)
		{
			float_7 = ParsePercentage(float_7);
		}
		return Mathf.FloorToInt((float)Int32_2 * float_7);
	}

	public int GetNextPointIndex(float float_7, bool bool_5)
	{
		if (bool_5)
		{
			float_7 = ParsePercentage(float_7);
		}
		return Mathf.CeilToInt((float)Int32_2 * float_7);
	}

	public float GetCurvePercentage(CameraPathControlPoint cameraPathControlPoint_2, CameraPathControlPoint cameraPathControlPoint_3, float float_7)
	{
		float num = GetPathPercentage(cameraPathControlPoint_2);
		float num2 = GetPathPercentage(cameraPathControlPoint_3);
		if (num == num2)
		{
			return num;
		}
		if (num > num2)
		{
			float num3 = num2;
			num2 = num;
			num = num3;
		}
		return Mathf.Clamp01((float_7 - num) / (num2 - num));
	}

	public float GetCurvePercentage(CameraPathPoint cameraPathPoint_0, CameraPathPoint cameraPathPoint_1, float float_7)
	{
		float num = cameraPathPoint_0.Single_0;
		float num2 = cameraPathPoint_1.Single_0;
		if (num > num2)
		{
			float num3 = num2;
			num2 = num;
			num = num3;
		}
		return Mathf.Clamp01((float_7 - num) / (num2 - num));
	}

	public float GetCurvePercentage(CameraPathPoint cameraPathPoint_0)
	{
		float num = GetPathPercentage(cameraPathPoint_0.cpointA);
		float num2 = GetPathPercentage(cameraPathPoint_0.cpointB);
		if (num > num2)
		{
			float num3 = num2;
			num2 = num;
			num = num3;
		}
		cameraPathPoint_0.curvePercentage = Mathf.Clamp01((cameraPathPoint_0.Single_0 - num) / (num2 - num));
		return cameraPathPoint_0.curvePercentage;
	}

	public float GetOutroEasePercentage(CameraPathDelay cameraPathDelay_0)
	{
		float num = cameraPathDelay_0.Single_0;
		float num2 = cameraPathDelayList_0.GetPoint(cameraPathDelay_0.index + 1).Single_0;
		if (num > num2)
		{
			float num3 = num2;
			num2 = num;
			num = num3;
		}
		return Mathf.Lerp(num, num2, cameraPathDelay_0.float_4);
	}

	public float GetIntroEasePercentage(CameraPathDelay cameraPathDelay_0)
	{
		float single_ = cameraPathDelayList_0.GetPoint(cameraPathDelay_0.index - 1).Single_0;
		float single_2 = cameraPathDelay_0.Single_0;
		return Mathf.Lerp(single_, single_2, 1f - cameraPathDelay_0.float_3);
	}

	public float GetPathPercentage(CameraPathControlPoint cameraPathControlPoint_2, CameraPathControlPoint cameraPathControlPoint_3, float float_7)
	{
		float pathPercentage = GetPathPercentage(cameraPathControlPoint_2);
		float pathPercentage2 = GetPathPercentage(cameraPathControlPoint_3);
		return Mathf.Lerp(pathPercentage, pathPercentage2, float_7);
	}

	public float GetPathPercentage(float float_7, float float_8, float float_9)
	{
		return Mathf.Lerp(float_7, float_8, float_9);
	}

	public int GetStoredPoint(float float_7)
	{
		float_7 = ParsePercentage(float_7);
		return Mathf.Clamp(Mathf.FloorToInt((float)Int32_3 * float_7), 0, Int32_3 - 1);
	}

	private void Awake()
	{
		Init();
	}

	private void Start()
	{
		if (!Application.isPlaying && version != float_1)
		{
			if (version > float_1)
			{
				Debug.LogError("Camera Path v." + version + ": Great scot! This data is from the future! (version:" + float_1 + ") - need to avoid contact to ensure the survival of the universe...");
			}
			else
			{
				Debug.Log("Camera Path v." + version + " Upgrading to version " + float_1 + "\nRemember to backup your data!");
				version = float_1;
			}
		}
	}

	private void OnValidate()
	{
		InitialiseLists();
		if (!Application.isPlaying)
		{
			RecalculateStoredValues();
		}
	}

	private void OnDestroy()
	{
		Clear();
		if (cleanUpListsHandler_0 != null)
		{
			cleanUpListsHandler_0();
		}
	}

	public void RecalculateStoredValues()
	{
		if (autoSetStoedPointRes && this.float_2 > 0f)
		{
			float_6 = this.float_2 / 1000f;
		}
		for (int i = 0; i < Int32_1; i++)
		{
			CameraPathControlPoint cameraPathControlPoint = list_0[i];
			cameraPathControlPoint.percentage = GetPathPercentage(i);
			cameraPathControlPoint.normalisedPercentage = CalculateNormalisedPercentage(list_0[i].percentage);
			cameraPathControlPoint.givenName = "Point " + i;
			cameraPathControlPoint.fullName = base.name + " Point " + i;
			cameraPathControlPoint.index = i;
			cameraPathControlPoint.hideFlags = HideFlags.HideInInspector;
		}
		if (list_0.Count >= 2)
		{
			this.float_2 = 0f;
			for (int j = 0; j < Int32_2; j++)
			{
				CameraPathControlPoint point = GetPoint(j);
				CameraPathControlPoint point2 = GetPoint(j + 1);
				float num = 0f;
				num += Vector3.Distance(point.Vector3_1, point.Vector3_2);
				num += Vector3.Distance(point.Vector3_2, point2.Vector3_5);
				num += Vector3.Distance(point2.Vector3_5, point2.Vector3_1);
				this.float_2 += num;
			}
			int_0 = Mathf.Max(Mathf.RoundToInt(this.float_2 / float_6), 1);
			this.float_3 = new float[Int32_2];
			float num2 = 1f / (float)int_0;
			float num3 = 0f;
			this.float_4 = new float[int_0];
			this.float_4[0] = 0f;
			for (int k = 0; k < int_0 - 1; k++)
			{
				float num4 = num2 * (float)(k + 1);
				float float_ = num2 * (float)(k + 1) + num2;
				Vector3 pathPosition = GetPathPosition(num4, true);
				Vector3 pathPosition2 = GetPathPosition(float_, true);
				float num5 = Vector3.Distance(pathPosition, pathPosition2);
				num3 += num5;
				int num6 = Mathf.FloorToInt(num4 * (float)Int32_2);
				this.float_3[num6] += num5;
				this.float_4[k + 1] = num3;
			}
			this.float_2 = num3;
			vector3_0 = new Vector3[int_0];
			vector3_1 = new Vector3[int_0];
			this.float_5 = new float[int_0];
			for (int l = 0; l < int_0; l++)
			{
				float float_2 = num2 * (float)(l + 1);
				float float_3 = num2 * (float)(l + 1);
				float float_4 = num2 * (float)(l - 1);
				this.float_5[l] = CalculateNormalisedPercentage(float_2);
				Vector3 pathPosition3 = GetPathPosition(float_2, true);
				Vector3 pathPosition4 = GetPathPosition(float_3, true);
				Vector3 pathPosition5 = GetPathPosition(float_4, true);
				vector3_1[l] = ((pathPosition4 - pathPosition3 + (pathPosition4 - pathPosition5)) * 0.5f).normalized;
			}
			for (int m = 0; m < int_0; m++)
			{
				float float_5 = num2 * (float)m;
				Vector3 pathPosition6 = GetPathPosition(float_5);
				vector3_0[m] = pathPosition6;
			}
			if (recalculateCurvesHandler_0 != null)
			{
				recalculateCurvesHandler_0();
			}
		}
	}

	public float GetNearestPoint(Vector3 vector3_2)
	{
		return GetNearestPoint(vector3_2, false, 4);
	}

	public float GetNearestPoint(Vector3 vector3_2, bool bool_5)
	{
		return GetNearestPoint(vector3_2, bool_5, 4);
	}

	public float GetNearestPoint(Vector3 vector3_2, bool bool_5, int int_1)
	{
		int num = 10;
		float num2 = 0.1f;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = float.PositiveInfinity;
		float num6 = float.PositiveInfinity;
		for (float num7 = 0f; num7 < 1f; num7 += num2)
		{
			Vector3 pathPosition = GetPathPosition(num7, bool_5);
			Vector3 a = pathPosition - vector3_2;
			float num8 = Vector3.SqrMagnitude(a);
			if (num5 > num8)
			{
				num3 = num7;
				num5 = num8;
			}
		}
		num4 = num3;
		num6 = num5;
		for (int i = 0; i < int_1; i++)
		{
			float num9 = num2 / 1.8f;
			float num10 = num3 - num9;
			float num11 = num3 + num9;
			float num12 = num2 / (float)num;
			for (float num13 = num10; num13 < num11; num13 += num12)
			{
				float num14 = num13 % 1f;
				if (num14 < 0f)
				{
					num14 += 1f;
				}
				Vector3 pathPosition2 = GetPathPosition(num14, bool_5);
				Vector3 a2 = pathPosition2 - vector3_2;
				float num15 = Vector3.SqrMagnitude(a2);
				if (num5 > num15)
				{
					num4 = num3;
					num6 = num5;
					num3 = num14;
					num5 = num15;
				}
				else if (num6 > num15)
				{
					num4 = num14;
					num6 = num15;
				}
			}
			num2 = num12;
		}
		float t = num5 / (num5 + num6);
		return Mathf.Clamp01(Mathf.Lerp(num3, num4, t));
	}

	public void Clear()
	{
		list_0.Clear();
	}

	public CameraPathControlPoint GetPoint(int int_1)
	{
		return this[GetPointIndex(int_1)];
	}

	public int GetPointIndex(int int_1)
	{
		if (list_0.Count == 0)
		{
			return -1;
		}
		if (!bool_2)
		{
			return Mathf.Clamp(int_1, 0, Int32_2);
		}
		if (int_1 >= Int32_2)
		{
			int_1 -= Int32_2;
		}
		if (int_1 < 0)
		{
			int_1 += Int32_2;
		}
		return int_1;
	}

	public int GetCurveIndex(int int_1)
	{
		if (list_0.Count == 0)
		{
			return -1;
		}
		if (!bool_2)
		{
			return Mathf.Clamp(int_1, 0, Int32_2 - 1);
		}
		if (int_1 >= Int32_2 - 1)
		{
			int_1 = int_1 - Int32_2 - 1;
		}
		if (int_1 < 0)
		{
			int_1 = int_1 + Int32_2 - 1;
		}
		return int_1;
	}

	private void Init()
	{
		InitialiseLists();
		if (!bool_0)
		{
			CameraPathControlPoint cameraPathControlPoint = base.gameObject.AddComponent<CameraPathControlPoint>();
			CameraPathControlPoint cameraPathControlPoint2 = base.gameObject.AddComponent<CameraPathControlPoint>();
			CameraPathControlPoint cameraPathControlPoint3 = base.gameObject.AddComponent<CameraPathControlPoint>();
			CameraPathControlPoint cameraPathControlPoint4 = base.gameObject.AddComponent<CameraPathControlPoint>();
			cameraPathControlPoint.hideFlags = HideFlags.HideInInspector;
			cameraPathControlPoint2.hideFlags = HideFlags.HideInInspector;
			cameraPathControlPoint3.hideFlags = HideFlags.HideInInspector;
			cameraPathControlPoint4.hideFlags = HideFlags.HideInInspector;
			cameraPathControlPoint.Vector3_0 = new Vector3(-20f, 0f, -20f);
			cameraPathControlPoint2.Vector3_0 = new Vector3(20f, 0f, -20f);
			cameraPathControlPoint3.Vector3_0 = new Vector3(20f, 0f, 20f);
			cameraPathControlPoint4.Vector3_0 = new Vector3(-20f, 0f, 20f);
			cameraPathControlPoint.Vector3_3 = new Vector3(0f, 0f, -20f);
			cameraPathControlPoint2.Vector3_3 = new Vector3(40f, 0f, -20f);
			cameraPathControlPoint3.Vector3_3 = new Vector3(0f, 0f, 20f);
			cameraPathControlPoint4.Vector3_3 = new Vector3(-40f, 0f, 20f);
			AddPoint(cameraPathControlPoint);
			AddPoint(cameraPathControlPoint2);
			AddPoint(cameraPathControlPoint3);
			AddPoint(cameraPathControlPoint4);
			bool_0 = true;
		}
	}

	private void InitialiseLists()
	{
		if (cameraPathOrientationList_0 == null)
		{
			cameraPathOrientationList_0 = base.gameObject.AddComponent<CameraPathOrientationList>();
		}
		if (cameraPathFOVList_0 == null)
		{
			cameraPathFOVList_0 = base.gameObject.AddComponent<CameraPathFOVList>();
		}
		if (cameraPathTiltList_0 == null)
		{
			cameraPathTiltList_0 = base.gameObject.AddComponent<CameraPathTiltList>();
		}
		if (cameraPathSpeedList_0 == null)
		{
			cameraPathSpeedList_0 = base.gameObject.AddComponent<CameraPathSpeedList>();
		}
		if (cameraPathEventList_0 == null)
		{
			cameraPathEventList_0 = base.gameObject.AddComponent<CameraPathEventList>();
		}
		if (cameraPathDelayList_0 == null)
		{
			cameraPathDelayList_0 = base.gameObject.AddComponent<CameraPathDelayList>();
		}
		cameraPathOrientationList_0.Init(this);
		cameraPathFOVList_0.Init(this);
		cameraPathTiltList_0.Init(this);
		cameraPathSpeedList_0.Init(this);
		cameraPathEventList_0.Init(this);
		cameraPathDelayList_0.Init(this);
	}
}
