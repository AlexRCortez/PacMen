using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMen.BL
{
    public class LogManager
    {
            protected readonly ILogger logger;
            public LogManager(ILogger logger)
            {
                this.logger = logger;
            }


            public void Log(NuGet.Common.LogMessage message)
            {
                // _logger.LogWarning("{UserId} logged in.", "bfoote");
                logger?.LogInformation(message.Message);

            }
    }
}
