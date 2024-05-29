using System;
using System.Collections.Generic;
using System.Linq;

namespace engine.network
{
	public static class NetworkCommands
	{
		private static Dictionary<int, Type> dictionary_0 = new Dictionary<int, Type>();

		public static void Register(Type type_0, int int_0)
		{
			if (!dictionary_0.ContainsKey(int_0))
			{
				dictionary_0.Add(int_0, type_0);
			}
		}

		public static void RegisterTest(Type type_0, int int_0)
		{
			if (!dictionary_0.ContainsKey(int_0))
			{
				dictionary_0.Add(int_0, type_0);
			}
			else
			{
				dictionary_0[int_0] = type_0;
			}
		}

		public static Type GetTypeById(int int_0)
		{
			if (dictionary_0.ContainsKey(int_0))
			{
				return dictionary_0[int_0];
			}
			return null;
		}

		public static int GetTypeIdByType(Type type_0)
		{
			if (dictionary_0.ContainsValue(type_0))
			{
				return dictionary_0.FirstOrDefault((KeyValuePair<int, Type> keyValuePair_0) => keyValuePair_0.Value == type_0).Key;
			}
			return -1;
		}
	}
}
