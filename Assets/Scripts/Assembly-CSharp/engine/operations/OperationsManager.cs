using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using engine.events;

namespace engine.operations
{
	public class OperationsManager
	{
		private static OperationsManager operationsManager_0;

		private List<OperationQueue> list_0 = new List<OperationQueue>();

		private List<OperationQueueSelfThread> list_1 = new List<OperationQueueSelfThread>();

		[CompilerGenerated]
		private OperationQueue operationQueue_0;

		[CompilerGenerated]
		private OperationQueue operationQueue_1;

		[CompilerGenerated]
		private long long_0;

		[CompilerGenerated]
		private static Action action_0;

		public static OperationsManager OperationsManager_0
		{
			get
			{
				if (operationsManager_0 == null)
				{
					operationsManager_0 = new OperationsManager();
				}
				return operationsManager_0;
			}
		}

		public OperationQueue OperationQueue_0
		{
			[CompilerGenerated]
			get
			{
				return operationQueue_0;
			}
			[CompilerGenerated]
			private set
			{
				operationQueue_0 = value;
			}
		}

		public OperationQueue OperationQueue_1
		{
			[CompilerGenerated]
			get
			{
				return operationQueue_1;
			}
			[CompilerGenerated]
			private set
			{
				operationQueue_1 = value;
			}
		}

		public long Int64_0
		{
			[CompilerGenerated]
			get
			{
				return long_0;
			}
			[CompilerGenerated]
			set
			{
				long_0 = value;
			}
		}

		private OperationsManager()
		{
			OperationQueue_0 = new OperationQueue();
			OperationQueue_1 = OperationQueue_0;
			list_0.Add(OperationQueue_0);
			DependSceneEvent<MainUpdate>.GlobalSubscribe(Update);
		}

		public OperationQueue AddNewQueue(int int_0)
		{
			OperationQueue operationQueue = AddNewQueue();
			operationQueue.Int32_0 = int_0;
			return operationQueue;
		}

		public OperationQueue AddNewQueue()
		{
			OperationQueue operationQueue = new OperationQueue();
			lock (list_0)
			{
				list_0.Add(operationQueue);
				return operationQueue;
			}
		}

		public OperationQueueSelfThread AddNewThreadQueue()
		{
			OperationQueueSelfThread operationQueueSelfThread = new OperationQueueSelfThread();
			list_1.Add(operationQueueSelfThread);
			operationQueueSelfThread.Run();
			return operationQueueSelfThread;
		}

		public Operation Add(Operation operation_0)
		{
			return Add(operation_0, OperationQueue_1);
		}

		public Operation Add(Operation operation_0, OperationQueue operationQueue_2)
		{
			operationQueue_2.Add(operation_0);
			return operation_0;
		}

		public Operation Add(Action action_1)
		{
			return Add(new ActionOperation(action_1), OperationQueue_1);
		}

		public Operation Add(Action action_1, OperationQueue operationQueue_2)
		{
			return Add(new ActionOperation(action_1), operationQueue_2);
		}

		public Operation AddToNewQueue(Operation operation_0)
		{
			AddNewQueue().Add(operation_0);
			return operation_0;
		}

		public void AddToNewQueue(Operation[] operation_0)
		{
			OperationQueue operationQueue = AddNewQueue();
			for (int i = 0; i < operation_0.Length; i++)
			{
				operationQueue.Add(operation_0[i]);
			}
		}

		public TheradOperation AddToThread(TheradOperation theradOperation_0)
		{
			if (list_1.Count == 0)
			{
				AddToNewThread(theradOperation_0);
			}
			else
			{
				list_1[0].AddToThread(theradOperation_0);
			}
			return theradOperation_0;
		}

		public TheradOperation AddToNewThread(TheradOperation theradOperation_0)
		{
			AddNewThreadQueue().AddToThread(theradOperation_0);
			return theradOperation_0;
		}

		public void AddToNewThread(TheradOperation[] theradOperation_0)
		{
			OperationQueueSelfThread operationQueueSelfThread = AddNewThreadQueue();
			for (int i = 0; i < theradOperation_0.Length; i++)
			{
				operationQueueSelfThread.AddToThread(theradOperation_0[i]);
			}
		}

		public IEnumerator Wait()
		{
			return Wait(OperationQueue_1);
		}

		public IEnumerator Wait(OperationQueue operationQueue_2)
		{
			return Wait(Add(delegate
			{
			}, operationQueue_2));
		}

		public IEnumerator Wait(Operation operation_0)
		{
			while (!operation_0.Boolean_0)
			{
				yield return null;
			}
		}

		public void SetMainQueueToDefault()
		{
			OperationQueue_1 = OperationQueue_0;
		}

		private void Update()
		{
			for (int i = 0; i < list_0.Count; i++)
			{
				OperationQueue operationQueue = list_0[i];
				operationQueue.Update();
				if (operationQueue != OperationQueue_0 && operationQueue != OperationQueue_1 && operationQueue.Int32_1 == 0 && operationQueue.Operation_0 == null)
				{
					list_0.Remove(operationQueue);
					i--;
				}
			}
			for (int j = 0; j < list_1.Count; j++)
			{
				OperationQueueSelfThread operationQueueSelfThread = list_1[j];
				operationQueueSelfThread.MainUpdate();
				if (operationQueueSelfThread.Int32_1 == 0 && operationQueueSelfThread.Int32_2 == 0 && operationQueueSelfThread.Operation_0 == null)
				{
					operationQueueSelfThread.Kill();
					list_1.Remove(operationQueueSelfThread);
					j--;
				}
			}
		}
	}
}
