using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TestWebApplication.Services
{
	public class StartupTask : IStartupTask
	{
		private IJobService jobService;

		public StartupTask(IJobService jobService)
		{
			this.jobService = jobService;
		}

		public async Task ExecuteAsync(CancellationToken cancellationToken = default)
		{
			var jobScheduler = await this.jobService.GetJobScheduler().ConfigureAwait(false);
			jobScheduler.RegisterJob("Test", this.jobService.TestSimpleJob);
		}
	}
}
