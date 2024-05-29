using UnityEngine;

public class ShopCarouselElement : MonoBehaviour
{
	public Transform arrow;

	public UILabel topSeller;

	public UILabel quantity;

	public UILabel newnew;

	public bool showTS;

	public bool showNew;

	public bool showQuantity;

	public string prefabPath;

	public Vector3 baseScale;

	public Vector3 ourPosition;

	public string itemID;

	public string readableName;

	public Transform model;

	private float float_0;

	public Vector3 arrnoInitialPos;

	public void SetQuantity()
	{
	}

	private void Awake()
	{
		arrnoInitialPos = new Vector3(70.05f, -0.00016f, -100f);
	}

	private void Start()
	{
	}

	private void HandlePotionActivated(string string_0)
	{
	}

	public void SetPos(float float_1, float float_2)
	{
		if (model != null)
		{
			model.localScale = baseScale * float_1;
			model.localPosition = ourPosition * float_1 + new Vector3(float_2, 0f, 0f);
		}
		if (arrow != null)
		{
			arrow.localScale = new Vector3(1f, 1f, 1f) * float_1;
			arrow.localPosition = new Vector3(arrnoInitialPos.x * float_1, arrnoInitialPos.y * float_1, arrnoInitialPos.z) + new Vector3(float_2, 0f, 0f);
		}
	}

	private void OnDestroy()
	{
	}
}
