using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using engine.helpers;

namespace engine.data
{
	[Obfuscation(Exclude = true)]
	public static class BaseStorageHelper
	{
		public static void Init()
		{
			IEnumerable<Type> listChildTypes = GetListChildTypes();
			object obj = null;
			foreach (Type item in listChildTypes)
			{
				Log.AddLine("[BaseStorage::Init. Initing storage, type]: " + item);
				obj = item.GetProperty("Get").GetValue(null, null);
				item.GetMethod("InitInstance", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(obj, new object[0]);
			}
		}

		private static IEnumerable<Type> GetListChildTypes()
		{
			Type type_2 = typeof(BaseStorage<, >);
			return from type_1 in Assembly.GetExecutingAssembly().GetTypes()
				where type_1.BaseType != null && type_1.BaseType.IsGenericType && type_1.BaseType.GetGenericTypeDefinition() == type_2
				select type_1;
		}
	}
}
