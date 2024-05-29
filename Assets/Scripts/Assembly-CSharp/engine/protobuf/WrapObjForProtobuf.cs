using System;
using ProtoBuf;

namespace engine.protobuf
{
	[ProtoInclude(3, typeof(WrapObjForProtobuf<float>))]
	[ProtoInclude(5, typeof(WrapObjForProtobuf<long>))]
	[ProtoInclude(4, typeof(WrapObjForProtobuf<double>))]
	[ProtoInclude(6, typeof(WrapObjForProtobuf<string>))]
	[ProtoInclude(1, typeof(WrapObjForProtobuf<bool>))]
	[ProtoInclude(2, typeof(WrapObjForProtobuf<int>))]
	[ProtoContract]
	public abstract class WrapObjForProtobuf
	{
		public abstract object Object_0 { get; }

		public static WrapObjForProtobuf<T> Create<T>(T gparam_0) where T : IComparable
		{
			WrapObjForProtobuf<T> wrapObjForProtobuf = new WrapObjForProtobuf<T>();
			wrapObjForProtobuf.Prop_0 = gparam_0;
			return wrapObjForProtobuf;
		}

		public override string ToString()
		{
			if (Object_0 == null)
			{
				return "null";
			}
			return Object_0.ToString();
		}
	}
	[ProtoContract]
	public sealed class WrapObjForProtobuf<T> : WrapObjForProtobuf
	{
		public override object Object_0
		{
			get
			{
				return Prop_0;
			}
		}

		[ProtoMember(1)]
		public T Prop_0 { get; set; }
	}
}
