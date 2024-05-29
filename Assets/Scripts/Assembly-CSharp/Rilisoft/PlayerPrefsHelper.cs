using System;
using System.Security.Cryptography;
using System.Text;

namespace Rilisoft
{
	public sealed class PlayerPrefsHelper : IDisposable
	{
		private bool bool_0;

		private readonly HMAC hmac_0;

		private readonly string string_0;

		internal PlayerPrefsHelper()
		{
			using (HashAlgorithm hashAlgorithm = new SHA256Managed())
			{
				string_0 = BitConverter.ToString(hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes("PrefsKey"))).Replace("-", string.Empty);
				string_0 = string_0.Substring(0, Math.Min(32, string_0.Length)).ToLower();
				byte[] key = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes("HmacKey"));
				hmac_0 = new HMACSHA256(key);
			}
		}

		public void Dispose()
		{
			if (!bool_0)
			{
				hmac_0.Clear();
				bool_0 = true;
			}
		}
	}
}
