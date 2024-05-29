using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using engine.controllers;
using engine.events;
using engine.filesystem;
using engine.helpers;
using engine.operations;
using engine.system;

namespace engine.network.auth
{
	public sealed class AuthManager : BaseEvent<AuthEventParams>
	{
		public enum Event
		{
			AuthSuccess = 0,
			AuthFail = 1,
			RegistrationSuccess = 2,
			RegistrationFail = 3,
			EnterSuccess = 4,
			EnterFail = 5
		}

		private static DictionarySerialize dictionarySerialize_0;

		private static StringAssetFile stringAssetFile_0;

		private static AuthManager authManager_0;

		[CompilerGenerated]
		private static bool bool_0;

		public static bool Boolean_0
		{
			[CompilerGenerated]
			get
			{
				return bool_0;
			}
			[CompilerGenerated]
			private set
			{
				bool_0 = value;
			}
		}

		public static AuthManager AuthManager_0
		{
			get
			{
				if (authManager_0 == null)
				{
					authManager_0 = new AuthManager();
				}
				return authManager_0;
			}
		}

		private string GetGUID()
		{
			string result = string.Empty;
			try
			{
				result = Utility.GetMachineGuid();
			}
			catch (Exception ex)
			{
				Log.AddLine("AuthManager::GetGUID > get guid failed with error: " + ex.Message);
			}
			return result;
		}

		public void ProcessEnter(string string_0)
		{
			Init();
			dictionarySerialize_0.Add("key", string_0);
			dictionarySerialize_0.Add("machine_guid", GetGUID());
			dictionarySerialize_0.Add("version", BaseAppController.BaseAppController_0.VersionInfo_0.ToString());
			dictionarySerialize_0.Add("hash0", CheatsChecker.CheatsChecker_0.GetAssemblyDllHash());
			Log.AddLine("----------- Process enter -------------");
			Log.AddLine("form data: \n" + dictionarySerialize_0.ToString());
			Log.AddLine("---------------------------------------");
			LoadNetworkFileBestHttpOperation loadNetworkFileBestHttpOperation = new LoadNetworkFileBestHttpOperation(BaseAppController.BaseAppController_0.serverInfo_0.GetEnterUrl(), stringAssetFile_0);
			loadNetworkFileBestHttpOperation.ReadForm(dictionarySerialize_0);
			loadNetworkFileBestHttpOperation.Subscribe(OnEnterOperationCompleted, Operation.StatusEvent.Complete);
			OperationsManager.OperationsManager_0.Add(loadNetworkFileBestHttpOperation);
		}

		public void ProcessAuth(string string_0, string string_1, string string_2)
		{
			Init();
			string_0 = ((!string.IsNullOrEmpty(string_0)) ? string_0 : string.Empty);
			string_1 = ((!string.IsNullOrEmpty(string_1)) ? string_1 : string.Empty);
			string_2 = ((!string.IsNullOrEmpty(string_2)) ? string_2 : string.Empty);
			dictionarySerialize_0.Add("email", string_0);
			dictionarySerialize_0.Add("pass", string_1);
			dictionarySerialize_0.Add("key", string_2);
			dictionarySerialize_0.Add("machine_guid", GetGUID());
			dictionarySerialize_0.Add("sig", BaseAppController.BaseAppController_0.serverInfo_0.GetSignature(string_0, string.Empty));
			Log.AddLine("----------- Process auth -------------");
			Log.AddLine("form data: \n" + dictionarySerialize_0.ToString());
			Log.AddLine("-------------------------------------  ");
			LoadNetworkFileBestHttpOperation loadNetworkFileBestHttpOperation = new LoadNetworkFileBestHttpOperation(BaseAppController.BaseAppController_0.serverInfo_0.GetAuthUrl(), stringAssetFile_0);
			loadNetworkFileBestHttpOperation.ReadForm(dictionarySerialize_0);
			loadNetworkFileBestHttpOperation.Subscribe(OnAuthOperationCompleted, Operation.StatusEvent.Complete);
			OperationsManager.OperationsManager_0.Add(loadNetworkFileBestHttpOperation);
		}

		public void ProcessRegistration(string string_0, string string_1, string string_2, string string_3)
		{
			Init();
			dictionarySerialize_0.Add("email", string_0);
			dictionarySerialize_0.Add("pass", string_1);
			dictionarySerialize_0.Add("src", "launcher");
			dictionarySerialize_0.Add("locale", string_3);
			dictionarySerialize_0.Add("sig", BaseAppController.BaseAppController_0.serverInfo_0.GetSignature(string_0, string_2));
			Log.AddLine("----------- Process registration -------------");
			Log.AddLine("form data: \n" + dictionarySerialize_0.ToString());
			Log.AddLine("-------------------------------------  ");
			LoadNetworkFileBestHttpOperation loadNetworkFileBestHttpOperation = new LoadNetworkFileBestHttpOperation(BaseAppController.BaseAppController_0.serverInfo_0.GetRegistrationUrl(), stringAssetFile_0);
			loadNetworkFileBestHttpOperation.ReadForm(dictionarySerialize_0);
			loadNetworkFileBestHttpOperation.Subscribe(OnRegistrationOperationCompleted, Operation.StatusEvent.Complete);
			OperationsManager.OperationsManager_0.Add(loadNetworkFileBestHttpOperation);
		}

