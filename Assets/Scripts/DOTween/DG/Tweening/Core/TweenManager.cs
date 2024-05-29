using System;
using System.Collections.Generic;
using DG.Tweening.Core.Enums;

namespace DG.Tweening.Core
{
	internal static class TweenManager
	{
		internal enum CapacityIncreaseMode
		{
			TweenersAndSequences = 0,
			TweenersOnly = 1,
			SequencesOnly = 2
		}

		private const int _DefaultMaxTweeners = 200;

		private const int _DefaultMaxSequences = 50;

		private const string _MaxTweensReached = "Max Tweens reached: capacity will be automatically increased from #0 to #1. Use DOTween.SetTweensCapacity to set it manually at startup";

		internal static int maxActive = 200;

		internal static int maxTweeners = 200;

		internal static int maxSequences = 50;

		internal static bool hasActiveTweens;

		internal static bool hasActiveDefaultTweens;

		internal static bool hasActiveLateTweens;

		internal static bool hasActiveFixedTweens;

		internal static int totActiveTweens;

		internal static int totActiveDefaultTweens;

		internal static int totActiveLateTweens;

		internal static int totActiveFixedTweens;

		internal static int totActiveTweeners;

		internal static int totActiveSequences;

		internal static int totPooledTweeners;

		internal static int totPooledSequences;

		internal static int totTweeners;

		internal static int totSequences;

		internal static bool isUpdateLoop;

		private static Tween[] _activeTweens = new Tween[200];

		private static Tween[] _pooledTweeners = new Tween[200];

		private static readonly Stack<Tween> _PooledSequences = new Stack<Tween>();

		private static readonly List<Tween> _KillList = new List<Tween>(200);

		private static int _maxActiveLookupId = -1;

		private static bool _requiresActiveReorganization;

		private static int _reorganizeFromId = -1;

		private static int _minPooledTweenerId = -1;

		private static int _maxPooledTweenerId = -1;

		internal static TweenerCore<T1, T2, TPlugOptions> GetTweener<T1, T2, TPlugOptions>() where TPlugOptions : struct
		{
			TweenerCore<T1, T2, TPlugOptions> tweenerCore;
			if (totPooledTweeners > 0)
			{
				Type typeFromHandle = typeof(T1);
				Type typeFromHandle2 = typeof(T2);
				Type typeFromHandle3 = typeof(TPlugOptions);
				int num = _maxPooledTweenerId;
				while (num > _minPooledTweenerId - 1)
				{
					Tween tween = _pooledTweeners[num];
					if (tween == null || tween.typeofT1 != typeFromHandle || tween.typeofT2 != typeFromHandle2 || tween.typeofTPlugOptions != typeFromHandle3)
					{
						num--;
						continue;
					}
					tweenerCore = (TweenerCore<T1, T2, TPlugOptions>)tween;
					AddActiveTween(tweenerCore);
					_pooledTweeners[num] = null;
					if (_maxPooledTweenerId != _minPooledTweenerId)
					{
						if (num == _maxPooledTweenerId)
						{
							_maxPooledTweenerId--;
						}
						else if (num == _minPooledTweenerId)
						{
							_minPooledTweenerId++;
						}
					}
					totPooledTweeners--;
					return tweenerCore;
				}
				if (totTweeners >= maxTweeners)
				{
					_pooledTweeners[_maxPooledTweenerId] = null;
					_maxPooledTweenerId--;
					totPooledTweeners--;
					totTweeners--;
				}
			}
			else if (totTweeners >= maxTweeners)
			{
				if (Debugger.logPriority >= 1)
				{
					Debugger.LogWarning("Max Tweens reached: capacity will be automatically increased from #0 to #1. Use DOTween.SetTweensCapacity to set it manually at startup".Replace("#0", maxTweeners + "/" + maxSequences).Replace("#1", maxTweeners + 200 + "/" + maxSequences));
				}
				IncreaseCapacities(CapacityIncreaseMode.TweenersOnly);
			}
			tweenerCore = new TweenerCore<T1, T2, TPlugOptions>();
			totTweeners++;
			AddActiveTween(tweenerCore);
			return tweenerCore;
		}

