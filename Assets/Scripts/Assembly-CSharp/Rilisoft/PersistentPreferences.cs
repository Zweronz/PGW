using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

namespace Rilisoft
{
	internal sealed class PersistentPreferences : Preferences
	{
		private const string string_0 = "Key";

		private const string string_1 = "Preference";

		private const string string_2 = "Preferences";

		private const string string_3 = "Value";

		private readonly XDocument xdocument_0;

		private static readonly string string_4;

		public override ICollection<string> Keys
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public override ICollection<string> Values
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public override int Count
		{
			get
			{
				return xdocument_0.Root.Elements().Count();
			}
		}

		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		internal static string String_0
		{
			get
			{
				return string_4;
			}
		}

		public PersistentPreferences()
		{
			try
			{
				xdocument_0 = XDocument.Load(string_4);
			}
			catch (Exception)
			{
				xdocument_0 = new XDocument(new XElement("Preferences"));
				xdocument_0.Save(string_4);
			}
		}

		static PersistentPreferences()
		{
			string_4 = Path.Combine(Application.persistentDataPath, "com.P3D.Pixlgun.Settings.xml");
		}

		protected override void AddCore(string string_5, string string_6)
		{
			XElement xElement = xdocument_0.Root.Elements("Preference").FirstOrDefault((XElement xelement_0) => xelement_0.Element("Key") != null && xelement_0.Element("Key").Value.Equals(string_5));
			if (xElement != null)
			{
				xElement.Remove();
			}
			XElement content = new XElement("Preference", new XElement("Key", string_5), new XElement("Value", string_6));
			xdocument_0.Root.Add(content);
			xdocument_0.Save(string_4);
		}

		protected override bool ContainsKeyCore(string string_5)
		{
			return xdocument_0.Root.Elements("Preference").Any((XElement xelement_0) => xelement_0.Element("Key") != null && xelement_0.Element("Key").Value.Equals(string_5));
		}

		protected override void CopyToCore(KeyValuePair<string, string>[] keyValuePair_0, int int_0)
		{
			throw new NotSupportedException();
		}

		protected override bool RemoveCore(string string_5)
		{
			XElement xElement = xdocument_0.Root.Elements("Preference").FirstOrDefault((XElement xelement_0) => xelement_0.Element("Key") != null && xelement_0.Element("Key").Value.Equals(string_5));
			if (xElement != null)
			{
				xElement.Remove();
				xdocument_0.Save(string_4);
				return true;
			}
			return false;
		}

		protected override bool TryGetValueCore(string string_5, out string string_6)
		{
			XElement xElement = xdocument_0.Root.Elements("Preference").FirstOrDefault((XElement xelement_0) => xelement_0.Element("Key") != null && xelement_0.Element("Key").Value.Equals(string_5));
			if (xElement != null)
			{
				XElement xElement2 = xElement.Element("Value");
				if (xElement2 != null)
				{
					string_6 = xElement2.Value;
					return true;
				}
			}
			string_6 = null;
			return false;
		}

		public override void Save()
		{
			xdocument_0.Save(string_4);
		}

		public override void Clear()
		{
			xdocument_0.Root.RemoveNodes();
			xdocument_0.Save(string_4);
		}

		public override IEnumerator<KeyValuePair<string, string>> GetEnumerator()
		{
			throw new NotSupportedException();
		}
	}
}
