using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class FireFade : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	internal sealed class _0024Start_002431 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		[CompilerGenerated]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal FireFade fireFade_0;

			public _0024(FireFade fireFade_1)
			{
				fireFade_0 = fireFade_1;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					result = (Yield(2, new WaitForSeconds(fireFade_0.smokeDestroyTime)) ? 1 : 0);
					break;
				case 2:
					fireFade_0.bool_0 = true;
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal FireFade fireFade_0;

		public _0024Start_002431(FireFade fireFade_1)
		{
			fireFade_0 = fireFade_1;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(fireFade_0);
		}
	}

	public float smokeDestroyTime;

	public float destroySpeed;

	private bool bool_0;

	public FireFade()
	{
		smokeDestroyTime = 6f;
		destroySpeed = 0.05f;
	}

	public virtual IEnumerator Start()
	{
		return new _0024Start_002431(this).GetEnumerator();
	}

	public virtual void Update()
	{
		if (bool_0)
		{
			ParticleRenderer particleRenderer = (ParticleRenderer)GetComponent(typeof(ParticleRenderer));
			Color color = particleRenderer.materials[1].GetColor("_TintColor");
			color.a -= destroySpeed * Time.deltaTime;
			particleRenderer.materials[1].SetColor("_TintColor", color);
		}
	}

	public virtual void Main()
	{
	}
}
