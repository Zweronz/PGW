using System.Collections;

namespace LitJson
{
	public interface IJsonWrapper : IDictionary, IList, ICollection, IEnumerable, IOrderedDictionary
	{
		bool Boolean_0 { get; }

		bool Boolean_1 { get; }

		bool Boolean_2 { get; }

		bool Boolean_3 { get; }

		bool Boolean_4 { get; }

		bool Boolean_5 { get; }

		bool Boolean_6 { get; }

		bool GetBoolean();

		double GetDouble();

		int GetInt();

		JsonType GetJsonType();

		long GetLong();

		string GetString();

		void SetBoolean(bool val);

		void SetDouble(double val);

		void SetInt(int val);

		void SetJsonType(JsonType type);

		void SetLong(long val);

		void SetString(string val);

		string ToJson();

		void ToJson(JsonWriter writer);
	}
}
