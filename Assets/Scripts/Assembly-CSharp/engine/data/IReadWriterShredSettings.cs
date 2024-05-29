using engine.helpers;

namespace engine.data
{
	public abstract class IReadWriterShredSettings
	{
		protected BaseSharedSettings baseSharedSettings_0;

		protected IReadWriterShredSettings(BaseSharedSettings baseSharedSettings_1)
		{
			baseSharedSettings_0 = baseSharedSettings_1;
			if (baseSharedSettings_0 == null)
			{
				Log.AddLine("IReadWriterShredSettings|Need settings not set", Log.LogLevel.ERROR);
			}
		}

		public abstract void Save();

		public abstract void Load();
	}
}
