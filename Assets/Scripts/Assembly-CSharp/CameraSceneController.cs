using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraSceneController : MonoBehaviour
{
	public enum States
	{
		None = 0,
		KillKam = 1,
		LevelUp = 2
	}

	public RPG_Camera killCamController;

	private Vector3 vector3_0 = new Vector3(17f, 11f, 17f);

	private Quaternion quaternion_0 = Quaternion.Euler(new Vector3(39f, 226f, 0f));

	private Transform transform_0;

	[CompilerGenerated]
	private static CameraSceneController cameraSceneController_0;

	public static CameraSceneController CameraSceneController_0
	{
		[CompilerGenerated]
		get
		{
			return cameraSceneController_0;
		}
		[CompilerGenerated]
		private set
		{
			cameraSceneController_0 = value;
		}
	}

	private void Awake()
	{
		CameraSceneController_0 = this;
		transform_0 = base.transform;
		transform_0.position = vector3_0;
		transform_0.rotation = quaternion_0;
		killCamController.enabled = false;
	}

	private void Start()
	{
		CameraSceneController_0.SetCameraPositionHardcode();
		CameraSceneController_0.transform_0.position = CameraSceneController_0.vector3_0;
		CameraSceneController_0.transform_0.rotation = CameraSceneController_0.quaternion_0;
	}

	private void OnDestroy()
	{
		CameraSceneController_0 = null;
	}

	public static void SetState(States states_0, Transform transform_1 = null)
	{
		if (!(CameraSceneController_0 == null))
		{
			bool flag = states_0 == States.KillKam && transform_1 != null;
			CameraSceneController_0.killCamController.enabled = flag;
			CameraSceneController_0.killCamController.cameraPivot = transform_1;
			CameraSceneController_0.killCamController.lastDistance = 1f;
			if (flag)
			{
				CameraSceneController_0.transform_0.position = transform_1.position;
				CameraSceneController_0.transform_0.rotation = transform_1.rotation;
			}
			else
			{
				CameraSceneController_0.SetCameraPositionHardcode();
				CameraSceneController_0.transform_0.position = CameraSceneController_0.vector3_0;
				CameraSceneController_0.transform_0.rotation = CameraSceneController_0.quaternion_0;
			}
			bool flag2 = states_0 == States.LevelUp;
			GrayscaleEffect component = CameraSceneController_0.GetComponent<GrayscaleEffect>();
			if (component != null)
			{
				component.enabled = flag2;
			}
		}
	}

	public static void UpdateMouse()
	{
		if (!(CameraSceneController_0 == null))
		{
			CameraSceneController_0.killCamController.UpdateMouseX();
		}
	}

	private void SetCameraPositionHardcode()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("CameraPositionZone");
		if (gameObject != null)
		{
			vector3_0 = gameObject.transform.position;
			quaternion_0 = gameObject.transform.rotation;
		}
		else if (Application.loadedLevelName.Equals("PiratIsland"))
		{
			vector3_0 = new Vector3(32.84f, 17.18f, 43.5f);
			quaternion_0 = Quaternion.Euler(new Vector3(3.541031f, -120.9944f, 0.3762207f));
		}
		else if (Application.loadedLevelName.Equals("Area52Labs"))
		{
			vector3_0 = new Vector3(-60f, 21.53f, 14.56f);
			quaternion_0 = Quaternion.Euler(new Vector3(15f, 118.2999f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Mine"))
		{
			vector3_0 = new Vector3(-29.63f, 11.26f, -44.48f);
			quaternion_0 = Quaternion.Euler(new Vector3(19.67534f, 30.00002f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Barge"))
		{
			vector3_0 = new Vector3(12.6f, 3.97f, 8.02f);
			quaternion_0 = Quaternion.Euler(new Vector3(22.97f, -131.3437f, 4.19f));
		}
		else if (Application.loadedLevelName.Equals("Pizza"))
		{
			vector3_0 = new Vector3(12.6f, 3.97f, 8.02f);
			quaternion_0 = Quaternion.Euler(new Vector3(22.97f, -131.3437f, 4.19f));
		}
		else if (Application.loadedLevelName.Equals("Bota"))
		{
			vector3_0 = new Vector3(-60f, 21.53f, 14.56f);
			quaternion_0 = Quaternion.Euler(new Vector3(15f, 118.2999f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Paradise"))
		{
			vector3_0 = new Vector3(18.48f, 0.54f, 19.47f);
			quaternion_0 = Quaternion.Euler(new Vector3(-6.089722f, 132.2656f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Day_D"))
		{
			vector3_0 = new Vector3(31.15712f, 7.614257f, -4.818801f);
			quaternion_0 = Quaternion.Euler(new Vector3(8.619919f, -123.1408f, 0f));
		}
		else if (Application.loadedLevelName.Equals("NuclearCity"))
		{
			vector3_0 = new Vector3(-27.71493f, 9.460411f, -2.671694f);
			quaternion_0 = Quaternion.Euler(new Vector3(12.17598f, 83.05725f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Cube"))
		{
			vector3_0 = new Vector3(-14.35343f, 12.65811f, 14.04167f);
			quaternion_0 = Quaternion.Euler(new Vector3(36.88519f, 135f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Train"))
		{
			vector3_0 = new Vector3(21f, 16f, -12f);
			quaternion_0 = Quaternion.Euler(new Vector3(25f, -60f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Sniper"))
		{
			vector3_0 = new Vector3(20f, 10f, 25f);
			quaternion_0 = Quaternion.Euler(new Vector3(3f, -41f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Supermarket"))
		{
			vector3_0 = new Vector3(75f, 7f, -30f);
			quaternion_0 = Quaternion.Euler(new Vector3(3f, -75f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Pumpkins"))
		{
			vector3_0 = new Vector3(-14f, 14f, 19f);
			quaternion_0 = Quaternion.Euler(new Vector3(16f, 126f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Christmas_Town"))
		{
			vector3_0 = new Vector3(-14f, 14f, 19f);
			quaternion_0 = Quaternion.Euler(new Vector3(16f, 126f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Christmas_Town_Night"))
		{
			vector3_0 = new Vector3(-14f, 14f, 19f);
			quaternion_0 = Quaternion.Euler(new Vector3(16f, 126f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Maze"))
		{
			vector3_0 = new Vector3(23f, 5.25f, -20.5f);
			quaternion_0 = Quaternion.Euler(new Vector3(33f, -50f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Cementery"))
		{
			vector3_0 = new Vector3(17f, 11f, 17f);
			quaternion_0 = Quaternion.Euler(new Vector3(39f, 226f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Hospital"))
		{
			vector3_0 = new Vector3(9.5f, 3.2f, 9.5f);
			quaternion_0 = Quaternion.Euler(new Vector3(25f, -140f, 0f));
		}
		else if (Application.loadedLevelName.Equals("City"))
		{
			vector3_0 = new Vector3(17f, 11f, 17f);
			quaternion_0 = Quaternion.Euler(new Vector3(39f, 226f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Jail"))
		{
			vector3_0 = new Vector3(13.5f, 2.9f, 3.1f);
			quaternion_0 = Quaternion.Euler(new Vector3(11f, -66f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Gluk"))
		{
			vector3_0 = new Vector3(17f, 11f, 17f);
			quaternion_0 = Quaternion.Euler(new Vector3(39f, 226f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Pool"))
		{
			vector3_0 = new Vector3(-17.36495f, 5.448204f, -5.605346f);
			quaternion_0 = Quaternion.Euler(new Vector3(31.34471f, 31.34471f, 0.2499542f));
		}
		else if (Application.loadedLevelName.Equals("Slender"))
		{
			vector3_0 = new Vector3(31.82355f, 5.959687f, 37.378f);
			quaternion_0 = Quaternion.Euler(new Vector3(36.08264f, -110.1159f, 2.307983f));
		}
		else if (Application.loadedLevelName.Equals("Castle"))
		{
			vector3_0 = new Vector3(-12.3107f, 4.9f, 0.2716838f);
			quaternion_0 = Quaternion.Euler(new Vector3(26.89935f, 89.99986f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Bridge"))
		{
			vector3_0 = new Vector3(-14.22702f, 14.6011f, -74.93485f);
			quaternion_0 = Quaternion.Euler(new Vector3(24.68127f, -151.4293f, 0.2789154f));
		}
		else if (Application.loadedLevelName.Equals("Farm"))
		{
			vector3_0 = new Vector3(22.4933f, 16.03175f, -35.17904f);
			quaternion_0 = Quaternion.Euler(new Vector3(29.99995f, -28.62347f, 0f));
		}
		else if (Application.loadedLevelName.Equals("School"))
		{
			vector3_0 = new Vector3(-19.52079f, 2.868755f, -19.50274f);
			quaternion_0 = Quaternion.Euler(new Vector3(14.96701f, 40.79106f, 1.266037f));
		}
		else if (Application.loadedLevelName.Equals("Sky_islands"))
		{
			vector3_0 = new Vector3(-3.111776f, 21.94557f, 25.31594f);
			quaternion_0 = Quaternion.Euler(new Vector3(41.94537f, -143.1731f, 6.383652f));
		}
		else if (Application.loadedLevelName.Equals("Dust"))
		{
			vector3_0 = new Vector3(-12.67253f, 6.92115f, 28.89415f);
			quaternion_0 = Quaternion.Euler(new Vector3(28.46265f, 147.2818f, 0.2389221f));
		}
		else if (Application.loadedLevelName.Equals("Utopia"))
		{
			vector3_0 = new Vector3(-10.62854f, 10.01794f, -51.20456f);
			quaternion_0 = Quaternion.Euler(new Vector3(13.26845f, 16.31204f, 1.440735f));
		}
		else if (Application.loadedLevelName.Equals("Assault"))
		{
			vector3_0 = new Vector3(19.36158f, 19.61019f, -24.24763f);
			quaternion_0 = Quaternion.Euler(new Vector3(35.9299f, -11.80757f, -1.581451f));
		}
		else if (Application.loadedLevelName.Equals("Aztec"))
		{
			vector3_0 = new Vector3(-6.693532f, 11.69715f, 24.21659f);
			quaternion_0 = Quaternion.Euler(new Vector3(41.08192f, 134.5497f, -1.380188f));
		}
		else if (Application.loadedLevelName.Equals("Parkour"))
		{
			vector3_0 = new Vector3(-7.352654f, 113.1507f, -29.85653f);
			quaternion_0 = Quaternion.Euler(new Vector3(11.99559f, -16.57709f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Coliseum_MP"))
		{
			vector3_0 = new Vector3(14.32691f, 9.814805f, -20.59482f);
			quaternion_0 = Quaternion.Euler(new Vector3(11.60112f, -34.35773f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Hungry"))
		{
			vector3_0 = new Vector3(-17.00313f, 26.73884f, 25.21794f);
			quaternion_0 = Quaternion.Euler(new Vector3(45f, 133.77f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Hungry_Night"))
		{
			vector3_0 = new Vector3(-17.00313f, 26.73884f, 25.21794f);
			quaternion_0 = Quaternion.Euler(new Vector3(45f, 133.77f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Estate"))
		{
			vector3_0 = new Vector3(-10.54591f, 12.52175f, 54.25265f);
			quaternion_0 = Quaternion.Euler(new Vector3(20.6673f, 147.7978f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Ranch"))
		{
			vector3_0 = new Vector3(6.929729f, 11.63932f, -12.79686f);
			quaternion_0 = Quaternion.Euler(new Vector3(32.05518f, -26.4068f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Space"))
		{
			vector3_0 = new Vector3(-26.34445f, 12.08921f, 50.52678f);
			quaternion_0 = Quaternion.Euler(new Vector3(8.542023f, 144.9284f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Hungry_2"))
		{
			vector3_0 = new Vector3(-14.88988f, 13.45897f, -13.3518f);
			quaternion_0 = Quaternion.Euler(new Vector3(38.7506f, 63.3826f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Portal"))
		{
			vector3_0 = new Vector3(-12.11895f, 13.50075f, 39.97712f);
			quaternion_0 = Quaternion.Euler(new Vector3(22.95538f, 147.1387f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Two_Castles"))
		{
			vector3_0 = new Vector3(-15.43661f, 1.870193f, -30.82652f);
			quaternion_0 = Quaternion.Euler(new Vector3(-17.48395f, -89.99988f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Ships"))
		{
			vector3_0 = new Vector3(-14.70674f, 30.88849f, 31.48288f);
			quaternion_0 = Quaternion.Euler(new Vector3(24.59309f, 119.2478f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Ships_Night"))
		{
			vector3_0 = new Vector3(-14.70674f, 30.88849f, 31.48288f);
			quaternion_0 = Quaternion.Euler(new Vector3(24.59309f, 119.2478f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Gluk_3"))
		{
			vector3_0 = new Vector3(11.4502f, 20.29328f, 19.8833f);
			quaternion_0 = Quaternion.Euler(new Vector3(32.95062f, -149.9998f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Matrix"))
		{
			vector3_0 = new Vector3(11.4502f, 20.29328f, 19.8833f);
			quaternion_0 = Quaternion.Euler(new Vector3(32.95062f, -149.9998f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Ants"))
		{
			vector3_0 = new Vector3(-5.627228f, 20.49741f, 15.93793f);
			quaternion_0 = Quaternion.Euler(new Vector3(26.42534f, 149.9999f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Hill"))
		{
			vector3_0 = new Vector3(43.99298f, 18.35728f, 44.65937f);
			quaternion_0 = Quaternion.Euler(new Vector3(14.39806f, -135f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Heaven"))
		{
			vector3_0 = new Vector3(0.8211896f, 22.78858f, 22.34459f);
			quaternion_0 = Quaternion.Euler(new Vector3(14.99997f, -180f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Underwater"))
		{
			vector3_0 = new Vector3(17.0383f, 16.49174f, -22.72179f);
			quaternion_0 = Quaternion.Euler(new Vector3(11.31189f, -130.5916f, 0f));
		}
		else if (Application.loadedLevelName.Equals("Knife"))
		{
			vector3_0 = new Vector3(-8.960545f, 10.92595f, 28.56895f);
			quaternion_0 = Quaternion.Euler(new Vector3(-8.730835f, 14.99998f, 0f));
		}
	}
}
