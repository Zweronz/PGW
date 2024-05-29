using System;
using System.Collections;
using UnityEngine;

public class Water : MonoBehaviour
{
	public enum WaterMode
	{
		Simple = 0,
		Reflective = 1,
		Refractive = 2
	}

	public WaterMode m_WaterMode = WaterMode.Refractive;

	public bool m_DisablePixelLights = true;

	public int m_TextureSize = 256;

	public float m_ClipPlaneOffset = 0.07f;

	public LayerMask m_ReflectLayers = -1;

	public LayerMask m_RefractLayers = -1;

	private Hashtable hashtable_0 = new Hashtable();

	private Hashtable hashtable_1 = new Hashtable();

	private RenderTexture renderTexture_0;

	private RenderTexture renderTexture_1;

	private WaterMode waterMode_0 = WaterMode.Refractive;

	private int int_0;

	private int int_1;

	private static bool bool_0;

	public void OnWillRenderObject()
	{
		if (!base.enabled || !base.GetComponent<Renderer>() || !base.GetComponent<Renderer>().sharedMaterial || !base.GetComponent<Renderer>().enabled)
		{
			return;
		}
		Camera current = Camera.current;
		if ((bool)current && !bool_0)
		{
			bool_0 = true;
			waterMode_0 = FindHardwareWaterSupport();
			WaterMode waterMode = GetWaterMode();
			Camera camera_;
			Camera camera_2;
			CreateWaterObjects(current, out camera_, out camera_2);
			Vector3 position = base.transform.position;
			Vector3 up = base.transform.up;
			int pixelLightCount = QualitySettings.pixelLightCount;
			if (m_DisablePixelLights)
			{
				QualitySettings.pixelLightCount = 0;
			}
			UpdateCameraModes(current, camera_);
			UpdateCameraModes(current, camera_2);
			if (waterMode >= WaterMode.Reflective)
			{
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
				camera_.transform.eulerAngles = new Vector3(0f - eulerAngles.x, eulerAngles.y, eulerAngles.z);
				camera_.Render();
				camera_.transform.position = position2;
				GL.SetRevertBackfacing(false);
				base.GetComponent<Renderer>().sharedMaterial.SetTexture("_ReflectionTex", renderTexture_0);
			}
			if (waterMode >= WaterMode.Refractive)
			{
				camera_2.worldToCameraMatrix = current.worldToCameraMatrix;
				Vector4 vector4_3 = CameraSpacePlane(camera_2, position, up, -1f);
				Matrix4x4 matrix4x4_3 = current.projectionMatrix;
				CalculateObliqueMatrix(ref matrix4x4_3, vector4_3);
				camera_2.projectionMatrix = matrix4x4_3;
				camera_2.cullingMask = -17 & m_RefractLayers.value;
				camera_2.targetTexture = renderTexture_1;
				camera_2.transform.position = current.transform.position;
				camera_2.transform.rotation = current.transform.rotation;
				camera_2.Render();
				base.GetComponent<Renderer>().sharedMaterial.SetTexture("_RefractionTex", renderTexture_1);
			}
			if (m_DisablePixelLights)
			{
				QualitySettings.pixelLightCount = pixelLightCount;
			}
			switch (waterMode)
			{
			case WaterMode.Simple:
				Shader.EnableKeyword("WATER_SIMPLE");
				Shader.DisableKeyword("WATER_REFLECTIVE");
				Shader.DisableKeyword("WATER_REFRACTIVE");
				break;
			case WaterMode.Reflective:
				Shader.DisableKeyword("WATER_SIMPLE");
				Shader.EnableKeyword("WATER_REFLECTIVE");
				Shader.DisableKeyword("WATER_REFRACTIVE");
				break;
			case WaterMode.Refractive:
				Shader.DisableKeyword("WATER_SIMPLE");
				Shader.DisableKeyword("WATER_REFLECTIVE");
				Shader.EnableKeyword("WATER_REFRACTIVE");
				break;
			}
			bool_0 = false;
		}
	}

	private void OnDisable()
	{
		if ((bool)renderTexture_0)
		{
			UnityEngine.Object.DestroyImmediate(renderTexture_0);
			renderTexture_0 = null;
		}
		if ((bool)renderTexture_1)
		{
			UnityEngine.Object.DestroyImmediate(renderTexture_1);
			renderTexture_1 = null;
		}
		foreach (DictionaryEntry item in hashtable_0)
		{
			UnityEngine.Object.DestroyImmediate(((Camera)item.Value).gameObject);
		}
		hashtable_0.Clear();
		foreach (DictionaryEntry item2 in hashtable_1)
		{
			UnityEngine.Object.DestroyImmediate(((Camera)item2.Value).gameObject);
		}
		hashtable_1.Clear();
	}

