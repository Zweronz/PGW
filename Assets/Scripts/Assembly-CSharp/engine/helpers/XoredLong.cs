namespace engine.helpers
{
	public sealed class XoredLong : XoredValue<long>
	{
		public override long Prop_0
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
