using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour {
	private static Globals _globals;
	public static Globals Instance
	{
		get
		{
			if (!_globals)
			{
				GameObject o = new GameObject("Globals");
				_globals = o.AddComponent<Globals>();
				DontDestroyOnLoad(o);
			}
			return _globals;
		}
	}
}
