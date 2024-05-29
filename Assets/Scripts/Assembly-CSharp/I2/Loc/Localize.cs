using System;
using System.Runtime.CompilerServices;
using ArabicSupport;
using UnityEngine;
using UnityEngine.UI;

namespace I2.Loc
{
	public class Localize : MonoBehaviour
	{
		public enum TermModification
		{
			DontModify = 0,
			ToUpper = 1,
			ToLower = 2
		}

		public delegate bool DelegateSetFinalTerms(string string_0, string string_1, out string string_2, out string string_3);

		public delegate void DelegateDoLocalize(string string_0, string string_1);

		public string mTerm;

		public string mTermSecondary;

		public string FinalTerm;

		public string FinalSecondaryTerm;

		public TermModification PrimaryTermModifier;

		public TermModification SecondaryTermModifier;

		public UnityEngine.Object mTarget;

		public DelegateSetFinalTerms EventSetFinalTerms;

		public DelegateDoLocalize EventDoLocalize;

		public bool CanUseSecondaryTerm;

		public bool AllowMainTermToBeRTL;

		public bool AllowSecondTermToBeRTL;

		public bool IgnoreRTL;

		public UnityEngine.Object[] TranslatedObjects;

		public EventCallback LocalizeCallBack = new EventCallback();

		public static string string_0;

		public static string string_1;

		private UILabel uilabel_0;

		private UISprite uisprite_0;

		private UITexture uitexture_0;

		private Text text_0;

		private Image image_0;

		private RawImage rawImage_0;

		private GUIText guitext_0;

		private TextMesh textMesh_0;

		private AudioSource audioSource_0;

		private GUITexture guitexture_0;

		private GameObject gameObject_0;

		private Action action_0;

		public string String_0
		{
			get
			{
				return mTerm;
			}
			set
			{
				mTerm = value;
			}
		}

		public string String_1
		{
			get
			{
				return mTermSecondary;
			}
			set
			{
				mTermSecondary = value;
			}
		}

		public event Action EventFindTarget
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				action_0 = (Action)Delegate.Combine(action_0, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				action_0 = (Action)Delegate.Remove(action_0, value);
			}
		}

		private void Awake()
		{
			RegisterTargets();
			action_0();
		}

		private void RegisterTargets()
		{
			if (action_0 == null)
			{
				RegisterEvents_NGUI();
				RegisterEvents_DFGUI();
				RegisterEvents_UGUI();
				RegisterEvents_2DToolKit();
				RegisterEvents_TextMeshPro();
				RegisterEvents_UnityStandard();
			}
		}

		private void OnEnable()
		{
			OnLocalize();
		}

		public void OnLocalize()
		{
			if (!base.enabled || !base.gameObject.activeInHierarchy || string.IsNullOrEmpty(LocalizationManager.String_0))
			{
				return;
			}
			if (!HasTargetCache())
			{
				FindTarget();
			}
			if (!HasTargetCache())
			{
				return;
			}
			GetFinalTerms(out FinalTerm, out FinalSecondaryTerm);
			if (string.IsNullOrEmpty(FinalTerm) && string.IsNullOrEmpty(FinalSecondaryTerm))
			{
				return;
			}
			string_0 = LocalizationManager.GetTermTranslation(FinalTerm);
			string_1 = LocalizationManager.GetTermTranslation(FinalSecondaryTerm);
			if (string.IsNullOrEmpty(string_0) && string.IsNullOrEmpty(string_1))
			{
				return;
			}
			LocalizeCallBack.Execute(this);
			if (LocalizationManager.bool_0 && !IgnoreRTL)
			{
				if (AllowMainTermToBeRTL && !string.IsNullOrEmpty(string_0))
				{
					string_0 = ArabicFixer.Fix(string_0);
				}
				if (AllowSecondTermToBeRTL && !string.IsNullOrEmpty(string_1))
				{
					string_1 = ArabicFixer.Fix(string_1);
				}
			}
			switch (PrimaryTermModifier)
			{
			case TermModification.ToLower:
				string_0 = string_0.ToLower();
				break;
			case TermModification.ToUpper:
				string_0 = string_0.ToUpper();
				break;
			}
			switch (SecondaryTermModifier)
			{
			case TermModification.ToLower:
				string_1 = string_1.ToLower();
				break;
			case TermModification.ToUpper:
				string_1 = string_1.ToUpper();
				break;
			}
			EventDoLocalize(string_0, string_1);
		}

		public bool FindTarget()
		{
			if (action_0 == null)
			{
				RegisterTargets();
			}
			action_0();
			return HasTargetCache();
		}

