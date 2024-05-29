using System.Collections.Generic;
using UnityEngine;
using engine.events;

namespace engine.unity
{
	public sealed class WindowController : BaseEvent
	{
		public enum EventType
		{
			SHOW_FIRST_WINDOW = 0,
			HIDE_LAST_WINDOW = 1
		}

		public enum GameEvent
		{
			UNUSED = 0,
			SHOW_PREV_WINDOW = 1,
			IN_MAIN_MENU = 2,
			TUTORIAL_END = 3,
			BATTLE_OVER_WINDOW_SHOW = 4,
			STOCK_GACHA_WND_HIDE = 5
		}

		private static WindowController windowController_0;

		private static readonly int int_0 = 100;

		private WindowFader windowFader_0;

		private WindowQueue.Item item_0;

		private List<BaseWindow> list_1 = new List<BaseWindow>();

		private List<WindowQueue.Item> list_2 = new List<WindowQueue.Item>();

		private List<WindowQueue> list_3 = new List<WindowQueue>();

		private int int_1 = int_0;

		public static WindowController WindowController_0
		{
			get
			{
				if (windowController_0 == null)
				{
					windowController_0 = new WindowController();
				}
				return windowController_0;
			}
		}

		public WindowFader WindowFader_0
		{
			get
			{
				if (windowFader_0 == null)
				{
					windowFader_0 = ScreenController.ScreenController_0.LoadUI("Fader").GetComponent<WindowFader>();
				}
				return windowFader_0;
			}
		}

		public BaseWindow BaseWindow_0
		{
			get
			{
				return (item_0 != null) ? item_0.BaseWindow_0 : null;
			}
		}

		public int Int32_0
		{
			get
			{
				return list_1.Count;
			}
		}

		private WindowController()
		{
			list_3.Add(new WindowQueue());
		}

		public void ShowWindow(BaseWindow baseWindow_0, WindowShowParameters windowShowParameters_0 = null)
		{
			if (baseWindow_0.Parameters_0.gameEvent_0 != 0)
			{
				AddToStack(baseWindow_0, windowShowParameters_0);
				CheckShow();
			}
			else
			{
				AddToQueue(baseWindow_0, windowShowParameters_0);
				ShowActive();
			}
		}

		public void HideWindow(BaseWindow baseWindow_0)
		{
			if (baseWindow_0 == null || item_0 == null)
			{
				return;
			}
			bool flag = baseWindow_0 != item_0.BaseWindow_0;
			BaseWindow baseWindow_ = item_0.BaseWindow_0;
			for (int i = 0; i < list_3.Count; i++)
			{
				WindowQueue windowQueue = list_3[i];
				if (windowQueue.IsInQueue(baseWindow_0))
				{
					list_1.Remove(baseWindow_0);
					baseWindow_0.Boolean_0 = false;
					NGUITools.SetActive(baseWindow_0.gameObject, false);
					baseWindow_0.OnHide();
					windowQueue.Remove(baseWindow_0);
					if (baseWindow_0.Parameters_0.bool_2)
					{
						baseWindow_0.Release();
					}
					break;
				}
			}
			if (!flag)
			{
				item_0 = null;
			}
			CheckQueues();
			if (!flag)
			{
				CheckShow();
				UpdateDepth();
				if (item_0 != null && item_0.BaseWindow_0 != baseWindow_)
				{
					item_0.BaseWindow_0.OnBecameActive();
				}
				UpdateVisibleInvisibleStuff();
				if (list_1.Count == 0)
				{
					Dispatch(EventType.HIDE_LAST_WINDOW);
				}
			}
		}

		public void HideActiveWindow()
		{
			if (item_0 != null)
			{
				HideWindow(item_0.BaseWindow_0);
			}
		}

		public void ForceHideAllWindow()
		{
			HideAllWindow(false, true);
		}

		public void HideAllWindow(bool bool_0 = true, bool bool_1 = false)
		{
			while (item_0 != null && ((!item_0.BaseWindow_0.Parameters_0.bool_0 && bool_0) || !bool_0))
			{
				HideActiveWindow();
			}
			if (bool_1 && list_2 != null)
			{
				ClearStack();
			}
		}

