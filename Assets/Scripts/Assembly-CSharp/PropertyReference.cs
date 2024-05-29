using System;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;

[Serializable]
public class PropertyReference
{
	[SerializeField]
	private Component component_0;

	[SerializeField]
	private string string_0;

	private FieldInfo fieldInfo_0;

	private PropertyInfo propertyInfo_0;

	private static int int_0 = "PropertyBinding".GetHashCode();

	public Component Component_0
	{
		get
		{
			return component_0;
		}
		set
		{
			component_0 = value;
			propertyInfo_0 = null;
			fieldInfo_0 = null;
		}
	}

	public string String_0
	{
		get
		{
			return string_0;
		}
		set
		{
			string_0 = value;
			propertyInfo_0 = null;
			fieldInfo_0 = null;
		}
	}

	public bool Boolean_0
	{
		get
		{
			return component_0 != null && !string.IsNullOrEmpty(string_0);
		}
	}

	public bool Boolean_1
	{
		get
		{
			if (component_0 == null)
			{
				return false;
			}
			MonoBehaviour monoBehaviour = component_0 as MonoBehaviour;
			return monoBehaviour == null || monoBehaviour.enabled;
		}
	}

	public PropertyReference()
	{
	}

	public PropertyReference(Component component_1, string string_1)
	{
		component_0 = component_1;
		string_0 = string_1;
	}

	public Type GetPropertyType()
	{
		if (propertyInfo_0 == null && fieldInfo_0 == null && Boolean_0)
		{
			Cache();
		}
		if (propertyInfo_0 != null)
		{
			return propertyInfo_0.PropertyType;
		}
		if (fieldInfo_0 != null)
		{
			return fieldInfo_0.FieldType;
		}
		return typeof(void);
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return !Boolean_0;
		}
		if (obj is PropertyReference)
		{
			PropertyReference propertyReference = obj as PropertyReference;
			return component_0 == propertyReference.component_0 && string.Equals(string_0, propertyReference.string_0);
		}
		return false;
	}

	public override int GetHashCode()
	{
		return int_0;
	}

	public void Set(Component component_1, string string_1)
	{
		component_0 = component_1;
		string_0 = string_1;
	}

	public void Clear()
	{
		component_0 = null;
		string_0 = null;
	}

	public void Reset()
	{
		fieldInfo_0 = null;
		propertyInfo_0 = null;
	}

	public override string ToString()
	{
		return ToString(component_0, String_0);
	}

	public static string ToString(Component component_1, string string_1)
	{
		if (component_1 != null)
		{
			string text = component_1.GetType().ToString();
			int num = text.LastIndexOf('.');
			if (num > 0)
			{
				text = text.Substring(num + 1);
			}
			if (!string.IsNullOrEmpty(string_1))
			{
				return text + "." + string_1;
			}
			return text + ".[property]";
		}
		return null;
	}

	[DebuggerStepThrough]
	[DebuggerHidden]
	public object Get()
	{
		if (propertyInfo_0 == null && fieldInfo_0 == null && Boolean_0)
		{
			Cache();
		}
		if (propertyInfo_0 != null)
		{
			if (propertyInfo_0.CanRead)
			{
				return propertyInfo_0.GetValue(component_0, null);
			}
		}
		else if (fieldInfo_0 != null)
		{
			return fieldInfo_0.GetValue(component_0);
		}
		return null;
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public bool Set(object object_0)
	{
		if (propertyInfo_0 == null && fieldInfo_0 == null && Boolean_0)
		{
			Cache();
		}
		if (propertyInfo_0 == null && fieldInfo_0 == null)
		{
			return false;
		}
		if (object_0 == null)
		{
			try
			{
				if (propertyInfo_0 != null)
				{
					propertyInfo_0.SetValue(component_0, null, null);
				}
				else
				{
					fieldInfo_0.SetValue(component_0, null);
				}
			}
			catch (Exception)
			{
				return false;
			}
		}
		if (!Convert(ref object_0))
		{
			if (Application.isPlaying)
			{
				UnityEngine.Debug.LogError(string.Concat("Unable to convert ", object_0.GetType(), " to ", GetPropertyType()));
			}
		}
		else
		{
			if (fieldInfo_0 != null)
			{
				fieldInfo_0.SetValue(component_0, object_0);
				return true;
			}
			if (propertyInfo_0.CanWrite)
			{
				propertyInfo_0.SetValue(component_0, object_0, null);
				return true;
			}
		}
		return false;
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	private bool Cache()
	{
		if (component_0 != null && !string.IsNullOrEmpty(string_0))
		{
			Type type = component_0.GetType();
			fieldInfo_0 = type.GetField(string_0);
			propertyInfo_0 = type.GetProperty(string_0);
		}
		else
		{
			fieldInfo_0 = null;
			propertyInfo_0 = null;
		}
		return fieldInfo_0 != null || propertyInfo_0 != null;
	}

	private bool Convert(ref object object_0)
	{
		if (component_0 == null)
		{
			return false;
		}
		Type propertyType = GetPropertyType();
		Type type_;
		if (object_0 == null)
		{
			if (!propertyType.IsClass)
			{
				return false;
			}
			type_ = propertyType;
		}
		else
		{
			type_ = object_0.GetType();
		}
		return Convert(ref object_0, type_, propertyType);
	}

	public static bool Convert(Type type_0, Type type_1)
	{
		object object_ = null;
		return Convert(ref object_, type_0, type_1);
	}

	public static bool Convert(object object_0, Type type_0)
	{
		if (object_0 == null)
		{
			object_0 = null;
			return Convert(ref object_0, type_0, type_0);
		}
		return Convert(ref object_0, object_0.GetType(), type_0);
	}

	public static bool Convert(ref object object_0, Type type_0, Type type_1)
	{
		if (type_1.IsAssignableFrom(type_0))
		{
			return true;
		}
		if (type_1 == typeof(string))
		{
			object_0 = ((object_0 == null) ? "null" : object_0.ToString());
			return true;
		}
		if (object_0 == null)
		{
			return false;
		}
		float result2;
		if (type_1 == typeof(int))
		{
			if (type_0 == typeof(string))
			{
				int result;
				if (int.TryParse((string)object_0, out result))
				{
					object_0 = result;
					return true;
				}
			}
			else if (type_0 == typeof(float))
			{
				object_0 = Mathf.RoundToInt((float)object_0);
				return true;
			}
		}
		else if (type_1 == typeof(float) && type_0 == typeof(string) && float.TryParse((string)object_0, out result2))
		{
			object_0 = result2;
			return true;
		}
		return false;
	}
}
