using CST.ICS.Gateway.IService;
using GDZ9.Dto.ICS.Param;
using Microsoft.Extensions.Options;

namespace CST.ICS.Gateway
{
    /// <summary>
    /// …Ë±∏…®√Ë“˝«Ê
    /// </summary>
    public class ScanDeviceEngine : BackgroundService
    {
        private readonly ILogger<ScanDeviceEngine> _logger;
        private readonly IOptionsMonitor<GateWayInfo> _gatewayInfo;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMQService _mqService;
        public ScanDeviceEngine(ILogger<ScanDeviceEngine> logger,
            IOptionsMonitor<GateWayInfo> gatewayInfo,
            IServiceProvider serviceProvider,
            IMQService mqService)
        {
            _logger = logger;
            _gatewayInfo = gatewayInfo;
            _serviceProvider = serviceProvider;
            _mqService = mqService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                
                
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}