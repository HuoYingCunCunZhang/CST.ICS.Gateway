using CST.ICS.Gateway.Pressure.IDevice;
using GDZ9.Dto.ICS.Param;
using GDZ9.Model.ICS.Common;
using System.IO.Ports;
using System.Net;
using Xmas11.Comm.Core;
using Xmas11.Comm.Data.Common;
using Xmas11.Comm.Devices;
using Xmas11.Domain.Thermology;

namespace GWS.Implement._9000.Device
{
    /// <summary>
    /// HotCoolHumidAirConditioner高低温箱
    /// </summary>
    public class HardyHotCoolHumid :  ITemperatureDevice
    {
        public HardyHotCoolHumid(CommSetting commSetting)
        {
            this.CommSetting = commSetting;
            InitDevice();
            Name = "HotCoolHumidAirConditioner";
        }

        private object _lock = new object();
        public CommSettings CommSettingInstance { get; set; }
        public List<DeviceRole> Role { get; set; }
        /// <summary>
        /// 管理端设备型号
        /// </summary>
        public string DeviceMode { get; set; }
        /// <summary>
        /// 高低温箱
        /// </summary>
        public HotCoolHumidAirConditioner Device
        {
            get; set;
        }

        /// <summary>
        /// 排序号
        /// </summary>
        public int SequenceNumber { get; set; }

        private bool isOnline;
        /// <summary>
        /// 标识设备在线状态
        /// </summary>
        public bool IsOnline
        {
            get { return isOnline; }
            set { isOnline=value; }
        }
        bool _IsEnable;
        /// <summary>
        /// 被检设备的启用状态
        /// </summary>
        public bool IsEnable
        {
            get { return _IsEnable; }
            set
            {
                _IsEnable= value;
            }
        }
        bool _IsNotScaning = true;
        /// <summary>
        /// 被检设备的扫描状态
        /// </summary>
        public bool IsNotScaning
        {
            get { return _IsNotScaning; }
            set
            {
                _IsNotScaning=value;
            }
        }
        private CommSetting commSetting;
        /// <summary>
        /// 通讯配置
        /// </summary>
        public CommSetting CommSetting
        {
            get { return commSetting; }
            set { commSetting=value; }
        }

        private string _Picture;
        /// <summary>
        /// 图片
        /// </summary>
        public string Picture
        {
            get { return _Picture; }
            set {  _Picture= value; }
        }

        private TemperatureRange _Range;
        /// <summary>
        /// 温度量程
        /// </summary>
        public TemperatureRange Range
        {
            get { return _Range; }
            set
            {
                 _Range=value;
            }
        }

        private Temperature _CurrentTemperature;
        /// <summary>
        /// 当前温度值
        /// </summary>
        public Temperature CurrentTemperature
        {
            get { return _CurrentTemperature; }
            set
            {
                _CurrentTemperature = value;
            }
        }
        private Temperature _CHTargetTemperature;
        /// <summary>
        /// 当前高低温箱目标温度值
        /// </summary>
        public Temperature CHTargetTemperature
        {
            get { return _CHTargetTemperature; }
            set
            {
                _CHTargetTemperature= value;
            }
        }

        private string _SerialNumber;
        /// <summary>
        /// 序列号
        /// </summary>
        public string SerialNumber
        {
            get { return _SerialNumber; }
            set {  _SerialNumber=value; }
        }

        private string _Name;
        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set {  _Name= value; }
        }

        private string _DeviceType;
        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceType
        {
            get { return _DeviceType; }
            set {  _DeviceType= value; }
        }

        private string _DeviceVersion;
        /// <summary>
        /// 设备版本号
        /// </summary>
        public string DeviceVersion
        {
            get { return _DeviceVersion; }
            set {  _DeviceVersion=value; }
        }

        private BaseDevice _CommInstance;
        /// <summary>
        /// 通讯实例
        /// </summary>
        public BaseDevice CommInstance
        {
            get { return _CommInstance; }
            set {  _CommInstance=value; }
        }