		internal static Sequence GetSequence()
		{
			Sequence sequence;
			if (totPooledSequences > 0)
			{
				sequence = (Sequence)_PooledSequences.Pop();
				AddActiveTween(sequence);
				totPooledSequences--;
				return sequence;
			}
			if (totSequences >= maxSequences)
			{
				if (Debugger.logPriority >= 1)
				{
					Debugger.LogWarning("Max Tweens reached: capacity will be automatically increased from #0 to #1. Use DOTween.SetTweensCapacity to set it manually at startup".Replace("#0", maxTweeners + "/" + maxSequences).Replace("#1", maxTweeners + "/" + (maxSequences + 50)));
				}
				IncreaseCapacities(CapacityIncreaseMode.SequencesOnly);
			}
			sequence = new Sequence();
			totSequences++;
			AddActiveTween(sequence);
			return sequence;
		}

		internal static void SetUpdateType(Tween t, UpdateType updateType, bool isIndependentUpdate)
		{
			if (t.active && t.updateType != updateType)
			{
				if (t.updateType == UpdateType.Normal)
				{
					totActiveDefaultTweens--;
					hasActiveDefaultTweens = totActiveDefaultTweens > 0;
				}
				else if (t.updateType == UpdateType.Fixed)
				{
					totActiveFixedTweens--;
					hasActiveFixedTweens = totActiveFixedTweens > 0;
				}
				else
				{
					totActiveLateTweens--;
					hasActiveLateTweens = totActiveLateTweens > 0;
				}
				t.updateType = updateType;
				t.isIndependentUpdate = isIndependentUpdate;
				switch (updateType)
				{
				case UpdateType.Normal:
					totActiveDefaultTweens++;
					hasActiveDefaultTweens = true;
					break;
				case UpdateType.Fixed:
					totActiveFixedTweens++;
					hasActiveFixedTweens = true;
					break;
				default:
					totActiveLateTweens++;
					hasActiveLateTweens = true;
					break;
				}
			}
			else
			{
				t.updateType = updateType;
				t.isIndependentUpdate = isIndependentUpdate;
			}
		}

		internal static void AddActiveTweenToSequence(Tween t)
		{
			RemoveActiveTween(t);
		}

		internal static int DespawnAll()
		{
			int result = totActiveTweens;
			for (int i = 0; i < _maxActiveLookupId + 1; i++)
			{
				Tween tween = _activeTweens[i];
				if (tween != null)
				{
					Despawn(tween, false);
				}
			}
			ClearTweenArray(_activeTweens);
			hasActiveFixedTweens = false;
			hasActiveLateTweens = false;
			hasActiveDefaultTweens = false;
			hasActiveTweens = false;
			totActiveFixedTweens = 0;
			totActiveLateTweens = 0;
			totActiveDefaultTweens = 0;
			totActiveTweens = 0;
			totActiveSequences = 0;
			totActiveTweeners = 0;
			_reorganizeFromId = -1;
			_maxActiveLookupId = -1;
			_requiresActiveReorganization = false;
			return result;
		}

