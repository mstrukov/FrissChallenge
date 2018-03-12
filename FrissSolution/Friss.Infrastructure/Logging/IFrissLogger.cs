using System;

namespace Friss.Infrastructure.Logging
{
	public interface IFrissLogger
	{
		void LogError(string message, Exception ex);
		void LogInfo<T>(T obj);
	}
}