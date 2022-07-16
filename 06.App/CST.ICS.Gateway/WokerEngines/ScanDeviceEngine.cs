using Microsoft.Extensions.Options;

namespace CST.ICS.Gateway
{
    public class ScanDeviceEngine : BackgroundService
    {
        private readonly ILogger<ScanDeviceEngine> _logger;
        private readonly IOptionsMonitor<Topic> _topic;
        public ScanDeviceEngine(ILogger<ScanDeviceEngine> logger,
            IOptionsMonitor<Topic> topic)
        {
            _logger = logger;
            _topic = topic;
            _topic.OnChange(t =>
            {
                Console.WriteLine($"配置更新了，最新的值是:{t.ENV}");
            });
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                _logger.LogError("ENV:{0}", _topic.CurrentValue.ENV);
                _logger.LogWarning("UUT:{0}", _topic.CurrentValue.UUT);
                _logger.LogDebug("UUT:{0}", _topic.CurrentValue.UUT);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}