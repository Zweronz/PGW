using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;

[Serializable]
public class ServerSettings : ScriptableObject
{
	public enum HostingOption
	{
		NotSet = 0,
		PhotonCloud = 1,
		SelfHosted = 2,
		OfflineMode = 3,
		BestRegion = 4
	}

	public HostingOption HostType;

	public ConnectionProtocol Protocol;

	public string ServerAddress = string.Empty;

	public int ServerPort = 5055;

	public CloudRegionCode PreferredRegion;

	public string AppID = string.Empty;

	public bool PingCloudServersOnAwake;

	public List<string> RpcList = new List<string>();

	public bool bool_0;

	public void UseCloudBestResion(string string_0)
	{
		HostType = HostingOption.BestRegion;
		AppID = string_0;
	}

	public void UseCloud(string string_0)
	{
		HostType = HostingOption.PhotonCloud;
		AppID = string_0;
	}

	public void UseCloud(string string_0, CloudRegionCode cloudRegionCode_0)
	{
		HostType = HostingOption.PhotonCloud;
		AppID = string_0;
		PreferredRegion = cloudRegionCode_0;
	}

	public void UseMyServer(string string_0, int int_0, string string_1)
	{
		HostType = HostingOption.SelfHosted;
		AppID = ((string_1 == null) ? "master" : string_1);
		ServerAddress = string_0;
		ServerPort = int_0;
	}

	public override string ToString()
	{
		return string.Concat("ServerSettings: ", HostType, " ", ServerAddress);
	}
}
