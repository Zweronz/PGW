using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace WebSocketSharp.Net
{
	public class ClientSslConfiguration : SslConfiguration
	{
		private X509CertificateCollection x509CertificateCollection_0;

		private string string_0;

		public X509CertificateCollection X509CertificateCollection_0
		{
			get
			{
				return x509CertificateCollection_0;
			}
			set
			{
				x509CertificateCollection_0 = value;
			}
		}

		public LocalCertificateSelectionCallback LocalCertificateSelectionCallback_1
		{
			get
			{
				return base.LocalCertificateSelectionCallback_0;
			}
			set
			{
				base.LocalCertificateSelectionCallback_0 = value;
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

		public string String_0
		{
			get
			{
				return string_0;
			}
			set
			{
				string_0 = value;
			}
		}

		public ClientSslConfiguration(string string_1)
			: this(string_1, null, SslProtocols.Default, false)
		{
		}

		public ClientSslConfiguration(string string_1, X509CertificateCollection x509CertificateCollection_1, SslProtocols sslProtocols_1, bool bool_1)
			: base(sslProtocols_1, bool_1)
		{
			string_0 = string_1;
			x509CertificateCollection_0 = x509CertificateCollection_1;
		}
	}
}
