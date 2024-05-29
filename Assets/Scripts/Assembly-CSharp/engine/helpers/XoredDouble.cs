namespace engine.helpers
{
	public sealed class XoredDouble : XoredValue<double>
	{
		public override double Prop_0
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
	}
}
