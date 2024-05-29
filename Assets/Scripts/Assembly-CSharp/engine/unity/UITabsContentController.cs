using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace engine.unity
{
	public class UITabsContentController : MonoBehaviour
	{
		public Color activeTabColor;

		public Color inactiveTabColor;

		public UITab defaultTab;

		public bool activateDefaultOnStart = true;

		public List<UITab> tabs;

		public List<UITabContent> contents;

		private int int_0;

		public Action<int> onTabActive = delegate
		{
		};

		public Action<int> onContentActive = delegate
		{
		};

		[CompilerGenerated]
		private static Action<int> action_0;

		[CompilerGenerated]
		private static Action<int> action_1;

		private void Awake()
		{
			for (int i = 0; i < tabs.Count; i++)
			{
				tabs[i].UITabsContentController_0 = this;
			}
		}

		private void Start()
		{
			if (defaultTab != null && activateDefaultOnStart)
			{
				Activate(defaultTab);
			}
		}

		public void Activate(UITab uitab_0)
		{
			for (int i = 0; i < tabs.Count; i++)
			{
				if (tabs[i].Equals(uitab_0))
				{
					Activate(i);
				}
			}
		}

		public void Activate(int int_1)
		{
			int_0 = int_1;
			ActivateTab();
			ActivateContent();
		}

		public void DisactiveAll()
		{
			for (int i = 0; i < tabs.Count; i++)
			{
				tabs[i].Activate(false);
			}
		}

		private void ActivateTab()
		{
			if (tabs.Count > int_0)
			{
				for (int i = 0; i < tabs.Count; i++)
				{
					tabs[i].Activate(i == int_0);
				}
			}
			onTabActive(int_0);
		}

		private void ActivateContent()
		{
			if (contents.Count > int_0)
			{
				for (int i = 0; i < contents.Count; i++)
				{
					contents[i].Activate(i == int_0);
				}
			}
			onContentActive(int_0);
		}
	}
}
