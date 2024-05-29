using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityThreading;
using engine.events;
using engine.system;
using engine.unity;

namespace engine.helpers
{
	public class Log : MonoSingleton<Log>
	{
		[Flags]
		public enum LogLevel
		{
			WARNING = 1,
			ERROR = 2,
			FATAL = 4,
			INFO = 8
		}

		internal class Record
		{
			public LogLevel logLevel_0 = LogLevel.INFO;

			[CompilerGenerated]
			private bool bool_0;

			[CompilerGenerated]
			private string string_0;

			[CompilerGenerated]
			private List<string> list_0;

			public bool Boolean_0
			{
				[CompilerGenerated]
				get
				{
					return bool_0;
				}
				[CompilerGenerated]
				set
				{
					bool_0 = value;
				}
			}

			public string String_0
			{
				[CompilerGenerated]
				get
				{
					return string_0;
				}
				[CompilerGenerated]
				set
				{
					string_0 = value;
				}
			}

			public List<string> List_0
			{
				[CompilerGenerated]
				get
				{
					return list_0;
				}
				[CompilerGenerated]
				set
				{
					list_0 = value;
				}
			}
		}

		internal class FilterData
		{
			private GUIStyle guistyle_0;

			[CompilerGenerated]
			private bool bool_0;

			[CompilerGenerated]
			private LogLevel logLevel_0;

			[CompilerGenerated]
			private string string_0;

			[CompilerGenerated]
			private Color color_0;

			public bool Boolean_0
			{
				[CompilerGenerated]
				get
				{
					return bool_0;
				}
				[CompilerGenerated]
				set
				{
					bool_0 = value;
				}
			}

			public LogLevel LogLevel_0
			{
				[CompilerGenerated]
				get
				{
					return logLevel_0;
				}
				[CompilerGenerated]
				set
				{
					logLevel_0 = value;
				}
			}

			public string String_0
			{
				[CompilerGenerated]
				get
				{
					return string_0;
				}
				[CompilerGenerated]
				set
				{
					string_0 = value;
				}
			}

			public Color Color_0
			{
				[CompilerGenerated]
				get
				{
					return color_0;
				}
				[CompilerGenerated]
				set
				{
					color_0 = value;
				}
			}

			public GUIStyle GUIStyle_0
			{
				get
				{
					if (guistyle_0 == null)
					{
						guistyle_0 = FilterStyle.GetStyle(MonoSingleton<Log>.Prop_0.LogLevelToColor(LogLevel_0), false, GUI.skin.toggle);
					}
					return guistyle_0;
				}
			}
		}

		internal class FilterStyle
		{
			private static GUIStyle guistyle_0;

			public static GUIStyle GUIStyle_0
			{
				get
				{
					if (guistyle_0 == null)
					{
						guistyle_0 = new GUIStyle(GUI.skin.toggle);
						guistyle_0.fontSize = 10;
					}
					return guistyle_0;
				}
			}

			public static GUIStyle GetStyle(Color32 color32_0, bool bool_0, GUIStyle guistyle_1 = null)
			{
				GUIStyle gUIStyle = ((guistyle_1 != null) ? new GUIStyle(guistyle_1) : new GUIStyle());
				gUIStyle.fontStyle = (bool_0 ? FontStyle.Bold : FontStyle.Normal);
				GUIStyleState normal = gUIStyle.normal;
				Color color = color32_0;
				gUIStyle.onFocused.textColor = color;
				color = color;
				gUIStyle.focused.textColor = color;
				color = color;
				gUIStyle.onActive.textColor = color;
				color = color;
				gUIStyle.active.textColor = color;
				color = color;
				gUIStyle.onHover.textColor = color;
				color = color;
				gUIStyle.hover.textColor = color;
				color = color;
				gUIStyle.onNormal.textColor = color;
				normal.textColor = color;
				return gUIStyle;
			}
		}

		internal int int_0 = 500;

		internal int int_1;

		private Vector2 vector2_0 = Vector2.zero;

		internal string string_0;

		private bool bool_0;

		private Dictionary<LogLevel, Texture2D> dictionary_0 = new Dictionary<LogLevel, Texture2D>();

