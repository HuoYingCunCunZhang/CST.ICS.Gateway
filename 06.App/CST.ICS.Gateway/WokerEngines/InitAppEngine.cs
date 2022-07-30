using CST.ICS.Gateway.IService;
using CST.ICS.Gateway.Model;
using Microsoft.Extensions.Options;

namespace CST.ICS.Gateway
{
    /// <summary>
    /// ��ʼ������
    /// </summary>
    public class InitAppEngine : BackgroundService
    {
        private readonly IHostApplicationLifetime _lifetime;
        private readonly ILogger<InitAppEngine> _logger;
        private readonly IApiService _apiService;
        private readonly IMQService _mqService;
        /// <summary>
        /// ����IHOST ����������
        /// </summary>
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();
        public InitAppEngine(IHostApplicationLifetime lifetime,
            ILogger<InitAppEngine> logger,
            IOptionsMonitor<TopicSet> topicSet,
            IApiService apiService,
            IMQService mqService)
        {
            _lifetime = lifetime;
            _logger = logger;
            _apiService = apiService;
            _mqService = mqService;

            _lifetime.ApplicationStarted.Register(() => Console.WriteLine(
            "[{0}]Application started", DateTimeOffset.Now));
            _lifetime.ApplicationStopping.Register(() => Console.WriteLine(
            "[{0}]Application is stopping.", DateTimeOffset.Now));
            _lifetime.ApplicationStopped.Register(() => Console.WriteLine(
            "[{0}]Application stopped.", DateTimeOffset.Now));
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            //�����������г��������쳣�ɶ�tokenSourceȡ��,����ֹͣӦ�ó���
            _tokenSource.Token.Register(_lifetime.StopApplication);
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _tokenSource?.Dispose();
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                // ����API�ӿڻ�ȡ����������Ϣ
                await _apiService.GetGatewayInfo();
                if (!_apiService.IsGatewayInfoComplete)
                {
                    _logger.LogError("����������Ϣ��ȡʧ�ܣ�");
                    _tokenSource.Cancel(); 
                    return;
                }
                //��ʼ���ͻ��ˣ�����MQTT������,��ʼ������Ϣ
                await _mqService.InitMqttClientAsync();
            }
            catch (Exception ex)
            {
                // ����IHost���������ڡ�
                _tokenSource.CancelAfter(1000);
                _logger.LogCritical(ex, ex.Message);

            }

        }
    }
}