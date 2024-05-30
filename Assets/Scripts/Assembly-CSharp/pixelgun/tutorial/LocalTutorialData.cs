using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.operations;
using engine.unity;

namespace pixelgun.tutorial
{
	internal static class LocalTutorialData
	{
		public static readonly int int_0;

		public static readonly string string_0;

		public static readonly string string_1;

		public static readonly Vector3 vector3_0;

		public static int int_1;

		public static readonly int int_2;

		public static readonly int int_3;

		private static GameObject gameObject_0;

		private static GameObject gameObject_1;

		private static GameObject gameObject_2;

		private static GameObject gameObject_3;

		private static GameObject gameObject_4;

		private static Dictionary<TUTORIAL_UI_IDS, GameObject> dictionary_0;

		public static GameObject gameObject_5;

		public static Dictionary<GameObject, GameObject> dictionary_1;

		public static SeveralOperations severalOperations_0;

		[CompilerGenerated]
		private static int int_4;

		[CompilerGenerated]
		private static WaveMonstersData waveMonstersData_0;

		[CompilerGenerated]
		private static string string_2;

		[CompilerGenerated]
		private static GameObject gameObject_6;

		[CompilerGenerated]
		private static GameObject gameObject_7;

		[CompilerGenerated]
		private static UICamera uicamera_0;

		public static int Int32_0
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

		public static WaveMonstersData WaveMonstersData_0
		{
			[CompilerGenerated]
			get
			{
				return waveMonstersData_0;
			}
			[CompilerGenerated]
			private set
			{
				waveMonstersData_0 = value;
			}
		}

		public static string String_0
		{
			[CompilerGenerated]
			get
			{
				return string_2;
			}
			[CompilerGenerated]
			private set
			{
				string_2 = value;
			}
		}

		public static GameObject GameObject_0
		{
			[CompilerGenerated]
			get
			{
				return gameObject_6;
			}
			[CompilerGenerated]
			set
			{
				gameObject_6 = value;
			}
		}

		public static bool Boolean_0
		{
			get
			{
				return WaveMonstersData_0 != null && !string.IsNullOrEmpty(String_0);
			}
		}

		public static GameObject GameObject_1
		{
			[CompilerGenerated]
			get
			{
				return gameObject_7;
			}
			[CompilerGenerated]
			set
			{
				gameObject_7 = value;
			}
		}

		public static GameObject GameObject_2
		{
			get
			{
				if (gameObject_0 == null)
				{
					InitCoinBonus();
				}
				return gameObject_0;
			}
		}

		public static GameObject GameObject_3
		{
			get
			{
				if (gameObject_1 == null)
				{
					InitWalkPoint();
				}
				return gameObject_1;
			}
		}

		public static GameObject GameObject_4
		{
			get
			{
				if (gameObject_2 == null)
				{
					IniBoxPoint1();
				}
				return gameObject_2;
			}
		}

		public static GameObject GameObject_5
		{
			get
			{
				if (gameObject_3 == null)
				{
					IniBoxPoint2();
				}
				return gameObject_3;
			}
		}

		public static GameObject GameObject_6
		{
			get
			{
				if (gameObject_4 == null)
				{
					IniBonusAmmoPoint();
				}
				return gameObject_4;
			}
		}

		public static UICamera UICamera_0
		{
			[CompilerGenerated]
			get
			{
				return uicamera_0;
			}
			[CompilerGenerated]
			private set
			{
				uicamera_0 = value;
			}
		}

		static LocalTutorialData()
		{
			int_0 = 294;
			string_0 = "NGUIRoot";
			string_1 = "NGUITutorial";
			vector3_0 = new Vector3(10f, 0f, 17f);
			int_1 = 0;
			int_2 = 285;
			int_3 = 7;
			gameObject_0 = null;
			gameObject_1 = null;
			gameObject_2 = null;
			gameObject_3 = null;
			gameObject_4 = null;
			dictionary_0 = new Dictionary<TUTORIAL_UI_IDS, GameObject>();
			dictionary_1 = new Dictionary<GameObject, GameObject>();
			severalOperations_0 = null;
			InitMapData();
			InitUIElements();
		}

