using System;
using System.Runtime.CompilerServices;

namespace engine.data
{
	[AttributeUsage(AttributeTargets.Class)]
	public class StorageDataKeyAttribute : Attribute
	{
		[CompilerGenerated]
		private Type type_0;

		public Type Type_0
		{
			[CompilerGenerated]
			get
			{
				return type_0;
			}
			[CompilerGenerated]
			private set
			{
				type_0 = value;
			}
		}

		public StorageDataKeyAttribute(Type type_1)
		{
			Type_0 = type_1;
		}
	}
}
