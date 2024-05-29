using System;
using UnityEngine;

public class CyclerClamper : MonoBehaviour
{
	public IPPickerBase observedPicker;

	private IPCycler ipcycler_0;

	private void Awake()
	{
		ipcycler_0 = base.gameObject.GetComponent(typeof(IPCycler)) as IPCycler;
		if (ipcycler_0 == null)
		{
			Debug.LogError("CyclerClamper must be placed on the same GameObject as IPCycler.");
			UnityEngine.Object.Destroy(this);
		}
	}

	private void Start()
	{
		OnPickerUpdated();
	}

	private void OnEnable()
	{
		IPPickerBase iPPickerBase = observedPicker;
		iPPickerBase.onPickerValueUpdated = (IPPickerBase.PickerValueUpdatedHandler)Delegate.Combine(iPPickerBase.onPickerValueUpdated, new IPPickerBase.PickerValueUpdatedHandler(OnPickerUpdated));
	}

	private void OnDisable()
	{
		IPPickerBase iPPickerBase = observedPicker;
		iPPickerBase.onPickerValueUpdated = (IPPickerBase.PickerValueUpdatedHandler)Delegate.Remove(iPPickerBase.onPickerValueUpdated, new IPPickerBase.PickerValueUpdatedHandler(OnPickerUpdated));
	}

	private void OnPickerUpdated()
	{
		ipcycler_0.Boolean_2 = observedPicker.Int32_0 <= ipcycler_0.Int32_3 / 2;
		ipcycler_0.Boolean_1 = observedPicker.Int32_0 >= observedPicker.Int32_1 - ipcycler_0.Int32_3 / 2 - 1;
	}
}
