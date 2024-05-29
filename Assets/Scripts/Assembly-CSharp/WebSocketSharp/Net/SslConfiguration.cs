using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace WebSocketSharp.Net
{
	public abstract class SslConfiguration
	{
		private LocalCertificateSelectionCallback localCertificateSelectionCallback_0;

		private RemoteCertificateValidationCallback remoteCertificateValidationCallback_0;

		private bool bool_0;

		private SslProtocols sslProtocols_0;

		[CompilerGenerated]
		private static LocalCertificateSelectionCallback localCertificateSelectionCallback_1;

		[CompilerGenerated]
		private static RemoteCertificateValidationCallback remoteCertificateValidationCallback_1;

		protected LocalCertificateSelectionCallback LocalCertificateSelectionCallback_0
		{
			get
			{
				return (object object_0, string string_0, X509CertificateCollection x509CertificateCollection_0, X509Certificate x509Certificate_0, string[] string_1) => null;
			}
			set
			{
				localCertificateSelectionCallback_0 = value;
			}
		}

		protected RemoteCertificateValidationCallback RemoteCertificateValidationCallback_0
		{
			get
			{
				return (object object_0, X509Certificate x509Certificate_0, X509Chain x509Chain_0, SslPolicyErrors sslPolicyErrors_0) => true;
			}
			set
			{
				remoteCertificateValidationCallback_0 = value;
			}
		}

		public bool Boolean_0
		{
			get
			{
				return bool_0;
			}
			set
			{
				bool_0 = value;
			}
		}

		public SslProtocols SslProtocols_0
		{
			get
			{
				return sslProtocols_0;
			}
			set
			{
				sslProtocols_0 = value;
			}
		}

		protected SslConfiguration(SslProtocols sslProtocols_1, bool bool_1)
		{
			sslProtocols_0 = sslProtocols_1;
			bool_0 = bool_1;
		}
	}
}
