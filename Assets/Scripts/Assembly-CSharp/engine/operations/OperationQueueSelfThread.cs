using System.Collections.Generic;
using System.Threading;
using UnityThreading;
using engine.events;

namespace engine.operations
{
	public class OperationQueueSelfThread : OperationQueue
	{
		private List<TheradOperation> list_1 = new List<TheradOperation>();

		private Thread thread_0;

		private bool bool_0;

		private EventWaitHandle eventWaitHandle_0;

		public int Int32_2
		{
			get
			{
				return list_1.Count;
			}
		}

		public OperationQueueSelfThread()
		{
			eventWaitHandle_0 = new EventWaitHandle(false, EventResetMode.AutoReset);
			DependSceneEvent<ApplicationQuitUnityEvent>.GlobalSubscribe(delegate
			{
				Kill();
			});
		}

		public void Run()
		{
			bool_0 = true;
			thread_0 = new Thread(Work);
			thread_0.IsBackground = true;
			thread_0.Start();
		}

		public void AddToThread(TheradOperation theradOperation_0)
		{
			lock (list_0)
			{
				list_1.Add(theradOperation_0);
			}
		}

		public void MainUpdate()
		{
			eventWaitHandle_0.Set();
		}

		private void Work()
		{
			Dispatcher.Dispatcher_0 = new Dispatcher(true);
			while (bool_0)
			{
				lock (list_1)
				{
					if (list_1.Count > 0)
					{
						for (int i = 0; i < list_1.Count; i++)
						{
							Add(list_1[i]);
						}
						list_1.Clear();
					}
				}
				lock (list_0)
				{
					Update();
				}
				if (operation_0 != null)
				{
					(operation_0 as TheradOperation).Update();
				}
				eventWaitHandle_0.WaitOne();
			}
		}

		public void Kill(ApplicationQuitUnityEvent applicationQuitUnityEvent_0 = null)
		{
			DependSceneEvent<ApplicationQuitUnityEvent>.GlobalUnsubscribe(delegate
			{
				Kill();
			});
			bool_0 = false;
			eventWaitHandle_0.Set();
			if (thread_0 != null)
			{
				thread_0.Abort();
			}
		}
	}
}
