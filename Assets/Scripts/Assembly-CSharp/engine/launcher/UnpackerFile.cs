using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using UnityThreading;
using engine.events;
using engine.helpers;
using engine.unity;

namespace engine.launcher
{
	public sealed class UnpackerFile : BaseEvent<UnpackerFileEventArgs>
	{
		public enum Events
		{
			DECOMPRESSING = 0,
			PROGRESS = 1,
			PREPARE = 2,
			COMPLETE = 3,
			ERROR = 4,
			STOP = 5
		}

		private static UnpackerFile unpackerFile_0;

		public readonly string string_0 = "update";

		private bool bool_0;

		private bool bool_1 = true;

		private ActionThread actionThread_0;

		private UnpackerFileEventArgs unpackerFileEventArgs_0 = new UnpackerFileEventArgs();

		private AppVersionInfo appVersionInfo_0;

		public static UnpackerFile UnpackerFile_0
		{
			get
			{
				if (unpackerFile_0 == null)
				{
					unpackerFile_0 = new UnpackerFile();
				}
				return unpackerFile_0;
			}
		}

		public void Unpack(AppVersionInfo appVersionInfo_1, bool bool_2 = true)
		{
			if (bool_0)
			{
				Log.AddLine("[UnpackerFile is busy!]", Log.LogLevel.WARNING);
				return;
			}
			if (appVersionInfo_1 == null)
			{
				Log.AddLine("[UnpackerFileEventArgs needs app version info for work!]");
				return;
			}
			bool_1 = bool_2;
			appVersionInfo_0 = appVersionInfo_1;
			bool_0 = true;
			actionThread_0 = null;
			try
			{
				actionThread_0 = UnityThreadHelper.CreateThread((Action)delegate
				{
					Process();
				});
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				Exception exception_0 = ex2;
				actionThread_0 = null;
				UnityThreadHelper.Dispatcher_0.Dispatch(delegate
				{
					unpackerFileEventArgs_0.string_0 = string.Format("[UnpackerFile({0}) Create thread exception error!]", appVersionInfo_0.GetPackageName());
					DispatchOfType(Events.ERROR, unpackerFileEventArgs_0);
					Reset();
					MonoSingleton<Log>.Prop_0.DumpError(exception_0);
				});
			}
		}

		private void Process()
		{
			try
			{
				if (CheckPackage())
				{
					UnpackZip();
					if (bool_1)
					{
						MovingFilesAndFolders();
					}
					UnityThreadHelper.Dispatcher_0.Dispatch(delegate
					{
						Reset();
						DispatchOfType(Events.COMPLETE, null);
					});
				}
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				Exception exception_0 = ex2;
				UnityThreadHelper.Dispatcher_0.Dispatch(delegate
				{
					unpackerFileEventArgs_0.string_0 = string.Format("[UnpackerFile({0}) exception error!]", appVersionInfo_0.GetPackageName());
					DispatchOfType(Events.ERROR, unpackerFileEventArgs_0);
					Reset();
					MonoSingleton<Log>.Prop_0.DumpError(exception_0);
				});
			}
		}

		private bool CheckPackage()
		{
			string string_0 = appVersionInfo_0.GetPackageName();
			Log.AddLine(string.Format("[UnpackerFile|CheckPackage]: {0}", string_0));
			if (!File.Exists(string_0))
			{
				UnityThreadHelper.Dispatcher_0.Dispatch(delegate
				{
					unpackerFileEventArgs_0.string_0 = string.Format("File package({0}) not found", string_0);
					DispatchOfType(Events.ERROR, unpackerFileEventArgs_0);
					Reset();
				});
				return false;
			}
			return true;
		}

		private void UnpackZip()
		{
			ZipFile zipFile_0 = new ZipFile(appVersionInfo_0.GetPackageName());
			try
			{
				UnityThreadHelper.Dispatcher_0.Dispatch(delegate
				{
					unpackerFileEventArgs_0.float_0 = 0f;
					unpackerFileEventArgs_0.long_0 = zipFile_0.Count;
					DispatchOfType(Events.DECOMPRESSING, unpackerFileEventArgs_0);
				});
				if (Directory.Exists(string_0))
				{
					Utility.DeleteDirectory(string_0);
				}
				string path = Path.Combine(string_0, appVersionInfo_0.string_4);
				foreach (ZipEntry item in zipFile_0)
				{
					if (actionThread_0 != null && actionThread_0.Boolean_1)
					{
						break;
					}
					Stream inputStream = zipFile_0.GetInputStream(item);
					byte[] buffer = new byte[4096];
					string name = item.Name;
					Log.AddLine(string.Format("[UnpackerFile|UnpackZip unzip file]: {0}", name));
					string text = Path.Combine(path, name);
					string directoryName = Path.GetDirectoryName(text);
					if (!string.IsNullOrEmpty(directoryName) && !Directory.Exists(directoryName))
					{
						Log.AddLine(string.Format("[UnpackerFile|UnpackZip create dir]: {0}", directoryName));
						Directory.CreateDirectory(directoryName);
					}
					if (!text.EndsWith("/"))
					{
						using (FileStream destination = File.Create(text))
						{
							StreamUtils.Copy(inputStream, destination, buffer);
						}
					}
					UnityThreadHelper.Dispatcher_0.Dispatch(delegate
					{
						unpackerFileEventArgs_0.float_0 += 1f;
						DispatchOfType(Events.PROGRESS, unpackerFileEventArgs_0);
					});
				}
			}
			finally
			{
				if (zipFile_0 != null)
				{
					((IDisposable)zipFile_0).Dispose();
				}
			}
		}

		private void MovingFilesAndFolders()
		{
			UnityThreadHelper.Dispatcher_0.Dispatch(delegate
			{
				DispatchOfType(Events.PREPARE, unpackerFileEventArgs_0);
			});
			string string_ = appVersionInfo_0.string_4;
			Log.AddLine(string.Format("[UnpackerFile. localPath]: {0}", string_));
			string text = Path.Combine(string_0, string_);
			Utility.SetChmod(Path.Combine(text, appVersionInfo_0.string_3), "755");
			Log.AddLine(string.Format("[UnpackerFile. localUpdatePath]: {0}", text));
			if (!string.IsNullOrEmpty(string_) && !Directory.Exists(string_))
			{
				Directory.CreateDirectory(string_);
			}
			File.Delete(appVersionInfo_0.GetPackageName());
			Utility.MovingDirectoryAndFiles(text, string_, true);
			Directory.Delete(string_0, true);
		}

		private void Reset()
		{
			appVersionInfo_0 = null;
			unpackerFileEventArgs_0.float_0 = 0f;
			unpackerFileEventArgs_0.long_0 = 0L;
			unpackerFileEventArgs_0.string_0 = string.Empty;
			bool_0 = false;
			bool_1 = true;
		}

		public void StopUnpacking()
		{
			if (actionThread_0 != null)
			{
				actionThread_0.Abort();
			}
			Reset();
			UnityThreadHelper.Dispatcher_0.Dispatch(delegate
			{
				DispatchOfType(Events.STOP, unpackerFileEventArgs_0);
			});
		}
	}
}
