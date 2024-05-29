using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.events;
using engine.system;

namespace engine.unity
{
	public sealed class ScreenController
	{
		public sealed class SceneUIRootParams
		{
			public string string_0;

			public int int_0;

			public SceneUIRootParams(string string_1, int int_1)
			{
				string_0 = string_1;
				int_0 = int_1;
			}
		}

		public enum DeviceType
		{
			STANDALONE = 0,
			TABLET = 1,
			PHONE = 2
		}

		public enum DeviceResolution
		{
			HD = 0,
			SD = 1
		}

		public enum ScreenType
		{
			UNUSED = 0
		}

		private static readonly string string_0 = "UIRoot";

		private static readonly string string_1 = string.Format("{0}@{1}", string_0, ISystemInfo.ISystemInfo_0.String_1);

		private static ScreenController screenController_0;

		private static Dictionary<string, SceneUIRootParams> dictionary_0 = new Dictionary<string, SceneUIRootParams>();

		private SceneUIRootParams sceneUIRootParams_0;

		private GameObject gameObject_0;

		[CompilerGenerated]
		private GameObject gameObject_1;

		[CompilerGenerated]
		private Camera camera_0;

		[CompilerGenerated]
		private AbstractScreen abstractScreen_0;

		public static ScreenController ScreenController_0
		{
			get
			{
				if (screenController_0 == null)
				{
					screenController_0 = new ScreenController();
				}
				return screenController_0;
			}
		}

		public GameObject GameObject_0
		{
			[CompilerGenerated]
			get
			{
				return gameObject_1;
			}
			[CompilerGenerated]
			private set
			{
				gameObject_1 = value;
			}
		}

		public bool Boolean_0
		{
			get
			{
				return GameObject_0;
			}
		}

		public Camera Camera_0
		{
			[CompilerGenerated]
			get
			{
				return camera_0;
			}
			[CompilerGenerated]
			private set
			{
				camera_0 = value;
			}
		}

		public AbstractScreen AbstractScreen_0
		{
			[CompilerGenerated]
			get
			{
				return abstractScreen_0;
			}
			[CompilerGenerated]
			private set
			{
				abstractScreen_0 = value;
			}
		}

		public float Single_0
		{
			get
			{
				return (float)GameObject_0.GetComponent<UIRoot>().Int32_0 / (float)Screen.height;
			}
		}

		private ScreenController()
		{
			InitUIRootDefault();
			Object.DontDestroyOnLoad(MonoSingleton<UpdateEventDispatcher>.Prop_0);
			DependSceneEvent<ChangeSceneUnityEvent>.GlobalSubscribe(ChangeScene);
			ChangeScene();
		}

		public void ShowScreen(AbstractScreen abstractScreen_1)
		{
			if (AbstractScreen_0 != null)
			{
				AbstractScreen_0.Release();
			}
			AbstractScreen_0 = abstractScreen_1;
			AbstractScreen_0.Init();
		}

		public void HideActiveScreen()
		{
			if (AbstractScreen_0 != null)
			{
				AbstractScreen_0.Release();
				AbstractScreen_0 = null;
			}
		}

		public GameObject LoadUI(string string_2)
		{
			string text = ISystemInfo.ISystemInfo_0.String_1;
			string path = string.Format("UI/{0}/{1}@{0}", text, string_2);
			GameObject gameObject = Resources.Load(path) as GameObject;
			if (gameObject == null)
			{
				text = "Common";
				path = string.Format("UI/{0}/{1}", text, string_2);
				gameObject = Resources.Load(path) as GameObject;
			}
			GameObject gameObject2 = NGUITools.AddChild(GameObject_0, gameObject);
			gameObject2.name = string.Format("{0}@{1}", string_2, text);
			NGUITools.SetActive(gameObject2.gameObject, false, false);
			return gameObject2;
		}

		public static void AddSceneUIRootInLevel(string string_2, SceneUIRootParams sceneUIRootParams_1)
		{
			if (!string.IsNullOrEmpty(string_2) && sceneUIRootParams_1 != null)
			{
				if (dictionary_0.ContainsKey(string_2))
				{
					dictionary_0.Remove(string_2);
				}
				dictionary_0.Add(string_2, sceneUIRootParams_1);
			}
		}

		private void UpdateUIRoot()
		{
			string string_;
			if (CheckNeedSwitchUIRoot(out string_))
			{
				WindowController.WindowController_0.HideAllWindow(false);
				if (GameObject_0 != null && GameObject_0 == gameObject_0)
				{
					NGUITools.SetActive(gameObject_0, false, false);
				}
				GameObject_0 = null;
				Camera_0 = null;
				if (!ParseUIRootOnScene(string_))
				{
					GameObject_0 = gameObject_0;
					NGUITools.SetActive(GameObject_0, true, false);
					Camera_0 = GameObject_0.GetComponentInChildren<Camera>();
				}
				Camera_0.depth = ((sceneUIRootParams_0 == null || sceneUIRootParams_0.int_0 == -1) ? Camera_0.depth : ((float)sceneUIRootParams_0.int_0));
			}
		}

		private void InitUIRootDefault()
		{
			gameObject_0 = (ParseUIRootOnScene(string_1) ? (gameObject_0 = GameObject_0) : (gameObject_0 = LoadUI(string_0)));
			Object.DontDestroyOnLoad(gameObject_0);
		}

		private void ChangeScene()
		{
			dictionary_0.TryGetValue(Application.loadedLevelName, out sceneUIRootParams_0);
			UpdateUIRoot();
		}

		private bool CheckNeedSwitchUIRoot(out string string_2)
		{
			if (sceneUIRootParams_0 != null && !string.IsNullOrEmpty(sceneUIRootParams_0.string_0))
			{
				string_2 = sceneUIRootParams_0.string_0;
			}
			else
			{
				string_2 = string_1;
			}
			if (!(GameObject_0 == null) && !(GameObject_0.name != string_2))
			{
				return false;
			}
			return true;
		}

		private bool ParseUIRootOnScene(string string_2)
		{
			UIRoot[] array = Object.FindObjectsOfType<UIRoot>();
			if (!string.IsNullOrEmpty(string_2))
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].gameObject.name == string_2)
					{
						GameObject_0 = array[i].gameObject;
						break;
					}
				}
				Camera_0 = ((!(GameObject_0 != null)) ? null : GameObject_0.GetComponentInChildren<Camera>());
				if (Camera_0 != null)
				{
					return true;
				}
			}
			GameObject_0 = null;
			Camera_0 = null;
			return false;
		}

		public static void Reset()
		{
			if (screenController_0 != null)
			{
				screenController_0.HideActiveScreen();
			}
			screenController_0 = null;
		}
	}
}
