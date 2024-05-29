using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WaterBase))]
public class PlanarReflection : MonoBehaviour
{
	public LayerMask reflectionMask;

	public bool reflectSkybox;

	public Color clearColor = Color.grey;

	public string reflectionSampler = "_ReflectionTex";

	public float clipPlaneOffset = 0.07f;

	private Vector3 vector3_0 = Vector3.zero;

	private Camera camera_0;

	private Material material_0;

	private Dictionary<Camera, bool> dictionary_0;

	public void Start()
	{
		material_0 = ((WaterBase)base.gameObject.GetComponent(typeof(WaterBase))).sharedMaterial;
	}

	private Camera CreateReflectionCameraFor(Camera camera_1)
	{
		string text = base.gameObject.name + "Reflection" + camera_1.name;
		GameObject gameObject = GameObject.Find(text);
		if (!gameObject)
		{
			gameObject = new GameObject(text, typeof(Camera));
		}
		if (!gameObject.GetComponent(typeof(Camera)))
		{
			gameObject.AddComponent(typeof(Camera));
		}
		Camera camera = gameObject.GetComponent<Camera>();
		camera.backgroundColor = clearColor;
		camera.clearFlags = (reflectSkybox ? CameraClearFlags.Skybox : CameraClearFlags.Color);
		SetStandardCameraParameter(camera, reflectionMask);
		if (!camera.targetTexture)
		{
			camera.targetTexture = CreateTextureFor(camera_1);
		}
		return camera;
	}

	private void SetStandardCameraParameter(Camera camera_1, LayerMask layerMask_0)
	{
		camera_1.cullingMask = (int)layerMask_0 & ~(1 << LayerMask.NameToLayer("Water"));
		camera_1.backgroundColor = Color.black;
		camera_1.enabled = false;
	}

	private RenderTexture CreateTextureFor(Camera camera_1)
	{
		RenderTexture renderTexture = new RenderTexture(Mathf.FloorToInt(camera_1.pixelWidth * 0.5f), Mathf.FloorToInt(camera_1.pixelHeight * 0.5f), 24);
		renderTexture.hideFlags = HideFlags.DontSave;
		return renderTexture;
	}

	public void RenderHelpCameras(Camera camera_1)
	{
		if (dictionary_0 == null)
		{
			dictionary_0 = new Dictionary<Camera, bool>();
		}
		if (!dictionary_0.ContainsKey(camera_1))
		{
			dictionary_0.Add(camera_1, false);
		}
		if (!dictionary_0[camera_1])
		{
			if (!camera_0)
			{
				camera_0 = CreateReflectionCameraFor(camera_1);
			}
			RenderReflectionFor(camera_1, camera_0);
			dictionary_0[camera_1] = true;
		}
	}

	public void LateUpdate()
	{
		if (dictionary_0 != null)
		{
			dictionary_0.Clear();
		}
	}

	public void WaterTileBeingRendered(Transform transform_0, Camera camera_1)
	{
		RenderHelpCameras(camera_1);
		if ((bool)camera_0 && (bool)material_0)
		{
			material_0.SetTexture(reflectionSampler, camera_0.targetTexture);
		}
	}

	public void OnEnable()
	{
		Shader.EnableKeyword("WATER_REFLECTIVE");
		Shader.DisableKeyword("WATER_SIMPLE");
	}

	public void OnDisable()
	{
		Shader.EnableKeyword("WATER_SIMPLE");
		Shader.DisableKeyword("WATER_REFLECTIVE");
	}

