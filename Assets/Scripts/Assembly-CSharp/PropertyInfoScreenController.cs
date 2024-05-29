using UnityEngine;

public class PropertyInfoScreenController : MonoBehaviour
{
	public GameObject description;

	public GameObject descriptionMelee;

	public virtual void Show(bool bool_0)
	{
		base.gameObject.SetActive(true);
		((!bool_0) ? description : descriptionMelee).SetActive(true);
		((!bool_0) ? descriptionMelee : description).SetActive(false);
	}

	public virtual void Hide()
	{
		base.gameObject.SetActive(false);
	}
}
