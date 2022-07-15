namespace CST.ICS.Gateway
{
    public class ReportedDataEngine : BackgroundService
    {
        private readonly ILogger<ReportedDataEngine> _logger;

        public ReportedDataEngine(ILogger<ReportedDataEngine> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}