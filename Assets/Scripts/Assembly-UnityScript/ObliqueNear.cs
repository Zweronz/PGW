using System;
using UnityEngine;

[Serializable]
public class ObliqueNear : MonoBehaviour
{
	public Transform plane;

	public virtual Matrix4x4 CalculateObliqueMatrix(Matrix4x4 matrix4x4_0, Vector4 vector4_0)
	{
		Vector4 b = matrix4x4_0.inverse * new Vector4(Mathf.Sign(vector4_0.x), Mathf.Sign(vector4_0.y), 1f, 1f);
		Vector4 vector = vector4_0 * (2f / Vector4.Dot(vector4_0, b));
		matrix4x4_0[2] = vector.x - matrix4x4_0[3];
		matrix4x4_0[6] = vector.y - matrix4x4_0[7];
		matrix4x4_0[10] = vector.z - matrix4x4_0[11];
		matrix4x4_0[14] = vector.w - matrix4x4_0[15];
		return matrix4x4_0;
	}

	public virtual void OnPreCull()
	{
		Matrix4x4 projectionMatrix = GetComponent<Camera>().projectionMatrix;
		Matrix4x4 worldToCameraMatrix = GetComponent<Camera>().worldToCameraMatrix;
		Vector3 rhs = worldToCameraMatrix.MultiplyPoint(plane.position);
		Vector3 vector = worldToCameraMatrix.MultiplyVector(-Vector3.up);
		vector.Normalize();
		Vector4 vector4_ = vector;
		vector4_.w = 0f - Vector3.Dot(vector, rhs);
		GetComponent<Camera>().projectionMatrix = CalculateObliqueMatrix(projectionMatrix, vector4_);
	}

	public virtual void Main()
	{
	}
}
