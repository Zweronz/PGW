using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.events;

public class FirstPersonPlayerController : MonoBehaviour
{
	public enum State
	{
		None = 0,
		Default = 1,
		Hook = 2,
		Turret_building = 3
	}

	public enum EventType
	{
		HOOK_END = 0
	}

	public GameObject playerGameObject;

	public Transform cameraPivot;

	public float angleFPSResverse = 0.3f;

	public Transform stairsTransform;

	public AudioClip jumpClip;

	private readonly BaseEvent<int> baseEvent_0 = new BaseEvent<int>();

	private readonly List<FPBaseProcess> list_0 = new List<FPBaseProcess>
	{
		new FPCheckBlockProcess(),
		new FPDefaultInputProcess(),
		new FPDefaultActionProcess(),
		new FPDefaultRotationProcess(),
		new FPDefaultJumpProcess(),
		new FPDefaultMoveProcess(),
		new FPDefaultAnimProcess()
	};

	private readonly List<FPBaseProcess> list_1 = new List<FPBaseProcess>
	{
		new FPCheckBlockProcess(),
		new FPDefaultInputProcess(),
		new FPDefaultActionProcess(),
		new FPHookMoveProcess(),
		new FPDefaultAnimProcess()
	};

	private readonly List<FPBaseProcess> list_2 = new List<FPBaseProcess>
	{
		new FPDefaultAnimProcess()
	};

	private List<FPBaseProcess> list_3;

	private List<FPBaseProcess> list_4 = new List<FPBaseProcess>();

	private FPProcessSharedData fpprocessSharedData_0;

	private float float_0 = 1f;

	private float float_1 = 1f;

	[CompilerGenerated]
	private static FirstPersonPlayerController firstPersonPlayerController_0;

	[CompilerGenerated]
	private State state_0;

	[CompilerGenerated]
	private bool bool_0;

	[CompilerGenerated]
	private Transform transform_0;

	[CompilerGenerated]
	private Player_move_c player_move_c_0;

	[CompilerGenerated]
	private CharacterController characterController_0;

	[CompilerGenerated]
	private FPSStateController fpsstateController_0;

	[CompilerGenerated]
	private SkinName skinName_0;

	[CompilerGenerated]
	private bool bool_1;

	[CompilerGenerated]
	private Vector3 vector3_0;

	public static FirstPersonPlayerController FirstPersonPlayerController_0
	{
		[CompilerGenerated]
		get
		{
			return firstPersonPlayerController_0;
		}
		[CompilerGenerated]
		private set
		{
			firstPersonPlayerController_0 = value;
		}
	}

	public State State_0
	{
		[CompilerGenerated]
		get
		{
			return state_0;
		}
		[CompilerGenerated]
		private set
		{
			state_0 = value;
		}
	}

	public bool Boolean_0
	{
		[CompilerGenerated]
		get
		{
			return bool_0;
		}
		[CompilerGenerated]
		private set
		{
			bool_0 = value;
		}
	}

	public Transform Transform_0
	{
		[CompilerGenerated]
		get
		{
			return transform_0;
		}
		[CompilerGenerated]
		private set
		{
			transform_0 = value;
		}
	}

	public Player_move_c Player_move_c_0
	{
		[CompilerGenerated]
		get
		{
			return player_move_c_0;
		}
		[CompilerGenerated]
		private set
		{
			player_move_c_0 = value;
		}
	}

	public CharacterController CharacterController_0
	{
		[CompilerGenerated]
		get
		{
			return characterController_0;
		}
		[CompilerGenerated]
		private set
		{
			characterController_0 = value;
		}
	}

	public FPSStateController FPSStateController_0
	{
		[CompilerGenerated]
		get
		{
			return fpsstateController_0;
		}
		[CompilerGenerated]
		private set
		{
			fpsstateController_0 = value;
		}
	}

	public SkinName SkinName_0
	{
		[CompilerGenerated]
		get
		{
			return skinName_0;
		}
		[CompilerGenerated]
		private set
		{
			skinName_0 = value;
		}
	}

	public bool Boolean_1
	{
		[CompilerGenerated]
		get
		{
			return bool_1;
		}
		[CompilerGenerated]
		set
		{
			bool_1 = value;
		}
	}

