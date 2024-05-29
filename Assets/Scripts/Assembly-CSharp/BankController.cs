using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Rilisoft.MiniJson;
using UnityEngine;
using engine.filesystem;
using engine.helpers;
using engine.network;
using engine.operations;
using engine.system;

public sealed class BankController
{
	public enum BankSourceType
	{
		BANK_LOBBY = 1,
		BANK_SHOP = 2,
		BANK_KILLCAM = 3
	}

	private const string string_0 = "static/bank/bank.json";

	private static BankController bankController_0;

	private StringAssetFile stringAssetFile_0;

	private BankSourceType bankSourceType_0;

	private bool bool_0;

	private bool bool_1;

	private bool bool_2 = true;

	private List<BankPositionData> list_0 = new List<BankPositionData>();

	private string string_1 = string.Empty;

	[CompilerGenerated]
	private static Comparison<BankPositionData> comparison_0;

	public static BankController BankController_0
	{
		get
		{
			if (bankController_0 == null)
			{
				bankController_0 = new BankController();
			}
			return bankController_0;
		}
	}

	public string String_0
	{
		get
		{
			return BaseAppController.BaseAppController_0.serverInfo_0.String_0 + "static/bank/bank.json";
		}
	}

	public bool Boolean_0
	{
		get
		{
			return BankWindow.Boolean_1 || bool_0;
		}
	}

	private BankController()
	{
	}

	public void Init()
	{
		CleanBankPositions();
	}

	public void TryOpenBank(BankSourceType bankSourceType_1)
	{
		bankSourceType_0 = bankSourceType_1;
		ProcessPositions();
	}

	public void GoToBank(int int_0, string string_2)
	{
		string fileName = string.Format("{0}?source={1}&token={2}&coins={3}&method={4}&bankHash={5}", AppController.AppController_0.String_3, (int)bankSourceType_0, AppController.AppController_0.ProcessArguments_0.String_0, int_0, string_2, string_1);
		Process.Start(fileName);
	}

	public void ConfirmGoToBank(BankSourceType bankSourceType_1)
	{
		MessageWindowConfirm.Show(new MessageWindowConfirmParams(LocalizationStorage.Get.Term("ui.sitebank.yes_no"), delegate
		{
			TryOpenBank(bankSourceType_1);
		}, "OK", KeyCode.None, null, string.Empty));
	}

	public bool CanBuy(int int_0, bool bool_3 = true)
	{
		ShopArtikulData shopArtikul = ShopArtikulController.ShopArtikulController_0.GetShopArtikul(int_0);
		if (shopArtikul == null)
		{
			return false;
		}
		bool flag = true;
		flag = shopArtikul.CanBuy();
		if (shopArtikul.Int32_8 > 0 && UserController.UserController_0.GetTimerForFreeBuy(UserTimerData.UserTimerType.USER_TIMER_GACHA, shopArtikul.Int32_0) + (double)shopArtikul.Int32_8 <= Utility.Double_0)
		{
			flag = true;
		}
		if (!flag && bool_3)
		{
			TryOpenBank(BankSourceType.BANK_SHOP);
		}
		return flag;
	}

	public void CleanBankPositions(bool bool_3 = true)
	{
		Log.AddLineFormat("BankController::CleanBankPositions > force_request: {0}", bool_3);
		list_0.Clear();
		bool_2 = true;
		if (bool_3)
		{
			bool_1 = true;
			RequestPositions();
		}
	}

	private void ProcessPositions()
	{
		Log.AddLineFormat("BankController::ProcessPositions > bank_available: {0}, bank_positions: {1}, source_type: {2}", bool_2, list_0.Count, bankSourceType_0);
		if (!bool_2)
		{
			MessageWindow.Show(new MessageWindowParams(Localizer.Get("window.bank.warn")));
		}
		else if (list_0.Count > 0)
		{
			OpenBankWindow(list_0);
		}
		else
		{
			RequestPositions();
		}
	}

