using UnityEngine;

public class PortalForPlayerController : MonoBehaviour
{
	public PortalForPlayerController myDublicatePortal;

	private Transform transform_0;

	private SoundFXChecker soundFXChecker_0;

	private void Awake()
	{
		soundFXChecker_0 = GetComponent<SoundFXChecker>();
	}

	private void Start()
	{
		transform_0 = base.transform.GetChild(0);
	}

	private void OnTriggerEnter(Collider collider_0)
	{
		if (myDublicatePortal == null)
		{
			return;
		}
		Transform transform = collider_0.gameObject.transform;
		if (transform.name.Equals("BodyCollider") && transform.parent != null && transform.parent.gameObject.Equals(WeaponManager.weaponManager_0.myPlayer))
		{
			PlayPortalSound();
			WeaponManager.weaponManager_0.myPlayer.transform.position = myDublicatePortal.transform_0.position;
			float y = transform_0.transform.rotation.eulerAngles.y;
			float num = myDublicatePortal.transform_0.transform.rotation.eulerAngles.y;
			if (num < y)
			{
				num += 360f;
			}
			float yAngle = num - y - 180f;
			WeaponManager.weaponManager_0.myPlayer.transform.Rotate(0f, yAngle, 0f);
			myDublicatePortal.PlayPortalSound();
		}
	}

	public void PlayPortalSound()
	{
		if (!(soundFXChecker_0 == null))
		{
			soundFXChecker_0.PlayOneShot();
		}
	}
}
