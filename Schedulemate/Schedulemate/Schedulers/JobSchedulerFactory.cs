using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Schedulemate
{
	public static class JobSchedulerFactory
	{
		public static Task<IJobScheduler> Create(string connectionString, SchedulerDataStore schedulerDataStore)
		{
			if (schedulerDataStore == SchedulerDataStore.AzureSQL)
			{
				return CreateAzureSQLJobScheduler(connectionString);
			}

			throw new ArgumentException("Invalid SchedulerDataStore provided");
		}

		private static async Task<IJobScheduler> CreateAzureSQLJobScheduler(string connectionString)
		{
			var scheduler = new AzureSQLJobScheduler(connectionString);
			await scheduler.Initialize().ConfigureAwait(false);
			return scheduler;
		}
	}
}
