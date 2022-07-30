using CST.ICS.Gateway.IService;
using Microsoft.Extensions.FileProviders;

namespace CST.ICS.Gateway
{
    public class ReportedDataEngine : BackgroundService
    {
        private readonly ILogger<ReportedDataEngine> _logger;
        private readonly IFileProvider _fileProvider;
        private readonly IMQService _mqService;

        public ReportedDataEngine(ILogger<ReportedDataEngine> logger,
                     IFileProvider fileProvider,
                     IMQService mqService)
        {
            _logger = logger;
            _fileProvider = fileProvider;
            _mqService = mqService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //var contents = _fileProvider.GetDirectoryContents("/");
                //if(contents.Count()>0)
                //{
                //    foreach (var file in contents)
                //    {
                //        _logger.LogInformation("ÎÄ¼þÃû{0}", file.Name);
                //    }
                //}
                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}