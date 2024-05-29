using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using ExitGames.Client.Photon;
using ICSharpCode.SharpZipLib.Zip;
using UnityEngine;

namespace engine.helpers
{
	public static class Utility
	{
		public delegate bool TryParseHandler<T>(string string_0, out T gparam_0);

		public const int int_0 = 131097;

		public const int int_1 = 1;

		public const int int_2 = 256;

		public const int int_3 = 512;

		public const int int_4 = 0;

		public const int int_5 = 1;

		public const int int_6 = 2;

		public const int int_7 = 3;

		public const int int_8 = 4;

		public const int int_9 = 5;

		public const int int_10 = 6;

		public const int int_11 = 7;

		public const int int_12 = 8;

		public const int int_13 = 9;

		public const int int_14 = 10;

		public const int int_15 = 11;

		private static DateTime dateTime_0 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		private static double double_0 = 0.0;

		public static readonly IntPtr intptr_0 = new IntPtr(-2147483646);

		public static double Double_0
		{
			get
			{
				return (DateTime.UtcNow - dateTime_0).TotalSeconds + double_0;
			}
		}

		public static string GetErrorLocation(int int_16 = 1, bool bool_0 = false)
		{
			StackTrace stackTrace = new StackTrace();
			string text = string.Empty;
			string text2 = string.Empty;
			for (int num = stackTrace.FrameCount - 1; num > int_16; num--)
			{
				if (num < stackTrace.FrameCount - 1)
				{
					text += " --> ";
				}
				StackFrame frame = stackTrace.GetFrame(num);
				if (frame.GetMethod().DeclaringType.ToString() == text2)
				{
					text = string.Empty;
				}
				text2 = frame.GetMethod().DeclaringType.ToString();
				text = text + text2 + ":" + frame.GetMethod().Name;
			}
			if (bool_0)
			{
				try
				{
					text = text.Substring(text.LastIndexOf(" --> "));
					text = text.Replace(" --> ", string.Empty);
				}
				catch
				{
				}
			}
			return text;
		}

		public static string[] GetAllFiles(string string_0, string string_1, string string_2)
		{
			List<string> list = new List<string>();
			GetAllFiles(list, string_0, string_1, string_2);
			return list.ToArray();
		}

		public static string[] GetAllFiles(string string_0)
		{
			return GetAllFiles(string_0, string.Empty, string.Empty);
		}

		public static void GetAllFiles(List<string> list_0, string string_0, string string_1, string string_2)
		{
			Regex regex_ = (string.IsNullOrEmpty(string_1) ? null : new Regex(string_1));
			Regex regex_2 = (string.IsNullOrEmpty(string_2) ? null : new Regex(string_2));
			GetAllFiles(list_0, string_0, regex_, regex_2);
		}

		public static void GetAllFiles(List<string> list_0, string string_0, Regex regex_0, Regex regex_1)
		{
			if (!Directory.Exists(string_0))
			{
				return;
			}
			string[] files = Directory.GetFiles(string_0);
			foreach (string text in files)
			{
				if ((regex_0 == null || !regex_0.IsMatch(text)) && (regex_1 == null || regex_1.IsMatch(text)))
				{
					list_0.Add(text);
				}
			}
			string[] directories = Directory.GetDirectories(string_0);
			for (int j = 0; j < directories.Length; j++)
			{
				GetAllFiles(list_0, directories[j], regex_0, regex_1);
			}
		}

		public static string[] GetAllFiles(string string_0, string[] string_1, string[] string_2)
		{
			List<string> list = new List<string>();
			GetAllFiles(list, string_0, string_1, string_2);
			return list.ToArray();
		}

		public static void GetAllFiles(List<string> list_0, string string_0, string[] string_1, string[] string_2)
		{
			if (!Directory.Exists(string_0))
			{
				return;
			}
			string[] files = Directory.GetFiles(string_0);
			foreach (string string_3 in files)
			{
				if ((string_1 == null || Array.Find(string_1, (string string_4) => string_3.IndexOf(string_4) != -1) == null) && (string_2 == null || Array.Find(string_2, (string string_4) => string_3.IndexOf(string_4) != -1) != null))
				{
					list_0.Add(string_3);
				}
			}
			string[] directories = Directory.GetDirectories(string_0);
			for (int j = 0; j < directories.Length; j++)
			{
				GetAllFiles(list_0, directories[j], string_1, string_2);
			}
		}

