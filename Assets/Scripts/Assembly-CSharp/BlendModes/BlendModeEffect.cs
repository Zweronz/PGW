using UnityEngine;
using UnityEngine.UI;

namespace BlendModes
{
	public class BlendModeEffect : MonoBehaviour
	{
		[SerializeField]
		private BlendMode blendMode_0;

		[SerializeField]
		private RenderMode renderMode_0;

		[SerializeField]
		private Texture2D texture2D_0;

		[SerializeField]
		private Color color_0 = Color.white;

		public BlendMode BlendMode_0
		{
			get
			{
				return blendMode_0;
			}
			set
			{
				SetBlendMode(value, RenderMode_0);
			}
		}

		public RenderMode RenderMode_0
		{
			get
			{
				return renderMode_0;
			}
			set
			{
				SetBlendMode(BlendMode_0, value);
			}
		}

		public Texture2D Texture2D_0
		{
			get
			{
				return texture2D_0;
			}
			set
			{
				if ((bool)Material_0 && (ObjectType_0 == ObjectType.MeshDefault || ObjectType_0 == ObjectType.ParticleDefault))
				{
					Material_0.mainTexture = value;
				}
				texture2D_0 = value;
			}
		}

		public Color Color_0
		{
			get
			{
				return color_0;
			}
			set
			{
				if ((bool)Material_0 && (ObjectType_0 == ObjectType.MeshDefault || ObjectType_0 == ObjectType.ParticleDefault))
				{
					Material_0.color = value;
				}
				color_0 = value;
			}
		}

		public ObjectType ObjectType_0
		{
			get
			{
				if ((bool)GetComponent<Text>())
				{
					return ObjectType.UIDefaultFont;
				}
				if ((bool)GetComponent<MaskableGraphic>())
				{
					return ObjectType.UIDefault;
				}
				if ((bool)GetComponent<SpriteRenderer>())
				{
					return ObjectType.SpriteDefault;
				}
				if ((bool)GetComponent<MeshRenderer>())
				{
					return ObjectType.MeshDefault;
				}
				if ((bool)GetComponent<ParticleSystem>())
				{
					return ObjectType.ParticleDefault;
				}
				return ObjectType.Unknown;
			}
		}

		public Material Material_0
		{
			get
			{
				switch (ObjectType_0)
				{
				default:
					return null;
				case ObjectType.UIDefault:
					return GetComponent<MaskableGraphic>().material;
				case ObjectType.UIDefaultFont:
					return GetComponent<Text>().material;
				case ObjectType.SpriteDefault:
					return GetComponent<SpriteRenderer>().sharedMaterial;
				case ObjectType.MeshDefault:
					return GetComponent<MeshRenderer>().sharedMaterial;
				case ObjectType.ParticleDefault:
					return GetComponent<ParticleSystem>().GetComponent<Renderer>().sharedMaterial;
				}
			}
			set
			{
				switch (ObjectType_0)
				{
				case ObjectType.UIDefault:
					GetComponent<MaskableGraphic>().material = value;
					break;
				case ObjectType.UIDefaultFont:
					GetComponent<Text>().material = value;
					break;
				case ObjectType.SpriteDefault:
					GetComponent<SpriteRenderer>().sharedMaterial = value;
					break;
				case ObjectType.MeshDefault:
					GetComponent<MeshRenderer>().sharedMaterial = value;
					break;
				case ObjectType.ParticleDefault:
					GetComponent<ParticleSystem>().GetComponent<Renderer>().sharedMaterial = value;
					break;
				}
			}
		}

		public void SetBlendMode(BlendMode blendMode_1, RenderMode renderMode_1 = RenderMode.Grab)
		{
			if (ObjectType_0 != 0)
			{
				Material_0 = BlendMaterials.GetMaterial(ObjectType_0, renderMode_1, blendMode_1);
				Texture2D_0 = Texture2D_0;
				Color_0 = Color_0;
				blendMode_0 = blendMode_1;
				renderMode_0 = renderMode_1;
			}
		}

		public void OnEnable()
		{
			if ((bool)Material_0 && (bool)Material_0.mainTexture)
			{
				Texture2D_0 = (Texture2D)Material_0.mainTexture;
			}
			SetBlendMode(BlendMode_0, RenderMode_0);
		}

		public void OnDisable()
		{
			Texture2D texture2D = Texture2D_0;
			Material_0 = BlendMaterials.GetMaterial(ObjectType_0, RenderMode.Grab, BlendMode.Normal);
			if ((bool)Material_0 && (bool)texture2D)
			{
				Material_0.mainTexture = texture2D;
			}
		}
	}
}
