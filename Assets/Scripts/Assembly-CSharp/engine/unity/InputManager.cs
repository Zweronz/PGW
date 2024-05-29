using System;
using System.Collections.Generic;
using Rilisoft.MiniJson;
using UnityEngine;
using engine.data;
using engine.events;

namespace engine.unity
{
	public static class InputManager
	{
		public static class Action
		{
			public const string string_0 = "Weapon1";

			public const string string_1 = "Weapon2";

			public const string string_2 = "Weapon3";

			public const string string_3 = "Weapon4";

			public const string string_4 = "Weapon5";

			public const string string_5 = "Weapon6";

			public const string string_6 = "Option1";

			public const string string_7 = "Option2";

			public const string string_8 = "Option3";

			public const string string_9 = "Option4";

			public const string string_10 = "Option5";

			public const string string_11 = "AddAmmo";

			public const string string_12 = "AddHealth";

			public const string string_13 = "Grenade";

			public const string string_14 = "Attack";

			public const string string_15 = "Zoom";

			public const string string_16 = "Reload";

			public const string string_17 = "Jump";

			public const string string_18 = "Sprint";

			public const string string_19 = "Seat";

			public const string string_20 = "Crawling";

			public const string string_21 = "WeaponChange";

			public const string string_22 = "Chat";

			public const string string_23 = "Tab";

			public const string string_24 = "Next";

			public const string string_25 = "Accept";

			public const string string_26 = "Back";

			public const string string_27 = "Vertical";

			public const string string_28 = "Horizontal";

			public const string string_29 = "Mouse X";

			public const string string_30 = "Mouse Y";

			public const string string_31 = "Mouse ScrollWheel";

			public const string string_32 = "Cursor";

			public const string string_33 = "ShowBugReport";

			public const string string_34 = "ShowTeamCommands";

			public const string string_35 = "Pause";
		}

		public static class AxisDirection
		{
			public const int int_0 = 0;

			public const int int_1 = 1;

			public const int int_2 = 2;
		}

		public sealed class ButtonState
		{
			public KeyCode keyCode_0;

			public KeyCode keyCode_1;

			public bool bool_0;

			public bool bool_1;

			public override string ToString()
			{
				return string.Format("{0},{1},{2}", keyCode_0, keyCode_1, bool_0);
			}

			public static ButtonState FromString(string string_0)
			{
				if (string.IsNullOrEmpty(string_0))
				{
					return null;
				}
				string[] array = string_0.Split(',');
				if (array.Length < 3)
				{
					return null;
				}
				ButtonState result = null;
				try
				{
					ButtonState buttonState = new ButtonState();
					buttonState.keyCode_0 = (KeyCode)(int)Enum.Parse(typeof(KeyCode), array[0]);
					buttonState.keyCode_1 = (KeyCode)(int)Enum.Parse(typeof(KeyCode), array[1]);
					buttonState.bool_0 = bool.Parse(array[2]);
					result = buttonState;
				}
				catch (Exception ex)
				{
					Debug.LogError(string.Format("[InputManaget|ButtonState|FromString. Error button info deserialize! Error]: {0} : {1}", ex, ex.StackTrace));
				}
				return result;
			}
		}

		public sealed class AxisState
		{
			public KeyCode keyCode_0;

			public KeyCode keyCode_1;

			public KeyCode keyCode_2;

			public KeyCode keyCode_3;

			public bool bool_0;

			public bool bool_1;

			public override string ToString()
			{
				return string.Format("{0},{1},{2},{3},{4}", keyCode_0, keyCode_1, keyCode_2, keyCode_3, bool_0);
			}

			public static AxisState FromString(string string_0)
			{
				if (string.IsNullOrEmpty(string_0))
				{
					return null;
				}
				string[] array = string_0.Split(',');
				if (array.Length < 5)
				{
					return null;
				}
				AxisState result = null;
				try
				{
					AxisState axisState = new AxisState();
					axisState.keyCode_0 = (KeyCode)(int)Enum.Parse(typeof(KeyCode), array[0]);
					axisState.keyCode_1 = (KeyCode)(int)Enum.Parse(typeof(KeyCode), array[1]);
					axisState.keyCode_2 = (KeyCode)(int)Enum.Parse(typeof(KeyCode), array[2]);
					axisState.keyCode_3 = (KeyCode)(int)Enum.Parse(typeof(KeyCode), array[3]);
					axisState.bool_0 = bool.Parse(array[4]);
					result = axisState;
				}
				catch (Exception ex)
				{
					Debug.LogError(string.Format("[InputManaget|AxisState|FromString. Error axis info deserialize! Error]: {0} : {1}", ex, ex.StackTrace));
				}
				return result;
			}
		}

