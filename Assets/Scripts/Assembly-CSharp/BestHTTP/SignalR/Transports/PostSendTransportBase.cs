using System.Collections.Generic;
using BestHTTP.Forms;
using BestHTTP.SignalR.Messages;

namespace BestHTTP.SignalR.Transports
{
	public abstract class PostSendTransportBase : TransportBase
	{
		protected List<HTTPRequest> list_0 = new List<HTTPRequest>();

		public PostSendTransportBase(string string_1, Connection connection_0)
			: base(string_1, connection_0)
		{
		}

		protected override void SendImpl(string string_1)
		{
			HTTPRequest hTTPRequest = new HTTPRequest(base.IConnection_0.BuildUri(RequestTypes.Send, this), HTTPMethods.Post, true, true, OnSendRequestFinished);
			hTTPRequest.HTTPFormUsage_0 = HTTPFormUsage.UrlEncoded;
			hTTPRequest.AddField("data", string_1);
			base.IConnection_0.PrepareRequest(hTTPRequest, RequestTypes.Send);
			hTTPRequest.Int32_3 = -1;
			hTTPRequest.Send();
			list_0.Add(hTTPRequest);
		}

		private void OnSendRequestFinished(HTTPRequest httprequest_0, HTTPResponse httpresponse_0)
		{
			list_0.Remove(httprequest_0);
			string text = string.Empty;
			switch (httprequest_0.HTTPRequestStates_0)
			{
			case HTTPRequestStates.Finished:
				if (httpresponse_0.Boolean_0)
				{
					HTTPManager.ILogger_0.Information("Transport - " + base.String_0, "Send - Request Finished Successfully! " + httpresponse_0.String_1);
					if (!string.IsNullOrEmpty(httpresponse_0.String_1))
					{
						IServerMessage serverMessage = TransportBase.Parse(base.IConnection_0.IJsonEncoder_0, httpresponse_0.String_1);
						if (serverMessage != null)
						{
							base.IConnection_0.OnMessage(serverMessage);
						}
					}
				}
				else
				{
					text = string.Format("Send - Request Finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}", httpresponse_0.Int32_2, httpresponse_0.String_0, httpresponse_0.String_1);
				}
				break;
			case HTTPRequestStates.Error:
				text = "Send - Request Finished with Error! " + ((httprequest_0.Exception_0 == null) ? "No Exception" : (httprequest_0.Exception_0.Message + "\n" + httprequest_0.Exception_0.StackTrace));
				break;
			case HTTPRequestStates.Aborted:
				text = "Send - Request Aborted!";
				break;
			case HTTPRequestStates.ConnectionTimedOut:
				text = "Send - Connection Timed Out!";
				break;
			case HTTPRequestStates.TimedOut:
				text = "Send - Processing the request Timed Out!";
				break;
			}
			if (!string.IsNullOrEmpty(text))
			{
				base.IConnection_0.Error(text);
			}
		}
	}
}
