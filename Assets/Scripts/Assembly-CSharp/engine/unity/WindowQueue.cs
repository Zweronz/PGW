using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace engine.unity
{
	public sealed class WindowQueue
	{
		public enum Type
		{
			Default = 0,
			New = 1,
			Top = 2
		}

		public sealed class Item
		{
			[CompilerGenerated]
			private BaseWindow baseWindow_0;

			[CompilerGenerated]
			private WindowShowParameters windowShowParameters_0;

			public BaseWindow BaseWindow_0
			{
				[CompilerGenerated]
				get
				{
					return baseWindow_0;
				}
				[CompilerGenerated]
				private set
				{
					baseWindow_0 = value;
				}
			}

			public WindowShowParameters WindowShowParameters_0
			{
				[CompilerGenerated]
				get
				{
					return windowShowParameters_0;
				}
				[CompilerGenerated]
				private set
				{
					windowShowParameters_0 = value;
				}
			}

			public Item(BaseWindow baseWindow_1, WindowShowParameters windowShowParameters_1)
			{
				BaseWindow_0 = baseWindow_1;
				WindowShowParameters_0 = windowShowParameters_1;
			}
		}

		private List<Item> list_0 = new List<Item>();

		public int Int32_0
		{
			get
			{
				return list_0.Count;
			}
		}

		public void Add(BaseWindow baseWindow_0, WindowShowParameters windowShowParameters_0)
		{
			list_0.Add(new Item(baseWindow_0, windowShowParameters_0));
		}

		public void Remove(BaseWindow baseWindow_0)
		{
			Item item = list_0.Find((Item item_0) => item_0.BaseWindow_0 == baseWindow_0);
			if (item != null)
			{
				list_0.Remove(item);
			}
		}

		public bool IsInQueue(BaseWindow baseWindow_0)
		{
			int num = 0;
			while (true)
			{
				if (num < list_0.Count)
				{
					if (list_0[num].BaseWindow_0 == baseWindow_0)
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

		public void Clear()
		{
			list_0.Clear();
		}

		public Item GetLastItem()
		{
			if (list_0.Count == 0)
			{
				return null;
			}
			list_0.Sort(Comparator);
			int num = list_0.Count;
			while (true)
			{
				if (num != 0)
				{
					if (list_0[num - 1].BaseWindow_0.isShowAvailable())
					{
						break;
					}
					num--;
					continue;
				}
				return null;
			}
			return list_0[num - 1];
		}

		private static int Comparator(Item item_0, Item item_1)
		{
			if (item_0.BaseWindow_0.Boolean_0 && !item_1.BaseWindow_0.Boolean_0)
			{
				return 1;
			}
			if (item_1.BaseWindow_0.Boolean_0 && !item_0.BaseWindow_0.Boolean_0)
			{
				return -1;
			}
			if (item_0.BaseWindow_0.Parameters_0.int_0 > item_1.BaseWindow_0.Parameters_0.int_0)
			{
				return 1;
			}
			if (item_0.BaseWindow_0.Parameters_0.int_0 < item_1.BaseWindow_0.Parameters_0.int_0)
			{
				return -1;
			}
			return 0;
		}
	}
}
