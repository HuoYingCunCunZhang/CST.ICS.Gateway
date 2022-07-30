using CST.ICS.Gateway.IService;
using CST.ICS.Gateway.Model;
using GDZ9.Dto.ICS.Param;
using Microsoft.Extensions.Options;

namespace CST.ICS.Gateway
{
    /// <summary>
    /// ��Ϣ��������
    /// </summary>
    public class ListenMessageEngine : BackgroundService
    {
        private readonly ILogger<ListenMessageEngine> _logger;

        private readonly IOptionsMonitor<TopicSet> _topicSet;
        private readonly IMQService _mqService;
        public ListenMessageEngine(ILogger<ListenMessageEngine> logger,
            IOptionsMonitor<TopicSet> topicSet,
            IMQService mqService)
        {
            _logger = logger;
            _topicSet = topicSet;
            _mqService = mqService;
            //����������ݸ���
            _topicSet.OnChange(t =>
            {
                Console.WriteLine($"���ø����ˣ����µ�ֵ��:{t.UUTTopic.SubTopics.Count}");
            });
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("ENV:{0}", _topicSet.CurrentValue.UUTTopic.SubTopics.Count);
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}