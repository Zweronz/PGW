using System;

namespace LitJson
{
	internal struct ArrayMetadata
	{
		private Type type_0;

		private bool bool_0;

		private bool bool_1;

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

		public bool Boolean_1
		{
			get
			{
				return bool_1;
			}
			set
			{
				bool_1 = value;
			}
		}
	}
}
