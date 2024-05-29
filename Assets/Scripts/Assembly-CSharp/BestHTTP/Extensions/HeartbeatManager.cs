using System;
using System.Collections.Generic;

namespace BestHTTP.Extensions
{
	public sealed class HeartbeatManager
	{
		private List<IHeartbeat> list_0 = new List<IHeartbeat>();

		private IHeartbeat[] iheartbeat_0;

		private DateTime dateTime_0 = DateTime.MinValue;

		public void Subscribe(IHeartbeat iheartbeat_1)
		{
			lock (list_0)
			{
				if (!list_0.Contains(iheartbeat_1))
				{
					list_0.Add(iheartbeat_1);
				}
			}
		}

		public void Unsubscribe(IHeartbeat iheartbeat_1)
		{
			lock (list_0)
			{
				list_0.Remove(iheartbeat_1);
			}
		}

		public void Update()
		{
			if (dateTime_0 == DateTime.MinValue)
			{
				dateTime_0 = DateTime.UtcNow;
				return;
			}
			TimeSpan dif = DateTime.UtcNow - dateTime_0;
			dateTime_0 = DateTime.UtcNow;
			int num = 0;
			lock (list_0)
			{
				if (iheartbeat_0 == null || iheartbeat_0.Length < list_0.Count)
				{
					Array.Resize(ref iheartbeat_0, list_0.Count);
				}
				list_0.CopyTo(0, iheartbeat_0, 0, list_0.Count);
				num = list_0.Count;
			}
			for (int i = 0; i < num; i++)
			{
				try
				{
					iheartbeat_0[i].OnHeartbeatUpdate(dif);
				}
				catch
				{
				}
			}
		}
	}
}
