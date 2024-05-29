using System.Collections;
using System.Collections.Generic;

public static class MiniJsonExtensions
{
	public static string toJson(this Hashtable hashtable_0)
	{
		return MiniJSON.jsonEncode(hashtable_0);
	}

	public static string toJson(this Dictionary<string, string> dictionary_0)
	{
		return MiniJSON.jsonEncode(dictionary_0);
	}

	public static ArrayList arrayListFromJson(this string string_0)
	{
		return MiniJSON.jsonDecode(string_0) as ArrayList;
	}

	public static Hashtable hashtableFromJson(this string string_0)
	{
		return MiniJSON.jsonDecode(string_0) as Hashtable;
	}
}
