using System.Collections;
using UnityEngine;

internal sealed class MenuBackgroundMusic : MonoBehaviour
{
	public static MenuBackgroundMusic menuBackgroundMusic_0;

	private bool bool_0;

	private AudioSource audioSource_0;

	private AudioSource audioSource_1;

	private Coroutine coroutine_0;

	private AudioSource audioSource_2;

	private float float_0;

	private float float_1;

	public bool Boolean_0
	{
		get
		{
			return bool_0;
		}
	}

	private void Start()
	{
		audioSource_1 = base.GetComponent<AudioSource>();
		menuBackgroundMusic_0 = this;
		bool_0 = Storager.GetBool(Defs.String_1, true);
		Defs.Boolean_0 = Storager.GetBool(Defs.String_2, true);
		Object.DontDestroyOnLoad(base.gameObject);
	}

	public void Play()
	{
		if (audioSource_0 != null)
		{
			StopMusic(audioSource_0);
			audioSource_0 = null;
		}
		if (!audioSource_1.isPlaying)
		{
			PlayMusic(audioSource_1);
		}
	}

	public void Stop()
	{
		if (audioSource_1.isPlaying)
		{
			StopMusic(audioSource_1);
		}
	}

	public void PlayBattleMusic(AudioSource audioSource_3)
	{
		if (audioSource_0 != null)
		{
			StopMusic(audioSource_0);
		}
		StopMusic(audioSource_1);
		audioSource_0 = audioSource_3;
		PlayMusic(audioSource_0);
	}

	public void StopBattleMusic()
	{
		if (audioSource_0 != null)
		{
			StopMusic(audioSource_0);
		}
		audioSource_0 = null;
	}

	public void ChangePlayMusicState(bool bool_1)
	{
		bool_0 = bool_1;
		if (bool_0)
		{
			if (audioSource_0 != null)
			{
				PlayMusic(audioSource_0);
			}
			else
			{
				PlayMusic(audioSource_1);
			}
		}
		else if (audioSource_0 != null)
		{
			StopMusic(audioSource_0);
		}
		else
		{
			StopMusic(audioSource_1);
		}
	}

	private void PlayMusic(AudioSource audioSource_3)
	{
		if (audioSource_3 == null || !Boolean_0 || audioSource_3.isPlaying)
		{
			return;
		}
		if (coroutine_0 != null)
		{
			StopCoroutine(coroutine_0);
			coroutine_0 = null;
			if (audioSource_2 != null)
			{
				audioSource_2.volume = float_0;
				audioSource_2 = null;
			}
		}
		coroutine_0 = StartCoroutine(PlayMusicInternal(audioSource_3));
	}

	private void StopMusic(AudioSource audioSource_3)
	{
		if (audioSource_3 == null || !audioSource_3.isPlaying)
		{
			return;
		}
		if (coroutine_0 != null)
		{
			StopCoroutine(coroutine_0);
			coroutine_0 = null;
			if (audioSource_2 != null)
			{
				audioSource_2.volume = float_0;
				audioSource_2 = null;
			}
		}
		coroutine_0 = StartCoroutine(StopMusicInternal(audioSource_3));
	}

	private IEnumerator PlayMusicInternal(AudioSource audioSource_3)
	{
		float volume = audioSource_3.volume;
		float_0 = audioSource_3.volume;
		audioSource_2 = audioSource_3;
		audioSource_3.volume = 0f;
		audioSource_3.Play();
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		float num = 0.5f;
		while (true)
		{
			if (!(Time.realtimeSinceStartup - realtimeSinceStartup <= num))
			{
				audioSource_3.volume = volume;
				audioSource_2 = null;
				yield break;
			}
			if (audioSource_3 == null)
			{
				break;
			}
			audioSource_3.volume = volume * (Time.realtimeSinceStartup - realtimeSinceStartup) / num;
			yield return null;
		}
		audioSource_2 = null;
	}

	private IEnumerator StopMusicInternal(AudioSource audioSource_3)
	{
		float volume = audioSource_3.volume;
		float_0 = audioSource_3.volume;
		audioSource_2 = audioSource_3;
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		float num = 0.5f;
		while (true)
		{
			if (!(Time.realtimeSinceStartup - realtimeSinceStartup <= num))
			{
				audioSource_3.volume = 0f;
				audioSource_3.Stop();
				audioSource_3.volume = volume;
				audioSource_2 = null;
				yield break;
			}
			if (audioSource_3 == null)
			{
				break;
			}
			audioSource_3.volume = volume * (1f - (Time.realtimeSinceStartup - realtimeSinceStartup) / num);
			yield return null;
		}
		audioSource_2 = null;
	}

	public void StopMenuMusicImmediately()
	{
		float volume = audioSource_1.volume;
		float_0 = audioSource_1.volume;
		audioSource_2 = audioSource_1;
		audioSource_1.volume = 0f;
		audioSource_1.Stop();
		audioSource_1.volume = volume;
		audioSource_2 = null;
	}

	private void OnApplicationFocus(bool bool_1)
	{
		/*if (!bool_0)
		{
			return;
		}
		if (!bool_1)
		{
			if (audioSource_0 != null)
			{
				float_1 = audioSource_0.volume;
				audioSource_0.volume = 0f;
			}
			else if (audioSource_1 != null)
			{
				float_1 = audioSource_1.volume;
				audioSource_1.volume = 0f;
			}
		}
		else if (audioSource_0 != null)
		{
			audioSource_0.volume = float_1;
		}
		else if (audioSource_1 != null)
		{
			audioSource_1.volume = float_1;
		}*/
	}
}
