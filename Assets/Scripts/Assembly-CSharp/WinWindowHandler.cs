using System;
using System.Runtime.InteropServices;
using UnityEngine;
using engine.helpers;

public sealed class WinWindowHandler : BaseWindowHandler
{
	public struct RECT
	{
		public int int_0;

		public int int_1;

		public int int_2;

		public int int_3;
	}

	private const int int_0 = 8388608;

	private const int int_1 = 12582912;

	private const int int_2 = 1073741824;

	private const int int_3 = 1073741824;

	private const int int_4 = 33554432;

	private const int int_5 = 67108864;

	private const int int_6 = 134217728;

	private const int int_7 = 4194304;

	private const int int_8 = 131072;

	private const int int_9 = 1048576;

	private const int int_10 = 536870912;

	private const int int_11 = 16777216;

	private const int int_12 = 65536;

	private const int int_13 = 536870912;

	private const int int_14 = 131072;

	private const int int_15 = 0;

	private const int int_16 = 13565952;

	private const int int_17 = int.MinValue;

	private const int int_18 = -2138570752;

	private const int int_19 = 262144;

	private const int int_20 = 524288;

	private const int int_21 = 65536;

	private const int int_22 = 262144;

	private const int int_23 = 0;

	private const int int_24 = 13565952;

	private const int int_25 = 268435456;

	private const int int_26 = 2097152;

	private const int int_27 = 1;

	private const int int_28 = 512;

	private const int int_29 = 131072;

	private const int int_30 = 32;

	private const int int_31 = 2;

	private const int int_32 = 1;

	private const int int_33 = 4;

	private const int int_34 = 512;

	private const int int_35 = 64;

	private const int int_36 = 1024;

	private const int int_37 = -16;

	private const int int_38 = -20;

	private string string_0;

	private RECT rect_0;

	private IntPtr IntPtr_0
	{
		get
		{
			return FindWindow(IntPtr.Zero, string_0);
		}
	}

	private IntPtr IntPtr_1
	{
		get
		{
			return GetDesktopWindow();
		}
	}

	public WinWindowHandler(string string_1)
	{
		if (string.IsNullOrEmpty(string_1))
		{
			throw new ArgumentException("WinWindowHandler|Ctr. Title window must be not empty.");
		}
		string_0 = string_1;
		GetWindowRect(IntPtr_1, out rect_0);
	}

	[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	public static extern IntPtr SetWindowLong(IntPtr intptr_0, int int_39, int int_40);

	[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	public static extern IntPtr GetWindowLong(IntPtr intptr_0, int int_39);

	[DllImport("user32.dll", SetLastError = true)]
	public static extern IntPtr FindWindow(IntPtr intptr_0, string string_1);

	[DllImport("user32.dll", SetLastError = true)]
	public static extern IntPtr GetDesktopWindow();

	[DllImport("user32.dll", SetLastError = true)]
	public static extern IntPtr SetWindowPos(IntPtr intptr_0, int int_39, int int_40, int int_41, int int_42, int int_43, int int_44);

	[DllImport("user32.dll", SetLastError = true)]
	public static extern bool GetWindowRect(IntPtr intptr_0, out RECT rect_1);

	[DllImport("user32.dll", SetLastError = true)]
	public static extern bool GetClientRect(IntPtr intptr_0, out RECT rect_1);

	[DllImport("user32.dll", SetLastError = true)]
	public static extern bool ShowWindow(IntPtr intptr_0, int int_39);

	public override void SetBorderlessStyle()
	{
		int gparam_ = (int)GetWindowLong(IntPtr_0, -16);
		Flags.Unset(ref gparam_, 12582912);
		SetWindowLong(IntPtr_0, -16, gparam_);
		SetWindowPosition(GetCenterPosition());
		SetWindowLong(IntPtr_0, -16, gparam_);
		SetWindowLong(IntPtr_0, -16, gparam_);
	}

	public override void SetWindowPosition(Vector2 vector2_1)
	{
		vector2_1.x = Mathf.Clamp(vector2_1.x, 0f, rect_0.int_2 - Screen.width);
		vector2_1.y = Mathf.Clamp(vector2_1.y, 0f, rect_0.int_3 - Screen.height);
		base.Vector2_0 = vector2_1;
		SetWindowPos(IntPtr_0, -2, (int)vector2_1.x, (int)vector2_1.y, Screen.width, Screen.height, 32);
	}

	public override void MinimizeWindwow()
	{
		ShowWindow(IntPtr_0, 2);
	}

	public override void MaximizeWindwow()
	{
		ShowWindow(IntPtr_0, 1);
	}

	private Vector2 GetCenterPosition()
	{
		RECT rect_;
		if (!GetWindowRect(IntPtr_1, out rect_))
		{
			return Vector2.zero;
		}
		int num = rect_.int_2 - rect_.int_0;
		int num2 = rect_.int_3 - rect_.int_1;
		int num3 = (num - Screen.width) / 2;
		int num4 = (num2 - Screen.height) / 2;
		return new Vector2(num3, num4);
	}
}
