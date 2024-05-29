using System;
using UnityEngine;
using engine.unity;

public sealed class LvlUpWindowParams : WindowShowParameters
{
	public int int_0 = 1;

	public Action action_0;

	public Texture texture_0;

	public LvlUpWindowParams(int int_1, Action action_1 = null, Texture texture_1 = null)
	{
		int_0 = int_1;
		action_0 = action_1;
		texture_0 = texture_1;
	}
}
