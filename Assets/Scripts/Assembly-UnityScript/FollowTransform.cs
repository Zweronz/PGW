using System;
using UnityEngine;

[Serializable]
public class FollowTransform : MonoBehaviour
{
	public Transform targetTransform;

	public bool faceForward;

	private Transform transform_0;

	public virtual void Start()
	{
		transform_0 = transform;
	}

	public virtual void Update()
	{
		transform_0.position = targetTransform.position;
		if (faceForward)
		{
			transform_0.forward = targetTransform.forward;
		}
	}

	public virtual void Main()
	{
	}
}
