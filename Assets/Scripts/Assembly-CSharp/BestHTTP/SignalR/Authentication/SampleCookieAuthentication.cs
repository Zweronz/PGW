using System;
using System.Runtime.CompilerServices;
using BestHTTP.Cookies;

namespace BestHTTP.SignalR.Authentication
{
	public sealed class SampleCookieAuthentication : IAuthenticationProvider
	{
		private HTTPRequest httprequest_0;

		private Cookie cookie_0;

		private OnAuthenticationSuccededDelegate onAuthenticationSuccededDelegate_0;

		private OnAuthenticationFailedDelegate onAuthenticationFailedDelegate_0;

		[CompilerGenerated]
		private Uri uri_0;

		[CompilerGenerated]
		private string string_0;

		[CompilerGenerated]
		private string string_1;

		[CompilerGenerated]
		private string string_2;

		[CompilerGenerated]
		private bool bool_0;

		[CompilerGenerated]
		private static Predicate<Cookie> predicate_0;

		public Uri Uri_0
		{
			[CompilerGenerated]
			get
			{
				return uri_0;
			}
			[CompilerGenerated]
			private set
			{
				uri_0 = value;
			}
		}

		public string String_0
		{
			[CompilerGenerated]
			get
			{
				return string_0;
			}
			[CompilerGenerated]
			private set
			{
				string_0 = value;
			}
		}

		public string String_1
		{
			[CompilerGenerated]
			get
			{
				return string_1;
			}
			[CompilerGenerated]
			private set
			{
				string_1 = value;
			}
		}

		public string String_2
		{
			[CompilerGenerated]
			get
			{
				return string_2;
			}
			[CompilerGenerated]
			private set
			{
				string_2 = value;
			}
		}

		public bool Boolean_0
		{
			[CompilerGenerated]
			get
			{
				return bool_0;
			}
			[CompilerGenerated]
			private set
			{
				bool_0 = value;
			}
		}

		public event OnAuthenticationSuccededDelegate OnAuthenticationSucceded
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				onAuthenticationSuccededDelegate_0 = (OnAuthenticationSuccededDelegate)Delegate.Combine(onAuthenticationSuccededDelegate_0, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				onAuthenticationSuccededDelegate_0 = (OnAuthenticationSuccededDelegate)Delegate.Remove(onAuthenticationSuccededDelegate_0, value);
			}
		}

		public event OnAuthenticationFailedDelegate OnAuthenticationFailed
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				onAuthenticationFailedDelegate_0 = (OnAuthenticationFailedDelegate)Delegate.Combine(onAuthenticationFailedDelegate_0, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				onAuthenticationFailedDelegate_0 = (OnAuthenticationFailedDelegate)Delegate.Remove(onAuthenticationFailedDelegate_0, value);
			}
		}

		public SampleCookieAuthentication(Uri uri_1, string string_3, string string_4, string string_5)
		{
			Uri_0 = uri_1;
			String_0 = string_3;
			String_1 = string_4;
			String_2 = string_5;
			Boolean_0 = true;
		}

		public void StartAuthentication()
		{
			httprequest_0 = new HTTPRequest(Uri_0, HTTPMethods.Post, OnAuthRequestFinished);
			httprequest_0.AddField("userName", String_0);
			httprequest_0.AddField("Password", String_1);
			httprequest_0.AddField("roles", String_2);
			httprequest_0.Send();
		}

		public void PrepareRequest(HTTPRequest request, RequestTypes type)
		{
			request.List_0.Add(cookie_0);
		}

		private void OnAuthRequestFinished(HTTPRequest httprequest_1, HTTPResponse httpresponse_0)
		{
			httprequest_0 = null;
			string text = string.Empty;
			switch (httprequest_1.HTTPRequestStates_0)
			{
			case HTTPRequestStates.Finished:
				if (httpresponse_0.Boolean_0)
				{
					cookie_0 = ((httpresponse_0.List_0 == null) ? null : httpresponse_0.List_0.Find((Cookie cookie_1) => cookie_1.String_0.Equals(".ASPXAUTH")));
					if (cookie_0 != null)
					{
						HTTPManager.ILogger_0.Information("CookieAuthentication", "Auth. Cookie found!");
						if (onAuthenticationSuccededDelegate_0 != null)
						{
							onAuthenticationSuccededDelegate_0(this);
						}
						return;
					}
					HTTPManager.ILogger_0.Warning("CookieAuthentication", text = "Auth. Cookie NOT found!");
				}
				else
				{
					HTTPManager.ILogger_0.Warning("CookieAuthentication", text = string.Format("Request Finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}", httpresponse_0.Int32_2, httpresponse_0.String_0, httpresponse_0.String_1));
				}
				break;
			case HTTPRequestStates.Error:
				HTTPManager.ILogger_0.Warning("CookieAuthentication", text = "Request Finished with Error! " + ((httprequest_1.Exception_0 == null) ? "No Exception" : (httprequest_1.Exception_0.Message + "\n" + httprequest_1.Exception_0.StackTrace)));
				break;
			case HTTPRequestStates.Aborted:
				HTTPManager.ILogger_0.Warning("CookieAuthentication", text = "Request Aborted!");
				break;
			case HTTPRequestStates.ConnectionTimedOut:
				HTTPManager.ILogger_0.Error("CookieAuthentication", text = "Connection Timed Out!");
				break;
			case HTTPRequestStates.TimedOut:
				HTTPManager.ILogger_0.Error("CookieAuthentication", text = "Processing the request Timed Out!");
				break;
			}
			if (onAuthenticationFailedDelegate_0 != null)
			{
				onAuthenticationFailedDelegate_0(this, text);
			}
		}
	}
}
