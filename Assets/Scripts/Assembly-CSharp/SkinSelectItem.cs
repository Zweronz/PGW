using System;
using System.Collections;
using UnityEngine;
using engine.helpers;

public class SkinSelectItem : MonoBehaviour
{
	public enum SkinSelectItemState
	{
		STATE_UNSELECTED = 0,
		STATE_SELECTED = 1,
		STATE_CAN_BUY = 2,
		STATE_BUY = 3,
		STATE_NEEDS = 4
	}

	public const int int_0 = -5;

	public UITexture image;

	public UITexture lockImage;

	public Transform model;

	public SkinSelectItemStateUnselected stateUnselected;

	public SkinSelectItemStateSelected stateSelected;

	public SkinSelectItemStateCanBuy stateCanBuy;

	public SkinSelectItemStateBuy stateBuy;

	public UIWidget[] states;

	private ArtikulData artikulData_0;

	private ShopArtikulData shopArtikulData_0;

	private SkinSelectItemState skinSelectItemState_0;

	private bool bool_0;

	private string string_0;

	private bool bool_1;

	private Action<SkinSelectItem> action_0;

	private static GameObject gameObject_0;

	private static Shader shader_0;

	private static Shader shader_1;

	public ArtikulData ArtikulData_0
	{
		get
		{
			return artikulData_0;
		}
	}

	public ShopArtikulData ShopArtikulData_0
	{
		get
		{
			return shopArtikulData_0;
		}
	}

	private ArtikulData GetEmptySkin()
	{
		ArtikulData artikulData = new ArtikulData();
		artikulData.Int32_0 = -5;
		artikulData.SlotType_0 = SlotType.SLOT_WEAR_SKIN;
		return artikulData;
	}

	public void SetData(ShopArtikulData shopArtikulData_1)
	{
		shopArtikulData_0 = shopArtikulData_1;
		artikulData_0 = shopArtikulData_1.GetArtikul();
		if (shopArtikulData_0.Int32_1 == -5)
		{
			UpdateItemEmptySkin();
		}
		else
		{
			UpdateItem();
		}
	}

	public void SetCallbacks(Action<SkinSelectItem> action_1)
	{
		action_0 = action_1;
	}

	public void UpdateItem()
	{
		if (shopArtikulData_0 != null && artikulData_0 != null)
		{
			string_0 = artikulData_0.String_4;
			if (SkinsController.CanWearCustomWear(artikulData_0.Int32_0))
			{
				string_0 = SkinsController.GetCustomWearName(artikulData_0.Int32_0);
			}
			InitStates();
			UpdateState();
		}
	}

	public void TryLoadModel()
	{
		NGUITools.SetActive(image.gameObject, false);
		if (shopArtikulData_0.Int32_1 == -5)
		{
			StartCoroutine(LoadModelEmptySkin());
		}
		else if (artikulData_0 != null)
		{
			StartCoroutine(LoadModel());
		}
	}

	private void InitStates()
	{
		int num = ((artikulData_0 != null) ? shopArtikulData_0.GetPrice() : 0);
		string string_ = ((shopArtikulData_0.MoneyType_0 != 0) ? "gems_ico" : "coin_ico");
		Vector3 localPosition = image.transform.localPosition;
		localPosition.y = -1f;
		image.transform.localPosition = localPosition;
		NGUITools.SetActive(lockImage.gameObject, false);
		stateUnselected.SetData(string_0);
		stateSelected.SetData(string_0);
		stateCanBuy.SetData(string_0, string_, num);
		stateBuy.SetData(string_0, shopArtikulData_0);
	}

