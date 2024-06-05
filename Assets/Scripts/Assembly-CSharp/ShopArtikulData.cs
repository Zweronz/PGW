using System.Runtime.CompilerServices;
using ProtoBuf;
using engine.data;
using engine.helpers;
using engine.network;

[StorageDataKey(typeof(int))]
[ProtoContract]
public sealed class ShopArtikulData
{
	//id
	[CompilerGenerated]
	private int int_0;

	//also id??
	[CompilerGenerated]
	private int int_1;

	[CompilerGenerated]
	private SlotType slotType_0;

	[CompilerGenerated]
	private MoneyType moneyType_0;
	
	//price
	[CompilerGenerated]
	private int int_2;

	//other price
	[CompilerGenerated]
	private int int_3;

	//idk has upgrades or something
	[CompilerGenerated]
	private bool bool_0;

	//also price idk
	[CompilerGenerated]
	private int int_4;

	[CompilerGenerated]
	private NeedsData needsData_0;

	//idk
	[CompilerGenerated]
	private int int_5;

	[CompilerGenerated]
	private bool bool_1;

	//current upgrade price or smth
	[CompilerGenerated]
	private int int_6;

	[CompilerGenerated]
	private bool bool_2;

	[CompilerGenerated]
	private bool bool_3;

	[CompilerGenerated]
	private bool bool_4;
	
	[CompilerGenerated]
	private int int_7;

	//something price related idk...
	[CompilerGenerated]
	private int int_8;

	[CompilerGenerated]
	private int int_9;

	[CompilerGenerated]
	private int int_10;

	[CompilerGenerated]
	private bool bool_5;

	[ProtoMember(1)]
	public int Int32_0
	{
		[CompilerGenerated]
		get
		{
			return int_0;
		}
		[CompilerGenerated]
		set
		{
			int_0 = value;
		}
	}

	[ProtoMember(2)]
	public int Int32_1
	{
		[CompilerGenerated]
		get
		{
			return int_1;
		}
		[CompilerGenerated]
		set
		{
			int_1 = value;
		}
	}

	[ProtoMember(3)]
	public SlotType SlotType_0
	{
		[CompilerGenerated]
		get
		{
			return slotType_0;
		}
		[CompilerGenerated]
		set
		{
			slotType_0 = value;
		}
	}

	[ProtoMember(4)]
	public MoneyType MoneyType_0
	{
		[CompilerGenerated]
		get
		{
			return moneyType_0;
		}
		[CompilerGenerated]
		set
		{
			moneyType_0 = value;
		}
	}

	[ProtoMember(5)]
	public int Int32_2
	{
		[CompilerGenerated]
		get
		{
			return int_2;
		}
		[CompilerGenerated]
		set
		{
			int_2 = value;
		}
	}

	[ProtoMember(6)]
	public int Int32_3
	{
		[CompilerGenerated]
		get
		{
			return int_3;
		}
		[CompilerGenerated]
		set
		{
			int_3 = value;
		}
	}

	[ProtoMember(7)]
	public bool Boolean_0
	{
		[CompilerGenerated]
		get
		{
			return bool_0;
		}
		[CompilerGenerated]
		set
		{
			bool_0 = value;
		}
	}

	[ProtoMember(8)]
	public int Int32_4
	{
		[CompilerGenerated]
		get
		{
			return int_4;
		}
		[CompilerGenerated]
		set
		{
			int_4 = value;
		}
	}

	[ProtoMember(9)]
	public NeedsData NeedsData_0
	{
		[CompilerGenerated]
		get
		{
			return needsData_0;
		}
		[CompilerGenerated]
		set
		{
			needsData_0 = value;
		}
	}

	[ProtoMember(10)]
	public int Int32_5
	{
		[CompilerGenerated]
		get
		{
			return int_5;
		}
		[CompilerGenerated]
		set
		{
			int_5 = value;
		}
	}

	[ProtoMember(11)]
	public bool Boolean_1
	{
		[CompilerGenerated]
		get
		{
			return bool_1;
		}
		[CompilerGenerated]
		set
		{
			bool_1 = value;
		}
	}

	[ProtoMember(12)]
	public int Int32_6
	{
		[CompilerGenerated]
		get
		{
			return int_6;
		}
		[CompilerGenerated]
		set
		{
			int_6 = value;
		}
	}

	[ProtoMember(13)]
	public bool Boolean_2
	{
		[CompilerGenerated]
		get
		{
			return bool_2;
		}
		[CompilerGenerated]
		set
		{
			bool_2 = value;
		}
	}

	[ProtoMember(14)]
	public bool Boolean_3
	{
		[CompilerGenerated]
		get
		{
			return bool_3;
		}
		[CompilerGenerated]
		set
		{
			bool_3 = value;
		}
	}

	[ProtoMember(15)]
	public bool Boolean_4
	{
		[CompilerGenerated]
		get
		{
			return bool_4;
		}
		[CompilerGenerated]
		set
		{
			bool_4 = value;
		}
	}

	[ProtoMember(16)]
	public int Int32_7
	{
		[CompilerGenerated]
		get
		{
			return int_7;
		}
		[CompilerGenerated]
		set
		{
			int_7 = value;
		}
	}

