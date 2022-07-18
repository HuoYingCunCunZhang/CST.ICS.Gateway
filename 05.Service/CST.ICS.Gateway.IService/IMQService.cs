using MQTTnet.Client;

namespace CST.ICS.Gateway.IService
{
    public interface IMQService
    {
        /// <summary>
        /// 消息服务客户端
        /// </summary>
        public IMqttClient Client
        {
            get;
        }

        /// <summary>
        /// 与消息服务器建立连接
        /// </summary>
        /// <returns></returns>
        public Task<bool> ConnectAsync();

        /// <summary>
        /// 与消息服务断开连接
        /// </summary>
        /// <returns></returns>
        public Task DisConnetAsync();

        /// <summary>
        /// 订阅主题
        /// </summary>
        /// <returns></returns>
        public Task SubscribeAsync();

        /// <summary>
        /// 初始化MQTT客户端
        /// </summary>
        /// <returns></returns>
        public Task InitMqttClientAsync();
    }
}