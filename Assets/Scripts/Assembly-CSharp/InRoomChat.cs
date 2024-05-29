using System.Collections.Generic;
using Photon;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class InRoomChat : Photon.MonoBehaviour
{
	public Rect rect_0 = new Rect(0f, 0f, 250f, 300f);

	public bool bool_0 = true;

	public bool bool_1;

	public List<string> list_0 = new List<string>();

	private string string_0 = string.Empty;

	private Vector2 vector2_0 = Vector2.zero;

	public static readonly string string_1 = "Chat";

	public void Start()
	{
		if (bool_1)
		{
			rect_0.y = (float)Screen.height - rect_0.height;
		}
	}

	public void OnGUI()
	{
		if (!bool_0 || PhotonNetwork.PeerState_0 != PeerState.Joined)
		{
			return;
		}
		if (Event.current.type == EventType.KeyDown && (Event.current.keyCode == KeyCode.KeypadEnter || Event.current.keyCode == KeyCode.Return))
		{
			if (!string.IsNullOrEmpty(string_0))
			{
				base.PhotonView_0.RPC("Chat", PhotonTargets.All, string_0);
				string_0 = string.Empty;
				GUI.FocusControl(string.Empty);
				return;
			}
			GUI.FocusControl("ChatInput");
		}
		GUI.SetNextControlName(string.Empty);
		GUILayout.BeginArea(rect_0);
		vector2_0 = GUILayout.BeginScrollView(vector2_0);
		GUILayout.FlexibleSpace();
		for (int num = list_0.Count - 1; num >= 0; num--)
		{
			GUILayout.Label(list_0[num]);
		}
		GUILayout.EndScrollView();
		GUILayout.BeginHorizontal();
		GUI.SetNextControlName("ChatInput");
		string_0 = GUILayout.TextField(string_0);
		if (GUILayout.Button("Send", GUILayout.ExpandWidth(false)))
		{
			base.PhotonView_0.RPC("Chat", PhotonTargets.All, string_0);
			string_0 = string.Empty;
			GUI.FocusControl(string.Empty);
		}
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}

	[RPC]
	public void Chat(string string_2, PhotonMessageInfo photonMessageInfo_0)
	{
		string text = "anonymous";
		if (photonMessageInfo_0 != null && photonMessageInfo_0.photonPlayer_0 != null)
		{
			text = (string.IsNullOrEmpty(photonMessageInfo_0.photonPlayer_0.String_0) ? ("player " + photonMessageInfo_0.photonPlayer_0.Int32_0) : photonMessageInfo_0.photonPlayer_0.String_0);
		}
		list_0.Add(text + ": " + string_2);
	}

	public void AddLine(string string_2)
	{
		list_0.Add(string_2);
	}
}