		private static BaseSharedSettings baseSharedSettings_0;

		public static Dictionary<string, ButtonState> dictionary_0;

		private static List<string> list_0;

		private static List<ButtonState> list_1;

		public static Dictionary<string, AxisState> dictionary_1;

		private static List<string> list_2;

		private static List<AxisState> list_3;

		private static Dictionary<string, bool> dictionary_2;

		private static Action<KeyCode> action_0;

		private static List<KeyCode> list_4;

		private static bool Boolean_0
		{
			get
			{
				return WindowController.WindowController_0.Int32_0 > 0;
			}
		}

		private static bool Boolean_1
		{
			get
			{
				return WindowController.WindowController_0.BaseWindow_0 != null && WindowController.WindowController_0.BaseWindow_0.Parameters_0.bool_7;
			}
		}

		static InputManager()
		{
			baseSharedSettings_0 = null;
			dictionary_0 = new Dictionary<string, ButtonState>();
			list_0 = new List<string>();
			list_1 = new List<ButtonState>();
			dictionary_1 = new Dictionary<string, AxisState>();
			list_2 = new List<string>();
			list_3 = new List<AxisState>();
			dictionary_2 = new Dictionary<string, bool>();
			action_0 = null;
			list_4 = new List<KeyCode>
			{
				KeyCode.A,
				KeyCode.B,
				KeyCode.C,
				KeyCode.D,
				KeyCode.E,
				KeyCode.F,
				KeyCode.G,
				KeyCode.H,
				KeyCode.I,
				KeyCode.J,
				KeyCode.K,
				KeyCode.L,
				KeyCode.M,
				KeyCode.N,
				KeyCode.O,
				KeyCode.Q,
				KeyCode.R,
				KeyCode.S,
				KeyCode.T,
				KeyCode.U,
				KeyCode.V,
				KeyCode.W,
				KeyCode.X,
				KeyCode.Y,
				KeyCode.Z,
				KeyCode.Alpha0,
				KeyCode.Alpha1,
				KeyCode.Alpha2,
				KeyCode.Alpha3,
				KeyCode.Alpha4,
				KeyCode.Alpha5,
				KeyCode.Alpha6,
				KeyCode.Alpha7,
				KeyCode.Alpha8,
				KeyCode.Alpha9,
				KeyCode.Mouse0,
				KeyCode.Mouse1,
				KeyCode.Mouse2,
				KeyCode.Mouse3,
				KeyCode.Mouse4,
				KeyCode.Mouse5,
				KeyCode.Mouse6,
				KeyCode.Space,
				KeyCode.LeftAlt,
				KeyCode.RightAlt,
				KeyCode.LeftControl,
				KeyCode.RightControl,
				KeyCode.LeftShift,
				KeyCode.RightShift,
				KeyCode.LeftCommand,
				KeyCode.RightCommand,
				KeyCode.Insert,
				KeyCode.Home,
				KeyCode.End,
				KeyCode.PageDown,
				KeyCode.PageUp,
				KeyCode.UpArrow,
				KeyCode.DownArrow,
				KeyCode.LeftArrow,
				KeyCode.RightArrow
			};
			SetupDefaults(string.Empty);
		}

		public static void Init(BaseSharedSettings baseSharedSettings_1)
		{
			baseSharedSettings_0 = baseSharedSettings_1;
			List<object> value = baseSharedSettings_1.GetValue<List<object>>("InputManagerData");
			if (value == null)
			{
				return;
			}
			Dictionary<string, object> dictionary = Json.Deserialize(value[0] as string) as Dictionary<string, object>;
			foreach (KeyValuePair<string, object> item in dictionary)
			{
				ButtonState value2 = ButtonState.FromString(item.Value as string);
				if (dictionary_0.ContainsKey(item.Key))
				{
					dictionary_0[item.Key] = value2;
				}
				else
				{
					dictionary_0.Add(item.Key, value2);
				}
			}
			dictionary = Json.Deserialize(value[1] as string) as Dictionary<string, object>;
			foreach (KeyValuePair<string, object> item2 in dictionary)
			{
				AxisState value3 = AxisState.FromString(item2.Value as string);
				if (dictionary_1.ContainsKey(item2.Key))
				{
					dictionary_1[item2.Key] = value3;
				}
				else
				{
					dictionary_1.Add(item2.Key, value3);
				}
			}
			dictionary = Json.Deserialize(value[2] as string) as Dictionary<string, object>;
			foreach (KeyValuePair<string, object> item3 in dictionary)
			{
				bool value4 = Convert.ToBoolean(item3.Value);
				if (dictionary_2.ContainsKey(item3.Key))
				{
					dictionary_2[item3.Key] = value4;
				}
				else
				{
					dictionary_2.Add(item3.Key, value4);
				}
			}
		}

