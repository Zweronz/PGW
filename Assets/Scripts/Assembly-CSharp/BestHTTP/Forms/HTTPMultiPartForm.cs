using System.IO;
using BestHTTP.Extensions;

namespace BestHTTP.Forms
{
	public sealed class HTTPMultiPartForm : HTTPFormBase
	{
		private string string_0;

		private byte[] byte_0;

		public HTTPMultiPartForm()
		{
			string_0 = GetHashCode().ToString("X");
		}

		public override void PrepareRequest(HTTPRequest httprequest_0)
		{
			httprequest_0.SetHeader("Content-Type", "multipart/form-data; boundary=\"" + string_0 + "\"");
		}

		public override byte[] GetData()
		{
			if (byte_0 != null)
			{
				return byte_0;
			}
			using (MemoryStream memoryStream = new MemoryStream())
			{
				for (int i = 0; i < base.List_0.Count; i++)
				{
					HTTPFieldData hTTPFieldData = base.List_0[i];
					memoryStream.WriteLine("--" + string_0);
					memoryStream.WriteLine("Content-Disposition: form-data; name=\"" + hTTPFieldData.String_0 + "\"" + (string.IsNullOrEmpty(hTTPFieldData.String_1) ? string.Empty : ("; filename=\"" + hTTPFieldData.String_1 + "\"")));
					if (!string.IsNullOrEmpty(hTTPFieldData.String_2))
					{
						memoryStream.WriteLine("Content-Type: " + hTTPFieldData.String_2);
					}
					memoryStream.WriteLine("Content-Length: " + hTTPFieldData.Byte_1.Length);
					memoryStream.WriteLine();
					memoryStream.Write(hTTPFieldData.Byte_1, 0, hTTPFieldData.Byte_1.Length);
					memoryStream.Write(HTTPRequest.byte_0, 0, HTTPRequest.byte_0.Length);
				}
				memoryStream.WriteLine("--" + string_0 + "--");
				base.Boolean_1 = false;
				return byte_0 = memoryStream.ToArray();
			}
		}
	}
}
