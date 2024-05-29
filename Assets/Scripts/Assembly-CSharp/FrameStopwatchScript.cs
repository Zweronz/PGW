using System.Diagnostics;
using UnityEngine;

internal sealed class FrameStopwatchScript : MonoBehaviour
{
	private readonly Stopwatch stopwatch_0 = new Stopwatch();

	public float GetSecondsSinceFrameStarted()
	{
		return (float)stopwatch_0.ElapsedMilliseconds / 1000f;
	}

	internal void Start()
	{
		stopwatch_0.Start();
	}

	internal void Update()
	{
		stopwatch_0.Reset();
		stopwatch_0.Start();
	}
}
