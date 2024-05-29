using System.Collections.Generic;
using UnityEngine;

public sealed class FPSStateController : MonoBehaviour
{
	public enum STATES
	{
		DEFAULT = 0,
		SPRINT = 1,
		CRAWLING = 2,
		SEAT = 3,
		STAIRS = 4
	}

	internal FirstPersonPlayerController firstPersonPlayerController_0;

	internal Player_move_c player_move_c_0;

	private Dictionary<STATES, FPSBaseState> dictionary_0;

	internal bool bool_0;

	private STATES states_0;

	private FPSBaseState fpsbaseState_0;

	private FPSNetworkStateSynchronizer fpsnetworkStateSynchronizer_0;

	public STATES STATES_0
	{
		get
		{
			return states_0;
		}
	}

	public int Int32_0
	{
		get
		{
			return (fpsnetworkStateSynchronizer_0 != null) ? fpsnetworkStateSynchronizer_0.Int32_0 : 0;
		}
		set
		{
			if (fpsnetworkStateSynchronizer_0 != null)
			{
				fpsnetworkStateSynchronizer_0.Int32_0 = value;
			}
		}
	}

	private void Start()
	{
		InitNetwork();
		Init();
		SetState(STATES.DEFAULT);
		fpsnetworkStateSynchronizer_0.SetState(0, true);
	}

	private void Update()
	{
		if (fpsbaseState_0 != null && fpsbaseState_0.Boolean_0)
		{
			fpsbaseState_0.Update();
		}
	}

	public void SetState(STATES states_1)
	{
		FPSBaseState value;
		if ((fpsbaseState_0 == null || states_1 != states_0) && dictionary_0.TryGetValue(states_1, out value))
		{
			if (fpsbaseState_0 != null)
			{
				fpsbaseState_0.Release();
				fpsnetworkStateSynchronizer_0.SetState((int)states_0, false);
			}
			states_0 = states_1;
			fpsbaseState_0 = value;
			fpsbaseState_0.Init();
			fpsnetworkStateSynchronizer_0.SetState((int)states_1, true);
		}
	}

	internal void SetNetworkState(int int_0, bool bool_1)
	{
		FPSBaseState value;
		if (!bool_0 && bool_1 && (fpsbaseState_0 == null || int_0 != (int)states_0) && dictionary_0.TryGetValue((STATES)int_0, out value))
		{
			if (fpsbaseState_0 != null)
			{
				fpsbaseState_0.Release();
			}
			states_0 = (STATES)int_0;
			fpsbaseState_0 = value;
			fpsbaseState_0.Init();
		}
	}

	private void Init()
	{
		dictionary_0 = new Dictionary<STATES, FPSBaseState>
		{
			{
				STATES.DEFAULT,
				new DefaultState(this)
			},
			{
				STATES.SPRINT,
				new SprintState(this)
			},
			{
				STATES.CRAWLING,
				new CrawlingState(this)
			},
			{
				STATES.SEAT,
				new SeatState(this)
			},
			{
				STATES.STAIRS,
				new StairsState(this)
			}
		};
		firstPersonPlayerController_0 = GetComponent<FirstPersonPlayerController>();
		player_move_c_0 = GetComponentInChildren<Player_move_c>();
	}

	private void InitNetwork()
	{
		PhotonView photonView = PhotonView.Get(this);
		bool_0 = true;
		if (!photonView.Boolean_1 && Defs.bool_2)
		{
			bool_0 = false;
		}
		fpsnetworkStateSynchronizer_0 = new FPSNetworkStateSynchronizer(this);
	}
}