		public static void WriteAllBytes(string string_0, byte[] byte_0)
		{
			File.WriteAllBytes(string_0, byte_0);
		}

		public static string ReadAllText(string string_0)
		{
			byte[] array = ReadAllBytes(string_0);
			return Encoding.UTF8.GetString(array, 0, array.Length);
		}

		public static void WriteAllText(string string_0, string string_1)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(string_1);
			WriteAllBytes(string_0, bytes);
		}

		public static byte[] ReadAllBytes(string string_0)
		{
			return File.ReadAllBytes(string_0);
		}

		public static void CreateDirectoryForFile(string string_0)
		{
			string directoryName = Path.GetDirectoryName(string_0);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
		}

		public static void DeleteAllFiles(string string_0)
		{
			DeleteAllFiles(string_0, null, null);
		}

		public static void DeleteAllFiles(string string_0, string string_1, string string_2)
		{
			if (Directory.Exists(string_0))
			{
				string[] allFiles = GetAllFiles(string_0, string_1, string_2);
				for (int i = 0; i < allFiles.Length; i++)
				{
					File.Delete(allFiles[i]);
				}
				DeleteEmptyFolders(string_0);
			}
		}

		public static void DeleteEmptyFolders(string string_0)
		{
			string[] directories = Directory.GetDirectories(string_0);
			string[] files = Directory.GetFiles(string_0);
			if (directories.Length == 0)
			{
				if (files.Length == 0)
				{
					Directory.Delete(string_0);
				}
				return;
			}
			for (int i = 0; i < directories.Length; i++)
			{
				DeleteEmptyFolders(directories[i]);
			}
			directories = Directory.GetDirectories(string_0);
			if (directories.Length == 0 && files.Length == 0)
			{
				Directory.Delete(string_0);
			}
		}

		public static void DeleteDirectory(string string_0)
		{
			if (Directory.Exists(string_0))
			{
				string[] files = Directory.GetFiles(string_0);
				string[] directories = Directory.GetDirectories(string_0);
				string[] array = files;
				foreach (string path in array)
				{
					File.SetAttributes(path, FileAttributes.Normal);
					File.Delete(path);
				}
				string[] array2 = directories;
				foreach (string string_ in array2)
				{
					DeleteDirectory(string_);
				}
				Directory.Delete(string_0, false);
			}
		}

		public static void MovingDirectoryAndFiles(string string_0, string string_1, bool bool_0 = false)
		{
			string[] directories = Directory.GetDirectories(string_0);
			foreach (string text in directories)
			{
				if (bool_0)
				{
					Log.AddLine(string.Format("[Utilites. Move dir from]: {0}", text));
				}
				string text2 = Path.Combine(string_1, Path.GetFileName(text));
				if (bool_0)
				{
					Log.AddLine(string.Format("[Utilites. Move dir to]: {0}", text2));
				}
				if (Directory.Exists(text2))
				{
					DeleteDirectory(text2);
				}
				Directory.Move(text, text2);
			}
			string[] files = Directory.GetFiles(string_0);
			foreach (string text3 in files)
			{
				if (bool_0)
				{
					Log.AddLine(string.Format("[Utilites. Move file from]: {0}", text3));
				}
				string text4 = Path.Combine(string_1, Path.GetFileName(text3));
				if (bool_0)
				{
					Log.AddLine(string.Format("[Utilites. Move file to]: {0}", text4));
				}
				if (File.Exists(text4))
				{
					File.Delete(text4);
				}
				File.Move(text3, text4);
			}
		}

