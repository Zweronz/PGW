using System;
using System.IO;
using System.Text;
using engine.helpers;
using engine.unity;

namespace engine.data
{
	public sealed class JSONReadWriterSharedSettings : IReadWriterShredSettings
	{
		private string String_0
		{
			get
			{
				string string_ = baseSharedSettings_0.String_1;
				string string_2 = baseSharedSettings_0.String_2;
				if (string.IsNullOrEmpty(string_2) || string.IsNullOrEmpty(string_))
				{
					Log.AddLine("JSONReadWriterSharedSettings|FullFilePath. For settings not set PathToDir or FileName", Log.LogLevel.FATAL);
				}
				return Path.Combine(string_2, string_);
			}
		}

		public JSONReadWriterSharedSettings(BaseSharedSettings baseSharedSettings_1)
			: base(baseSharedSettings_1)
		{
		}

		public override void Save()
		{
			try
			{
				File.WriteAllText(String_0, baseSharedSettings_0.dictionarySerialize_0.ToJSON(), Encoding.UTF8);
			}
			catch (Exception ex)
			{
				MonoSingleton<Log>.Prop_0.DumpError(ex);
				Log.AddLine("Save shared setting error!", ex, Log.LogLevel.FATAL);
			}
		}

		public override void Load()
		{
			try
			{
				if (File.Exists(String_0))
				{
					baseSharedSettings_0.dictionarySerialize_0.ParseJSON(File.ReadAllText(String_0, Encoding.UTF8));
				}
			}
			catch (Exception ex)
			{
				MonoSingleton<Log>.Prop_0.DumpError(ex);
				Log.AddLine("Load shared setting error!", ex, Log.LogLevel.FATAL);
			}
		}
	}
}
