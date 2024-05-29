using System;
using System.Collections.Generic;

namespace Rilisoft
{
	internal abstract class SignedPreferences : Preferences
	{
		private readonly Preferences preferences_0;

		public override ICollection<string> Keys
		{
			get
			{
				return preferences_0.Keys;
			}
		}

		public override ICollection<string> Values
		{
			get
			{
				return preferences_0.Values;
			}
		}

		public override int Count
		{
			get
			{
				return preferences_0.Count;
			}
		}

		public override bool IsReadOnly
		{
			get
			{
				return preferences_0.IsReadOnly;
			}
		}

		protected Preferences Preferences_0
		{
			get
			{
				return preferences_0;
			}
		}

		protected SignedPreferences(Preferences preferences_1)
		{
			preferences_0 = preferences_1;
		}

		public bool Verify(string string_0)
		{
			if (string_0 == null)
			{
				throw new ArgumentNullException("key");
			}
			return VerifyCore(string_0);
		}

		protected abstract void AddSignedCore(string string_0, string string_1);

		protected abstract bool RemoveSignedCore(string string_0);

		protected abstract bool VerifyCore(string string_0);

		protected override void AddCore(string string_0, string string_1)
		{
			AddSignedCore(string_0, string_1);
		}

		protected override bool ContainsKeyCore(string string_0)
		{
			return preferences_0.ContainsKey(string_0);
		}

		protected override void CopyToCore(KeyValuePair<string, string>[] keyValuePair_0, int int_0)
		{
			preferences_0.CopyTo(keyValuePair_0, int_0);
		}

		protected override bool RemoveCore(string string_0)
		{
			return RemoveSignedCore(string_0);
		}

		protected override bool TryGetValueCore(string string_0, out string string_1)
		{
			return preferences_0.TryGetValue(string_0, out string_1);
		}

		public override void Save()
		{
			preferences_0.Save();
		}

		public override void Clear()
		{
			preferences_0.Clear();
		}

		public override IEnumerator<KeyValuePair<string, string>> GetEnumerator()
		{
			return preferences_0.GetEnumerator();
		}
	}
}
