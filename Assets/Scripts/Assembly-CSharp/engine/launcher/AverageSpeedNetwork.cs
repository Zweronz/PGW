using System;
using System.Diagnostics;
using engine.helpers;

namespace engine.launcher
{
	public static class AverageSpeedNetwork
	{
		private static MovingAverage movingAverage_0 = new MovingAverage(40u);

		private static long long_0 = 0L;

		private static long long_1 = 0L;

		private static Stopwatch stopwatch_0 = new Stopwatch();

		public static long Int64_0
		{
			set
			{
				if (!stopwatch_0.IsRunning)
				{
					stopwatch_0.Start();
				}
				long num = value - long_0;
				if (num != 0L)
				{
					long_0 = value;
					long num2 = 1L + stopwatch_0.ElapsedMilliseconds - long_1;
					int num3 = (int)Math.Round((float)num * 1000f / (float)num2);
					long_1 = stopwatch_0.ElapsedMilliseconds;
					movingAverage_0.AddSample(num3);
				}
			}
		}

		public static int Int32_0
		{
			get
			{
				return (int)movingAverage_0.Double_0;
			}
		}

		public static void Reset()
		{
			movingAverage_0.ClearSamples();
			long_0 = 0L;
			long_1 = 0L;
			stopwatch_0.Stop();
		}
	}
}
