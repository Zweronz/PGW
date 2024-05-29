using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace engine.unity
{
	public class BaseWindow : MonoBehaviour
	{
		public sealed class Parameters
		{
			public string string_0;

			public WindowQueue.Type type_0;

			public bool bool_0;

			public float float_0;

			public int int_0;

			public ScreenController.ScreenType screenType_0;

			public int int_1;

			public bool bool_1;

			public bool bool_2;

			public bool bool_3;

			public WindowController.GameEvent gameEvent_0;

			public bool bool_4;

			public bool bool_5;

			public bool bool_6;

			public bool bool_7;
		}

		[Serializable]
		public sealed class ChildPanel
		{
			public UIPanel Panel;

			public int RelativeZ = 1;
		}

		public ChildPanel[] ChildPanels;

		private Dictionary<KeyCode, Action> dictionary_0 = new Dictionary<KeyCode, Action>();

		[CompilerGenerated]
		private WindowShowParameters windowShowParameters_0;

		[CompilerGenerated]
		private Parameters parameters_0;

		[CompilerGenerated]
		private bool bool_0;

		public WindowShowParameters WindowShowParameters_0
		{
			[CompilerGenerated]
			get
			{
				return windowShowParameters_0;
			}
			[CompilerGenerated]
			set
			{
				windowShowParameters_0 = value;
			}
		}

		public Parameters Parameters_0
		{
			[CompilerGenerated]
			get
			{
				return parameters_0;
			}
			[CompilerGenerated]
			private set
			{
				parameters_0 = value;
			}
		}

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

		public UIPanel UIPanel_0
		{
			get
			{
				return GetComponent<UIPanel>();
			}
		}

		protected virtual void Awake()
		{
			Parameters_0 = new Parameters();
			Parameters_0.bool_0 = false;
			Parameters_0.bool_3 = false;
			Parameters_0.bool_2 = true;
			Parameters_0.bool_5 = true;
			Parameters_0.bool_6 = true;
			Parameters_0.bool_7 = false;
		}

		public void Release()
		{
			if (Boolean_0)
			{
				Hide();
			}
			dictionary_0.Clear();
			base.transform.parent = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		public void Hide()
		{
			WindowController.WindowController_0.HideWindow(this);
		}

		public virtual bool isShowAvailable()
		{
			return true;
		}

		public virtual void OnShow()
		{
		}

		public virtual void OnHide()
		{
		}

		public virtual void OnBecameActive()
		{
		}

		public virtual void OnAddedToStack()
		{
		}

		protected static BaseWindow Load(string string_0)
		{
			if (string.IsNullOrEmpty(string_0))
			{
				throw new ArgumentNullException("Prefab name not set! wtf?");
			}
			GameObject gameObject = ScreenController.ScreenController_0.LoadUI(string_0);
			return gameObject.GetComponent<BaseWindow>();
		}

		protected void AddInputKey(KeyCode keyCode_0, Action action_0)
		{
			if (!dictionary_0.ContainsKey(keyCode_0))
			{
				dictionary_0.Add(keyCode_0, null);
			}
			dictionary_0[keyCode_0] = action_0;
		}

		protected void InternalShow(WindowShowParameters windowShowParameters_1 = null)
		{
			if (Parameters_0.bool_6)
			{
				AddInputKey(KeyCode.Escape, delegate
				{
					if (Boolean_0)
					{
						Hide();
					}
				});
			}
			WindowController.WindowController_0.ShowWindow(this, windowShowParameters_1);
		}

		protected virtual void Update()
		{
			UpdateKeyInput();
		}

		private void UpdateKeyInput()
		{
			if (WindowController.WindowController_0.BaseWindow_0 != this || dictionary_0.Count == 0)
			{
				return;
			}
			foreach (KeyValuePair<KeyCode, Action> item in dictionary_0)
			{
				if (Input.GetKeyUp(item.Key))
				{
					item.Value();
					break;
				}
			}
		}
	}
}
