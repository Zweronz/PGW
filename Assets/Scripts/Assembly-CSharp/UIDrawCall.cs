using System;
using System.Collections.Generic;
using UnityEngine;

public class UIDrawCall : MonoBehaviour
{
	public enum Clipping
	{
		None = 0,
		SoftClip = 3,
		ConstrainButDontClip = 4
	}

	private const int int_0 = 10;

	private static BetterList<UIDrawCall> betterList_0 = new BetterList<UIDrawCall>();

	private static BetterList<UIDrawCall> betterList_1 = new BetterList<UIDrawCall>();

	[NonSerialized]
	public int int_1 = int.MaxValue;

	[NonSerialized]
	public int int_2 = int.MinValue;

	[NonSerialized]
	public UIPanel uipanel_0;

	[NonSerialized]
	public UIPanel uipanel_1;

	[NonSerialized]
	public bool bool_0;

	[NonSerialized]
	public BetterList<Vector3> betterList_2 = new BetterList<Vector3>();

	[NonSerialized]
	public BetterList<Vector3> betterList_3 = new BetterList<Vector3>();

	[NonSerialized]
	public BetterList<Vector4> betterList_4 = new BetterList<Vector4>();

	[NonSerialized]
	public BetterList<Vector2> betterList_5 = new BetterList<Vector2>();

	[NonSerialized]
	public BetterList<Color32> betterList_6 = new BetterList<Color32>();

	private Material material_0;

	private Texture texture_0;

	private Shader shader_0;

	private int int_3;

	private Transform transform_0;

	private Mesh mesh_0;

	private MeshFilter meshFilter_0;

	private MeshRenderer meshRenderer_0;

	private Material material_1;

	private int[] int_4;

	private bool bool_1 = true;

	private bool bool_2;

	private int int_5 = 3000;

	private int int_6;

	[NonSerialized]
	public bool isDirty;

	private static List<int[]> list_0 = new List<int[]>(10);

	private static string[] string_0 = new string[4] { "_ClipRange0", "_ClipRange1", "_ClipRange2", "_ClipRange4" };

	private static string[] string_1 = new string[4] { "_ClipArgs0", "_ClipArgs1", "_ClipArgs2", "_ClipArgs3" };

	[Obsolete("Use UIDrawCall.activeList")]
	public static BetterList<UIDrawCall> BetterList_0
	{
		get
		{
			return betterList_0;
		}
	}

	public static BetterList<UIDrawCall> BetterList_1
	{
		get
		{
			return betterList_0;
		}
	}

	public static BetterList<UIDrawCall> BetterList_2
	{
		get
		{
			return betterList_1;
		}
	}

	public int Int32_0
	{
		get
		{
			return int_5;
		}
		set
		{
			if (int_5 != value)
			{
				int_5 = value;
				if (material_1 != null)
				{
					material_1.renderQueue = value;
				}
			}
		}
	}

	public int Int32_1
	{
		get
		{
			return (meshRenderer_0 != null) ? meshRenderer_0.sortingOrder : 0;
		}
		set
		{
			if (meshRenderer_0 != null && meshRenderer_0.sortingOrder != value)
			{
				meshRenderer_0.sortingOrder = value;
			}
		}
	}

	public int Int32_2
	{
		get
		{
			return (!(material_1 != null)) ? int_5 : material_1.renderQueue;
		}
	}

	public Transform Transform_0
	{
		get
		{
			if (transform_0 == null)
			{
				transform_0 = base.transform;
			}
			return transform_0;
		}
	}

	public Material Material_0
	{
		get
		{
			return material_0;
		}
		set
		{
			if (material_0 != value)
			{
				material_0 = value;
				bool_1 = true;
			}
		}
	}

	public Material Material_1
	{
		get
		{
			return material_1;
		}
	}