		private void OnRegistrationOperationCompleted(Operation operation_0)
		{
			LoadNetworkFileBestHttpOperation loadNetworkFileBestHttpOperation = operation_0 as LoadNetworkFileBestHttpOperation;
			int num = 0;
			if (loadNetworkFileBestHttpOperation != null && !loadNetworkFileBestHttpOperation.Boolean_1 && loadNetworkFileBestHttpOperation.BaseAssetFile_0.IsLoaded)
			{
				StringAssetFile stringAssetFile = (StringAssetFile)loadNetworkFileBestHttpOperation.BaseAssetFile_0;
				try
				{
					num = int.Parse(stringAssetFile.StringData);
				}
				catch
				{
					num = 0;
				}
			}
			Log.AddLine("----------- Result process registration -------------");
			Log.AddLine("Status code:" + num);
			Log.AddLine("-------------------------------------  ");
			AuthEventParams authEventParams = new AuthEventParams();
			authEventParams.int_0 = num;
			AuthEventParams gparam_ = authEventParams;
			if (num == 1)
			{
				Dispatch(gparam_, Event.RegistrationSuccess);
			}
			else
			{
				Dispatch(gparam_, Event.RegistrationFail);
			}
			UnsubscribeAll();
		}

		private void OnAuthOperationCompleted(Operation operation_0)
		{
			LoadNetworkFileBestHttpOperation loadNetworkFileBestHttpOperation = operation_0 as LoadNetworkFileBestHttpOperation;
			int num = 0;
			string text = string.Empty;
			string text2 = string.Empty;
			int num2 = 0;
			if (loadNetworkFileBestHttpOperation != null && !loadNetworkFileBestHttpOperation.Boolean_1 && loadNetworkFileBestHttpOperation.BaseAssetFile_0.IsLoaded)
			{
				StringAssetFile stringAssetFile = (StringAssetFile)loadNetworkFileBestHttpOperation.BaseAssetFile_0;
				string[] array = stringAssetFile.StringData.Split('\n');
				num = int.Parse(array[0]);
				text = ((array.Length <= 1) ? text : array[1]);
				text2 = ((array.Length <= 2) ? text2 : array[2]);
				num2 = ((array.Length <= 3) ? num2 : int.Parse(array[3]));
			}
			Log.AddLine("----------- Result process auth -------------");
			Log.AddLine("Status code:" + num);
			Log.AddLine("Nick:" + text);
			Log.AddLine("AuthKey:" + text2);
			Log.AddLine("Uid:" + num2);
			Log.AddLine("-------------------------------------  ");
			AuthEventParams authEventParams = new AuthEventParams();
			authEventParams.int_0 = num;
			authEventParams.string_0 = text;
			authEventParams.string_1 = text2;
			authEventParams.int_1 = num2;
			AuthEventParams gparam_ = authEventParams;
			if (num == 1)
			{
				Dispatch(gparam_, Event.AuthSuccess);
			}
			else
			{
				Dispatch(gparam_, Event.AuthFail);
			}
			UnsubscribeAll();
		}

		private void OnEnterOperationCompleted(Operation operation_0)
		{
			LoadNetworkFileBestHttpOperation loadNetworkFileBestHttpOperation = operation_0 as LoadNetworkFileBestHttpOperation;
			int int_ = 0;
			string text = string.Empty;
			double time = 0.0;
			string text2 = string.Empty;
			Dictionary<string, string> dictionary = null;
			if (loadNetworkFileBestHttpOperation != null && !loadNetworkFileBestHttpOperation.Boolean_1 && loadNetworkFileBestHttpOperation.BaseAssetFile_0.IsLoaded)
			{
				EnterServerResponse enterServerResponse = EnterServerResponse.FromByte(loadNetworkFileBestHttpOperation.BaseAssetFile_0.Bytes);
				int_ = enterServerResponse.int_0;
				text = enterServerResponse.string_0;
				time = enterServerResponse.double_0;
				text2 = enterServerResponse.string_1;
				dictionary = enterServerResponse.dictionary_0;
				Utility.setTime(time);
			}
			Log.AddLine("----------- Result process enter -------------");
			Log.AddLine("Status code: " + int_);
			Log.AddLine("SocketIntrerface: " + text);
			Log.AddLine("serverTime: " + time);
			Log.AddLine("deployVersion: " + text2);
			Log.AddLine("deployHash: " + ((dictionary != null) ? "present!" : "not present!"));
			Log.AddLine("-------------------------------------  ");
			AuthEventParams authEventParams = new AuthEventParams();
			authEventParams.int_0 = int_;
			authEventParams.string_2 = text;
			authEventParams.string_3 = text2;
			authEventParams.dictionary_0 = dictionary;
			AuthEventParams authEventParams2 = authEventParams;
			if (authEventParams2.int_0 == 1)
			{
				Boolean_0 = true;
				Dispatch(authEventParams2, Event.EnterSuccess);
			}
			else
			{
				Dispatch(authEventParams2, Event.EnterFail);
			}
			UnsubscribeAll();
		}

		private static void Init()
		{
			if (dictionarySerialize_0 == null)
			{
				dictionarySerialize_0 = new DictionarySerialize();
			}
			else
			{
				dictionarySerialize_0.Clear();
			}
			stringAssetFile_0 = new StringAssetFile();
		}
	}
}
