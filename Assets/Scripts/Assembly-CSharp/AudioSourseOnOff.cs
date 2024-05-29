using UnityEngine;

public class AudioSourseOnOff : MonoBehaviour
{
	private AudioSource audioSource_0;

	private void Start()
	{
		audioSource_0 = GetComponent<AudioSource>();
		if (audioSource_0 != null)
		{
			audioSource_0.enabled = Defs.Boolean_0;
		}
	}

	private void OnEnable()
	{
		if (audioSource_0 != null && audioSource_0.enabled != Defs.Boolean_0)
		{
			audioSource_0.enabled = Defs.Boolean_0;
		}
	}
}
