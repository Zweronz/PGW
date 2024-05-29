using System;
using System.Collections.Generic;
using System.Text;
using BestHTTP;
using engine.events;
using engine.filesystem;
using engine.helpers;
using engine.unity;

namespace engine.operations
{
	public class LoadNetworkFileBestHttpOperation : LoadFileOperation
	{
		public static string string_1 = string.Empty;

		private string string_2;

		private bool bool_6;

		private HTTPRequest httprequest_0;

		private DictionarySerialize dictionarySerialize_0;

		private float float_0;

		private HTTPResponse httpresponse_0;

		public LoadNetworkFileBestHttpOperation(string string_3, BaseAssetFile baseAssetFile_1)
		{
			if (baseAssetFile_1 == null)
			{
				Log.AddLine("Not set asset file! File can not be downloaded!", Log.LogLevel.ERROR);
				return;
			}
			base.BaseAssetFile_0 = baseAssetFile_1;
			string_2 = string_3;
			DependSceneEvent<ChangeSceneUnityEvent>.GlobalSubscribe(Destroy);
		}

		public static LoadNetworkFileBestHttpOperation Create<T>(string string_3) where T : BaseAssetFile, new()
		{
			BaseAssetFile baseAssetFile_ = BaseAssetFile.Create<T>();
			return new LoadNetworkFileBestHttpOperation(string_3, baseAssetFile_);
		}

		public void ReadForm(DictionarySerialize dictionarySerialize_1)
		{
			dictionarySerialize_0 = dictionarySerialize_1;
		}

		protected override void Execute()
		{
			Uri uri_ = new Uri((!string_2.StartsWith("http")) ? (string_1 + string_2) : string_2);
			if (dictionarySerialize_0 == null)
			{
				httprequest_0 = new HTTPRequest(uri_, OnRequestFinished);
			}
			else
			{
				httprequest_0 = new HTTPRequest(uri_, HTTPMethods.Post, OnRequestFinished);
				foreach (KeyValuePair<string, object> item in dictionarySerialize_0.dictionary_0)
				{
					httprequest_0.AddField(item.Key, item.Value.ToString());
				}
			}
			httprequest_0.onDownloadProgressDelegate_0 = OnDownloadProgress;
			try
			{
				httprequest_0.TimeSpan_0 = TimeSpan.FromSeconds(15.0);
				httprequest_0.TimeSpan_1 = TimeSpan.FromSeconds(15.0);
				httprequest_0.Send();
			}
			catch (Exception)
			{
				base.BaseAssetFile_0.IsLoaded = false;
				Log.AddLine(string.Format("URL:{0}. Exeption message: {1}", httprequest_0.Uri_0, (httprequest_0.Exception_0 == null) ? string.Empty : httprequest_0.Exception_0.Message), Log.LogLevel.ERROR);
				if (httprequest_0.Exception_0 != null)
				{
					MonoSingleton<Log>.Prop_0.DumpError(httprequest_0.Exception_0);
				}
				Error();
				return;
			}
			DependSceneEvent<MainUpdate>.GlobalSubscribe(Update);
		}

		private void OnRequestFinished(HTTPRequest httprequest_1, HTTPResponse httpresponse_1)
		{
			bool_6 = true;
			httprequest_0 = httprequest_1;
			httpresponse_0 = httpresponse_1;
		}

		private void OnDownloadProgress(HTTPRequest httprequest_1, int int_0, int int_1)
		{
			float_0 = (float)int_0 / (float)int_1;
		}

