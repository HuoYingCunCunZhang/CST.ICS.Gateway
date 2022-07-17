using CST.ICS.Gateway.IDevice;
using Microsoft.Extensions.Options;

namespace CST.ICS.Gateway
{
    public class ScanDeviceEngine : BackgroundService
    {
        private readonly ILogger<ScanDeviceEngine> _logger;
        private readonly IOptionsMonitor<Topic> _topic;
        private readonly IServiceProvider _serviceProvider;
        public ScanDeviceEngine(ILogger<ScanDeviceEngine> logger,
            IServiceProvider serviceProvider,
            IOptionsMonitor<Topic> topic)
        {
            _logger = logger;
            _topic = topic;
            _serviceProvider = serviceProvider;
            _topic.OnChange(t =>
            {
                Console.WriteLine($"配置更新了，最新的值是:{t.ENV}");
            });
        }

        /// <summary>
        /// 属性注入设备列表
        /// </summary>
        public IList<IPressureSourceDevice> PressureSourceDevices { get; set; }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //_logger.LogError("ENV:{0}", _topic.CurrentValue.ENV);
                //_logger.LogWarning("UUT:{0}", _topic.CurrentValue.UUT);
                //_logger.LogDebug("UUT:{0}", _topic.CurrentValue.UUT);
                #region 测试获取多个实现内容

                //var devices = _serviceProvider.GetServices<IPressureSourceDevice>();
                //foreach (var device in devices)
                //{
                //    device.DeviceName = device.GetType().Name; 
                //    device.SetParam(device.GetHashCode().ToString());
                //}
                #endregion

                #region 利用属性注入
                foreach (var device in PressureSourceDevices)
                {
                    device.DeviceName = device.GetType().Name;
                    device.SetParam(device.GetHashCode().ToString());
                }
                #endregion
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}