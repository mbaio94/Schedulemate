using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Schedulemate
{
	internal class AzureSQLJobScheduler : IJobScheduler
	{
		private string connectionString { get; }
		public LogLevel LogLevel { get; set; } = LogLevel.Debug;
		private ConcurrentDictionary<string, Func<Job, Task>> jobMap { get; }

		public AzureSQLJobScheduler(string connectionString)
		{
			this.connectionString = connectionString;
			this.jobMap = new ConcurrentDictionary<string, Func<Job, Task>>();
		}

		internal Task Initialize()
		{
			return Task.CompletedTask;
		}

		public void RegisterJob(string jobName, Func<Job, Task> executeJob)
		{
			this.jobMap.AddOrUpdate(jobName, executeJob, (k, v) => executeJob);
		}

		public async Task ScheduleOneTimeJob(string jobName, DateTime scheduledTimeUtc)
		{
			if (!this.jobMap.ContainsKey(jobName))
			{
				throw new KeyNotFoundException("JobName is not registered");
			}

			using (var sqlConnection = new SqlConnection(this.connectionString))
			{
				await sqlConnection.OpenAsync().ConfigureAwait(false);
				var cmd = new SqlCommand("SchedulemateJob_Insert", sqlConnection);
				
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("JobName", jobName);
				cmd.Parameters.AddWithValue("Scheduled", scheduledTimeUtc);
				cmd.Parameters.AddWithValue("TimespanTicks", DBNull.Value);
				
				await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
			}
		}


		public async Task SchedulePeriodicJob(string jobName, DateTime scheduledStartTimeUtc, TimeSpan periodicity)
		{
			if (!this.jobMap.ContainsKey(jobName))
			{
				throw new KeyNotFoundException("JobName is not registered");
			}

			using (var sqlConnection = new SqlConnection(this.connectionString))
			{
				await sqlConnection.OpenAsync().ConfigureAwait(false);
				var cmd = new SqlCommand("SchedulemateJob_Insert", sqlConnection);

				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("JobName", jobName);
				cmd.Parameters.AddWithValue("Scheduled", scheduledStartTimeUtc);
				cmd.Parameters.AddWithValue("TimespanTicks", periodicity.Ticks);

				await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
			}
		}

		public async Task WriteLog(string message, Dictionary<string, object> metaData, LogLevel logLevel)
		{
			if ((int)this.LogLevel <= (int)logLevel)
			{
				return;
			}

			using (var sqlConnection = new SqlConnection(this.connectionString))
			{
				await sqlConnection.OpenAsync().ConfigureAwait(false);
				var cmd = new SqlCommand("SchedulemateLog_Write", sqlConnection);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add("Test", SqlDbType.NVarChar, 10);
				await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
			}
		}
	}
}
