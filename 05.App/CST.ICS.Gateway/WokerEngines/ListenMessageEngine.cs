namespace CST.ICS.Gateway
{
    public class ListenMessageEngine : BackgroundService
    {
        private readonly ILogger<ListenMessageEngine> _logger;

        public ListenMessageEngine(ILogger<ListenMessageEngine> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}