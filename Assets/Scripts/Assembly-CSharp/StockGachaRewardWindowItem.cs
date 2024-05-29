using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using engine.helpers;

public class StockGachaRewardWindowItem : MonoBehaviour
{
	public UILabel title;

	public UILabel count;

	public UITexture icon;

	public Transform model;

	public UISprite timeIcon;

	public KeyValuePair<int, int> art = default(KeyValuePair<int, int>);

	public KeyValuePair<MoneyType, int> money = default(KeyValuePair<MoneyType, int>);

	public int exp;

	private ArtikulData artikulData_0;

	private void Start()
	{
		timeIcon.gameObject.SetActive(false);
		ArtikulData artikul = ArtikulController.ArtikulController_0.GetArtikul(art.Key);
		if (artikul != null)
		{
			artikulData_0 = artikul;
			title.String_0 = artikul.String_4;
			count.String_0 = "x" + art.Value;
			if (artikul.SlotType_0 != SlotType.SLOT_WEAR_CAPE && artikul.SlotType_0 != SlotType.SLOT_WEAR_SKIN)
			{
				icon.Texture_0 = ImageLoader.LoadArtikulTexture(artikul.Int32_0);
			}
			else
			{
				NGUITools.SetActive(icon.gameObject, false);
				StartCoroutine(LoadModel());
			}
			timeIcon.gameObject.SetActive(artikul.Int32_5 > 0);
		}
		else if (money.Value > 0)
		{
			title.String_0 = Localizer.Get("ui.gacha_item.money");
			count.String_0 = "x" + money.Value;
			icon.Texture_0 = ImageLoader.LoadTexture("UI/images/coins");
		}
		else if (exp > 0)
		{
			title.String_0 = Localizer.Get("ui.gacha_item.exp");
			count.String_0 = "x" + exp;
			icon.Texture_0 = ImageLoader.LoadTexture("UI/images/exp");
		}
	}

	private IEnumerator LoadModel()
	{
		GameObject gameObject = null;
		Vector3 vector3_ = Vector3.zero;
		Vector3 localPosition = Vector3.zero;
		Vector3 eulerAngles = Vector3.zero;
		Vector3 localScale = Vector3.one;
		switch (artikulData_0.SlotType_0)
		{
		case SlotType.SLOT_WEAR_CAPE:
		{
			GameObject gameObject3 = UserController.UserController_0.GetGameObject(artikulData_0.Int32_0);
			GameObject gameObjectInstance = StockGachaRewardWindow.GetGameObjectInstance(artikulData_0.Int32_0, gameObject3.transform.GetChild(0).gameObject);
			ShopPositionParams component2 = gameObject3.GetComponent<ShopPositionParams>();
			localPosition = new Vector3(80f, -55f, -100f);
			eulerAngles = component2.rotationShop;
			localScale = new Vector3(80f, 80f, 80f);
			gameObject = GetMesh(gameObjectInstance, out vector3_);
			WearData wear2 = WearController.WearController_0.GetWear(artikulData_0.Int32_0);
			if (wear2.Boolean_0)
			{
				Texture capeTexture = SkinsController.GetCapeTexture(wear2.Int32_0);
				if (capeTexture != null)
				{
					MeshRenderer component3 = gameObject.GetComponent<MeshRenderer>();
					component3.material.SetTexture("_MainTex", capeTexture);
				}
			}
			break;
		}
		case SlotType.SLOT_WEAR_SKIN:
		{
			GameObject original = Resources.Load<GameObject>("PixlManForSkins");
			GameObject gameObject2 = Object.Instantiate(original) as GameObject;
			Texture skinTexture = SkinsController.GetSkinTexture(artikulData_0.Int32_0);
			Utility.SetLayerRecursive(gameObject2, LayerMask.NameToLayer("NGUIRoot"));
			Utility.SetTextureRecursiveFrom(gameObject2, skinTexture, new GameObject[0]);
			localPosition = new Vector3(120f, -262f, -100f);
			gameObject2.transform.localPosition = new Vector3(0f, -83f, -50f);
			gameObject2.transform.localRotation = Quaternion.Euler(new Vector3(4f, -156f, 0f));
			gameObject2.transform.localScale = new Vector3(100f, 100f, 100f);
			MeshRenderer component = gameObject2.transform.GetChild(0).GetComponent<MeshRenderer>();
			Shader shader = Shader.Find("Custom/TextureClipLocal");
			component.material.shader = shader;
			component.material.SetVector("_ClipBounds", new Vector4(0.73f, -1.76f, 1f, -1f));
			component.material.SetVector("_ClipLocal", new Vector4(0f, -0.81f, 0f, 0f));
			component.material.SetFloat("_AlphaFrom", 0f);
			component.material.SetFloat("_AlphaCoeff", 1f);
			WearData wear = WearController.WearController_0.GetWear(artikulData_0.Int32_0);
			if (wear.Boolean_0)
			{
				Texture skinTexture2 = SkinsController.GetSkinTexture(wear.Int32_0);
				if (skinTexture2 != null)
				{
					component.material.SetTexture("_MainTex", skinTexture2);
				}
			}
			vector3_ = new Vector3(0f, -1f, 0f);
			gameObject = gameObject2;
			break;
		}
		}
		if (gameObject != null)
		{
			GameObject gameObject4 = new GameObject
			{
				name = gameObject.name
			};
			gameObject.transform.localPosition = vector3_;
			gameObject.transform.parent = gameObject4.transform;
			Utility.SetLayerRecursive(gameObject4, LayerMask.NameToLayer("NGUIRoot"));
			gameObject4.transform.parent = base.gameObject.transform;
			model = gameObject4.transform;
			model.localPosition = localPosition;
			model.localScale = localScale;
			model.Rotate(eulerAngles, Space.World);
		}
		yield return null;
	}

