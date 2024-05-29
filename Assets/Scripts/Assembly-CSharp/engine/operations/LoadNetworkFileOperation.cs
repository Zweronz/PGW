using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.events;
using engine.filesystem;
using engine.helpers;
using engine.unity;

namespace engine.operations
{
	public class LoadNetworkFileOperation : LoadFileOperation
	{
		public static string string_1 = string.Empty;

		private string string_2;

		private WWWForm wwwform_0;

		private WWW www_0;

		[CompilerGenerated]
		private bool bool_6;

		private WWWForm WWWForm_0
		{
			get
			{
				return wwwform_0;
			}
			set
			{
				wwwform_0 = value;
				Boolean_4 = value == null;
			}
		}

		public new bool Boolean_4
		{
			[CompilerGenerated]
			get
			{
				return bool_6;
			}
			[CompilerGenerated]
			private set
			{
				bool_6 = value;
			}
		}

		public LoadNetworkFileOperation(string string_3, BaseAssetFile baseAssetFile_1)
		{
			if (baseAssetFile_1 == null)
			{
				Log.AddLine("Not set asset file! File can not be downloaded!", Log.LogLevel.ERROR);
				return;
			}
			Boolean_4 = true;
			base.BaseAssetFile_0 = baseAssetFile_1;
			string_2 = string_3;
			DependSceneEvent<ChangeSceneUnityEvent>.GlobalSubscribe(Destroy);
		}

		public static LoadNetworkFileOperation Create<T>(string string_3) where T : BaseAssetFile, new()
		{
			BaseAssetFile baseAssetFile_ = BaseAssetFile.Create<T>();
			return new LoadNetworkFileOperation(string_3, baseAssetFile_);
		}

		public void ReadForm(DictionarySerialize dictionarySerialize_0)
		{
			WWWForm wWWForm = new WWWForm();
			foreach (KeyValuePair<string, object> item in dictionarySerialize_0.dictionary_0)
			{
				wWWForm.AddField(item.Key, item.Value.ToString());
			}
			WWWForm_0 = wWWForm;
		}

		protected override void Execute()
		{
			if (Boolean_4)
			{
				if (string_2.StartsWith("http"))
				{
					www_0 = new WWW(string_2);
				}
				else
				{
					www_0 = new WWW(string_1 + string_2);
				}
			}
			else
			{
				www_0 = new WWW(string_1 + string_2, WWWForm_0);
			}
			DependSceneEvent<MainUpdate>.GlobalSubscribe(Update);
		}

		public override void Update()
		{
			if (!www_0.isDone)
			{
				if (www_0.progress != 0f)
				{
					base.ProgressEvent_0.Dispatch(www_0.progress);
				}
				return;
			}
			DependSceneEvent<MainUpdate>.GlobalUnsubscribe(Update);
			if (www_0.error != null)
			{
				base.BaseAssetFile_0.IsLoaded = false;
				MonoSingleton<Log>.Prop_0.DumpError(new Exception(www_0.url + ": " + www_0.error));
				Error();
				return;
			}
			base.BaseAssetFile_0.IsLoaded = true;
			if (base.BaseAssetFile_0 is AssetBundleFile)
			{
				if (!(www_0.assetBundle != null))
				{
					base.BaseAssetFile_0.IsLoaded = false;
					MonoSingleton<Log>.Prop_0.DumpError(new Exception(www_0.url + ": Loading AssetBundle error, _www.assetBundle == null"));
					Error();
					return;
				}
				AssetBundleFile assetBundleFile = (AssetBundleFile)base.BaseAssetFile_0;
				assetBundleFile.AssetBundleData = www_0.assetBundle;
				assetBundleFile.BundleData = www_0.assetBundle.mainAsset;
				base.BaseAssetFile_0.Bytes = www_0.bytes;
			}
			else if (base.BaseAssetFile_0 is TextureAssetFile)
			{
				TextureAssetFile textureAssetFile = (TextureAssetFile)base.BaseAssetFile_0;
				textureAssetFile.Texture = www_0.texture;
				base.BaseAssetFile_0.Bytes = www_0.bytes;
			}
			else
			{
				base.BaseAssetFile_0.Bytes = www_0.bytes;
				try
				{
					base.BaseAssetFile_0.Convert();
				}
				catch (Exception ex)
				{
					base.BaseAssetFile_0.IsLoaded = false;
					MonoSingleton<Log>.Prop_0.DumpError(new Exception(www_0.url + " : " + ex.Message + " : " + ex.StackTrace));
					Error();
				}
			}
			Complete();
		}

		private void Destroy()
		{
			if (base.BaseAssetFile_0 is AssetBundleFile)
			{
				base.BaseAssetFile_0.Destroy();
				if (www_0 != null && www_0.assetBundle != null)
				{
					www_0.assetBundle.Unload(true);
					www_0.Dispose();
				}
				www_0 = null;
			}
		}
	}
}
