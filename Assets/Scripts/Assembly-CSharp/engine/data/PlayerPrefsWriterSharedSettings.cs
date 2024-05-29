using System;
using UnityEngine;
using engine.helpers;
using engine.unity;

namespace engine.data
{
	public sealed class PlayerPrefsWriterSharedSettings : IReadWriterShredSettings
	{
		private string String_0
		{
			get
			{
				string string_ = baseSharedSettings_0.String_1;
				if (string.IsNullOrEmpty(string_))
				{
					Log.AddLine("PlayerPrefsWriterSharedSettings|FieldName. For settings not set fileName", Log.LogLevel.FATAL);
				}
				return string_;
			}
		}

		public PlayerPrefsWriterSharedSettings(BaseSharedSettings baseSharedSettings_1)
			: base(baseSharedSettings_1)
		{
		}

		public override void Save()
		{
			try
			{
				PlayerPrefs.SetString(String_0, baseSharedSettings_0.dictionarySerialize_0.ToJSON());
				PlayerPrefs.Save();
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
				string string_ = String_0;
				if (PlayerPrefs.HasKey(string_))
				{
					baseSharedSettings_0.dictionarySerialize_0.ParseJSON(PlayerPrefs.GetString(string_));
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
