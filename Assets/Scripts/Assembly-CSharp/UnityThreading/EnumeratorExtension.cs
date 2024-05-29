using System.Collections;

namespace UnityThreading
{
	public static class EnumeratorExtension
	{
		public static Task RunAsync(this IEnumerator ienumerator_0)
		{
			return ienumerator_0.RunAsync(UnityThreadHelper.TaskDistributor_0);
		}

		public static Task RunAsync(this IEnumerator ienumerator_0, TaskDistributor taskDistributor_0)
		{
			return taskDistributor_0.Dispatch(Task.Create(ienumerator_0));
		}
	}
}
