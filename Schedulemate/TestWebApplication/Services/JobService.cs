using Schedulemate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApplication.Services
{
	public class JobService : IJobService
	{
		private IJobScheduler jobScheduler { get; set; }

		public async Task<IJobScheduler> GetJobScheduler()
		{
			if (this.jobScheduler == null)
			{
				this.jobScheduler = await JobSchedulerFactory.Create("Server=tcp:schedulemate.database.windows.net,1433;Initial Catalog=Schedulemate;Persist Security Info=False;User ID=mbaio;Password=9n43r8@t/@,9V=3W;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;", SchedulerDataStore.AzureSQL).ConfigureAwait(false);
			}

			return this.jobScheduler;
		}

		public Task TestSimpleJob(Job job)
		{
			throw new NotImplementedException();
		}
	}
}
