using UnityEngine;

internal sealed class BackgroundMusicController : MonoBehaviour
{
	private void Start()
	{
		MenuBackgroundMusic.menuBackgroundMusic_0.PlayBattleMusic(base.GetComponent<AudioSource>());
	}

	private void onDestroy()
	{
		MenuBackgroundMusic.menuBackgroundMusic_0.StopBattleMusic();
	}
}