		public void FindAndCacheTarget<T>(ref T gparam_0, DelegateSetFinalTerms delegateSetFinalTerms_0, DelegateDoLocalize delegateDoLocalize_0, bool bool_0, bool bool_1, bool bool_2) where T : Component
		{
			if (mTarget != null)
			{
				gparam_0 = mTarget as T;
			}
			else
			{
				mTarget = (gparam_0 = GetComponent<T>());
			}
			if ((UnityEngine.Object)gparam_0 != (UnityEngine.Object)null)
			{
				EventSetFinalTerms = delegateSetFinalTerms_0;
				EventDoLocalize = delegateDoLocalize_0;
				CanUseSecondaryTerm = bool_0;
				AllowMainTermToBeRTL = bool_1;
				AllowSecondTermToBeRTL = bool_2;
			}
		}

		private void FindAndCacheTarget(ref GameObject gameObject_1, DelegateSetFinalTerms delegateSetFinalTerms_0, DelegateDoLocalize delegateDoLocalize_0, bool bool_0, bool bool_1, bool bool_2)
		{
			if (mTarget != gameObject_1 && (bool)gameObject_1)
			{
				UnityEngine.Object.Destroy(gameObject_1);
			}
			if (mTarget != null)
			{
				gameObject_1 = mTarget as GameObject;
			}
			else
			{
				Transform transform = base.transform;
				mTarget = (gameObject_1 = ((transform.childCount >= 1) ? transform.GetChild(0).gameObject : null));
			}
			if (gameObject_1 != null)
			{
				EventSetFinalTerms = delegateSetFinalTerms_0;
				EventDoLocalize = delegateDoLocalize_0;
				CanUseSecondaryTerm = bool_0;
				AllowMainTermToBeRTL = bool_1;
				AllowSecondTermToBeRTL = bool_2;
			}
		}

		private bool HasTargetCache()
		{
			return EventDoLocalize != null;
		}

		public bool GetFinalTerms(out string string_2, out string string_3)
		{
			if (!mTarget && !HasTargetCache())
			{
				FindTarget();
			}
			if (!string.IsNullOrEmpty(mTerm))
			{
				return SetFinalTerms(mTerm, mTermSecondary, out string_2, out string_3);
			}
			if (!string.IsNullOrEmpty(FinalTerm))
			{
				return SetFinalTerms(FinalTerm, FinalSecondaryTerm, out string_2, out string_3);
			}
			if (EventSetFinalTerms != null)
			{
				return EventSetFinalTerms(mTerm, mTermSecondary, out string_2, out string_3);
			}
			return SetFinalTerms(string.Empty, string.Empty, out string_2, out string_3);
		}

		private bool SetFinalTerms(string string_2, string string_3, out string string_4, out string string_5)
		{
			string_4 = string_2;
			string_5 = string_3;
			return true;
		}

		public void SetTerm(string string_2, string string_3)
		{
			if (!string.IsNullOrEmpty(string_2))
			{
				String_0 = string_2;
			}
			if (!string.IsNullOrEmpty(string_3))
			{
				String_1 = string_3;
			}
			OnLocalize();
		}

		private T GetSecondaryTranslatedObj<T>(ref string string_2, ref string string_3) where T : UnityEngine.Object
		{
			string string_4;
			DeserializeTranslation(string_2, out string_2, out string_4);
			if (string.IsNullOrEmpty(string_4))
			{
				string_4 = string_3;
			}
			if (string.IsNullOrEmpty(string_4))
			{
				return (T)null;
			}
			T translatedObject = GetTranslatedObject<T>(string_4);
			if ((UnityEngine.Object)translatedObject == (UnityEngine.Object)null)
			{
				int num = string_4.LastIndexOfAny("/\\".ToCharArray());
				if (num >= 0)
				{
					string_4 = string_4.Substring(num + 1);
					translatedObject = GetTranslatedObject<T>(string_4);
				}
			}
			return translatedObject;
		}

		private T GetTranslatedObject<T>(string string_2) where T : UnityEngine.Object
		{
			return FindTranslatedObject<T>(string_2);
		}

		private void DeserializeTranslation(string string_2, out string string_3, out string string_4)
		{
			if (!string.IsNullOrEmpty(string_2) && string_2.Length > 1 && string_2[0] == '[')
			{
				int num = string_2.IndexOf(']');
				if (num > 0)
				{
					string_4 = string_2.Substring(1, num - 1);
					string_3 = string_2.Substring(num + 1);
					return;
				}
			}
			string_3 = string_2;
			string_4 = string.Empty;
		}