		internal static void Despawn(Tween t, bool modifyActiveLists = true)
		{
			if (t.onKill != null)
			{
				Tween.OnTweenCallback(t.onKill);
			}
			if (modifyActiveLists)
			{
				RemoveActiveTween(t);
			}
			if (t.isRecyclable)
			{
				switch (t.tweenType)
				{
				case TweenType.Tweener:
					if (_maxPooledTweenerId == -1)
					{
						_maxPooledTweenerId = maxTweeners - 1;
						_minPooledTweenerId = maxTweeners - 1;
					}
					if (_maxPooledTweenerId < maxTweeners - 1)
					{
						_pooledTweeners[_maxPooledTweenerId + 1] = t;
						_maxPooledTweenerId++;
						if (_minPooledTweenerId > _maxPooledTweenerId)
						{
							_minPooledTweenerId = _maxPooledTweenerId;
						}
					}
					else
					{
						int num = _maxPooledTweenerId;
						while (num > -1)
						{
							if (_pooledTweeners[num] != null)
							{
								num--;
								continue;
							}
							_pooledTweeners[num] = t;
							if (num < _minPooledTweenerId)
							{
								_minPooledTweenerId = num;
							}
							if (_maxPooledTweenerId < _minPooledTweenerId)
							{
								_maxPooledTweenerId = _minPooledTweenerId;
							}
							break;
						}
					}
					totPooledTweeners++;
					break;
				case TweenType.Sequence:
				{
					_PooledSequences.Push(t);
					totPooledSequences++;
					Sequence sequence = (Sequence)t;
					int count = sequence.sequencedTweens.Count;
					for (int i = 0; i < count; i++)
					{
						Despawn(sequence.sequencedTweens[i], false);
					}
					break;
				}
				}
			}
			else
			{
				switch (t.tweenType)
				{
				case TweenType.Tweener:
					totTweeners--;
					break;
				case TweenType.Sequence:
				{
					totSequences--;
					Sequence sequence2 = (Sequence)t;
					int count2 = sequence2.sequencedTweens.Count;
					for (int j = 0; j < count2; j++)
					{
						Despawn(sequence2.sequencedTweens[j], false);
					}
					break;
				}
				}
			}
			t.active = false;
			t.Reset();
		}

		internal static void PurgeAll()
		{
			for (int i = 0; i < totActiveTweens; i++)
			{
				Tween tween = _activeTweens[i];
				if (tween != null && tween.onKill != null)
				{
					Tween.OnTweenCallback(tween.onKill);
				}
			}
			ClearTweenArray(_activeTweens);
			hasActiveFixedTweens = false;
			hasActiveLateTweens = false;
			hasActiveDefaultTweens = false;
			hasActiveTweens = false;
			totActiveFixedTweens = 0;
			totActiveLateTweens = 0;
			totActiveDefaultTweens = 0;
			totActiveTweens = 0;
			totActiveSequences = 0;
			totActiveTweeners = 0;
			_reorganizeFromId = -1;
			_maxActiveLookupId = -1;
			_requiresActiveReorganization = false;
			PurgePools();
			ResetCapacities();
			totSequences = 0;
			totTweeners = 0;
		}

		internal static void PurgePools()
		{
			totTweeners -= totPooledTweeners;
			totSequences -= totPooledSequences;
			ClearTweenArray(_pooledTweeners);
			_PooledSequences.Clear();
			totPooledSequences = 0;
			totPooledTweeners = 0;
			_maxPooledTweenerId = -1;
			_minPooledTweenerId = -1;
		}

		internal static void ResetCapacities()
		{
			SetCapacities(200, 50);
		}

		internal static void SetCapacities(int tweenersCapacity, int sequencesCapacity)
		{
			if (tweenersCapacity < sequencesCapacity)
			{
				tweenersCapacity = sequencesCapacity;
			}
			maxActive = tweenersCapacity;
			maxTweeners = tweenersCapacity;
			maxSequences = sequencesCapacity;
			Array.Resize(ref _activeTweens, maxActive);
			Array.Resize(ref _pooledTweeners, tweenersCapacity);
			_KillList.Capacity = maxActive;
		}

		internal static int Validate()
		{
			if (_requiresActiveReorganization)
			{
				ReorganizeActiveTweens();
			}
			int num = 0;
			for (int i = 0; i < _maxActiveLookupId + 1; i++)
			{
				Tween tween = _activeTweens[i];
				if (!tween.Validate())
				{
					num++;
					MarkForKilling(tween);
				}
			}
			if (num > 0)
			{
				DespawnTweens(_KillList, false);
				int num2 = _KillList.Count - 1;
				for (int num3 = num2; num3 > -1; num3--)
				{
					RemoveActiveTween(_KillList[num3]);
				}
				_KillList.Clear();
			}
			return num;
		}

