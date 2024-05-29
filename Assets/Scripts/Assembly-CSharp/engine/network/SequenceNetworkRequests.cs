using System.Collections.Generic;
using engine.helpers;

namespace engine.network
{
	internal sealed class SequenceNetworkRequests
	{
		private Dictionary<int, AbstractNetworkCommand> dictionary_0 = new Dictionary<int, AbstractNetworkCommand>();

		public void Add(AbstractNetworkCommand abstractNetworkCommand_0)
		{
			if (abstractNetworkCommand_0.NetworkCommandInfo_0.int_1 != -1)
			{
				if (dictionary_0.ContainsKey(abstractNetworkCommand_0.NetworkCommandInfo_0.int_1))
				{
					Log.AddLine(string.Format("Command: {0} with sequence = {1} already available in list request!", abstractNetworkCommand_0.GetType().ToString(), abstractNetworkCommand_0.NetworkCommandInfo_0.int_1), Log.LogLevel.ERROR);
				}
				else
				{
					dictionary_0.Add(abstractNetworkCommand_0.NetworkCommandInfo_0.int_1, abstractNetworkCommand_0);
				}
			}
		}

		public void ProcessCommand(AbstractNetworkCommand abstractNetworkCommand_0)
		{
			AbstractNetworkCommand value;
			if (dictionary_0.TryGetValue(abstractNetworkCommand_0.NetworkCommandInfo_0.int_1, out value))
			{
				dictionary_0.Remove(abstractNetworkCommand_0.NetworkCommandInfo_0.int_1);
				if (abstractNetworkCommand_0.NetworkCommandInfo_0.int_0 > 0)
				{
					Log.AddLine(string.Format("SequenceNetworkRequests::ProcessCommand > code: {0}, message: {1}", abstractNetworkCommand_0.NetworkCommandInfo_0.int_0, (!string.IsNullOrEmpty(abstractNetworkCommand_0.NetworkCommandInfo_0.string_0)) ? abstractNetworkCommand_0.NetworkCommandInfo_0.string_0 : "empty"), Log.LogLevel.WARNING);
					value.HandleError(abstractNetworkCommand_0);
				}
				else
				{
					value.Answered(abstractNetworkCommand_0);
				}
			}
		}

		public void CheckHangingRequests()
		{
			foreach (int key in dictionary_0.Keys)
			{
				Log.AddLine(string.Format("Hanged Request: sequence {0}, type: {1}", key, dictionary_0[key].GetType().ToString()), Log.LogLevel.WARNING);
			}
		}
	}
}
