using System.Linq;
using UnityEngine;

public class StockViewController : MonoBehaviour
{
	public BaseStockView[] containers;

	private void Awake()
	{
		Subscibe();
		UpdateStockView(null);
	}

	private void OnDestroy()
	{
		Unsubscribe();
	}

	private void Subscibe()
	{
		UserOverrideContentGroupStorage.Subscribe(OverrideContentGroupEventType.UPDATE_ALL, UpdateStockView);
		UserOverrideContentGroupStorage.Subscribe(OverrideContentGroupEventType.REMOVE_ALL, UpdateStockView);
	}

	public void Unsubscribe()
	{
		UserOverrideContentGroupStorage.Unsubscribe(OverrideContentGroupEventType.UPDATE_ALL, UpdateStockView);
		UserOverrideContentGroupStorage.Unsubscribe(OverrideContentGroupEventType.REMOVE_ALL, UpdateStockView);
	}

	public void UpdateStockTable()
	{
		Invoke("UpdateStockView", 0.1f);
	}

	private void UpdateStockView(OverrideContentGroupEventData overrideContentGroupEventData_0)
	{
		for (int i = 0; i < containers.Count(); i++)
		{
			containers[i].UpdateStock();
		}
	}
}
