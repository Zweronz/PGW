public class ShopSkinComparePanel : ShopItemComparePanel
{
	public ShopItemSkillsBigPanel shopItemSkillsBigPanel_0;

	public ShopItemSkillsBigPanel shopItemSkillsBigPanel_1;

	protected override void InitComponents()
	{
		shopItemSkillsBigPanel_0.Init(artikulData_0);
		shopItemSkillsBigPanel_1.Init(artikulData_1);
	}
}