		private Dictionary<LogLevel, GUIStyle> dictionary_1 = new Dictionary<LogLevel, GUIStyle>();

		private List<FilterData> list_0 = new List<FilterData>();

		internal List<Record> list_1 = new List<Record>();

		private TextEditor textEditor_0 = new TextEditor();

		internal string string_1 = "[DEBUG] ";

		public bool ShowFilters;

		public bool syncLogs = true;

		public bool OpenLogByError;

		private static bool Boolean_0
		{
			get
			{
				return Dispatcher.Dispatcher_3 == Dispatcher.Dispatcher_1;
			}
		}

		public static void AddLineFormat(string string_2, params object[] object_0)
		{
			AddLine(string.Format(string_2, object_0));
		}

		public static void AddLineWarning(string string_2, params object[] object_0)
		{
			AddLine(string.Format(string_2, object_0), LogLevel.WARNING);
		}

		public static void AddLineError(string string_2, params object[] object_0)
		{
			AddLine(string.Format(string_2, object_0), LogLevel.ERROR);
		}

		public static void AddLine(string string_2)
		{
			AddLine(string_2, null, LogLevel.INFO);
		}

		public static void AddLine(string string_2, object object_0)
		{
			AddLine(string_2, object_0, LogLevel.INFO);
		}

		public static void AddLine(string string_2, LogLevel logLevel_0)
		{
			AddLine(string_2, null, logLevel_0);
		}

		public static void AddLine(string string_2, object object_0, LogLevel logLevel_0)
		{
			string string_3 = string.Empty;
			if (Boolean_0)
			{
				AddLineSync(string_2, object_0, logLevel_0, string_3);
			}
			else if (Dispatcher.Dispatcher_3 != null)
			{
				UnityThreadHelper.Dispatcher_0.Dispatch(delegate
				{
					AddLineSync(string_2, object_0, logLevel_0, string_3);
				});
			}
		}

		public static void AddLineSync(string string_2, object object_0, LogLevel logLevel_0, string string_3 = "")
		{
			string_2 = string.Format("[{0}]:{1}", Utility.Double_0, string_2);
			Log prop_ = MonoSingleton<Log>.Prop_0;
			Record record = new Record();
			record.logLevel_0 = logLevel_0;
			record.String_0 = string_2;
			record.List_0 = prop_.ParseObject(object_0);
			Record record2 = record;
			prop_.list_1.Add(record2);
			string_3 = prop_.RemoveLines(string_3, 3);
			prop_.DebugLog(string_2 + "\n" + string_3, logLevel_0);
			if (logLevel_0 == LogLevel.ERROR && prop_.OpenLogByError)
			{
				prop_.Open();
			}
			if (!string.IsNullOrEmpty(MonoSingleton<Log>.Prop_0.string_0))
			{
				File.AppendAllText(contents: (record2.List_0.Count <= 0) ? string.Format("==========\n{0}\n==========\n\n", string_2) : string.Format("==========\n{0}\n{1}\n==========\n\n", string_2, string.Join("\n", record2.List_0.ToArray())), path: MonoSingleton<Log>.Prop_0.string_0);
			}
			prop_.int_1 = prop_.list_1.Count / prop_.int_0;
		}

		public void Toggle()
		{
			bool_0 = !bool_0;
		}

		public void Close()
		{
			bool_0 = false;
		}

		public void Open()
		{
			bool_0 = true;
		}

		public void DumpError(Exception exception_0, bool bool_1 = false)
		{
			AddLine(exception_0.Message, LogLevel.ERROR);
			if (bool_1 && !string.IsNullOrEmpty(exception_0.StackTrace))
			{
				AddLine(exception_0.StackTrace, LogLevel.ERROR);
			}
		}

		private void Update()
		{
		}

