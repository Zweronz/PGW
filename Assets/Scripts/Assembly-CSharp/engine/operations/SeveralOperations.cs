using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace engine.operations
{
	public class SeveralOperations : Operation
	{
		private bool bool_6;

		private List<Operation> list_1;

		public override bool Boolean_4
		{
			get
			{
				return bool_6;
			}
			set
			{
				bool_6 = value;
				if (bool_6 && list_1.Count > 0)
				{
					list_1[0].Boolean_4 = true;
				}
			}
		}

		public SeveralOperations(params Operation[] operation_0)
		{
			if (operation_0.Length > 0)
			{
				list_1 = new List<Operation>(operation_0);
			}
			else
			{
				list_1 = new List<Operation>();
			}
		}

		public void Add(Action action_0)
		{
			Add(new ActionOperation(action_0));
		}

		public void Add(Operation operation_0)
		{
			list_1.Add(operation_0);
		}

		protected override void Execute()
		{
			if (list_1.Count == 0)
			{
				OperationComplete();
				return;
			}
			Operation operation = list_1[0];
			operation.Subscribe(OperationComplete, StatusEvent.Complete);
			OperationsManager.OperationsManager_0.AddToNewQueue(operation);
		}

		protected void OperationComplete(Operation operation_0 = null)
		{
			if (Boolean_4)
			{
				list_1.Clear();
			}
			if (list_1.Count != 0)
			{
				list_1.RemoveAt(0);
			}
			if (list_1.Count == 0)
			{
				Complete();
			}
			else
			{
				Execute();
			}
		}
	}
}
