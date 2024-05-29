using System.IO;

namespace SimpleJSON
{
	public class JSONData : JSONNode
	{
		private string string_0;

		public override string String_0
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

		public JSONData(string string_1)
		{
			string_0 = string_1;
		}

		public JSONData(float float_0)
		{
			Single_0 = float_0;
		}

		public JSONData(double double_0)
		{
			Double_0 = double_0;
		}

		public JSONData(bool bool_0)
		{
			Boolean_0 = bool_0;
		}

		public JSONData(int int_0)
		{
			Int32_1 = int_0;
		}

		public override string ToString()
		{
			return "\"" + JSONNode.Escape(string_0) + "\"";
		}

		public override string ToString(string string_1)
		{
			return "\"" + JSONNode.Escape(string_0) + "\"";
		}

		public override void Serialize(BinaryWriter binaryWriter_0)
		{
			JSONData jSONData = new JSONData(string.Empty);
			jSONData.Int32_1 = Int32_1;
			if (jSONData.string_0 == string_0)
			{
				binaryWriter_0.Write((byte)4);
				binaryWriter_0.Write(Int32_1);
				return;
			}
			jSONData.Single_0 = Single_0;
			if (jSONData.string_0 == string_0)
			{
				binaryWriter_0.Write((byte)7);
				binaryWriter_0.Write(Single_0);
				return;
			}
			jSONData.Double_0 = Double_0;
			if (jSONData.string_0 == string_0)
			{
				binaryWriter_0.Write((byte)5);
				binaryWriter_0.Write(Double_0);
				return;
			}
			jSONData.Boolean_0 = Boolean_0;
			if (jSONData.string_0 == string_0)
			{
				binaryWriter_0.Write((byte)6);
				binaryWriter_0.Write(Boolean_0);
			}
			else
			{
				binaryWriter_0.Write((byte)3);
				binaryWriter_0.Write(string_0);
			}
		}
	}
}
