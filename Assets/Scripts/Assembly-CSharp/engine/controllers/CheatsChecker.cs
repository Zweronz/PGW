using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using engine.filesystem;
using engine.helpers;
using engine.unity;

namespace engine.controllers
{
	public sealed class CheatsChecker
	{
		private static CheatsChecker cheatsChecker_0;

		private bool bool_0;

		[CompilerGenerated]
		private Dictionary<string, string> dictionary_0;

		public static CheatsChecker CheatsChecker_0
		{
			get
			{
				return cheatsChecker_0 ?? (cheatsChecker_0 = new CheatsChecker());
			}
		}

		public Dictionary<string, string> Dictionary_0
		{
			[CompilerGenerated]
			get
			{
				return dictionary_0;
			}
			[CompilerGenerated]
			set
			{
				dictionary_0 = value;
			}
		}

		private CheatsChecker()
		{
		}

		public string GetAssemblyDllHash()
		{
			DictionaryProtoSerialize dictionaryProtoSerialize = EncryptionHelper.GenerateMd5AssemblyDllFiles(string.Empty);
			string result = string.Empty;
			try
			{
				byte[] buffer = dictionaryProtoSerialize.Serialize();
				using (MemoryStream stream_ = new MemoryStream(buffer))
				{
					result = EncryptionHelper.Md5Sum(stream_, true);
				}
			}
			catch (Exception exception_)
			{
				Log.AddLine("[AssemblyChecker::GetAssemblyDllHash. Error calculate hash for assembly dlls!]", Log.LogLevel.WARNING);
				MonoSingleton<Log>.Prop_0.DumpError(exception_);
			}
			return result;
		}

		public void CheckHashFileDeploy(FileName fileName_0)
		{
			if (Dictionary_0 == null)
			{
				Log.AddLine("[CheatsChecker::CheckHashFileDeploy. DeployHash not settings from server!]");
				return;
			}
			string fileName = Path.GetFileName(fileName_0.String_1);
			string value = null;
			if (!Dictionary_0.TryGetValue(fileName, out value))
			{
				Log.AddLine("[CheatsChecker::CheckHashFileDeploy. File not found in server DeployHash, file]: " + fileName);
				return;
			}
			string text = Path.Combine(FileCache.FileCache_0.String_0, fileName_0.String_2);
			if (!File.Exists(text))
			{
				Log.AddLine("[CheatsChecker::CheckHashFileDeploy. File not found in file cache, file]: " + text);
				return;
			}
			string text2 = EncryptionHelper.FileMd5Sum(text);
			if (!text2.Equals(value))
			{
				Log.AddLine(string.Format("[CheatsChecker::CheckHashFileDeploy. Hash for file = {0}, not equals hash from server, need reload file!]: ", text));
				ResourcesManager.ResourcesManager_0.RemoveFileFromActiveCache(fileName_0);
				bool_0 = true;
			}
		}

		public bool CheckSendDeployCheat()
		{
			if (!bool_0)
			{
				return false;
			}
			bool_0 = false;
			return true;
		}
	}
}
