using System.Collections.Generic;
using LitJson;

namespace BestHTTP.SocketIO.JsonEncoders
{
	public sealed class LitJsonEncoder : IJsonEncoder
	{
		public List<object> Decode(string json)
		{
			JsonReader jsonReader_ = new JsonReader(json);
			return JsonMapper.ToObject<List<object>>(jsonReader_);
		}

		public string Encode(List<object> obj)
		{
			JsonWriter jsonWriter = new JsonWriter();
			JsonMapper.ToJson(obj, jsonWriter);
			return jsonWriter.ToString();
		}
	}
}
