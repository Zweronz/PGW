using System;
using System.Reflection;
using UnityEngine;

internal sealed class AddCamFX : MonoBehaviour
{
	private void Start()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("CamFX");
		if (!(gameObject != null) || !(gameObject.GetComponent<CamFXSetting>().CamFX != null))
		{
			return;
		}
		Component[] components = gameObject.GetComponent<CamFXSetting>().CamFX.GetComponents<Component>();
		Component[] array = components;
		foreach (Component component in array)
		{
			if (component.GetType() != typeof(Camera) && component.GetType() != typeof(Transform))
			{
				CopyComponent(component, base.gameObject);
			}
		}
		Debug.Log("_camFXComponents" + components);
	}

	private Component CopyComponent(Component component_0, GameObject gameObject_0)
	{
		Type type = component_0.GetType();
		Component component = gameObject_0.AddComponent(type);
		FieldInfo[] fields = type.GetFields();
		FieldInfo[] array = fields;
		foreach (FieldInfo fieldInfo in array)
		{
			fieldInfo.SetValue(component, fieldInfo.GetValue(component_0));
		}
		return component;
	}
}
