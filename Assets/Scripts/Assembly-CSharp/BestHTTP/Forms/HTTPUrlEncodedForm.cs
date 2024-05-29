using System;
using System.Text;

namespace BestHTTP.Forms
{
	public sealed class HTTPUrlEncodedForm : HTTPFormBase
	{
		private byte[] byte_0;

		public override void PrepareRequest(HTTPRequest httprequest_0)
		{
			httprequest_0.SetHeader("Content-Type", "application/x-www-form-urlencoded");
		}

		public override byte[] GetData()
		{
			if (byte_0 != null && !base.Boolean_1)
			{
				return byte_0;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < base.List_0.Count; i++)
			{
				HTTPFieldData hTTPFieldData = base.List_0[i];
				if (i > 0)
				{
					stringBuilder.Append("&");
				}
				stringBuilder.Append(Uri.EscapeDataString(hTTPFieldData.String_0));
				stringBuilder.Append("=");
				if (string.IsNullOrEmpty(hTTPFieldData.String_3) && hTTPFieldData.Byte_0 != null)
				{
					stringBuilder.Append(Uri.EscapeDataString(Encoding.UTF8.GetString(hTTPFieldData.Byte_0, 0, hTTPFieldData.Byte_0.Length)));
				}
				else
				{
					stringBuilder.Append(Uri.EscapeDataString(hTTPFieldData.String_3));
				}
			}
			base.Boolean_1 = false;
			return byte_0 = Encoding.UTF8.GetBytes(stringBuilder.ToString());
		}
	}
}
