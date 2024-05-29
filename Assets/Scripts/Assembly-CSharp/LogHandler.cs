using System.Collections;
using UnityEngine;

internal sealed class LogHandler : MonoBehaviour
{
	private bool bool_0;

	private bool bool_1;

	private string string_0 = string.Empty;

	private string string_1 = string.Empty;

	private void Start()
	{
		Object.Destroy(base.gameObject);
	}

	private void OnEnable()
	{
		StartCoroutine(RegisterLogCallbackCoroutine());
	}

	private void OnDisable()
	{
		bool_0 = true;
		if (bool_1)
		{
			Application.RegisterLogCallback(null);
		}
	}

	private IEnumerator RegisterLogCallbackCoroutine()
	{
		yield return new WaitForSeconds(0.5f);
		if (!bool_0)
		{
			Application.RegisterLogCallback(HandleLog);
			bool_1 = true;
		}
	}

	private void HandleLog(string string_2, string string_3, LogType logType_0)
	{
		if (logType_0 == LogType.Exception)
		{
			string_0 = string_2;
			string_1 = string_3;
		}
	}
}
