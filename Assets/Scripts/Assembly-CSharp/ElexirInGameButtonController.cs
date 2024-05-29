using UnityEngine;

public class ElexirInGameButtonController : MonoBehaviour
{
	public bool isActivePotion;

	public UIButton myButton;

	public UILabel myLabelTime;

	public UILabel myLabelCount;

	public GameObject plusSprite;

	public GameObject myPotion;

	public GameObject priceLabel;

	public GameObject lockSprite;

	private void Awake()
	{
	}

	private void Start()
	{
		plusSprite.SetActive(false);
		priceLabel.SetActive(false);
	}

	public void SetStateBuy()
	{
	}

	public void SetStateUse()
	{
		myButton.String_0 = "game_clear";
		myButton.string_1 = "game_clear_n";
		myLabelCount.gameObject.SetActive(true);
		plusSprite.SetActive(false);
		priceLabel.SetActive(false);
		if (!isActivePotion)
		{
			myLabelTime.enabled = false;
		}
	}

	private void Update()
	{
	}

	private void OnDestroy()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}
}
