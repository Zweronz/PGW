using System.Collections.Generic;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(UILabel))]
public class TypewriterEffect : MonoBehaviour
{
	private struct FadeEntry
	{
		public int int_0;

		public string string_0;

		public float float_0;
	}

	public static TypewriterEffect typewriterEffect_0;

	public int charsPerSecond = 20;

	public float fadeInTime;

	public float delayOnPeriod;

	public float delayOnNewLine;

	public UIScrollView scrollView;

	public bool keepFullDimensions;

	public List<EventDelegate> onFinished = new List<EventDelegate>();

	private UILabel uilabel_0;

	private string string_0 = string.Empty;

	private int int_0;

	private float float_0;

	private bool bool_0 = true;

	private bool bool_1;

	private BetterList<FadeEntry> betterList_0 = new BetterList<FadeEntry>();

	public bool Boolean_0
	{
		get
		{
			return bool_1;
		}
	}

	public void ResetToBeginning()
	{
		bool_0 = true;
	}

	public void Finish()
	{
		if (bool_1)
		{
			bool_1 = false;
			if (!bool_0)
			{
				int_0 = string_0.Length;
				betterList_0.Clear();
				uilabel_0.String_0 = string_0;
			}
			if (keepFullDimensions && scrollView != null)
			{
				scrollView.UpdatePosition();
			}
			typewriterEffect_0 = this;
			EventDelegate.Execute(onFinished);
			typewriterEffect_0 = null;
		}
	}

	private void OnEnable()
	{
		bool_0 = true;
		bool_1 = true;
	}

	private void Update()
	{
		if (!bool_1)
		{
			return;
		}
		if (bool_0)
		{
			int_0 = 0;
			bool_0 = false;
			uilabel_0 = GetComponent<UILabel>();
			string_0 = uilabel_0.String_1;
			betterList_0.Clear();
			if (keepFullDimensions && scrollView != null)
			{
				scrollView.UpdatePosition();
			}
		}
		while (int_0 < string_0.Length && !(float_0 > RealTime.Single_0))
		{
			int num = int_0;
			charsPerSecond = Mathf.Max(1, charsPerSecond);
			while (NGUIText.ParseSymbol(string_0, ref int_0))
			{
			}
			int_0++;
			float num2 = 1f / (float)charsPerSecond;
			char c = ((num >= string_0.Length) ? '\n' : string_0[num]);
			if (c == '\n')
			{
				num2 += delayOnNewLine;
			}
			else if (num + 1 == string_0.Length || string_0[num + 1] <= ' ')
			{
				switch (c)
				{
				case '.':
					if (num + 2 < string_0.Length && string_0[num + 1] == '.' && string_0[num + 2] == '.')
					{
						num2 += delayOnPeriod * 3f;
						num += 2;
					}
					else
					{
						num2 += delayOnPeriod;
					}
					break;
				case '!':
				case '?':
					num2 += delayOnPeriod;
					break;
				}
			}
			if (float_0 == 0f)
			{
				float_0 = RealTime.Single_0 + num2;
			}
			else
			{
				float_0 += num2;
			}
			if (fadeInTime != 0f)
			{
				FadeEntry gparam_ = default(FadeEntry);
				gparam_.int_0 = num;
				gparam_.float_0 = 0f;
				gparam_.string_0 = string_0.Substring(num, int_0 - num);
				betterList_0.Add(gparam_);
			}
			else
			{
				uilabel_0.String_0 = ((!keepFullDimensions) ? string_0.Substring(0, int_0) : (string_0.Substring(0, int_0) + "[00]" + string_0.Substring(int_0)));
				if (!keepFullDimensions && scrollView != null)
				{
					scrollView.UpdatePosition();
				}
			}
		}
		if (betterList_0.size != 0)
		{
			int num3 = 0;
			while (num3 < betterList_0.size)
			{
				FadeEntry value = betterList_0[num3];
				value.float_0 += RealTime.Single_1 / fadeInTime;
				if (value.float_0 < 1f)
				{
					betterList_0[num3] = value;
					num3++;
				}
				else
				{
					betterList_0.RemoveAt(num3);
				}
			}
			if (betterList_0.size == 0)
			{
				if (keepFullDimensions)
				{
					uilabel_0.String_0 = string_0.Substring(0, int_0) + "[00]" + string_0.Substring(int_0);
				}
				else
				{
					uilabel_0.String_0 = string_0.Substring(0, int_0);
				}
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < betterList_0.size; i++)
			{
				FadeEntry fadeEntry = betterList_0[i];
				if (i == 0)
				{
					stringBuilder.Append(string_0.Substring(0, fadeEntry.int_0));
				}
				stringBuilder.Append('[');
				stringBuilder.Append(NGUIText.EncodeAlpha(fadeEntry.float_0));
				stringBuilder.Append(']');
				stringBuilder.Append(fadeEntry.string_0);
			}
			if (keepFullDimensions)
			{
				stringBuilder.Append("[00]");
				stringBuilder.Append(string_0.Substring(int_0));
			}
			uilabel_0.String_0 = stringBuilder.ToString();
		}
		else if (int_0 == string_0.Length)
		{
			typewriterEffect_0 = this;
			EventDelegate.Execute(onFinished);
			typewriterEffect_0 = null;
			bool_1 = false;
		}
	}
}
