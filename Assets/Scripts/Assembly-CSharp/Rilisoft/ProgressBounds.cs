using System;
using UnityEngine;

namespace Rilisoft
{
	internal sealed class ProgressBounds
	{
		private float float_0;

		private float float_1 = 1f;

		public float Single_0
		{
			get
			{
				return float_0;
			}
		}

		public float Single_1
		{
			get
			{
				return float_1;
			}
		}

		public float Clamp(float float_2)
		{
			return Mathf.Clamp(float_2, float_0, float_1);
		}

		public float Lerp(float float_2, float float_3)
		{
			return Mathf.Lerp(Clamp(float_2), Single_1, float_3);
		}

		public void SetBounds(float float_2, float float_3)
		{
			float_2 = Mathf.Clamp01(float_2);
			float_3 = Mathf.Clamp01(float_3);
			if (float_2 > float_3)
			{
				throw new ArgumentException("Bounds are not ordered.");
			}
			float_0 = float_2;
			float_1 = float_3;
		}
	}
}
