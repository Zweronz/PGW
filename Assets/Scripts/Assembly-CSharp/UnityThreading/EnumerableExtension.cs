using System;
using System.Collections.Generic;

namespace UnityThreading
{
	public static class EnumerableExtension
	{
		public static IEnumerable<Task> ParallelForEach<T>(this IEnumerable<T> ienumerable_0, Action<T> action_0)
		{
			return ienumerable_0.ParallelForEach(action_0, null);
		}

		public static IEnumerable<Task> ParallelForEach<T>(this IEnumerable<T> ienumerable_0, Action<T> action_0, TaskDistributor taskDistributor_0)
		{
			return (IEnumerable<Task>)ienumerable_0.ParallelForEach(delegate(T gparam_0)
			{
				action_0(gparam_0);
				return default(Task.Unit);
			}, taskDistributor_0);
		}

		public static IEnumerable<Task<TResult>> ParallelForEach<TResult, T>(this IEnumerable<T> ienumerable_0, Func<T, TResult> func_0)
		{
			return ienumerable_0.ParallelForEach(func_0);
		}

		public static IEnumerable<Task<TResult>> ParallelForEach<TResult, T>(this IEnumerable<T> ienumerable_0, Func<T, TResult> func_0, TaskDistributor taskDistributor_0)
		{
			List<Task<TResult>> list = new List<Task<TResult>>();
			foreach (T item2 in ienumerable_0)
			{
				T tmp = item2;
				Task<TResult> item = Task.Create(() => func_0(tmp)).Run(taskDistributor_0);
				list.Add(item);
			}
			return list;
		}

		public static IEnumerable<Task> SequentialForEach<T>(this IEnumerable<T> ienumerable_0, Action<T> action_0)
		{
			return ienumerable_0.SequentialForEach(action_0, null);
		}

		public static IEnumerable<Task> SequentialForEach<T>(this IEnumerable<T> ienumerable_0, Action<T> action_0, TaskDistributor taskDistributor_0)
		{
			return (IEnumerable<Task>)ienumerable_0.SequentialForEach(delegate(T gparam_0)
			{
				action_0(gparam_0);
				return default(Task.Unit);
			}, taskDistributor_0);
		}

		public static IEnumerable<Task<TResult>> SequentialForEach<TResult, T>(this IEnumerable<T> ienumerable_0, Func<T, TResult> func_0)
		{
			return ienumerable_0.SequentialForEach(func_0);
		}

		public static IEnumerable<Task<TResult>> SequentialForEach<TResult, T>(this IEnumerable<T> ienumerable_0, Func<T, TResult> func_0, TaskDistributor taskDistributor_0)
		{
			List<Task<TResult>> list = new List<Task<TResult>>();
			Task task2 = null;
			foreach (T item in ienumerable_0)
			{
				T tmp = item;
				Task<TResult> task = Task.Create(() => func_0(tmp));
				if (task2 == null)
				{
					task.Run(taskDistributor_0);
				}
				else
				{
					task2.WhenEnded((Action)delegate
					{
						task.Run(taskDistributor_0);
					});
				}
				task2 = task;
				list.Add(task);
			}
			return list;
		}
	}
}
