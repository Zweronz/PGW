using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace CodeStage.AntiCheat.Detectors
{
	[DisallowMultipleComponent]
	public class ObscuredCheatingDetector : ActDetectorBase
	{
		private const string string_2 = "Obscured Cheating Detector";

		internal static bool bool_1;

		public float float_0 = 0.0001f;

		public float float_1 = 0.1f;

		public float float_2 = 0.1f;

		public float float_3 = 0.1f;

		[CompilerGenerated]
		private static ObscuredCheatingDetector obscuredCheatingDetector_0;

		public static ObscuredCheatingDetector ObscuredCheatingDetector_0
		{
			[CompilerGenerated]
			get
			{
				return obscuredCheatingDetector_0;
			}
			[CompilerGenerated]
			private set
			{
				obscuredCheatingDetector_0 = value;
			}
		}

		private static ObscuredCheatingDetector ObscuredCheatingDetector_1
		{
			get
			{
				if (ObscuredCheatingDetector_0 == null)
				{
					ObscuredCheatingDetector obscuredCheatingDetector = UnityEngine.Object.FindObjectOfType<ObscuredCheatingDetector>();
					if (obscuredCheatingDetector != null)
					{
						ObscuredCheatingDetector_0 = obscuredCheatingDetector;
					}
					else
					{
						if (ActDetectorBase.gameObject_0 == null)
						{
							ActDetectorBase.gameObject_0 = new GameObject("Anti-Cheat Toolkit Detectors");
						}
						ActDetectorBase.gameObject_0.AddComponent<ObscuredCheatingDetector>();
					}
				}
				return ObscuredCheatingDetector_0;
			}
		}

		private ObscuredCheatingDetector()
		{
		}

		public static void StartDetection(Action action_1)
		{
			ObscuredCheatingDetector_1.StartDetectionInternal(action_1);
		}

		public static void StopDetection()
		{
			if (ObscuredCheatingDetector_0 != null)
			{
				ObscuredCheatingDetector_0.StopDetectionInternal();
			}
		}

		public static void Dispose()
		{
			if (ObscuredCheatingDetector_0 != null)
			{
				ObscuredCheatingDetector_0.DisposeInternal();
			}
		}

		private void Awake()
		{
			if (Init(ObscuredCheatingDetector_0, "Obscured Cheating Detector"))
			{
				ObscuredCheatingDetector_0 = this;
			}
		}

		private void StartDetectionInternal(Action action_1)
		{
			if (bool_1)
			{
				Debug.LogWarning("[ACTk] Obscured Cheating Detector already running!");
				return;
			}
			if (!base.enabled)
			{
				Debug.LogWarning("[ACTk] Obscured Cheating Detector disabled but StartDetection still called from somewhere!");
				return;
			}
			action_0 = action_1;
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
			if (ObscuredCheatingDetector_0 == this)
			{
				ObscuredCheatingDetector_0 = null;
			}
		}

		internal void OnCheatingDetected()
		{
			if (action_0 != null)
			{
				action_0();
				if (autoDispose)
				{
					Dispose();
				}
				else
				{
					StopDetection();
				}
			}
		}
	}
}
