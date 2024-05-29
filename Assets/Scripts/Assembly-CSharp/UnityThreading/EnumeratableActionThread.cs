using System;
using System.Collections;

namespace UnityThreading
{
	public sealed class EnumeratableActionThread : ThreadBase
	{
		private Func<ThreadBase, IEnumerator> func_0;

		public EnumeratableActionThread(Func<ThreadBase, IEnumerator> func_1)
			: this(func_1, true)
		{
		}

		public EnumeratableActionThread(Func<ThreadBase, IEnumerator> func_1, bool bool_0)
			: base("EnumeratableActionThread", Dispatcher.Dispatcher_0, false)
		{
			func_0 = func_1;
			if (bool_0)
			{
				Start();
			}
		}

		protected override IEnumerator Do()
		{
			return func_0(this);
		}
	}
}