	public void UpdateState()
	{
		if (shopArtikulData_0 != null && shopArtikulData_0.Int32_1 == -5)
		{
			UpdateStateEmprySkin();
		}
		else
		{
			if (shopArtikulData_0 == null || artikulData_0 == null)
			{
				return;
			}
			SkinSelectItemState skinSelectItemState = skinSelectItemState_0;
			bool_1 = false;
			WearData wear = WearController.WearController_0.GetWear(artikulData_0.Int32_0);
			if (wear != null && wear.Boolean_0)
			{
				bool_1 = true;
			}
			NeedData needData_ = null;
			bool flag = ArtikulController.ArtikulController_0.CheckNeeds(artikulData_0.Int32_0, out needData_);
			bool flag2 = UserController.UserController_0.HasUserArtikul(artikulData_0.Int32_0);
			skinSelectItemState_0 = ((!flag2) ? ((!flag) ? SkinSelectItemState.STATE_NEEDS : SkinSelectItemState.STATE_CAN_BUY) : SkinSelectItemState.STATE_UNSELECTED);
			if (bool_0)
			{
				if (skinSelectItemState_0 == SkinSelectItemState.STATE_UNSELECTED)
				{
					skinSelectItemState_0 = SkinSelectItemState.STATE_SELECTED;
				}
				else if (skinSelectItemState_0 == SkinSelectItemState.STATE_CAN_BUY)
				{
					skinSelectItemState_0 = SkinSelectItemState.STATE_BUY;
				}
			}
			if (skinSelectItemState != skinSelectItemState_0 || skinSelectItemState_0 == SkinSelectItemState.STATE_UNSELECTED)
			{
				SwitchState(skinSelectItemState, skinSelectItemState_0);
			}
		}
	}

	public void SetSelected(bool bool_2)
	{
		bool_0 = bool_2;
		UpdateState();
	}

	private void SwitchState(SkinSelectItemState skinSelectItemState_1, SkinSelectItemState skinSelectItemState_2)
	{
		NGUITools.SetActive(states[(int)skinSelectItemState_1].gameObject, false);
		NGUITools.SetActive(states[(int)skinSelectItemState_2].gameObject, true);
	}

