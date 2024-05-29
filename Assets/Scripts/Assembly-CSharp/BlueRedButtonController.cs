using UnityEngine;

public class BlueRedButtonController : MonoBehaviour
{
	public UIButton blueButton;

	public UIButton redButton;

	public bool isBlueAvalible = true;

	public bool isRedAvalible = true;

	private void Start()
	{
		if (!Defs.bool_6 && !Defs.bool_5)
		{
			base.enabled = false;
		}
	}

	private void Update()
	{
		int num = 0;
		int num2 = 0;
		GameObject[] array = GameObject.FindGameObjectsWithTag("NetworkTable");
		foreach (GameObject gameObject in array)
		{
			if (gameObject.GetComponent<NetworkStartTable>().PlayerCommandController_0.TypeCommand_1 == TypeCommand.Diggers)
			{
				num++;
			}
			if (gameObject.GetComponent<NetworkStartTable>().PlayerCommandController_0.TypeCommand_1 == TypeCommand.Kritters)
			{
				num2++;
			}
		}
		isBlueAvalible = true;
		isRedAvalible = true;
		if (PhotonNetwork.Room_0 != null)
		{
			if (num >= PhotonNetwork.Room_0.Int32_2 / 2 || num - num2 > 0 || (num > 0 && num2 == 0))
			{
				isBlueAvalible = false;
			}
			if (num2 >= PhotonNetwork.Room_0.Int32_2 / 2 || num2 - num > 0 || (num2 > 0 && num == 0))
			{
				isRedAvalible = false;
			}
			if (isBlueAvalible != blueButton.Boolean_0)
			{
				blueButton.Boolean_0 = isBlueAvalible;
			}
			if (isRedAvalible != redButton.Boolean_0)
			{
				redButton.Boolean_0 = isRedAvalible;
			}
		}
	}
}