	public Texture Texture_0
	{
		get
		{
			return texture_0;
		}
		set
		{
			texture_0 = value;
			if (material_1 != null)
			{
				material_1.mainTexture = value;
			}
		}
	}

	public Shader Shader_0
	{
		get
		{
			return shader_0;
		}
		set
		{
			if (shader_0 != value)
			{
				shader_0 = value;
				bool_1 = true;
			}
		}
	}

	public int Int32_3
	{
		get
		{
			return (mesh_0 != null) ? int_6 : 0;
		}
	}

	public bool Boolean_0
	{
		get
		{
			return int_3 != 0;
		}
	}

	private void CreateMaterial()
	{
		bool_2 = false;
		int_3 = uipanel_1.Int32_3;
		string text = ((shader_0 != null) ? shader_0.name : ((!(material_0 != null)) ? "Unlit/Transparent Colored" : material_0.shader.name));
		text = text.Replace("GUI/Text Shader", "Unlit/Text");
		if (text.Length > 2 && text[text.Length - 2] == ' ')
		{
			int num = text[text.Length - 1];
			if (num > 48 && num <= 57)
			{
				text = text.Substring(0, text.Length - 2);
			}
		}
		if (text.StartsWith("Hidden/"))
		{
			text = text.Substring(7);
		}
		text = text.Replace(" (SoftClip)", string.Empty);
		if (int_3 != 0)
		{
			Shader_0 = Shader.Find("Hidden/" + text + " " + int_3);
			if (Shader_0 == null)
			{
				Shader.Find(text + " " + int_3);
			}
			if (Shader_0 == null && int_3 == 1)
			{
				bool_2 = true;
				Shader_0 = Shader.Find(text + " (SoftClip)");
			}
		}
		else
		{
			Shader_0 = Shader.Find(text);
		}
		if (material_0 != null)
		{
			material_1 = new Material(material_0);
			material_1.hideFlags = HideFlags.DontSave | HideFlags.NotEditable;
			material_1.CopyPropertiesFromMaterial(material_0);
			string[] shaderKeywords = material_0.shaderKeywords;
			for (int i = 0; i < shaderKeywords.Length; i++)
			{
				material_1.EnableKeyword(shaderKeywords[i]);
			}
			if (Shader_0 != null)
			{
				material_1.shader = Shader_0;
			}
			else if (int_3 != 0)
			{
				Debug.LogError(text + " shader doesn't have a clipped shader version for " + int_3 + " clip regions");
			}
		}
		else
		{
			material_1 = new Material(Shader_0);
			material_1.hideFlags = HideFlags.DontSave | HideFlags.NotEditable;
		}
	}

	private Material RebuildMaterial()
	{
		NGUITools.DestroyImmediate(material_1);
		CreateMaterial();
		material_1.renderQueue = int_5;
		if (texture_0 != null)
		{
			material_1.mainTexture = texture_0;
		}
		if (meshRenderer_0 != null)
		{
			meshRenderer_0.sharedMaterials = new Material[1] { material_1 };
		}
		return material_1;
	}

	private void UpdateMaterials()
	{
		if (!bool_1 && !(material_1 == null) && int_3 == uipanel_1.Int32_3)
		{
			if (meshRenderer_0.sharedMaterial != material_1)
			{
				meshRenderer_0.sharedMaterials = new Material[1] { material_1 };
			}
		}
		else
		{
			RebuildMaterial();
			bool_1 = false;
		}
	}