	public static GameObject GetMesh(GameObject gameObject_0, out Vector3 vector3_0, bool bool_0 = false)
	{
		GameObject gameObject = null;
		vector3_0 = Vector3.zero;
		if (gameObject_0 != null)
		{
			Material[] array = null;
			Mesh mesh = null;
			SkinnedMeshRenderer skinnedMeshRenderer = gameObject_0.GetComponent<SkinnedMeshRenderer>();
			if (skinnedMeshRenderer == null)
			{
				SkinnedMeshRenderer[] componentsInChildren = gameObject_0.GetComponentsInChildren<SkinnedMeshRenderer>(true);
				if (componentsInChildren != null && componentsInChildren.Length > 0)
				{
					skinnedMeshRenderer = componentsInChildren[0];
				}
			}
			if (skinnedMeshRenderer != null)
			{
				array = skinnedMeshRenderer.materials;
				mesh = skinnedMeshRenderer.sharedMesh;
			}
			else
			{
				MeshFilter component = gameObject_0.GetComponent<MeshFilter>();
				MeshRenderer component2 = gameObject_0.GetComponent<MeshRenderer>();
				if (component != null)
				{
					mesh = component.mesh;
				}
				if (component2 != null)
				{
					array = component2.materials;
				}
			}
			if (array != null && mesh != null)
			{
				gameObject = new GameObject();
				gameObject.AddComponent<MeshFilter>().sharedMesh = mesh;
				MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
				if (array.Length > 0)
				{
					Material material = array[0];
					Shader shader = Shader.Find("Custom/TextureClip");
					material.shader = shader;
					material.SetVector("_ClipBounds", new Vector4(-0.26f, -0.52f, 0.77f, -0.77f));
				}
				meshRenderer.materials = array;
				vector3_0 = -meshRenderer.bounds.center;
			}
			if (bool_0)
			{
				Object.Destroy(gameObject_0);
			}
		}
		return gameObject;
	}
}
