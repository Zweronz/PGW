using System.Collections.Generic;
using UnityEngine;

namespace BlendModes
{
	public static class BlendMaterials
	{
		private static Dictionary<ObjectType, Dictionary<RenderMode, Dictionary<BlendMode, Material>>> dictionary_0 = new Dictionary<ObjectType, Dictionary<RenderMode, Dictionary<BlendMode, Material>>>();

		public static Material GetMaterial(ObjectType objectType_0, RenderMode renderMode_0, BlendMode blendMode_0)
		{
			if (blendMode_0 == BlendMode.Normal)
			{
				switch (objectType_0)
				{
				case ObjectType.MeshDefault:
				{
					Material material3 = new Material(Shader.Find("Diffuse"));
					material3.hideFlags = HideFlags.HideAndDontSave;
					return material3;
				}
				case ObjectType.SpriteDefault:
				{
					Material material2 = new Material(Shader.Find("Sprites/Default"));
					material2.hideFlags = HideFlags.HideAndDontSave;
					return material2;
				}
				case ObjectType.ParticleDefault:
				{
					Material material = new Material(Shader.Find("Particles/Additive"));
					material.hideFlags = HideFlags.HideAndDontSave;
					return material;
				}
				default:
					return null;
				}
			}
			if (Application.isEditor && renderMode_0 == RenderMode.Framebuffer)
			{
				renderMode_0 = RenderMode.Grab;
			}
			if (objectType_0 != ObjectType.MeshDefault && objectType_0 != ObjectType.ParticleDefault && dictionary_0.ContainsKey(objectType_0) && dictionary_0[objectType_0].ContainsKey(renderMode_0) && dictionary_0[objectType_0][renderMode_0].ContainsKey(blendMode_0))
			{
				return dictionary_0[objectType_0][renderMode_0][blendMode_0];
			}
			Material material4 = new Material(Resources.Load<Shader>(string.Format("BlendModes/{0}/{1}", objectType_0, renderMode_0)));
			material4.hideFlags = HideFlags.HideAndDontSave;
			material4.EnableKeyword("BM" + blendMode_0);
			if (!dictionary_0.ContainsKey(objectType_0))
			{
				dictionary_0.Add(objectType_0, new Dictionary<RenderMode, Dictionary<BlendMode, Material>>());
			}
			if (!dictionary_0[objectType_0].ContainsKey(renderMode_0))
			{
				dictionary_0[objectType_0].Add(renderMode_0, new Dictionary<BlendMode, Material>());
			}
			if (!dictionary_0[objectType_0][renderMode_0].ContainsKey(blendMode_0))
			{
				dictionary_0[objectType_0][renderMode_0].Add(blendMode_0, material4);
			}
			return material4;
		}
	}
}
