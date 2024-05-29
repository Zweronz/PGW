using UnityEngine;

public class StaticOccluderController : MonoBehaviour
{
	private void Awake()
	{
		HighlightableObject highlightableObject = base.gameObject.AddComponent<HighlightableObject>();
		highlightableObject.OccluderOn();
	}
}