	[ProtoMember(17)]
	public int Int32_8
	{
		[CompilerGenerated]
		get
		{
			return int_8;
		}
		[CompilerGenerated]
		set
		{
			int_8 = value;
		}
	}

	[ProtoMember(18)]
	public int Int32_9
	{
		[CompilerGenerated]
		get
		{
			return int_9;
		}
		[CompilerGenerated]
		set
		{
			int_9 = value;
		}
	}

	[ProtoMember(19)]
	public int Int32_10
	{
		[CompilerGenerated]
		get
		{
			return int_10;
		}
		[CompilerGenerated]
		set
		{
			int_10 = value;
		}
	}

	public bool Boolean_5
	{
		[CompilerGenerated]
		get
		{
			return bool_5;
		}
		[CompilerGenerated]
		set
		{
			bool_5 = value;
		}
	}

	public bool Boolean_6
	{
		get
		{
			return ShopArtikulController.ShopArtikulController_0.isArtikulNew(Int32_1) || Boolean_2;
		}
	}

	public bool Boolean_7
	{
		get
		{
			if (!Boolean_1)
			{
				return false;
			}
			if (UserController.UserController_0.UserData_0 != null && UserController.UserController_0.UserData_0.user_0 != null)
			{
				int num = UserController.UserController_0.UserData_0.user_0.int_2;
				if (num < Int32_9)
				{
					return false;
				}
				if (num > Int32_10 && Int32_10 > 0)
				{
					return false;
				}
				return true;
			}
			return false;
		}
	}

	public ArtikulData GetArtikul()
	{
		return ArtikulController.ArtikulController_0.GetArtikul(Int32_1);
	}

	private int GetUpgradePrice()
	{
		return (Int32_6 <= 0) ? GetShopPrice() : Int32_6;
	}

	private int GetShopPrice()
	{
		return (!Boolean_0) ? Int32_2 : Int32_3;
	}

	public int GetPrice()
	{
		if (GetArtikul() != null && GetArtikul().Int32_2 != 0 && UserController.UserController_0.GetUserArtikulByArtikulId(GetArtikul().Int32_2) != null && UserController.UserController_0.GetUserArtikulByArtikulId(Int32_1) == null)
		{
			return GetUpgradePrice();
		}
		return GetShopPrice();
	}

	public bool CanBuy(bool bool_6 = true)
	{
		UserData userData_ = UsersData.UsersData_0.UserData_0;
		if (userData_ == null)
		{
			Log.AddLine(string.Format("ShopArtikulData::CanBuy -> userData is null"));
			return false;
		}
		if (userData_.user_0 == null)
		{
			Log.AddLine(string.Format("ShopArtikulData::CanBuy -> userData.user is null"));
			return false;
		}
		ArtikulData artikul = GetArtikul();
		if (artikul == null && Int32_7 == 0)
		{
			Log.AddLine(string.Format("ShopArtikulData::CanBuy -> GetArtikul() is null"));
			return false;
		}
		if (userData_.user_0.dictionary_0.ContainsKey(MoneyType_0))
		{
			int num = userData_.user_0.dictionary_0[MoneyType_0];
			int price = GetPrice();
			if (num < price && bool_6 && (artikul == null || UserController.UserController_0.GetUserArtikulByArtikulId(artikul.Int32_0) != null || artikul.Int32_5 <= 0 || !Boolean_4))
			{
				return false;
			}
			if (artikul != null && artikul.Int32_3 > 0 && UserController.UserController_0.GetUserArtikulCount(artikul.Int32_0) + Int32_4 > artikul.Int32_3)
			{
				return false;
			}
			if (NeedsData_0 != null && !NeedsData_0.Check())
			{
				return false;
			}
			if (artikul != null && artikul.NeedsData_0 != null && !artikul.NeedsData_0.Check())
			{
				return false;
			}
			return true;
		}
		return false;
	}

	public void Buy(bool bool_6 = true, ShopArtikulController.SourceBuyType sourceBuyType_0 = ShopArtikulController.SourceBuyType.TYPE_SHOP_WND)
	{
		BuyArtikulNetworkCommand buyArtikulNetworkCommand = new BuyArtikulNetworkCommand();
		buyArtikulNetworkCommand.int_1 = Int32_0;
		buyArtikulNetworkCommand.bool_0 = bool_6;
		buyArtikulNetworkCommand.int_2 = (int)sourceBuyType_0;
		buyArtikulNetworkCommand.Spoof();
	}

	public void BuyUpgrade()
	{
		ArtikulData artikul = ArtikulController.ArtikulController_0.GetArtikul(Int32_1);
		if (artikul == null)
		{
			Log.AddLine(string.Format("ShopArtikulData::BuyUpgrade > artikul {0} is null", Int32_1));
			return;
		}
		BuyArtikulNetworkCommand buyArtikulNetworkCommand = new BuyArtikulNetworkCommand();
		buyArtikulNetworkCommand.int_1 = Int32_0;
		buyArtikulNetworkCommand.bool_0 = true;
		buyArtikulNetworkCommand.Spoof();
	}
}
