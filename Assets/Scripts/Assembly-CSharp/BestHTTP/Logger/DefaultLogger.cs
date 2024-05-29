using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace BestHTTP.Logger
{
	public class DefaultLogger : ILogger
	{
		[CompilerGenerated]
		private Loglevels loglevels_0;

		[CompilerGenerated]
		private string string_0;

		[CompilerGenerated]
		private string string_1;

		[CompilerGenerated]
		private string string_2;

		[CompilerGenerated]
		private string string_3;

		[CompilerGenerated]
		private string string_4;

		public Loglevels Loglevels_0
		{
			[CompilerGenerated]
			get
			{
				return loglevels_0;
			}
			[CompilerGenerated]
			set
			{
				loglevels_0 = value;
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
			set
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
			set
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
			set
			{
				string_2 = value;
			}
		}

		public string String_3
		{
			[CompilerGenerated]
			get
			{
				return string_3;
			}
			[CompilerGenerated]
			set
			{
				string_3 = value;
			}
		}

		public string String_4
		{
			[CompilerGenerated]
			get
			{
				return string_4;
			}
			[CompilerGenerated]
			set
			{
				string_4 = value;
			}
		}

		public DefaultLogger()
		{
			String_0 = "I [{0}]: {1}";
			String_1 = "I [{0}]: {1}";
			String_2 = "W [{0}]: {1}";
			String_3 = "Err [{0}]: {1}";
			String_4 = "Ex [{0}]: {1} - Message: {2}  StackTrace: {3}";
			Loglevels_0 = ((!Debug.isDebugBuild) ? Loglevels.Error : Loglevels.Warning);
		}

		public void Verbose(string division, string verb)
		{
			if (Loglevels_0 <= Loglevels.All)
			{
				try
				{
					Debug.Log(string.Format(String_0, division, verb));
				}
				catch
				{
				}
			}
		}

		public void Information(string division, string info)
		{
			if (Loglevels_0 <= Loglevels.Information)
			{
				try
				{
					Debug.Log(string.Format(String_1, division, info));
				}
				catch
				{
				}
			}
		}

		public void Warning(string division, string warn)
		{
			if (Loglevels_0 <= Loglevels.Warning)
			{
				try
				{
					Debug.LogWarning(string.Format(String_2, division, warn));
				}
				catch
				{
				}
			}
		}

		public void Error(string division, string err)
		{
			if (Loglevels_0 <= Loglevels.Error)
			{
				try
				{
					Debug.LogError(string.Format(String_3, division, err));
				}
				catch
				{
				}
			}
		}

		public void Exception(string division, string msg, Exception ex)
		{
			if (Loglevels_0 <= Loglevels.Exception)
			{
				try
				{
					Debug.LogError(string.Format(String_4, division, msg, (ex == null) ? "null" : ex.Message, (ex == null) ? "null" : ex.StackTrace));
				}
				catch
				{
				}
			}
		}
	}
}
