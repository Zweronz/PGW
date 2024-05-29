using System;
using System.Collections.Generic;

namespace LitJson
{
	internal struct ObjectMetadata
	{
		private Type type_0;

		private bool bool_0;

		private IDictionary<string, PropertyMetadata> idictionary_0;

		public Type Type_0
		{
			get
			{
				if (type_0 == null)
				{
					return typeof(JsonData);
				}
				return type_0;
			}
			set
			{
				type_0 = value;
			}
		}

		public bool Boolean_0
		{
			get
			{
				return bool_0;
			}
			set
			{
				bool_0 = value;
			}
		}

		public IDictionary<string, PropertyMetadata> IDictionary_0
		{
			get
			{
				return idictionary_0;
			}
			set
			{
				idictionary_0 = value;
			}
		}
	}
}
