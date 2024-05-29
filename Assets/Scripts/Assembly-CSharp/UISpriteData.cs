using System;

[Serializable]
public class UISpriteData
{
	public string name = "Sprite";

	public int x;

	public int y;

	public int width;

	public int height;

	public int borderLeft;

	public int borderRight;

	public int borderTop;

	public int borderBottom;

	public int paddingLeft;

	public int paddingRight;

	public int paddingTop;

	public int paddingBottom;

	public bool Boolean_0
	{
		get
		{
			return (borderLeft | borderRight | borderTop | borderBottom) != 0;
		}
	}

	public bool Boolean_1
	{
		get
		{
			return (paddingLeft | paddingRight | paddingTop | paddingBottom) != 0;
		}
	}

	public void SetRect(int int_0, int int_1, int int_2, int int_3)
	{
		x = int_0;
		y = int_1;
		width = int_2;
		height = int_3;
	}

	public void SetPadding(int int_0, int int_1, int int_2, int int_3)
	{
		paddingLeft = int_0;
		paddingBottom = int_1;
		paddingRight = int_2;
		paddingTop = int_3;
	}

	public void SetBorder(int int_0, int int_1, int int_2, int int_3)
	{
		borderLeft = int_0;
		borderBottom = int_1;
		borderRight = int_2;
		borderTop = int_3;
	}

	public void CopyFrom(UISpriteData uispriteData_0)
	{
		name = uispriteData_0.name;
		x = uispriteData_0.x;
		y = uispriteData_0.y;
		width = uispriteData_0.width;
		height = uispriteData_0.height;
		borderLeft = uispriteData_0.borderLeft;
		borderRight = uispriteData_0.borderRight;
		borderTop = uispriteData_0.borderTop;
		borderBottom = uispriteData_0.borderBottom;
		paddingLeft = uispriteData_0.paddingLeft;
		paddingRight = uispriteData_0.paddingRight;
		paddingTop = uispriteData_0.paddingTop;
		paddingBottom = uispriteData_0.paddingBottom;
	}

	public void CopyBorderFrom(UISpriteData uispriteData_0)
	{
		borderLeft = uispriteData_0.borderLeft;
		borderRight = uispriteData_0.borderRight;
		borderTop = uispriteData_0.borderTop;
		borderBottom = uispriteData_0.borderBottom;
	}
}
