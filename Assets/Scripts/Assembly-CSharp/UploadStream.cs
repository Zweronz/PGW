using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using BestHTTP;

public sealed class UploadStream : Stream
{
	private MemoryStream memoryStream_0 = new MemoryStream();

	private MemoryStream memoryStream_1 = new MemoryStream();

	private bool bool_0;

	private AutoResetEvent autoResetEvent_0 = new AutoResetEvent(false);

	private object object_0 = new object();

	[CompilerGenerated]
	private string string_0;

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

	private bool Boolean_0
	{
		get
		{
			lock (object_0)
			{
				return memoryStream_0.Position == memoryStream_0.Length;
			}
		}
	}

	public override bool CanRead
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public override bool CanSeek
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public override bool CanWrite
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public override long Length
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public override long Position
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	public UploadStream(string string_1)
		: this()
	{
		String_0 = string_1;
	}

	public UploadStream()
	{
		memoryStream_0 = new MemoryStream();
		memoryStream_1 = new MemoryStream();
		String_0 = string.Empty;
	}

	public override int Read(byte[] b, int off, int len)
	{
		if (bool_0)
		{
			if (memoryStream_0.Position != memoryStream_0.Length)
			{
				return memoryStream_0.Read(b, off, len);
			}
			if (memoryStream_1.Length <= 0L)
			{
				HTTPManager.ILogger_0.Information("UploadStream", string.Format("{0} - Read - End Of Stream", String_0));
				return -1;
			}
			SwitchBuffers();
		}
		if (Boolean_0)
		{
			autoResetEvent_0.WaitOne();
			lock (object_0)
			{
				if (Boolean_0 && memoryStream_1.Length > 0L)
				{
					SwitchBuffers();
				}
			}
		}
		int num = -1;
		lock (object_0)
		{
			return memoryStream_0.Read(b, off, len);
		}
	}

	public override void Write(byte[] buffer, int offset, int count)
	{
		if (bool_0)
		{
			throw new ArgumentException("noMoreData already set!");
		}
		lock (object_0)
		{
			memoryStream_1.Write(buffer, offset, count);
			SwitchBuffers();
		}
		autoResetEvent_0.Set();
	}

	public override void Flush()
	{
		Finish();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			HTTPManager.ILogger_0.Information("UploadStream", string.Format("{0} - Dispose", String_0));
			memoryStream_0.Dispose();
			memoryStream_0 = null;
			memoryStream_1.Dispose();
			memoryStream_1 = null;
			autoResetEvent_0.Close();
			autoResetEvent_0 = null;
		}
		base.Dispose(disposing);
	}

	public void Finish()
	{
		if (bool_0)
		{
			throw new ArgumentException("noMoreData already set!");
		}
		HTTPManager.ILogger_0.Information("UploadStream", string.Format("{0} - Finish", String_0));
		bool_0 = true;
		autoResetEvent_0.Set();
	}

	private bool SwitchBuffers()
	{
		lock (object_0)
		{
			if (memoryStream_0.Position == memoryStream_0.Length)
			{
				memoryStream_1.Seek(0L, SeekOrigin.Begin);
				memoryStream_0.SetLength(0L);
				MemoryStream memoryStream = memoryStream_1;
				memoryStream_1 = memoryStream_0;
				memoryStream_0 = memoryStream;
				return true;
			}
		}
		return false;
	}

	public override long Seek(long offset, SeekOrigin origin)
	{
		throw new NotImplementedException();
	}

	public override void SetLength(long value)
	{
		throw new NotImplementedException();
	}
}