		public T FindTranslatedObject<T>(string string_2) where T : UnityEngine.Object
		{
			if (string.IsNullOrEmpty(string_2))
			{
				return (T)null;
			}
			if (TranslatedObjects != null)
			{
				int i = 0;
				for (int num = TranslatedObjects.Length; i < num; i++)
				{
					if ((UnityEngine.Object)(TranslatedObjects[i] as T) != (UnityEngine.Object)null && string_2 == TranslatedObjects[i].name)
					{
						return TranslatedObjects[i] as T;
					}
				}
			}
			T val = LocalizationManager.FindAsset(string_2) as T;
			if ((bool)(UnityEngine.Object)val)
			{
				return val;
			}
			return ResourceManager.ResourceManager_0.GetAsset<T>(string_2);
		}

		private bool HasTranslatedObject(UnityEngine.Object object_0)
		{
			if (Array.IndexOf(TranslatedObjects, object_0) >= 0)
			{
				return true;
			}
			return ResourceManager.ResourceManager_0.HasAsset(object_0);
		}

		public void SetGlobalLanguage(string string_2)
		{
			LocalizationManager.String_0 = string_2;
		}

		public static void RegisterEvents_2DToolKit()
		{
		}

		public static void RegisterEvents_DFGUI()
		{
		}

		public void RegisterEvents_NGUI()
		{
			action_0 = (Action)Delegate.Combine(action_0, new Action(FindTarget_UILabel));
			action_0 = (Action)Delegate.Combine(action_0, new Action(FindTarget_UISprite));
			action_0 = (Action)Delegate.Combine(action_0, new Action(FindTarget_UITexture));
		}

		private void FindTarget_UILabel()
		{
			FindAndCacheTarget(ref uilabel_0, SetFinalTerms_UIlabel, DoLocalize_UILabel, true, true, false);
		}

		private void FindTarget_UISprite()
		{
			FindAndCacheTarget(ref uisprite_0, SetFinalTerms_UISprite, DoLocalize_UISprite, true, false, false);
		}

		private void FindTarget_UITexture()
		{
			FindAndCacheTarget(ref uitexture_0, SetFinalTerms_UITexture, DoLocalize_UITexture, false, false, false);
		}

		private bool SetFinalTerms_UIlabel(string string_2, string string_3, out string string_4, out string string_5)
		{
			string string_6 = ((!(uilabel_0.Object_0 != null)) ? string.Empty : uilabel_0.Object_0.name);
			return SetFinalTerms(uilabel_0.String_0, string_6, out string_4, out string_5);
		}

		public bool SetFinalTerms_UISprite(string string_2, string string_3, out string string_4, out string string_5)
		{
			string string_6 = ((!(uisprite_0.UIAtlas_0 != null)) ? string.Empty : uisprite_0.UIAtlas_0.name);
			return SetFinalTerms(uisprite_0.String_0, string_6, out string_4, out string_5);
		}

		public bool SetFinalTerms_UITexture(string string_2, string string_3, out string string_4, out string string_5)
		{
			return SetFinalTerms(uitexture_0.Texture_0.name, null, out string_4, out string_5);
		}

		public void DoLocalize_UILabel(string string_2, string string_3)
		{
			Font secondaryTranslatedObj = GetSecondaryTranslatedObj<Font>(ref string_2, ref string_3);
			if (secondaryTranslatedObj != null)
			{
				uilabel_0.Object_0 = secondaryTranslatedObj;
				return;
			}
			UIFont secondaryTranslatedObj2 = GetSecondaryTranslatedObj<UIFont>(ref string_2, ref string_3);
			if (secondaryTranslatedObj2 != null)
			{
				uilabel_0.Object_0 = secondaryTranslatedObj2;
				return;
			}
			UIInput uIInput = NGUITools.FindInParents<UIInput>(uilabel_0.gameObject);
			if (uIInput != null && uIInput.label == uilabel_0)
			{
				if (!(uIInput.String_0 == string_2))
				{
					uIInput.String_0 = string_2;
				}
			}
			else if (!(uilabel_0.String_0 == string_2))
			{
				uilabel_0.String_0 = string_2;
			}
		}

		public void DoLocalize_UISprite(string string_2, string string_3)
		{
			if (!(uisprite_0.String_0 == string_2))
			{
				UIAtlas secondaryTranslatedObj = GetSecondaryTranslatedObj<UIAtlas>(ref string_2, ref string_3);
				if (secondaryTranslatedObj != null)
				{
					uisprite_0.UIAtlas_0 = secondaryTranslatedObj;
				}
				uisprite_0.String_0 = string_2;
				uisprite_0.MakePixelPerfect();
			}
		}

