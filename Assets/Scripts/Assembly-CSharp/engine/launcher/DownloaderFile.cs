using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using engine.events;
using engine.helpers;

namespace engine.launcher
{
	public class DownloaderFile : BaseEvent<DownloaderFileEventArgs>
	{
		public enum Events
		{
			START_CALCULATE_SIZE = 0,
			COMPLETE_CALCULATE_SIZE = 1,
			START = 2,
			STARTED = 3,
			PAUSED = 4,
			RESUMED = 5,
			PROGRESS = 6,
			STOPPED = 7,
			CANCEL = 8,
			CANCELED = 9,
			COMPLETE = 10,
			ERROR = 11
		}

		private enum InternalEvents
		{
			FileDownloadStart = 0,
			FileDownloadStarted = 1,
			FileDownloadStopped = 2,
			FileDownloadProgressChanged = 3,
			CalculationFileSizesStarted = 4,
			FileSizesCalculationComplete = 5
		}

		private enum InvokeType
		{
			FileDownloadState = 0,
			FileDownloadFailed = 1
		}

		private const int int_0 = 2;

		private static DownloaderFile downloaderFile_0;

		private DownloaderFileEventArgs downloaderFileEventArgs_0 = new DownloaderFileEventArgs();

		private BackgroundWorker backgroundWorker_0 = new BackgroundWorker();

		private bool bool_0;

		private bool bool_1;

		private bool bool_2;

		private int int_1;

		[CompilerGenerated]
		private string string_0;

		[CompilerGenerated]
		private string string_1;

		[CompilerGenerated]
		private string string_2;

		[CompilerGenerated]
		private long long_0;

		[CompilerGenerated]
		private long long_1;

		[CompilerGenerated]
		private bool bool_3;

		public static DownloaderFile DownloaderFile_0
		{
			get
			{
				if (downloaderFile_0 == null)
				{
					downloaderFile_0 = new DownloaderFile();
				}
				return downloaderFile_0;
			}
		}

		public bool Boolean_0
		{
			get
			{
				return bool_1;
			}
			set
			{
				if (Boolean_0 != value)
				{
					bool_1 = value;
					Boolean_6 = !value;
					if (Boolean_0)
					{
						backgroundWorker_0.RunWorkerAsync();
						return;
					}
					bool_0 = false;
					backgroundWorker_0.CancelAsync();
					DispatchOfType(Events.CANCEL, null);
				}
			}
		}

		public bool Boolean_1
		{
			get
			{
				return bool_0;
			}
			set
			{
				if (!Boolean_0)
				{
					throw new InvalidOperationException("You can not change the IsPaused property when the DownloaderFile is not busy");
				}
				if (Boolean_1 != value)
				{
					bool_0 = value;
					if (Boolean_1)
					{
						DispatchOfType(Events.PAUSED, null);
					}
					else
					{
						DispatchOfType(Events.RESUMED, null);
					}
				}
			}
		}

		public bool Boolean_2
		{
			get
			{
				return !Boolean_0;
			}
		}

		public bool Boolean_3
		{
			get
			{
				return Boolean_0 && !Boolean_1 && !backgroundWorker_0.CancellationPending;
			}
		}

		public bool Boolean_4
		{
			get
			{
				return Boolean_0 && Boolean_1 && !backgroundWorker_0.CancellationPending;
			}
		}

		public bool Boolean_5
		{
			get
			{
				return Boolean_0 && !backgroundWorker_0.CancellationPending;
			}
		}

		public string String_0
		{
			[CompilerGenerated]
			get
			{
				return string_0;
			}
			[CompilerGenerated]
			private set
			{
				string_0 = value;
			}
		}

		public string String_1
		{
			[CompilerGenerated]
			get
			{
				return string_1;
			}
			[CompilerGenerated]
			private set
			{
				string_1 = value;
			}
		}

		public string String_2
		{
			[CompilerGenerated]
			get
			{
				return string_2;
			}
			[CompilerGenerated]
			private set
			{
				string_2 = value;
			}
		}

		public long Int64_0
		{
			[CompilerGenerated]
			get
			{
				return long_0;
			}
			[CompilerGenerated]
			private set
			{
				long_0 = value;
			}
		}

		public long Int64_1
		{
			[CompilerGenerated]
			get
			{
				return long_1;
			}
			[CompilerGenerated]
			private set
			{
				long_1 = value;
			}
		}

		public int Int32_0
		{
			get
			{
				return AverageSpeedNetwork.Int32_0;
			}
		}

		public bool Boolean_6
		{
			[CompilerGenerated]
			get
			{
				return bool_3;
			}
			[CompilerGenerated]
			private set
			{
				bool_3 = value;
			}
		}

