using System;
using System.IO;
using UnityEngine;

namespace Rilisoft
{
	internal sealed class CrashReporter : MonoBehaviour
	{
		private string string_0 = string.Empty;

		private string string_1 = string.Empty;

		private bool bool_0;

		internal void OnGUI()
		{
			float num = ((Screen.dpi != 0f) ? Screen.dpi : 160f);
			if (GUILayout.Button("Simulate exception", GUILayout.Width(1f * num)))
			{
				throw new InvalidOperationException(DateTime.Now.ToString("s"));
			}
			GUILayout.Label("Report path: " + Application.persistentDataPath);
			if (!string.IsNullOrEmpty(string_0))
			{
				bool_0 = GUILayout.Toggle(bool_0, "Show: " + string_1);
				if (bool_0)
				{
					GUILayout.Label(string_0);
				}
			}
		}

		internal void Start()
		{
			if (Debug.isDebugBuild)
			{
				AppDomain.CurrentDomain.UnhandledException += HandleException;
				string[] files = Directory.GetFiles(Application.persistentDataPath, "Report_*.txt", SearchOption.TopDirectoryOnly);
				if (files.Length > 0)
				{
					string path = files[files.Length - 1];
					string_1 = Path.GetFileNameWithoutExtension(path);
					string_0 = File.ReadAllText(path);
				}
			}
			else
			{
				base.enabled = false;
			}
		}

		private static void HandleException(object sender, UnhandledExceptionEventArgs e)
		{
			Exception ex = e.ExceptionObject as Exception;
			if (ex != null)
			{
				string path = string.Format("Report_{0:s}.txt", DateTime.Now).Replace(':', '-');
				string path2 = Path.Combine(Application.persistentDataPath, path);
				File.WriteAllText(path2, ex.ToString());
			}
		}
	}
}