		public void DoLocalize_UITexture(string string_2, string string_3)
		{
			Texture texture_ = uitexture_0.Texture_0;
			if (!texture_ || !(texture_.name == string_2))
			{
				uitexture_0.Texture_0 = FindTranslatedObject<Texture>(string_2);
			}
		}

		public static void RegisterEvents_TextMeshPro()
		{
		}

		public void RegisterEvents_UGUI()
		{
			action_0 = (Action)Delegate.Combine(action_0, new Action(FindTarget_uGUI_Text));
			action_0 = (Action)Delegate.Combine(action_0, new Action(FindTarget_uGUI_Image));
			action_0 = (Action)Delegate.Combine(action_0, new Action(FindTarget_uGUI_RawImage));
		}

		private void FindTarget_uGUI_Text()
		{
			FindAndCacheTarget(ref text_0, SetFinalTerms_uGUI_Text, DoLocalize_uGUI_Text, true, true, false);
		}

		private void FindTarget_uGUI_Image()
		{
			FindAndCacheTarget(ref image_0, SetFinalTerms_uGUI_Image, DoLocalize_uGUI_Image, false, false, false);
		}

		private void FindTarget_uGUI_RawImage()
		{
			FindAndCacheTarget(ref rawImage_0, SetFinalTerms_uGUI_RawImage, DoLocalize_uGUI_RawImage, false, false, false);
		}

		private bool SetFinalTerms_uGUI_Text(string string_2, string string_3, out string string_4, out string string_5)
		{
			string string_6 = ((!(text_0.font != null)) ? string.Empty : text_0.font.name);
			return SetFinalTerms(text_0.text, string_6, out string_4, out string_5);
		}

		public bool SetFinalTerms_uGUI_Image(string string_2, string string_3, out string string_4, out string string_5)
		{
			return SetFinalTerms(image_0.mainTexture.name, null, out string_4, out string_5);
		}

		public bool SetFinalTerms_uGUI_RawImage(string string_2, string string_3, out string string_4, out string string_5)
		{
			return SetFinalTerms(rawImage_0.texture.name, null, out string_4, out string_5);
		}

		public static T FindInParents<T>(Transform transform_0) where T : Component
		{
			if (!transform_0)
			{
				return (T)null;
			}
			T component = transform_0.GetComponent<T>();
			while (!(UnityEngine.Object)component && (bool)transform_0)
			{
				component = transform_0.GetComponent<T>();
				transform_0 = transform_0.parent;
			}
			return component;
		}

		public void DoLocalize_uGUI_Text(string string_2, string string_3)
		{
			if (!(text_0.text == string_2))
			{
				text_0.text = string_2;
				Font secondaryTranslatedObj = GetSecondaryTranslatedObj<Font>(ref string_2, ref string_3);
				if (secondaryTranslatedObj != null)
				{
					text_0.font = secondaryTranslatedObj;
				}
			}
		}

		public void DoLocalize_uGUI_Image(string string_2, string string_3)
		{
			Sprite sprite = image_0.sprite;
			if (!sprite || !(sprite.name == string_2))
			{
				image_0.sprite = FindTranslatedObject<Sprite>(string_2);
			}
		}

		public void DoLocalize_uGUI_RawImage(string string_2, string string_3)
		{
			Texture texture = rawImage_0.texture;
			if (!texture || !(texture.name == string_2))
			{
				rawImage_0.texture = FindTranslatedObject<Texture>(string_2);
			}
		}

		public void RegisterEvents_UnityStandard()
		{
			action_0 = (Action)Delegate.Combine(action_0, new Action(FindTarget_GUIText));
			action_0 = (Action)Delegate.Combine(action_0, new Action(FindTarget_TextMesh));
			action_0 = (Action)Delegate.Combine(action_0, new Action(FindTarget_AudioSource));
			action_0 = (Action)Delegate.Combine(action_0, new Action(FindTarget_GUITexture));
			action_0 = (Action)Delegate.Combine(action_0, new Action(FindTarget_Child));
		}

		private void FindTarget_GUIText()
		{
			FindAndCacheTarget(ref guitext_0, SetFinalTerms_GUIText, DoLocalize_GUIText, true, true, false);
		}

		private void FindTarget_TextMesh()
		{
			FindAndCacheTarget(ref textMesh_0, SetFinalTerms_TextMesh, DoLocalize_TextMesh, true, true, false);
		}

		private void FindTarget_AudioSource()
		{
			FindAndCacheTarget(ref audioSource_0, SetFinalTerms_AudioSource, DoLocalize_AudioSource, false, false, false);
		}