		public int Int32_1
		{
			get
			{
				return int_1;
			}
			set
			{
				if (value <= 0)
				{
					throw new InvalidOperationException("The PackageSize needs to be greather then 0");
				}
				int_1 = value;
			}
		}

		private DownloaderFile()
		{
			initizalize();
		}

		public double FileProgressPercentage()
		{
			return FileProgressPercentage(2);
		}

		public double FileProgressPercentage(int int_2)
		{
			return Math.Round((double)Int64_1 / (double)Int64_0 * 100.0, int_2);
		}

		private void initizalize()
		{
			backgroundWorker_0.WorkerReportsProgress = true;
			backgroundWorker_0.WorkerSupportsCancellation = true;
			backgroundWorker_0.DoWork += WorkerDoWork;
			backgroundWorker_0.RunWorkerCompleted += WorkerComplete;
			backgroundWorker_0.ProgressChanged += WorkerProgress;
			Int32_1 = 4096;
		}

		public void Start(string string_3, bool bool_4 = false, string string_4 = "")
		{
			if (Boolean_0)
			{
				throw new InvalidOperationException("You can not start loadinf file when the DownloaderFile is busy");
			}
			Uri uri_ = null;
			if (!TryCreateUri(string_3, out uri_))
			{
				Log.AddLine("Downloader needs valid url! url = " + string_3, Log.LogLevel.ERROR);
				return;
			}
			Log.AddLine("[DownloaderFile. URL]: " + string_3);
			bool_2 = bool_4;
			String_0 = string_3;
			String_2 = Path.GetFileName(uri_.LocalPath);
			String_1 = string_4;
			if (!bool_2)
			{
				string path = Path.Combine(String_1, String_2);
				if (File.Exists(path))
				{
					File.Delete(path);
				}
			}
			Boolean_0 = true;
		}

		public void Pause()
		{
			Boolean_1 = true;
		}

		public void Resume()
		{
			Boolean_1 = false;
		}

		public void Stop()
		{
			Boolean_0 = false;
		}

		public string FormatSizeBinary(long long_2)
		{
			return FormatSizeBinary(long_2, 2);
		}

		public string FormatSizeBinary(long long_2, int int_2)
		{
			string[] array = new string[9] { "B", "KiB", "MiB", "GiB", "TiB", "PiB", "EiB", "ZiB", "YiB" };
			double num = long_2;
			int num2 = 0;
			while (!(num < 1024.0) && num2 < array.Length)
			{
				num /= 1024.0;
				num2++;
			}
			return Math.Round(num, int_2) + array[num2];
		}

		public string FormatSizeDecimal(long long_2)
		{
			return FormatSizeDecimal(long_2, 2);
		}

