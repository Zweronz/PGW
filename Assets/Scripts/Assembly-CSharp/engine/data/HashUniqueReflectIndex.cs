using System.Reflection;

namespace engine.data
{
	public class HashUniqueReflectIndex<K, T> : HashUniqueIndex<K, T> where T : class
	{
		private string _keyFieldName;

		private PropertyInfo _keyField;

		public HashUniqueReflectIndex(string string_0)
		{
			_keyFieldName = string_0;
			_keyField = typeof(T).GetProperty(_keyFieldName);
		}

		public override K GetIndexField(T gparam_0, HashUniqueIndex<K, T> hashUniqueIndex_0)
		{
			return (K)_keyField.GetValue(gparam_0, null);
		}
	}
}