		private void FindTarget_GUITexture()
		{
			FindAndCacheTarget(ref guitexture_0, SetFinalTerms_GUITexture, DoLocalize_GUITexture, false, false, false);
		}

		private void FindTarget_Child()
		{
			FindAndCacheTarget(ref gameObject_0, SetFinalTerms_Child, DoLocalize_Child, false, false, false);
		}

		public bool SetFinalTerms_GUIText(string string_2, string string_3, out string string_4, out string string_5)
		{
			string string_6 = ((!(guitext_0.font != null)) ? string.Empty : guitext_0.font.name);
			return SetFinalTerms(guitext_0.text, string_6, out string_4, out string_5);
		}

		public bool SetFinalTerms_TextMesh(string string_2, string string_3, out string string_4, out string string_5)
		{
			string string_6 = ((!(textMesh_0.font != null)) ? string.Empty : textMesh_0.font.name);
			return SetFinalTerms(textMesh_0.text, string_6, out string_4, out string_5);
		}

		public bool SetFinalTerms_GUITexture(string string_2, string string_3, out string string_4, out string string_5)
		{
			if ((bool)guitexture_0 && (bool)guitexture_0.texture)
			{
				return SetFinalTerms(guitexture_0.texture.name, string.Empty, out string_4, out string_5);
			}
			SetFinalTerms(string.Empty, string.Empty, out string_4, out string_5);
			return false;
		}

		public bool SetFinalTerms_AudioSource(string string_2, string string_3, out string string_4, out string string_5)
		{
			if ((bool)audioSource_0 && (bool)audioSource_0.clip)
			{
				return SetFinalTerms(audioSource_0.clip.name, string.Empty, out string_4, out string_5);
			}
			SetFinalTerms(string.Empty, string.Empty, out string_4, out string_5);
			return false;
		}

		public bool SetFinalTerms_Child(string string_2, string string_3, out string string_4, out string string_5)
		{
			return SetFinalTerms(gameObject_0.name, string.Empty, out string_4, out string_5);
		}

		private void DoLocalize_GUIText(string string_2, string string_3)
		{
			if (!(guitext_0.text == string_2))
			{
				Font secondaryTranslatedObj = GetSecondaryTranslatedObj<Font>(ref string_2, ref string_3);
				if (secondaryTranslatedObj != null)
				{
					guitext_0.font = secondaryTranslatedObj;
				}
				guitext_0.text = string_2;
			}
		}

		private void DoLocalize_TextMesh(string string_2, string string_3)
		{
			if (!(textMesh_0.text == string_2))
			{
				Font secondaryTranslatedObj = GetSecondaryTranslatedObj<Font>(ref string_2, ref string_3);
				if (secondaryTranslatedObj != null)
				{
					textMesh_0.font = secondaryTranslatedObj;
					GetComponent<Renderer>().sharedMaterial = secondaryTranslatedObj.material;
				}
				textMesh_0.text = string_2;
			}
		}

		private void DoLocalize_AudioSource(string string_2, string string_3)
		{
			bool isPlaying = audioSource_0.isPlaying;
			AudioClip clip = audioSource_0.clip;
			AudioClip audioClip = FindTranslatedObject<AudioClip>(string_2);
			if (!(clip == audioClip))
			{
				audioSource_0.clip = audioClip;
				if (isPlaying && (bool)audioSource_0.clip)
				{
					audioSource_0.Play();
				}
			}
		}

		private void DoLocalize_GUITexture(string string_2, string string_3)
		{
			Texture texture = guitexture_0.texture;
			if (!texture || !(texture.name == string_2))
			{
				guitexture_0.texture = FindTranslatedObject<Texture>(string_2);
			}
		}

		private void DoLocalize_Child(string string_2, string string_3)
		{
			if (!gameObject_0 || !(gameObject_0.name == string_2))
			{
				GameObject gameObject = gameObject_0;
				GameObject gameObject2 = FindTranslatedObject<GameObject>(string_2);
				if ((bool)gameObject2)
				{
					gameObject_0 = (GameObject)UnityEngine.Object.Instantiate(gameObject2);
					Transform transform = gameObject_0.transform;
					Transform transform2 = ((!gameObject) ? gameObject2.transform : gameObject.transform);
					transform.parent = base.transform;
					transform.localScale = transform2.localScale;
					transform.localRotation = transform2.localRotation;
					transform.localPosition = transform2.localPosition;
				}
				if ((bool)gameObject)
				{
					UnityEngine.Object.Destroy(gameObject);
				}
			}
		}
	}
}
