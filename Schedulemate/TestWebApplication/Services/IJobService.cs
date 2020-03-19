using Schedulemate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApplication.Services
{
	public interface IJobService
	{
		Task<IJobScheduler> GetJobScheduler();
		Task TestSimpleJob(Job job);
	}
}
