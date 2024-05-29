using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

public class ActiveAnimation : MonoBehaviour
{
	public static ActiveAnimation activeAnimation_0;

	public List<EventDelegate> onFinished = new List<EventDelegate>();

	public GameObject gameObject_0;

	public string string_0;

	private Animation animation_0;

	private Direction direction_0;

	private Direction direction_1;

	private bool bool_0;

	private Animator animator_0;

	private string string_1 = string.Empty;

	private float Single_0
	{
		get
		{
			return Mathf.Clamp01(animator_0.GetCurrentAnimatorStateInfo(0).normalizedTime);
		}
	}

	public bool Boolean_0
	{
		get
		{
			if (animation_0 == null)
			{
				if (animator_0 != null)
				{
					if (direction_0 == Direction.Reverse)
					{
						if (Single_0 == 0f)
						{
							return false;
						}
					}
					else if (Single_0 == 1f)
					{
						return false;
					}
					return true;
				}
				return false;
			}
			foreach (AnimationState item in animation_0)
			{
				if (!animation_0.IsPlaying(item.name))
				{
					continue;
				}
				if (direction_0 == Direction.Forward)
				{
					if (item.time < item.length)
					{
						return true;
					}
					continue;
				}
				if (direction_0 == Direction.Reverse)
				{
					if (!(item.time <= 0f))
					{
						return true;
					}
					continue;
				}
				return true;
			}
			return false;
		}
	}

	public void Finish()
	{
		if (animation_0 != null)
		{
			foreach (AnimationState item in animation_0)
			{
				if (direction_0 == Direction.Forward)
				{
					item.time = item.length;
				}
				else if (direction_0 == Direction.Reverse)
				{
					item.time = 0f;
				}
			}
			return;
		}
		if (animator_0 != null)
		{
			animator_0.Play(string_1, 0, (direction_0 != Direction.Forward) ? 0f : 1f);
		}
	}

	public void Reset()
	{
		if (animation_0 != null)
		{
			foreach (AnimationState item in animation_0)
			{
				if (direction_0 == Direction.Reverse)
				{
					item.time = item.length;
				}
				else if (direction_0 == Direction.Forward)
				{
					item.time = 0f;
				}
			}
			return;
		}
		if (animator_0 != null)
		{
			animator_0.Play(string_1, 0, (direction_0 != Direction.Reverse) ? 0f : 1f);
		}
	}

	private void Start()
	{
		if (gameObject_0 != null && EventDelegate.IsValid(onFinished))
		{
			gameObject_0 = null;
			string_0 = null;
		}
	}

	private void Update()
	{
		float single_ = RealTime.Single_1;
		if (single_ == 0f)
		{
			return;
		}
		if (animator_0 != null)
		{
			animator_0.Update((direction_0 != Direction.Reverse) ? single_ : (0f - single_));
			if (Boolean_0)
			{
				return;
			}
			animator_0.enabled = false;
			base.enabled = false;
		}
		else
		{
			if (!(animation_0 != null))
			{
				base.enabled = false;
				return;
			}
			bool flag = false;
			foreach (AnimationState item in animation_0)
			{
				if (!animation_0.IsPlaying(item.name))
				{
					continue;
				}
				float num = item.speed * single_;
				item.time += num;
				if (num < 0f)
				{
					if (item.time > 0f)
					{
						flag = true;
					}
					else
					{
						item.time = 0f;
					}
				}
				else if (item.time < item.length)
				{
					flag = true;
				}
				else
				{
					item.time = item.length;
				}
			}
			animation_0.Sample();
			if (flag)
			{
				return;
			}
			base.enabled = false;
		}
		if (!bool_0)
		{
			return;
		}
		bool_0 = false;
		if (activeAnimation_0 == null)
		{
			activeAnimation_0 = this;
			EventDelegate.Execute(onFinished);
			if (gameObject_0 != null && !string.IsNullOrEmpty(string_0))
			{
				gameObject_0.SendMessage(string_0, SendMessageOptions.DontRequireReceiver);
			}
			activeAnimation_0 = null;
		}
		if (direction_1 != 0 && direction_0 == direction_1)
		{
			NGUITools.SetActive(base.gameObject, false);
		}
	}

