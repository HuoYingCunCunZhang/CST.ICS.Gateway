using CST.ICS.Gateway.Common;
using CST.ICS.Gateway.Model;
using GDZ9.Dto.ICS.Param;
using GDZ9.Model.ICS.Business;
using GDZ9.Model.ICS.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Formatter;
using System.Text;

namespace CST.ICS.Gateway.Service
{
    public class MQService
    {
        private readonly IOptionsMonitor<TopicSet> _mqttTopic;
        private readonly IOptionsMonitor<GateWayInfo> _gatewayInfo;
        private readonly ILogger<MQService> _logger;
        private IMqttClient? _mqttClient;
        public MQService(ILogger<MQService> logger, IOptionsMonitor<TopicSet> mqttTopic, IOptionsMonitor<GateWayInfo> gatewayInfo)
        {
            _logger = logger;
            _mqttTopic = mqttTopic ?? throw new ArgumentNullException( nameof( mqttTopic));
            _gatewayInfo = gatewayInfo ?? throw new ArgumentNullException( nameof(gatewayInfo));
        }

        /// <summary>
        /// 消息服务客户端
        /// </summary>
        public IMqttClient Client
        {
            get 
            {
                if(_mqttClient == null)
                {
                    _mqttClient = new MqttFactory().CreateMqttClient();
                    return _mqttClient;
                }
                return _mqttClient;
            }
        }

        /// <summary>
        /// 与消息服务器建立连接
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ConnectAsync()
        {
            var ipAddress = _gatewayInfo.CurrentValue.MqttConfig?.IpAddress;
            var port = Convert.ToInt32(_gatewayInfo.CurrentValue.MqttConfig?.Port);
            var userName = _gatewayInfo.CurrentValue.MqttConfig?.UserName;
            var options = new MqttClientOptionsBuilder()
                   .WithTcpServer(ipAddress, port)
                   .WithClientId(userName)
                   .WithProtocolVersion(MqttProtocolVersion.V500)
                   .Build();
            var result = await Client.ConnectAsync(options);
            if(result.ResultCode != 0)
            {
                _logger.LogError("MQTT客户端连接失败！具体错误内容：{message}",result.ReasonString);
                return false;
            }
            return true;
            
        }

        /// <summary>
        /// 与消息服务断开连接
        /// </summary>
        /// <returns></returns>
        public async Task DisConnetAsync()
        {
             await Client.DisconnectAsync();
        }

        /// <summary>
        /// 订阅主题
        /// </summary>
        /// <returns></returns>
        public async Task SubscribeAsync()
        {
            List<string> topics = new List<string>();
            // 获取所有需要订阅的主题
            if (_gatewayInfo.CurrentValue.GatewayClass.Equals(GatewayClass.UutGateWay))
            {
                topics = _mqttTopic.CurrentValue.UUTTopic.SubTopics.Select(topic => topic.TopicValue.ToString()).ToList();
            }else if (_gatewayInfo.CurrentValue.GatewayClass.Equals(GatewayClass.EnvGateway))
            {
                topics = _mqttTopic.CurrentValue.ENVTopic.SubTopics.Select(topic => topic.TopicValue.ToString()).ToList(); 
            }
            foreach (var topic in topics)
            {
                var options = new MqttClientSubscribeOptionsBuilder().WithTopicFilter(builder => builder.WithTopic("").WithNoLocal(true)).Build();
                await Client.SubscribeAsync(options);
            }
        }

        /// <summary>
        /// 初始化MQTT客户端
        /// </summary>
        /// <returns></returns>
        public async Task InitAsync()
        {
            // 创建连接
            var isConnect = await ConnectAsync();
            if (!isConnect) return;
            // 订阅消息
            await SubscribeAsync();
            // 监听消息
            Client.ApplicationMessageReceivedAsync += MqttClient_onMessageReceived;
        }

        #region MQTT指令接收事件相关方法

        /// <summary>
        /// MQTT指令接收事件
        /// </summary>
        /// <param name="eventArgs"></param>
        /// <exception cref="ArgumentNullException"></exception>
        private Task MqttClient_onMessageReceived(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            //1.通过paload解析是否为指令请求
            byte[] returnPayload = eventArgs.ApplicationMessage.Payload;
            string returnPayloadStr = Encoding.Default.GetString(returnPayload);
            return Task.Factory.StartNew(() => CommandMapToBusiness(returnPayloadStr));
        }

        /// <summary>
        /// 将指令映射到业务方法
        /// </summary>
        /// <param name="command">命令</param>
        void CommandMapToBusiness(string command)
        {

            if (!CommandHelper.ResolveCommandPayload(command, out CommandModel commandmodel))
            {
                _logger.LogError("指令{0}解析失败", command);
                return;
            }
            if (_gatewayInfo.CurrentValue.GatewayClass == GatewayClass.UutGateWay)
            {
                if (commandmodel.Role == RoleType.UUT)
                {
                    //2.通过解析指令前缀区分要调用哪个指令实例


                }
            }
            else if (_gatewayInfo.CurrentValue.GatewayClass == GatewayClass.EnvGateway)
            {
                if (commandmodel.Role == RoleType.PressureCntrl)
                {
                }
                else if (commandmodel.Role == RoleType.PressureSTD)
                {
                }
                else if (commandmodel.Role == RoleType.TemperatureCntrl)
                {
                }
                else if (commandmodel.Role == RoleType.TemperatureSTD)
                {
                }
            }
        }

        
        #endregion
    }
}