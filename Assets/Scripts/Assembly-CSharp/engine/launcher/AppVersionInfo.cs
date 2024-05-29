using System;
using System.IO;
using System.Text;
using engine.helpers;

namespace engine.launcher
{
	public sealed class AppVersionInfo
	{
		public VersionInfo versionInfo_0 = new VersionInfo();

		public string string_0 = string.Empty;

		public string string_1 = string.Empty;

		public string string_2 = string.Empty;

		public string string_3 = string.Empty;

		public string string_4 = string.Empty;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("title: " + string_0);
			stringBuilder.AppendLine("version: " + versionInfo_0.ToString());
			stringBuilder.AppendLine("url: " + string_1);
			stringBuilder.AppendLine("urlSDN: " + string_2);
			stringBuilder.AppendLine("fileName: " + string_0);
			stringBuilder.AppendLine("packageName: " + GetPackageName());
			stringBuilder.AppendLine("rootDirectory: " + string_4);
			return stringBuilder.ToString();
		}

		public string GetPackageName()
		{
			Uri result = null;
			bool flag;
			if (flag = !string.IsNullOrEmpty(string_1))
			{
				flag = Uri.TryCreate(string_1, UriKind.Absolute, out result);
			}
			return flag ? Path.GetFileName(result.LocalPath) : string.Empty;
		}

		public void CopyFrom(AppVersionInfo appVersionInfo_0)
		{
			if (appVersionInfo_0 == null)
			{
				Log.AddLine("For copy AppVersionInfo needs source info!", Log.LogLevel.ERROR);
				return;
			}
			versionInfo_0.CopyFrom(appVersionInfo_0.versionInfo_0);
			string_0 = appVersionInfo_0.string_0;
			string_1 = appVersionInfo_0.string_1;
			string_2 = appVersionInfo_0.string_2;
			string_3 = appVersionInfo_0.string_3;
			string_4 = appVersionInfo_0.string_4;
		}
	}
}
