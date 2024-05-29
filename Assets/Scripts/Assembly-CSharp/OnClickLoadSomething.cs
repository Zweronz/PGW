using UnityEngine;

public class OnClickLoadSomething : MonoBehaviour
{
	public enum ResourceTypeOption : byte
	{
		Scene = 0,
		Web = 1
	}

	public ResourceTypeOption ResourceTypeToLoad;

	public string ResourceToLoad;

	public void OnClick()
	{
		switch (ResourceTypeToLoad)
		{
		case ResourceTypeOption.Web:
			Application.OpenURL(ResourceToLoad);
			break;
		case ResourceTypeOption.Scene:
			Application.LoadLevel(ResourceToLoad);
			break;
		}
	}
}