        /// <summary>
        /// 关闭通讯
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            try
            {
                if (CommInstance != null)
                {
                    CommInstance.Open();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 获取送风机状态
        /// </summary>
        /// <returns></returns>
        public OpenCloseState GetBlowerState()
        {
            return OpenCloseState.UnKnown;
        }

        /// <summary>
        /// 获取压缩机状态
        /// </summary>
        /// <returns></returns>
        public OpenCloseState GetCompressorState()
        {
            return OpenCloseState.UnKnown;
        }

        /// <summary>
        /// 获取当前温度值
        /// </summary>
        /// <returns></returns>
        public Temperature GetCurrentTemperature()
        {
            lock (_lock)
            {
                this.CHTargetTemperature = Device.GetSV().Result;
                var res = Device.GetPV();
                this.CurrentTemperature = Device.GetPV().Result;
                return CurrentTemperature;
            }
        }

        /// <summary>
        /// 获取设备类型
        /// </summary>
        /// <returns></returns>
        public string GetDeviceType()
        {
            return string.Empty;
        }

        /// <summary>
        /// 获取设备名称
        /// </summary>
        /// <returns></returns>
        public virtual string GetName()
        {
            var res = Device.GetName();
            return Device.GetName();
        }

        /// <summary>
        /// 获取运行状态
        /// </summary>
        /// <returns></returns>
        public OperateStandbyState GetOperationState()
        {
            return OperateStandbyState.Unknown;
        }

        /// <summary>
        /// 获取序列号
        /// </summary>
        /// <returns></returns>
        public string GetSerialNumber()
        {
            return Device.GetSN();
        }

        /// <summary>
        /// 获取目标温度
        /// </summary>
        /// <returns></returns>
        public Temperature GetTargetTemperature()
        {
            return Device.GetSV().Result;
        }

        /// <summary>
        /// 获取温度量程
        /// </summary>
        /// <returns></returns>
        public TemperatureRange GetTemperatureRange()
        {
            return new TemperatureRange();
        }

        /// <summary>
        /// 获取设备固件版本
        /// </summary>
        /// <returns></returns>
        public string GetVersion()
        {
            return string.Empty;
        }

        /// <summary>
        /// 设备是否存在
        /// </summary>
        /// <returns></returns>
        public bool IsExist()
        {
            return Device.IsExist();
        }

        /// <summary>
        /// 打开通讯
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            return CommInstance.Open();
        }

        /// <summary>
        /// 运行
        /// </summary>
        /// <returns></returns>
        public bool Operate()
        {
            return Device.StartDevice();
        }

        /// <summary>
        /// 设置送风机状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool SetBlowerState(OpenCloseState state)
        {
            return true;
        }

        /// <summary>
        /// 设置压缩机状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool SetCompressorState(OpenCloseState state)
        {
            return true;
        }

        /// <summary>
        /// 设置目标温度
        /// </summary>
        /// <param name="targetTemperature"></param>
        /// <returns></returns>
        public bool SetTargetTemperature(Temperature targetTemperature)
        {
            return Device.SetSV(targetTemperature).ErrorCode.IsEmpty();
        }

        /// <summary>
        /// 待机
        /// </summary>
        /// <returns></returns>
        public bool Standby()
        {
            return Device.StopDevice();
        }
        /// <summary>
        /// 初始化设备通讯信息
        /// </summary>
        public void InitDevice()
        {
            CommSettingInstance = new SerialPortCommSettings("COM1", 9600, 8, StopBits.Two, Parity.None);
            if (commSetting is SocketCommSetting)
            {

                CommSettingInstance = new SocketCommSettings(IPAddress.Parse(((SocketCommSetting)commSetting).IpAddress), ((SocketCommSetting)commSetting).Port, true);
            }
            else if (commSetting is SerialPortCommSetting)
            {
                CommSettingInstance = new SerialPortCommSettings(((SerialPortCommSetting)commSetting).SerialPortName, ((SerialPortCommSetting)commSetting).Baudrate, ((SerialPortCommSetting)commSetting).DataBits, ((SerialPortCommSetting)commSetting).StopBits, ((SerialPortCommSetting)commSetting).Parity);
            }
            else if (commSetting is UsbCommSetting)
            {
                CommSettingInstance = new UsbCommSettings(((UsbCommSetting)commSetting).Vid, ((UsbCommSetting)commSetting).Pid, ((UsbCommSetting)commSetting).Location);
            }
            if (Role == null)
            {
                Role = new List<DeviceRole>();
            }
            CommInstance = new HotCoolHumidAirConditioner(CommSettingInstance);
            Device = CommInstance as HotCoolHumidAirConditioner;
        }

        /// <summary>
        /// 初始化设备基本信息
        /// </summary>
        public void GetBaseInfo()
        {
            this.SerialNumber = GetSerialNumber();
            this.DeviceType = GetDeviceType();
            this.DeviceVersion = GetVersion();
            this.Name = GetName();
        }

    }
}
