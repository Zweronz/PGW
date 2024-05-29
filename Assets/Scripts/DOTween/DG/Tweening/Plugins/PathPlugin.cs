using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.Plugins
{
	public class PathPlugin : ABSTweenPlugin<Vector3, Path, PathOptions>
	{
		public const float MinLookAhead = 0.0001f;

		public override void Reset(TweenerCore<Vector3, Path, PathOptions> t)
		{
			t.endValue.Destroy();
			t.startValue = (t.endValue = (t.changeValue = null));
		}

		public override void SetFrom(TweenerCore<Vector3, Path, PathOptions> t, bool isRelative)
		{
		}

		public static ABSTweenPlugin<Vector3, Path, PathOptions> Get()
		{
			return PluginsManager.GetCustomPlugin<PathPlugin, Vector3, Path, PathOptions>();
		}

		public override Path ConvertToStartValue(TweenerCore<Vector3, Path, PathOptions> t, Vector3 value)
		{
			return t.endValue;
		}

		public override void SetRelativeEndValue(TweenerCore<Vector3, Path, PathOptions> t)
		{
			if (!t.endValue.isFinalized)
			{
				Vector3 vector = t.getter();
				int num = t.endValue.wps.Length;
				for (int i = 0; i < num; i++)
				{
					t.endValue.wps[i] += vector;
				}
			}
		}

		public override void SetChangeValue(TweenerCore<Vector3, Path, PathOptions> t)
		{
			if (t.endValue.isFinalized)
			{
				t.changeValue = t.endValue;
				return;
			}
			Vector3 vector = t.getter();
			Path endValue = t.endValue;
			int num = endValue.wps.Length;
			int num2 = 0;
			bool flag = false;
			bool flag2 = false;
			if (endValue.wps[0] != vector)
			{
				flag = true;
				num2++;
			}
			if (t.plugOptions.isClosedPath && endValue.wps[num - 1] != vector)
			{
				flag2 = true;
				num2++;
			}
			int num3 = num + num2;
			Vector3[] array = new Vector3[num3];
			int num4 = (flag ? 1 : 0);
			if (flag)
			{
				array[0] = vector;
			}
			for (int i = 0; i < num; i++)
			{
				array[i + num4] = endValue.wps[i];
			}
			if (flag2)
			{
				array[array.Length - 1] = array[0];
			}
			endValue.wps = array;
			endValue.FinalizePath(t.plugOptions.isClosedPath, t.plugOptions.lockPositionAxis, vector);
			Transform transform = (Transform)t.target;
			t.plugOptions.startupZRot = transform.eulerAngles.z;
			if (t.plugOptions.orientType == OrientType.ToPath && t.plugOptions.useLocalPosition)
			{
				t.plugOptions.parent = transform.parent;
			}
			t.changeValue = t.endValue;
		}

		public override float GetSpeedBasedDuration(PathOptions options, float unitsXSecond, Path changeValue)
		{
			return changeValue.length / unitsXSecond;
		}

		public override void EvaluateAndApply(PathOptions options, Tween t, bool isRelative, DOGetter<Vector3> getter, DOSetter<Vector3> setter, float elapsed, Path startValue, Path changeValue, float duration, bool usingInversePosition)
		{
			float perc = EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			float num = changeValue.ConvertToConstantPathPerc(perc);
			Vector3 vector = (changeValue.targetPosition = changeValue.GetPoint(num));
			setter(vector);
			if (options.mode != 0 && options.orientType != 0)
			{
				SetOrientation(options, t, changeValue, num, vector);
			}
			bool flag = !usingInversePosition;
			if (t.isBackwards)
			{
				flag = !flag;
			}
			int waypointIndexFromPerc = changeValue.GetWaypointIndexFromPerc(perc, flag);
			if (waypointIndexFromPerc != t.miscInt)
			{
				t.miscInt = waypointIndexFromPerc;
				if (t.onWaypointChange != null)
				{
					Tween.OnTweenCallback(t.onWaypointChange, waypointIndexFromPerc);
				}
			}
		}

		public void SetOrientation(PathOptions options, Tween t, Path path, float pathPerc, Vector3 tPos)
		{
			Transform transform = (Transform)t.target;
			Quaternion rotation = Quaternion.identity;
			switch (options.orientType)
			{
			case OrientType.ToPath:
			{
				Vector3 vector;
				if (path.type == PathType.Linear && options.lookAhead <= 0.0001f)
				{
					vector = tPos + path.wps[path.linearWPIndex] - path.wps[path.linearWPIndex - 1];
				}
				else
				{
					float num = pathPerc + options.lookAhead;
					if (num > 1f)
					{
						num = (options.isClosedPath ? (num - 1f) : 1.00001f);
					}
					vector = path.GetPoint(num);
				}
				Vector3 upwards = transform.up;
				if (options.useLocalPosition && options.parent != null)
				{
					vector = options.parent.TransformPoint(vector);
				}
				if (options.lockRotationAxis != 0)
				{
					if ((options.lockRotationAxis & AxisConstraint.X) == AxisConstraint.X)
					{
						Vector3 position = transform.InverseTransformPoint(vector);
						position.y = 0f;
						vector = transform.TransformPoint(position);
						upwards = ((!options.useLocalPosition || !(options.parent != null)) ? Vector3.up : options.parent.up);
					}
					if ((options.lockRotationAxis & AxisConstraint.Y) == AxisConstraint.Y)
					{
						Vector3 position2 = transform.InverseTransformPoint(vector);
						if (position2.z < 0f)
						{
							position2.z = 0f - position2.z;
						}
						position2.x = 0f;
						vector = transform.TransformPoint(position2);
					}
					if ((options.lockRotationAxis & AxisConstraint.Z) == AxisConstraint.Z)
					{
						upwards = ((!options.useLocalPosition || !(options.parent != null)) ? transform.TransformDirection(Vector3.up) : options.parent.TransformDirection(Vector3.up));
						upwards.z = options.startupZRot;
					}
				}
				if (options.mode == PathMode.Full3D)
				{
					Vector3 vector2 = vector - transform.position;
					if (vector2 == Vector3.zero)
					{
						vector2 = transform.forward;
					}
					rotation = Quaternion.LookRotation(vector2, upwards);
					break;
				}
				float y = 0f;
				float num2 = Utils.Angle2D(transform.position, vector);
				if (num2 < 0f)
				{
					num2 = 360f + num2;
				}
				if (options.mode == PathMode.Sidescroller2D)
				{
					y = ((vector.x < transform.position.x) ? 180 : 0);
					if (num2 > 90f && num2 < 270f)
					{
						num2 = 180f - num2;
					}
				}
				rotation = Quaternion.Euler(0f, y, num2);
				break;
			}
			case OrientType.LookAtTransform:
				if (options.lookAtTransform != null)
				{
					path.lookAtPosition = options.lookAtTransform.position;
					rotation = Quaternion.LookRotation(options.lookAtTransform.position - transform.position, transform.up);
				}
				break;
			case OrientType.LookAtPosition:
				path.lookAtPosition = options.lookAtPosition;
				rotation = Quaternion.LookRotation(options.lookAtPosition - transform.position, transform.up);
				break;
			}
			if (options.hasCustomForwardDirection)
			{
				rotation *= options.forward;
			}
			transform.rotation = rotation;
		}
	}
}
