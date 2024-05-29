using System.IO;
using System.Xml;
using System.Xml.Serialization;
using engine.helpers;

namespace engine.launcher
{
	public sealed class AppGameVersionInfo
	{
		public AppVersionInfo appVersionInfo_0 = new AppVersionInfo();

		public AppVersionInfo appVersionInfo_1 = new AppVersionInfo();

		public bool Boolean_0
		{
			get
			{
				return appVersionInfo_0.versionInfo_0.Boolean_0 && appVersionInfo_1.versionInfo_0.Boolean_0;
			}
		}

		public void Serialize(string string_0)
		{
			if (string.IsNullOrEmpty(string_0))
			{
				Log.AddLine("Error AppGameVersionInfo serialize!", Log.LogLevel.ERROR);
				return;
			}
			XmlSerializer xmlSerializer = new XmlSerializer(GetType());
			using (StreamWriter writer = new StreamWriter(string_0))
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(writer, new XmlWriterSettings
				{
					Indent = true,
					IndentChars = "\t"
				}))
				{
					xmlSerializer.Serialize(xmlWriter, this);
				}
			}
		}

		public static AppGameVersionInfo Deserialize(string string_0)
		{
			if (string.IsNullOrEmpty(string_0))
			{
				Log.AddLine("Error AppGameVersionInfo deserialize!", Log.LogLevel.ERROR);
				return null;
			}
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(AppGameVersionInfo));
			using (StreamReader reader = new StreamReader(string_0))
			{
				using (XmlReader xmlReader = XmlReader.Create(reader, new XmlReaderSettings()))
				{
					return xmlSerializer.Deserialize(xmlReader) as AppGameVersionInfo;
				}
			}
		}
	}
}
