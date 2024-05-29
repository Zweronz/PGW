using System.Collections.Generic;
using BestHTTP.JSON;

namespace BestHTTP.SignalR.JsonEncoders
{
	public sealed class DefaultJsonEncoder : IJsonEncoder
	{
		public string Encode(object obj)
		{
			return Json.Encode(obj);
		}

		public IDictionary<string, object> DecodeMessage(string json)
		{
			bool bool_ = false;
			IDictionary<string, object> dictionary = Json.Decode(json, ref bool_) as IDictionary<string, object>;
			object result;
			if (bool_)
			{
				IDictionary<string, object> dictionary2 = dictionary;
				result = dictionary2;
			}
			else
			{
				result = null;
			}
			return (IDictionary<string, object>)result;
		}
	}
}
