using CST.ICS.Gateway.IDevice;
using Microsoft.Extensions.Logging;

namespace CST.ICS.Gateway.Device
{
    public class ConST283_Device : IPressureSourceDevice
    {
        private readonly ILogger<ConST283_Device> _logger;
        
        public ConST283_Device(ILogger<ConST283_Device> logger)
        {
            _logger = logger;
        }
        #region 属性
        public string DeviceName { get; set; }
        #endregion
        public bool SetParam(string param)
        {
            _logger.LogInformation("调用了{name}的设置参数方法，其参数是{param}",DeviceName,param);
            return true;
        }
    }
}