using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using engine.events;
using engine.filesystem;

namespace engine.operations
{
	public class LoadLocalFileOperation : LoadFileOperation
	{
		private static readonly int int_0 = 1024;

		[ThreadStatic]
		private static byte[] byte_0;

		private List<byte[]> list_1;

		private FileStream fileStream_0;

		private int int_1;

		private int int_2;

		private int int_3;

		private bool bool_6;

		private AsyncCallback asyncCallback_0;

		private string string_1;

		public LoadLocalFileOperation(string string_2, BaseAssetFile baseAssetFile_1)
		{
			string_1 = string_2;
			base.BaseAssetFile_0 = baseAssetFile_1;
		}

		protected override void Execute()
		{
			string text = string_1 + base.BaseAssetFile_0.Name.String_2;
			FileInfo fileInfo = new FileInfo(text);
			int_1 = (int)fileInfo.Length;
			int_2 = 0;
			int_3 = 0;
			fileStream_0 = new FileStream(text, FileMode.Open, FileAccess.Read, FileShare.Read, int_0, true);
			asyncCallback_0 = ReadCallback;
			if (byte_0 == null)
			{
				byte_0 = new byte[int_0];
			}
			list_1 = new List<byte[]>();
			DependSceneEvent<ChangeSceneUnityEvent>.GlobalSubscribe(Dispose);
			BeginRead();
		}

		private void Dispose()
		{
			base.BaseAssetFile_0.Destroy();
			if (list_1 != null)
			{
				list_1.Clear();
				list_1 = null;
				fileStream_0.Dispose();
			}
		}

		public override void Update()
		{
			if (bool_6)
			{
				bool_6 = false;
				BeginRead();
			}
		}

		private void BeginRead()
		{
			int numBytes = Mathf.Min(int_0, int_1 - int_2);
			fileStream_0.BeginRead(byte_0, 0, numBytes, asyncCallback_0, null);
		}

		private void ReadCallback(IAsyncResult iasyncResult_0)
		{
			int num = fileStream_0.EndRead(iasyncResult_0);
			int_2 += num;
			byte[] array = new byte[num];
			Array.Copy(byte_0, 0, array, 0, num);
			list_1.Add(array);
			if (int_2 == int_1)
			{
				base.BaseAssetFile_0.Bytes = new byte[int_1];
				int num2 = 0;
				for (int i = 0; i < list_1.Count; i++)
				{
					Buffer.BlockCopy(list_1[i], 0, base.BaseAssetFile_0.Bytes, num2, list_1[i].Length);
					num2 += list_1[i].Length;
				}
				base.BaseAssetFile_0.IsLoaded = true;
				list_1.Clear();
				list_1 = null;
				fileStream_0.Dispose();
				Complete();
			}
			else
			{
				int_3++;
				if (int_3 == 1024)
				{
					float num3 = int_2;
					float num4 = int_1;
					base.ProgressEvent_0.Dispatch(num3 / num4);
				}
				else
				{
					BeginRead();
				}
			}
		}
	}
}