		public static void Save()
		{
			List<string> list = new List<string>();
			list.Add(Json.Serialize(dictionary_0));
			list.Add(Json.Serialize(dictionary_1));
			list.Add(Json.Serialize(dictionary_2));
			baseSharedSettings_0.SetValue("InputManagerData", list, true);
		}

		public static void AddButton(string string_0, KeyCode keyCode_0 = KeyCode.None, KeyCode keyCode_1 = KeyCode.None, bool bool_0 = false)
		{
			ButtonState buttonState = null;
			if (list_0.Contains(string_0))
			{
				buttonState = list_1[list_0.IndexOf(string_0)];
				buttonState.keyCode_0 = keyCode_0;
				buttonState.keyCode_1 = keyCode_1;
				buttonState.bool_0 = bool_0;
			}
			else
			{
				list_0.Add(string_0);
				ButtonState buttonState2 = new ButtonState();
				buttonState2.keyCode_0 = keyCode_0;
				buttonState2.keyCode_1 = keyCode_1;
				buttonState2.bool_0 = bool_0;
				buttonState = buttonState2;
				list_1.Add(buttonState);
			}
		}

		public static void SetActiveButton(string string_0, bool bool_0)
		{
			ButtonState value = null;
			if (dictionary_0.TryGetValue(string_0, out value))
			{
				value.bool_1 = !bool_0;
			}
		}

		public static void AddAxis(string string_0, KeyCode keyCode_0 = KeyCode.None, KeyCode keyCode_1 = KeyCode.None, KeyCode keyCode_2 = KeyCode.None, KeyCode keyCode_3 = KeyCode.None, bool bool_0 = false)
		{
			AxisState axisState = null;
			if (list_2.Contains(string_0))
			{
				axisState = list_3[list_2.IndexOf(string_0)];
				axisState.keyCode_0 = keyCode_0;
				axisState.keyCode_1 = keyCode_1;
				axisState.keyCode_2 = keyCode_2;
				axisState.keyCode_3 = keyCode_3;
				axisState.bool_0 = bool_0;
			}
			else
			{
				list_2.Add(string_0);
				AxisState axisState2 = new AxisState();
				axisState2.keyCode_0 = keyCode_0;
				axisState2.keyCode_1 = keyCode_1;
				axisState2.keyCode_2 = keyCode_2;
				axisState2.keyCode_3 = keyCode_3;
				axisState2.bool_0 = bool_0;
				axisState = axisState2;
				list_3.Add(axisState);
			}
		}

		public static void SetActiveAxis(string string_0, bool bool_0)
		{
			AxisState value = null;
			if (dictionary_1.TryGetValue(string_0, out value))
			{
				value.bool_1 = !bool_0;
			}
		}

		public static void AddUnityAxis(string string_0, bool bool_0 = false)
		{
			if (dictionary_2.ContainsKey(string_0))
			{
				dictionary_2[string_0] = bool_0;
			}
			else
			{
				dictionary_2.Add(string_0, bool_0);
			}
		}

		public static void GetAnyButton(Action<KeyCode> action_1)
		{
			if (action_1 != null)
			{
				action_0 = action_1;
				DependSceneEvent<MainUpdate>.GlobalSubscribe(Update);
			}
		}

		public static bool GetButtonAnyState(string string_0)
		{
			ButtonState value = null;
			if (dictionary_0.TryGetValue(string_0, out value))
			{
				if (!value.bool_1 && (!value.bool_0 || !Boolean_0 || Boolean_1))
				{
					return Input.GetKey(value.keyCode_0) || Input.GetKeyDown(value.keyCode_0) || Input.GetKeyUp(value.keyCode_0) || Input.GetKey(value.keyCode_1) || Input.GetKeyDown(value.keyCode_1) || Input.GetKeyUp(value.keyCode_1);
				}
				return false;
			}
			Debug.LogError("\"" + string_0 + "\" is not in Input Manager's Buttons. You must add it for this Button to work.");
			return false;
		}

		public static bool GetButton(string string_0)
		{
			ButtonState value = null;
			if (dictionary_0.TryGetValue(string_0, out value))
			{
				if (!value.bool_1 && (!value.bool_0 || !Boolean_0 || Boolean_1))
				{
					return Input.GetKey(value.keyCode_0) || Input.GetKey(value.keyCode_1);
				}
				return false;
			}
			Debug.LogError("\"" + string_0 + "\" is not in Input Manager's Buttons. You must add it for this Button to work.");
			return false;
		}

