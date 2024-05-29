using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

public class ActTesterGUI : MonoBehaviour
{
	public ObscuredInt dummyObscuredInt = 1234;

	public ObscuredFloat dummyObscuredFloat = 5678f;

	public ObscuredString dummyObscuredString = "dummy obscured string";

	public ObscuredBool dummyObscuredBool = true;

	private bool bool_0;

	private int int_0;

	private bool bool_1;

	private ObscuredVector3Test obscuredVector3Test_0;

	private ObscuredFloatTest obscuredFloatTest_0;

	private ObscuredIntTest obscuredIntTest_0;

	private ObscuredStringTest obscuredStringTest_0;

	private ObscuredPrefsTest obscuredPrefsTest_0;

	private DetectorsUsageExample detectorsUsageExample_0;

	private void Awake()
	{
		ObscuredPrefs.action_0 = SavesAlterationDetected;
		ObscuredPrefs.action_1 = ForeignSavesDetected;
		obscuredVector3Test_0 = GetComponent<ObscuredVector3Test>();
		obscuredFloatTest_0 = GetComponent<ObscuredFloatTest>();
		obscuredIntTest_0 = GetComponent<ObscuredIntTest>();
		obscuredStringTest_0 = GetComponent<ObscuredStringTest>();
		obscuredPrefsTest_0 = GetComponent<ObscuredPrefsTest>();
		detectorsUsageExample_0 = Object.FindObjectOfType<DetectorsUsageExample>();
	}

	private void SavesAlterationDetected()
	{
		bool_0 = true;
	}

	private void ForeignSavesDetected()
	{
		bool_1 = true;
	}

	private void OnGUI()
	{
		GUILayout.BeginHorizontal();
		GUILayout.BeginVertical();
		CenteredLabel("Memory cheating protection");
		GUILayout.Space(10f);
		if ((bool)obscuredStringTest_0 && obscuredStringTest_0.enabled)
		{
			if (GUILayout.Button("Use regular string"))
			{
				obscuredStringTest_0.UseRegular();
			}
			if (GUILayout.Button("Use obscured string"))
			{
				obscuredStringTest_0.UseObscured();
			}
			string text = ((!obscuredStringTest_0.bool_0) ? ((string)obscuredStringTest_0.obscuredString_0) : obscuredStringTest_0.string_0);
			GUILayout.Label("Current string (try to change it!):\n" + text);
		}
		if ((bool)obscuredIntTest_0 && obscuredIntTest_0.enabled)
		{
			GUILayout.Space(10f);
			if (GUILayout.Button("Use regular int (click to generate new number)"))
			{
				obscuredIntTest_0.UseRegular();
			}
			if (GUILayout.Button("Use ObscuredInt (click to generate new number)"))
			{
				obscuredIntTest_0.UseObscured();
			}
			int num = ((!obscuredIntTest_0.bool_0) ? ((int)obscuredIntTest_0.obscuredInt_0) : obscuredIntTest_0.int_0);
			GUILayout.Label("Current lives count (try to change them!):\n" + num);
		}
		GUILayout.BeginHorizontal();
		GUILayout.Label("ObscuredInt from inspector: " + dummyObscuredInt);
		if (GUILayout.Button("+"))
		{
			++dummyObscuredInt;
		}
		if (GUILayout.Button("-"))
		{
			--dummyObscuredInt;
		}
		GUILayout.EndHorizontal();
		if ((bool)obscuredFloatTest_0 && obscuredFloatTest_0.enabled)
		{
			GUILayout.Space(10f);
			if (GUILayout.Button("Use regular float (click to generate new number)"))
			{
				obscuredFloatTest_0.UseRegular();
			}
			if (GUILayout.Button("Use ObscuredFloat (click to generate new number)"))
			{
				obscuredFloatTest_0.UseObscured();
			}
			float num2 = ((!obscuredFloatTest_0.bool_0) ? ((float)obscuredFloatTest_0.obscuredFloat_0) : obscuredFloatTest_0.float_0);
			GUILayout.Label("Current health bar (try to change it!):\n" + string.Format("{0:0.000}", num2));
		}
		if ((bool)obscuredVector3Test_0 && obscuredVector3Test_0.enabled)
		{
			GUILayout.Space(10f);
			if (GUILayout.Button("Use regular Vector3 (click to generate new one)"))
			{
				obscuredVector3Test_0.UseRegular();
			}
			if (GUILayout.Button("Use ObscuredVector3 (click to generate new one)"))
			{
				obscuredVector3Test_0.UseObscured();
			}
			Vector3 vector = ((!obscuredVector3Test_0.bool_0) ? ((Vector3)obscuredVector3Test_0.obscuredVector3_0) : obscuredVector3Test_0.vector3_0);
			GUILayout.Label("Current player position (try to change it!):\n" + vector);
		}
		GUILayout.Space(10f);
		GUILayout.EndVertical();
		GUILayout.Space(10f);
		GUILayout.BeginVertical();
		CenteredLabel("Saves cheating protection");
		GUILayout.Space(10f);
		if ((bool)obscuredPrefsTest_0 && obscuredPrefsTest_0.enabled)
		{
			if (GUILayout.Button("Save game with regular PlayerPrefs!"))
			{
				obscuredPrefsTest_0.SaveGame(false);
			}
			if (GUILayout.Button("Read data saved with regular PlayerPrefs"))
			{
				obscuredPrefsTest_0.ReadSavedGame(false);
			}
			GUILayout.Space(10f);
			if (GUILayout.Button("Save game with ObscuredPrefs!"))
			{
				obscuredPrefsTest_0.SaveGame(true);
			}
			if (GUILayout.Button("Read data saved with ObscuredPrefs"))
			{
				obscuredPrefsTest_0.ReadSavedGame(true);
			}
			ObscuredPrefs.bool_1 = GUILayout.Toggle(ObscuredPrefs.bool_1, "preservePlayerPrefs");
			ObscuredPrefs.bool_3 = GUILayout.Toggle(ObscuredPrefs.bool_3, "emergencyMode");
			GUILayout.Label("LockToDevice level:");
			int_0 = GUILayout.SelectionGrid(int_0, new string[3]
			{
				ObscuredPrefs.DeviceLockLevel.None.ToString(),
				ObscuredPrefs.DeviceLockLevel.Soft.ToString(),
				ObscuredPrefs.DeviceLockLevel.Strict.ToString()
			}, 3);
			ObscuredPrefs.deviceLockLevel_0 = (ObscuredPrefs.DeviceLockLevel)int_0;
			ObscuredPrefs.bool_2 = GUILayout.Toggle(ObscuredPrefs.bool_2, "readForeignSaves");
			GUILayout.Label("PlayerPrefs: \n" + obscuredPrefsTest_0.string_10);
			if (bool_0)
			{
				GUILayout.Label("Saves were altered! }:>");
			}
			if (bool_1)
			{
				GUILayout.Label("Saves more likely from another device! }:>");
			}
		}
		if (detectorsUsageExample_0 != null)
		{
			GUILayout.Label("Speed hack detected: " + detectorsUsageExample_0.bool_1);
			GUILayout.Label("Injection detected: " + detectorsUsageExample_0.bool_0);
			GUILayout.Label("Obscured type cheating detected: " + detectorsUsageExample_0.bool_2);
			GUILayout.Label("Wall hack detected: " + detectorsUsageExample_0.bool_3);
		}
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
	}

	private void CenteredLabel(string string_0)
	{
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.Label(string_0);
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
	}
}