	private void Update()
	{
		if ((bool)base.GetComponent<Renderer>())
		{
			Material sharedMaterial = base.GetComponent<Renderer>().sharedMaterial;
			if ((bool)sharedMaterial)
			{
				Vector4 vector = sharedMaterial.GetVector("WaveSpeed");
				float @float = sharedMaterial.GetFloat("_WaveScale");
				Vector4 vector2 = new Vector4(@float, @float, @float * 0.4f, @float * 0.45f);
				double num = (double)Time.timeSinceLevelLoad / 20.0;
				Vector4 vector3 = new Vector4((float)Math.IEEERemainder((double)(vector.x * vector2.x) * num, 1.0), (float)Math.IEEERemainder((double)(vector.y * vector2.y) * num, 1.0), (float)Math.IEEERemainder((double)(vector.z * vector2.z) * num, 1.0), (float)Math.IEEERemainder((double)(vector.w * vector2.w) * num, 1.0));
				sharedMaterial.SetVector("_WaveOffset", vector3);
				sharedMaterial.SetVector("_WaveScale4", vector2);
				Vector3 size = base.GetComponent<Renderer>().bounds.size;
				Matrix4x4 matrix = Matrix4x4.TRS(s: new Vector3(size.x * vector2.x, size.z * vector2.y, 1f), pos: new Vector3(vector3.x, vector3.y, 0f), q: Quaternion.identity);
				sharedMaterial.SetMatrix("_WaveMatrix", matrix);
				matrix = Matrix4x4.TRS(s: new Vector3(size.x * vector2.z, size.z * vector2.w, 1f), pos: new Vector3(vector3.z, vector3.w, 0f), q: Quaternion.identity);
				sharedMaterial.SetMatrix("_WaveMatrix2", matrix);
			}
		}
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

	private void CreateWaterObjects(Camera camera_0, out Camera camera_1, out Camera camera_2)
	{
		WaterMode waterMode = GetWaterMode();
		camera_1 = null;
		camera_2 = null;
		if (waterMode >= WaterMode.Reflective)
		{
			if (!renderTexture_0 || int_0 != m_TextureSize)
			{
				if ((bool)renderTexture_0)
				{
					UnityEngine.Object.DestroyImmediate(renderTexture_0);
				}
				renderTexture_0 = new RenderTexture(m_TextureSize, m_TextureSize, 16);
				renderTexture_0.name = "__WaterReflection" + GetInstanceID();
				renderTexture_0.isPowerOfTwo = true;
				renderTexture_0.hideFlags = HideFlags.DontSave;
				int_0 = m_TextureSize;
			}
			camera_1 = hashtable_0[camera_0] as Camera;
			if (!camera_1)
			{
				GameObject gameObject = new GameObject("Water Refl Camera id" + GetInstanceID() + " for " + camera_0.GetInstanceID(), typeof(Camera), typeof(Skybox));
				camera_1 = gameObject.GetComponent<Camera>();
				camera_1.enabled = false;
				camera_1.transform.position = base.transform.position;
				camera_1.transform.rotation = base.transform.rotation;
				camera_1.gameObject.AddComponent<FlareLayer>();
				gameObject.hideFlags = HideFlags.HideAndDontSave;
				hashtable_0[camera_0] = camera_1;
			}
		}
		if (waterMode < WaterMode.Refractive)
		{
			return;
		}
		if (!renderTexture_1 || int_1 != m_TextureSize)
		{
			if ((bool)renderTexture_1)
			{
				UnityEngine.Object.DestroyImmediate(renderTexture_1);
			}
			renderTexture_1 = new RenderTexture(m_TextureSize, m_TextureSize, 16);
			renderTexture_1.name = "__WaterRefraction" + GetInstanceID();
			renderTexture_1.isPowerOfTwo = true;
			renderTexture_1.hideFlags = HideFlags.DontSave;
			int_1 = m_TextureSize;
		}
		camera_2 = hashtable_1[camera_0] as Camera;
		if (!camera_2)
		{
			GameObject gameObject2 = new GameObject("Water Refr Camera id" + GetInstanceID() + " for " + camera_0.GetInstanceID(), typeof(Camera), typeof(Skybox));
			camera_2 = gameObject2.GetComponent<Camera>();
			camera_2.enabled = false;
			camera_2.transform.position = base.transform.position;
			camera_2.transform.rotation = base.transform.rotation;
			camera_2.gameObject.AddComponent<FlareLayer>();
			gameObject2.hideFlags = HideFlags.HideAndDontSave;
			hashtable_1[camera_0] = camera_2;
		}
	}

	private WaterMode GetWaterMode()
	{
		if (waterMode_0 < m_WaterMode)
		{
			return waterMode_0;
		}
		return m_WaterMode;
	}

	private WaterMode FindHardwareWaterSupport()
	{
		if (SystemInfo.supportsRenderTextures && (bool)base.GetComponent<Renderer>())
		{
			Material sharedMaterial = base.GetComponent<Renderer>().sharedMaterial;
			if (!sharedMaterial)
			{
				return WaterMode.Simple;
			}
			string text = sharedMaterial.GetTag("WATERMODE", false);
			if (text == "Refractive")
			{
				return WaterMode.Refractive;
			}
			if (text == "Reflective")
			{
				return WaterMode.Reflective;
			}
			return WaterMode.Simple;
		}
		return WaterMode.Simple;
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
