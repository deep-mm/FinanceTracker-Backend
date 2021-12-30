using FinancialTracker.Core.Lib.CoreServices;
using FinancialTracker.Models.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialTracker.Common
{
    public class Helper
    {
        private readonly IOptions<AppInsightsOptions> options;
        private readonly ISessionService sessionService;

        public Helper(IOptions<AppInsightsOptions> options, ISessionService sessionService)
        {
            this.options = options;
            this.sessionService = sessionService;
        }

        public string getLoggedInUsername()
        {
            return sessionService.GetUsername();
        }
    }
}