		internal static void Update(UpdateType updateType, float deltaTime, float independentTime)
		{
			if (_requiresActiveReorganization)
			{
				ReorganizeActiveTweens();
			}
			isUpdateLoop = true;
			bool flag = false;
			int num = _maxActiveLookupId + 1;
			for (int i = 0; i < num; i++)
			{
				Tween tween = _activeTweens[i];
				if (tween == null || tween.updateType != updateType)
				{
					continue;
				}
				if (!tween.active)
				{
					flag = true;
					MarkForKilling(tween);
				}
				else
				{
					if (!tween.isPlaying)
					{
						continue;
					}
					tween.creationLocked = true;
					float num2 = (tween.isIndependentUpdate ? independentTime : deltaTime) * tween.timeScale;
					if (!tween.delayComplete)
					{
						num2 = tween.UpdateDelay(tween.elapsedDelay + num2);
						if (num2 <= -1f)
						{
							flag = true;
							MarkForKilling(tween);
							continue;
						}
						if (num2 <= 0f)
						{
							continue;
						}
					}
					if (!tween.startupDone && !tween.Startup())
					{
						flag = true;
						MarkForKilling(tween);
						continue;
					}
					float position = tween.position;
					bool flag2 = position >= tween.duration;
					int num3 = tween.completedLoops;
					if (tween.duration <= 0f)
					{
						position = 0f;
						num3 = ((tween.loops == -1) ? (tween.completedLoops + 1) : tween.loops);
					}
					else
					{
						if (tween.isBackwards)
						{
							position -= num2;
							while (position < 0f && num3 > 0)
							{
								position += tween.duration;
								num3--;
							}
						}
						else
						{
							position += num2;
							while (!(position < tween.duration) && (tween.loops == -1 || num3 < tween.loops))
							{
								position -= tween.duration;
								num3++;
							}
						}
						if (flag2)
						{
							num3--;
						}
						if (tween.loops != -1 && num3 >= tween.loops)
						{
							position = tween.duration;
						}
					}
					if (Tween.DoGoto(tween, position, num3, UpdateMode.Update))
					{
						flag = true;
						MarkForKilling(tween);
					}
				}
			}
			if (flag)
			{
				DespawnTweens(_KillList, false);
				int num4 = _KillList.Count - 1;
				for (int num5 = num4; num5 > -1; num5--)
				{
					RemoveActiveTween(_KillList[num5]);
				}
				_KillList.Clear();
			}
			isUpdateLoop = false;
		}

