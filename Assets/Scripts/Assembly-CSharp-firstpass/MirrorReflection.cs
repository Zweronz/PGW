using System.Collections;
using UnityEngine;

public class MirrorReflection : MonoBehaviour
{
	public bool m_DisablePixelLights = true;

	public int m_TextureSize = 256;

	public float m_ClipPlaneOffset = 0.07f;

	public LayerMask m_ReflectLayers = -1;

	private Hashtable hashtable_0 = new Hashtable();

	private RenderTexture renderTexture_0;

	private int int_0;

	private static bool bool_0;

	public void OnWillRenderObject()
	{
		if (!base.enabled || !base.GetComponent<Renderer>() || !base.GetComponent<Renderer>().sharedMaterial || !base.GetComponent<Renderer>().enabled)
		{
			return;
		}
		Camera current = Camera.current;
		if (!current || bool_0)
		{
			return;
		}
		bool_0 = true;
		Camera camera_;
		CreateMirrorObjects(current, out camera_);
		Vector3 position = base.transform.position;
		Vector3 up = base.transform.up;
		int pixelLightCount = QualitySettings.pixelLightCount;
		if (m_DisablePixelLights)
		{
			QualitySettings.pixelLightCount = 0;
		}
		UpdateCameraModes(current, camera_);
		float w = 0f - Vector3.Dot(up, position) - m_ClipPlaneOffset;
		Vector4 vector4_ = new Vector4(up.x, up.y, up.z, w);
		Matrix4x4 matrix4x4_ = Matrix4x4.zero;
		CalculateReflectionMatrix(ref matrix4x4_, vector4_);
		Vector3 position2 = current.transform.position;
		Vector3 position3 = matrix4x4_.MultiplyPoint(position2);
		camera_.worldToCameraMatrix = current.worldToCameraMatrix * matrix4x4_;
		Vector4 vector4_2 = CameraSpacePlane(camera_, position, up, 1f);
		Matrix4x4 matrix4x4_2 = current.projectionMatrix;
		CalculateObliqueMatrix(ref matrix4x4_2, vector4_2);
		camera_.projectionMatrix = matrix4x4_2;
		camera_.cullingMask = -17 & m_ReflectLayers.value;
		camera_.targetTexture = renderTexture_0;
		GL.SetRevertBackfacing(true);
		camera_.transform.position = position3;
		Vector3 eulerAngles = current.transform.eulerAngles;
		camera_.transform.eulerAngles = new Vector3(0f, eulerAngles.y, eulerAngles.z);
		camera_.Render();
		camera_.transform.position = position2;
		GL.SetRevertBackfacing(false);
		Material[] sharedMaterials = base.GetComponent<Renderer>().sharedMaterials;
		Material[] array = sharedMaterials;
		foreach (Material material in array)
		{
			if (material.HasProperty("_ReflectionTex"))
			{
				material.SetTexture("_ReflectionTex", renderTexture_0);
			}
		}
		Matrix4x4 matrix4x = Matrix4x4.TRS(new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity, new Vector3(0.5f, 0.5f, 0.5f));
		Vector3 lossyScale = base.transform.lossyScale;
		Matrix4x4 matrix4x2 = base.transform.localToWorldMatrix * Matrix4x4.Scale(new Vector3(1f / lossyScale.x, 1f / lossyScale.y, 1f / lossyScale.z));
		matrix4x2 = matrix4x * current.projectionMatrix * current.worldToCameraMatrix * matrix4x2;
		Material[] array2 = sharedMaterials;
		foreach (Material material2 in array2)
		{
			material2.SetMatrix("_ProjMatrix", matrix4x2);
		}
		if (m_DisablePixelLights)
		{
			QualitySettings.pixelLightCount = pixelLightCount;
		}
		bool_0 = false;
	}

	private void OnDisable()
	{
		if ((bool)renderTexture_0)
		{
			Object.DestroyImmediate(renderTexture_0);
			renderTexture_0 = null;
		}
		foreach (DictionaryEntry item in hashtable_0)
		{
			Object.DestroyImmediate(((Camera)item.Value).gameObject);
		}
		hashtable_0.Clear();
	}

	private void UpdateCameraModes(Camera camera_0, Camera camera_1)
	{
		if (camera_1 == null)
		{
			return;
		}
		camera_1.clearFlags = camera_0.clearFlags;
		camera_1.backgroundColor = camera_0.backgroundColor;
		if (camera_0.clearFlags == CameraClearFlags.Skybox)
		{
			Skybox skybox = camera_0.GetComponent(typeof(Skybox)) as Skybox;
			Skybox skybox2 = camera_1.GetComponent(typeof(Skybox)) as Skybox;
			if ((bool)skybox && (bool)skybox.material)
			{
				skybox2.enabled = true;
				skybox2.material = skybox.material;
			}
			else
			{
				skybox2.enabled = false;
			}
		}
		camera_1.farClipPlane = camera_0.farClipPlane;
		camera_1.nearClipPlane = camera_0.nearClipPlane;
		camera_1.orthographic = camera_0.orthographic;
		camera_1.fieldOfView = camera_0.fieldOfView;
		camera_1.aspect = camera_0.aspect;
		camera_1.orthographicSize = camera_0.orthographicSize;
	}