	private void RenderReflectionFor(Camera camera_1, Camera camera_2)
	{
		if (!camera_2 || ((bool)material_0 && !material_0.HasProperty(reflectionSampler)))
		{
			return;
		}
		camera_2.cullingMask = (int)reflectionMask & ~(1 << LayerMask.NameToLayer("Water"));
		SaneCameraSettings(camera_2);
		camera_2.backgroundColor = clearColor;
		camera_2.clearFlags = (reflectSkybox ? CameraClearFlags.Skybox : CameraClearFlags.Color);
		if (reflectSkybox && (bool)camera_1.gameObject.GetComponent(typeof(Skybox)))
		{
			Skybox skybox = (Skybox)camera_2.gameObject.GetComponent(typeof(Skybox));
			if (!skybox)
			{
				skybox = (Skybox)camera_2.gameObject.AddComponent(typeof(Skybox));
			}
			skybox.material = ((Skybox)camera_1.GetComponent(typeof(Skybox))).material;
		}
		GL.SetRevertBackfacing(true);
		Transform transform = base.transform;
		Vector3 eulerAngles = camera_1.transform.eulerAngles;
		camera_2.transform.eulerAngles = new Vector3(0f - eulerAngles.x, eulerAngles.y, eulerAngles.z);
		camera_2.transform.position = camera_1.transform.position;
		Vector3 position = transform.transform.position;
		position.y = transform.position.y;
		Vector3 up = transform.transform.up;
		float w = 0f - Vector3.Dot(up, position) - clipPlaneOffset;
		Vector4 vector4_ = new Vector4(up.x, up.y, up.z, w);
		Matrix4x4 zero = Matrix4x4.zero;
		zero = CalculateReflectionMatrix(zero, vector4_);
		vector3_0 = camera_1.transform.position;
		Vector3 position2 = zero.MultiplyPoint(vector3_0);
		camera_2.worldToCameraMatrix = camera_1.worldToCameraMatrix * zero;
		Vector4 vector4_2 = CameraSpacePlane(camera_2, position, up, 1f);
		Matrix4x4 projectionMatrix = camera_1.projectionMatrix;
		projectionMatrix = CalculateObliqueMatrix(projectionMatrix, vector4_2);
		camera_2.projectionMatrix = projectionMatrix;
		camera_2.transform.position = position2;
		Vector3 eulerAngles2 = camera_1.transform.eulerAngles;
		camera_2.transform.eulerAngles = new Vector3(0f - eulerAngles2.x, eulerAngles2.y, eulerAngles2.z);
		camera_2.Render();
		GL.SetRevertBackfacing(false);
	}

	private void SaneCameraSettings(Camera camera_1)
	{
		camera_1.depthTextureMode = DepthTextureMode.None;
		camera_1.backgroundColor = Color.black;
		camera_1.clearFlags = CameraClearFlags.Color;
		camera_1.renderingPath = RenderingPath.Forward;
	}

	private static Matrix4x4 CalculateObliqueMatrix(Matrix4x4 matrix4x4_0, Vector4 vector4_0)
	{
		Vector4 b = matrix4x4_0.inverse * new Vector4(sgn(vector4_0.x), sgn(vector4_0.y), 1f, 1f);
		Vector4 vector = vector4_0 * (2f / Vector4.Dot(vector4_0, b));
		matrix4x4_0[2] = vector.x - matrix4x4_0[3];
		matrix4x4_0[6] = vector.y - matrix4x4_0[7];
		matrix4x4_0[10] = vector.z - matrix4x4_0[11];
		matrix4x4_0[14] = vector.w - matrix4x4_0[15];
		return matrix4x4_0;
	}

	private static Matrix4x4 CalculateReflectionMatrix(Matrix4x4 matrix4x4_0, Vector4 vector4_0)
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
		return matrix4x4_0;
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

	private Vector4 CameraSpacePlane(Camera camera_1, Vector3 vector3_1, Vector3 vector3_2, float float_0)
	{
		Vector3 v = vector3_1 + vector3_2 * clipPlaneOffset;
		Matrix4x4 worldToCameraMatrix = camera_1.worldToCameraMatrix;
		Vector3 lhs = worldToCameraMatrix.MultiplyPoint(v);
		Vector3 rhs = worldToCameraMatrix.MultiplyVector(vector3_2).normalized * float_0;
		return new Vector4(rhs.x, rhs.y, rhs.z, 0f - Vector3.Dot(lhs, rhs));
	}
}
