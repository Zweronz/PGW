namespace engine.helpers
{
	public class MovingAverage
	{
		private CircularBuffer<double> circularBuffer_0;

		private double double_0;

		public int Int32_0
		{
			get
			{
				return circularBuffer_0.Count;
			}
		}

		public double Double_0
		{
			get
			{
				if (circularBuffer_0.Count == 0)
				{
					return 0.0;
				}
				return double_0 / (double)circularBuffer_0.Count;
			}
		}

		public MovingAverage(uint uint_0)
		{
			circularBuffer_0 = new CircularBuffer<double>(uint_0);
			double_0 = 0.0;
		}

		public void AddSample(double double_1)
		{
			if (circularBuffer_0.Count == circularBuffer_0.UInt32_0)
			{
				double_0 -= circularBuffer_0[0];
			}
			circularBuffer_0.Add(double_1);
			double_0 += double_1;
		}

		public void ClearSamples()
		{
			double_0 = 0.0;
			circularBuffer_0.Clear();
		}

		public void InitializeSamples(double double_1)
		{
			for (int i = 0; i < circularBuffer_0.Count; i++)
			{
				circularBuffer_0[i] = double_1;
			}
		}
	}
}
