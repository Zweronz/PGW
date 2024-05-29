using System.Reflection;

namespace engine.data
{
	public class HashReflectIndex<K, T> : HashIndex<K, T> where T : class
	{
		private string _keyFieldName;

		private PropertyInfo _keyField;

		public HashReflectIndex(string string_0)
		{
			_keyFieldName = string_0;
			_keyField = typeof(T).GetProperty(_keyFieldName);
		}

		public override K GetIndexField(T gparam_0, HashIndex<K, T> hashIndex_0)
		{
			return (K)_keyField.GetValue(gparam_0, null);
		}
	}
}
