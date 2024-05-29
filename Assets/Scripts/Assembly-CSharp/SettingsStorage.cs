using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ProtoBuf;
using engine.data;
using engine.helpers;
using engine.unity;

[Obfuscation(Exclude = true)]
public sealed class SettingsStorage : BaseStorage<int, SettingsData>
{
	private static readonly Dictionary<Type, int> _mapTypeStorageKey = new Dictionary<Type, int>
	{
		{
			typeof(ChatParamSettings),
			1
		},
		{
			typeof(PlayerParamsSettings),
			2
		},
		{
			typeof(CommonParamsSettings),
			3
		},
		{
			typeof(FlagParamsSettings),
			4
		},
		{
			typeof(MatchMakingSettings),
			5
		}
	};

	private static SettingsStorage _instance = null;

	public static SettingsStorage Get
	{
		get
		{
			if (_instance == null)
			{
				_instance = new SettingsStorage();
			}
			return _instance;
		}
	}

	protected override void OnCreate()
	{
		MethodInfo method = typeof(Serializer).GetMethod("Deserialize", new Type[1] { typeof(Stream) });
		foreach (KeyValuePair<Type, int> item in _mapTypeStorageKey)
		{
			Log.AddLine("[SettingsStorage::Init. Initing setting, type]: " + item.Key);
			SettingsData objectByKey = Get.Storage.GetObjectByKey(item.Value);
			if (objectByKey != null)
			{
				object obj = null;
				try
				{
					using (MemoryStream memoryStream = new MemoryStream(objectByKey.Byte_0))
					{
						obj = method.MakeGenericMethod(item.Key).Invoke(null, new object[1] { memoryStream });
					}
				}
				catch (Exception ex)
				{
					MonoSingleton<Log>.Prop_0.DumpError(ex);
					Log.AddLine(string.Format("[SettingsStorage::Init. Setting {0} can not Deserialize Exception = {1}", item.Key, ex.Message), Log.LogLevel.ERROR);
				}
				if (obj != null)
				{
					item.Key.GetProperty("Get").SetValue(null, obj, null);
				}
				continue;
			}
			Log.AddLine(string.Format("[SettingsStorage::Init. SettingsStorage have no id = {0} for object = {1}]", item.Value, item.Key), Log.LogLevel.ERROR);
			break;
		}
	}
}
