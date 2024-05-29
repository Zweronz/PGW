using UnityEngine;

public class BattleOverTitleScript : MonoBehaviour
{
	public UISprite leftWing;

	public UISprite rightWing;

	public UISprite skull;

	public new UILabel name;

	public UILabel kills;

	public string nameTxt;

	public string killsTxt;

	public void DoIt()
	{
		name.String_0 = nameTxt;
		kills.String_0 = killsTxt;
		int num = 0;
		if (leftWing != null)
		{
			leftWing.transform.localPosition = new Vector3(0f, leftWing.transform.localPosition.y, leftWing.transform.localPosition.z);
			num += Mathf.RoundToInt((float)leftWing.Int32_0 * leftWing.transform.localScale.x);
			num += 5;
		}
		if (name != null)
		{
			Transform transform = name.transform;
			transform.localPosition = new Vector3(num, transform.localPosition.y, transform.localPosition.z);
			num += Mathf.RoundToInt((float)name.Int32_0 * transform.localScale.x);
			num += 5;
		}
		if (skull != null)
		{
			skull.transform.localPosition = new Vector3(num, skull.transform.localPosition.y, skull.transform.localPosition.z);
			num += Mathf.RoundToInt((float)skull.Int32_0 * skull.transform.localScale.x);
			num += 5;
		}
		if (kills != null)
		{
			kills.transform.localPosition = new Vector3(num, kills.transform.localPosition.y, kills.transform.localPosition.z);
			num += Mathf.RoundToInt((float)kills.Int32_0 * kills.transform.localScale.x);
			num += 5;
		}
		if (rightWing != null)
		{
			rightWing.transform.localPosition = new Vector3(num, rightWing.transform.localPosition.y, rightWing.transform.localPosition.z);
			num += Mathf.RoundToInt((float)rightWing.Int32_0 * rightWing.transform.localScale.x);
		}
		UIWidget component = base.gameObject.GetComponent<UIWidget>();
		base.gameObject.transform.parent.GetComponent<UIWidget>();
		component.Int32_0 = num;
		component.transform.localPosition = new Vector3(kills.Vector2_3.x / 2f - (float)(num / 2), component.transform.localPosition.y, component.transform.localPosition.z);
	}
}
