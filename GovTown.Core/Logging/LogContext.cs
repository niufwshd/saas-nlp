using GovTown.Core.Domain.Logging;

namespace GovTown.Core.Logging
{
	public class LogContext
	{
		public string ShortMessage { get; set; }
		public string FullMessage { get; set; }
		public LogLevel LogLevel { get; set; }

		public bool HashNotFullMessage { get; set; }
		public bool HashIpAddress { get; set; }
	}
}
