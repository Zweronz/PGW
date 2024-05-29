using System.Reflection;
using Photon;
using UnityEngine;

public class PhotonView : Photon.MonoBehaviour
{
	public int int_0;

	public int int_1;

	public int int_2;

	protected internal bool bool_0;

	public int int_3 = -1;

	private object[] object_0;

	protected internal object[] object_1;

	protected internal object[] object_2;

	public Component component_0;

	public ViewSynchronization viewSynchronization_0;

	public OnSerializeTransform onSerializeTransform_0 = OnSerializeTransform.PositionAndRotation;

	public OnSerializeRigidBody onSerializeRigidBody_0 = OnSerializeRigidBody.All;

	public int int_4;

	protected internal bool bool_1;

	protected internal bool bool_2;

	private MethodInfo methodInfo_0;

	private bool bool_3;

	public int Int32_0
	{
		get
		{
			if (int_3 == -1 && PhotonNetwork.networkingPeer_0 != null)
			{
				int_3 = PhotonNetwork.networkingPeer_0.short_0;
			}
			return int_3;
		}
		set
		{
			int_3 = value;
		}
	}

	public object[] Object_0
	{
		get
		{
			if (!bool_1)
			{
				object_0 = PhotonNetwork.networkingPeer_0.FetchInstantiationData(int_4);
			}
			return object_0;
		}
		set
		{
			object_0 = value;
		}
	}

	public int Int32_1
	{
		get
		{
			return int_1 * PhotonNetwork.int_0 + int_0;
		}
		set
		{
			bool flag = bool_1 && int_0 == 0;
			int_1 = value / PhotonNetwork.int_0;
			int_0 = value % PhotonNetwork.int_0;
			if (flag)
			{
				PhotonNetwork.networkingPeer_0.RegisterPhotonView(this);
			}
		}
	}

	public bool Boolean_0
	{
		get
		{
			return int_1 == 0;
		}
	}

	public PhotonPlayer PhotonPlayer_0
	{
		get
		{
			return PhotonPlayer.Find(int_1);
		}
	}

	public int Int32_2
	{
		get
		{
			return int_1;
		}
	}

	public bool Boolean_1
	{
		get
		{
			return int_1 == PhotonNetwork.PhotonPlayer_0.Int32_0 || (Boolean_0 && PhotonNetwork.Boolean_9);
		}
	}

	protected internal void Awake()
	{
		PhotonNetwork.networkingPeer_0.RegisterPhotonView(this);
		object_0 = PhotonNetwork.networkingPeer_0.FetchInstantiationData(int_4);
		bool_1 = true;
	}

	protected internal void OnApplicationQuit()
	{
		bool_2 = true;
	}

	protected internal void OnDestroy()
	{
		if (!bool_2)
		{
			PhotonNetwork.networkingPeer_0.LocalCleanPhotonView(this);
		}
		if (!bool_2 && !Application.isLoadingLevel)
		{
			if (int_4 > 0)
			{
				Debug.LogError(string.Concat("OnDestroy() seems to be called without PhotonNetwork.Destroy()?! GameObject: ", base.gameObject, " Application.isLoadingLevel: ", Application.isLoadingLevel));
			}
			else if (Int32_1 <= 0)
			{
				Debug.LogWarning(string.Format("OnDestroy manually allocated PhotonView {0}. The viewID is 0. Was it ever (manually) set?", this));
			}
			else if (Boolean_1 && !PhotonNetwork.list_0.Contains(Int32_1))
			{
				Debug.LogWarning(string.Format("OnDestroy manually allocated PhotonView {0}. The viewID is local (isMine) but not in manuallyAllocatedViewIds list. Use UnAllocateViewID() after you destroyed the PV.", this));
			}
		}
		if (PhotonNetwork.networkingPeer_0.dictionary_4.ContainsKey(int_4))
		{
			GameObject gameObject = PhotonNetwork.networkingPeer_0.dictionary_4[int_4];
			bool flag;
			if (flag = gameObject == base.gameObject)
			{
				Debug.LogWarning(string.Format("OnDestroy for PhotonView {0} but GO is still in instantiatedObjects. instantiationId: {1}. Use PhotonNetwork.Destroy(). {2} Identical with this: {3} PN.Destroyed called for this PV: {4}", this, int_4, (!Application.isLoadingLevel) ? string.Empty : "Loading new scene caused this.", flag, bool_2));
			}
		}
	}

	protected internal void ExecuteOnSerialize(PhotonStream photonStream_0, PhotonMessageInfo photonMessageInfo_0)
	{
		if (!bool_3)
		{
			if (methodInfo_0 == null && !NetworkingPeer.GetMethod(component_0 as UnityEngine.MonoBehaviour, PhotonNetworkingMessage.OnPhotonSerializeView.ToString(), out methodInfo_0))
			{
				Debug.LogError("The observed monobehaviour (" + component_0.name + ") of this PhotonView does not implement OnPhotonSerializeView()!");
				bool_3 = true;
			}
			else
			{
				methodInfo_0.Invoke(component_0, new object[2] { photonStream_0, photonMessageInfo_0 });
			}
		}
	}

	public void RPC(string string_0, PhotonTargets photonTargets_0, params object[] object_3)
	{
		if (PhotonNetwork.networkingPeer_0.bool_1 && photonTargets_0 == PhotonTargets.MasterClient)
		{
			PhotonNetwork.RPC(this, string_0, PhotonNetwork.PhotonPlayer_1, object_3);
		}
		else
		{
			PhotonNetwork.RPC(this, string_0, photonTargets_0, object_3);
		}
	}

	public void RPC(string string_0, PhotonPlayer photonPlayer_0, params object[] object_3)
	{
		PhotonNetwork.RPC(this, string_0, photonPlayer_0, object_3);
	}

	public static PhotonView Get(Component component_1)
	{
		return component_1.GetComponent<PhotonView>();
	}

	public static PhotonView Get(GameObject gameObject_0)
	{
		return gameObject_0.GetComponent<PhotonView>();
	}

	public static PhotonView Find(int int_5)
	{
		return PhotonNetwork.networkingPeer_0.GetPhotonView(int_5);
	}

	public override string ToString()
	{
		return string.Format("View ({3}){0} on {1} {2}", Int32_1, (!(base.gameObject != null)) ? "GO==null" : base.gameObject.name, (!Boolean_0) ? string.Empty : "(scene)", Int32_0);
	}
}
