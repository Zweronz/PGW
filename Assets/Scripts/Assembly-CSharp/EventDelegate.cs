using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[Serializable]
public class EventDelegate
{
	[Serializable]
	public class Parameter
	{
		public UnityEngine.Object obj;

		public string field;

		[NonSerialized]
		public Type expectedType = typeof(void);

		[NonSerialized]
		public bool cached;

		[NonSerialized]
		public PropertyInfo propInfo;

		[NonSerialized]
		public FieldInfo fieldInfo;

		public object Object_0
		{
			get
			{
				if (!cached)
				{
					cached = true;
					fieldInfo = null;
					propInfo = null;
					if (obj != null && !string.IsNullOrEmpty(field))
					{
						Type type = obj.GetType();
						propInfo = type.GetProperty(field);
						if (propInfo == null)
						{
							fieldInfo = type.GetField(field);
						}
					}
				}
				if (propInfo != null)
				{
					return propInfo.GetValue(obj, null);
				}
				if (fieldInfo != null)
				{
					return fieldInfo.GetValue(obj);
				}
				return obj;
			}
		}

		public Type Type_0
		{
			get
			{
				if (obj == null)
				{
					return typeof(void);
				}
				return obj.GetType();
			}
		}

		public Parameter()
		{
		}

		public Parameter(UnityEngine.Object object_0, string string_0)
		{
			obj = object_0;
			field = string_0;
		}
	}

	public delegate void Callback();

	[SerializeField]
	private MonoBehaviour monoBehaviour_0;

	[SerializeField]
	private string string_0;

	[SerializeField]
	private Parameter[] parameter_0;

	public bool oneShot;

	[NonSerialized]
	private Callback callback_0;

	[NonSerialized]
	private bool bool_0;

	[NonSerialized]
	private bool bool_1;

	[NonSerialized]
	private MethodInfo methodInfo_0;

	[NonSerialized]
	private object[] object_0;

	private static int int_0 = "EventDelegate".GetHashCode();

	public MonoBehaviour MonoBehaviour_0
	{
		get
		{
			return monoBehaviour_0;
		}
		set
		{
			monoBehaviour_0 = value;
			callback_0 = null;
			bool_0 = false;
			bool_1 = false;
			methodInfo_0 = null;
			parameter_0 = null;
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
			callback_0 = null;
			bool_0 = false;
			bool_1 = false;
			methodInfo_0 = null;
			parameter_0 = null;
		}
	}

	public Parameter[] Parameter_0
	{
		get
		{
			if (!bool_1)
			{
				Cache();
			}
			return parameter_0;
		}
	}

	public bool Boolean_0
	{
		get
		{
			if (!bool_1)
			{
				Cache();
			}
			return (bool_0 && callback_0 != null) || (monoBehaviour_0 != null && !string.IsNullOrEmpty(string_0));
		}
	}

	public bool Boolean_1
	{
		get
		{
			if (!bool_1)
			{
				Cache();
			}
			if (bool_0 && callback_0 != null)
			{
				return true;
			}
			if (monoBehaviour_0 == null)
			{
				return false;
			}
			MonoBehaviour monoBehaviour = monoBehaviour_0;
			return monoBehaviour == null || monoBehaviour.enabled;
		}
	}

	public EventDelegate()
	{
	}

	public EventDelegate(Callback callback_1)
	{
		Set(callback_1);
	}

	public EventDelegate(MonoBehaviour monoBehaviour_1, string string_1)
	{
		Set(monoBehaviour_1, string_1);
	}

	private static string GetMethodName(Callback callback_1)
	{
		return callback_1.Method.Name;
	}

