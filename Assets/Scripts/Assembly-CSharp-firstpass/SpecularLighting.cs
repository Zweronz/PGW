using UnityEngine;

[RequireComponent(typeof(WaterBase))]
public class SpecularLighting : MonoBehaviour
{
	public Transform specularLight;

	private WaterBase waterBase_0;

	public void Start()
	{
		waterBase_0 = (WaterBase)base.gameObject.GetComponent(typeof(WaterBase));
	}

	public void Update()
	{
		if (!waterBase_0)
		{
			waterBase_0 = (WaterBase)base.gameObject.GetComponent(typeof(WaterBase));
		}
		if ((bool)specularLight && (bool)waterBase_0.sharedMaterial)
		{
			waterBase_0.sharedMaterial.SetVector("_WorldLightDir", specularLight.transform.forward);
		}
	}
}
