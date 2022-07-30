using CST.ICS.Gateway.IService;
using GDZ9.Dto.ICS.Param;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST.ICS.Gateway.Service
{
    public class ApiService : IApiService
    {
        private readonly ILogger<ApiService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IOptionsMonitor<GateWayInfo> _gatewayInfo;

        private bool _isGatewayInfoComplete;


        public ApiService(ILogger<ApiService> logger, IConfiguration configuration, IOptionsMonitor<GateWayInfo> gatewayInfo)
        {
            _logger = logger;
            _configuration = configuration;
            _gatewayInfo = gatewayInfo;
        }

        public bool IsGatewayInfoComplete => _isGatewayInfoComplete;
        public async Task GetGatewayInfo()
        {
            //调用HttpClient去请求ICSM端的API接口
            var gatewaySn = _configuration.GetValue<string>("Gateway:SN");
            var baseUrl = _configuration.GetValue<string>("Api:BaseUrl");
            var actionUrl = _configuration.GetValue<string>("Api:GetGatewayInfoAction");
            var queryUrl = baseUrl + actionUrl + "?sn=" + gatewaySn;
            HttpClient client = new HttpClient();
            var result = await client.GetAsync(queryUrl);
            if (result.IsSuccessStatusCode)
            {
                var content = result.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<dynamic>(content);
                var gatewayInfo = JsonConvert.DeserializeObject<GateWayInfo>(data["Data"].ToString());
                _gatewayInfo.CurrentValue.MqttConfig = gatewayInfo.MqttConfig;
                _gatewayInfo.CurrentValue.DeviceInfos = gatewayInfo.DeviceInfos;
                _gatewayInfo.CurrentValue.ToolSN = gatewayInfo.ToolSN;
                _gatewayInfo.CurrentValue.GateWaySN = gatewayInfo.GateWaySN;
                _gatewayInfo.CurrentValue.WorkStations = gatewayInfo.WorkStations;
                _gatewayInfo.CurrentValue.GatewayClass = gatewayInfo.GatewayClass;
                _isGatewayInfoComplete = true;
                return;
            }
            _logger.LogError("请求接口获取网关的配置信息失败！,请求参数：{0}", queryUrl);
            return;
        }
    }
}
