using System;
using System.Collections;
using System.Diagnostics;
using System.Net;
using ExitGames.Client.Photon;
using UnityEngine;

public class PhotonPingManager
{
	public bool UseNative;

	public static int int_0 = 5;

	public static bool bool_0 = true;

	public static int int_1 = 800;

	private int int_2;

	public Region Region_0
	{
		get
		{
			Region result = null;
			int num = int.MaxValue;
			foreach (Region item in PhotonNetwork.networkingPeer_0.List_0)
			{
				UnityEngine.Debug.Log("BestRegion checks region: " + item);
				if (item.int_0 != 0 && item.int_0 < num)
				{
					num = item.int_0;
					result = item;
				}
			}
			return result;
		}
	}

	public bool Boolean_0
	{
		get
		{
			return int_2 == 0;
		}
	}

	public IEnumerator PingSocket(Region region_0)
	{
		region_0.int_0 = int_0 * int_1;
		int_2++;
		PhotonPing photonPing;
		if (PhotonHandler.type_0 == typeof(PingNativeDynamic))
		{
			UnityEngine.Debug.Log("Using constructor for new PingNativeDynamic()");
			photonPing = new PingNativeDynamic();
		}
		else
		{
			photonPing = (PhotonPing)Activator.CreateInstance(PhotonHandler.type_0);
		}
		float num = 0f;
		int num2 = 0;
		string text = region_0.string_0;
		int num3 = text.LastIndexOf(':');
		if (num3 > 1)
		{
			text = text.Substring(0, num3);
		}
		text = ResolveHost(text);
		for (int i = 0; i < int_0; i++)
		{
			bool flag = false;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			try
			{
				photonPing.StartPing(text);
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.Log("catched: " + ex);
				int_2--;
				break;
			}
			while (!photonPing.Done())
			{
				if (stopwatch.ElapsedMilliseconds >= int_1)
				{
					flag = true;
					break;
				}
				yield return 0;
			}
			int num4 = (int)stopwatch.ElapsedMilliseconds;
			if ((!bool_0 || i != 0) && photonPing.Successful && !flag)
			{
				num += (float)num4;
				num2++;
				region_0.int_0 = (int)(num / (float)num2);
			}
			yield return new WaitForSeconds(0.1f);
		}
		int_2--;
		yield return null;
	}

	public static string ResolveHost(string string_0)
	{
		try
		{
			IPAddress[] hostAddresses = Dns.GetHostAddresses(string_0);
			if (hostAddresses.Length == 1)
			{
				return hostAddresses[0].ToString();
			}
			foreach (IPAddress iPAddress in hostAddresses)
			{
				if (iPAddress != null)
				{
					string text = iPAddress.ToString();
					if (text.IndexOf('.') >= 0)
					{
						return text;
					}
				}
			}
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log("Exception caught! " + ex.Source + " Message: " + ex.Message);
		}
		return string.Empty;
	}
}
