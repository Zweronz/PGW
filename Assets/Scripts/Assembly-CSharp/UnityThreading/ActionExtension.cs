using System;

namespace UnityThreading
{
	public static class ActionExtension
	{
		public static Task RunAsync(this Action action_0)
		{
			return action_0.RunAsync(UnityThreadHelper.TaskDistributor_0);
		}

		public static Task RunAsync(this Action action_0, TaskDistributor taskDistributor_0)
		{
			return taskDistributor_0.Dispatch(action_0);
		}

		public static Task AsTask(this Action action_0)
		{
			return Task.Create(action_0);
		}

		public static Task<T> RunAsync<T>(this Func<T> func_0)
		{
			return func_0.RunAsync(UnityThreadHelper.TaskDistributor_0);
		}

		public static Task<T> RunAsync<T>(this Func<T> func_0, TaskDistributor taskDistributor_0)
		{
			return taskDistributor_0.Dispatch(func_0);
		}

		public static Task<T> AsTask<T>(this Func<T> func_0)
		{
			return new Task<T>(func_0);
		}
	}
}
