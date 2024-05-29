using UnityEngine;

namespace engine.unity
{
	public abstract class UITabContent : MonoBehaviour
	{
		public UIWidget container;

		private void Start()
		{
			InitContent();
		}

		public void Activate(bool bool_0)
		{
			NGUITools.SetActive(container.gameObject, bool_0);
			if (bool_0)
			{
				UpdateContent();
			}
		}

		public abstract void InitContent();

		public abstract void UpdateContent();
	}
}
