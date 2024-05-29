using UnityEngine;

namespace Rilisoft
{
	internal sealed class Invitation : MonoBehaviour
	{
		public UILabel nm;

		public GameObject accept;

		public GameObject reject;

		public GameObject JoinClan;

		public GameObject RejectClan;

		public GameObject youAlready;

		public UISprite rank;

		public string id;

		public string recordId;

		private float float_0;

		public bool outgoing;

		public bool IsClanInv;

		public UITexture ClanLogo;

		public string clanLogoString;

		private float float_1;

		private void Start()
		{
			float_1 = float.PositiveInfinity;
			_UpdateInfo();
			if (ClanLogo != null)
			{
				ClanLogo.gameObject.SetActive(IsClanInv);
			}
			if (accept != null)
			{
				accept.SetActive(!IsClanInv);
			}
			reject.SetActive(!IsClanInv);
			rank.gameObject.SetActive(!IsClanInv);
		}

		public void KeepClanData()
		{
		}

		private void _UpdateInfo()
		{
		}

		public void DisableButtons()
		{
			if (accept != null)
			{
				accept.SetActive(false);
			}
			reject.SetActive(false);
			float_1 = Time.realtimeSinceStartup;
			if (JoinClan != null)
			{
				JoinClan.SetActive(false);
			}
			if (RejectClan != null)
			{
				RejectClan.SetActive(false);
			}
		}

		private void Update()
		{
			if (Time.realtimeSinceStartup - float_1 > 15f)
			{
				float_1 = float.PositiveInfinity;
				if (accept != null)
				{
					accept.SetActive(true);
				}
				reject.SetActive(true);
			}
			if (Time.realtimeSinceStartup - float_0 > 1f)
			{
				float_0 = Time.realtimeSinceStartup;
				_UpdateInfo();
			}
		}
	}
}