	private void Play(string string_2, Direction direction_2)
	{
		if (direction_2 == Direction.Toggle)
		{
			direction_2 = ((direction_0 != Direction.Forward) ? Direction.Forward : Direction.Reverse);
		}
		if (animation_0 != null)
		{
			base.enabled = true;
			animation_0.enabled = false;
			if (string.IsNullOrEmpty(string_2))
			{
				if (!animation_0.isPlaying)
				{
					animation_0.Play();
				}
			}
			else if (!animation_0.IsPlaying(string_2))
			{
				animation_0.Play(string_2);
			}
			foreach (AnimationState item in animation_0)
			{
				if (string.IsNullOrEmpty(string_2) || item.name == string_2)
				{
					float num = Mathf.Abs(item.speed);
					item.speed = num * (float)direction_2;
					if (direction_2 == Direction.Reverse && item.time == 0f)
					{
						item.time = item.length;
					}
					else if (direction_2 == Direction.Forward && item.time == item.length)
					{
						item.time = 0f;
					}
				}
			}
			direction_0 = direction_2;
			bool_0 = true;
			animation_0.Sample();
		}
		else if (animator_0 != null)
		{
			if (base.enabled && Boolean_0 && string_1 == string_2)
			{
				direction_0 = direction_2;
				return;
			}
			base.enabled = true;
			bool_0 = true;
			direction_0 = direction_2;
			string_1 = string_2;
			animator_0.Play(string_1, 0, (direction_2 != Direction.Forward) ? 1f : 0f);
		}
	}

	public static ActiveAnimation Play(Animation animation_1, string string_2, Direction direction_2, EnableCondition enableCondition_0, DisableCondition disableCondition_0)
	{
		if (!NGUITools.GetActive(animation_1.gameObject))
		{
			if (enableCondition_0 != EnableCondition.EnableThenPlay)
			{
				return null;
			}
			NGUITools.SetActive(animation_1.gameObject, true);
			UIPanel[] componentsInChildren = animation_1.gameObject.GetComponentsInChildren<UIPanel>();
			int i = 0;
			for (int num = componentsInChildren.Length; i < num; i++)
			{
				componentsInChildren[i].Refresh();
			}
		}
		ActiveAnimation activeAnimation = animation_1.GetComponent<ActiveAnimation>();
		if (activeAnimation == null)
		{
			activeAnimation = animation_1.gameObject.AddComponent<ActiveAnimation>();
		}
		activeAnimation.animation_0 = animation_1;
		activeAnimation.direction_1 = (Direction)disableCondition_0;
		activeAnimation.onFinished.Clear();
		activeAnimation.Play(string_2, direction_2);
		if (activeAnimation.animation_0 != null)
		{
			activeAnimation.animation_0.Sample();
		}
		else if (activeAnimation.animator_0 != null)
		{
			activeAnimation.animator_0.Update(0f);
		}
		return activeAnimation;
	}

	public static ActiveAnimation Play(Animation animation_1, string string_2, Direction direction_2)
	{
		return Play(animation_1, string_2, direction_2, EnableCondition.DoNothing, DisableCondition.DoNotDisable);
	}

	public static ActiveAnimation Play(Animation animation_1, Direction direction_2)
	{
		return Play(animation_1, null, direction_2, EnableCondition.DoNothing, DisableCondition.DoNotDisable);
	}

	public static ActiveAnimation Play(Animator animator_1, string string_2, Direction direction_2, EnableCondition enableCondition_0, DisableCondition disableCondition_0)
	{
		if (!NGUITools.GetActive(animator_1.gameObject))
		{
			if (enableCondition_0 != EnableCondition.EnableThenPlay)
			{
				return null;
			}
			NGUITools.SetActive(animator_1.gameObject, true);
			UIPanel[] componentsInChildren = animator_1.gameObject.GetComponentsInChildren<UIPanel>();
			int i = 0;
			for (int num = componentsInChildren.Length; i < num; i++)
			{
				componentsInChildren[i].Refresh();
			}
		}
		ActiveAnimation activeAnimation = animator_1.GetComponent<ActiveAnimation>();
		if (activeAnimation == null)
		{
			activeAnimation = animator_1.gameObject.AddComponent<ActiveAnimation>();
		}
		activeAnimation.animator_0 = animator_1;
		activeAnimation.direction_1 = (Direction)disableCondition_0;
		activeAnimation.onFinished.Clear();
		activeAnimation.Play(string_2, direction_2);
		if (activeAnimation.animation_0 != null)
		{
			activeAnimation.animation_0.Sample();
		}
		else if (activeAnimation.animator_0 != null)
		{
			activeAnimation.animator_0.Update(0f);
		}
		return activeAnimation;
	}
}
