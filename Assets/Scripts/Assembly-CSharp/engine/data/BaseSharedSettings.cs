using System;
using System.IO;
using System.Runtime.CompilerServices;
using engine.events;
using engine.helpers;

namespace engine.data
{
	public class BaseSharedSettings : BaseEvent
	{
		internal DictionarySerialize dictionarySerialize_0 = new DictionarySerialize();

		protected IReadWriterShredSettings[] ireadWriterShredSettings_0;

		private bool bool_0;

		[CompilerGenerated]
		private static Action<IReadWriterShredSettings> action_0;

		[CompilerGenerated]
		private static Action<IReadWriterShredSettings> action_1;

		public virtual string String_1
		{
			get
			{
				return string.Empty;
			}
			set
			{
			}
		}

		public virtual string String_2
		{
			get
			{
				return string.Empty;
			}
			set
			{
			}
		}

		public string String_0
		{
			get
			{
				return Path.Combine(String_2, String_1);
			}
		}

		public T GetValue<T>(string string_0)
		{
			return GetValue(string_0, default(T));
		}

		public T GetValue<T>(string string_0, T gparam_0)
		{
			if (!bool_0)
			{
				Load();
			}
			object obj = dictionarySerialize_0[string_0];
			if (obj == null)
			{
				return gparam_0;
			}
			return (T)Convert.ChangeType(obj, typeof(T));
		}

		public void SetValue<T>(string string_0, T gparam_0, bool bool_1 = false)
		{
			if (!bool_0)
			{
				Load();
			}
			dictionarySerialize_0[string_0] = gparam_0;
			if (bool_1)
			{
				Save();
			}
			Dispatch(string_0);
		}

		public virtual void Save()
		{
			Array.ForEach(ireadWriterShredSettings_0, delegate(IReadWriterShredSettings ireadWriterShredSettings_1)
			{
				ireadWriterShredSettings_1.Save();
			});
		}

		protected virtual void Load()
		{
			Array.ForEach(ireadWriterShredSettings_0, delegate(IReadWriterShredSettings ireadWriterShredSettings_1)
			{
				ireadWriterShredSettings_1.Load();
			});
			bool_0 = true;
			InternalInit();
		}

		protected virtual void InternalInit()
		{
			Save();
		}
	}
}
