using UnityEngine;

public class ShrinkPanelObject : MonoBehaviour
{
	public ShrinkPanel myPanel;

	public UIPanel panel;

	public UIWidget header;

	public UIWidget body;

	public UIButton openButton;

	public UIButton closeButton;

	public float height;

	public bool _isOpen = true;

	private void Start()
	{
		openButton.gameObject.SetActive(!_isOpen);
		closeButton.gameObject.SetActive(_isOpen);
	}

	public void moveEnd()
	{
		_isOpen = !_isOpen;
		openButton.gameObject.SetActive(!_isOpen);
		closeButton.gameObject.SetActive(_isOpen);
	}

	public void OnOpenButtonClick()
	{
		myPanel.OnOpenButtonClick(this);
	}

	public void OnCloseButtonClick()
	{
		myPanel.OnCloseButtonClick(this);
	}
}
