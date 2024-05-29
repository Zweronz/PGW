using System;
using UnityEngine;

namespace CodeStage.AntiCheat.Detectors
{
	public abstract class ActDetectorBase : MonoBehaviour
	{
		protected const string string_0 = "Anti-Cheat Toolkit Detectors";

		protected const string string_1 = "GameObject/Create Other/Code Stage/Anti-Cheat Toolkit/";

		[Tooltip("Automatically dispose Detector after firing callback.")]
		public bool autoDispose = true;

		[Tooltip("Detector will survive new level (scene) load if checked.")]
		public bool keepAlive = true;

		protected static GameObject gameObject_0;

		protected Action action_0;

		private bool bool_0;

		private void Start()
		{
			bool_0 = true;
		}

		protected virtual bool Init(ActDetectorBase actDetectorBase_0, string string_2)
		{
			if (actDetectorBase_0 != null && actDetectorBase_0 != this && actDetectorBase_0.keepAlive)
			{
				UnityEngine.Object.Destroy(this);
				return false;
			}
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			return true;
		}

		private void OnDisable()
		{
			if (bool_0)
			{
				PauseDetector();
			}
		}

		private void OnEnable()
		{
			if (bool_0 && action_0 != null)
			{
				ResumeDetector();
			}
		}

		private void OnApplicationQuit()
		{
			DisposeInternal();
		}

		private void OnLevelWasLoaded(int int_0)
		{
			if (bool_0 && !keepAlive)
			{
				DisposeInternal();
			}
		}

		protected abstract void StopDetectionInternal();

		protected abstract void PauseDetector();

		protected abstract void ResumeDetector();

		protected virtual void DisposeInternal()
		{
			StopDetectionInternal();
			UnityEngine.Object.Destroy(this);
		}

		protected virtual void OnDestroy()
		{
			if (base.transform.childCount == 0 && GetComponentsInChildren<Component>().Length <= 2)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			else if (base.name == "Anti-Cheat Toolkit Detectors" && GetComponentsInChildren<ActDetectorBase>().Length <= 1)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}
}
