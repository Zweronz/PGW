using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityThreading;

public class UnityThreadHelper : MonoBehaviour
{
	private static UnityThreadHelper unityThreadHelper_0 = null;

	private static object object_0 = new object();

	private Dispatcher dispatcher_0;

	private TaskDistributor taskDistributor_0;

	private List<ThreadBase> list_0 = new List<ThreadBase>();

	[CompilerGenerated]
	private static Func<ThreadBase, bool> func_0;

	private static UnityThreadHelper UnityThreadHelper_0
	{
		get
		{
			EnsureHelper();
			return unityThreadHelper_0;
		}
	}

	public static Dispatcher Dispatcher_0
	{
		get
		{
			return UnityThreadHelper_0.Dispatcher_1;
		}
	}

	public static TaskDistributor TaskDistributor_0
	{
		get
		{
			return UnityThreadHelper_0.TaskDistributor_1;
		}
	}

	public Dispatcher Dispatcher_1
	{
		get
		{
			return dispatcher_0;
		}
	}

	public TaskDistributor TaskDistributor_1
	{
		get
		{
			return taskDistributor_0;
		}
	}

	public static void EnsureHelper()
	{
		lock (object_0)
		{
			if ((object)unityThreadHelper_0 == null)
			{
				unityThreadHelper_0 = UnityEngine.Object.FindObjectOfType(typeof(UnityThreadHelper)) as UnityThreadHelper;
				if ((object)unityThreadHelper_0 == null)
				{
					GameObject gameObject = new GameObject("[UnityThreadHelper]");
					unityThreadHelper_0 = gameObject.AddComponent<UnityThreadHelper>();
					unityThreadHelper_0.EnsureHelperInstance();
					UnityEngine.Object.DontDestroyOnLoad(unityThreadHelper_0.gameObject);
				}
			}
		}
	}

	private void EnsureHelperInstance()
	{
		dispatcher_0 = Dispatcher.Dispatcher_3 ?? new Dispatcher();
		taskDistributor_0 = TaskDistributor.TaskDistributor_1 ?? new TaskDistributor("TaskDistributor");
	}

	public static ActionThread CreateThread(Action<ActionThread> action_0, bool bool_0)
	{
		UnityThreadHelper_0.EnsureHelperInstance();
		Action<ActionThread> action_ = delegate(ActionThread actionThread_0)
		{
			try
			{
				action_0(actionThread_0);
			}
			catch (Exception message)
			{
				Debug.LogError(message);
			}
		};
		ActionThread actionThread = new ActionThread(action_, bool_0);
		UnityThreadHelper_0.RegisterThread(actionThread);
		return actionThread;
	}

	public static ActionThread CreateThread(Action<ActionThread> action_0)
	{
		return CreateThread(action_0, true);
	}

	public static ActionThread CreateThread(Action action_0, bool bool_0)
	{
		return CreateThread((Action<ActionThread>)delegate
		{
			action_0();
		}, bool_0);
	}

	public static ActionThread CreateThread(Action action_0)
	{
		return CreateThread((Action<ActionThread>)delegate
		{
			action_0();
		}, true);
	}

	public static ThreadBase CreateThread(Func<ThreadBase, IEnumerator> func_1, bool bool_0)
	{
		UnityThreadHelper_0.EnsureHelperInstance();
		EnumeratableActionThread enumeratableActionThread = new EnumeratableActionThread(func_1, bool_0);
		UnityThreadHelper_0.RegisterThread(enumeratableActionThread);
		return enumeratableActionThread;
	}

	public static ThreadBase CreateThread(Func<ThreadBase, IEnumerator> func_1)
	{
		return CreateThread(func_1, true);
	}

	public static ThreadBase CreateThread(Func<IEnumerator> func_1, bool bool_0)
	{
		Func<ThreadBase, IEnumerator> func_2 = (ThreadBase threadBase_0) => func_1();
		return CreateThread(func_2, bool_0);
	}

	public static ThreadBase CreateThread(Func<IEnumerator> func_1)
	{
		Func<ThreadBase, IEnumerator> func_2 = (ThreadBase threadBase_0) => func_1();
		return CreateThread(func_2, true);
	}

	private void RegisterThread(ThreadBase threadBase_0)
	{
		if (!list_0.Contains(threadBase_0))
		{
			list_0.Add(threadBase_0);
		}
	}

	private void OnDestroy()
	{
		foreach (ThreadBase item in list_0)
		{
			item.Dispose();
		}
		if (dispatcher_0 != null)
		{
			dispatcher_0.Dispose();
		}
		dispatcher_0 = null;
		if (taskDistributor_0 != null)
		{
			taskDistributor_0.Dispose();
		}
		taskDistributor_0 = null;
		if (unityThreadHelper_0 == this)
		{
			unityThreadHelper_0 = null;
		}
	}

	private void OnApplicationQuit()
	{
		OnDestroy();
	}

	private void Update()
	{
		if (dispatcher_0 != null)
		{
			dispatcher_0.ProcessTasks();
		}
		ThreadBase[] array = list_0.Where((ThreadBase threadBase_0) => !threadBase_0.Boolean_0).ToArray();
		ThreadBase[] array2 = array;
		foreach (ThreadBase threadBase in array2)
		{
			threadBase.Dispose();
			list_0.Remove(threadBase);
		}
	}
}
