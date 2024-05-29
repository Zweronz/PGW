namespace WebSocketSharp.Net
{
	internal class HttpHeaderInfo
	{
		private string string_0;

		private HttpHeaderType httpHeaderType_0;

		internal bool Boolean_0
		{
			get
			{
				return (httpHeaderType_0 & HttpHeaderType.MultiValueInRequest) == HttpHeaderType.MultiValueInRequest;
			}
		}

		internal bool Boolean_1
		{
			get
			{
				return (httpHeaderType_0 & HttpHeaderType.MultiValueInResponse) == HttpHeaderType.MultiValueInResponse;
			}
		}

		public bool Boolean_2
		{
			get
			{
				return (httpHeaderType_0 & HttpHeaderType.Request) == HttpHeaderType.Request;
			}
		}

		public bool Boolean_3
		{
			get
			{
				return (httpHeaderType_0 & HttpHeaderType.Response) == HttpHeaderType.Response;
			}
		}

		public string String_0
		{
			get
			{
				return string_0;
			}
		}

		public HttpHeaderType HttpHeaderType_0
		{
			get
			{
				return httpHeaderType_0;
			}
		}

		internal HttpHeaderInfo(string string_1, HttpHeaderType httpHeaderType_1)
		{
			string_0 = string_1;
			httpHeaderType_0 = httpHeaderType_1;
		}

		public bool IsMultiValue(bool bool_0)
		{
			return ((httpHeaderType_0 & HttpHeaderType.MultiValue) == HttpHeaderType.MultiValue) ? ((!bool_0) ? Boolean_2 : Boolean_3) : ((!bool_0) ? Boolean_0 : Boolean_1);
		}

		public bool IsRestricted(bool bool_0)
		{
			return (httpHeaderType_0 & HttpHeaderType.Restricted) == HttpHeaderType.Restricted && ((!bool_0) ? Boolean_2 : Boolean_3);
		}
	}
}
