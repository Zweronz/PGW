using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Enums;

namespace DG.Tweening
{
	public abstract class Tween : ABSSequentiable
	{
		public float timeScale;

		public bool isBackwards;

		public object id;

		public object target;

		internal UpdateType updateType;

		internal bool isIndependentUpdate;

		internal TweenCallback onPlay;

		internal TweenCallback onPause;

		internal TweenCallback onRewind;

		internal TweenCallback onUpdate;

		internal TweenCallback onStepComplete;

		internal TweenCallback onComplete;

		internal TweenCallback onKill;

		internal TweenCallback<int> onWaypointChange;

		internal bool isFrom;

		internal bool isRecyclable;

		internal bool isSpeedBased;

		internal bool autoKill;

		internal float duration;

		internal int loops;

		internal LoopType loopType;

		internal float delay;

		internal bool isRelative;

		internal Ease easeType;

		internal EaseFunction customEase;

		public float easeOvershootOrAmplitude;

		public float easePeriod;

		internal Type typeofT1;

		internal Type typeofT2;

		internal Type typeofTPlugOptions;

		internal bool active;

		internal bool isSequenced;

		internal Sequence sequenceParent;

		internal int activeId = -1;

		internal SpecialStartupMode specialStartupMode;

		internal bool creationLocked;

		internal bool startupDone;

		internal bool playedOnce;

		internal float position;

		internal float fullDuration;

		internal int completedLoops;

		internal bool isPlaying;

		internal bool isComplete;

		internal float elapsedDelay;

		internal bool delayComplete = true;

		internal int miscInt = -1;

		public float fullPosition
		{
			get
			{
				return this.Elapsed();
			}
			set
			{
				this.Goto(value, isPlaying);
			}
		}

		internal virtual void Reset()
		{
			timeScale = 1f;
			isBackwards = false;
			id = null;
			isIndependentUpdate = false;
			onStart = (onPlay = (onRewind = (onUpdate = (onComplete = (onStepComplete = (onKill = null))))));
			onWaypointChange = null;
			target = null;
			isFrom = false;
			isSpeedBased = false;
			duration = 0f;
			loops = 1;
			delay = 0f;
			isRelative = false;
			customEase = null;
			isSequenced = false;
			sequenceParent = null;
			specialStartupMode = SpecialStartupMode.None;
			playedOnce = false;
			startupDone = false;
			creationLocked = false;
			completedLoops = 0;
			float num = 0f;
			fullDuration = 0f;
			position = num;
			isComplete = false;
			isPlaying = false;
			elapsedDelay = 0f;
			delayComplete = true;
			miscInt = -1;
		}

		internal abstract bool Validate();

		internal virtual float UpdateDelay(float elapsed)
		{
			return 0f;
		}

		internal abstract bool Startup();

		internal abstract bool ApplyTween(float prevPosition, int prevCompletedLoops, int newCompletedSteps, bool useInversePosition, UpdateMode updateMode);

		internal static bool DoGoto(Tween t, float toPosition, int toCompletedLoops, UpdateMode updateMode)
		{
			if (!t.startupDone && !t.Startup())
			{
				return true;
			}
			if (!t.playedOnce && updateMode == UpdateMode.Update)
			{
				t.playedOnce = true;
				if (t.onStart != null)
				{
					OnTweenCallback(t.onStart);
					if (!t.active)
					{
						return true;
					}
				}
				if (t.onPlay != null)
				{
					OnTweenCallback(t.onPlay);
					if (!t.active)
					{
						return true;
					}
				}
			}
			float prevPosition = t.position;
			int num = t.completedLoops;
			t.completedLoops = toCompletedLoops;
			bool flag = t.position <= 0f && num <= 0;
			bool flag2 = t.isComplete;
			if (t.loops != -1)
			{
				t.isComplete = t.completedLoops == t.loops;
			}
			int num2 = 0;
			if (updateMode == UpdateMode.Update)
			{
				if (t.isBackwards)
				{
					num2 = ((t.completedLoops < num) ? (num - t.completedLoops) : ((toPosition <= 0f && !flag) ? 1 : 0));
					if (flag2)
					{
						num2--;
					}
				}
				else
				{
					num2 = ((t.completedLoops > num) ? (t.completedLoops - num) : 0);
				}
			}
			else if (t.tweenType == TweenType.Sequence)
			{
				num2 = num - toCompletedLoops;
				if (num2 < 0)
				{
					num2 = -num2;
				}
			}
			t.position = toPosition;
			if (t.position > t.duration)
			{
				t.position = t.duration;
			}
			else if (t.position <= 0f)
			{
				if (t.completedLoops <= 0 && !t.isComplete)
				{
					t.position = 0f;
				}
				else
				{
					t.position = t.duration;
				}
			}
			bool flag3 = t.isPlaying;
			if (t.isPlaying)
			{
				if (!t.isBackwards)
				{
					t.isPlaying = !t.isComplete;
				}
				else
				{
					t.isPlaying = t.completedLoops != 0 || !(t.position <= 0f);
				}
			}
			bool useInversePosition = t.loopType == LoopType.Yoyo && ((t.position < t.duration) ? (t.completedLoops % 2 != 0) : (t.completedLoops % 2 == 0));
			if (t.ApplyTween(prevPosition, num, num2, useInversePosition, updateMode))
			{
				return true;
			}
			if (t.onUpdate != null)
			{
				OnTweenCallback(t.onUpdate);
			}
			if (t.position <= 0f && t.completedLoops <= 0 && !flag && t.onRewind != null)
			{
				OnTweenCallback(t.onRewind);
			}
			if (num2 > 0 && updateMode == UpdateMode.Update && t.onStepComplete != null)
			{
				for (int i = 0; i < num2; i++)
				{
					OnTweenCallback(t.onStepComplete);
				}
			}
			if (t.isComplete && !flag2 && t.onComplete != null)
			{
				OnTweenCallback(t.onComplete);
			}
			if (!t.isPlaying && flag3 && (!t.isComplete || !t.autoKill) && t.onPause != null)
			{
				OnTweenCallback(t.onPause);
			}
			if (t.autoKill)
			{
				return t.isComplete;
			}
			return false;
		}

		internal static bool OnTweenCallback(TweenCallback callback)
		{
			if (DOTween.useSafeMode)
			{
				try
				{
					callback();
				}
				catch (Exception ex)
				{
					Debugger.LogWarning("An error inside a tween callback was silently taken care of > " + ex.Message);
					return false;
				}
			}
			else
			{
				callback();
			}
			return true;
		}

		internal static bool OnTweenCallback<T>(TweenCallback<T> callback, T param)
		{
			if (DOTween.useSafeMode)
			{
				try
				{
					callback(param);
				}
				catch (Exception ex)
				{
					Debugger.LogWarning("An error inside a tween callback was silently taken care of > " + ex.Message);
					return false;
				}
			}
			else
			{
				callback(param);
			}
			return true;
		}
	}
}