		private void OnGUI()
		{
			if (ISystemInfo.ISystemInfo_0.DEVICE_TYPE_0 != 0 && Debug.isDebugBuild && GUI.Button(new Rect(0f, 0f, 72f, 48f), "Toggle Log"))
			{
				Toggle();
			}
			if (!bool_0)
			{
				return;
			}
			GUILayout.BeginVertical();
			LogLevel? logLevel = null;
			if (ShowFilters)
			{
				logLevel = ProcessFilters();
			}
			ProcessTabs();
			vector2_0 = GUILayout.BeginScrollView(vector2_0);
			int num = int_1 * int_0;
			int num2 = num + int_0;
			IEnumerable<Record> enumerable = list_1.Slice(num, num2);
			if (ShowFilters)
			{
				enumerable = Filter(enumerable, logLevel.Value);
			}
			foreach (Record item in enumerable)
			{
				GUIStyle style = LogLevelToGUIStyle(item.logLevel_0);
				bool flag = item.List_0 != null && item.List_0.Count > 1;
				bool boolean_ = item.Boolean_0;
				bool flag2 = flag && boolean_;
				string text = " • ";
				if (flag)
				{
					text = ((!boolean_) ? " ↑ " : " ↓ ");
				}
				bool flag3 = GUILayout.Toggle(item.Boolean_0, text + item.String_0, style, GUILayout.ExpandWidth(false));
				if (item.Boolean_0 != flag3)
				{
					item.Boolean_0 = flag3;
					StringToClipboard(item.String_0);
				}
				if (!flag2)
				{
					continue;
				}
				foreach (string item2 in item.List_0)
				{
					if (flag3 = GUILayout.Toggle(false, item2, style, GUILayout.ExpandWidth(false)))
					{
						if (Input.GetKey(KeyCode.LeftShift))
						{
							StringToClipboard(item2);
						}
						else
						{
							StringToClipboard(string.Join("\n", item.List_0.ToArray()));
						}
					}
				}
			}
			GUILayout.EndScrollView();
			GUILayout.EndVertical();
		}

		protected override void Init()
		{
			UnityEngine.Object.DontDestroyOnLoad(this);
			if (!string.IsNullOrEmpty(string_0))
			{
				File.Delete(string_0);
			}
			int num = 0;
			Array values = Enum.GetValues(typeof(LogLevel));
			foreach (int item in values)
			{
				list_0.Add(new FilterData
				{
					String_0 = ((LogLevel)item).ToString(),
					LogLevel_0 = (LogLevel)item,
					Boolean_0 = (num == 0 || (item & num) == item)
				});
			}
			if (syncLogs)
			{
				DependSceneEvent<LogEvent, LogEventArgs>.GlobalSubscribe(AppHandler);
			}
		}

		private GUIStyle LogLevelToGUIStyle(LogLevel logLevel_0)
		{
			GUIStyle value = null;
			if (dictionary_1.TryGetValue(logLevel_0, out value))
			{
				return value;
			}
			dictionary_1.Add(logLevel_0, new GUIStyle());
			dictionary_1[logLevel_0].normal.background = LogLevelToTexture2D(logLevel_0);
			return dictionary_1[logLevel_0];
		}

		private Texture2D LogLevelToTexture2D(LogLevel logLevel_0)
		{
			Texture2D value = null;
			if (dictionary_0.TryGetValue(logLevel_0, out value))
			{
				return value;
			}
			dictionary_0.Add(logLevel_0, MakeTexture(1, 1, LogLevelToColor(logLevel_0)));
			return dictionary_0[logLevel_0];
		}

		private Color32 LogLevelToColor(LogLevel logLevel_0)
		{
			Color32 result;
			switch (logLevel_0)
			{
			case LogLevel.WARNING:
				result = new Color32(254, 207, 156, byte.MaxValue);
				break;
			case LogLevel.ERROR:
				result = new Color32(byte.MaxValue, 149, 149, byte.MaxValue);
				break;
			case LogLevel.FATAL:
				result = new Color32(byte.MaxValue, 94, 102, byte.MaxValue);
				break;
			default:
				result = new Color32(225, 233, 244, byte.MaxValue);
				break;
			case LogLevel.INFO:
				result = new Color32(204, 230, byte.MaxValue, byte.MaxValue);
				break;
			}
			return result;
		}

