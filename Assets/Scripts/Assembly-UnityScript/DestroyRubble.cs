using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class DestroyRubble : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	internal sealed class _0024Start_002427 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		[CompilerGenerated]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal int int_0;

			internal DestroyRubble destroyRubble_0;

			public _0024(DestroyRubble destroyRubble_1)
			{
				destroyRubble_0 = destroyRubble_1;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					result = (Yield(2, new WaitForSeconds(destroyRubble_0.time)) ? 1 : 0);
					break;
				case 2:
					int_0 = 0;
					goto IL_005b;
				case 3:
					UnityEngine.Object.Destroy(destroyRubble_0.gameObject);
					int_0++;
					goto IL_005b;
				case 1:
					{
						result = 0;
						break;
					}
					IL_005b:
					if (int_0 >= destroyRubble_0.particleEmitters.Length)
					{
						YieldDefault(1);
						goto case 1;
					}
					destroyRubble_0.particleEmitters[int_0].emit = false;
					result = (Yield(3, new WaitForSeconds(destroyRubble_0.maxTime)) ? 1 : 0);
					break;
				}
				return (byte)result != 0;
			}
		}

		internal DestroyRubble destroyRubble_0;

		public _0024Start_002427(DestroyRubble destroyRubble_1)
		{
			destroyRubble_0 = destroyRubble_1;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(destroyRubble_0);
		}
	}

	public float maxTime;

	public ParticleEmitter[] particleEmitters;

	public float time;

	public DestroyRubble()
	{
		maxTime = 3f;
	}

	public virtual IEnumerator Start()
	{
		return new _0024Start_002427(this).GetEnumerator();
	}

	public virtual void Main()
	{
	}
}
