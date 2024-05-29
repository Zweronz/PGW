using UnityEngine;

public sealed class ClanMembersSortHandler : MonoBehaviour
{
	public enum SortType
	{
		Place = 0,
		Nick = 1,
		Level = 2,
		Score = 3,
		Status = 4
	}

	public UISprite sortArrow;

	public SortType sortType;

	private bool bool_0;

	private bool bool_1;

	public void ResetFlip()
	{
		if (!bool_1)
		{
			sortArrow.Flip_0 = UIBasicSprite.Flip.Nothing;
			bool_0 = false;
		}
	}

	private void OnClick()
	{
		if (ClanWindow.Boolean_1)
		{
			bool_0 = !bool_0;
			bool_1 = true;
			ClanWindow.ClanWindow_0.SortMembers(sortType, bool_0);
			sortArrow.Flip_0 = (bool_0 ? UIBasicSprite.Flip.Vertically : UIBasicSprite.Flip.Nothing);
			bool_1 = false;
		}
	}
}
