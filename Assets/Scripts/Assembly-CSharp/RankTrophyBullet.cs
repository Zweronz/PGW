using UnityEngine;

public class RankTrophyBullet : MonoBehaviour
{
	public UISprite icon;

	private bool bool_0;

	public int Int32_0
	{
		get
		{
			return icon.Int32_0;
		}
	}

	public int Int32_1
	{
		get
		{
			return icon.Int32_1;
		}
	}

	private void Start()
	{
		UpdateIcon();
	}

	public void SetActiveState(bool bool_1)
	{
		bool_0 = bool_1;
	}

	public void AnimateAppear()
	{
		base.GetComponent<Animation>().Play("BulletAppear");
	}

	public void AnimateDisappear()
	{
		base.GetComponent<Animation>().Play("BulletDisappear");
	}

	public void AnimateCount()
	{
		base.GetComponent<Animation>().Play("BulletCount");
	}

	public void OnAppearComplete()
	{
		bool_0 = true;
		UpdateIcon();
	}

	public void OnDisappearComplete()
	{
		bool_0 = false;
		UpdateIcon();
	}

	private void UpdateIcon()
	{
		icon.String_0 = string.Format("bullet_{0}", (!bool_0) ? "inactive" : "active");
	}
}
