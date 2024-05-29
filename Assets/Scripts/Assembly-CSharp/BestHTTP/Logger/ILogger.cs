using System;

namespace BestHTTP.Logger
{
	public interface ILogger
	{
		Loglevels Loglevels_0 { get; set; }

		string String_0 { get; set; }

		string String_1 { get; set; }

		string String_2 { get; set; }

		string String_3 { get; set; }

		string String_4 { get; set; }

		void Verbose(string division, string verb);

		void Information(string division, string info);

		void Warning(string division, string warn);

		void Error(string division, string err);

		void Exception(string division, string msg, Exception ex);
	}
}
