using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace CodeStage.AntiCheat.Detectors
{
	[DisallowMultipleComponent]
	public class WallHackDetector : ActDetectorBase
	{
		private const string string_2 = "WallHack Detector";

		private const string string_3 = "[WH Detector Service]";

		private readonly Vector3 vector3_0 = new Vector3(0f, 0f, 1f);

		internal static bool bool_1;

		[Tooltip("World position of the container for service objects within 3x3x3 cube (drawn as red wireframe cube in scene).")]
		public Vector3 vector3_1;

		private int int_0 = -1;

		private GameObject gameObject_1;

		private Rigidbody rigidbody_0;

		private CharacterController characterController_0;

		private float float_0;

		[CompilerGenerated]
		private static WallHackDetector wallHackDetector_0;

		public static WallHackDetector WallHackDetector_0
		{
			[CompilerGenerated]
			get
			{
				return wallHackDetector_0;
			}
			[CompilerGenerated]
			private set
			{
				wallHackDetector_0 = value;
			}
		}

		private static WallHackDetector WallHackDetector_1
		{
			get
			{
				if (WallHackDetector_0 == null)
				{
					WallHackDetector wallHackDetector = UnityEngine.Object.FindObjectOfType<WallHackDetector>();
					if (wallHackDetector != null)
					{
						WallHackDetector_0 = wallHackDetector;
					}
					else
					{
						if (ActDetectorBase.gameObject_0 == null)
						{
							ActDetectorBase.gameObject_0 = new GameObject("Anti-Cheat Toolkit Detectors");
						}
						ActDetectorBase.gameObject_0.AddComponent<WallHackDetector>();
					}
				}
				return WallHackDetector_0;
			}
		}

		private WallHackDetector()
		{
		}

		public static void StartDetection(Action action_1)
		{
			StartDetection(action_1, WallHackDetector_1.vector3_1);
		}

		public static void StartDetection(Action action_1, Vector3 vector3_2)
		{
			WallHackDetector_1.StartDetectionInternal(action_1, vector3_2);
		}

		public static void StopDetection()
		{
			if (WallHackDetector_0 != null)
			{
				WallHackDetector_0.StopDetectionInternal();
			}
		}

		public static void Dispose()
		{
			if (WallHackDetector_0 != null)
			{
				WallHackDetector_0.DisposeInternal();
			}
		}

		private void Awake()
		{
			if (Init(WallHackDetector_0, "WallHack Detector"))
			{
				WallHackDetector_0 = this;
			}
		}

		private void StartDetectionInternal(Action action_1, Vector3 vector3_2)
		{
			if (bool_1)
			{
				Debug.LogWarning("[ACTk] WallHack Detector already running!");
				return;
			}
			if (!base.enabled)
			{
				Debug.LogWarning("[ACTk] WallHack Detector disabled but StartDetection still called from somewhere!");
				return;
			}
			action_0 = action_1;
			vector3_1 = vector3_2;
			InitDetector();
			bool_1 = true;
		}

		protected override void StopDetectionInternal()
		{
			if (bool_1)
			{
				UninitDetector();
				action_0 = null;
				bool_1 = false;
			}
		}

		protected override void PauseDetector()
		{
			if (bool_1)
			{
				bool_1 = false;
				StopRigidModule();
				StopControllerModule();
			}
		}

		protected override void ResumeDetector()
		{
			bool_1 = true;
			StartRigidModule();
			StartControllerModule();
		}

		protected override void DisposeInternal()
		{
			base.DisposeInternal();
			if (WallHackDetector_0 == this)
			{
				WallHackDetector_0 = null;
			}
		}

		private void InitDetector()
		{
			InitCommon();
			InitRigidModule();
			InitControllerModule();
			StartRigidModule();
			StartControllerModule();
		}

		private void UninitDetector()
		{
			bool_1 = false;
			StopRigidModule();
			StopControllerModule();
			UnityEngine.Object.Destroy(gameObject_1);
		}

		private void InitCommon()
		{
			if (int_0 == -1)
			{
				int_0 = LayerMask.NameToLayer("Ignore Raycast");
			}
			gameObject_1 = new GameObject("[WH Detector Service]");
			gameObject_1.layer = int_0;
			gameObject_1.transform.position = vector3_1;
			UnityEngine.Object.DontDestroyOnLoad(gameObject_1);
			GameObject gameObject = new GameObject("Wall");
			gameObject.AddComponent<BoxCollider>();
			gameObject.layer = int_0;
			gameObject.transform.parent = gameObject_1.transform;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = new Vector3(3f, 3f, 0.5f);
		}

		private void InitRigidModule()
		{
			GameObject gameObject = new GameObject("RigidPlayer");
			gameObject.AddComponent<CapsuleCollider>().height = 2f;
			gameObject.layer = int_0;
			gameObject.transform.parent = gameObject_1.transform;
			gameObject.transform.localPosition = new Vector3(0.75f, 0f, -1f);
			rigidbody_0 = gameObject.AddComponent<Rigidbody>();
			rigidbody_0.useGravity = false;
		}

		private void InitControllerModule()
		{
			GameObject gameObject = new GameObject("ControlledPlayer");
			gameObject.AddComponent<CapsuleCollider>().height = 2f;
			gameObject.layer = int_0;
			gameObject.transform.parent = gameObject_1.transform;
			gameObject.transform.localPosition = new Vector3(-0.75f, 0f, -1f);
			characterController_0 = gameObject.AddComponent<CharacterController>();
		}

		private void StartRigidModule()
		{
			rigidbody_0.rotation = Quaternion.identity;
			rigidbody_0.angularVelocity = Vector3.zero;
			rigidbody_0.transform.localPosition = new Vector3(0.75f, 0f, -1f);
			rigidbody_0.velocity = vector3_0;
			Invoke("StartRigidModule", 4f);
		}

		private void StopRigidModule()
		{
			rigidbody_0.velocity = Vector3.zero;
			CancelInvoke("StartRigidModule");
		}

		private void StartControllerModule()
		{
			characterController_0.transform.localPosition = new Vector3(-0.75f, 0f, -1f);
			float_0 = 0.01f;
			Invoke("StartControllerModule", 4f);
		}

		private void StopControllerModule()
		{
			float_0 = 0f;
			CancelInvoke("StartControllerModule");
		}

		private void FixedUpdate()
		{
			if (bool_1 && rigidbody_0.transform.localPosition.z > 1f)
			{
				StopRigidModule();
				Detect();
			}
		}

		private void Update()
		{
			if (bool_1 && float_0 > 0f)
			{
				characterController_0.Move(new Vector3(UnityEngine.Random.Range(-0.002f, 0.002f), 0f, float_0));
				if (characterController_0.transform.localPosition.z > 1f)
				{
					StopControllerModule();
					Detect();
				}
			}
		}

		private void Detect()
		{
			if (action_0 != null)
			{
				action_0();
			}
			if (autoDispose)
			{
				Dispose();
			}
			else
			{
				StopDetection();
			}
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireCube(vector3_1, new Vector3(3f, 3f, 3f));
		}
	}
}
