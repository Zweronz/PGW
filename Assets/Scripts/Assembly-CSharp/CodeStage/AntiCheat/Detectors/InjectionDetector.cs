using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

namespace CodeStage.AntiCheat.Detectors
{
	[DisallowMultipleComponent]
	public class InjectionDetector : ActDetectorBase
	{
		private class AllowedAssembly
		{
			public readonly string string_0;

			public readonly int[] int_0;

			public AllowedAssembly(string string_1, int[] int_1)
			{
				string_0 = string_1;
				int_0 = int_1;
			}
		}

		private const string string_2 = "Injection Detector";

		internal static bool bool_1;

		private bool bool_2;

		private AllowedAssembly[] allowedAssembly_0;

		private string[] string_3;

		[CompilerGenerated]
		private static InjectionDetector injectionDetector_0;

		public static InjectionDetector InjectionDetector_0
		{
			[CompilerGenerated]
			get
			{
				return injectionDetector_0;
			}
			[CompilerGenerated]
			private set
			{
				injectionDetector_0 = value;
			}
		}

		private static InjectionDetector InjectionDetector_1
		{
			get
			{
				if (InjectionDetector_0 == null)
				{
					InjectionDetector injectionDetector = UnityEngine.Object.FindObjectOfType<InjectionDetector>();
					if (injectionDetector != null)
					{
						InjectionDetector_0 = injectionDetector;
					}
					else
					{
						if (ActDetectorBase.gameObject_0 == null)
						{
							ActDetectorBase.gameObject_0 = new GameObject("Anti-Cheat Toolkit Detectors");
						}
						ActDetectorBase.gameObject_0.AddComponent<InjectionDetector>();
					}
				}
				return InjectionDetector_0;
			}
		}

		private InjectionDetector()
		{
		}

		public static void StartDetection(Action action_1)
		{
			InjectionDetector_1.StartDetectionInternal(action_1);
		}

		public static void StopDetection()
		{
			if (InjectionDetector_0 != null)
			{
				InjectionDetector_0.StopDetectionInternal();
			}
		}

		public static void Dispose()
		{
			if (InjectionDetector_0 != null)
			{
				InjectionDetector_0.DisposeInternal();
			}
		}

		private void Awake()
		{
			if (Init(InjectionDetector_0, "Injection Detector"))
			{
				InjectionDetector_0 = this;
			}
		}

		private void StartDetectionInternal(Action action_1)
		{
			if (bool_1)
			{
				Debug.LogWarning("[ACTk] Injection Detector already running!");
				return;
			}
			if (!base.enabled)
			{
				Debug.LogWarning("[ACTk] Injection Detector disabled but StartDetection still called from somewhere!");
				return;
			}
			action_0 = action_1;
			if (allowedAssembly_0 == null)
			{
				LoadAndParseAllowedAssemblies();
			}
			if (bool_2)
			{
				OnInjectionDetected();
			}
			else if (!FindInjectionInCurrentAssemblies())
			{
				AppDomain.CurrentDomain.AssemblyLoad += OnNewAssemblyLoaded;
				bool_1 = true;
			}
			else
			{
				OnInjectionDetected();
			}
		}

		protected override void StopDetectionInternal()
		{
			if (bool_1)
			{
				AppDomain.CurrentDomain.AssemblyLoad -= OnNewAssemblyLoaded;
				action_0 = null;
				bool_1 = false;
			}
		}

		protected override void PauseDetector()
		{
			bool_1 = false;
			AppDomain.CurrentDomain.AssemblyLoad -= OnNewAssemblyLoaded;
		}

		protected override void ResumeDetector()
		{
			bool_1 = true;
			AppDomain.CurrentDomain.AssemblyLoad += OnNewAssemblyLoaded;
		}

		protected override void DisposeInternal()
		{
			base.DisposeInternal();
			if (InjectionDetector_0 == this)
			{
				InjectionDetector_0 = null;
			}
		}

		private void OnInjectionDetected()
		{
			if (action_0 != null)
			{
				action_0();
			}
			if (autoDispose)
			{
				Dispose();
			}
			else
			{
				StopDetectionInternal();
			}
		}

		private void OnNewAssemblyLoaded(object sender, AssemblyLoadEventArgs e)
		{
			if (!AssemblyAllowed(e.LoadedAssembly))
			{
				OnInjectionDetected();
			}
		}

		private bool FindInjectionInCurrentAssemblies()
		{
			bool result = false;
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			if (assemblies.Length == 0)
			{
				result = true;
			}
			else
			{
				Assembly[] array = assemblies;
				foreach (Assembly assembly_ in array)
				{
					if (!AssemblyAllowed(assembly_))
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		private bool AssemblyAllowed(Assembly assembly_0)
		{
			string text = assembly_0.GetName().Name;
			int assemblyHash = GetAssemblyHash(assembly_0);
			bool result = false;
			for (int i = 0; i < allowedAssembly_0.Length; i++)
			{
				AllowedAssembly allowedAssembly = allowedAssembly_0[i];
				if (allowedAssembly.string_0 == text && Array.IndexOf(allowedAssembly.int_0, assemblyHash) != -1)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		private void LoadAndParseAllowedAssemblies()
		{
			TextAsset textAsset = (TextAsset)Resources.Load("fndid", typeof(TextAsset));
			if (textAsset == null)
			{
				bool_2 = true;
				return;
			}
			string[] separator = new string[1] { ":" };
			MemoryStream memoryStream = new MemoryStream(textAsset.bytes);
			BinaryReader binaryReader = new BinaryReader(memoryStream);
			int num = binaryReader.ReadInt32();
			allowedAssembly_0 = new AllowedAssembly[num];
			int num2 = 0;
			while (true)
			{
				if (num2 < num)
				{
					string text = binaryReader.ReadString();
					text = ObscuredString.EncryptDecrypt(text, "Elina");
					string[] array = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
					int num3 = array.Length;
					if (num3 <= 1)
					{
						break;
					}
					string text2 = array[0];
					int[] array2 = new int[num3 - 1];
					for (int i = 1; i < num3; i++)
					{
						array2[i - 1] = int.Parse(array[i]);
					}
					allowedAssembly_0[num2] = new AllowedAssembly(text2, array2);
					num2++;
					continue;
				}
				binaryReader.Close();
				memoryStream.Close();
				Resources.UnloadAsset(textAsset);
				string_3 = new string[256];
				for (int j = 0; j < 256; j++)
				{
					string_3[j] = j.ToString("x2");
				}
				return;
			}
			bool_2 = true;
			binaryReader.Close();
			memoryStream.Close();
		}

		private int GetAssemblyHash(Assembly assembly_0)
		{
			AssemblyName assemblyName = assembly_0.GetName();
			byte[] publicKeyToken = assemblyName.GetPublicKeyToken();
			string text = ((publicKeyToken.Length != 8) ? assemblyName.Name : (assemblyName.Name + PublicKeyTokenToString(publicKeyToken)));
			int num = 0;
			int length = text.Length;
			for (int i = 0; i < length; i++)
			{
				num += text[i];
				num += num << 10;
				num ^= num >> 6;
			}
			num += num << 3;
			num ^= num >> 11;
			return num + (num << 15);
		}

		private string PublicKeyTokenToString(byte[] byte_0)
		{
			string text = string.Empty;
			for (int i = 0; i < 8; i++)
			{
				text += string_3[byte_0[i]];
			}
			return text;
		}
	}
}
