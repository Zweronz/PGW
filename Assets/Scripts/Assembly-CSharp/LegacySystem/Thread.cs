using System;

namespace LegacySystem
{
	public class Thread
	{
		public bool Boolean_0
		{
			get
			{
				return true;
			}
			set
			{
				throw new NotImplementedException("currently always on background");
			}
		}

		public Thread(ThreadStart threadStart_0)
		{
			throw new NotSupportedException();
		}

		public Thread(ParameterizedThreadStart parameterizedThreadStart_0)
		{
			throw new NotSupportedException();
		}

		public void Abort()
		{
			throw new NotSupportedException();
		}

		public bool Join(int int_0)
		{
			throw new NotSupportedException();
		}

		public void Start()
		{
			throw new NotSupportedException();
		}

		public void Start(object object_0)
		{
			throw new NotSupportedException();
		}

		public static void Sleep(int int_0)
		{
			throw new NotSupportedException();
		}
	}
}
