using System;
using Rilisoft;
using UnityEngine;

public class PlusMinusController : MonoBehaviour
{
	public int stepValue = 1;

	public SaltedInt minValue = default(SaltedInt);

	public SaltedInt maxValue = default(SaltedInt);

	public SaltedInt value = default(SaltedInt);

	public GameObject plusButton;

	public GameObject minusButton;

	public UILabel countLabel;

	public UILabel headLabel;

	private void Awake()
	{
		minValue.Int32_0 = 4;
		maxValue.Int32_0 = 8;
		value.Int32_0 = 4;
	}

	private void Start()
	{
		if (plusButton != null)
		{
			ButtonHandler component = plusButton.GetComponent<ButtonHandler>();
			if (component != null)
			{
				component.Clicked += HandlePlusButtonClicked;
			}
		}
		if (minusButton != null)
		{
			ButtonHandler component2 = minusButton.GetComponent<ButtonHandler>();
			if (component2 != null)
			{
				component2.Clicked += HandleMinusButtonClicked;
			}
		}
	}

	private void HandlePlusButtonClicked(object sender, EventArgs e)
	{
		value.Int32_0 += stepValue;
		if (value.Int32_0 > maxValue.Int32_0)
		{
			value.Int32_0 = maxValue.Int32_0;
		}
	}

	private void HandleMinusButtonClicked(object sender, EventArgs e)
	{
		value.Int32_0 -= stepValue;
		if (value.Int32_0 < minValue.Int32_0)
		{
			value.Int32_0 = minValue.Int32_0;
		}
	}

	private void Update()
	{
		if (countLabel != null)
		{
			countLabel.String_0 = value.Int32_0.ToString();
		}
	}
}