	private static bool IsValid(Callback callback_1)
	{
		return callback_1 != null && callback_1.Method != null;
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return !Boolean_0;
		}
		if (obj is Callback)
		{
			Callback callback = obj as Callback;
			if (callback.Equals(callback_0))
			{
				return true;
			}
			MonoBehaviour monoBehaviour = callback.Target as MonoBehaviour;
			return monoBehaviour_0 == monoBehaviour && string.Equals(string_0, GetMethodName(callback));
		}
		if (obj is EventDelegate)
		{
			EventDelegate eventDelegate = obj as EventDelegate;
			return monoBehaviour_0 == eventDelegate.monoBehaviour_0 && string.Equals(string_0, eventDelegate.string_0);
		}
		return false;
	}

	public override int GetHashCode()
	{
		return int_0;
	}

	private void Set(Callback callback_1)
	{
		Clear();
		if (callback_1 != null && IsValid(callback_1))
		{
			monoBehaviour_0 = callback_1.Target as MonoBehaviour;
			if (monoBehaviour_0 == null)
			{
				bool_0 = true;
				callback_0 = callback_1;
				string_0 = null;
			}
			else
			{
				string_0 = GetMethodName(callback_1);
				bool_0 = false;
			}
		}
	}

	public void Set(MonoBehaviour monoBehaviour_1, string string_1)
	{
		Clear();
		monoBehaviour_0 = monoBehaviour_1;
		string_0 = string_1;
	}

	private void Cache()
	{
		bool_1 = true;
		if (bool_0 || (callback_0 != null && !(callback_0.Target as MonoBehaviour != monoBehaviour_0) && !(GetMethodName(callback_0) != string_0)) || !(monoBehaviour_0 != null) || string.IsNullOrEmpty(string_0))
		{
			return;
		}
		Type type = monoBehaviour_0.GetType();
		methodInfo_0 = null;
		while (type != null)
		{
			try
			{
				methodInfo_0 = type.GetMethod(string_0, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (methodInfo_0 != null)
				{
					break;
				}
			}
			catch (Exception)
			{
			}
			type = type.BaseType;
		}
		if (methodInfo_0 == null)
		{
			Debug.LogError("Could not find method '" + string_0 + "' on " + monoBehaviour_0.GetType(), monoBehaviour_0);
			return;
		}
		if (methodInfo_0.ReturnType != typeof(void))
		{
			Debug.LogError(string.Concat(monoBehaviour_0.GetType(), ".", string_0, " must have a 'void' return type."), monoBehaviour_0);
			return;
		}
		ParameterInfo[] parameters = methodInfo_0.GetParameters();
		if (parameters.Length == 0)
		{
			callback_0 = (Callback)Delegate.CreateDelegate(typeof(Callback), monoBehaviour_0, string_0);
			object_0 = null;
			parameter_0 = null;
			return;
		}
		callback_0 = null;
		if (parameter_0 == null || parameter_0.Length != parameters.Length)
		{
			parameter_0 = new Parameter[parameters.Length];
			int i = 0;
			for (int num = parameter_0.Length; i < num; i++)
			{
				parameter_0[i] = new Parameter();
			}
		}
		int j = 0;
		for (int num2 = parameter_0.Length; j < num2; j++)
		{
			parameter_0[j].expectedType = parameters[j].ParameterType;
		}
	}

	public bool Execute()
	{
		if (!bool_1)
		{
			Cache();
		}
		if (callback_0 != null)
		{
			callback_0();
			return true;
		}
		if (methodInfo_0 != null)
		{
			if (parameter_0 == null || parameter_0.Length == 0)
			{
				methodInfo_0.Invoke(monoBehaviour_0, null);
			}
			else
			{
				if (object_0 == null || object_0.Length != parameter_0.Length)
				{
					object_0 = new object[parameter_0.Length];
				}
				int i = 0;
				for (int num = parameter_0.Length; i < num; i++)
				{
					object_0[i] = parameter_0[i].Object_0;
				}
				try
				{
					methodInfo_0.Invoke(monoBehaviour_0, object_0);
				}
				catch (ArgumentException ex)
				{
					string text = "Error calling ";
					if (monoBehaviour_0 == null)
					{
						text += methodInfo_0.Name;
					}
					else
					{
						string text2 = text;
						text = string.Concat(text2, monoBehaviour_0.GetType(), ".", methodInfo_0.Name);
					}
					text = text + ": " + ex.Message;
					text += "\n  Expected: ";
					ParameterInfo[] parameters = methodInfo_0.GetParameters();
					if (parameters.Length == 0)
					{
						text += "no arguments";
					}
					else
					{
						text += parameters[0];
						for (int j = 1; j < parameters.Length; j++)
						{
							text = text + ", " + parameters[j].ParameterType;
						}
					}
					text += "\n  Received: ";
					if (parameter_0.Length == 0)
					{
						text += "no arguments";
					}
					else
					{
						text += parameter_0[0].Type_0;
						for (int k = 1; k < parameter_0.Length; k++)
						{
							text = text + ", " + parameter_0[k].Type_0;
						}
					}
					text += "\n";
					Debug.LogError(text);
				}
				int l = 0;
				for (int num2 = object_0.Length; l < num2; l++)
				{
					object_0[l] = null;
				}
			}
			return true;
		}
		return false;
	}

	public void Clear()
	{
		monoBehaviour_0 = null;
		string_0 = null;
		bool_0 = false;
		callback_0 = null;
		parameter_0 = null;
		bool_1 = false;
		methodInfo_0 = null;
		object_0 = null;
	}

	public override string ToString()
	{
		if (monoBehaviour_0 != null)
		{
			string text = monoBehaviour_0.GetType().ToString();
			int num = text.LastIndexOf('.');
			if (num > 0)
			{
				text = text.Substring(num + 1);
			}
			if (!string.IsNullOrEmpty(String_0))
			{
				return text + "/" + String_0;
			}
			return text + "/[delegate]";
		}
		return (!bool_0) ? null : "[delegate]";
	}

	public static void Execute(List<EventDelegate> list_0)
	{
		if (list_0 == null)
		{
			return;
		}
		int num = 0;
		while (num < list_0.Count)
		{
			EventDelegate eventDelegate = list_0[num];
			if (eventDelegate != null)
			{
				eventDelegate.Execute();
				if (num >= list_0.Count)
				{
					break;
				}
				if (list_0[num] != eventDelegate)
				{
					continue;
				}
				if (eventDelegate.oneShot)
				{
					list_0.RemoveAt(num);
					continue;
				}
			}
			num++;
		}
	}

	public static bool IsValid(List<EventDelegate> list_0)
	{
		if (list_0 != null)
		{
			int i = 0;
			for (int count = list_0.Count; i < count; i++)
			{
				EventDelegate eventDelegate = list_0[i];
				if (eventDelegate != null && eventDelegate.Boolean_0)
				{
					return true;
				}
			}
		}
		return false;
	}

	public static EventDelegate Set(List<EventDelegate> list_0, Callback callback_1)
	{
		if (list_0 != null)
		{
			EventDelegate eventDelegate = new EventDelegate(callback_1);
			list_0.Clear();
			list_0.Add(eventDelegate);
			return eventDelegate;
		}
		return null;
	}

	public static void Set(List<EventDelegate> list_0, EventDelegate eventDelegate_0)
	{
		if (list_0 != null)
		{
			list_0.Clear();
			list_0.Add(eventDelegate_0);
		}
	}

	public static EventDelegate Add(List<EventDelegate> list_0, Callback callback_1)
	{
		return Add(list_0, callback_1, false);
	}

	public static EventDelegate Add(List<EventDelegate> list_0, Callback callback_1, bool bool_2)
	{
		if (list_0 != null)
		{
			int num = 0;
			int count = list_0.Count;
			EventDelegate eventDelegate;
			while (true)
			{
				if (num < count)
				{
					eventDelegate = list_0[num];
					if (eventDelegate != null && eventDelegate.Equals(callback_1))
					{
						break;
					}
					num++;
					continue;
				}
				EventDelegate eventDelegate2 = new EventDelegate(callback_1);
				eventDelegate2.oneShot = bool_2;
				list_0.Add(eventDelegate2);
				return eventDelegate2;
			}
			return eventDelegate;
		}
		Debug.LogWarning("Attempting to add a callback to a list that's null");
		return null;
	}

	public static void Add(List<EventDelegate> list_0, EventDelegate eventDelegate_0)
	{
		Add(list_0, eventDelegate_0, eventDelegate_0.oneShot);
	}

	public static void Add(List<EventDelegate> list_0, EventDelegate eventDelegate_0, bool bool_2)
	{
		if (!eventDelegate_0.bool_0 && !(eventDelegate_0.MonoBehaviour_0 == null) && !string.IsNullOrEmpty(eventDelegate_0.String_0))
		{
			if (list_0 != null)
			{
				int num = 0;
				int count = list_0.Count;
				while (true)
				{
					if (num < count)
					{
						EventDelegate eventDelegate = list_0[num];
						if (eventDelegate == null || !eventDelegate.Equals(eventDelegate_0))
						{
							num++;
							continue;
						}
						break;
					}
					EventDelegate eventDelegate2 = new EventDelegate(eventDelegate_0.MonoBehaviour_0, eventDelegate_0.String_0);
					eventDelegate2.oneShot = bool_2;
					if (eventDelegate_0.parameter_0 != null && eventDelegate_0.parameter_0.Length > 0)
					{
						eventDelegate2.parameter_0 = new Parameter[eventDelegate_0.parameter_0.Length];
						for (int i = 0; i < eventDelegate_0.parameter_0.Length; i++)
						{
							eventDelegate2.parameter_0[i] = eventDelegate_0.parameter_0[i];
						}
					}
					list_0.Add(eventDelegate2);
					break;
				}
			}
			else
			{
				Debug.LogWarning("Attempting to add a callback to a list that's null");
			}
		}
		else
		{
			Add(list_0, eventDelegate_0.callback_0, bool_2);
		}
	}

	public static bool Remove(List<EventDelegate> list_0, Callback callback_1)
	{
		if (list_0 != null)
		{
			int i = 0;
			for (int count = list_0.Count; i < count; i++)
			{
				EventDelegate eventDelegate = list_0[i];
				if (eventDelegate != null && eventDelegate.Equals(callback_1))
				{
					list_0.RemoveAt(i);
					return true;
				}
			}
		}
		return false;
	}

	public static bool Remove(List<EventDelegate> list_0, EventDelegate eventDelegate_0)
	{
		if (list_0 != null)
		{
			int i = 0;
			for (int count = list_0.Count; i < count; i++)
			{
				EventDelegate eventDelegate = list_0[i];
				if (eventDelegate != null && eventDelegate.Equals(eventDelegate_0))
				{
					list_0.RemoveAt(i);
					return true;
				}
			}
		}
		return false;
	}
}
