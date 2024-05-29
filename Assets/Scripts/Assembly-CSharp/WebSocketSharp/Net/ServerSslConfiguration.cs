using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace WebSocketSharp.Net
{
	public class ServerSslConfiguration : SslConfiguration
	{
		private X509Certificate2 x509Certificate2_0;

		private bool bool_1;

		public bool Boolean_1
		{
			get
			{
				return bool_1;
			}
			set
			{
				bool_1 = value;
			}
		}

		public RemoteCertificateValidationCallback RemoteCertificateValidationCallback_1
		{
			get
			{
				return base.RemoteCertificateValidationCallback_0;
			}
			set
			{
				base.RemoteCertificateValidationCallback_0 = value;
			}
		}

		public X509Certificate2 X509Certificate2_0
		{
			get
			{
				return x509Certificate2_0;
			}
			set
			{
				x509Certificate2_0 = value;
			}
		}

		public ServerSslConfiguration(X509Certificate2 x509Certificate2_1)
			: this(x509Certificate2_1, false, SslProtocols.Default, false)
		{
		}

		public ServerSslConfiguration(X509Certificate2 x509Certificate2_1, bool bool_2, SslProtocols sslProtocols_1, bool bool_3)
			: base(sslProtocols_1, bool_3)
		{
			x509Certificate2_0 = x509Certificate2_1;
			bool_1 = bool_2;
		}
	}
}