		public void DispatchEvent(GameEvent gameEvent_0)
		{
			CheckShow(CheckStack(gameEvent_0));
		}

		private void ClearStack()
		{
			for (int i = 0; i < list_2.Count; i++)
			{
				list_2[i].BaseWindow_0.OnHide();
				list_2[i].BaseWindow_0.Release();
			}
			list_2.Clear();
		}

		private void AddToStack(BaseWindow baseWindow_0, WindowShowParameters windowShowParameters_0)
		{
			baseWindow_0.Parameters_0.bool_1 = true;
			baseWindow_0.OnAddedToStack();
			list_2.Add(new WindowQueue.Item(baseWindow_0, windowShowParameters_0));
		}

		private bool CheckStack(GameEvent gameEvent_0)
		{
			if (ScreenController.ScreenController_0.AbstractScreen_0 == null)
			{
				return false;
			}
			int num = 0;
			bool result = false;
			bool flag = false;
			WindowQueue.Item item = null;
			while (num < list_2.Count)
			{
				item = list_2[num];
				if (item.BaseWindow_0.Parameters_0.gameEvent_0 == gameEvent_0)
				{
					if (!flag)
					{
						flag = true;
						result = item.BaseWindow_0.Parameters_0.bool_4;
					}
					item.BaseWindow_0.Parameters_0.bool_1 = false;
					AddToQueue(item.BaseWindow_0, item.WindowShowParameters_0);
					list_2.RemoveAt(num);
				}
				else
				{
					num++;
				}
			}
			return result;
		}

		private void AddToQueue(BaseWindow baseWindow_0, WindowShowParameters windowShowParameters_0 = null)
		{
			WindowQueue windowQueue = null;
			switch (baseWindow_0.Parameters_0.type_0)
			{
			case WindowQueue.Type.Default:
				windowQueue = list_3[0];
				break;
			case WindowQueue.Type.New:
				windowQueue = new WindowQueue();
				list_3.Add(windowQueue);
				break;
			case WindowQueue.Type.Top:
				windowQueue = list_3[list_3.Count - 1];
				break;
			}
			windowQueue.Add(baseWindow_0, windowShowParameters_0);
		}

		private void CheckQueues()
		{
			int num = 0;
			for (int i = 1; i < list_3.Count; i++)
			{
				if (list_3[i].Int32_0 == 0)
				{
					num = i;
					break;
				}
			}
			if (num > 0)
			{
				list_3.RemoveAt(num);
			}
		}

		private void UpdateDepth()
		{
			if (list_1.Count == 0)
			{
				int_1 = int_0;
				WindowFader_0.Hide();
				return;
			}
			int num = int_1;
			for (int i = 0; i < list_1.Count; i++)
			{
				if (list_1[i] == BaseWindow_0 || list_1[i] == null || list_1[i].UIPanel_0 == null)
				{
					continue;
				}
				list_1[i].UIPanel_0.Int32_1 = num;
				int num2 = num;
				if (list_1[i].ChildPanels.Length > 0)
				{
					int num3 = list_1[i].ChildPanels.Length;
					for (int j = 0; j < num3; j++)
					{
						int num4 = num + list_1[i].ChildPanels[j].RelativeZ;
						list_1[i].ChildPanels[j].Panel.Int32_1 = num4;
						num2 = Mathf.Max(num2, num4);
					}
				}
				num = 1 + num2;
			}
			if (BaseWindow_0.Parameters_0.bool_5)
			{
				WindowFader_0.GetComponent<UIPanel>().Int32_1 = num;
				num++;
				WindowFader_0.Show();
			}
			else
			{
				WindowFader_0.Hide();
			}
			BaseWindow_0.UIPanel_0.Int32_1 = num;
			int num5 = num;
			if (BaseWindow_0.ChildPanels.Length > 0)
			{
				int num6 = BaseWindow_0.ChildPanels.Length;
				for (int k = 0; k < num6; k++)
				{
					int num7 = num + BaseWindow_0.ChildPanels[k].RelativeZ;
					BaseWindow_0.ChildPanels[k].Panel.Int32_1 = num7;
					num5 = Mathf.Max(num5, num7);
				}
			}
			num = 1 + num5;
			int_1 = num;
		}

