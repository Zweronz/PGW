using UnityEngine;

namespace engine.unity
{
	public class MainBase : MonoBehaviour
	{
		public virtual void Start()
		{
			UnityThreadHelper.EnsureHelper();
			Object.DontDestroyOnLoad(this);
		}
	}
}
