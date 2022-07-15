using Microsoft.Extensions.FileProviders;

namespace CST.ICS.Gateway
{
    public class ReportedDataEngine : BackgroundService
    {
        private readonly ILogger<ReportedDataEngine> _logger;
        private readonly IFileProvider _fileProvider;

        public ReportedDataEngine(ILogger<ReportedDataEngine> logger,
                     IFileProvider fileProvider)
        {
            _logger = logger;
            _fileProvider = fileProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var contents = _fileProvider.GetDirectoryContents("/");
                if(contents.Count()>0)
                {
                    foreach (var file in contents)
                    {
                        _logger.LogInformation("ÎÄ¼þÃû{0}", file.Name);
                    }
                }
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}