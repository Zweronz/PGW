using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(Camera))]
public class CameraMotionBlur : PostEffectsBase
{
	[Serializable]
	public enum MotionBlurFilter
	{
		CameraMotion = 0,
		LocalBlur = 1,
		Reconstruction = 2,
		ReconstructionDX11 = 3
	}

	[NonSerialized]
	public static int int_0 = 10;

	public MotionBlurFilter filterType;

	public bool preview;

	public Vector3 previewScale;

	public float movementScale;

	public float rotationScale;

	public float maxVelocity;

	public int maxNumSamples;

	public float minVelocity;

	public float velocityScale;

	public float softZDistance;

	public int velocityDownsample;

	public LayerMask excludeLayers;

	private GameObject gameObject_0;

	public Shader shader;

	public Shader dx11MotionBlurShader;

	public Shader replacementClear;

	private Material material_0;

	private Material material_1;

	public Texture2D noiseTexture;

	public bool showVelocity;

	public float showVelocityScale;

	private Matrix4x4 matrix4x4_0;

	private Matrix4x4 matrix4x4_1;

	private int int_1;

	private bool bool_3;

	private Vector3 vector3_0;

	private Vector3 vector3_1;

	private Vector3 vector3_2;

	private Vector3 vector3_3;

	public CameraMotionBlur()
	{
		filterType = MotionBlurFilter.Reconstruction;
		previewScale = Vector3.one;
		rotationScale = 1f;
		maxVelocity = 8f;
		maxNumSamples = 17;
		minVelocity = 0.1f;
		velocityScale = 0.375f;
		softZDistance = 0.005f;
		velocityDownsample = 1;
		showVelocityScale = 1f;
		vector3_0 = Vector3.forward;
		vector3_1 = Vector3.right;
		vector3_2 = Vector3.up;
		vector3_3 = Vector3.zero;
	}

	private void CalculateViewProjection()
	{
		Matrix4x4 worldToCameraMatrix = GetComponent<Camera>().worldToCameraMatrix;
		Matrix4x4 gPUProjectionMatrix = GL.GetGPUProjectionMatrix(GetComponent<Camera>().projectionMatrix, true);
		matrix4x4_0 = gPUProjectionMatrix * worldToCameraMatrix;
	}

	public override void Start()
	{
		CheckResources();
		bool_3 = gameObject.activeInHierarchy;
		CalculateViewProjection();
		Remember();
		int_1 = -1;
		bool_3 = false;
	}

	public override void OnEnable()
	{
		GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;
	}

	public virtual void OnDisable()
	{
		if (null != material_0)
		{
			UnityEngine.Object.DestroyImmediate(material_0);
			material_0 = null;
		}
		if (null != material_1)
		{
			UnityEngine.Object.DestroyImmediate(material_1);
			material_1 = null;
		}
		if (null != gameObject_0)
		{
			UnityEngine.Object.DestroyImmediate(gameObject_0);
			gameObject_0 = null;
		}
	}

	public override bool CheckResources()
	{
		CheckSupport(true, true);
		material_0 = CheckShaderAndCreateMaterial(shader, material_0);
		if (bool_1 && filterType == MotionBlurFilter.ReconstructionDX11)
		{
			material_1 = CheckShaderAndCreateMaterial(dx11MotionBlurShader, material_1);
		}
		if (!bool_2)
		{
			ReportAutoDisable();
		}
		return bool_2;
	}

