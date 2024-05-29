using UnityEngine;

namespace Photon
{
	public class MonoBehaviour : UnityEngine.MonoBehaviour
	{
		public PhotonView PhotonView_0
		{
			get
			{
				return PhotonView.Get(this);
			}
		}

		public PhotonView PhotonView_1
		{
			get
			{
				Debug.LogWarning("Why are you still using networkView? should be PhotonView?");
				return PhotonView.Get(this);
			}
		}
	}
}
