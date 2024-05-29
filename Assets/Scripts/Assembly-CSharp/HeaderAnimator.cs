using UnityEngine;

public class HeaderAnimator : MonoBehaviour
{
	public UISprite leftWing;

	public UISprite rightWing;

	public UISprite plate;

	public UISprite sprite;

	public UILabel label;

	public UIWidget widget;

	public UIWidget Container;

	private int int_0;

	private void Awake()
	{
		widget.Single_2 = 0f;
	}

	private void Start()
	{
	}

	private void Update()
	{
		int_0++;
		if (int_0 == 5)
		{
			lastAction();
		}
	}

	private void lastAction()
	{
		plate.transform.localScale = new Vector3(0.21f, 1f, 1f);
		plate.GetComponent<TweenScale>().enabled = true;
		leftWing.GetComponent<TweenPosition>().vector3_1 = new Vector3(leftWing.transform.localPosition.x, leftWing.transform.localPosition.y, leftWing.transform.localPosition.z);
		float x = -(leftWing.Int32_0 / 2);
		leftWing.transform.localPosition = new Vector3(x, leftWing.transform.localPosition.y, leftWing.transform.localPosition.z);
		leftWing.GetComponent<TweenPosition>().vector3_0 = new Vector3(leftWing.transform.localPosition.x, leftWing.transform.localPosition.y, leftWing.transform.localPosition.z);
		leftWing.GetComponent<TweenPosition>().enabled = true;
		rightWing.GetComponent<TweenPosition>().vector3_1 = new Vector3(rightWing.transform.localPosition.x, rightWing.transform.localPosition.y, rightWing.transform.localPosition.z);
		x = 0 + rightWing.Int32_0 / 2;
		rightWing.transform.localPosition = new Vector3(x, rightWing.transform.localPosition.y, rightWing.transform.localPosition.z);
		rightWing.GetComponent<TweenPosition>().vector3_0 = new Vector3(rightWing.transform.localPosition.x, rightWing.transform.localPosition.y, rightWing.transform.localPosition.z);
		rightWing.GetComponent<TweenPosition>().enabled = true;
		sprite.GetComponent<TweenPosition>().vector3_1 = new Vector3(sprite.transform.localPosition.x, sprite.transform.localPosition.y, sprite.transform.localPosition.z);
		x = -(rightWing.Int32_0 / 2);
		sprite.transform.localPosition = new Vector3(x, sprite.transform.localPosition.y, sprite.transform.localPosition.z);
		sprite.GetComponent<TweenPosition>().vector3_0 = new Vector3(sprite.transform.localPosition.x, sprite.transform.localPosition.y, sprite.transform.localPosition.z);
		sprite.GetComponent<TweenPosition>().enabled = true;
		label.transform.localScale = new Vector3(0f, 0.65f, 0.65f);
		label.GetComponent<TweenScale>().enabled = true;
		Container.GetComponent<ObjectHorizontalPositioner>().enabled = false;
		setNullAnchors(leftWing);
		setNullAnchors(rightWing);
		setNullAnchors(sprite);
		setNullAnchors(plate);
		widget.GetComponent<TweenAlpha>().enabled = true;
		base.enabled = false;
	}

	private void setNullAnchors(UIWidget uiwidget_0)
	{
		setNullAnchor(uiwidget_0.leftAnchor);
		setNullAnchor(uiwidget_0.rightAnchor);
		setNullAnchor(uiwidget_0.topAnchor);
		setNullAnchor(uiwidget_0.bottomAnchor);
	}

	private void setNullAnchor(UIRect.AnchorPoint anchorPoint_0)
	{
		anchorPoint_0.target = null;
		anchorPoint_0.rect = null;
		anchorPoint_0.targetCam = null;
		anchorPoint_0.relative = 0f;
		anchorPoint_0.absolute = 0;
	}
}
