using UnityEngine;

public class WeaponEffects : MonoBehaviour
{
	private enum TypeEffect
	{
		Time = 0,
		AnimationTime = 1
	}

	public float timeActive = 0.15f;

	public AnimationClip animationEffect;

	public int startFrame;

	public int endFrame;

	private TypeEffect typeEffect_0;

	private AnimationState animationState_0;

	private float float_0;

	private float float_1;

	private float float_2;

	private float float_3;

	private float float_4;

	private bool bool_0 = true;

	private bool bool_1;

	private Transform transform_0;

	private void Awake()
	{
		transform_0 = base.transform;
	}

	private void OnEnable()
	{
		SetActiveEffects(false);
	}

	private void Start()
	{
		if (animationEffect != null)
		{
			InitBaseAnimationTime();
		}
	}

	private void Update()
	{
		if (bool_1)
		{
			if (typeEffect_0 == TypeEffect.AnimationTime)
			{
				UpdateAnimationTime();
			}
			else
			{
				UpdateTime();
			}
		}
	}

	private void ShowEffects()
	{
		if (bool_1)
		{
			Reset();
			return;
		}
		bool_1 = true;
		bool_0 = true;
		if (animationEffect == null)
		{
			InitBaseTime();
		}
	}

	private void Reset()
	{
		if (typeEffect_0 == TypeEffect.Time)
		{
			InitBaseTime();
		}
	}

	private void InitBaseAnimationTime()
	{
		typeEffect_0 = TypeEffect.AnimationTime;
		float num = animationEffect.frameRate * animationEffect.length;
		float_0 = (float)startFrame / num;
		float_1 = (float)endFrame / num;
		animationState_0 = base.GetComponent<Animation>()[animationEffect.name];
	}

	private void InitBaseTime()
	{
		typeEffect_0 = TypeEffect.Time;
		float_1 = Time.time + timeActive;
	}

	private void UpdateAnimationTime()
	{
		float_2 += animationState_0.normalizedTime - float_4;
		if (float_2 > 1f || bool_0)
		{
			if (!bool_0)
			{
				float_2 -= 1f;
			}
			bool_0 = false;
		}
		if (float_3 < float_0 && float_2 >= float_0)
		{
			StartEffects();
		}
		else if (float_3 < float_1 && float_2 >= float_1)
		{
			StopEffects();
		}
		float_3 = float_2;
		float_4 = animationState_0.normalizedTime;
	}

	private void UpdateTime()
	{
		if (bool_0)
		{
			bool_0 = false;
			StartEffects();
		}
		else if (float_1 <= Time.time)
		{
			StopEffects();
		}
	}

	protected void StartEffects()
	{
		SetActiveEffects(true);
	}

	protected void StopEffects()
	{
		bool_1 = false;
		SetActiveEffects(false);
	}

	private void SetActiveEffects(bool bool_2)
	{
		foreach (Transform item in transform_0)
		{
			WeaponCustomEffect component = item.GetComponent<WeaponCustomEffect>();
			if (component != null)
			{
				component.SetActiveEffect(bool_2);
			}
			else
			{
				item.gameObject.SetActive(bool_2);
			}
		}
	}
}