	public Vector3 Vector3_0
	{
		[CompilerGenerated]
		get
		{
			return vector3_0;
		}
		[CompilerGenerated]
		set
		{
			vector3_0 = value;
		}
	}

	public float Single_0
	{
		get
		{
			return float_1;
		}
		set
		{
			float_1 = ((value != 0f) ? value : 1f);
		}
	}

	public float Single_1
	{
		get
		{
			return float_0;
		}
		set
		{
			float_0 = ((value != 0f) ? value : 1f);
		}
	}

	private void Awake()
	{
		InitCpmponents();
	}

	private void Start()
	{
		Init();
	}

	private void OnDestroy()
	{
		Release();
	}

	private void Update()
	{
		if (Boolean_0 && list_3 != null)
		{
			list_4.Clear();
			list_4.AddRange(list_3);
			for (int i = 0; i < list_4.Count; i++)
			{
				FPBaseProcess fPBaseProcess = list_4[i];
				fPBaseProcess.Update(fpprocessSharedData_0);
			}
		}
	}

	public void Subscribe(EventType eventType_0, Action<int> action_0)
	{
		if (!baseEvent_0.Contains(action_0, eventType_0))
		{
			baseEvent_0.Subscribe(action_0, eventType_0);
		}
	}

	public void Unsubscribe(EventType eventType_0, Action<int> action_0)
	{
		baseEvent_0.Unsubscribe(action_0, eventType_0);
	}

	private void Dispatch<EventType>(EventType gparam_0, int int_0 = 0)
	{
		baseEvent_0.Dispatch(int_0, gparam_0);
	}

	public void SetState(State state_1)
	{
		if (State_0 != state_1)
		{
			PreProcessSetState(state_1);
			switch (state_1)
			{
			default:
				list_3 = list_0;
				break;
			case State.Turret_building:
				list_3 = list_2;
				break;
			case State.Hook:
				list_3 = list_1;
				break;
			}
			State_0 = state_1;
		}
	}

	private void InitCpmponents()
	{
		fpprocessSharedData_0 = new FPProcessSharedData(this);
		Transform_0 = GetComponent<Transform>();
		CharacterController_0 = GetComponent<CharacterController>();
		Player_move_c_0 = playerGameObject.GetComponent<Player_move_c>();
		FPSStateController_0 = GetComponent<FPSStateController>();
		SkinName_0 = GetComponent<SkinName>();
	}

	private void Init()
	{
		Boolean_0 = PhotonView.Get(this).Boolean_1;
		SetState(State.Default);
		if (Boolean_0)
		{
			FirstPersonPlayerController_0 = this;
		}
		Player_move_c_0.PlayerStateController_0.Subscribe(OnPlayerRespawn, PlayerEvents.EndRespawn);
		HandleInvertCamUpdated();
		Player_move_c_0.PlayerStateController_0.Subscribe(HandleInvertCamUpdated, PlayerEvents.ChangeCameraSensitive);
	}

	private void Release()
	{
		if (Player_move_c_0 != null)
		{
			Player_move_c_0.PlayerStateController_0.Unsubscribe(OnPlayerRespawn, PlayerEvents.EndRespawn);
			Player_move_c_0.PlayerStateController_0.Unsubscribe(HandleInvertCamUpdated, PlayerEvents.ChangeCameraSensitive);
		}
	}

	private void HandleInvertCamUpdated()
	{
		Boolean_1 = Storager.GetInt(Defs.string_2) == 1;
	}

	private void OnPlayerRespawn()
	{
		FPSStateController_0.SetState(FPSStateController.STATES.DEFAULT);
	}

	private void PreProcessSetState(State state_1)
	{
		State state = State_0;
		if (state == State.Hook)
		{
			Dispatch(EventType.HOOK_END);
			if (Boolean_0)
			{
				Player_move_c_0.PhotonView_0.RPC("EndHookRPC", PhotonTargets.Others);
			}
		}
	}

	public void SetStairsMovement(Vector3 vector3_1)
	{
		fpprocessSharedData_0.vector3_2 = vector3_1;
	}
}
