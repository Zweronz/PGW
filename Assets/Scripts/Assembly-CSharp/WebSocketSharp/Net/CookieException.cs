using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace WebSocketSharp.Net
{
	[Serializable]
	public class CookieException : FormatException, ISerializable
	{
		internal CookieException(string string_0)
			: base(string_0)
		{
		}

		internal CookieException(string string_0, Exception exception_0)
			: base(string_0, exception_0)
		{
		}

		protected CookieException(SerializationInfo serializationInfo_0, StreamingContext streamingContext_0)
			: base(serializationInfo_0, streamingContext_0)
		{
		}

		public CookieException()
		{
		}

		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\r\nversion=\"1\">\r\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\r\nversion=\"1\"\r\nFlags=\"SerializationFormatter\"/>\r\n</PermissionSet>\r\n")]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\r\nversion=\"1\">\r\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\r\nversion=\"1\"\r\nFlags=\"SerializationFormatter\"/>\r\n</PermissionSet>\r\n")]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
