using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class TasharenWater : MonoBehaviour
{
	public enum Quality
	{
		Fastest = 0,
		Low = 1,
		Medium = 2,
		High = 3,
		Uber = 4
	}

	public Quality quality = Quality.High;

	public LayerMask highReflectionMask = -1;

	public LayerMask mediumReflectionMask = -1;

	public bool keepUnderCamera = true;

	private Transform transform_0;

	private Hashtable hashtable_0 = new Hashtable();

	private RenderTexture renderTexture_0;

	private int int_0;

	private Renderer renderer_0;

	private static bool bool_0;

	public int Int32_0
	{
		get
		{
			switch (quality)
			{
			default:
				return 0;
			case Quality.Medium:
			case Quality.High:
				return 512;
			case Quality.Uber:
				return 1024;
			}
		}
	}

	public LayerMask LayerMask_0
	{
		get
		{
			switch (quality)
			{
			default:
				return 0;
			case Quality.Medium:
				return mediumReflectionMask;
			case Quality.High:
			case Quality.Uber:
				return highReflectionMask;
			}
		}
	}

	public bool Boolean_0
	{
		get
		{
			return quality > Quality.Fastest;
		}
	}

	private static float SignExt(float float_0)
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

	private static void CalculateObliqueMatrix(ref Matrix4x4 matrix4x4_0, Vector4 vector4_0)
	{
		Vector4 b = matrix4x4_0.inverse * new Vector4(SignExt(vector4_0.x), SignExt(vector4_0.y), 1f, 1f);
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

	public static Quality GetQuality()
	{
		return (Quality)Storager.GetInt("Water", 3);
	}

	public static void SetQuality(Quality quality_0)
	{
		TasharenWater[] array = Object.FindObjectsOfType(typeof(TasharenWater)) as TasharenWater[];
		if (array.Length > 0)
		{
			TasharenWater[] array2 = array;
			foreach (TasharenWater tasharenWater in array2)
			{
				tasharenWater.quality = quality_0;
			}
		}
		else
		{
			Storager.SetInt("Water", (int)quality_0);
		}
	}

	private void Awake()
	{
		transform_0 = base.transform;
		renderer_0 = base.GetComponent<Renderer>();
		quality = GetQuality();
	}

	private void OnDisable()
	{
		Clear();
		foreach (DictionaryEntry item in hashtable_0)
		{
			Object.DestroyImmediate(((Camera)item.Value).gameObject);
		}
		hashtable_0.Clear();
	}

	private void Clear()
	{
		if ((bool)renderTexture_0)
		{
			Object.DestroyImmediate(renderTexture_0);
			renderTexture_0 = null;
		}
	}

	private void CopyCamera(Camera camera_0, Camera camera_1)
	{
		if (camera_0.clearFlags == CameraClearFlags.Skybox)
		{
			Skybox component = camera_0.GetComponent<Skybox>();
			Skybox component2 = camera_1.GetComponent<Skybox>();
			if ((bool)component && (bool)component.material)
			{
				component2.enabled = true;
				component2.material = component.material;
			}
			else
			{
				component2.enabled = false;
			}
		}
		camera_1.clearFlags = camera_0.clearFlags;
		camera_1.backgroundColor = camera_0.backgroundColor;
		camera_1.farClipPlane = camera_0.farClipPlane;
		camera_1.nearClipPlane = camera_0.nearClipPlane;
		camera_1.orthographic = camera_0.orthographic;
		camera_1.fieldOfView = camera_0.fieldOfView;
		camera_1.aspect = camera_0.aspect;
		camera_1.orthographicSize = camera_0.orthographicSize;
		camera_1.depthTextureMode = DepthTextureMode.None;
		camera_1.renderingPath = RenderingPath.Forward;
	}

	private Camera GetReflectionCamera(Camera camera_0, Material material_0, int int_1)
	{
		if (!renderTexture_0 || int_0 != int_1)
		{
			if ((bool)renderTexture_0)
			{
				Object.DestroyImmediate(renderTexture_0);
			}
			renderTexture_0 = new RenderTexture(int_1, int_1, 16);
			renderTexture_0.name = "__MirrorReflection" + GetInstanceID();
			renderTexture_0.isPowerOfTwo = true;
			renderTexture_0.hideFlags = HideFlags.DontSave;
			int_0 = int_1;
		}
		Camera camera = hashtable_0[camera_0] as Camera;
		if (!camera)
		{
			GameObject gameObject = new GameObject("Mirror Refl Camera id" + GetInstanceID() + " for " + camera_0.GetInstanceID(), typeof(Camera), typeof(Skybox));
			gameObject.hideFlags = HideFlags.HideAndDontSave;
			camera = gameObject.GetComponent<Camera>();
			camera.enabled = false;
			Transform transform = camera.transform;
			transform.position = transform_0.position;
			transform.rotation = transform_0.rotation;
			camera.gameObject.AddComponent<FlareLayer>();
			hashtable_0[camera_0] = camera;
		}
		if (material_0.HasProperty("_ReflectionTex"))
		{
			material_0.SetTexture("_ReflectionTex", renderTexture_0);
		}
		return camera;
	}

	private Vector4 CameraSpacePlane(Camera camera_0, Vector3 vector3_0, Vector3 vector3_1, float float_0)
	{
		Matrix4x4 worldToCameraMatrix = camera_0.worldToCameraMatrix;
		Vector3 lhs = worldToCameraMatrix.MultiplyPoint(vector3_0);
		Vector3 rhs = worldToCameraMatrix.MultiplyVector(vector3_1).normalized * float_0;
		return new Vector4(rhs.x, rhs.y, rhs.z, 0f - Vector3.Dot(lhs, rhs));
	}

	private void LateUpdate()
	{
		if (keepUnderCamera)
		{
			Transform transform = Camera.main.transform;
			Vector3 position = transform.position;
			position.y = transform_0.position.y;
			if (transform_0.position != position)
			{
				transform_0.position = position;
			}
		}
	}

	private void OnWillRenderObject()
	{
		if (bool_0)
		{
			return;
		}
		if (base.enabled && (bool)renderer_0 && renderer_0.enabled)
		{
			Material sharedMaterial = renderer_0.sharedMaterial;
			if (!sharedMaterial)
			{
				return;
			}
			Camera current = Camera.current;
			if (!current)
			{
				return;
			}
			bool supportsImageEffects;
			if (supportsImageEffects = SystemInfo.supportsImageEffects)
			{
				current.depthTextureMode |= DepthTextureMode.Depth;
			}
			else
			{
				quality = Quality.Fastest;
			}
			if (!Boolean_0)
			{
				sharedMaterial.shader.maximumLOD = ((!supportsImageEffects) ? 100 : 200);
				Clear();
				return;
			}
			LayerMask layerMask_ = LayerMask_0;
			int int32_ = Int32_0;
			if ((int)layerMask_ != 0 && int32_ >= 512)
			{
				sharedMaterial.shader.maximumLOD = 400;
				bool_0 = true;
				Camera reflectionCamera = GetReflectionCamera(current, sharedMaterial, int32_);
				Vector3 position = transform_0.position;
				Vector3 up = transform_0.up;
				CopyCamera(current, reflectionCamera);
				float w = 0f - Vector3.Dot(up, position);
				Vector4 vector4_ = new Vector4(up.x, up.y, up.z, w);
				Matrix4x4 matrix4x4_ = Matrix4x4.zero;
				CalculateReflectionMatrix(ref matrix4x4_, vector4_);
				Vector3 position2 = current.transform.position;
				Vector3 position3 = matrix4x4_.MultiplyPoint(position2);
				reflectionCamera.worldToCameraMatrix = current.worldToCameraMatrix * matrix4x4_;
				Vector4 vector4_2 = CameraSpacePlane(reflectionCamera, position, up, 1f);
				Matrix4x4 matrix4x4_2 = current.projectionMatrix;
				CalculateObliqueMatrix(ref matrix4x4_2, vector4_2);
				reflectionCamera.projectionMatrix = matrix4x4_2;
				reflectionCamera.cullingMask = -17 & layerMask_.value;
				reflectionCamera.targetTexture = renderTexture_0;
				GL.SetRevertBackfacing(true);
				reflectionCamera.transform.position = position3;
				Vector3 eulerAngles = current.transform.eulerAngles;
				reflectionCamera.transform.eulerAngles = new Vector3(0f, eulerAngles.y, eulerAngles.z);
				reflectionCamera.Render();
				reflectionCamera.transform.position = position2;
				GL.SetRevertBackfacing(false);
				bool_0 = false;
			}
			else
			{
				sharedMaterial.shader.maximumLOD = 300;
				Clear();
			}
		}
		else
		{
			Clear();
		}
	}
}
