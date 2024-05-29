using engine.filesystem;
using engine.helpers;
using engine.operations;
using engine.system;

namespace engine.launcher
{
	public sealed class LauncherStatNetwork
	{
		public sealed class LauncherStatEventData
		{
			public int int_0;

			public LauncherStatEvents launcherStatEvents_0;

			public string string_0;

			public string string_1;
		}

		private const string string_0 = "/stat";

		private static LauncherStatNetwork launcherStatNetwork_0;

		private static DictionarySerialize dictionarySerialize_0;

		private static StringAssetFile stringAssetFile_0;

		public static LauncherStatNetwork LauncherStatNetwork_0
		{
			get
			{
				if (launcherStatNetwork_0 == null)
				{
					launcherStatNetwork_0 = new LauncherStatNetwork();
				}
				return launcherStatNetwork_0;
			}
		}

		private LauncherStatNetwork()
		{
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

		public void SendStat(LauncherStatEventData launcherStatEventData_0)
		{
			Init();
			dictionarySerialize_0.Add("uid", launcherStatEventData_0.int_0);
			dictionarySerialize_0.Add("event", (int)launcherStatEventData_0.launcherStatEvents_0);
			dictionarySerialize_0.Add("var_1", launcherStatEventData_0.string_0);
			dictionarySerialize_0.Add("var_2", launcherStatEventData_0.string_1);
			dictionarySerialize_0.Add("sig", BaseAppController.BaseAppController_0.serverInfo_0.GetSignature(string.Format("{0}", launcherStatEventData_0.int_0), string.Format("{0}", (int)launcherStatEventData_0.launcherStatEvents_0)));
			LoadNetworkFileBestHttpOperation loadNetworkFileBestHttpOperation = new LoadNetworkFileBestHttpOperation(BaseAppController.BaseAppController_0.serverInfo_0.String_0 + "/stat", stringAssetFile_0);
			loadNetworkFileBestHttpOperation.ReadForm(dictionarySerialize_0);
			loadNetworkFileBestHttpOperation.Subscribe(OnStatResponse, Operation.StatusEvent.Complete);
			OperationsManager.OperationsManager_0.Add(loadNetworkFileBestHttpOperation);
		}

		private void OnStatResponse(Operation operation_0)
		{
			LoadNetworkFileBestHttpOperation loadNetworkFileBestHttpOperation = operation_0 as LoadNetworkFileBestHttpOperation;
			int num = 0;
			if (loadNetworkFileBestHttpOperation != null && !loadNetworkFileBestHttpOperation.Boolean_1 && loadNetworkFileBestHttpOperation.BaseAssetFile_0.IsLoaded)
			{
				StringAssetFile stringAssetFile = (StringAssetFile)loadNetworkFileBestHttpOperation.BaseAssetFile_0;
				num = int.Parse(stringAssetFile.StringData);
			}
			Log.AddLine(string.Format("StatManager::OnStatResponse > code: {0}", num));
		}
	}
}
