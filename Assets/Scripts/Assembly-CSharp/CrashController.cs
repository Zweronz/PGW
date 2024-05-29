using System;
using System.Text;
using UnityEngine;
using engine.data;
using engine.filesystem;
using engine.helpers;
using engine.operations;
using engine.system;

public sealed class CrashController
{
	public const string string_0 = "last_crash_report";

	private const string string_1 = "crash";

	private const string string_2 = "crash_user_id";

	private const string string_3 = "crash_user_email";

	private BaseSharedSettings baseSharedSettings_0;

	private static CrashController crashController_0;

	private DictionarySerialize dictionarySerialize_0;

	private StringAssetFile stringAssetFile_0;

	public static CrashController CrashController_0
	{
		get
		{
			if (crashController_0 == null)
			{
				crashController_0 = new CrashController();
			}
			return crashController_0;
		}
	}

	public void Init(BaseSharedSettings baseSharedSettings_1)
	{
		baseSharedSettings_0 = baseSharedSettings_1;
		Check();
	}

	public void SaveUser(int int_0, string string_4)
	{
		if (baseSharedSettings_0 != null)
		{
			baseSharedSettings_0.SetValue("crash_user_id", int_0, true);
			baseSharedSettings_0.SetValue("crash_user_email", string_4, true);
		}
	}

	private void Check()
	{
		if (baseSharedSettings_0 != null)
		{
			string value = baseSharedSettings_0.GetValue("last_crash_report", string.Empty);
			if (!string.IsNullOrEmpty(value))
			{
				Debug.Log("CrashController::Check > Trying send crash report!");
				SendReport(value);
			}
			else
			{
				Debug.Log("CrashController::Check > Nothing to send!");
			}
		}
	}

	private void SendReport(string string_4)
	{
		dictionarySerialize_0 = new DictionarySerialize();
		stringAssetFile_0 = new StringAssetFile();
		int value = baseSharedSettings_0.GetValue<int>("crash_user_id");
		string value2 = baseSharedSettings_0.GetValue("crash_user_email", string.Empty);
		dictionarySerialize_0.Add("uid", value);
		dictionarySerialize_0.Add("email", value2);
		dictionarySerialize_0.Add("stacktrace", string_4);
		dictionarySerialize_0.Add("version", BaseAppController.BaseAppController_0.VersionInfo_0.ToString());
		dictionarySerialize_0.Add("sys_info", GetSystemInfo());
		dictionarySerialize_0.Add("sig", BaseAppController.BaseAppController_0.serverInfo_0.GetSignature(value2, string.Format("{0}", value)));
		byte[] array = null;
		try
		{
			array = Utility.LoadCompressedApplicationLogFile();
		}
		catch (Exception ex)
		{
			Debug.LogWarning("[CrashController::SendReport. Error get game log file for game report! Error]: " + ex.Message);
		}
		if (array != null)
		{
			dictionarySerialize_0.Add("zip", Convert.ToBase64String(array));
		}
		LoadNetworkFileBestHttpOperation loadNetworkFileBestHttpOperation = new LoadNetworkFileBestHttpOperation(BaseAppController.BaseAppController_0.serverInfo_0.String_0 + "crash", stringAssetFile_0);
		loadNetworkFileBestHttpOperation.ReadForm(dictionarySerialize_0);
		loadNetworkFileBestHttpOperation.Subscribe(OnSendReportCompleted, Operation.StatusEvent.Complete);
		OperationsManager.OperationsManager_0.Add(loadNetworkFileBestHttpOperation);
	}

	private void OnSendReportCompleted(Operation operation_0)
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
		if (num == 1)
		{
			Debug.Log("CrashController::OnSendReportCompleted > Report sent OK");
			ClearReport();
		}
		else
		{
			Debug.LogWarning("CrashController::OnSendReportCompleted > Report sent FAIL");
		}
	}

	private void ClearReport()
	{
		if (baseSharedSettings_0 != null)
		{
			baseSharedSettings_0.SetValue("last_crash_report", string.Empty, true);
			Debug.Log("CrashController::ClearReport > Report cleared");
		}
	}

	private string GetSystemInfo()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("cpu: {0}", SystemInfo.processorType);
		stringBuilder.AppendFormat(" memory: {0}", SystemInfo.systemMemorySize);
		stringBuilder.AppendFormat(" gpu: {0} {1}", SystemInfo.graphicsDeviceName, SystemInfo.graphicsMemorySize);
		stringBuilder.AppendFormat(" os: {0}", SystemInfo.operatingSystem);
		return stringBuilder.ToString();
	}

	public void CrashApp()
	{
		throw new ApplicationException("Keep calm! We all die!");
	}
}