		private void CheckShow(bool bool_0 = false)
		{
			if (!bool_0 && item_0 != null)
			{
				return;
			}
			int num = list_3.Count;
			while (true)
			{
				if (num != 0)
				{
					item_0 = list_3[num - 1].GetLastItem();
					if (item_0 != null)
					{
						break;
					}
					num--;
					continue;
				}
				return;
			}
			ShowActive(false);
		}

		private void ShowActive(bool bool_0 = true)
		{
			if (bool_0)
			{
				for (int num = list_3.Count; num != 0; num--)
				{
					item_0 = list_3[num - 1].GetLastItem();
					if (item_0 != null)
					{
						break;
					}
				}
			}
			if (item_0 != null && !item_0.BaseWindow_0.Boolean_0)
			{
				list_1.Add(item_0.BaseWindow_0);
			}
			int_1 = int_0;
			UpdateDepth();
			UpdateVisibleInvisibleStuff();
			if (item_0 != null && !item_0.BaseWindow_0.Boolean_0)
			{
				item_0.BaseWindow_0.Boolean_0 = true;
				NGUITools.SetActive(item_0.BaseWindow_0.gameObject, true);
				item_0.BaseWindow_0.WindowShowParameters_0 = item_0.WindowShowParameters_0;
				item_0.BaseWindow_0.OnShow();
				if (list_1.Count == 1)
				{
					Dispatch(EventType.SHOW_FIRST_WINDOW);
				}
			}
		}

		private void UpdateVisibleInvisibleStuff()
		{
			if (item_0 != null && item_0.BaseWindow_0.Parameters_0.bool_3)
			{
				SetVisibleNotActiveStuff(false);
			}
			else if (IsWholeScreenInShownWindows())
			{
				SetVisibleNotActiveStuff(true);
			}
			else
			{
				SetVisibleNotActiveStuff(true);
			}
		}

		private bool IsWholeScreenInShownWindows()
		{
			int num = 0;
			while (true)
			{
				if (num < list_1.Count)
				{
					if (list_1[num].Parameters_0.bool_3)
					{
						break;
					}
					num++;
					continue;
				}
				return false;
			}
			return true;
		}

		private void SetVisibleNotActiveStuff(bool bool_0)
		{
			BaseWindow baseWindow = null;
			UIPanel uIPanel = null;
			BaseWindow baseWindow_ = BaseWindow_0;
			for (int i = 0; i < list_1.Count; i++)
			{
				baseWindow = list_1[i];
				if (baseWindow == baseWindow_ || baseWindow == null)
				{
					continue;
				}
				uIPanel = baseWindow.GetComponent<UIPanel>();
				if (!(uIPanel == null))
				{
					uIPanel.Single_2 = ((!bool_0) ? 0f : 1f);
					for (int j = 0; j < baseWindow.ChildPanels.Length; j++)
					{
						baseWindow.ChildPanels[j].Panel.GetComponent<UIPanel>().Single_2 = ((!bool_0) ? 0f : 1f);
					}
				}
			}
			if (baseWindow_ != null)
			{
				uIPanel = baseWindow_.GetComponent<UIPanel>();
				if (uIPanel != null)
				{
					uIPanel.Single_2 = 1f;
					for (int k = 0; k < baseWindow_.ChildPanels.Length; k++)
					{
						baseWindow_.ChildPanels[k].Panel.GetComponent<UIPanel>().Single_2 = 1f;
					}
				}
			}
			if (list_1.Count > 0)
			{
				WindowFader_0.SetAlphaForce((!bool_0) ? 0f : 0.75f);
			}
		}
	}
}
