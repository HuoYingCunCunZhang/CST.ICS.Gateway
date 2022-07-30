using CST.ICS.Gateway.IService;
using CST.ICS.Gateway.Model;
using Microsoft.Extensions.Options;

namespace CST.ICS.Gateway
{
    /// <summary>
    /// 初始化引擎
    /// </summary>
    public class InitAppEngine : BackgroundService
    {
        private readonly IHostApplicationLifetime _lifetime;
        private readonly ILogger<InitAppEngine> _logger;
        private readonly IApiService _apiService;
        private readonly IMQService _mqService;
        /// <summary>
        /// 控制IHOST 的生命周期
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
            //当启动过程中出现数据异常可对tokenSource取消,进而停止应用程序。
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
                // 调用API接口获取网关配置信息
                await _apiService.GetGatewayInfo();
                if (!_apiService.IsGatewayInfoComplete)
                {
                    _logger.LogError("网关配置信息读取失败！");
                    _tokenSource.Cancel(); 
                    return;
                }
                //初始化客户端，连接MQTT服务器,开始订阅消息
                await _mqService.InitMqttClientAsync();
            }
            catch (Exception ex)
            {
                // 结束IHost的生命周期。
                _tokenSource.CancelAfter(1000);
                _logger.LogCritical(ex, ex.Message);

            }

        }
    }
}