		internal static int FilteredOperation(OperationType operationType, FilterType filterType, object id, bool optionalBool, float optionalFloat)
		{
			int num = 0;
			bool flag = false;
			for (int num2 = _maxActiveLookupId; num2 > -1; num2--)
			{
				Tween tween = _activeTweens[num2];
				if (tween != null && tween.active)
				{
					bool flag2 = false;
					switch (filterType)
					{
					case FilterType.All:
						flag2 = true;
						break;
					case FilterType.TargetOrId:
						flag2 = id.Equals(tween.id) || id.Equals(tween.target);
						break;
					}
					if (flag2)
					{
						switch (operationType)
						{
						case OperationType.Complete:
						{
							bool autoKill = tween.autoKill;
							if (!Complete(tween, false))
							{
								break;
							}
							num += ((!optionalBool) ? 1 : (autoKill ? 1 : 0));
							if (autoKill)
							{
								if (isUpdateLoop)
								{
									tween.active = false;
									break;
								}
								flag = true;
								_KillList.Add(tween);
							}
							break;
						}
						case OperationType.Despawn:
							num++;
							if (isUpdateLoop)
							{
								tween.active = false;
								break;
							}
							Despawn(tween, false);
							flag = true;
							_KillList.Add(tween);
							break;
						case OperationType.Flip:
							if (Flip(tween))
							{
								num++;
							}
							break;
						case OperationType.Goto:
							Goto(tween, optionalFloat, optionalBool);
							num++;
							break;
						case OperationType.Pause:
							if (Pause(tween))
							{
								num++;
							}
							break;
						case OperationType.Play:
							if (Play(tween))
							{
								num++;
							}
							break;
						case OperationType.PlayForward:
							if (PlayForward(tween))
							{
								num++;
							}
							break;
						case OperationType.PlayBackwards:
							if (PlayBackwards(tween))
							{
								num++;
							}
							break;
						case OperationType.Rewind:
							if (Rewind(tween, optionalBool))
							{
								num++;
							}
							break;
						case OperationType.Restart:
							if (Restart(tween, optionalBool))
							{
								num++;
							}
							break;
						case OperationType.TogglePause:
							if (TogglePause(tween))
							{
								num++;
							}
							break;
						case OperationType.IsTweening:
							num++;
							break;
						}
					}
				}
			}
			if (flag)
			{
				int num3 = _KillList.Count - 1;
				for (int num4 = num3; num4 > -1; num4--)
				{
					RemoveActiveTween(_KillList[num4]);
				}
				_KillList.Clear();
			}
			return num;
		}

		internal static bool Complete(Tween t, bool modifyActiveLists = true)
		{
			if (t.loops == -1)
			{
				return false;
			}
			if (!t.isComplete)
			{
				Tween.DoGoto(t, t.duration, t.loops, UpdateMode.Goto);
				t.isPlaying = false;
				if (t.autoKill)
				{
					if (isUpdateLoop)
					{
						t.active = false;
					}
					else
					{
						Despawn(t, modifyActiveLists);
					}
				}
				return true;
			}
			return false;
		}

		internal static bool Flip(Tween t)
		{
			t.isBackwards = !t.isBackwards;
			return true;
		}

		internal static void ForceInit(Tween t)
		{
			if (!t.startupDone && !t.Startup())
			{
				if (isUpdateLoop)
				{
					t.active = false;
				}
				else
				{
					RemoveActiveTween(t);
				}
			}
		}

		internal static bool Goto(Tween t, float to, bool andPlay = false, UpdateMode updateMode = UpdateMode.Goto)
		{
			bool isPlaying = t.isPlaying;
			t.isPlaying = andPlay;
			t.delayComplete = true;
			t.elapsedDelay = t.delay;
			int num = (int)(to / t.duration);
			float num2 = to % t.duration;
			if (t.loops != -1 && num >= t.loops)
			{
				num = t.loops;
				num2 = t.duration;
			}
			else if (num2 >= t.duration)
			{
				num2 = 0f;
			}
			bool flag = Tween.DoGoto(t, num2, num, updateMode);
			if (!andPlay && isPlaying && !flag && t.onPause != null)
			{
				Tween.OnTweenCallback(t.onPause);
			}
			return flag;
		}

		internal static bool Pause(Tween t)
		{
			if (t.isPlaying)
			{
				t.isPlaying = false;
				if (t.onPause != null)
				{
					Tween.OnTweenCallback(t.onPause);
				}
				return true;
			}
			return false;
		}

		internal static bool Play(Tween t)
		{
			if (!t.isPlaying && ((!t.isBackwards && !t.isComplete) || (t.isBackwards && (t.completedLoops > 0 || t.position > 0f))))
			{
				t.isPlaying = true;
				if (t.playedOnce && t.onPlay != null)
				{
					Tween.OnTweenCallback(t.onPlay);
				}
				return true;
			}
			return false;
		}

		internal static bool PlayBackwards(Tween t)
		{
			if (!t.isBackwards)
			{
				t.isBackwards = true;
				Play(t);
				return true;
			}
			return Play(t);
		}

