using System;
using UnityEngine;

namespace DG.Tweening.Plugins.Core.PathCore
{
	[Serializable]
	public class Path
	{
		private static CatmullRomDecoder _catmullRomDecoder;

		private static LinearDecoder _linearDecoder;

		[SerializeField]
		internal PathType type;

		[SerializeField]
		internal int subdivisionsXSegment;

		[SerializeField]
		internal int subdivisions;

		[SerializeField]
		internal Vector3[] wps;

		[SerializeField]
		internal ControlPoint[] controlPoints;

		[SerializeField]
		internal float length;

		[SerializeField]
		internal float[] wpLengths;

		[SerializeField]
		internal bool isFinalized;

		[SerializeField]
		internal float[] timesTable;

		[SerializeField]
		internal float[] lengthsTable;

		internal int linearWPIndex = -1;

		private ABSPathDecoder _decoder;

		private bool _changed;

		internal Vector3[] nonLinearDrawWps;

		internal Vector3 targetPosition;

		internal Vector3? lookAtPosition;

		internal Color gizmoColor = new Color(1f, 1f, 1f, 0.7f);

		public Path(PathType type, Vector3[] waypoints, int subdivisionsXSegment, Color? gizmoColor = null)
		{
			this.type = type;
			this.subdivisionsXSegment = subdivisionsXSegment;
			if (gizmoColor.HasValue)
			{
				this.gizmoColor = gizmoColor.Value;
			}
			AssignWaypoints(waypoints, true);
			AssignDecoder(type);
			if (DOTween.isUnityEditor)
			{
				DOTween.GizmosDelegates.Add(Draw);
			}
		}

		internal void FinalizePath(bool isClosedPath, AxisConstraint lockPositionAxes, Vector3 currTargetVal)
		{
			if (lockPositionAxes != 0)
			{
				bool flag = (lockPositionAxes & AxisConstraint.X) == AxisConstraint.X;
				bool flag2 = (lockPositionAxes & AxisConstraint.Y) == AxisConstraint.Y;
				bool flag3 = (lockPositionAxes & AxisConstraint.Z) == AxisConstraint.Z;
				for (int i = 0; i < wps.Length; i++)
				{
					Vector3 vector = wps[i];
					wps[i] = new Vector3(flag ? currTargetVal.x : vector.x, flag2 ? currTargetVal.y : vector.y, flag3 ? currTargetVal.z : vector.z);
				}
			}
			_decoder.FinalizePath(this, wps, isClosedPath);
			isFinalized = true;
		}

		internal Vector3 GetPoint(float perc, bool convertToConstantPerc = false)
		{
			if (convertToConstantPerc)
			{
				perc = ConvertToConstantPathPerc(perc);
			}
			return _decoder.GetPoint(perc, wps, this, controlPoints);
		}

		internal float ConvertToConstantPathPerc(float perc)
		{
			if (type == PathType.Linear)
			{
				return perc;
			}
			if (perc > 0f && perc < 1f)
			{
				float num = length * perc;
				float num2 = 0f;
				float num3 = 0f;
				float num4 = 0f;
				float num5 = 0f;
				int num6 = lengthsTable.Length;
				for (int i = 0; i < num6; i++)
				{
					if (lengthsTable[i] <= num)
					{
						num2 = timesTable[i];
						continue;
					}
					num4 = timesTable[i];
					num5 = lengthsTable[i];
					if (i > 0)
					{
						num3 = lengthsTable[i - 1];
					}
					break;
				}
				perc = num2 + (num - num3) / (num5 - num3) * (num4 - num2);
			}
			if (perc > 1f)
			{
				perc = 1f;
			}
			else if (perc < 0f)
			{
				perc = 0f;
			}
			return perc;
		}

		internal int GetWaypointIndexFromPerc(float perc, bool isMovingForward)
		{
			if (perc >= 1f)
			{
				return wps.Length - 1;
			}
			if (perc <= 0f)
			{
				return 0;
			}
			float num = length * perc;
			float num2 = 0f;
			int num3 = 0;
			int num4 = wpLengths.Length;
			while (true)
			{
				if (num3 < num4)
				{
					num2 += wpLengths[num3];
					if (num2 >= num)
					{
						break;
					}
					num3++;
					continue;
				}
				return 0;
			}
			if (num2 > num)
			{
				if (!isMovingForward)
				{
					return num3;
				}
				return num3 - 1;
			}
			return num3;
		}

		internal static void RefreshNonLinearDrawWps(Path p)
		{
			int num = p.wps.Length;
			int num2 = num * 10;
			if (p.nonLinearDrawWps == null || p.nonLinearDrawWps.Length != num2 + 1)
			{
				p.nonLinearDrawWps = new Vector3[num2 + 1];
			}
			for (int i = 0; i <= num2; i++)
			{
				float perc = (float)i / (float)num2;
				Vector3 point = p.GetPoint(perc);
				p.nonLinearDrawWps[i] = point;
			}
		}

		internal void Destroy()
		{
			if (DOTween.isUnityEditor)
			{
				DOTween.GizmosDelegates.Remove(Draw);
			}
			wps = null;
			wpLengths = (timesTable = (lengthsTable = null));
			nonLinearDrawWps = null;
			isFinalized = false;
		}

		internal void AssignWaypoints(Vector3[] newWps, bool cloneWps = false)
		{
			if (cloneWps)
			{
				int num = newWps.Length;
				wps = new Vector3[num];
				for (int i = 0; i < num; i++)
				{
					wps[i] = newWps[i];
				}
			}
			else
			{
				wps = newWps;
			}
		}

		internal void AssignDecoder(PathType pathType)
		{
			type = pathType;
			if (pathType == PathType.Linear)
			{
				if (_linearDecoder == null)
				{
					_linearDecoder = new LinearDecoder();
				}
				_decoder = _linearDecoder;
			}
			else
			{
				if (_catmullRomDecoder == null)
				{
					_catmullRomDecoder = new CatmullRomDecoder();
				}
				_decoder = _catmullRomDecoder;
			}
		}

		internal void Draw()
		{
			Draw(this);
		}

		private static void Draw(Path p)
		{
			if (p.timesTable == null)
			{
				return;
			}
			Color color = p.gizmoColor;
			color.a *= 0.5f;
			Gizmos.color = p.gizmoColor;
			int num = p.wps.Length;
			if (p._changed || (p.type != 0 && p.nonLinearDrawWps == null))
			{
				p._changed = false;
				if (p.type != 0)
				{
					RefreshNonLinearDrawWps(p);
				}
			}
			if (p.type == PathType.Linear)
			{
				Vector3 to = p.wps[0];
				for (int i = 0; i < num; i++)
				{
					Vector3 vector = p.wps[i];
					Gizmos.DrawLine(vector, to);
					to = vector;
				}
			}
			else
			{
				Vector3 to = p.nonLinearDrawWps[0];
				int num2 = p.nonLinearDrawWps.Length;
				for (int j = 1; j < num2; j++)
				{
					Vector3 vector = p.nonLinearDrawWps[j];
					Gizmos.DrawLine(vector, to);
					to = vector;
				}
			}
			Gizmos.color = color;
			for (int k = 0; k < num; k++)
			{
				Gizmos.DrawSphere(p.wps[k], 0.075f);
			}
			if (p.lookAtPosition.HasValue)
			{
				Vector3 value = p.lookAtPosition.Value;
				Gizmos.DrawLine(p.targetPosition, value);
				Gizmos.DrawWireSphere(value, 0.075f);
			}
		}
	}
}
