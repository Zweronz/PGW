using System;
using System.Collections.Generic;
using engine.network;

internal sealed class FPSNetworkStateSynchronizer : NetworkIntState
{
	private FPSStateController fpsstateController_0;

	public FPSNetworkStateSynchronizer(FPSStateController fpsstateController_1)
	{
		fpsstateController_0 = fpsstateController_1;
	}

	protected override List<int> MapBits()
	{
		Array values = Enum.GetValues(typeof(FPSStateController.STATES));
		List<int> list = new List<int>();
		foreach (int item in values)
		{
			list.Add(item);
		}
		return list;
	}

	protected override void ChangeState(int int_1, bool bool_0)
	{
		fpsstateController_0.SetNetworkState(int_1, bool_0);
	}
}
