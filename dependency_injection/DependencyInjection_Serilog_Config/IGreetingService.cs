using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace SerilogDIExample
{
	public interface IGreetingService
	{
		public void Run();
	}

    public class GreetingService : IGreetingService
    {
        private readonly ILogger<GreetingService>? _log;
        private readonly IConfiguration? _config;


        public GreetingService(ILogger<GreetingService> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }

        public void Run()
        {
            for (int i = 0; i < _config.GetValue<int>("LoopCount"); i++)
            {
                _log?.LogInformation("Run Count {runNumber}", i);
            }
        }
    }

}

