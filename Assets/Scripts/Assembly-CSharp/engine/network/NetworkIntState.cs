using System.Collections.Generic;
using engine.events;

namespace engine.network
{
	public abstract class NetworkIntState : BaseEvent<BoolWrapper>
	{
		private int int_0;

		private List<int> list_1 = new List<int>();

		public int Int32_0
		{
			get
			{
				return int_0;
			}
			set
			{
				for (int i = 0; i < list_1.Count; i++)
				{
					int num = 1 << list_1[i];
					int num2 = value & num;
					if ((int_0 & num) != num2)
					{
						ChangeState(list_1[i], num2 != 0);
					}
				}
				int_0 = value;
			}
		}

		public NetworkIntState()
		{
			list_1 = MapBits();
		}

		public void SetState(int int_1, bool bool_0)
		{
			int num = 1 << int_1;
			bool flag = (int_0 & num) != 0;
			if (bool_0)
			{
				int_0 |= num;
			}
			else
			{
				int_0 &= ~num;
			}
			if (flag != bool_0)
			{
				ChangeState(int_1, bool_0);
			}
		}

		public bool GetState(int int_1)
		{
			int num = 1 << int_1;
			return (int_0 & num) != 0;
		}

		protected abstract void ChangeState(int int_1, bool bool_0);

		protected abstract List<int> MapBits();
	}
}
