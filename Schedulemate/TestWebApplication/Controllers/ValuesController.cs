using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestWebApplication.Services;

namespace TestWebApplication.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		private IJobService jobService { get; }

		public ValuesController(IJobService jobService)
		{
			this.jobService = jobService;
		}

		// GET api/values
		[HttpGet]
		public async Task<int> Get()
		{
			var jobScheduler = await this.jobService.GetJobScheduler().ConfigureAwait(false);
			await jobScheduler.ScheduleOneTimeJob("Test", DateTime.UtcNow);

			return 1;
		}
	}
}
