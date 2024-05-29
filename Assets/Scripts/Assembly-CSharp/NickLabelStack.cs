using UnityEngine;

public class NickLabelStack : MonoBehaviour
{
	public NickLabelController[] lables;

	public new Camera camera;

	public static NickLabelStack nickLabelStack_0;

	private int int_0;

	private void Awake()
	{
		nickLabelStack_0 = this;
	}

	private void Start()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		base.transform.localPosition = Vector3.zero;
		Transform transform = base.transform.GetChild(0).transform;
		base.transform.position = Vector3.zero;
		int childCount = transform.childCount;
		lables = new NickLabelController[childCount];
		for (int i = 0; i < childCount; i++)
		{
			lables[i] = transform.GetChild(i).GetComponent<NickLabelController>();
		}
	}

	private void OnDestroy()
	{
		nickLabelStack_0 = null;
	}

	public NickLabelController GetNextCurrentLabel()
	{
		base.transform.localPosition = Vector3.zero;
		bool flag = true;
		do
		{
			int_0++;
			if (int_0 >= lables.Length)
			{
				if (!flag)
				{
					return null;
				}
				int_0 = 0;
				flag = false;
			}
		}
		while (lables[int_0].Transform_0 != null);
		return lables[int_0];
	}

	public NickLabelController GetCurrentLabel()
	{
		return lables[int_0];
	}

	public void SetCameraActive(bool bool_0)
	{
		if (!(camera == null))
		{
			camera.enabled = bool_0;
		}
	}
}
