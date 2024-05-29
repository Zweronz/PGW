using System;
using System.Collections.Generic;
using engine.helpers;
using engine.unity;

namespace engine.operations
{
	public class OperationQueue
	{
		private int int_0 = 50;

		private int int_1;

		protected List<Operation> list_0 = new List<Operation>();

		protected Operation operation_0;

		private OperationQueueProgress operationQueueProgress_0;

		public int Int32_0
		{
			get
			{
				return int_0;
			}
			set
			{
				int_0 = ((value != 0) ? value : 50);
			}
		}

		public OperationQueueProgress OperationQueueProgress_0
		{
			get
			{
				if (operationQueueProgress_0 == null)
				{
					operationQueueProgress_0 = new OperationQueueProgress();
					operationQueueProgress_0.AllOperationsCompleteCallback += delegate
					{
						operationQueueProgress_0 = null;
					};
				}
				return operationQueueProgress_0;
			}
		}

		public Operation Operation_0
		{
			get
			{
				return operation_0;
			}
		}

		public int Int32_1
		{
			get
			{
				return list_0.Count;
			}
		}

		public void Clear()
		{
			list_0.Clear();
			int_1 = 0;
			operation_0 = null;
		}

		public void Add(Operation operation_1)
		{
			if (operation_1 == null)
			{
				Log.AddLine(Utility.GetErrorLocation());
				return;
			}
			lock (list_0)
			{
				list_0.Add(operation_1);
			}
			if (operationQueueProgress_0 == null)
			{
				return;
			}
			lock (operationQueueProgress_0)
			{
				operationQueueProgress_0.Add(operation_1);
			}
		}

		public void Update()
		{
			if (operation_0 == null)
			{
				ExecuteFirstOperation();
			}
			else if (operation_0.Boolean_0)
			{
				operation_0.CompleteInUnityThread();
				lock (list_0)
				{
					list_0.Remove(operation_0);
				}
				operation_0 = null;
				ExecuteFirstOperation();
				if (operationQueueProgress_0 != null)
				{
					operationQueueProgress_0.Update();
				}
			}
		}

		private void ExecuteFirstOperation()
		{
			if (list_0.Count > 0 && int_1 <= int_0)
			{
				Operation operation = list_0[0];
				if (operation == null)
				{
					lock (list_0)
					{
						list_0.RemoveAt(0);
						return;
					}
				}
				int_1++;
				Execute(operation);
			}
			else
			{
				int_1 = 0;
			}
		}

		private void Execute(Operation operation_1)
		{
			operation_0 = operation_1;
			if (operation_0.Boolean_0)
			{
				return;
			}
			try
			{
				operation_1.Start();
				Update();
			}
			catch (Exception exception_)
			{
				Log.AddLine("Error at " + operation_0.GetType().FullName, Log.LogLevel.ERROR);
				MonoSingleton<Log>.Prop_0.DumpError(exception_);
			}
		}
	}
}
