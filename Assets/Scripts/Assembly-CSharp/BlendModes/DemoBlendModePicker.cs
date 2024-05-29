using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace BlendModes
{
	public class DemoBlendModePicker : MonoBehaviour
	{
		public static BlendMode blendMode_0 = BlendMode.ColorDodge;

		private void Start()
		{
			List<Button> list_0 = new List<Button>(22);
			foreach (Transform item in base.transform)
			{
				if ((bool)item.GetComponent<Button>())
				{
					list_0.Add(item.GetComponent<Button>());
				}
			}
			for (int i = 0; i < 22; i++)
			{
				int int_0 = i;
				list_0[int_0].GetComponentInChildren<Text>().text = Regex.Replace(((BlendMode)int_0).ToString(), "(\\B[A-Z])", " $1");
				if (blendMode_0 == (BlendMode)int_0)
				{
					list_0[int_0].GetComponent<Image>().color = Color.green;
				}
				list_0[int_0].onClick.RemoveAllListeners();
				list_0[int_0].onClick.AddListener(delegate
				{
					blendMode_0 = (BlendMode)int_0;
					foreach (Button item2 in list_0)
					{
						item2.GetComponent<Image>().color = ((!(item2 == list_0[int_0])) ? Color.white : Color.green);
					}
				});
			}
		}
	}
}