		public static bool GetButtonDown(string string_0)
		{
			ButtonState value = null;
			if (dictionary_0.TryGetValue(string_0, out value))
			{
				if (!value.bool_1 && (!value.bool_0 || !Boolean_0 || Boolean_1))
				{
					return Input.GetKeyDown(value.keyCode_0) || Input.GetKeyDown(value.keyCode_1);
				}
				return false;
			}
			Debug.LogError("\"" + string_0 + "\" is not in Input Manager's Buttons. You must add it for this Button to work.");
			return false;
		}

		public static bool GetButtonUp(string string_0)
		{
			ButtonState value = null;
			if (dictionary_0.TryGetValue(string_0, out value))
			{
				if (!value.bool_1 && (!value.bool_0 || !Boolean_0 || Boolean_1))
				{
					return Input.GetKeyUp(value.keyCode_0) || Input.GetKeyUp(value.keyCode_1);
				}
				return false;
			}
			Debug.LogError("\"" + string_0 + "\" is not in Input Manager's Buttons. You must add it for this Button to work.");
			return false;
		}

		public static float GetAxisRaw(string string_0)
		{
			float result = 0f;
			AxisState value = null;
			if (dictionary_1.TryGetValue(string_0, out value))
			{
				if (!value.bool_1 && (!value.bool_0 || !Boolean_0 || Boolean_1))
				{
					if (Input.GetKey(value.keyCode_0) || Input.GetKey(value.keyCode_1))
					{
						result = 1f;
					}
					if (Input.GetKey(value.keyCode_2) || Input.GetKey(value.keyCode_3))
					{
						result = -1f;
					}
					return result;
				}
				return result;
			}
			if (dictionary_2.ContainsKey(string_0))
			{
				if (dictionary_2[string_0] && Boolean_0 && !Boolean_1)
				{
					return result;
				}
				return Input.GetAxisRaw(string_0);
			}
			Debug.LogError("\"" + string_0 + "\" is not in Input Manager's Unity Axis. You must add it for this Axis to work.");
			return result;
		}

		public static float GetAxis(string string_0)
		{
			float result = 0f;
			AxisState value = null;
			if (dictionary_1.TryGetValue(string_0, out value))
			{
				if (!value.bool_1 && (!value.bool_0 || !Boolean_0 || Boolean_1))
				{
					if (Input.GetKey(value.keyCode_0) || Input.GetKey(value.keyCode_1))
					{
						result = 1f;
					}
					if (Input.GetKey(value.keyCode_2) || Input.GetKey(value.keyCode_3))
					{
						result = -1f;
					}
					return result;
				}
				return result;
			}
			if (dictionary_2.ContainsKey(string_0))
			{
				if (dictionary_2[string_0] && Boolean_0 && !Boolean_1)
				{
					return result;
				}
				return Input.GetAxis(string_0);
			}
			Debug.LogError("\"" + string_0 + "\" is not in Input Manager's Unity Axis. You must add it for this Axis to work.");
			return result;
		}

		public static void ChangeButtonKey(string string_0, KeyCode keyCode_0, KeyCode keyCode_1, bool bool_0 = false)
		{
			if (!dictionary_0.ContainsKey(string_0))
			{
				Debug.LogWarning("The Button \"" + string_0 + "\" Doesn't Exist");
				return;
			}
			ButtonState buttonState = null;
			if (bool_0)
			{
				buttonState = list_1[list_0.IndexOf(string_0)];
				buttonState.keyCode_0 = keyCode_0;
				buttonState.keyCode_1 = keyCode_1;
			}
			buttonState = dictionary_0[string_0];
			buttonState.keyCode_0 = keyCode_0;
			buttonState.keyCode_1 = keyCode_1;
		}

		public static void ChangeAxisKey(string string_0, int int_0, KeyCode keyCode_0, KeyCode keyCode_1, bool bool_0 = false)
		{
			if (!dictionary_1.ContainsKey(string_0))
			{
				Debug.LogWarning("The Axis \"" + string_0 + "\" Doesn't Exist");
				return;
			}
			AxisState axisState = null;
			if (bool_0)
			{
				axisState = list_3[list_2.IndexOf(string_0)];
				if (int_0 == 1)
				{
					axisState.keyCode_0 = keyCode_0;
					axisState.keyCode_1 = keyCode_1;
				}
				else if (int_0 == 1)
				{
					axisState.keyCode_2 = keyCode_0;
					axisState.keyCode_3 = keyCode_1;
				}
			}
			axisState = dictionary_1[string_0];
			switch (int_0)
			{
			case 1:
				axisState.keyCode_0 = keyCode_0;
				axisState.keyCode_1 = keyCode_1;
				break;
			case 2:
				axisState.keyCode_2 = keyCode_0;
				axisState.keyCode_3 = keyCode_1;
				break;
			}
		}

