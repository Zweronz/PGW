using System;
using System.Text;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;

namespace DG.Tweening.Plugins
{
	public class StringPlugin : ABSTweenPlugin<string, string, StringOptions>
	{
		private static readonly StringBuilder _Buffer = new StringBuilder();

		public override void SetFrom(TweenerCore<string, string, StringOptions> t, bool isRelative)
		{
			string endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = endValue;
			t.setter(t.startValue);
		}

		public override void Reset(TweenerCore<string, string, StringOptions> t)
		{
			t.startValue = (t.endValue = (t.changeValue = null));
		}

		public override string ConvertToStartValue(TweenerCore<string, string, StringOptions> t, string value)
		{
			return value;
		}

		public override void SetRelativeEndValue(TweenerCore<string, string, StringOptions> t)
		{
		}

		public override void SetChangeValue(TweenerCore<string, string, StringOptions> t)
		{
			t.changeValue = t.endValue;
		}

		public override float GetSpeedBasedDuration(StringOptions options, float unitsXSecond, string changeValue)
		{
			float num = (float)changeValue.Length / unitsXSecond;
			if (num < 0f)
			{
				num = 0f - num;
			}
			return num;
		}

		public override void EvaluateAndApply(StringOptions options, Tween t, bool isRelative, DOGetter<string> getter, DOSetter<string> setter, float elapsed, string startValue, string changeValue, float duration, bool usingInversePosition)
		{
			_Buffer.Remove(0, _Buffer.Length);
			if (isRelative && t.loopType == LoopType.Incremental)
			{
				int num = (t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
				if (num > 0)
				{
					_Buffer.Append(startValue);
					for (int i = 0; i < num; i++)
					{
						_Buffer.Append(changeValue);
					}
					startValue = _Buffer.ToString();
					_Buffer.Remove(0, _Buffer.Length);
				}
			}
			int length = startValue.Length;
			int length2 = changeValue.Length;
			int num2 = (int)Math.Round((float)length2 * EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod));
			if (num2 > length2)
			{
				num2 = length2;
			}
			else if (num2 < 0)
			{
				num2 = 0;
			}
			if (isRelative)
			{
				_Buffer.Append(startValue);
				if (options.scramble)
				{
					setter(_Buffer.Append(changeValue, 0, num2).AppendScrambledChars(length2 - num2, options.scrambledChars ?? StringPluginExtensions.ScrambledChars).ToString());
				}
				else
				{
					setter(_Buffer.Append(changeValue, 0, num2).ToString());
				}
				return;
			}
			if (options.scramble)
			{
				setter(_Buffer.Append(changeValue, 0, num2).AppendScrambledChars(length2 - num2, options.scrambledChars ?? StringPluginExtensions.ScrambledChars).ToString());
				return;
			}
			int num3 = length - length2;
			int num4 = length;
			if (num3 > 0)
			{
				float num5 = (float)num2 / (float)length2;
				num4 -= (int)((float)num4 * num5);
			}
			else
			{
				num4 -= num2;
			}
			_Buffer.Append(changeValue, 0, num2);
			if (num2 < length2 && num2 < length)
			{
				_Buffer.Append(startValue, num2, num4);
			}
			setter(_Buffer.ToString());
		}
	}
}