		public override void Update()
		{
			if (!bool_6)
			{
				if (float_0 != 0f)
				{
					base.ProgressEvent_0.Dispatch(float_0);
				}
				return;
			}
			DependSceneEvent<MainUpdate>.GlobalUnsubscribe(Update);
			if (CheckError())
			{
				base.BaseAssetFile_0.IsLoaded = false;
				Error();
				return;
			}
			base.BaseAssetFile_0.IsLoaded = true;
			if (base.BaseAssetFile_0 is AssetBundleFile)
			{
				base.BaseAssetFile_0.IsLoaded = false;
				Log.AddLine(string.Format("URL:{0}. Message: {1}", httprequest_0.Uri_0, "Loading AssetBundle error, not implemented for this operation!"), Log.LogLevel.ERROR);
				Error();
				return;
			}
			if (base.BaseAssetFile_0 is TextureAssetFile)
			{
				TextureAssetFile textureAssetFile = (TextureAssetFile)base.BaseAssetFile_0;
				textureAssetFile.Texture = httpresponse_0.Texture2D_0;
				textureAssetFile.Bytes = httpresponse_0.Byte_0;
			}
			else
			{
				base.BaseAssetFile_0.Bytes = httpresponse_0.Byte_0;
				try
				{
					base.BaseAssetFile_0.Convert();
				}
				catch (Exception ex)
				{
					base.BaseAssetFile_0.IsLoaded = false;
					Log.AddLine(string.Format("URL:{0}. Exeption message: {1}", httprequest_0.Uri_0, ex.Message + " : " + ex.StackTrace), Log.LogLevel.ERROR);
					MonoSingleton<Log>.Prop_0.DumpError(ex);
					Error();
					return;
				}
			}
			Complete();
		}

		private void Destroy()
		{
			if (base.BaseAssetFile_0 is AssetBundleFile)
			{
				base.BaseAssetFile_0.Destroy();
			}
			httprequest_0 = null;
			httpresponse_0 = null;
		}

		private bool CheckError()
		{
			switch (httprequest_0.HTTPRequestStates_0)
			{
			default:
				return true;
			case HTTPRequestStates.Finished:
				return false;
			case HTTPRequestStates.Error:
				Log.AddLine(string.Format("URL:{0}. Exeption message: {1}", httprequest_0.Uri_0, (httprequest_0.Exception_0 == null) ? string.Empty : (httprequest_0.Exception_0.Message + " : " + httprequest_0.Exception_0.StackTrace)), Log.LogLevel.ERROR);
				if (httprequest_0.Exception_0 != null)
				{
					MonoSingleton<Log>.Prop_0.DumpError(httprequest_0.Exception_0);
				}
				return true;
			case HTTPRequestStates.Aborted:
				Log.AddLine(string.Format("URL:{0}. Exeption message: {1}", httprequest_0.Uri_0, "Request Aborted!"), Log.LogLevel.ERROR);
				return true;
			case HTTPRequestStates.ConnectionTimedOut:
				Log.AddLine(string.Format("URL:{0}. Exeption message: {1}, {2}", httprequest_0.Uri_0, "Connection Timed Out!", "ConnectTimeout = " + httprequest_0.TimeSpan_0), Log.LogLevel.ERROR);
				return true;
			case HTTPRequestStates.TimedOut:
				Log.AddLine(string.Format("URL:{0}. Exeption message: {1}, {2}", httprequest_0.Uri_0, "Processing the request Timed Out!", "TimeOut = " + httprequest_0.TimeSpan_1), Log.LogLevel.ERROR);
				return true;
			}
		}

		public override string ToString()
		{
			Uri uri = new Uri((!string_2.StartsWith("http")) ? (string_1 + string_2) : string_2);
			StringBuilder stringBuilder = new StringBuilder("\n");
			stringBuilder.AppendLine("Uri = " + uri);
			if (dictionarySerialize_0 != null && dictionarySerialize_0.dictionary_0 != null)
			{
				stringBuilder.AppendLine("---- Post Data Start ----");
				foreach (KeyValuePair<string, object> item in dictionarySerialize_0.dictionary_0)
				{
					string text = item.Value.ToString();
					text = ((text.Length <= 1024) ? text : text.Substring(0, 1024));
					stringBuilder.AppendLine(string.Format("Key = {0}, Value = {1}", item.Key, text));
				}
				stringBuilder.AppendLine("---- Post Data End ----");
			}
			stringBuilder.AppendLine("Cancel = " + Boolean_4);
			stringBuilder.AppendLine("IsComplete = " + base.Boolean_0);
			stringBuilder.AppendLine("Failed = " + base.Boolean_1);
			return stringBuilder.ToString();
		}
	}
}
