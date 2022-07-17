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
                Console.WriteLine($"���ø����ˣ����µ�ֵ��:{t.ENV}");
            });
        }

        /// <summary>
        /// ����ע���豸�б�
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
                #region ���Ի�ȡ���ʵ������

                //var devices = _serviceProvider.GetServices<IPressureSourceDevice>();
                //foreach (var device in devices)
                //{
                //    device.DeviceName = device.GetType().Name; 
                //    device.SetParam(device.GetHashCode().ToString());
                //}
                #endregion

                #region ��������ע��
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