		public static void CopyDirectoryAndFiles(string string_0, string string_1, bool bool_0 = true, bool bool_1 = false)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(string_0);
			DirectoryInfo[] directories = directoryInfo.GetDirectories();
			if (!directoryInfo.Exists)
			{
				Log.AddLine(string.Format("[Utilites. Source directory does not exist or could not be found]: {0}", string_0), Log.LogLevel.ERROR);
				return;
			}
			if (!Directory.Exists(string_1))
			{
				if (bool_1)
				{
					Log.AddLine(string.Format("[Utilites. Create dest dir]: {0}", string_1));
				}
				Directory.CreateDirectory(string_1);
			}
			string empty = string.Empty;
			FileInfo[] files = directoryInfo.GetFiles();
			FileInfo[] array = files;
			foreach (FileInfo fileInfo in array)
			{
				if (bool_1)
				{
					Log.AddLine(string.Format("[Utilites. Copy file from]: {0}", fileInfo.FullName));
				}
				empty = Path.Combine(string_1, fileInfo.Name);
				if (bool_1)
				{
					Log.AddLine(string.Format("[Utilites. Copy file to]: {0}", empty));
				}
				fileInfo.CopyTo(empty, true);
			}
			if (!bool_0)
			{
				return;
			}
			DirectoryInfo[] array2 = directories;
			foreach (DirectoryInfo directoryInfo2 in array2)
			{
				if (bool_1)
				{
					Log.AddLine(string.Format("[Utilites. Copy dir from]: {0}", directoryInfo2.FullName));
				}
				empty = Path.Combine(string_1, directoryInfo2.Name);
				if (bool_1)
				{
					Log.AddLine(string.Format("[Utilites. Move dir to]: {0}", empty));
				}
				CopyDirectoryAndFiles(directoryInfo2.FullName, empty, bool_0);
			}
		}

		public static byte[] LoadCompressedApplicationLogFile()
		{
			string text = Path.Combine(Application.dataPath, "output_log.txt");
			string text2 = Path.Combine(Application.dataPath, "output_log_copy.txt");
			if (!File.Exists(text))
			{
				return null;
			}
			if (File.Exists(text2))
			{
				try
				{
					File.Delete(text2);
				}
				catch (Exception ex)
				{
					throw new IOException("[BugReportController::LoadGameLogFile. Error remove old copy game log file! Error]: " + ex.Message);
				}
			}
			FileInfo fileInfo = new FileInfo(text);
			fileInfo.CopyTo(text2);
			byte[] array;
			using (FileStream fileStream = new FileStream(text2, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				int num = 0;
				long length = fileStream.Length;
				if (length > 2147483647L)
				{
					throw new IOException("[BugReportController::LoadGameLogFile. Game log file too long]");
				}
				int num2 = (int)length;
				array = new byte[num2];
				while (num2 > 0)
				{
					int num3 = fileStream.Read(array, num, num2);
					if (num3 != 0)
					{
						num += num3;
						num2 -= num3;
						continue;
					}
					throw new EndOfStreamException("[BugReportController::LoadGameLogFile. Read beyond end of game log file.]");
				}
			}
			if (array.Length == 0)
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream();
			ZipOutputStream zipOutputStream = new ZipOutputStream(memoryStream);
			zipOutputStream.SetLevel(9);
			ZipEntry zipEntry = new ZipEntry("output_log.txt");
			zipEntry.DateTime = DateTime.Now;
			zipOutputStream.PutNextEntry(zipEntry);
			zipOutputStream.Write(array, 0, array.Length);
			zipOutputStream.CloseEntry();
			zipOutputStream.IsStreamOwner = false;
			zipOutputStream.Close();
			return memoryStream.ToArray();
		}

		public static void setTime(double double_1)
		{
			double totalSeconds = (DateTime.UtcNow - dateTime_0).TotalSeconds;
			double_0 = double_1 - totalSeconds;
		}

		public static string GetLocalizedTime(int int_16, string string_0, string string_1, string string_2, string string_3, bool bool_0 = true)
		{
			if (int_16 < 0)
			{
				int_16 = 0;
			}
			int num = int_16 / 86400;
			int_16 -= num * 86400;
			int num2 = int_16 / 3600;
			int_16 -= num2 * 3600;
			int num3 = int_16 / 60;
			int num4 = int_16 % 60;
			string text = string.Empty;
			string text2 = string.Format("{0}{1} ", num, string_0);
			string text3 = string.Format("{0}{1} ", num2, string_1);
			string text4 = string.Format("{0}{1} ", num3, string_2);
			string text5 = string.Format("{0}{1} ", num4, string_3);
			if (num > 0)
			{
				text += (bool_0 ? (text2 + text3 + text4) : text2);
				if (bool_0)
				{
					return text;
				}
			}
			if (num2 > 0)
			{
				text += (bool_0 ? (text3 + text4 + text5) : text3);
				if (bool_0)
				{
					return text;
				}
			}
			if (num3 > 0)
			{
				text += (bool_0 ? (text4 + text5) : text4);
				if (bool_0)
				{
					return text;
				}
			}
			if (num4 > 0)
			{
				text += text5;
				if (bool_0)
				{
					return text5;
				}
			}
			return text;
		}

		public static string GetLocalTime(int int_16, string string_0 = "hh:mm:ss tt")
		{
			return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(int_16).ToLocalTime().ToString(string_0);
		}

		public static void KillAllProcess(string string_0)
		{
			if (!string.IsNullOrEmpty(string_0))
			{
				string fileName = Path.GetFileName(string_0);
				Log.AddLine(string.Format("Utilites|KillAllProcess. Start taskkill for {0} process", fileName));
				Process process = new Process();
				process.EnableRaisingEvents = false;
				process.StartInfo.FileName = "taskkill";
				process.StartInfo.Arguments = string.Format("/im \"{0}\" /F", fileName);
				process.Start();
				process.WaitForExit();
			}
		}

		public static Process[] GetProcessByName(string string_0)
		{
			if (string.IsNullOrEmpty(string_0))
			{
				return new Process[0];
			}
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(string_0);
			return Process.GetProcessesByName(fileNameWithoutExtension);
		}

		public static void SetChmod(string string_0, string string_1)
		{
		}

		public static Process StartApplicationProcess(string string_0, string string_1)
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			processStartInfo.WorkingDirectory = string_0;
			Log.AddLine(string.Format("[Utilites|StartApplicationProcess. Start process: {0} witch arguments: {1}]", string_0, string_1));
			processStartInfo.FileName = string_0;
			return Process.Start(string_0, string_1);
		}

		public static void SetLayerRecursive(GameObject gameObject_0, int int_16)
		{
			if (null == gameObject_0)
			{
				return;
			}
			gameObject_0.layer = int_16;
			int childCount = gameObject_0.transform.childCount;
			Transform transform = gameObject_0.transform;
			for (int i = 0; i < childCount; i++)
			{
				Transform child = transform.GetChild(i);
				if (!(null == child))
				{
					SetLayerRecursive(child.gameObject, int_16);
				}
			}
		}

		public static void setAnchorCameraRecursive(Camera camera_0, Transform transform_0)
		{
			if (!(transform_0 == null))
			{
				UIAnchor component = transform_0.gameObject.GetComponent<UIAnchor>();
				if (component != null)
				{
					component.uiCamera = camera_0;
				}
				for (int i = 0; i < transform_0.childCount; i++)
				{
					setAnchorCameraRecursive(camera_0, transform_0.GetChild(i));
				}
			}
		}

		private static bool isChildInStopObjects(Transform transform_0, GameObject[] gameObject_0)
		{
			int num = 0;
			while (true)
			{
				if (num < gameObject_0.Length)
				{
					GameObject o = gameObject_0[num];
					if (transform_0.gameObject.Equals(o))
					{
						break;
					}
					num++;
					continue;
				}
				return false;
			}
			return true;
		}

		public static void SetTextureRecursiveFrom(GameObject gameObject_0, Texture texture_0, GameObject[] gameObject_1 = null, bool bool_0 = true)
		{
			if (!bool_0 && !gameObject_0.activeSelf)
			{
				return;
			}
			Transform transform = gameObject_0.transform;
			int childCount = gameObject_0.transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				Transform child = transform.GetChild(i);
				if (gameObject_1 == null || !isChildInStopObjects(child, gameObject_1))
				{
					if ((bool)child.gameObject.GetComponent<Renderer>() && (bool)child.gameObject.GetComponent<Renderer>().material)
					{
						child.gameObject.GetComponent<Renderer>().material.mainTexture = texture_0;
					}
					if (gameObject_1 == null || !isChildInStopObjects(child, gameObject_1))
					{
						SetTextureRecursiveFrom(child.gameObject, texture_0, gameObject_1, bool_0);
					}
				}
			}
		}

		public static Texture2D CopyTexture(Texture texture_0)
		{
			return CopyTexture((Texture2D)texture_0);
		}

		public static Texture2D CopyTexture(Texture2D texture2D_0)
		{
			Texture2D texture2D = new Texture2D(texture2D_0.width, texture2D_0.height, TextureFormat.RGBA32, false);
			texture2D.SetPixels(texture2D_0.GetPixels());
			texture2D.filterMode = FilterMode.Point;
			texture2D.Apply();
			return texture2D;
		}

		public static Texture2D TextureFromRect(Texture2D texture2D_0, Rect rect_0)
		{
			Color[] pixels = texture2D_0.GetPixels((int)rect_0.x, (int)rect_0.y, (int)rect_0.width, (int)rect_0.height);
			Texture2D texture2D = new Texture2D((int)rect_0.width, (int)rect_0.height);
			texture2D.filterMode = FilterMode.Point;
			texture2D.SetPixels(pixels);
			texture2D.Apply();
			return texture2D;
		}

		public static Texture2D TextureFromData(byte[] byte_0, int int_16, int int_17)
		{
			Texture2D texture2D = new Texture2D(int_16, int_17, TextureFormat.ARGB32, false);
			texture2D.LoadImage(byte_0);
			texture2D.filterMode = FilterMode.Point;
			texture2D.Apply();
			return texture2D;
		}

		public static string ColorToHex(Color32 color32_0)
		{
			return color32_0.r.ToString("X2") + color32_0.g.ToString("X2") + color32_0.b.ToString("X2");
		}

		public static Color HexToColor(string string_0)
		{
			byte r = byte.Parse(string_0.Substring(0, 2), NumberStyles.HexNumber);
			byte g = byte.Parse(string_0.Substring(2, 2), NumberStyles.HexNumber);
			byte b = byte.Parse(string_0.Substring(4, 2), NumberStyles.HexNumber);
			return new Color32(r, g, b, byte.MaxValue);
		}

		public static void SetShaderRecursive(Transform transform_0, Shader shader_0)
		{
			IgnoreApplyRecursiveParametrs component = transform_0.GetComponent<IgnoreApplyRecursiveParametrs>();
			if (component != null && component.IgnoreShader)
			{
				return;
			}
			SkinnedMeshRenderer component2 = transform_0.GetComponent<SkinnedMeshRenderer>();
			MeshRenderer component3 = transform_0.GetComponent<MeshRenderer>();
			if (component2 != null && component2.material.shader != shader_0)
			{
				component2.material.shader = shader_0;
			}
			else if (component3 != null && component3.material.shader != shader_0)
			{
				component3.material.shader = shader_0;
			}
			foreach (Transform item in transform_0)
			{
				SetShaderRecursive(item, shader_0);
			}
		}

		public static void SetAlphaToGameObject(GameObject gameObject_0, float float_0)
		{
			for (int i = 0; i < gameObject_0.transform.childCount; i++)
			{
				Material[] array = null;
				SkinnedMeshRenderer skinnedMeshRenderer = gameObject_0.transform.GetChild(i).GetComponent<SkinnedMeshRenderer>();
				if (skinnedMeshRenderer == null)
				{
					SkinnedMeshRenderer[] componentsInChildren = gameObject_0.transform.GetChild(i).GetComponentsInChildren<SkinnedMeshRenderer>(true);
					if (componentsInChildren != null && componentsInChildren.Length > 0)
					{
						skinnedMeshRenderer = componentsInChildren[0];
					}
				}
				if (skinnedMeshRenderer != null)
				{
					array = skinnedMeshRenderer.materials;
				}
				else
				{
					MeshRenderer component = gameObject_0.transform.GetChild(i).GetComponent<MeshRenderer>();
					if (component != null)
					{
						array = component.materials;
					}
				}
				if (array != null)
				{
					for (int j = 0; j < array.Length; j++)
					{
						array[j].shader = Shader.Find("Custom/PGW/MobileDiffuse");
						array[j].SetFloat("_Alpha", float_0);
					}
				}
			}
		}

		public static T GetCustomProperties<T>(this Hashtable hashtable_0, string string_0, TryParseHandler<T> tryParseHandler_0, [Optional] T gparam_0)
		{
			if (hashtable_0 != null && tryParseHandler_0 != null)
			{
				object value = null;
				if (!hashtable_0.TryGetValue(string_0, out value))
				{
					return gparam_0;
				}
				if (value == null)
				{
					return gparam_0;
				}
				T gparam_ = gparam_0;
				if (!tryParseHandler_0(value.ToString(), out gparam_))
				{
					return gparam_0;
				}
				return gparam_;
			}
			return gparam_0;
		}

		[DllImport("Advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegOpenKeyExW(IntPtr intptr_1, [In] string string_0, int int_16, int int_17, out IntPtr intptr_2);

		[DllImport("Advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegQueryValueExW(IntPtr intptr_1, [In] string string_0, IntPtr intptr_2, out int int_16, [Out] byte[] byte_0, ref int int_17);

		[DllImport("advapi32.dll")]
		private static extern int RegCloseKey(IntPtr intptr_1);

		public static string GetMachineGuid()
		{
			int error;
			IntPtr intptr_;
			if ((error = RegOpenKeyExW(intptr_0, "Software\\Microsoft\\Cryptography", 0, 131353, out intptr_)) != 0)
			{
				throw new Win32Exception(error);
			}
			try
			{
				return RegQueryValue(intptr_, "MachineGuid").ToString();
			}
			finally
			{
				RegCloseKey(intptr_);
			}
		}

		private static object RegQueryValue(IntPtr intptr_1, string string_0)
		{
			return RegQueryValue(intptr_1, string_0, null);
		}

		private static object RegQueryValue(IntPtr intptr_1, string string_0, object object_0)
		{
			int int_ = 0;
			int num = 65000;
			int int_2 = 65000;
			byte[] array = new byte[65000];
			int num2;
			while ((num2 = RegQueryValueExW(intptr_1, string_0, IntPtr.Zero, out int_, array, ref int_2)) == 234)
			{
				num *= 2;
				int_2 = num;
				array = new byte[num];
			}
			switch (num2)
			{
			case 2:
				return object_0;
			default:
				throw new Win32Exception(num2);
			case 0:
				switch (int_)
				{
				case 1:
					return Encoding.Unicode.GetString(array, 0, int_2);
				case 2:
					return Environment.ExpandEnvironmentVariables(Encoding.Unicode.GetString(array, 0, int_2));
				case 0:
				case 3:
					return array;
				case 4:
					return array[0] | (array[1] << 8) | (array[2] << 16) | (array[3] << 24);
				case 5:
					return array[3] | (array[2] << 8) | (array[1] << 16) | (array[0] << 24);
				case 7:
				{
					List<string> list = new List<string>();
					string @string = Encoding.Unicode.GetString(array, 0, int_2);
					int num5 = 0;
					for (int num6 = @string.IndexOf('\0', 0); num6 > num5; num6 = @string.IndexOf('\0', num5))
					{
						list.Add(@string.Substring(num5, num6 - num5));
						num5 = num6 + 1;
					}
					return list.ToArray();
				}
				default:
					throw new NotSupportedException();
				case 11:
				{
					uint num3 = (uint)(array[0] | (array[1] << 8) | (array[2] << 16) | (array[3] << 24));
					uint num4 = (uint)(array[4] | (array[5] << 8) | (array[6] << 16) | (array[7] << 24));
					return (long)(((ulong)num4 << 32) | num3);
				}
				}
			}
		}

		public static string DeleteBBCode(string string_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			while (num < string_0.Length)
			{
				int num2 = num;
				if (NGUIText.ParseSymbol(string_0, ref num2))
				{
					num = num2;
					continue;
				}
				stringBuilder.Append(string_0[num]);
				num++;
			}
			return stringBuilder.ToString();
		}
	}
}
