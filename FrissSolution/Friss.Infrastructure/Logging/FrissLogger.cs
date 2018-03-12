using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friss.Infrastructure.Logging
{
	/// <summary>
	/// Simple wrapper over a third party logger (NLog now) so it's easier to replace it with a different
	/// library if required.
	/// </summary>
	public class FrissLogger : IFrissLogger
	{
		private static Logger _logger = LogManager.GetCurrentClassLogger();

		public void LogInfo<T>(T obj)
		{
			_logger.Info(obj);
		}

		public void LogError(string message, Exception ex)
		{
			_logger.Error(ex, message);
		}
	}
}
