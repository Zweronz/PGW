using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundFXChecker : MonoBehaviour
{
	public bool playOnEnable = true;

	public AudioClip audioClip;

	private AudioSource audioSource_0;

	private void Awake()
	{
		audioSource_0 = GetComponent<AudioSource>();
	}

	private void OnEnable()
	{
		if (playOnEnable)
		{
			PlayOneShot();
		}
	}

	public void PlayOneShot()
	{
		if (!(audioSource_0 == null) && !(audioClip == null) && Defs.Boolean_0)
		{
			audioSource_0.PlayOneShot(audioClip);
		}
	}
}