		private static void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				DependSceneEvent<MainUpdate>.GlobalUnsubscribe(Update);
				action_0 = null;
				return;
			}
			foreach (KeyCode item in list_4)
			{
				if (Input.GetKeyDown(item))
				{
					Action<KeyCode> action = action_0;
					action_0 = null;
					DependSceneEvent<MainUpdate>.GlobalUnsubscribe(Update);
					action(item);
					break;
				}
			}
		}

		public static void SetupDefaults(string string_0 = "")
		{
			if (string_0 == string.Empty || string_0 == "Buttons")
			{
				list_0.Clear();
				list_1.Clear();
				AddButton("Weapon1", KeyCode.Alpha1, KeyCode.None, true);
				AddButton("Weapon2", KeyCode.Alpha2, KeyCode.None, true);
				AddButton("Weapon3", KeyCode.Alpha3, KeyCode.None, true);
				AddButton("Weapon4", KeyCode.Alpha4, KeyCode.None, true);
				AddButton("Weapon5", KeyCode.Alpha5, KeyCode.None, true);
				AddButton("Weapon6", KeyCode.Alpha6, KeyCode.None, true);
				AddButton("Option1", KeyCode.H, KeyCode.None, true);
				AddButton("Option2", KeyCode.E, KeyCode.None, true);
				AddButton("Option3", KeyCode.F, KeyCode.None, true);
				AddButton("Option4", KeyCode.V, KeyCode.None, true);
				AddButton("Option5", KeyCode.G, KeyCode.None, true);
				AddButton("AddAmmo", KeyCode.Z, KeyCode.None, true);
				AddButton("AddHealth", KeyCode.X, KeyCode.None, true);
				AddButton("Grenade", KeyCode.C, KeyCode.None, true);
				AddButton("Attack", KeyCode.Mouse0, KeyCode.None, true);
				AddButton("Zoom", KeyCode.Mouse1, KeyCode.None, true);
				AddButton("Reload", KeyCode.R, KeyCode.None, true);
				AddButton("Jump", KeyCode.Space, KeyCode.None, true);
				AddButton("Sprint", KeyCode.LeftShift, KeyCode.None, true);
				AddButton("Seat", KeyCode.LeftControl, KeyCode.None, true);
				AddButton("Crawling", KeyCode.LeftAlt, KeyCode.None, true);
				AddButton("Tab", KeyCode.Tab);
				AddButton("Back", KeyCode.Escape);
				AddButton("Next", KeyCode.Space);
				AddButton("Accept", KeyCode.Return, KeyCode.None, true);
				AddButton("Chat", KeyCode.Y, KeyCode.None, true);
				AddButton("WeaponChange", KeyCode.Q, KeyCode.None, true);
				AddButton("Cursor", KeyCode.M);
				AddButton("ShowBugReport", KeyCode.F1);
				AddButton("ShowTeamCommands", KeyCode.F2);
				AddButton("Pause", KeyCode.P);
			}
			if (string_0 == string.Empty || string_0 == "Axis")
			{
				list_2.Clear();
				list_3.Clear();
				AddAxis("Vertical", KeyCode.W, KeyCode.UpArrow, KeyCode.S, KeyCode.DownArrow, true);
				AddAxis("Horizontal", KeyCode.D, KeyCode.RightArrow, KeyCode.A, KeyCode.LeftArrow, true);
			}
			if (string_0 == string.Empty || string_0 == "UnityAxis")
			{
				dictionary_2.Clear();
				AddUnityAxis("Mouse X", true);
				AddUnityAxis("Mouse Y", true);
				AddUnityAxis("Mouse ScrollWheel", true);
			}
			UpdateDictionaries();
		}

		private static void UpdateDictionaries()
		{
			if (Application.isPlaying)
			{
				dictionary_0.Clear();
				for (int i = 0; i < list_0.Count; i++)
				{
					dictionary_0.Add(list_0[i], list_1[i]);
				}
				dictionary_1.Clear();
				for (int j = 0; j < list_2.Count; j++)
				{
					dictionary_1.Add(list_2[j], list_3[j]);
				}
			}
		}
	}
}
