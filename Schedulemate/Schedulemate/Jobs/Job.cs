using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Schedulemate
{
	public class Job
	{
		public Guid JobId { get; private set; }
		public string JobName { get; private set; }
		private IJobScheduler JobScheduler { get; }
		
		internal Job()
		{
			
		}

		public Task Log(string messsage, Dictionary<string, object> metaData, LogLevel logLevel = LogLevel.Debug)
		{
			return this.JobScheduler.WriteLog(messsage, metaData, logLevel);
		}

		public Task Log(string messsage, LogLevel logLevel = LogLevel.Debug)
		{
			return this.JobScheduler.WriteLog(messsage, null, logLevel);
		}
	}
}