	private IEnumerator LoadModel()
	{
		GameObject gameObject = null;
		Vector3 vector3_ = Vector3.zero;
		Vector3 localPosition = Vector3.zero;
		Vector3 eulerAngles = Vector3.zero;
		Vector3 localScale = Vector3.one;
		NGUITools.SetActive(image.gameObject, false);
		switch (artikulData_0.SlotType_0)
		{
		case SlotType.SLOT_WEAR_CAPE:
		{
			GameObject gameObject3 = UserController.UserController_0.GetGameObject(artikulData_0.Int32_0);
			GameObject gameObjectInstance = ShopWindow.GetGameObjectInstance(artikulData_0.Int32_0, gameObject3.transform.GetChild(0).gameObject);
			ShopPositionParams component2 = gameObject3.GetComponent<ShopPositionParams>();
			localPosition = new Vector3(0f, -5f, -100f);
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
			if (gameObject_0 == null)
			{
				gameObject_0 = Resources.Load<GameObject>("PixlManForSkins");
			}
			GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject_0) as GameObject;
			Texture skinTexture = SkinsController.GetSkinTexture(artikulData_0.Int32_0);
			Utility.SetLayerRecursive(gameObject2, LayerMask.NameToLayer("NGUIRoot"));
			Utility.SetTextureRecursiveFrom(gameObject2, skinTexture, new GameObject[0]);
			localPosition = new Vector3(0f, -130f, -100f);
			gameObject2.transform.localPosition = new Vector3(0f, -83f, -50f);
			gameObject2.transform.localRotation = Quaternion.Euler(new Vector3(4f, -156f, 0f));
			gameObject2.transform.localScale = new Vector3(85f, 85f, 85f);
			MeshRenderer component = gameObject2.transform.GetChild(0).GetComponent<MeshRenderer>();
			if (shader_1 == null)
			{
				shader_1 = Shader.Find("Custom/TextureClipLocal");
			}
			component.material.shader = shader_1;
			component.material.SetVector("_ClipBounds", new Vector4(0.746f, -0.697f, 10f, -10f));
			component.material.SetVector("_ClipLocal", new Vector4(0f, 0.81f, 0f, 0f));
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

	private GameObject GetMesh(GameObject gameObject_1, out Vector3 vector3_0)
	{
		GameObject gameObject = null;
		vector3_0 = Vector3.zero;
		if (gameObject_1 != null)
		{
			Material[] array = null;
			Mesh mesh = null;
			SkinnedMeshRenderer skinnedMeshRenderer = gameObject_1.GetComponent<SkinnedMeshRenderer>();
			if (skinnedMeshRenderer == null)
			{
				SkinnedMeshRenderer[] componentsInChildren = gameObject_1.GetComponentsInChildren<SkinnedMeshRenderer>(true);
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
				MeshFilter component = gameObject_1.GetComponent<MeshFilter>();
				MeshRenderer component2 = gameObject_1.GetComponent<MeshRenderer>();
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
					if (shader_0 == null)
					{
						shader_0 = Shader.Find("Custom/TextureClip");
					}
					material.shader = shader_0;
					material.SetVector("_ClipBounds", new Vector4(0.746f, -0.697f, 10f, -10f));
				}
				meshRenderer.materials = array;
				vector3_0 = -meshRenderer.bounds.center;
			}
		}
		return gameObject;
	}

	public void OnClick()
	{
		if (!bool_0)
		{
			if (SkinEditController.SkinEditController_0.skinEditorState_0 == SkinEditController.SkinEditorState.STATE_EDITED)
			{
				MessageWindowConfirm.Show(new MessageWindowConfirmParams(LocalizationStorage.Get.Term("window.skin_editor.exit_select.confirm"), OnExitOk, LocalizationStorage.Get.Term("ui.common.btn.yes"), KeyCode.Return, OnExitCancel, LocalizationStorage.Get.Term("ui.common.btn.no")));
			}
			else
			{
				ClickProcess();
			}
		}
	}

	public void ClickProcess()
	{
		if (shopArtikulData_0 != null && shopArtikulData_0.Int32_1 == -5)
		{
			SetSelected(true);
			if (action_0 != null)
			{
				action_0(this);
			}
		}
		else if (shopArtikulData_0 != null && artikulData_0 != null && !bool_0)
		{
			Debug.Log("SkinSelectItem::OnClick > artikulId " + shopArtikulData_0.Int32_1);
			SetSelected(true);
			if (action_0 != null)
			{
				action_0(this);
			}
		}
	}

	private void OnExitOk()
	{
		if (MessageWindowConfirm.MessageWindowConfirm_0 != null)
		{
			MessageWindowConfirm.MessageWindowConfirm_0.Hide();
		}
		if (SkinSelectWindow.SkinSelectWindow_0 != null)
		{
			SkinSelectWindow.SkinSelectWindow_0.OnSaveButtonClick();
		}
	}

	private void OnExitCancel()
	{
		SkinEditController.SkinEditController_0.skinEditorState_0 = SkinEditController.SkinEditorState.STATE_DEFAULT;
		if (MessageWindowConfirm.MessageWindowConfirm_0 != null)
		{
			MessageWindowConfirm.MessageWindowConfirm_0.Hide();
		}
		ClickProcess();
	}

	private void UpdateItemEmptySkin()
	{
		string_0 = LocalizationStorage.Get.Term("window.skin_select.empty_skin");
		NGUITools.SetActive(lockImage.gameObject, false);
		InitStates();
		UpdateState();
	}

	private void UpdateStateEmprySkin()
	{
		SkinSelectItemState skinSelectItemState_ = skinSelectItemState_0;
		skinSelectItemState_0 = (bool_0 ? SkinSelectItemState.STATE_SELECTED : SkinSelectItemState.STATE_UNSELECTED);
		SwitchState(skinSelectItemState_, skinSelectItemState_0);
	}

	private IEnumerator LoadModelEmptySkin()
	{
		GameObject gameObject = null;
		Vector3 zero = Vector3.zero;
		Vector3 zero2 = Vector3.zero;
		Vector3 zero3 = Vector3.zero;
		Vector3 one = Vector3.one;
		NGUITools.SetActive(image.gameObject, true);
		image.Texture_0 = ImageLoader.LoadTexture("UI/images/skin_empty");
		Vector3 localPosition = image.transform.localPosition;
		localPosition.y = -7f;
		image.transform.localPosition = localPosition;
		gameObject = null;
		if (gameObject_0 == null)
		{
			gameObject_0 = Resources.Load<GameObject>("PixlManForSkins");
		}
		if (gameObject != null)
		{
			GameObject gameObject2 = new GameObject
			{
				name = gameObject.name
			};
			gameObject.transform.localPosition = zero;
			gameObject.transform.parent = gameObject2.transform;
			Utility.SetLayerRecursive(gameObject2, LayerMask.NameToLayer("NGUIRoot"));
			gameObject2.transform.parent = base.gameObject.transform;
			model = gameObject2.transform;
			model.localPosition = zero2;
			model.localScale = one;
			model.Rotate(zero3, Space.World);
		}
		yield return null;
	}
}
