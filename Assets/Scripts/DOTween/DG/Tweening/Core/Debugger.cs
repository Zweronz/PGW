using UnityEngine;

namespace DG.Tweening.Core
{
	internal static class Debugger
	{
		internal static int logPriority;

		internal static void Log(object message)
		{
			Debug.Log("DOTWEEN :: " + message);
		}

		internal static void LogWarning(object message)
		{
			Debug.LogWarning("DOTWEEN :: " + message);
		}

		internal static void LogError(object message)
		{
			Debug.LogError("DOTWEEN :: " + message);
		}

		internal static void LogReport(object message)
		{
			Debug.Log(string.Concat("<color=#00B500FF>DOTWEEN :: ", message, "</color>"));
		}

		internal static void LogInvalidTween(Tween t)
		{
			LogWarning("This Tween has been killed and is now invalid");
		}

		internal static void LogNestedTween(Tween t)
		{
			LogWarning("This Tween was added to a Sequence and can't be controlled directly");
		}

		internal static void LogNullTween(Tween t)
		{
			LogWarning("Null Tween");
		}

		internal static void LogNonPathTween(Tween t)
		{
			LogWarning("This Tween is not a path tween");
		}

		internal static void SetLogPriority(LogBehaviour logBehaviour)
		{
			switch (logBehaviour)
			{
			default:
				logPriority = 0;
				break;
			case LogBehaviour.Default:
				logPriority = 1;
				break;
			case LogBehaviour.Verbose:
				logPriority = 2;
				break;
			}
		}
	}
}
