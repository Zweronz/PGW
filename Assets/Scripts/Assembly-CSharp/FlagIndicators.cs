using System.Runtime.CompilerServices;
using UnityEngine;

public class FlagIndicators : MonoBehaviour
{
	public FlagIndicator flagIndicatorBlue;

	public FlagIndicator bazaIndicatorBlue;

	public FlagIndicator flagIndicatorRed;

	public FlagIndicator bazaIndicatorRed;

	[CompilerGenerated]
	private static FlagIndicators flagIndicators_0;

	public static FlagIndicators FlagIndicators_0
	{
		[CompilerGenerated]
		get
		{
			return flagIndicators_0;
		}
		[CompilerGenerated]
		private set
		{
			flagIndicators_0 = value;
		}
	}

	private void Awake()
	{
		FlagIndicators_0 = this;
	}

	private void OnDestroy()
	{
		FlagIndicators_0 = null;
	}
}
