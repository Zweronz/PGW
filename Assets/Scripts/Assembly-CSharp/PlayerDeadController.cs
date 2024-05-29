using UnityEngine;

public sealed class PlayerDeadController : MonoBehaviour
{
	private float float_0 = -1f;

	private float float_1 = 4.8f;

	public bool isUseMine;

	private Transform transform_0;

	public Animation myAnimation;

	public GameObject[] playerDeads;

	public DeadEnergyController deadEnergyController;

	public DeadExplosionController deadExplosionController;

	private void Start()
	{
		transform_0 = base.transform;
		transform_0.position = new Vector3(-10000f, -10000f, -10000f);
	}

	private void TryPlayAudioClip(GameObject gameObject_0)
	{
		if (Defs.Boolean_0)
		{
			AudioSource component = gameObject_0.GetComponent<AudioSource>();
			if (!(component == null))
			{
				component.Play();
			}
		}
	}

	public void StartShow(Vector3 vector3_0, Quaternion quaternion_0, int int_0, bool bool_0, Texture texture_0)
	{
		isUseMine = bool_0;
		float_0 = float_1;
		transform_0.position = vector3_0;
		transform_0.rotation = quaternion_0;
		switch (int_0)
		{
		case 1:
			playerDeads[1].SetActive(true);
			TryPlayAudioClip(playerDeads[1]);
			deadExplosionController.StartAnim();
			break;
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
		case 7:
		case 8:
		case 9:
		{
			playerDeads[2].SetActive(true);
			TryPlayAudioClip(playerDeads[2]);
			Color color_ = new Color(0f, 0.5f, 1f);
			if (int_0 == 3)
			{
				color_ = new Color(1f, 0f, 0f);
			}
			if (int_0 == 4)
			{
				color_ = new Color(1f, 0f, 1f);
			}
			if (int_0 == 5)
			{
				color_ = new Color(0f, 0.5f, 1f);
			}
			if (int_0 == 6)
			{
				color_ = new Color(1f, 0.91f, 0f);
			}
			if (int_0 == 7)
			{
				color_ = new Color(0f, 0.85f, 0f);
			}
			if (int_0 == 8)
			{
				color_ = new Color(1f, 0.55f, 0f);
			}
			if (int_0 == 9)
			{
				color_ = new Color(1f, 1f, 1f);
			}
			deadEnergyController.StartAnim(color_, texture_0);
			break;
		}
		default:
			playerDeads[0].SetActive(true);
			break;
		}
	}

	private void Update()
	{
		if (!(float_0 < 0f))
		{
			float_0 -= Time.deltaTime;
			if (float_0 < 0f)
			{
				transform_0.position = new Vector3(-10000f, -10000f, -10000f);
				playerDeads[0].SetActive(false);
				playerDeads[1].SetActive(false);
				playerDeads[2].SetActive(false);
				isUseMine = false;
			}
		}
	}
}
