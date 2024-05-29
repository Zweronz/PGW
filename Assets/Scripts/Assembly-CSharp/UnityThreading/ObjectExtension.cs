namespace UnityThreading
{
	public static class ObjectExtension
	{
		public static Task RunAsync(this object object_0, string string_0, params object[] object_1)
		{
			return object_0.RunAsync<object>(string_0, null, object_1);
		}

		public static Task RunAsync(this object object_0, string string_0, TaskDistributor taskDistributor_0, params object[] object_1)
		{
			return object_0.RunAsync<object>(string_0, taskDistributor_0, object_1);
		}

		public static Task<T> RunAsync<T>(this object object_0, string string_0, params object[] object_1)
		{
			return object_0.RunAsync<T>(string_0, null, object_1);
		}

		public static Task<T> RunAsync<T>(this object object_0, string string_0, TaskDistributor taskDistributor_0, params object[] object_1)
		{
			return Task.Create<T>(object_0, string_0, object_1).Run(taskDistributor_0);
		}
	}
}