		private LogLevel ProcessFilters()
		{
			LogLevel logLevel = (LogLevel)0;
			GUILayout.BeginHorizontal(GUI.skin.box, GUILayout.MaxWidth(1f));
			GUILayout.BeginVertical();
			for (int i = 0; i < list_0.Count; i++)
			{
				if (i % 5 == 0)
				{
					GUILayout.EndVertical();
					GUILayout.BeginVertical();
				}
				FilterData filterData = list_0[i];
				filterData.Boolean_0 = GUILayout.Toggle(filterData.Boolean_0, filterData.String_0, filterData.GUIStyle_0);
				if (filterData.Boolean_0)
				{
					logLevel |= filterData.LogLevel_0;
				}
			}
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
			return logLevel;
		}

		private void ProcessTabs()
		{
			int num = list_1.Count / int_0 + 1;
			if (num <= 1)
			{
				int_1 = 0;
				return;
			}
			GUILayout.BeginHorizontal();
			for (int i = 0; i < num; i++)
			{
				if (GUILayout.Button((i + 1).ToString(), GUILayout.ExpandWidth(false)))
				{
					int_1 = i;
					vector2_0 = Vector3.zero;
				}
			}
			GUILayout.EndHorizontal();
		}

		private IEnumerable<Record> Filter(IEnumerable<Record> ienumerable_0, LogLevel logLevel_0)
		{
			IEnumerator<Record> enumerator = ienumerable_0.GetEnumerator();
			/*Error near IL_0037: Could not find block for branch target IL_0079*/;
			yield break;
		}

		private string ObjectToString(object object_0)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			string[] value = ObjectParser.Parse(object_0, 1, 0, ref dictionary).ToArray();
			return string.Join("\n", value);
		}

		private List<string> ParseObject(object object_0)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			return ObjectParser.Parse(object_0, 1, 0, ref dictionary);
		}

		private void StringToClipboard(string string_2)
		{
			textEditor_0.content = new GUIContent(string_2);
			textEditor_0.SelectAll();
			textEditor_0.Copy();
		}

		private string RemoveLines(string string_2, int int_2)
		{
			string[] array = string_2.Split('\n');
			int num = array.Length - int_2;
			if (num <= 0)
			{
				return string.Empty;
			}
			string[] array2 = new string[num];
			Array.Copy(array, int_2, array2, 0, num);
			return string.Join("\n", array2);
		}

		private void AppHandler(LogEventArgs logEventArgs_0)
		{
			AppHandler(logEventArgs_0.string_0, logEventArgs_0.string_1, logEventArgs_0.logType_0);
		}

		private void AppHandler(string string_2, string string_3, LogType logType_0)
		{
			Log prop_ = MonoSingleton<Log>.Prop_0;
			if (string_2.IndexOf(prop_.string_1) == 0)
			{
				return;
			}
			LogLevel logLevel;
			switch (logType_0)
			{
			default:
				logLevel = LogLevel.ERROR;
				break;
			case LogType.Log:
				logLevel = LogLevel.INFO;
				break;
			case LogType.Warning:
				logLevel = LogLevel.WARNING;
				break;
			}
			if (logLevel == LogLevel.ERROR)
			{
				AddLineSync(string_2, string_3, logLevel, string.Empty);
				if (!bool_0 && OpenLogByError)
				{
					prop_.Open();
				}
			}
		}

		private void DebugLog(string string_2, LogLevel logLevel_0)
		{
			if (syncLogs)
			{
				DependSceneEvent<LogEvent, LogEventArgs>.GlobalUnsubscribe(AppHandler);
			}
			switch (logLevel_0)
			{
			default:
				Debug.Log(string_2);
				break;
			case LogLevel.ERROR:
				Debug.LogError(string_2);
				break;
			case LogLevel.WARNING:
				Debug.LogWarning(string_2);
				break;
			}
			if (syncLogs)
			{
				DependSceneEvent<LogEvent, LogEventArgs>.GlobalSubscribe(AppHandler);
			}
		}

		private Texture2D MakeTexture(int int_2, int int_3, Color color_0)
		{
			Color[] array = new Color[int_2 * int_3];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = color_0;
			}
			Texture2D texture2D = new Texture2D(int_2, int_3);
			texture2D.SetPixels(array);
			texture2D.Apply();
			return texture2D;
		}
	}
}
