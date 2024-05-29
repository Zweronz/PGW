using System.Runtime.CompilerServices;
using UnityEngine;

namespace BestHTTP.Forms
{
	public sealed class UnityForm : HTTPFormBase
	{
		[CompilerGenerated]
		private WWWForm wwwform_0;

		public WWWForm WWWForm_0
		{
			[CompilerGenerated]
			get
			{
				return wwwform_0;
			}
			[CompilerGenerated]
			set
			{
				wwwform_0 = value;
			}
		}

		public UnityForm()
		{
		}

		public UnityForm(WWWForm wwwform_1)
		{
			WWWForm_0 = wwwform_1;
		}

		public override void CopyFrom(HTTPFormBase httpformBase_0)
		{
			base.List_0 = httpformBase_0.List_0;
			base.Boolean_1 = true;
			if (WWWForm_0 != null)
			{
				return;
			}
			WWWForm_0 = new WWWForm();
			if (base.List_0 == null)
			{
				return;
			}
			for (int i = 0; i < base.List_0.Count; i++)
			{
				HTTPFieldData hTTPFieldData = base.List_0[i];
				if (string.IsNullOrEmpty(hTTPFieldData.String_3) && hTTPFieldData.Byte_0 != null)
				{
					WWWForm_0.AddBinaryData(hTTPFieldData.String_0, hTTPFieldData.Byte_0, hTTPFieldData.String_1, hTTPFieldData.String_2);
				}
				else
				{
					WWWForm_0.AddField(hTTPFieldData.String_0, hTTPFieldData.String_3, hTTPFieldData.Encoding_0);
				}
			}
		}

		public override void PrepareRequest(HTTPRequest httprequest_0)
		{
			if (WWWForm_0.headers.ContainsKey("Content-Type"))
			{
				httprequest_0.SetHeader("Content-Type", WWWForm_0.headers["Content-Type"] as string);
			}
			else
			{
				httprequest_0.SetHeader("Content-Type", "application/x-www-form-urlencoded");
			}
		}

		public override byte[] GetData()
		{
			return WWWForm_0.data;
		}
	}
}
