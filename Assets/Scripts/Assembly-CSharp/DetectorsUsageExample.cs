using CodeStage.AntiCheat.Detectors;
using UnityEngine;

public class DetectorsUsageExample : MonoBehaviour
{
	public bool bool_0;

	public bool bool_1;

	public bool bool_2;

	public bool bool_3;

	private void Start()
	{
		SpeedHackDetector.StartDetection(OnSpeedHackDetected);
		InjectionDetector.StartDetection(OnInjectionDetected);
		ObscuredCheatingDetector.StartDetection(OnObscuredTypeCheatingDetected);
		ObscuredCheatingDetector.ObscuredCheatingDetector_0.autoDispose = true;
		ObscuredCheatingDetector.ObscuredCheatingDetector_0.keepAlive = true;
		WallHackDetector.StartDetection(OnWallHackDetected);
	}

	private void OnSpeedHackDetected()
	{
		bool_1 = true;
		Debug.LogWarning("Speed hack detected!");
	}

	private void OnInjectionDetected()
	{
		bool_0 = true;
		Debug.LogWarning("Injection detected!");
	}

	private void OnObscuredTypeCheatingDetected()
	{
		bool_2 = true;
		Debug.LogWarning("Obscured type cheating detected!");
	}

	private void OnWallHackDetected()
	{
		bool_3 = true;
		Debug.LogWarning("Wall hack detected!");
	}
}
