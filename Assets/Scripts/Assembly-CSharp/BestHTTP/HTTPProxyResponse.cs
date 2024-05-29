using System.IO;

namespace BestHTTP
{
	public class HTTPProxyResponse : HTTPResponse
	{
		internal HTTPProxyResponse(HTTPRequest httprequest_1, Stream stream_2, bool bool_5, bool bool_6)
			: base(httprequest_1, stream_2, bool_5, bool_6)
		{
		}

		internal override bool Receive(int int_6 = -1, bool bool_5 = false)
		{
			return base.Receive(int_6, false);
		}
	}
}
