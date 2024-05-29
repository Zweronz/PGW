using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class DragRigidbody : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	internal sealed class _0024DragObject_002463 : GenericGenerator<object>
	{
		[Serializable]
		[CompilerGenerated]
		internal sealed class _0024 : GenericGeneratorEnumerator<object>, IEnumerator
		{
			internal float float_0;

			internal float float_1;

			internal Camera camera_0;

			internal Ray ray_0;

			internal float float_2;

			internal DragRigidbody dragRigidbody_0;

			public _0024(float float_3, DragRigidbody dragRigidbody_1)
			{
				float_2 = float_3;
				dragRigidbody_0 = dragRigidbody_1;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					float_0 = dragRigidbody_0.springJoint_0.connectedBody.drag;
					float_1 = dragRigidbody_0.springJoint_0.connectedBody.angularDrag;
					dragRigidbody_0.springJoint_0.connectedBody.drag = dragRigidbody_0.drag;
					dragRigidbody_0.springJoint_0.connectedBody.angularDrag = dragRigidbody_0.angularDrag;
					camera_0 = dragRigidbody_0.FindCamera();
					goto case 2;
				case 2:
					if (!Input.GetMouseButton(0))
					{
						if ((bool)dragRigidbody_0.springJoint_0.connectedBody)
						{
							dragRigidbody_0.springJoint_0.connectedBody.drag = float_0;
							dragRigidbody_0.springJoint_0.connectedBody.angularDrag = float_1;
							dragRigidbody_0.springJoint_0.connectedBody = null;
						}
						YieldDefault(1);
						goto case 1;
					}
					ray_0 = camera_0.ScreenPointToRay(Input.mousePosition);
					dragRigidbody_0.springJoint_0.transform.position = ray_0.GetPoint(float_2);
					result = (YieldDefault(2) ? 1 : 0);
					break;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal float float_0;

		internal DragRigidbody dragRigidbody_0;

		public _0024DragObject_002463(float float_1, DragRigidbody dragRigidbody_1)
		{
			float_0 = float_1;
			dragRigidbody_0 = dragRigidbody_1;
		}

		public override IEnumerator<object> GetEnumerator()
		{
			return new _0024(float_0, dragRigidbody_0);
		}
	}

	public float spring;

	public float damper;

	public float drag;

	public float angularDrag;

	public float distance;

	public bool attachToCenterOfMass;

	private SpringJoint springJoint_0;

	public DragRigidbody()
	{
		spring = 50f;
		damper = 5f;
		drag = 10f;
		angularDrag = 5f;
		distance = 0.2f;
	}

	public virtual void Update()
	{
		if (!Input.GetMouseButtonDown(0))
		{
			return;
		}
		Camera camera = FindCamera();
		RaycastHit hitInfo = default(RaycastHit);
		if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hitInfo, 100f) && (bool)hitInfo.rigidbody && !hitInfo.rigidbody.isKinematic)
		{
			if (!springJoint_0)
			{
				GameObject gameObject = new GameObject("Rigidbody dragger");
				Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>() as Rigidbody;
				springJoint_0 = (SpringJoint)gameObject.AddComponent<SpringJoint>();
				rigidbody.isKinematic = true;
			}
			springJoint_0.transform.position = hitInfo.point;
			if (attachToCenterOfMass)
			{
				Vector3 position = transform.TransformDirection(hitInfo.rigidbody.centerOfMass) + hitInfo.rigidbody.transform.position;
				position = springJoint_0.transform.InverseTransformPoint(position);
				springJoint_0.anchor = position;
			}
			else
			{
				springJoint_0.anchor = Vector3.zero;
			}
			springJoint_0.spring = spring;
			springJoint_0.damper = damper;
			springJoint_0.maxDistance = distance;
			springJoint_0.connectedBody = hitInfo.rigidbody;
			StartCoroutine("DragObject", hitInfo.distance);
		}
	}

	public virtual IEnumerator DragObject(float float_0)
	{
		return new _0024DragObject_002463(float_0, this).GetEnumerator();
	}

	public virtual Camera FindCamera()
	{
		return (!GetComponent<Camera>()) ? Camera.main : GetComponent<Camera>();
	}

	public virtual void Main()
	{
	}
}