	public void UpdateGeometry()
	{
		int size = betterList_2.size;
		if (size > 0 && size == betterList_5.size && size == betterList_6.size && size % 4 == 0)
		{
			if (meshFilter_0 == null)
			{
				meshFilter_0 = base.gameObject.GetComponent<MeshFilter>();
			}
			if (meshFilter_0 == null)
			{
				meshFilter_0 = base.gameObject.AddComponent<MeshFilter>();
			}
			if (betterList_2.size < 65000)
			{
				int num = (size >> 1) * 3;
				bool flag = int_4 == null || int_4.Length != num;
				if (mesh_0 == null)
				{
					mesh_0 = new Mesh();
					mesh_0.hideFlags = HideFlags.DontSave;
					mesh_0.name = ((!(material_0 != null)) ? "Mesh" : material_0.name);
					mesh_0.MarkDynamic();
					flag = true;
				}
				bool flag2;
				if (!(flag2 = betterList_5.buffer.Length != betterList_2.buffer.Length || betterList_6.buffer.Length != betterList_2.buffer.Length || (betterList_3.buffer != null && betterList_3.buffer.Length != betterList_2.buffer.Length) || (betterList_4.buffer != null && betterList_4.buffer.Length != betterList_2.buffer.Length)) && uipanel_1.renderQueue_0 != 0)
				{
					flag2 = mesh_0 == null || mesh_0.vertexCount != betterList_2.buffer.Length;
				}
				if (!flag2 && betterList_2.size << 1 < betterList_2.buffer.Length)
				{
					flag2 = true;
				}
				int_6 = betterList_2.size >> 1;
				if (!flag2 && betterList_2.buffer.Length <= 65000)
				{
					if (mesh_0.vertexCount != betterList_2.buffer.Length)
					{
						mesh_0.Clear();
						flag = true;
					}
					mesh_0.vertices = betterList_2.buffer;
					mesh_0.uv = betterList_5.buffer;
					mesh_0.colors32 = betterList_6.buffer;
					if (betterList_3 != null)
					{
						mesh_0.normals = betterList_3.buffer;
					}
					if (betterList_4 != null)
					{
						mesh_0.tangents = betterList_4.buffer;
					}
				}
				else
				{
					if (flag2 || mesh_0.vertexCount != betterList_2.size)
					{
						mesh_0.Clear();
						flag = true;
					}
					mesh_0.vertices = betterList_2.ToArray();
					mesh_0.uv = betterList_5.ToArray();
					mesh_0.colors32 = betterList_6.ToArray();
					if (betterList_3 != null)
					{
						mesh_0.normals = betterList_3.ToArray();
					}
					if (betterList_4 != null)
					{
						mesh_0.tangents = betterList_4.ToArray();
					}
				}
				if (flag)
				{
					int_4 = GenerateCachedIndexBuffer(size, num);
					mesh_0.triangles = int_4;
				}
				if (flag2 || !bool_0)
				{
					mesh_0.RecalculateBounds();
				}
				meshFilter_0.mesh = mesh_0;
			}
			else
			{
				int_6 = 0;
				if (meshFilter_0.mesh != null)
				{
					meshFilter_0.mesh.Clear();
				}
				Debug.LogError("Too many vertices on one panel: " + betterList_2.size);
			}
			if (meshRenderer_0 == null)
			{
				meshRenderer_0 = base.gameObject.GetComponent<MeshRenderer>();
			}
			if (meshRenderer_0 == null)
			{
				meshRenderer_0 = base.gameObject.AddComponent<MeshRenderer>();
			}
			UpdateMaterials();
		}
		else
		{
			if (meshFilter_0.mesh != null)
			{
				meshFilter_0.mesh.Clear();
			}
			Debug.LogError("UIWidgets must fill the buffer with 4 vertices per quad. Found " + size);
		}
		betterList_2.Clear();
		betterList_5.Clear();
		betterList_6.Clear();
		betterList_3.Clear();
		betterList_4.Clear();
	}

	private int[] GenerateCachedIndexBuffer(int int_7, int int_8)
	{
		int num = 0;
		int count = list_0.Count;
		int[] array;
		while (true)
		{
			if (num < count)
			{
				array = list_0[num];
				if (array != null && array.Length == int_8)
				{
					break;
				}
				num++;
				continue;
			}
			int[] array2 = new int[int_8];
			int num2 = 0;
			for (int i = 0; i < int_7; i += 4)
			{
				array2[num2++] = i;
				array2[num2++] = i + 1;
				array2[num2++] = i + 2;
				array2[num2++] = i + 2;
				array2[num2++] = i + 3;
				array2[num2++] = i;
			}
			if (list_0.Count > 10)
			{
				list_0.RemoveAt(0);
			}
			list_0.Add(array2);
			return array2;
		}
		return array;
	}