	public virtual void OnRenderImage(RenderTexture renderTexture_0, RenderTexture renderTexture_1)
	{
		if (!CheckResources())
		{
			Graphics.Blit(renderTexture_0, renderTexture_1);
			return;
		}
		if (filterType == MotionBlurFilter.CameraMotion)
		{
			StartFrame();
		}
		RenderTextureFormat format = ((!SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.RGHalf)) ? RenderTextureFormat.ARGBHalf : RenderTextureFormat.RGHalf);
		RenderTexture temporary = RenderTexture.GetTemporary(divRoundUp(renderTexture_0.width, velocityDownsample), divRoundUp(renderTexture_0.height, velocityDownsample), 0, format);
		int num = 1;
		int num2 = 1;
		maxVelocity = Mathf.Max(2f, maxVelocity);
		float num3 = maxVelocity;
		bool flag = false;
		if (filterType == MotionBlurFilter.ReconstructionDX11 && material_1 == null)
		{
			flag = true;
		}
		if (filterType != MotionBlurFilter.Reconstruction && !flag)
		{
			num = divRoundUp(temporary.width, (int)maxVelocity);
			num2 = divRoundUp(temporary.height, (int)maxVelocity);
			num3 = temporary.width / num;
		}
		else
		{
			maxVelocity = Mathf.Min(maxVelocity, int_0);
			num = divRoundUp(temporary.width, (int)maxVelocity);
			num2 = divRoundUp(temporary.height, (int)maxVelocity);
			num3 = temporary.width / num;
		}
		RenderTexture temporary2 = RenderTexture.GetTemporary(num, num2, 0, format);
		RenderTexture temporary3 = RenderTexture.GetTemporary(num, num2, 0, format);
		temporary.filterMode = FilterMode.Point;
		temporary2.filterMode = FilterMode.Point;
		temporary3.filterMode = FilterMode.Point;
		if ((bool)noiseTexture)
		{
			noiseTexture.filterMode = FilterMode.Point;
		}
		renderTexture_0.wrapMode = TextureWrapMode.Clamp;
		temporary.wrapMode = TextureWrapMode.Clamp;
		temporary3.wrapMode = TextureWrapMode.Clamp;
		temporary2.wrapMode = TextureWrapMode.Clamp;
		CalculateViewProjection();
		if (gameObject.activeInHierarchy && !bool_3)
		{
			Remember();
		}
		bool_3 = gameObject.activeInHierarchy;
		Matrix4x4 matrix4x = Matrix4x4.Inverse(matrix4x4_0);
		material_0.SetMatrix("_InvViewProj", matrix4x);
		material_0.SetMatrix("_PrevViewProj", matrix4x4_1);
		material_0.SetMatrix("_ToPrevViewProjCombined", matrix4x4_1 * matrix4x);
		material_0.SetFloat("_MaxVelocity", num3);
		material_0.SetFloat("_MaxRadiusOrKInPaper", num3);
		material_0.SetFloat("_MinVelocity", minVelocity);
		material_0.SetFloat("_VelocityScale", velocityScale);
		material_0.SetTexture("_NoiseTex", noiseTexture);
		material_0.SetTexture("_VelTex", temporary);
		material_0.SetTexture("_NeighbourMaxTex", temporary3);
		material_0.SetTexture("_TileTexDebug", temporary2);
		if (preview)
		{
			Matrix4x4 worldToCameraMatrix = this.GetComponent<Camera>().worldToCameraMatrix;
			Matrix4x4 identity = Matrix4x4.identity;
			identity.SetTRS(previewScale * 0.25f, Quaternion.identity, Vector3.one);
			Matrix4x4 gPUProjectionMatrix = GL.GetGPUProjectionMatrix(this.GetComponent<Camera>().projectionMatrix, true);
			matrix4x4_1 = gPUProjectionMatrix * identity * worldToCameraMatrix;
			material_0.SetMatrix("_PrevViewProj", matrix4x4_1);
			material_0.SetMatrix("_ToPrevViewProjCombined", matrix4x4_1 * matrix4x);
		}
		if (filterType == MotionBlurFilter.CameraMotion)
		{
			Vector4 zero = Vector4.zero;
			float num4 = Vector3.Dot(transform.up, Vector3.up);
			Vector3 rhs = vector3_3 - transform.position;
			float magnitude = rhs.magnitude;
			float num5 = 1f;
			num5 = Vector3.Angle(transform.up, vector3_2) / this.GetComponent<Camera>().fieldOfView * ((float)renderTexture_0.width * 0.75f);
			zero.x = rotationScale * num5;
			num5 = Vector3.Angle(transform.forward, vector3_0) / this.GetComponent<Camera>().fieldOfView * ((float)renderTexture_0.width * 0.75f);
			zero.y = rotationScale * num4 * num5;
			num5 = Vector3.Angle(transform.forward, vector3_0) / this.GetComponent<Camera>().fieldOfView * ((float)renderTexture_0.width * 0.75f);
			zero.z = rotationScale * (1f - num4) * num5;
			if (!(magnitude <= float.Epsilon) && !(movementScale <= float.Epsilon))
			{
				zero.w = movementScale * Vector3.Dot(transform.forward, rhs) * ((float)renderTexture_0.width * 0.5f);
				zero.x += movementScale * Vector3.Dot(transform.up, rhs) * ((float)renderTexture_0.width * 0.5f);
				zero.y += movementScale * Vector3.Dot(transform.right, rhs) * ((float)renderTexture_0.width * 0.5f);
			}
			if (preview)
			{
				material_0.SetVector("_BlurDirectionPacked", new Vector4(previewScale.y, previewScale.x, 0f, previewScale.z) * 0.5f * this.GetComponent<Camera>().fieldOfView);
			}
			else
			{
				material_0.SetVector("_BlurDirectionPacked", zero);
			}
		}
		else
		{
			Graphics.Blit(renderTexture_0, temporary, material_0, 0);
			Camera camera = null;
			if (excludeLayers.value != 0)
			{
				camera = GetTmpCam();
			}
			if ((bool)camera && excludeLayers.value != 0 && (bool)replacementClear && replacementClear.isSupported)
			{
				camera.targetTexture = temporary;
				camera.cullingMask = excludeLayers;
				camera.RenderWithShader(replacementClear, string.Empty);
			}
		}
		if (!preview && Time.frameCount != int_1)
		{
			int_1 = Time.frameCount;
			Remember();
		}
		renderTexture_0.filterMode = FilterMode.Bilinear;
		if (showVelocity)
		{
			material_0.SetFloat("_DisplayVelocityScale", showVelocityScale);
			Graphics.Blit(temporary, renderTexture_1, material_0, 1);
		}
		else if (filterType == MotionBlurFilter.ReconstructionDX11 && !flag)
		{
			material_1.SetFloat("_MaxVelocity", num3);
			material_1.SetFloat("_MinVelocity", minVelocity);
			material_1.SetFloat("_VelocityScale", velocityScale);
			material_1.SetTexture("_NoiseTex", noiseTexture);
			material_1.SetTexture("_VelTex", temporary);
			material_1.SetTexture("_NeighbourMaxTex", temporary3);
			material_1.SetFloat("_SoftZDistance", Mathf.Max(0.00025f, softZDistance));
			material_1.SetFloat("_MaxRadiusOrKInPaper", num3);
			maxNumSamples = 2 * (maxNumSamples / 2) + 1;
			material_1.SetFloat("_SampleCount", (float)maxNumSamples * 1f);
			Graphics.Blit(temporary, temporary2, material_1, 0);
			Graphics.Blit(temporary2, temporary3, material_1, 1);
			Graphics.Blit(renderTexture_0, renderTexture_1, material_1, 2);
		}
		else if (filterType != MotionBlurFilter.Reconstruction && !flag)
		{
			if (filterType == MotionBlurFilter.CameraMotion)
			{
				Graphics.Blit(renderTexture_0, renderTexture_1, material_0, 6);
			}
			else
			{
				Graphics.Blit(renderTexture_0, renderTexture_1, material_0, 5);
			}
		}
		else
		{
			material_0.SetFloat("_SoftZDistance", Mathf.Max(0.00025f, softZDistance));
			Graphics.Blit(temporary, temporary2, material_0, 2);
			Graphics.Blit(temporary2, temporary3, material_0, 3);
			Graphics.Blit(renderTexture_0, renderTexture_1, material_0, 4);
		}
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
		RenderTexture.ReleaseTemporary(temporary3);
	}

	public virtual void Remember()
	{
		matrix4x4_1 = matrix4x4_0;
		vector3_0 = transform.forward;
		vector3_1 = transform.right;
		vector3_2 = transform.up;
		vector3_3 = transform.position;
	}

	public virtual Camera GetTmpCam()
	{
		if (gameObject_0 == null)
		{
			string text = "_" + GetComponent<Camera>().name + "_MotionBlurTmpCam";
			GameObject gameObject = GameObject.Find(text);
			if (null == gameObject)
			{
				gameObject_0 = new GameObject(text, typeof(Camera));
			}
			else
			{
				gameObject_0 = gameObject;
			}
		}
		gameObject_0.hideFlags = HideFlags.DontSave;
		gameObject_0.transform.position = GetComponent<Camera>().transform.position;
		gameObject_0.transform.rotation = GetComponent<Camera>().transform.rotation;
		gameObject_0.transform.localScale = GetComponent<Camera>().transform.localScale;
		gameObject_0.GetComponent<Camera>().CopyFrom(GetComponent<Camera>());
		gameObject_0.GetComponent<Camera>().enabled = false;
		gameObject_0.GetComponent<Camera>().depthTextureMode = DepthTextureMode.None;
		gameObject_0.GetComponent<Camera>().clearFlags = CameraClearFlags.Nothing;
		return gameObject_0.GetComponent<Camera>();
	}

	public virtual void StartFrame()
	{
		vector3_3 = Vector3.Slerp(vector3_3, transform.position, 0.75f);
	}

	public virtual int divRoundUp(int int_2, int int_3)
	{
		return (int_2 + int_3 - 1) / int_3;
	}

	public override void Main()
	{
	}
}
