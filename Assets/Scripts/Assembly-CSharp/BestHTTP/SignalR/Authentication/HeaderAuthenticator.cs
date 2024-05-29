using System;
using System.Runtime.CompilerServices;

namespace BestHTTP.SignalR.Authentication
{
	internal class HeaderAuthenticator : IAuthenticationProvider
	{
		private OnAuthenticationSuccededDelegate onAuthenticationSuccededDelegate_0;

		private OnAuthenticationFailedDelegate onAuthenticationFailedDelegate_0;

		[CompilerGenerated]
		private string string_0;

		[CompilerGenerated]
		private string string_1;

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

		public bool Boolean_0
		{
			get
			{
				return false;
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

		public HeaderAuthenticator(string string_2, string string_3)
		{
			String_0 = string_2;
			String_1 = string_3;
		}

		public void StartAuthentication()
		{
		}

		public void PrepareRequest(HTTPRequest request, RequestTypes type)
		{
			request.SetHeader("username", String_0);
			request.SetHeader("roles", String_1);
		}
	}
}