	private void OnWillRenderObject()
	{
		UpdateMaterials();
		if (material_1 == null || int_3 == 0)
		{
			return;
		}
		if (!bool_2)
		{
			UIPanel uIPanel_ = uipanel_1;
			int num = 0;
			while (uIPanel_ != null)
			{
				if (uIPanel_.Boolean_4)
				{
					float float_ = 0f;
					Vector4 vector4_ = uIPanel_.vector4_0;
					if (uIPanel_ != uipanel_1)
					{
						Vector3 vector = uIPanel_.Transform_0.InverseTransformPoint(uipanel_1.Transform_0.position);
						vector4_.x -= vector.x;
						vector4_.y -= vector.y;
						Vector3 eulerAngles = uipanel_1.Transform_0.rotation.eulerAngles;
						Vector3 eulerAngles2 = uIPanel_.Transform_0.rotation.eulerAngles;
						Vector3 vector2 = eulerAngles2 - eulerAngles;
						vector2.x = NGUIMath.WrapAngle(vector2.x);
						vector2.y = NGUIMath.WrapAngle(vector2.y);
						vector2.z = NGUIMath.WrapAngle(vector2.z);
						if (Mathf.Abs(vector2.x) > 0.001f || Mathf.Abs(vector2.y) > 0.001f)
						{
							Debug.LogWarning("Panel can only be clipped properly if X and Y rotation is left at 0", uipanel_1);
						}
						float_ = vector2.z;
					}
					SetClipping(num++, vector4_, uIPanel_.Vector2_1, float_);
				}
				uIPanel_ = uIPanel_.UIPanel_0;
			}
		}
		else
		{
			Vector2 vector2_ = uipanel_1.Vector2_1;
			Vector4 vector4_2 = uipanel_1.vector4_0;
			Vector2 mainTextureOffset = new Vector2((0f - vector4_2.x) / vector4_2.z, (0f - vector4_2.y) / vector4_2.w);
			Vector2 mainTextureScale = new Vector2(1f / vector4_2.z, 1f / vector4_2.w);
			Vector2 vector3 = new Vector2(1000f, 1000f);
			if (vector2_.x > 0f)
			{
				vector3.x = vector4_2.z / vector2_.x;
			}
			if (vector2_.y > 0f)
			{
				vector3.y = vector4_2.w / vector2_.y;
			}
			material_1.mainTextureOffset = mainTextureOffset;
			material_1.mainTextureScale = mainTextureScale;
			material_1.SetVector("_ClipSharpness", vector3);
		}
	}

	private void SetClipping(int int_7, Vector4 vector4_0, Vector2 vector2_0, float float_0)
	{
		float_0 *= -(float)Math.PI / 180f;
		Vector2 vector = new Vector2(1000f, 1000f);
		if (vector2_0.x > 0f)
		{
			vector.x = vector4_0.z / vector2_0.x;
		}
		if (vector2_0.y > 0f)
		{
			vector.y = vector4_0.w / vector2_0.y;
		}
		if (int_7 < string_0.Length)
		{
			material_1.SetVector(string_0[int_7], new Vector4((0f - vector4_0.x) / vector4_0.z, (0f - vector4_0.y) / vector4_0.w, 1f / vector4_0.z, 1f / vector4_0.w));
			material_1.SetVector(string_1[int_7], new Vector4(vector.x, vector.y, Mathf.Sin(float_0), Mathf.Cos(float_0)));
		}
	}

	private void OnEnable()
	{
		bool_1 = true;
	}