		internal static bool PlayForward(Tween t)
		{
			if (t.isBackwards)
			{
				t.isBackwards = false;
				Play(t);
				return true;
			}
			return Play(t);
		}

		internal static bool Restart(Tween t, bool includeDelay = true)
		{
			bool isPlaying = t.isPlaying;
			t.isBackwards = false;
			Rewind(t, includeDelay);
			t.isPlaying = true;
			if (isPlaying && t.playedOnce && t.onPlay != null)
			{
				Tween.OnTweenCallback(t.onPlay);
			}
			return true;
		}

		internal static bool Rewind(Tween t, bool includeDelay = true)
		{
			bool isPlaying = t.isPlaying;
			t.isPlaying = false;
			bool result = false;
			if (t.delay > 0f)
			{
				if (includeDelay)
				{
					result = t.delay > 0f && t.elapsedDelay > 0f;
					t.elapsedDelay = 0f;
					t.delayComplete = false;
				}
				else
				{
					result = t.elapsedDelay < t.delay;
					t.elapsedDelay = t.delay;
					t.delayComplete = true;
				}
			}
			if (t.position > 0f || t.completedLoops > 0 || !t.startupDone)
			{
				result = true;
				if (!Tween.DoGoto(t, 0f, 0, UpdateMode.Goto) && isPlaying && t.onPause != null)
				{
					Tween.OnTweenCallback(t.onPause);
				}
			}
			return result;
		}

		internal static bool TogglePause(Tween t)
		{
			if (t.isPlaying)
			{
				return Pause(t);
			}
			return Play(t);
		}

		internal static int TotalPooledTweens()
		{
			return totPooledTweeners + totPooledSequences;
		}

		internal static int TotalPlayingTweens()
		{
			if (!hasActiveTweens)
			{
				return 0;
			}
			if (_requiresActiveReorganization)
			{
				ReorganizeActiveTweens();
			}
			int num = 0;
			for (int i = 0; i < _maxActiveLookupId + 1; i++)
			{
				Tween tween = _activeTweens[i];
				if (tween != null && tween.isPlaying)
				{
					num++;
				}
			}
			return num;
		}

		internal static List<Tween> GetActiveTweens(bool playing)
		{
			if (_requiresActiveReorganization)
			{
				ReorganizeActiveTweens();
			}
			if (totActiveTweens <= 0)
			{
				return null;
			}
			int num = totActiveTweens;
			List<Tween> list = new List<Tween>(num);
			for (int i = 0; i < num; i++)
			{
				Tween tween = _activeTweens[i];
				if (tween.isPlaying == playing)
				{
					list.Add(tween);
				}
			}
			if (list.Count > 0)
			{
				return list;
			}
			return null;
		}

		internal static List<Tween> GetTweensById(object id)
		{
			if (_requiresActiveReorganization)
			{
				ReorganizeActiveTweens();
			}
			if (totActiveTweens <= 0)
			{
				return null;
			}
			int num = totActiveTweens;
			List<Tween> list = new List<Tween>(num);
			for (int i = 0; i < num; i++)
			{
				Tween tween = _activeTweens[i];
				if (tween.id == id)
				{
					list.Add(tween);
				}
			}
			if (list.Count > 0)
			{
				return list;
			}
			return null;
		}

		internal static List<Tween> GetTweensByTarget(object target)
		{
			if (_requiresActiveReorganization)
			{
				ReorganizeActiveTweens();
			}
			if (totActiveTweens <= 0)
			{
				return null;
			}
			int num = totActiveTweens;
			List<Tween> list = new List<Tween>(num);
			for (int i = 0; i < num; i++)
			{
				Tween tween = _activeTweens[i];
				if (tween.target == target)
				{
					list.Add(tween);
				}
			}
			if (list.Count > 0)
			{
				return list;
			}
			return null;
		}

		private static void MarkForKilling(Tween t)
		{
			t.active = false;
			_KillList.Add(t);
		}

