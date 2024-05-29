using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Rilisoft
{
	internal sealed class RsaSignedPreferences : SignedPreferences
	{
		private const string string_0 = "{0}__{1}__{2}";

		private const string string_1 = "com.Rilisoft";

		private readonly HashAlgorithm hashAlgorithm_0 = new SHA1CryptoServiceProvider();

		private readonly RSACryptoServiceProvider rsacryptoServiceProvider_0;

		private readonly string string_2;

		private readonly byte[] byte_0 = Encoding.UTF8.GetBytes("com.Rilisoft");

		private readonly RSACryptoServiceProvider rsacryptoServiceProvider_1;

		public RsaSignedPreferences(Preferences preferences_1, RSACryptoServiceProvider rsacryptoServiceProvider_2, string string_3)
			: base(preferences_1)
		{
			rsacryptoServiceProvider_0 = rsacryptoServiceProvider_2;
			rsacryptoServiceProvider_1 = new RSACryptoServiceProvider();
			rsacryptoServiceProvider_1.ImportParameters(rsacryptoServiceProvider_2.ExportParameters(false));
			string_2 = string_3;
		}

		protected override void AddSignedCore(string string_3, string string_4)
		{
			if (string_3.StartsWith("com.Rilisoft"))
			{
				throw new ArgumentException("Key starts with reserved prefix.", "key");
			}
			base.Preferences_0.Add(string_3, string_4);
			base.Preferences_0.Add(GetSignatureKey(string_3), Sign(string_3, string_4));
		}

		protected override bool RemoveSignedCore(string string_3)
		{
			if (string_3.StartsWith("com.Rilisoft"))
			{
				throw new ArgumentException("Key starts with reserved prefix.", "key");
			}
			base.Preferences_0.Remove(GetSignatureKey(string_3));
			return base.Preferences_0.Remove(string_3);
		}

		protected override bool VerifyCore(string string_3)
		{
			string arg;
			if (!TryGetValueCore(string_3, out arg))
			{
				throw new KeyNotFoundException(string.Format("The given key was not present in the dictionary: {0}", string_3));
			}
			string s = string.Format("{0}__{1}__{2}", string_2, string_3, arg);
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			string s2;
			if (!TryGetValueCore(GetSignatureKey(string_3), out s2))
			{
				return false;
			}
			try
			{
				byte[] signature = Convert.FromBase64String(s2);
				return rsacryptoServiceProvider_1.VerifyData(bytes, hashAlgorithm_0, signature);
			}
			catch (FormatException)
			{
				return false;
			}
		}

		private string GetSignatureKey(string string_3)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(string_2);
			byte[] bytes2 = Encoding.UTF8.GetBytes(string_3);
			byte[] buffer = bytes.Concat(byte_0).Concat(bytes2).ToArray();
			byte[] inArray = hashAlgorithm_0.ComputeHash(buffer);
			return string.Format("{0}_{1}", "com.Rilisoft", Convert.ToBase64String(inArray));
		}

		private string Sign(string string_3, string string_4)
		{
			string s = string.Format("{0}__{1}__{2}", string_2, string_3, string_4);
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			byte[] inArray = rsacryptoServiceProvider_0.SignData(bytes, hashAlgorithm_0);
			return Convert.ToBase64String(inArray);
		}
	}
}