		public static GameObject GetTutorialUIElement(TUTORIAL_UI_IDS tutorial_UI_IDS_0)
		{
			GameObject value = null;
			dictionary_0.TryGetValue(tutorial_UI_IDS_0, out value);
			return value;
		}

		public static void SetTutorialUIElement(TUTORIAL_UI_IDS tutorial_UI_IDS_0, GameObject gameObject_8)
		{
			if (dictionary_0.ContainsKey(tutorial_UI_IDS_0) || !(gameObject_8 == null))
			{
				if (gameObject_8 == null)
				{
					dictionary_0.Remove(tutorial_UI_IDS_0);
				}
				else
				{
					dictionary_0.Add(tutorial_UI_IDS_0, gameObject_8);
				}
			}
		}

		private static List<ModeData> modeData
		{
			get
			{
				if (_modeData == null)
				{
					List<ModeData> data = new List<ModeData>();
					Modes modes = Resources.Load<Modes>("Modes");

					foreach (Modes.Mode mode in modes.modes)
					{
						data.Add(mode.ToModeData());
					}

					_modeData = data;
				}

				return _modeData;
			}
		}

		private static List<ModeData> _modeData;

		private static List<MapData> mapData
		{
			get
			{
				if (_mapData == null)
				{
					List<MapData> data = new List<MapData>();
					Maps maps = Resources.Load<Maps>("Maps");

					foreach (Maps.Map map in maps.maps)
					{
						data.Add(map.ToMapData());
					}

					_mapData = data;
				}

				return _mapData;
			}
		}

		private static List<MapData> _mapData;

		private static void InitMapData()
		{
			List<ModeData> modesByType = LocalTutorialData.modeData.FindAll(x => x.ModeType_0 == ModeType.TUTORIAL);//ModesController.ModesController_0.GetModesByType(ModeType.TUTORIAL);
			if (modesByType == null || modesByType.Count == 0)
			{
				return;
			}
			ModeData modeData = modesByType[0];
			MapData objectByKey = mapData.Find(x => x.Int32_0 == modeData.Int32_1);//MapStorage.Get.Storage.GetObjectByKey(modeData.Int32_1);
			if (objectByKey == null)
			{
				return;
			}
			String_0 = objectByKey.String_1;
			List<WaveMonstersData> list = WaveMonstersStorage.Get.Storage.Search(0, modeData.Int32_0);
			if (list == null || list.Count == 0)
			{
				return;
			}
			int num = 0;
			while (true)
			{
				if (num < list.Count)
				{
					if (list[0].Boolean_0)
					{
						break;
					}
					num++;
					continue;
				}
				return;
			}
			WaveMonstersData_0 = list[0];
		}

		private static void InitCoinBonus()
		{
			gameObject_0 = GameObject.FindGameObjectWithTag("GrenadeTutorBonus");
			gameObject_0.GetComponent<CoinBonus>().SetPlayer();
		}

		private static void InitWalkPoint()
		{
			gameObject_1 = GameObject.FindGameObjectWithTag("TutorialWalkPoint");
		}

		private static void IniBoxPoint1()
		{
			gameObject_2 = GameObject.FindGameObjectWithTag("TutorialBoxPoint1");
		}

		private static void IniBoxPoint2()
		{
			gameObject_3 = GameObject.FindGameObjectWithTag("TutorialBoxPoint2");
		}

		private static void IniBonusAmmoPoint()
		{
			gameObject_4 = GameObject.FindGameObjectWithTag("TutorialAmmoPoint");
		}

		public static void InitUIElements()
		{
			UICamera_0 = ScreenController.ScreenController_0.Camera_0.GetComponent<UICamera>();
		}
	}
}
