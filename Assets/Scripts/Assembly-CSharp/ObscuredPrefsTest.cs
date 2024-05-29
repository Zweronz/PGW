using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

public class ObscuredPrefsTest : MonoBehaviour
{
	private const string string_0 = "name";

	private const string string_1 = "money";

	private const string string_2 = "lifeBar";

	private const string string_3 = "gameComplete";

	private const string string_4 = "demoUint";

	private const string string_5 = "demoLong";

	private const string string_6 = "demoDouble";

	private const string string_7 = "demoVector3";

	private const string string_8 = "demoRect";

	private const string string_9 = "demoByteArray";

	public string encryptionKey = "change me!";

	internal string string_10 = string.Empty;

	private void OnApplicationQuit()
	{
		PlayerPrefs.DeleteKey("name");
		PlayerPrefs.DeleteKey("money");
		PlayerPrefs.DeleteKey("lifeBar");
		ObscuredPrefs.DeleteKey("name");
		ObscuredPrefs.DeleteKey("money");
		ObscuredPrefs.DeleteKey("lifeBar");
		ObscuredPrefs.DeleteKey("gameComplete");
		ObscuredPrefs.DeleteKey("demoUint");
		ObscuredPrefs.DeleteKey("demoLong");
		ObscuredPrefs.DeleteKey("demoDouble");
		ObscuredPrefs.DeleteKey("demoVector3");
		ObscuredPrefs.DeleteKey("demoRect");
		ObscuredPrefs.DeleteKey("demoByteArray");
	}

	private void Awake()
	{
		ObscuredPrefs.SetNewCryptoKey(encryptionKey);
	}

	public void SaveGame(bool bool_0)
	{
		if (bool_0)
		{
			ObscuredPrefs.SetString("name", "obscured focus oO");
			ObscuredPrefs.SetInt("money", 1500);
			ObscuredPrefs.SetFloat("lifeBar", 25.9f);
			ObscuredPrefs.SetBool("gameComplete", true);
			ObscuredPrefs.SetUInt("demoUint", 4294967290u);
			ObscuredPrefs.SetLong("demoLong", 3457657543456775432L);
			ObscuredPrefs.SetDouble("demoDouble", 345765.1312315678);
			ObscuredPrefs.SetRect("demoRect", new Rect(1f, 2f, 3f, 4f));
			ObscuredPrefs.SetVector3("demoVector3", new Vector3(123.312f, 453.12344f, 1223f));
			ObscuredPrefs.SetByteArray("demoByteArray", new byte[4] { 44, 104, 43, 32 });
			Debug.Log("Game saved using ObscuredPrefs. Try to find and change saved data now! ;)");
		}
		else
		{
			PlayerPrefs.SetString("name", "focus :D");
			PlayerPrefs.SetInt("money", 2100);
			PlayerPrefs.SetFloat("lifeBar", 88.4f);
			Debug.Log("Game saved with regular PlayerPrefs. Try to find and change saved data now (it's easy)!");
		}
		ObscuredPrefs.Save();
	}

	public void ReadSavedGame(bool bool_0)
	{
		if (bool_0)
		{
			byte[] byteArray = ObscuredPrefs.GetByteArray("demoByteArray", 0, 4);
			string_10 = "Name: " + ObscuredPrefs.GetString("name") + "\n";
			string text = string_10;
			string_10 = text + "Money: " + ObscuredPrefs.GetInt("money") + "\n";
			text = string_10;
			string_10 = text + "Life bar: " + ObscuredPrefs.GetFloat("lifeBar") + "\n";
			text = string_10;
			string_10 = text + "bool: " + ObscuredPrefs.GetBool("gameComplete") + "\n";
			text = string_10;
			string_10 = text + "uint: " + ObscuredPrefs.GetUInt("demoUint") + "\n";
			text = string_10;
			string_10 = text + "long: " + ObscuredPrefs.GetLong("demoLong") + "\n";
			text = string_10;
			string_10 = text + "double: " + ObscuredPrefs.GetDouble("demoDouble") + "\n";
			text = string_10;
			string_10 = string.Concat(text, "Vector3: ", ObscuredPrefs.GetVector3("demoVector3"), "\n");
			text = string_10;
			string_10 = string.Concat(text, "Rect: ", ObscuredPrefs.GetRect("demoRect"), "\n");
			text = string_10;
			string_10 = text + "byte[]: {" + byteArray[0] + "," + byteArray[1] + "," + byteArray[2] + "," + byteArray[3] + "}";
		}
		else
		{
			string_10 = "Name: " + PlayerPrefs.GetString("name") + "\n";
			string text = string_10;
			string_10 = text + "Money: " + PlayerPrefs.GetInt("money") + "\n";
			string_10 = string_10 + "Life bar: " + PlayerPrefs.GetFloat("lifeBar");
		}
	}
}
