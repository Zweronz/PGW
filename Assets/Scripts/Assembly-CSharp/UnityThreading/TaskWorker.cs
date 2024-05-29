using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;

namespace UnityThreading
{
	internal sealed class TaskWorker : ThreadBase
	{
		public Dispatcher dispatcher_1;

		[CompilerGenerated]
		private TaskDistributor taskDistributor_0;

		public TaskDistributor TaskDistributor_0
		{
			[CompilerGenerated]
			get
			{
				return taskDistributor_0;
			}
			[CompilerGenerated]
			private set
			{
				taskDistributor_0 = value;
			}
		}

		public bool Boolean_2
		{
			get
			{
				return dispatcher_1.Boolean_0;
			}
		}

		public TaskWorker(string string_1, TaskDistributor taskDistributor_1)
			: base(string_1, false)
		{
			TaskDistributor_0 = taskDistributor_1;
			dispatcher_1 = new Dispatcher(false);
		}

		protected override IEnumerator Do()
		{
			while (true)
			{
				if (!manualResetEvent_0.InterWaitOne(0))
				{
					if (dispatcher_1.ProcessNextTask())
					{
						continue;
					}
					TaskDistributor_0.FillTasks(dispatcher_1);
					if (dispatcher_1.Int32_0 == 0)
					{
						if (WaitHandle.WaitAny(new WaitHandle[2] { manualResetEvent_0, TaskDistributor_0.WaitHandle_0 }) == 0)
						{
							break;
						}
						TaskDistributor_0.FillTasks(dispatcher_1);
					}
					continue;
				}
				return null;
			}
			return null;
		}

		public override void Dispose()
		{
			base.Dispose();
			if (dispatcher_1 != null)
			{
				dispatcher_1.Dispose();
			}
			dispatcher_1 = null;
		}
	}
}