	private void OnDisable()
	{
		int_1 = int.MaxValue;
		int_2 = int.MinValue;
		uipanel_1 = null;
		uipanel_0 = null;
		material_0 = null;
		texture_0 = null;
		NGUITools.DestroyImmediate(material_1);
		material_1 = null;
		if (meshRenderer_0 != null)
		{
			meshRenderer_0.sharedMaterials = new Material[0];
		}
	}

	private void OnDestroy()
	{
		NGUITools.DestroyImmediate(mesh_0);
	}

	public static UIDrawCall Create(UIPanel uipanel_2, Material material_2, Texture texture_1, Shader shader_1)
	{
		return Create(null, uipanel_2, material_2, texture_1, shader_1);
	}

	private static UIDrawCall Create(string string_2, UIPanel uipanel_2, Material material_2, Texture texture_1, Shader shader_1)
	{
		UIDrawCall uIDrawCall = Create(string_2);
		uIDrawCall.gameObject.layer = uipanel_2.GameObject_0.layer;
		uIDrawCall.Material_0 = material_2;
		uIDrawCall.Texture_0 = texture_1;
		uIDrawCall.Shader_0 = shader_1;
		uIDrawCall.Int32_0 = uipanel_2.int_1;
		uIDrawCall.Int32_1 = uipanel_2.Int32_2;
		uIDrawCall.uipanel_0 = uipanel_2;
		return uIDrawCall;
	}

	private static UIDrawCall Create(string string_2)
	{
		if (betterList_1.size > 0)
		{
			UIDrawCall uIDrawCall = betterList_1.Pop();
			betterList_0.Add(uIDrawCall);
			if (string_2 != null)
			{
				uIDrawCall.name = string_2;
			}
			NGUITools.SetActive(uIDrawCall.gameObject, true);
			return uIDrawCall;
		}
		GameObject gameObject = new GameObject(string_2);
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
		UIDrawCall uIDrawCall2 = gameObject.AddComponent<UIDrawCall>();
		betterList_0.Add(uIDrawCall2);
		return uIDrawCall2;
	}

	public static void ClearAll()
	{
		bool isPlaying = Application.isPlaying;
		int num = betterList_0.size;
		while (num > 0)
		{
			UIDrawCall uIDrawCall = betterList_0[--num];
			if ((bool)uIDrawCall)
			{
				if (isPlaying)
				{
					NGUITools.SetActive(uIDrawCall.gameObject, false);
				}
				else
				{
					NGUITools.DestroyImmediate(uIDrawCall.gameObject);
				}
			}
		}
		betterList_0.Clear();
	}

	public static void ReleaseAll()
	{
		ClearAll();
		ReleaseInactive();
	}

	public static void ReleaseInactive()
	{
		int num = betterList_1.size;
		while (num > 0)
		{
			UIDrawCall uIDrawCall = betterList_1[--num];
			if ((bool)uIDrawCall)
			{
				NGUITools.DestroyImmediate(uIDrawCall.gameObject);
			}
		}
		betterList_1.Clear();
	}

	public static int Count(UIPanel uipanel_2)
	{
		int num = 0;
		for (int i = 0; i < betterList_0.size; i++)
		{
			if (betterList_0[i].uipanel_0 == uipanel_2)
			{
				num++;
			}
		}
		return num;
	}

	public static void Destroy(UIDrawCall uidrawCall_0)
	{
		if (!uidrawCall_0)
		{
			return;
		}
		if (Application.isPlaying)
		{
			if (betterList_0.Remove(uidrawCall_0))
			{
				NGUITools.SetActive(uidrawCall_0.gameObject, false);
				betterList_1.Add(uidrawCall_0);
			}
		}
		else
		{
			betterList_0.Remove(uidrawCall_0);
			NGUITools.DestroyImmediate(uidrawCall_0.gameObject);
		}
	}
}
