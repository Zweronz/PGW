namespace engine.helpers
{
	public sealed class XoredFloat : XoredValue<float>
	{
		public override float Prop_0
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

		public XoredFloat()
		{
			Prop_0 = 0f;
		}

		public XoredFloat(float float_0)
		{
			Prop_0 = float_0;
		}
	}
}
