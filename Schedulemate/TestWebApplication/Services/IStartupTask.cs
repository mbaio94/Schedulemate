﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TestWebApplication.Services
{
	public interface IStartupTask
	{
		Task ExecuteAsync(CancellationToken cancellationToken = default);
	}
}
