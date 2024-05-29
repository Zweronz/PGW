using System.Runtime.CompilerServices;
using UnityEngine;

namespace engine.unity
{
	public class UITab : MonoBehaviour
	{
		public UITabState defaultState;

		public UITabState selectedState;

		public bool locked;

		[CompilerGenerated]
		private UITabsContentController uitabsContentController_0;

		public UITabsContentController UITabsContentController_0
		{
			[CompilerGenerated]
			get
			{
				return uitabsContentController_0;
			}
			[CompilerGenerated]
			set
			{
				uitabsContentController_0 = value;
			}
		}

		private void Awake()
		{
			if (selectedState != null)
			{
				NGUITools.SetActive(selectedState.gameObject, false);
			}
		}

		public virtual void Activate(bool bool_0)
		{
			if (defaultState == null)
			{
				defaultState = selectedState;
			}
			if (defaultState == null)
			{
				return;
			}
			UITabState uITabState = ((!bool_0) ? defaultState : selectedState);
			if (uITabState == null)
			{
				defaultState.SetColor(bool_0, UITabsContentController_0.activeTabColor, UITabsContentController_0.inactiveTabColor);
				return;
			}
			UITabState uITabState2 = ((!(uITabState == defaultState)) ? defaultState : selectedState);
			if (uITabState2 != null)
			{
				NGUITools.SetActive(uITabState2.gameObject, false);
			}
			uITabState.SetColor(bool_0, UITabsContentController_0.activeTabColor, UITabsContentController_0.inactiveTabColor);
			NGUITools.SetActive(uITabState.gameObject, true);
		}

		public void SetText(string string_0)
		{
			if (defaultState != null)
			{
				defaultState.SetText(string_0);
			}
			if (selectedState != null)
			{
				selectedState.SetText(string_0);
			}
		}

		public void SetLocked(bool bool_0)
		{
			locked = bool_0;
		}

		public void OnClick()
		{
			if (!locked)
			{
				UITabsContentController_0.Activate(this);
			}
		}
	}
}
