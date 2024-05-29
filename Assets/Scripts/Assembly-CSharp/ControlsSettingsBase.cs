using System;
using System.Runtime.CompilerServices;
using Rilisoft;
using UnityEngine;

public class ControlsSettingsBase : MonoBehaviour
{
	public GameObject settingsPanel;

	public GameObject savePosJoystikButton;

	public GameObject defaultPosJoystikButton;

	public GameObject cancelPosJoystikButton;

	public GameObject SettingsJoysticksPanel;

	public GameObject zoomButton;

	public GameObject reloadButton;

	public GameObject jumpButton;

	public GameObject fireButton;

	public GameObject joystick;

	public GameObject grenadeButton;

	public GameObject fireButtonInJoystick;

	public UIAnchor BottomLeftControlsAnchor;

	public UIAnchor BottomRightControlsAnchor;

	public Transform BottomLeft;

	public Transform TopLeft;

	public Transform BottomRight;

	public Transform TopRight;

	public static string string_0 = "JoystickSettSettSett";

	protected bool bool_0;

	private static Action action_0;

	public static event Action ControlsChanged
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			action_0 = (Action)Delegate.Combine(action_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			action_0 = (Action)Delegate.Remove(action_0, value);
		}
	}

	protected void HandleControlsClicked()
	{
		Debug.Log("HandleControlsClicked " + GlobalGameController.bool_0);
		if (GlobalGameController.bool_0)
		{
			BottomRight.localPosition = new Vector3(0f, 0f, 0f);
			TopRight.localPosition = new Vector3(-512f, 450f, 0f);
			TopLeft.localPosition = new Vector3(0f, 450f, 0f);
			BottomLeft.localPosition = new Vector3(512f, 0f, 0f);
			BottomLeftControlsAnchor.side = UIAnchor.Side.BottomLeft;
			BottomRightControlsAnchor.side = UIAnchor.Side.BottomRight;
		}
		else
		{
			BottomRight.localPosition = new Vector3(512f, 0f, 0f);
			TopRight.localPosition = new Vector3(0f, 450f, 0f);
			TopLeft.localPosition = new Vector3(-512f, 450f, 0f);
			BottomLeft.localPosition = new Vector3(0f, 0f, 0f);
			BottomLeftControlsAnchor.side = UIAnchor.Side.BottomRight;
			BottomRightControlsAnchor.side = UIAnchor.Side.BottomLeft;
		}
		SetControlsCoords();
	}

	private void SetControlsCoords()
	{
		float num = ((!GlobalGameController.bool_0) ? (-1f) : 1f);
		Debug.Log("SetControlsCoords " + num);
		Vector3[] array = Storager.LoadVector3Array(string_0);
		if (array != null && array.Length >= 7)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i].x *= num;
			}
			zoomButton.transform.localPosition = array[0];
			reloadButton.transform.localPosition = array[1];
			jumpButton.transform.localPosition = array[2];
			fireButton.transform.localPosition = array[3];
			joystick.transform.localPosition = array[4];
			grenadeButton.transform.localPosition = array[5];
			fireButtonInJoystick.transform.localPosition = array[6];
		}
		else
		{
			Defs.InitCoordsIphone();
			zoomButton.transform.localPosition = new Vector3((float)Defs.int_2 * num, Defs.int_3, zoomButton.transform.localPosition.z);
			reloadButton.transform.localPosition = new Vector3((float)Defs.int_4 * num, Defs.int_5, reloadButton.transform.localPosition.z);
			jumpButton.transform.localPosition = new Vector3((float)Defs.int_6 * num, Defs.int_7, jumpButton.transform.localPosition.z);
			fireButton.transform.localPosition = new Vector3((float)Defs.int_8 * num, Defs.int_9, fireButton.transform.localPosition.z);
			grenadeButton.transform.localPosition = new Vector3((float)Defs.int_12 * num, Defs.int_13, grenadeButton.transform.localPosition.z);
			joystick.transform.localPosition = new Vector3((float)Defs.int_10 * num, Defs.int_11, joystick.transform.localPosition.z);
			fireButtonInJoystick.transform.localPosition = new Vector3((float)Defs.int_14 * num, Defs.int_15, fireButtonInJoystick.transform.localPosition.z);
		}
	}

	protected void OnEnable()
	{
		SetControlsCoords();
	}

	protected virtual void HandleSavePosJoystikClicked(object sender, EventArgs e)
	{
		float num = (GlobalGameController.bool_0 ? 1 : (-1));
		Storager.SaveVector3Array(string_0, new Vector3[7]
		{
			new Vector3(zoomButton.transform.localPosition.x * num, zoomButton.transform.localPosition.y, zoomButton.transform.localPosition.z),
			new Vector3(reloadButton.transform.localPosition.x * num, reloadButton.transform.localPosition.y, reloadButton.transform.localPosition.z),
			new Vector3(jumpButton.transform.localPosition.x * num, jumpButton.transform.localPosition.y, jumpButton.transform.localPosition.z),
			new Vector3(fireButton.transform.localPosition.x * num, fireButton.transform.localPosition.y, fireButton.transform.localPosition.z),
			new Vector3(joystick.transform.localPosition.x * num, joystick.transform.localPosition.y, joystick.transform.localPosition.z),
			new Vector3(grenadeButton.transform.localPosition.x * num, grenadeButton.transform.localPosition.y, grenadeButton.transform.localPosition.z),
			new Vector3(fireButtonInJoystick.transform.localPosition.x * num, fireButtonInJoystick.transform.localPosition.y, fireButtonInJoystick.transform.localPosition.z)
		});
		SettingsJoysticksPanel.SetActive(false);
		settingsPanel.SetActive(true);
		Action action = action_0;
		if (action != null)
		{
			action_0();
		}
	}

	private void HandleDefaultPosJoystikClicked(object sender, EventArgs e)
	{
		float num = (GlobalGameController.bool_0 ? 1 : (-1));
		Defs.InitCoordsIphone();
		zoomButton.transform.localPosition = new Vector3((float)Defs.int_2 * num, Defs.int_3, zoomButton.transform.localPosition.z);
		reloadButton.transform.localPosition = new Vector3((float)Defs.int_4 * num, Defs.int_5, reloadButton.transform.localPosition.z);
		jumpButton.transform.localPosition = new Vector3((float)Defs.int_6 * num, Defs.int_7, jumpButton.transform.localPosition.z);
		fireButton.transform.localPosition = new Vector3((float)Defs.int_8 * num, Defs.int_9, fireButton.transform.localPosition.z);
		joystick.transform.localPosition = new Vector3((float)Defs.int_10 * num, Defs.int_11, joystick.transform.localPosition.z);
		grenadeButton.transform.localPosition = new Vector3((float)Defs.int_12 * num, Defs.int_13, grenadeButton.transform.localPosition.z);
		fireButtonInJoystick.transform.localPosition = new Vector3((float)Defs.int_14 * num, Defs.int_15, fireButtonInJoystick.transform.localPosition.z);
	}

	protected virtual void HandleCancelPosJoystikClicked(object sender, EventArgs e)
	{
		bool_0 = true;
	}

	protected void Start()
	{
		if (savePosJoystikButton != null)
		{
			ButtonHandler component = savePosJoystikButton.GetComponent<ButtonHandler>();
			if (component != null)
			{
				component.Clicked += HandleSavePosJoystikClicked;
			}
		}
		if (defaultPosJoystikButton != null)
		{
			ButtonHandler component2 = defaultPosJoystikButton.GetComponent<ButtonHandler>();
			if (component2 != null)
			{
				component2.Clicked += HandleDefaultPosJoystikClicked;
			}
		}
		if (cancelPosJoystikButton != null)
		{
			ButtonHandler component3 = cancelPosJoystikButton.GetComponent<ButtonHandler>();
			if (component3 != null)
			{
				component3.Clicked += HandleCancelPosJoystikClicked;
			}
		}
	}
}
