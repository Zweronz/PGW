using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.controllers;
using engine.unity;

[RequireComponent(typeof(UICursor))]
public class CursorPGW : MonoBehaviour
{
	public enum State
	{
		None = 0,
		Arrow = 1,
		Hover = 2,
		Profile = 3,
		Menu = 4,
		Pencil = 5,
		Brush = 6,
		Pouring = 7,
		Dropper = 8,
		Eraser = 9
	}

	private static CursorPGW cursorPGW_0;

	private LayerMask layerMask_0;

	private LayerMask layerMask_1;

	private LayerMask layerMask_2;

	private LayerMask layerMask_3;

	private UISprite uisprite_0;

	private State state_0;

	private readonly Dictionary<State, string> dictionary_0 = new Dictionary<State, string>
	{
		{
			State.Arrow,
			"arrow_cursor"
		},
		{
			State.Hover,
			"hand_cursor"
		},
		{
			State.Pencil,
			"pencil_cursor"
		},
		{
			State.Brush,
			"brush_cursor"
		},
		{
			State.Pouring,
			"pouring_cursor"
		},
		{
			State.Dropper,
			"dropper_cursor"
		},
		{
			State.Eraser,
			"eraser_cursor"
		},
		{
			State.Menu,
			"menu_cursor"
		},
		{
			State.Profile,
			"profile_cursor"
		}
	};

	[CompilerGenerated]
	private Func<GameObject> func_0;

	public static CursorPGW CursorPGW_0
	{
		get
		{
			return cursorPGW_0;
		}
	}

	public Func<GameObject> Func_0
	{
		[CompilerGenerated]
		get
		{
			return func_0;
		}
		[CompilerGenerated]
		set
		{
			func_0 = value;
		}
	}

	private void Awake()
	{
		cursorPGW_0 = this;
		layerMask_0 = LayerMask.NameToLayer("NGUIRoot");
		layerMask_1 = LayerMask.NameToLayer("NGUITutorial");
		layerMask_2 = LayerMask.NameToLayer("NickLabel");
		layerMask_3 = LayerMask.NameToLayer("CursorPGWHowered");
	}

	private void OnDestroy()
	{
		cursorPGW_0 = null;
	}

	private void Start()
	{
		uisprite_0 = GetComponentInChildren<UISprite>();
		SetState(State.None);
		AppStateController.AppStateController_0.Subscribe(OnHide, AppStateController.States.APP_INIT);
		AppStateController.AppStateController_0.Subscribe(OnShow, AppStateController.States.MAIN_MENU);
	}

	private void Update()
	{
		if (CheckVisibleCursor())
		{
			SetVisibleStateCursor();
		}
	}

	public void SetCursorEnable(bool bool_0)
	{
		SetState(bool_0 ? State.Arrow : State.None);
	}

	private bool CheckVisibleCursor()
	{
		bool result = !Screen.lockCursor;
		uisprite_0.enabled = result;
		return result;
	}

	private void SetVisibleStateCursor()
	{
		if (state_0 == State.None)
		{
			return;
		}
		GameObject gameObject = ((Func_0 == null) ? UICamera.gameObject_6 : Func_0());
		State state = State.Arrow;
		if (gameObject != null && (gameObject.layer == (int)layerMask_0 || gameObject.layer == (int)layerMask_1 || gameObject.layer == (int)layerMask_2 || gameObject.layer == (int)layerMask_3) && gameObject.activeSelf)
		{
			CheckHowerChecker(gameObject);
			bool flag = false;
			CursorChanger cursorChanger = CheckHowerForWidgetType<CursorChanger>(gameObject);
			if (cursorChanger != null)
			{
				flag = true;
			}
			else if (CheckHowerForWidgetType<UIButton>(gameObject) != null)
			{
				flag = true;
			}
			else if (CheckHowerForWidgetType<UITab>(gameObject) != null)
			{
				flag = true;
			}
			else if (CheckHowerForWidgetType<SettingsControlItemSlot>(gameObject) != null)
			{
				flag = true;
			}
			if (!flag && CheckHowerForWidgetType<UIEventListener>(gameObject) != null && gameObject.transform.parent != null && string.Equals(gameObject.transform.parent.name, "Drop-down List"))
			{
				flag = true;
			}
			if (!flag && CheckEditorCanvas(gameObject))
			{
				flag = false;
				state = GetNeedStateForEditorCanvas();
			}
			if (flag)
			{
				state = State.Hover;
				if (cursorChanger != null)
				{
					state = cursorChanger.state;
				}
			}
		}
		SetState(state);
	}

	private void OnShow()
	{
		SetState(State.Arrow);
	}

	private void OnHide()
	{
		SetState(State.None);
	}

	private void SetState(State state_1)
	{
		if (state_0 != state_1)
		{
			state_0 = state_1;
			string value;
			dictionary_0.TryGetValue(state_1, out value);
			SetCursor(value);
		}
	}

	private void SetCursor(string string_0)
	{
		Cursor.visible = string.IsNullOrEmpty(string_0);
		UICursor.Set(uisprite_0.UIAtlas_0, string_0);
	}

	private T CheckHowerForWidgetType<T>(GameObject gameObject_0) where T : MonoBehaviour
	{
		T component = gameObject_0.GetComponent<T>();
		if ((UnityEngine.Object)component != (UnityEngine.Object)null && component.enabled)
		{
			BoxCollider component2 = component.GetComponent<BoxCollider>();
			if (component2 != null)
			{
				return component;
			}
		}
		return (T)null;
	}

	private void CheckHowerChecker(GameObject gameObject_0)
	{
		HoverChecker component = gameObject_0.GetComponent<HoverChecker>();
		if (component != null)
		{
			BoxCollider component2 = component.GetComponent<BoxCollider>();
			if (component2 != null)
			{
				component.Show();
			}
			return;
		}
		ButtonHoverChecker component3 = gameObject_0.GetComponent<ButtonHoverChecker>();
		if (component3 != null)
		{
			BoxCollider component4 = component3.GetComponent<BoxCollider>();
			if (component4 != null)
			{
				component3.Show();
			}
		}
	}

	private bool CheckEditorCanvas(GameObject gameObject_0)
	{
		if (SkinEditController.SkinEditController_0.IsEditing() && gameObject_0.tag.Equals("EditorCanvas"))
		{
			return true;
		}
		return false;
	}

	private State GetNeedStateForEditorCanvas()
	{
		switch (SkinEditController.SkinEditController_0.toolType_0)
		{
		default:
			return State.Arrow;
		case SkinEditorToolItem.ToolType.PENCIL:
			return State.Pencil;
		case SkinEditorToolItem.ToolType.BRUSH:
			return State.Brush;
		case SkinEditorToolItem.ToolType.POURING:
			return State.Pouring;
		case SkinEditorToolItem.ToolType.DROPPER:
			return State.Dropper;
		case SkinEditorToolItem.ToolType.ERASER:
			return State.Eraser;
		}
	}

	public Vector2 GetCursorSize()
	{
		return new Vector2(uisprite_0.Int32_0, uisprite_0.Int32_1);
	}
}
