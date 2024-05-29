using Photon;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class ShowInfoOfPlayer : Photon.MonoBehaviour
{
	private const int int_0 = 0;

	private GameObject gameObject_0;

	private TextMesh textMesh_0;

	public Font font_0;

	public bool bool_0;

	private void Start()
	{
		if (font_0 == null)
		{
			font_0 = (Font)Resources.FindObjectsOfTypeAll(typeof(Font))[0];
			Debug.LogWarning("No font defined. Found font: " + font_0);
		}
		if (textMesh_0 == null)
		{
			gameObject_0 = new GameObject("3d text");
			gameObject_0.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
			gameObject_0.transform.parent = base.gameObject.transform;
			gameObject_0.transform.localPosition = Vector3.zero;
			MeshRenderer meshRenderer = gameObject_0.AddComponent<MeshRenderer>();
			meshRenderer.material = font_0.material;
			textMesh_0 = gameObject_0.AddComponent<TextMesh>();
			textMesh_0.font = font_0;
			textMesh_0.fontSize = 0;
			textMesh_0.anchor = TextAnchor.MiddleCenter;
		}
		if (!bool_0 && base.PhotonView_0.Boolean_1)
		{
			base.enabled = false;
		}
	}

	private void OnEnable()
	{
		if (gameObject_0 != null)
		{
			gameObject_0.SetActive(true);
		}
	}

	private void OnDisable()
	{
		if (gameObject_0 != null)
		{
			gameObject_0.SetActive(false);
		}
	}

	private void Update()
	{
		if (bool_0)
		{
			base.enabled = false;
			if (gameObject_0 != null)
			{
				gameObject_0.SetActive(false);
			}
			return;
		}
		PhotonPlayer photonPlayer_ = base.PhotonView_0.PhotonPlayer_0;
		if (photonPlayer_ != null)
		{
			textMesh_0.text = ((!string.IsNullOrEmpty(photonPlayer_.String_0)) ? photonPlayer_.String_0 : "n/a");
		}
		else if (base.PhotonView_0.Boolean_0)
		{
			if (!bool_0 && base.PhotonView_0.Boolean_1)
			{
				base.enabled = false;
				gameObject_0.SetActive(false);
			}
			else
			{
				textMesh_0.text = "scn";
			}
		}
		else
		{
			textMesh_0.text = "n/a";
		}
	}
}
