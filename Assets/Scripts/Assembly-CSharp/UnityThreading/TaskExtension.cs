using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityThreading
{
	public static class TaskExtension
	{
		public static Task WithName(this Task task_0, string string_0)
		{
			task_0.string_0 = string_0;
			return task_0;
		}

		public static Task<T> WithName<T>(this Task<T> task_0, string string_0)
		{
			task_0.string_0 = string_0;
			return task_0;
		}

		public static void WaitAll(this IEnumerable<Task> ienumerable_0)
		{
			foreach (Task item in ienumerable_0)
			{
				item.Wait();
			}
		}

		public static IEnumerable<Task> Then(this IEnumerable<Task> ienumerable_0, Task task_0, DispatcherBase dispatcherBase_0)
		{
			int int_0 = ienumerable_0.Count();
			object object_0 = new object();
			foreach (Task item in ienumerable_0)
			{
				item.WhenFailed((Action)delegate
				{
					if (!task_0.Boolean_0)
					{
						task_0.Abort();
					}
				});
				item.WhenSucceeded((Action)delegate
				{
					if (task_0.Boolean_0)
					{
						return;
					}
					lock (object_0)
					{
						int_0--;
						if (int_0 == 0)
						{
							if (dispatcherBase_0 != null)
							{
								task_0.Run(dispatcherBase_0);
							}
							else if (ThreadBase.ThreadBase_0 is TaskWorker)
							{
								task_0.Run(((TaskWorker)ThreadBase.ThreadBase_0).TaskDistributor_0);
							}
							else
							{
								task_0.Run();
							}
						}
					}
				});
			}
			return ienumerable_0;
		}

		public static IEnumerable<Task> WhenSucceeded(this IEnumerable<Task> ienumerable_0, Action action_0, DispatcherBase dispatcherBase_0)
		{
			int int_0 = ienumerable_0.Count();
			object object_0 = new object();
			foreach (Task item in ienumerable_0)
			{
				item.WhenSucceeded((Action)delegate
				{
					lock (object_0)
					{
						int_0--;
						if (int_0 == 0)
						{
							if (dispatcherBase_0 == null)
							{
								action_0();
							}
							else
							{
								dispatcherBase_0.Dispatch(delegate
								{
									action_0();
								});
							}
						}
					}
				});
			}
			return ienumerable_0;
		}

		public static IEnumerable<Task> WhenFailed(this IEnumerable<Task> ienumerable_0, Action action_0, DispatcherBase dispatcherBase_0)
		{
			bool bool_0 = false;
			object object_0 = new object();
			foreach (Task item in ienumerable_0)
			{
				item.WhenFailed((Action)delegate
				{
					lock (object_0)
					{
						if (!bool_0)
						{
							bool_0 = true;
							if (dispatcherBase_0 == null)
							{
								action_0();
							}
							else
							{
								dispatcherBase_0.Dispatch(delegate
								{
									action_0();
								});
							}
						}
					}
				});
			}
			return ienumerable_0;
		}

		public static Task OnResult(this Task task_0, Action<object> action_0)
		{
			return task_0.OnResult(action_0, null);
		}

		public static Task OnResult(this Task task_0, Action<object> action_0, DispatcherBase dispatcherBase_0)
		{
			return task_0.WhenSucceeded(delegate(Task task_1)
			{
				action_0(task_1.Object_0);
			}, dispatcherBase_0);
		}

		public static Task OnResult<T>(this Task task_0, Action<T> action_0)
		{
			return task_0.OnResult(action_0, null);
		}

		public static Task OnResult<T>(this Task task_0, Action<T> action_0, DispatcherBase dispatcherBase_0)
		{
			return task_0.WhenSucceeded(delegate(Task task_1)
			{
				action_0((T)task_1.Object_0);
			}, dispatcherBase_0);
		}

		public static Task<T> OnResult<T>(this Task<T> task_0, Action<T> action_0)
		{
			return task_0.OnResult(action_0, null);
		}

		public static Task<T> OnResult<T>(this Task<T> task_0, Action<T> action_0, DispatcherBase dispatcherBase_0)
		{
			return task_0.WhenSucceeded(delegate(Task<T> task_1)
			{
				action_0(task_1.Prop_0);
			}, dispatcherBase_0);
		}

		public static Task<T> WhenSucceeded<T>(this Task<T> task_0, Action action_0)
		{
			return task_0.WhenSucceeded(delegate
			{
				action_0();
			}, null);
		}

		public static Task<T> WhenSucceeded<T>(this Task<T> task_0, Action<Task<T>> action_0)
		{
			return task_0.WhenSucceeded(action_0, null);
		}

		public static Task<T> WhenSucceeded<T>(this Task<T> task_0, Action<Task<T>> action_0, DispatcherBase dispatcherBase_0)
		{
			Action<Task<T>> perform = delegate(Task<T> task_1)
			{
				if (dispatcherBase_0 == null)
				{
					action_0(task_1);
				}
				else
				{
					dispatcherBase_0.Dispatch(delegate
					{
						if (task_1.Boolean_3)
						{
							action_0(task_1);
						}
					});
				}
			};
			return task_0.WhenEnded(delegate(Task<T> task_1)
			{
				if (task_1.Boolean_3)
				{
					perform(task_1);
				}
			}, null);
		}

		public static Task WhenSucceeded(this Task task_0, Action action_0)
		{
			return task_0.WhenEnded(delegate(Task task_1)
			{
				if (task_1.Boolean_3)
				{
					action_0();
				}
			});
		}

		public static Task WhenSucceeded(this Task task_0, Action<Task> action_0)
		{
			return task_0.WhenEnded(delegate(Task task_1)
			{
				if (task_1.Boolean_3)
				{
					action_0(task_1);
				}
			});
		}

		public static Task WhenSucceeded(this Task task_0, Action<Task> action_0, DispatcherBase dispatcherBase_0)
		{
			Action<Task> action_1 = delegate(Task task_1)
			{
				if (dispatcherBase_0 == null)
				{
					action_0(task_1);
				}
				else
				{
					dispatcherBase_0.Dispatch(delegate
					{
						if (task_0.Boolean_3)
						{
							action_0(task_1);
						}
					});
				}
			};
			return task_0.WhenEnded(delegate(Task task_1)
			{
				if (task_1.Boolean_3)
				{
					action_1(task_1);
				}
			}, null);
		}

		public static Task<T> WhenFailed<T>(this Task<T> task_0, Action action_0)
		{
			return task_0.WhenFailed(delegate
			{
				action_0();
			}, null);
		}

		public static Task<T> WhenFailed<T>(this Task<T> task_0, Action<Task<T>> action_0)
		{
			return task_0.WhenFailed(action_0, null);
		}

		public static Task<T> WhenFailed<T>(this Task<T> task_0, Action<Task<T>> action_0, DispatcherBase dispatcherBase_0)
		{
			return task_0.WhenEnded(delegate(Task<T> task_1)
			{
				if (task_1.Boolean_4)
				{
					action_0(task_1);
				}
			}, dispatcherBase_0);
		}

		public static Task WhenFailed(this Task task_0, Action action_0)
		{
			return task_0.WhenEnded(delegate(Task task_1)
			{
				if (task_1.Boolean_4)
				{
					action_0();
				}
			});
		}

		public static Task WhenFailed(this Task task_0, Action<Task> action_0)
		{
			return task_0.WhenEnded(delegate(Task task_1)
			{
				if (task_1.Boolean_4)
				{
					action_0(task_1);
				}
			});
		}

		public static Task WhenFailed(this Task task_0, Action<Task> action_0, DispatcherBase dispatcherBase_0)
		{
			return task_0.WhenEnded(delegate(Task task_1)
			{
				if (task_1.Boolean_4)
				{
					action_0(task_1);
				}
			}, dispatcherBase_0);
		}

		public static Task<T> WhenEnded<T>(this Task<T> task_0, Action action_0)
		{
			return task_0.WhenEnded(delegate
			{
				action_0();
			}, null);
		}

		public static Task<T> WhenEnded<T>(this Task<T> task_0, Action<Task<T>> action_0)
		{
			return task_0.WhenEnded(action_0, null);
		}

		public static Task<T> WhenEnded<T>(this Task<T> task_0, Action<Task<T>> action_0, DispatcherBase dispatcherBase_0)
		{
			task_0.TaskEnded += delegate
			{
				if (dispatcherBase_0 == null)
				{
					action_0(task_0);
				}
				else
				{
					dispatcherBase_0.Dispatch(delegate
					{
						action_0(task_0);
					});
				}
			};
			return task_0;
		}

		public static Task WhenEnded(this Task task_0, Action action_0)
		{
			return task_0.WhenEnded((Action<Task>)delegate
			{
				action_0();
			});
		}

		public static Task WhenEnded(this Task task_0, Action<Task> action_0)
		{
			return task_0.WhenEnded(delegate(Task task_1)
			{
				action_0(task_1);
			}, null);
		}

		public static Task WhenEnded(this Task task_0, Action<Task> action_0, DispatcherBase dispatcherBase_0)
		{
			task_0.TaskEnded += delegate
			{
				if (dispatcherBase_0 == null)
				{
					action_0(task_0);
				}
				else
				{
					dispatcherBase_0.Dispatch(delegate
					{
						action_0(task_0);
					});
				}
			};
			return task_0;
		}

		public static Task Then(this Task task_0, Task task_1)
		{
			TaskDistributor dispatcherBase_ = null;
			if (ThreadBase.ThreadBase_0 is TaskWorker)
			{
				dispatcherBase_ = ((TaskWorker)ThreadBase.ThreadBase_0).TaskDistributor_0;
			}
			return task_0.Then(task_1, dispatcherBase_);
		}

		public static Task Then(this Task task_0, Task task_1, DispatcherBase dispatcherBase_0)
		{
			task_0.WhenFailed((Action)delegate
			{
				task_1.Abort();
			});
			task_0.WhenSucceeded((Action)delegate
			{
				if (dispatcherBase_0 != null)
				{
					task_1.Run(dispatcherBase_0);
				}
				else if (ThreadBase.ThreadBase_0 is TaskWorker)
				{
					task_1.Run(((TaskWorker)ThreadBase.ThreadBase_0).TaskDistributor_0);
				}
				else
				{
					task_1.Run();
				}
			});
			return task_0;
		}

		public static Task Await(this Task task_0, Task task_1)
		{
			task_1.Then(task_0);
			return task_0;
		}

		public static Task Await(this Task task_0, Task task_1, DispatcherBase dispatcherBase_0)
		{
			task_1.Then(task_0, dispatcherBase_0);
			return task_0;
		}

		public static Task<T> As<T>(this Task task_0)
		{
			return (Task<T>)task_0;
		}

		public static IEnumerable<Task> ContinueWhenAnyEnded(this IEnumerable<Task> ienumerable_0, Action action_0)
		{
			return ienumerable_0.ContinueWhenAnyEnded((Action<Task>)delegate
			{
				action_0();
			});
		}

		public static IEnumerable<Task> ContinueWhenAnyEnded(this IEnumerable<Task> ienumerable_0, Action<Task> action_0)
		{
			object object_0 = new object();
			bool bool_0 = false;
			foreach (Task item in ienumerable_0)
			{
				item.WhenEnded(delegate(Task task_1)
				{
					lock (object_0)
					{
						if (!bool_0)
						{
							bool_0 = true;
							action_0(task_1);
						}
					}
				});
			}
			return ienumerable_0;
		}

		public static IEnumerable<Task> ContinueWhenAllEnded(this IEnumerable<Task> ienumerable_0, Action action_0)
		{
			return ienumerable_0.ContinueWhenAllEnded((Action<IEnumerable<Task>>)delegate
			{
				action_0();
			});
		}

		public static IEnumerable<Task> ContinueWhenAllEnded(this IEnumerable<Task> ienumerable_0, Action<IEnumerable<Task>> action_0)
		{
			int int_0 = ienumerable_0.Count();
			if (int_0 == 0)
			{
				action_0(new Task[0]);
			}
			List<Task> list_0 = new List<Task>();
			object object_0 = new object();
			foreach (Task task_2 in ienumerable_0)
			{
				task_2.WhenEnded((Action<Task>)delegate
				{
					lock (object_0)
					{
						list_0.Add(task_2);
						if (list_0.Count == int_0)
						{
							action_0(list_0);
						}
					}
				});
			}
			return ienumerable_0;
		}
	}
}