	private void CreateMirrorObjects(Camera camera_0, out Camera camera_1)
	{
		camera_1 = null;
		if (!renderTexture_0 || int_0 != m_TextureSize)
		{
			if ((bool)renderTexture_0)
			{
				Object.DestroyImmediate(renderTexture_0);
			}
			renderTexture_0 = new RenderTexture(m_TextureSize, m_TextureSize, 16);
			renderTexture_0.name = "__MirrorReflection" + GetInstanceID();
			renderTexture_0.isPowerOfTwo = true;
			renderTexture_0.hideFlags = HideFlags.DontSave;
			int_0 = m_TextureSize;
		}
		camera_1 = hashtable_0[camera_0] as Camera;
		if (!camera_1)
		{
			GameObject gameObject = new GameObject("Mirror Refl Camera id" + GetInstanceID() + " for " + camera_0.GetInstanceID(), typeof(Camera), typeof(Skybox));
			camera_1 = gameObject.GetComponent<Camera>();
			camera_1.enabled = false;
			camera_1.transform.position = base.transform.position;
			camera_1.transform.rotation = base.transform.rotation;
			camera_1.gameObject.AddComponent<FlareLayer>();
			gameObject.hideFlags = HideFlags.HideAndDontSave;
			hashtable_0[camera_0] = camera_1;
		}
	}

	private static float sgn(float float_0)
	{
		if (float_0 > 0f)
		{
			return 1f;
		}
		if (float_0 < 0f)
		{
			return -1f;
		}
		return 0f;
	}

	private Vector4 CameraSpacePlane(Camera camera_0, Vector3 vector3_0, Vector3 vector3_1, float float_0)
	{
		Vector3 v = vector3_0 + vector3_1 * m_ClipPlaneOffset;
		Matrix4x4 worldToCameraMatrix = camera_0.worldToCameraMatrix;
		Vector3 lhs = worldToCameraMatrix.MultiplyPoint(v);
		Vector3 rhs = worldToCameraMatrix.MultiplyVector(vector3_1).normalized * float_0;
		return new Vector4(rhs.x, rhs.y, rhs.z, 0f - Vector3.Dot(lhs, rhs));
	}

	private static void CalculateObliqueMatrix(ref Matrix4x4 matrix4x4_0, Vector4 vector4_0)
	{
		Vector4 b = matrix4x4_0.inverse * new Vector4(sgn(vector4_0.x), sgn(vector4_0.y), 1f, 1f);
		Vector4 vector = vector4_0 * (2f / Vector4.Dot(vector4_0, b));
		matrix4x4_0[2] = vector.x - matrix4x4_0[3];
		matrix4x4_0[6] = vector.y - matrix4x4_0[7];
		matrix4x4_0[10] = vector.z - matrix4x4_0[11];
		matrix4x4_0[14] = vector.w - matrix4x4_0[15];
	}

	private static void CalculateReflectionMatrix(ref Matrix4x4 matrix4x4_0, Vector4 vector4_0)
	{
		matrix4x4_0.m00 = 1f - 2f * vector4_0[0] * vector4_0[0];
		matrix4x4_0.m01 = -2f * vector4_0[0] * vector4_0[1];
		matrix4x4_0.m02 = -2f * vector4_0[0] * vector4_0[2];
		matrix4x4_0.m03 = -2f * vector4_0[3] * vector4_0[0];
		matrix4x4_0.m10 = -2f * vector4_0[1] * vector4_0[0];
		matrix4x4_0.m11 = 1f - 2f * vector4_0[1] * vector4_0[1];
		matrix4x4_0.m12 = -2f * vector4_0[1] * vector4_0[2];
		matrix4x4_0.m13 = -2f * vector4_0[3] * vector4_0[1];
		matrix4x4_0.m20 = -2f * vector4_0[2] * vector4_0[0];
		matrix4x4_0.m21 = -2f * vector4_0[2] * vector4_0[1];
		matrix4x4_0.m22 = 1f - 2f * vector4_0[2] * vector4_0[2];
		matrix4x4_0.m23 = -2f * vector4_0[3] * vector4_0[2];
		matrix4x4_0.m30 = 0f;
		matrix4x4_0.m31 = 0f;
		matrix4x4_0.m32 = 0f;
		matrix4x4_0.m33 = 1f;
	}
}