	private void RequestPositions()
	{
		bool_0 = true;
		Log.AddLine("BankController::RequestPositions");
		stringAssetFile_0 = new StringAssetFile();
		LoadNetworkFileBestHttpOperation loadNetworkFileBestHttpOperation = new LoadNetworkFileBestHttpOperation(String_0, stringAssetFile_0);
		loadNetworkFileBestHttpOperation.Subscribe(OnResponsePositions, Operation.StatusEvent.Complete);
		OperationsManager.OperationsManager_0.Add(loadNetworkFileBestHttpOperation);
	}

	private void OnResponsePositions(Operation operation_0)
	{
		bool_0 = false;
		LoadNetworkFileBestHttpOperation loadNetworkFileBestHttpOperation = operation_0 as LoadNetworkFileBestHttpOperation;
		if (loadNetworkFileBestHttpOperation != null && !loadNetworkFileBestHttpOperation.Boolean_1 && loadNetworkFileBestHttpOperation.BaseAssetFile_0.IsLoaded)
		{
			StringAssetFile stringAssetFile = (StringAssetFile)loadNetworkFileBestHttpOperation.BaseAssetFile_0;
			string text = stringAssetFile.StringData.ToString();
			Dictionary<string, object> dictionary = Json.Deserialize(text) as Dictionary<string, object>;
			if (dictionary.ContainsKey("closed"))
			{
				bool_2 = !Convert.ToBoolean(dictionary["closed"]);
			}
			if (dictionary.ContainsKey("positions"))
			{
				List<object> list = dictionary["positions"] as List<object>;
				foreach (object item2 in list)
				{
					Dictionary<string, object> dictionary2 = item2 as Dictionary<string, object>;
					BankPositionData bankPositionData = new BankPositionData();
					bankPositionData.Int32_0 = Convert.ToInt32(dictionary2["id"]);
					bankPositionData.String_0 = dictionary2["title"] as string;
					bankPositionData.String_1 = dictionary2["sku"] as string;
					bankPositionData.Int32_1 = Convert.ToInt32(dictionary2["money_amount"]);
					bankPositionData.Int32_2 = Convert.ToInt32(dictionary2["money_amount_add"]);
					bankPositionData.Int32_3 = Convert.ToInt32(dictionary2["bonus_id"]);
					bankPositionData.String_2 = dictionary2["bonus_img"] as string;
					bankPositionData.Boolean_0 = Convert.ToBoolean(dictionary2["popular"]);
					bankPositionData.Boolean_1 = Convert.ToBoolean(dictionary2["special"]);
					bankPositionData.Boolean_2 = Convert.ToBoolean(dictionary2["best"]);
					bankPositionData.Boolean_3 = Convert.ToBoolean(dictionary2["published"]);
					bankPositionData.Int32_4 = Convert.ToInt32(dictionary2["time_start"]);
					bankPositionData.Int32_5 = Convert.ToInt32(dictionary2["time_end"]);
					bankPositionData.Single_0 = (float)Convert.ToDouble(dictionary2["price"]);
					bankPositionData.Int32_6 = Convert.ToInt32(dictionary2["bkg_num"]);
					BankPositionData item = bankPositionData;
					list_0.Add(item);
				}
				list_0.Sort((BankPositionData bankPositionData_0, BankPositionData bankPositionData_1) => bankPositionData_0.Int32_1.CompareTo(bankPositionData_1.Int32_1));
				if (!bool_1)
				{
					ProcessPositions();
				}
			}
		}
		bool_1 = false;
	}

	private void OpenBankWindow(List<BankPositionData> list_1)
	{
		SendStatisticOpenBank();
		BankWindowParams bankWindowParams = new BankWindowParams();
		bankWindowParams.List_0 = list_1;
		BankWindow.Show(bankWindowParams);
	}

	private void SendStatisticOpenBank()
	{
		string_1 = EncryptionHelper.Md5Sum(Utility.Double_0.ToString());
		BankWindowOpenedCommand bankWindowOpenedCommand = new BankWindowOpenedCommand();
		bankWindowOpenedCommand.string_0 = string_1;
		AbstractNetworkCommand.Send(bankWindowOpenedCommand);
	}
}
