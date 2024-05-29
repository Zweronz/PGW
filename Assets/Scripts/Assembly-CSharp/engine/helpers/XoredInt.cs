namespace engine.helpers
{
	public sealed class XoredInt : XoredValue<int>
	{
		public override int Prop_0
		{
			get
			{
				return EncryptionHelper.ProcessXor(_value);
			}
			set
			{
				_value = EncryptionHelper.ProcessXor(value);
			}
		}

		public XoredInt()
		{
			Prop_0 = 0;
		}

		public XoredInt(int int_0)
		{
			Prop_0 = int_0;
		}
	}
}
