using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using ProtoBuf;
using engine.controllers;
using engine.helpers;

namespace engine.network
{
	public abstract class AbstractNetworkCommand
	{
		private static int int_0;

		protected HashSet<AppStateController.States> hashSet_0 = new HashSet<AppStateController.States>();

		[CompilerGenerated]
		private static Func<Type, bool> func_0;

		[CompilerGenerated]
		private static Func<Type, bool> func_1;

		public abstract NetworkCommandInfo NetworkCommandInfo_0 { get; }

		public static void Init()
		{
			IEnumerable<Type> enumerable = from type_0 in typeof(AbstractNetworkCommand).Assembly.GetTypes()
				where type_0.IsSubclassOf(typeof(AbstractNetworkCommand))
				select type_0;
			foreach (Type item in enumerable)
			{
				item.GetMethod("Init", BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, new object[0]);
			}
		}

		public static void InitTest()
		{
			IEnumerable<Type> enumerable = from type_0 in typeof(AbstractNetworkCommand).Assembly.GetTypes()
				where type_0.IsSubclassOf(typeof(AbstractNetworkCommand))
				select type_0;
			foreach (Type item in enumerable)
			{
				MethodInfo method = item.GetMethod("InitTest", BindingFlags.Static | BindingFlags.NonPublic);
				if (method != null)
				{
					method.Invoke(null, new object[0]);
				}
			}
		}

		public virtual void Run()
		{
			if (NetworkCommandInfo_0 != null && NetworkCommandInfo_0.double_0 > 0.0)
			{
				Utility.setTime(NetworkCommandInfo_0.double_0);
			}
			if (hashSet_0.Count == 0)
			{
				DeferredRun();
				return;
			}
			if (hashSet_0.Contains(AppStateController.AppStateController_0.States_0))
			{
				DeferredRun();
				return;
			}
			foreach (AppStateController.States item in hashSet_0)
			{
				AppStateController.AppStateController_0.Subscribe(DefferedRunInternal, item);
			}
		}

		public virtual void Answered(AbstractNetworkCommand abstractNetworkCommand_0)
		{
		}

		public virtual void HandleError(AbstractNetworkCommand abstractNetworkCommand_0)
		{
		}

		private void DefferedRunInternal()
		{
			foreach (AppStateController.States item in hashSet_0)
			{
				AppStateController.AppStateController_0.Unsubscribe(DefferedRunInternal, item);
			}
			hashSet_0.Clear();
			DeferredRun();
		}

		public virtual void DeferredRun()
		{
		}

		public static void Send<T>(T gparam_0) where T : AbstractNetworkCommand, new()
		{
			gparam_0.NetworkCommandInfo_0.int_1 = ++int_0;
			byte[] byte_ = ToByte(gparam_0);
			BaseConnection.BaseConnection_0.Send(byte_, gparam_0);
		}

		public static void Send<T>() where T : AbstractNetworkCommand, new()
		{
			T gparam_ = new T();
			Send(gparam_);
		}

		public static byte[] ToByte<T>(T gparam_0) where T : AbstractNetworkCommand, new()
		{
			return NetworkCommandWrapper.ToByte(gparam_0);
		}

		public static AbstractNetworkCommand FromByte(byte[] byte_0)
		{
			if (byte_0 != null && byte_0.Length != 0)
			{
				AbstractNetworkCommand abstractNetworkCommand = Serializer.Deserialize<AbstractNetworkCommand>(new MemoryStream(byte_0));
				if (abstractNetworkCommand == null)
				{
					throw new NotImplementedException("Data from byte. Data in message is not data server command!");
				}
				return abstractNetworkCommand;
			}
			throw new ArgumentNullException("Data from byte. Data in message is null or empty!");
		}
	}
}
