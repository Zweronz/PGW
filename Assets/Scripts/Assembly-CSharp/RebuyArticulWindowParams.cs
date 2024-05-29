using System;
using engine.unity;

public sealed class RebuyArticulWindowParams : WindowShowParameters
{
	public enum RebuyWndType
	{
		COMMON = 0,
		REBUY_WND = 1,
		NEW_ITEM_WND = 2
	}

	public int int_0;

	public Action action_0;

	public RebuyWndType rebuyWndType_0;

	public RebuyArticulWindowParams(int int_1, RebuyWndType rebuyWndType_1, Action action_1 = null)
	{
		int_0 = int_1;
		rebuyWndType_0 = rebuyWndType_1;
		action_0 = action_1;
	}
}
