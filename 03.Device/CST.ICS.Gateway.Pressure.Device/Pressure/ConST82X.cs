using CST.ICS.Gateway.Pressure.IDevice;
using GDZ9.Dto.ICS.Param;
using GDZ9.Model.ICS.Common;
using System.IO.Ports;
using System.Net;
using Xmas11.Comm.Core;
using Xmas11.Comm.Data.Common;
using Xmas11.Comm.Devices;
using Xmas11.Domain;
using Xmas11.Domain.Electricity;
using Xmas11.Domain.Mechanics;
using Xmas11.Domain.Thermology;
using PressureRange = Xmas11.Domain.Mechanics.PressureRange;
using Unit = Xmas11.Domain.Unit;

namespace CST.ICS.Gateway.Pressure.Device
{
    /// <summary>
    /// ConST82X
    /// </summary>
    public class ConST82X : IPressureControlDevice,IPressureStdDevice
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="commSetting"></param>
        public ConST82X(CommSetting commSetting)
        {
            this.CommSetting = commSetting;
            InitDevice();
            Name = "ConST82X";
        }
        public ConST82X()
        {
            this.CommSetting = new CommSetting();
        }
        private int _SequenceNumber;
        /// <summary>
        /// 排序号
        /// </summary>
        public int SequenceNumber
        {
            get { return _SequenceNumber; }
            set
            {
                 _SequenceNumber=value;
            }
        }
        private string _DeviceMode;
        /// <summary>
        /// 管理端设备型号
        /// </summary>
        public string DeviceMode
        {
            get { return _DeviceMode; }
            set
            {
                _DeviceMode = value;
            }
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
                _IsEnable=value;
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
                _IsNotScaning= value;
            }
        }
        private bool _isOnline;
        /// <summary>
        /// 标识设备在线状态
        /// </summary>
        public bool IsOnline
        {
            get { return _isOnline; }
            set { _isOnline=value;}
        }

        private CommSetting commSetting;
        /// <summary>
        /// 通讯配置
        /// </summary>
        public CommSetting CommSetting
        {
            get { return commSetting; }
            set { commSetting= value; }
        }

        private string _Picture;
        /// <summary>
        /// 图片
        /// </summary>
        public string Picture
        {
            get { return _Picture; }
            set {  _Picture=value; }
        }


        /// <summary>
        /// ConST82X通讯库类
        /// </summary>
        public Xmas11.Comm.Devices.MPCBase mPCBase { get; set; }

        private Xmas11.Domain.Mechanics.Pressure _CurrentTargetPressure;
        /// <summary>
        /// 当前目标压力
        /// </summary>
        public Xmas11.Domain.Mechanics.Pressure CurrentTargetPressure
        {
            get { return _CurrentTargetPressure; }
            set {  _CurrentTargetPressure= value; }
        }

        private int _Digit;
        /// <summary>
        /// 显示位
        /// </summary>
        public int Digit
        {
            get { return _Digit; }
            set
            {
                 _Digit= value;
            }
        }


        private PressureControlMode _CurrentControlState;
        /// <summary>
        /// 当前控制状态
        /// </summary>
        public PressureControlMode CurrentControlState
        {
            get { return _CurrentControlState; }
            set {  _CurrentControlState=value; }
        }

        private PressureRange _Range;
        /// <summary>
        /// 压力量程
        /// </summary>
        public PressureRange Range
        {
            get { return _Range; }
            set {  _Range= value; }
        }

        private PressureType _PressureType;
        /// <summary>
        /// 压力类型
        /// </summary>
        public PressureType PressureType
        {
            get { return _PressureType; }
            set {  _PressureType= value; }
        }

        private Xmas11.Domain.Mechanics.Pressure _CurrentPressure;
        /// <summary>
        /// 当前压力值
        /// </summary>
        public Xmas11.Domain.Mechanics.Pressure CurrentPressure
        {
            get { return _CurrentPressure; }
            set
            {
                this.Unit = value.Unit.ToString();
                 _CurrentPressure=value;
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
            set {  _Name=value; }
        }

        private string _DeviceType;
        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceType
        {
            get { return _DeviceType; }
            set { _DeviceType = value; }
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

        private readonly object _lock = new object();

        private BaseDevice _CommInstance;
        /// <summary>
        /// 通讯实例
        /// </summary>
        public BaseDevice CommInstance
        {
            get { return _CommInstance; }
            set {  _CommInstance=value; }
        }

      

        private string _Unit;
        /// <summary>
        /// 当前单位
        /// </summary>
        public string Unit
        {
            get { return _Unit; }
            set
            {
                 _Unit=value;
            }
        }

        public List<DeviceRole> Role { get; set; }
        public CommSettings CommSettingInstance { get; set; }
        public Temperature CurrentTemperature { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// 关闭通讯
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            try
            {
                //CommInstance.Close();
                mPCBase.Close();
                //mPCBase = null ;

                return !mPCBase.Connected;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 获取控制状态
        /// </summary>
        /// <returns></returns>
        public PressureControlMode GetControlState()
        {
            return mPCBase.GetPressureControlMode().Result;
        }

        /// <summary>
        /// 获取设备类型
        /// </summary>
        /// <returns></returns>
        public string GetDeviceType()
        {
            return mPCBase.GetDevType().Result;
        }

        /// <summary>
        /// 获取设备名称
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return mPCBase.GetName();
        }

        /// <summary>
        /// 获取压力值
        /// </summary>
        /// <returns></returns>
        public Xmas11.Domain.Mechanics.Pressure GetPressure()
        {
            lock (_lock)
            {
                var pressure = mPCBase.GetPressure().Result;
                this.CurrentPressure = pressure;
                return pressure;
            }
        }

        /// <summary>
        /// 获取压力量程
        /// </summary>
        /// <returns></returns>
        public PressureRange GetPressureRange()
        {
            return mPCBase.GetPressureRange().Result;
        }

        /// <summary>
        /// 获取压力类型
        /// </summary>
        /// <returns></returns>
        public PressureType GetPressureType()
        {
            return mPCBase.GetPressureType().Result;
        }

        /// <summary>
        /// 获取压力单位
        /// </summary>
        /// <returns></returns>
        public Unit GetPressureUnit()
        {
            return mPCBase.GetPressureUnit().Result;
        }

        /// <summary>
        /// 获取序列号
        /// </summary>
        /// <returns></returns>
        public string GetSerialNumber()
        {
            return mPCBase.GetSerialNumber().Result;
        }

        /// <summary>
        /// 获取目标压力值
        /// </summary>
        /// <returns></returns>
        public Xmas11.Domain.Mechanics.Pressure GetTargetPressure()
        {
            lock (_lock)
            {
                return mPCBase.GetTargetPressure().Result;
            }
        }

        /// <summary>
        /// 获取固件版本
        /// </summary>
        /// <returns></returns>
        public string GetVersion()
        {
            return mPCBase.GetVersion().Result;
        }

        /// <summary>
        /// 设备是否存在
        /// </summary>
        /// <returns></returns>
        public bool IsExist()
        {
            return mPCBase.IsExist();
        }

        /// <summary>
        /// 打开通讯
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            return mPCBase.Open();
        }

        /// <summary>
        /// 压力清零
        /// </summary>
        /// <returns></returns>
        public bool PressureZero()
        {
            return mPCBase.PressureZero().ErrorCode.IsEmpty();
        }

        /// <summary>
        /// 设置控制状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool SetControlState(PressureControlMode state)
        {
            return mPCBase.SetPressureControlMode(state).ErrorCode.IsEmpty();
        }

        /// <summary>
        /// 设置压力类型
        /// </summary>
        /// <param name="pressureType"></param>
        /// <returns></returns>
        public bool SetPressureType(PressureType pressureType)
        {
            return mPCBase.SetPressureType(pressureType).ErrorCode.IsEmpty();
        }

        /// <summary>
        /// 设置压力单位
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public bool SetPressureUnit(Unit unit)
        {
            return mPCBase.SetPressureUnit(unit).ErrorCode.IsEmpty();
        }

        /// <summary>
        /// 设置目标压力
        /// </summary>
        /// <param name="targetPressure"></param>
        /// <returns></returns>
        public bool SetTargetPressure(Xmas11.Domain.Mechanics.Pressure targetPressure)
        {
            this.CurrentTargetPressure = targetPressure;
            return mPCBase.SetTargetPressure(targetPressure).ErrorCode.IsEmpty();
        }

        /// <summary>
        /// 排空
        /// </summary>
        /// <returns></returns>
        public bool Vent()
        {
            try
            {
                if (mPCBase.Connected)
                {
                    return mPCBase.Vent().ErrorCode.IsEmpty();
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Thread.Sleep(1000);
                        if (mPCBase.Connected) return mPCBase.Vent().ErrorCode.IsEmpty();
                    }
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }


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
            CommInstance = new MPCBase(CommSettingInstance);
            CommInstance.Policy = new Xmas11.Comm.Commander.iPolicy(3000, 100, 3);
            mPCBase = CommInstance as MPCBase;
            if (Role == null)
            {
                Role = new List<DeviceRole>();
            }
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
            this.PressureType = GetPressureType();
            this.Range = GetPressureRange();
            this.CurrentControlState = GetControlState();
        }

        #region 表标定
        public bool StartCalibrationAD()
        {
            throw new NotImplementedException();
        }

        public bool SetADValue(int value)
        {
            throw new NotImplementedException();
        }

        public double GetADOriginalFlagValue()
        {
            throw new NotImplementedException();
        }

        public bool SaveADValue()
        {
            throw new NotImplementedException();
        }

        public ADData GetADData()
        {
            throw new NotImplementedException();
        }

        public bool ResumeAD(ADData data)
        {
            throw new NotImplementedException();
        }

        public bool StartCalibrationRange()
        {
            throw new NotImplementedException();
        }

        public bool SetCalibrationRange(PressureRange range, VoltageRange voltageRange)
        {
            throw new NotImplementedException();
        }

        public bool CalibrationRangeUpper(double pressure)
        {
            throw new NotImplementedException();
        }

        public bool CalibrationRangeLower(double pressure)
        {
            throw new NotImplementedException();
        }

        public bool StopAndSaveCalibrationRange()
        {
            throw new NotImplementedException();
        }

        public RangeSettingData GetCalibrationRangeData()
        {
            throw new NotImplementedException();
        }

        public bool ResumeRangeSettingData(RangeSettingData rsData)
        {
            throw new NotImplementedException();
        }

        public bool MulT_SafeStartTemperatureCompensate()
        {
            throw new NotImplementedException();
        }

        public bool MulT_SetTemperatureCompensateOrder(int pOrder, int tOrder)
        {
            throw new NotImplementedException();
        }

        public int MulT_GetPressureOrder()
        {
            throw new NotImplementedException();
        }

        public int MulT_GetTemperatureOrder()
        {
            throw new NotImplementedException();
        }

        public bool MulT_SetTemperatureCompensateMatrixData(string[] dataArray)
        {
            throw new NotImplementedException();
        }

        public string[] MulT_GetTemperatureCompensateMatrixData(int count)
        {
            throw new NotImplementedException();
        }

        public bool MulT_SelfStopTemperatureCompensate()
        {
            throw new NotImplementedException();
        }

        public bool MulT_CalcelTemperatureCompensate()
        {
            throw new NotImplementedException();
        }

        public double GetORIV()
        {
            throw new NotImplementedException();
        }

        public double GetTemperature()
        {
            throw new NotImplementedException();
        }

        public bool SetWorkMode(PressureWorkMode workMode)
        {
            throw new NotImplementedException();
        }

        public bool SetOffset(double value)
        {
            throw new NotImplementedException();
        }

        public bool CloseOffset()
        {
            throw new NotImplementedException();
        }

        public bool StartDownloadCalibrationData(string calibrationData, CalibrationType calibrationType)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
