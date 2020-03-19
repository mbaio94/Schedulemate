using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Schedulemate
{
	public interface IJobScheduler
	{
		LogLevel LogLevel { get; set; }
		void RegisterJob(string jobName, Func<Job, Task> executeJob);
		Task ScheduleOneTimeJob(string jobName, DateTime scheduledTimeUtc);
		Task SchedulePeriodicJob(string jobName, DateTime scheduledStartTimeUtc, TimeSpan periodicity);
		Task WriteLog(string messsage, Dictionary<string, object> metaData, LogLevel logLevel);
	}
}
