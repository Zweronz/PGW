using System;
using UnityEngine;

namespace Rilisoft
{
	internal sealed class EscapeInput : MonoBehaviour
	{
		private static bool bool_0;

		private bool bool_1;

		private static EscapeInput escapeInput_0;

		public static EscapeInput EscapeInput_0
		{
			get
			{
				if (bool_0)
				{
					throw new ObjectDisposedException(typeof(EscapeInput).Name);
				}
				if (escapeInput_0 == null)
				{
					escapeInput_0 = UnityEngine.Object.FindObjectOfType<EscapeInput>();
					if (escapeInput_0 == null)
					{
						GameObject gameObject = new GameObject(typeof(EscapeInput).Name + " Singleton");
						UnityEngine.Object.DontDestroyOnLoad(gameObject);
						gameObject.SetActive(true);
						escapeInput_0 = gameObject.AddComponent<EscapeInput>();
					}
				}
				return escapeInput_0;
			}
		}

		public bool Boolean_0
		{
			get
			{
				return bool_1;
			}
		}

		private EscapeInput()
		{
		}

		private void DoUpdate()
		{
			bool_1 = Input.GetKeyUp(KeyCode.Escape);
		}

		public void Reset()
		{
			bool_1 = false;
		}

		private void OnDestroy()
		{
			bool_0 = true;
		}

		private void Start()
		{
			bool_1 = Input.GetKeyUp(KeyCode.Escape);
		}

		private void Update()
		{
			DoUpdate();
		}
	}
}
