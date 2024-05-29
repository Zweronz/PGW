using System.Collections;
using UnityEngine;

namespace DG.Tweening.Core
{
	[AddComponentMenu("")]
	public class DOTweenComponent : MonoBehaviour, IDOTweenInit
	{
		public int inspectorUpdater;

		private float _unscaledTime;

		private float _unscaledDeltaTime;

		private bool _duplicateToDestroy;

		private void Awake()
		{
			inspectorUpdater = 0;
			_unscaledTime = Time.realtimeSinceStartup;
		}

		private void Start()
		{
			if (DOTween.instance != this)
			{
				_duplicateToDestroy = true;
				Object.Destroy(base.gameObject);
			}
		}

		private void Update()
		{
			_unscaledDeltaTime = Time.realtimeSinceStartup - _unscaledTime;
			if (TweenManager.hasActiveDefaultTweens)
			{
				TweenManager.Update(UpdateType.Normal, Time.deltaTime * DOTween.timeScale, _unscaledDeltaTime * DOTween.timeScale);
			}
			_unscaledTime = Time.realtimeSinceStartup;
			if (!DOTween.isUnityEditor)
			{
				return;
			}
			inspectorUpdater++;
			if (DOTween.showUnityEditorReport && TweenManager.hasActiveTweens)
			{
				if (TweenManager.totActiveTweeners > DOTween.maxActiveTweenersReached)
				{
					DOTween.maxActiveTweenersReached = TweenManager.totActiveTweeners;
				}
				if (TweenManager.totActiveSequences > DOTween.maxActiveSequencesReached)
				{
					DOTween.maxActiveSequencesReached = TweenManager.totActiveSequences;
				}
			}
		}

		private void LateUpdate()
		{
			if (TweenManager.hasActiveLateTweens)
			{
				TweenManager.Update(UpdateType.Late, Time.deltaTime * DOTween.timeScale, _unscaledDeltaTime * DOTween.timeScale);
			}
		}

		private void FixedUpdate()
		{
			if (TweenManager.hasActiveFixedTweens && Time.timeScale > 0f)
			{
				TweenManager.Update(UpdateType.Fixed, Time.deltaTime * DOTween.timeScale, Time.deltaTime / Time.timeScale * DOTween.timeScale);
			}
		}

		private void OnLevelWasLoaded()
		{
			if (DOTween.useSafeMode)
			{
				DOTween.Validate();
			}
		}

		private void OnDrawGizmos()
		{
			int count = DOTween.GizmosDelegates.Count;
			if (count != 0)
			{
				for (int i = 0; i < count; i++)
				{
					DOTween.GizmosDelegates[i]();
				}
			}
		}

		private void OnDestroy()
		{
			if (!_duplicateToDestroy)
			{
				if (DOTween.showUnityEditorReport)
				{
					string message = "REPORT > Max overall simultaneous active Tweeners/Sequences: " + DOTween.maxActiveTweenersReached + "/" + DOTween.maxActiveSequencesReached;
					Debugger.LogReport(message);
				}
				if (DOTween.instance == this)
				{
					DOTween.instance = null;
				}
			}
		}

		private void OnApplicationQuit()
		{
			DOTween.isQuitting = true;
		}

		public IDOTweenInit SetCapacity(int tweenersCapacity, int sequencesCapacity)
		{
			TweenManager.SetCapacities(tweenersCapacity, sequencesCapacity);
			return this;
		}

		internal IEnumerator WaitForCompletion(Tween t)
		{
			while (t.active && !t.isComplete)
			{
				yield return null;
			}
		}

		internal IEnumerator WaitForRewind(Tween t)
		{
			while (t.active && (!t.playedOnce || !(t.position * (float)(t.completedLoops + 1) <= 0f)))
			{
				yield return null;
			}
		}

		internal IEnumerator WaitForKill(Tween t)
		{
			while (t.active)
			{
				yield return null;
			}
		}

		internal IEnumerator WaitForElapsedLoops(Tween t, int elapsedLoops)
		{
			while (t.active && t.completedLoops < elapsedLoops)
			{
				yield return null;
			}
		}

		internal IEnumerator WaitForPosition(Tween t, float position)
		{
			while (t.active && !(t.position * (float)(t.completedLoops + 1) >= position))
			{
				yield return null;
			}
		}

		internal IEnumerator WaitForStart(Tween t)
		{
			while (t.active && !t.playedOnce)
			{
				yield return null;
			}
		}

		internal static void Create()
		{
			if (!(DOTween.instance != null))
			{
				GameObject gameObject = new GameObject("[DOTween]");
				Object.DontDestroyOnLoad(gameObject);
				DOTween.instance = gameObject.AddComponent<DOTweenComponent>();
			}
		}

		internal static void DestroyInstance()
		{
			if (DOTween.instance != null)
			{
				Object.Destroy(DOTween.instance.gameObject);
			}
			DOTween.instance = null;
		}
	}
}