		private static void AddActiveTween(Tween t)
		{
			if (_requiresActiveReorganization)
			{
				ReorganizeActiveTweens();
			}
			t.active = true;
			t.updateType = DOTween.defaultUpdateType;
			t.isIndependentUpdate = DOTween.defaultTimeScaleIndependent;
			t.activeId = (_maxActiveLookupId = totActiveTweens);
			_activeTweens[totActiveTweens] = t;
			hasActiveDefaultTweens = true;
			totActiveDefaultTweens++;
			totActiveTweens++;
			if (t.tweenType == TweenType.Tweener)
			{
				totActiveTweeners++;
			}
			else
			{
				totActiveSequences++;
			}
			hasActiveTweens = true;
		}

		private static void ReorganizeActiveTweens()
		{
			if (totActiveTweens <= 0)
			{
				_maxActiveLookupId = -1;
				_requiresActiveReorganization = false;
				_reorganizeFromId = -1;
				return;
			}
			if (_reorganizeFromId == _maxActiveLookupId)
			{
				_maxActiveLookupId--;
				_requiresActiveReorganization = false;
				_reorganizeFromId = -1;
				return;
			}
			int num = 1;
			int num2 = _maxActiveLookupId + 1;
			_maxActiveLookupId = _reorganizeFromId - 1;
			for (int i = _reorganizeFromId + 1; i < num2; i++)
			{
				Tween tween = _activeTweens[i];
				if (tween == null)
				{
					num++;
					continue;
				}
				tween.activeId = (_maxActiveLookupId = i - num);
				_activeTweens[i - num] = tween;
				_activeTweens[i] = null;
			}
			_requiresActiveReorganization = false;
			_reorganizeFromId = -1;
		}

		private static void DespawnTweens(List<Tween> tweens, bool modifyActiveLists = true)
		{
			int count = tweens.Count;
			for (int i = 0; i < count; i++)
			{
				Despawn(tweens[i], modifyActiveLists);
			}
		}

		private static void RemoveActiveTween(Tween t)
		{
			int activeId = t.activeId;
			t.activeId = -1;
			_requiresActiveReorganization = true;
			if (_reorganizeFromId == -1 || _reorganizeFromId > activeId)
			{
				_reorganizeFromId = activeId;
			}
			_activeTweens[activeId] = null;
			if (t.updateType == UpdateType.Normal)
			{
				totActiveDefaultTweens--;
				hasActiveDefaultTweens = totActiveDefaultTweens > 0;
			}
			else if (t.updateType == UpdateType.Fixed)
			{
				totActiveFixedTweens--;
				hasActiveFixedTweens = totActiveFixedTweens > 0;
			}
			else
			{
				totActiveLateTweens--;
				hasActiveLateTweens = totActiveLateTweens > 0;
			}
			totActiveTweens--;
			hasActiveTweens = totActiveTweens > 0;
			if (t.tweenType == TweenType.Tweener)
			{
				totActiveTweeners--;
			}
			else
			{
				totActiveSequences--;
			}
		}

		private static void ClearTweenArray(Tween[] tweens)
		{
			int num = tweens.Length;
			for (int i = 0; i < num; i++)
			{
				tweens[i] = null;
			}
		}

		private static void IncreaseCapacities(CapacityIncreaseMode increaseMode)
		{
			int num = 0;
			switch (increaseMode)
			{
			default:
				num += 200;
				maxTweeners += 200;
				maxSequences += 50;
				Array.Resize(ref _pooledTweeners, maxTweeners);
				break;
			case CapacityIncreaseMode.TweenersOnly:
				num += 200;
				maxTweeners += 200;
				Array.Resize(ref _pooledTweeners, maxTweeners);
				break;
			case CapacityIncreaseMode.SequencesOnly:
				num += 50;
				maxSequences += 50;
				break;
			}
			maxActive = maxTweeners;
			Array.Resize(ref _activeTweens, maxActive);
			if (num > 0)
			{
				_KillList.Capacity += num;
			}
		}
	}
}
