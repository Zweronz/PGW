namespace engine.helpers
{
	public sealed class XoredString : XoredValue<string>
	{
		public override string Prop_0
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
