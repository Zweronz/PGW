using System;
using WebSocketSharp.Net.WebSockets;

namespace WebSocketSharp.Server
{
	public abstract class WebSocketServiceHost
	{
		internal ServerState ServerState_0
		{
			get
			{
				return WebSocketSessionManager_0.ServerState_0;
			}
		}

		public abstract bool Boolean_0 { get; set; }

		public abstract string String_0 { get; }

		public abstract WebSocketSessionManager WebSocketSessionManager_0 { get; }

		public abstract Type Type_0 { get; }

		public abstract TimeSpan TimeSpan_0 { get; set; }

		internal void Start()
		{
			WebSocketSessionManager_0.Start();
		}

		internal void StartSession(WebSocketContext webSocketContext_0)
		{
			CreateSession().Start(webSocketContext_0, WebSocketSessionManager_0);
		}

		internal void Stop(ushort ushort_0, string string_0)
		{
			CloseEventArgs closeEventArgs = new CloseEventArgs(ushort_0, string_0);
			bool flag;
			byte[] byte_ = ((!(flag = !ushort_0.IsReserved())) ? null : WebSocketFrame.CreateCloseFrame(closeEventArgs.PayloadData_0, false).ToByteArray());
			TimeSpan timeSpan_ = ((!flag) ? TimeSpan.Zero : TimeSpan_0);
			WebSocketSessionManager_0.Stop(closeEventArgs, byte_, timeSpan_);
		}

		protected abstract WebSocketBehavior CreateSession();
	}
	internal class WebSocketServiceHost<TBehavior> : WebSocketServiceHost where TBehavior : WebSocketBehavior
	{
		private Func<TBehavior> _initializer;

		private Logger _logger;

		private string _path;

		private WebSocketSessionManager _sessions;

		public override bool Boolean_0
		{
			get
			{
				return _sessions.Boolean_0;
			}
			set
			{
				string text = _sessions.ServerState_0.CheckIfAvailable(true, false, false);
				if (text != null)
				{
					_logger.Error(text);
				}
				else
				{
					_sessions.Boolean_0 = value;
				}
			}
		}

		public override string String_0
		{
			get
			{
				return _path;
			}
		}

		public override WebSocketSessionManager WebSocketSessionManager_0
		{
			get
			{
				return _sessions;
			}
		}

		public override Type Type_0
		{
			get
			{
				return typeof(TBehavior);
			}
		}

		public override TimeSpan TimeSpan_0
		{
			get
			{
				return _sessions.TimeSpan_0;
			}
			set
			{
				string text = _sessions.ServerState_0.CheckIfAvailable(true, false, false) ?? value.CheckIfValidWaitTime();
				if (text != null)
				{
					_logger.Error(text);
				}
				else
				{
					_sessions.TimeSpan_0 = value;
				}
			}
		}

		internal WebSocketServiceHost(string string_0, Func<TBehavior> func_0, Logger logger_0)
		{
			_path = string_0;
			_initializer = func_0;
			_logger = logger_0;
			_sessions = new WebSocketSessionManager(logger_0);
		}

		protected override WebSocketBehavior CreateSession()
		{
			return _initializer();
		}
	}
}
