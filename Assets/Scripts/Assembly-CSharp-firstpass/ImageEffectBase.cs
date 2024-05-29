using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ImageEffectBase : MonoBehaviour
{
	public Shader shader;

	private Material material_0;

	protected Material Material_0
	{
		get
		{
			if (material_0 == null)
			{
				material_0 = new Material(shader);
				material_0.hideFlags = HideFlags.HideAndDontSave;
			}
			return material_0;
		}
	}

	protected virtual void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
		}
		else if (!shader || !shader.isSupported)
		{
			base.enabled = false;
		}
	}

	protected virtual void OnDisable()
	{
		if ((bool)material_0)
		{
			Object.DestroyImmediate(material_0);
		}
	}
}
