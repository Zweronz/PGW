using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace CodeStage.AntiCheat.Detectors
{
	[DisallowMultipleComponent]
	public class SpeedHackDetector : ActDetectorBase
	{
		private const string string_2 = "Speed Hack Detector";

		private const long long_0 = 10000000L;

		private const int int_0 = 5000000;

		internal static bool bool_1;

		[Tooltip("Time (in seconds) between detector checks.")]
		public float float_0 = 1f;

		[Tooltip("Maximum false positives count allowed before registering speed hack.")]
		public byte byte_0 = 3;

		[Tooltip("Amount of sequential successful checks before clearing internal false positives counter.\nSet 0 to disable Cool Down feature.")]
		public int int_1 = 30;

		private byte byte_1;

		private int int_2;

		private long long_1;

		private long long_2;

		private long long_3;

		private long long_4;

		[CompilerGenerated]
		private static SpeedHackDetector speedHackDetector_0;

		public static SpeedHackDetector SpeedHackDetector_0
		{
			[CompilerGenerated]
			get
			{
				return speedHackDetector_0;
			}
			[CompilerGenerated]
			private set
			{
				speedHackDetector_0 = value;
			}
		}

		private static SpeedHackDetector SpeedHackDetector_1
		{
			get
			{
				if (SpeedHackDetector_0 == null)
				{
					SpeedHackDetector speedHackDetector = UnityEngine.Object.FindObjectOfType<SpeedHackDetector>();
					if (speedHackDetector != null)
					{
						SpeedHackDetector_0 = speedHackDetector;
					}
					else
					{
						if (ActDetectorBase.gameObject_0 == null)
						{
							ActDetectorBase.gameObject_0 = new GameObject("Anti-Cheat Toolkit Detectors");
						}
						ActDetectorBase.gameObject_0.AddComponent<SpeedHackDetector>();
					}
				}
				return SpeedHackDetector_0;
			}
		}

		private SpeedHackDetector()
		{
		}

		public static void StartDetection(Action action_1)
		{
			StartDetection(action_1, SpeedHackDetector_1.float_0);
		}

		public static void StartDetection(Action action_1, float float_1)
		{
			StartDetection(action_1, float_1, SpeedHackDetector_1.byte_0);
		}

		public static void StartDetection(Action action_1, float float_1, byte byte_2)
		{
			StartDetection(action_1, float_1, byte_2, SpeedHackDetector_1.int_1);
		}

		public static void StartDetection(Action action_1, float float_1, byte byte_2, int int_3)
		{
			SpeedHackDetector_1.StartDetectionInternal(action_1, float_1, byte_2, int_3);
		}

		public static void StopDetection()
		{
			if (SpeedHackDetector_0 != null)
			{
				SpeedHackDetector_0.StopDetectionInternal();
			}
		}

		public static void Dispose()
		{
			if (SpeedHackDetector_0 != null)
			{
				SpeedHackDetector_0.DisposeInternal();
			}
		}

		private void Awake()
		{
			if (Init(SpeedHackDetector_0, "Speed Hack Detector"))
			{
				SpeedHackDetector_0 = this;
			}
		}

		private void StartDetectionInternal(Action action_1, float float_1, byte byte_2, int int_3)
		{
			if (bool_1)
			{
				Debug.LogWarning("[ACTk] Speed Hack Detector already running!");
				return;
			}
			if (!base.enabled)
			{
				Debug.LogWarning("[ACTk] Speed Hack Detector disabled but StartDetection still called from somewhere!");
				return;
			}
			action_0 = action_1;
			float_0 = float_1;
			byte_0 = byte_2;
			int_1 = int_3;
			ResetStartTicks();
			byte_1 = 0;
			int_2 = 0;
			bool_1 = true;
		}

		protected override void StopDetectionInternal()
		{
			if (bool_1)
			{
				action_0 = null;
				bool_1 = false;
			}
		}

		protected override void PauseDetector()
		{
			bool_1 = false;
		}

		protected override void ResumeDetector()
		{
			bool_1 = true;
		}

		protected override void DisposeInternal()
		{
			base.DisposeInternal();
			if (SpeedHackDetector_0 == this)
			{
				SpeedHackDetector_0 = null;
			}
		}

		private void ResetStartTicks()
		{
			long_1 = DateTime.UtcNow.Ticks;
			long_2 = Environment.TickCount * 10000L;
			long_3 = long_1;
			long_4 = long_1;
		}

		private void OnApplicationPause(bool bool_2)
		{
			if (!bool_2)
			{
				ResetStartTicks();
			}
		}

		private void Update()
		{
			if (!bool_1)
			{
				return;
			}
			long num = 0L;
			num = DateTime.UtcNow.Ticks;
			long num2 = num - long_3;
			if (num2 >= 0L && num2 <= 10000000L)
			{
				long_3 = num;
				long num3 = (long)(float_0 * 10000000f);
				if (num - long_4 < num3)
				{
					return;
				}
				long num4 = 0L;
				num4 = Environment.TickCount * 10000L;
				if (Mathf.Abs(num4 - long_2 - (num - long_1)) > 5000000f)
				{
					byte_1++;
					if (byte_1 > byte_0)
					{
						if (Debug.isDebugBuild)
						{
							Debug.LogWarning("[ACTk] SpeedHackDetector: final detection!");
						}
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
					else
					{
						if (Debug.isDebugBuild)
						{
							Debug.LogWarning("[ACTk] SpeedHackDetector: detection! Allowed false positives left: " + (byte_0 - byte_1));
						}
						int_2 = 0;
						ResetStartTicks();
					}
				}
				else if (byte_1 > 0 && int_1 > 0)
				{
					if (Debug.isDebugBuild)
					{
						Debug.LogWarning("[ACTk] SpeedHackDetector: success shot! Shots till Cooldown: " + (int_1 - int_2));
					}
					int_2++;
					if (int_2 >= int_1)
					{
						if (Debug.isDebugBuild)
						{
							Debug.LogWarning("[ACTk] SpeedHackDetector: Cooldown!");
						}
						byte_1 = 0;
					}
				}
				long_4 = num;
			}
			else
			{
				if (Debug.isDebugBuild)
				{
					Debug.LogWarning("[ACTk] SpeedHackDetector: System DateTime change or > 1 second game freeze detected!");
				}
				ResetStartTicks();
			}
		}
	}
}