		public string FormatSizeDecimal(long long_2, int int_2)
		{
			string[] array = new string[9] { "B", "kB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
			double num = long_2;
			int num2 = 0;
			while (!(num < 1000.0) && num2 < array.Length)
			{
				num /= 1000.0;
				num2++;
			}
			return Math.Round(num, int_2) + array[num2];
		}

		public bool TryCreateUri(string string_3, out Uri uri_0)
		{
			uri_0 = null;
			bool result;
			if (result = !string.IsNullOrEmpty(string_3))
			{
				result = Uri.TryCreate(string_3, UriKind.Absolute, out uri_0);
			}
			return result;
		}

		private void WorkerDoWork(object sender, DoWorkEventArgs e)
		{
			if (!string.IsNullOrEmpty(String_1) && !Directory.Exists(String_1))
			{
				Directory.CreateDirectory(String_1);
			}
			DownloadFile();
		}

		private void WorkerComplete(object sender, RunWorkerCompletedEventArgs e)
		{
			bool_0 = false;
			bool_1 = false;
			AverageSpeedNetwork.Reset();
			Events events_0 = Events.COMPLETE;
			if (Boolean_6)
			{
				events_0 = Events.CANCELED;
			}
			UnityThreadHelper.Dispatcher_0.Dispatch(delegate
			{
				DispatchOfType(events_0, null);
			});
		}

		private void WorkerProgress(object sender, ProgressChangedEventArgs e)
		{
			UnityThreadHelper.Dispatcher_0.Dispatch(delegate
			{
				switch ((InvokeType)e.ProgressPercentage)
				{
				case InvokeType.FileDownloadFailed:
					downloaderFileEventArgs_0.string_0 = string.Format("[Exception message]: {0} \n [Stacktrace]: {1}", ((Exception)e.UserState).Message, ((Exception)e.UserState).StackTrace);
					DispatchOfType(Events.ERROR, downloaderFileEventArgs_0);
					downloaderFileEventArgs_0.Reset();
					break;
				case InvokeType.FileDownloadState:
					switch ((InternalEvents)(int)e.UserState)
					{
					case InternalEvents.FileDownloadStart:
						DispatchOfType(Events.START, null);
						break;
					case InternalEvents.FileDownloadStarted:
						DispatchOfType(Events.STARTED, null);
						break;
					case InternalEvents.FileDownloadStopped:
						DispatchOfType(Events.STOPPED, null);
						break;
					case InternalEvents.FileDownloadProgressChanged:
						AverageSpeedNetwork.Int64_0 = Int64_1;
						downloaderFileEventArgs_0.double_0 = FileProgressPercentage();
						downloaderFileEventArgs_0.long_0 = Int64_1;
						downloaderFileEventArgs_0.long_1 = Int64_0;
						DispatchOfType(Events.PROGRESS, downloaderFileEventArgs_0);
						downloaderFileEventArgs_0.Reset();
						break;
					case InternalEvents.CalculationFileSizesStarted:
						DispatchOfType(Events.START_CALCULATE_SIZE, null);
						break;
					case InternalEvents.FileSizesCalculationComplete:
						downloaderFileEventArgs_0.long_1 = Int64_0;
						DispatchOfType(Events.COMPLETE_CALCULATE_SIZE, null);
						downloaderFileEventArgs_0.Reset();
						break;
					}
					break;
				}
			});
		}

		private void DownloadFile()
		{
			FireStateEventToMainThread(InternalEvents.FileDownloadStart);
			Int64_1 = 0L;
			long num = 0L;
			string text = Path.Combine(String_1, String_2);
			if (File.Exists(text))
			{
				FileInfo fileInfo = new FileInfo(text);
				num = fileInfo.Length;
			}
			HttpWebResponse httpWebResponse = null;
			HttpWebRequest httpWebRequest = null;
			if (num > 0L)
			{
				FireStateEventToMainThread(InternalEvents.CalculationFileSizesStarted);
				bool flag = false;
				try
				{
					httpWebRequest = (HttpWebRequest)WebRequest.Create(String_0);
					httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
					Int64_0 = httpWebResponse.ContentLength;
					httpWebResponse.Close();
					if (Int64_0 > num)
					{
						Int64_1 = num;
						flag = true;
					}
				}
				catch (Exception userState)
				{
					backgroundWorker_0.ReportProgress(1, userState);
					FireStateEventToMainThread(InternalEvents.FileDownloadStopped);
					return;
				}
				FireStateEventToMainThread(InternalEvents.FileSizesCalculationComplete);
				if (!flag)
				{
					FireStateEventToMainThread(InternalEvents.FileDownloadStopped);
					return;
				}
			}
			FileStream fileStream = null;
			fileStream = ((num <= 0L) ? new FileStream(text, FileMode.Create, FileAccess.Write, FileShare.ReadWrite) : new FileStream(text, FileMode.Append, FileAccess.Write, FileShare.ReadWrite));
			Exception ex = null;
			try
			{
				httpWebRequest = (HttpWebRequest)WebRequest.Create(String_0);
				httpWebRequest.ServicePoint.ConnectionLimit = 500;
				if (num > 0L)
				{
					httpWebRequest.AddRange((int)num);
				}
				httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				Int64_0 = httpWebResponse.ContentLength + num;
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			FireStateEventToMainThread(InternalEvents.FileDownloadStarted);
			byte[] array = new byte[Int32_1];
			int num2 = 0;
			if (ex != null)
			{
				backgroundWorker_0.ReportProgress(1, ex);
				fileStream.Close();
				fileStream.Dispose();
			}
			else
			{
				while (Int64_1 < Int64_0 && !backgroundWorker_0.CancellationPending)
				{
					while (Boolean_1)
					{
						Thread.Sleep(100);
					}
					try
					{
						num2 = httpWebResponse.GetResponseStream().Read(array, 0, Int32_1);
					}
					catch (Exception ex3)
					{
						ex = ex3;
						break;
					}
					Int64_1 += num2;
					FireStateEventToMainThread(InternalEvents.FileDownloadProgressChanged);
					try
					{
						fileStream.Write(array, 0, num2);
					}
					catch (Exception ex4)
					{
						ex = ex4;
						break;
					}
				}
				fileStream.Close();
				fileStream.Dispose();
				httpWebResponse.Close();
				if (ex != null)
				{
					backgroundWorker_0.ReportProgress(1, ex);
				}
			}
			FireStateEventToMainThread(InternalEvents.FileDownloadStopped);
		}

		private void FireStateEventToMainThread(InternalEvents internalEvents_0)
		{
			backgroundWorker_0.ReportProgress(0, internalEvents_0);
		}

		private void FireErrorEventToMainMenuThread(object object_0)
		{
			backgroundWorker_0.ReportProgress(1, object_0);
		}
	}
}
