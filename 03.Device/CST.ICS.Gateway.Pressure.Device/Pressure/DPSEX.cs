using CST.ICS.Gateway.Pressure.IDevice;
using GDZ9.Dto.ICS.Param;
using GDZ9.Model.ICS.Common;
using System.IO.Ports;
using System.Net;
using Xmas11.Comm.Core;
using Xmas11.Comm.Data.Common;
using Xmas11.Comm.Devices;
using Xmas11.Domain.Mechanics;
using Press = Xmas11.Domain.Mechanics.Pressure;

namespace CST.ICS.Gateway.Pressure.Device
{
    public class DPSEX: IPressureStdDevice
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="commSettings"></param>
        public DPSEX(GDZ9.Dto.ICS.Param.CommSetting commSetting)
        {
            this.CommSetting = commSetting;
            InitDevice();
            this.Name = "DPSEX";
        }
        public DPSEX()
        {
            _CommSetting = new CommSetting();
        }
        #endregion

        #region 属性
        const int RETRY_COUNT = 3;
        /// <summary>
        /// 线程锁
        /// </summary>
        private object _lock = new object();

        /// <summary>
        /// DPSEX通讯库类
        /// </summary>
        public DPSEXBase DPSEXBase
        {
            get
            {
                DPSEXBase dpsex = CommInstance as DPSEXBase;
                return dpsex;
            }
        }

        /// <summary>
        /// 线性插值通讯实例
        /// </summary>
        public DPSEXTradCalBase DPSEXTradCal
        {
            get
            {
                DPSEXTradCalBase dpsex = CommInstance as DPSEXTradCalBase;
                return dpsex;
            }
        }

        /// <summary>
        /// 多点拟合通讯实例
        /// </summary>
        public DPSEXMulTCalBase DPSEXMulTCal
        {
            get
            {
                DPSEXMulTCalBase dpsex = CommInstance as DPSEXMulTCalBase;
                if (dpsex == null) dpsex = new DPSEXMulTCalBase(CommSettingInstance);
                return dpsex;
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
        
        private int _Digit;
        /// <summary>
        /// 显示位
        /// </summary>
        public int Digit
        {
            get { return _Digit; }
            set
            {
                _Digit = value;
            }
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
                _SequenceNumber = value;
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
                _IsEnable = value;
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
                _IsNotScaning = value;
            }
        }
        private bool _IsOnline;
        /// <summary>
        /// 标识设备在线状态
        /// </summary>
        public bool IsOnline
        {
            get { return _IsOnline; }
            set
            {
                _IsOnline = value;
            }
        }

        private Xmas11.Comm.Core.CommSettings _CommSettingInstance;
        /// <summary>
        /// 通讯配置实例
        /// </summary>
        public Xmas11.Comm.Core.CommSettings CommSettingInstance
        {
            get { return _CommSettingInstance; }
            set
            {
                _CommSettingInstance = value;
            }
        }
        private CommSetting _CommSetting;
        /// <summary>
        /// 通讯配置
        /// </summary>
        public CommSetting CommSetting
        {
            get { return _CommSetting; }
            set
            {
                _CommSetting = value;
            }
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
                _Unit = value;
            }
        }
        private Xmas11.Domain.Mechanics.PressureRange _Range;
        /// <summary>
        /// 压力量程
        /// </summary>
        public Xmas11.Domain.Mechanics.PressureRange Range
        {
            get { return _Range; }
            set
            {
                _Range = value;
            }
        }

        private PressureType _PressureType;
        /// <summary>
        /// 压力类型
        /// </summary>
        public PressureType PressureType
        {
            get { return _PressureType; }
            set
            {
                _PressureType = value;
            }
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
                _CurrentPressure = value;
            }
        }

        private Xmas11.Domain.Thermology.Temperature _CurrentTemperature;
        /// <summary>
        ///当前温度值
        /// </summary>
        public Xmas11.Domain.Thermology.Temperature CurrentTemperature
        {
            get { return _CurrentTemperature; }
            set
            {
                _CurrentTemperature = value;
            }
        }

        private string _SerialNumber;
        /// <summary>
        /// 序列号
        /// </summary>
        public string SerialNumber
        {
            get { return _SerialNumber; }
            set
            {
                _SerialNumber = value;
            }
        }

        private string _Name;
        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
            }
        }

        private string _DeviceType;
        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceType
        {
            get { return _DeviceType; }
            set
            {
                _DeviceType = value;
            }
        }
        private string _DeviceVersion;
        /// <summary>
        /// 设备固件版本
        /// </summary>
        public string DeviceVersion
        {
            get { return _DeviceVersion; }
            set
            {
                _DeviceVersion = value;
            }
        }

        /// <summary>
        /// 通讯实例
        /// </summary>
        public BaseDevice CommInstance { get; set; }
        public List<DeviceRole> Role { get; set; }


        #endregion

        #region 方法
        #region 基本方法
        /// <summary>
        /// 关闭通讯
        /// </summary>
        /// <returns></returns>
        public virtual bool Close()
        {
            try
            {
                if (CommInstance != null)
                {
                    CommInstance.Close();
                }
                return !CommInstance.Connected;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 获取背光状态
        /// </summary>
        /// <returns></returns>
        public virtual OpenCloseState GetBacklightingState()
        {
            return OpenCloseState.UnKnown;
        }
        /// <summary>
        /// 获取设备类型
        /// </summary>
        /// <returns></returns>
        public virtual string GetDeviceType()
        {
            lock (_lock)
            {
                return DPSEXTradCal.GetName();
            }
        }
        /// <summary>
        /// 获取设备名称
        /// </summary>
        /// <returns></returns>
        public virtual string GetName()
        {
            return DPSEXTradCal.GetName();
        }
        /// <summary>
        /// 获取激励值
        /// </summary>
        /// <returns></returns>
        public virtual double GetORIV()
        {
            return DPSEXTradCal.GetSensorPowerSupplyValue().Result;
        }
        /// <summary>
        /// 获取压力值
        /// </summary>
        /// <returns></returns>
        public Xmas11.Domain.Mechanics.Pressure GetPressure()
        {
            lock (_lock)
            {
                var response = DPSEXTradCal.GetPressure();
                if (response.IsCorrect)
                {
                    var pressure = response.Result;
                    this.CurrentPressure = pressure;
                    return pressure;
                }
                else
                {
                    var pressure = new Press(0, PressureUnit.kPa);
                    this.CurrentPressure = pressure;
                    return pressure;
                }
               
            }

        }
        /// <summary>
        /// 获取压力量程
        /// </summary>
        /// <returns></returns>
        public virtual Xmas11.Domain.Mechanics.PressureRange GetPressureRange()
        {
            lock (_lock)
            {
                var response = DPSEXTradCal.GetPressureRange();
                if (response.IsCorrect)
                {
                    var pressureRange = response.Result;
                    return pressureRange;
                }
                else
                {
                    var pressureRange = new Xmas11.Domain.Mechanics.PressureRange(0,100, PressureUnit.kPa);
                    return pressureRange;
                }
            }
        }
        /// <summary>
        /// 获取压力类型
        /// </summary>
        /// <returns></returns>
        public virtual PressureType GetPressureType()
        {
            lock (_lock)
            {
                return DPSEXTradCal.GetPressureType().Result;
            }
        }
        /// <summary>
        /// 获取压力单位
        /// </summary>
        /// <returns></returns>
        public virtual Xmas11.Domain.Unit GetPressureUnit()
        {
            return DPSEXTradCal.GetPressureUnit().Result;
        }
        /// <summary>
        /// 获取序列号
        /// </summary>
        /// <returns></returns>
        public virtual string GetSerialNumber()
        {
            lock (_lock)
            {
                return DPSEXTradCal.GetSerialNumber().Result;
            }
        }
        /// <summary>
        /// 获取温度值
        /// </summary>
        /// <returns></returns>
        public double GetTemperature()
        {
            lock (_lock)
            {
                var temperature = DPSEXTradCal.GetTemperature();
                this.CurrentTemperature = temperature.Result;
                return temperature.Result.Value;
            }

        }
        /// <summary>
        /// 获取设备固件版本
        /// </summary>
        /// <returns></returns>
        public virtual string GetVersion()
        {
            lock (_lock)
            {
                return DPSEXTradCal.GetVersion().Result;
            }
        }
        /// <summary>
        /// 设备是否存在
        /// </summary>
        /// <returns></returns>
        public virtual bool IsExist()
        {
            lock (_lock)
            {
                return DPSEXTradCal.IsExist();
            }
        }
        /// <summary>
        /// 打开通讯
        /// </summary>
        /// <returns></returns>
        public virtual bool Open()
        {
            return CommInstance.Open();
        }
        /// <summary>
        /// 压力清零
        /// </summary>
        /// <returns></returns>
        public virtual bool PressureZero()
        {
            var errorCode = DPSEXTradCal.PressureZero().IsCorrect;
            return errorCode;
        }
        /// <summary>
        /// 设置背光状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public virtual bool SetBacklightingState(OpenCloseState state)
        {
            return true;
        }
        /// <summary>
        /// 设置压力类型
        /// </summary>
        /// <param name="pressureType"></param>
        /// <returns></returns>
        public virtual bool SetPressureType(PressureType pressureType)
        {
            var response = DPSEXTradCal.SetPressureType(pressureType).IsCorrect;
            return response;
        }
        /// <summary>
        /// 设置压力单位
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public virtual bool SetPressureUnit(Xmas11.Domain.Unit unit)
        {
            var response = DPSEXTradCal.SetPressureUnit(unit).IsCorrect;
            return response;
        }
        /// <summary>
        /// 开始数据下载
        /// </summary>
        /// <param name="calibrationData"></param>
        /// <param name="calibrationType"></param>
        /// <returns></returns>
        public virtual bool StartDownloadCalibrationData(string calibrationData, CalibrationType calibrationType)
        {
            lock (_lock)
            {
                if (!DPSEXTradCal.RS_SafeStart().IsCorrect) return false;
                if (!DPSEXTradCal.RS_Stop(true).IsCorrect) return false;
                return DPSEXTradCal.ResumeAllPressureCalDataByOriginal(calibrationData, calibrationType).IsCorrect;
            }

        }
        /// <summary>
        /// 恢复放大倍数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual bool ResumeAD(ADData data)
        {
            lock (_lock)
            {
                return DPSEXTradCal.AD_ResumeAD(data.Times, data.Input).IsCorrect;
            }

        }
        /// <summary>
        /// 恢复量程设置数据
        /// </summary>
        /// <param name="rsData"></param>
        /// <returns></returns>
        public virtual bool ResumeRangeSettingData(RangeSettingData rsData)
        {
            lock (_lock)
            {
                return DPSEXTradCal.RS_ResumeRangeSettingData(rsData).IsCorrect;
            }

        }
        #endregion

        #region 标定AD放大倍数
        /// <summary>
        /// 开始标定放大倍数
        /// </summary>
        /// <returns></returns>
        public virtual bool StartCalibrationAD()
        {
            lock (_lock)
            {
                bool result = false;
                int tryCount = 0;
                while (!result)
                {
                    result = DPSEXTradCal.RS_SafeStart().IsCorrect;
                    tryCount++;
                    if (tryCount >= RETRY_COUNT) break;
                }
                return result;
            }
        }

        /// <summary>
        /// 设置放大倍数值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool SetADValue(int value)
        {
            lock (_lock)
            {
                bool result = false;
                int tryCount = 0;
                while (!result)
                {
                    result = DPSEXTradCal.AD_SetAD((ADTimes)Enum.ToObject(typeof(ADTimes), value)).IsCorrect;
                    tryCount++;
                    if (tryCount >= RETRY_COUNT) break;
                }
                return result;
            }
        }

        /// <summary>
        /// 获取放大倍数原始标识值
        /// </summary>
        /// <returns></returns>
        public virtual double GetADOriginalFlagValue()
        {
            lock (_lock)
            {
                bool result = false;
                int tryCount = 0;
                iResponse<double> response = new iResponse<double>();
                while (!result)
                {
                    response = DPSEXTradCal.GetADOriginalFlagValue();
                    if (response.IsCorrect)
                    {
                        this.CurrentTemperature = new Xmas11.Domain.Thermology.Temperature(response.Result, Xmas11.Domain.Thermology.TemperatureUnit.C);
                        return response.Result;
                    }
                    tryCount++;
                    if (tryCount >= RETRY_COUNT) break;
                }
                return response.Result;
            }
        }

        /// <summary>
        /// 保存放大倍数值
        /// </summary>
        /// <returns></returns>
        public virtual bool SaveADValue()
        {
            return true;
        }

        /// <summary>
        /// 获取放大倍数值
        /// </summary>
        /// <returns></returns>
        public virtual ADData GetADData()
        {
            return new ADData(DPSEXTradCal.AD_GetADTimes().Result);
        }
        #endregion

        #region 量程标定
        /// <summary>
        /// 开始标定量程
        /// </summary>
        /// <returns></returns>
        public virtual bool StartCalibrationRange()
        {
            lock (_lock)
            {
                bool result = false;
                int tryCount = 0;
                while (!result)
                {
                    result = DPSEXTradCal.RS_SafeStart().IsCorrect;
                    tryCount++;
                    if (tryCount >= RETRY_COUNT) break;
                }
                return result;
            }
        }
        /// <summary>
        /// 设置标定量程
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public virtual bool SetCalibrationRange(Xmas11.Domain.Mechanics.PressureRange range, Xmas11.Domain.Electricity.VoltageRange voltageRange)
        {
            var rangedata = new RangeSettingData(range, voltageRange);
            //标定量程上下限
            if (!StartCalibrationRange()) return false;
            System.Threading.Thread.Sleep(500);
            if (!CalibrationRangeLower(range.LowerValue)) return false;
            if (!CalibrationRangeUpper(range.UpperValue)) return false;
            System.Threading.Thread.Sleep(1000);
            //if (StopAndSaveCalibrationRange()) return false;
            //设置KB值
            //if (!StartCalibrationRange()) return false;
            if (!DPSEXTradCal.RS_SetRangeSettingData(rangedata).IsCorrect) return false;
            System.Threading.Thread.Sleep(1000);
            if (!StopAndSaveCalibrationRange()) return false;
            return true;
        }
        /// <summary>
        /// 标定量程上限
        /// </summary>
        /// <returns></returns>
        public virtual bool CalibrationRangeUpper(double pressure)
        {
            lock (_lock)
            {
                bool result = false;
                int tryCount = 0;
                while (!result)
                {
                    result = DPSEXTradCal.RS_SetPressureRangeUpper(pressure).IsCorrect;
                    tryCount++;
                    if (tryCount >= RETRY_COUNT) break;
                }
                return result;
            }
        }
        /// <summary>
        /// 标定量程下限
        /// </summary>
        /// <returns></returns>
        public virtual bool CalibrationRangeLower(double pressure)
        {
            lock (_lock)
            {
                bool result = false;
                int tryCount = 0;
                while (!result)
                {
                    result = DPSEXTradCal.RS_SetPressureRangeLower(pressure).IsCorrect;
                    tryCount++;
                    if (tryCount >= RETRY_COUNT) break;
                }
                return result;
            }
        }
        /// <summary>
        /// 停止并保存标定量程
        /// </summary>
        /// <returns></returns>
        public virtual bool StopAndSaveCalibrationRange()
        {
            lock (_lock)
            {
                bool result = false;
                int tryCount = 0;
                while (!result)
                {
                    var response = DPSEXTradCal.RS_Stop(true);
                    result = response.IsCorrect;
                    if (!result)
                    {
                        //Debug.WriteLineIf(!result, response.ErrorCode);
                    }

                    tryCount++;
                    if (tryCount >= RETRY_COUNT) break;
                }
                return result;
            }
        }
        /// <summary>
        /// 获取标定量程数据
        /// </summary>
        /// <returns></returns>
        public virtual RangeSettingData GetCalibrationRangeData()
        {
            lock (_lock)
            {
                int tryCount = 0;
                var response = DPSEXTradCal.RS_GetRangeSettingData();
                while (!response.IsCorrect)
                {
                    tryCount++;
                    response = DPSEXTradCal.RS_GetRangeSettingData();
                    if (tryCount >= RETRY_COUNT) break;
                }
                return response.Result;
            }
        }
        #endregion

        #region 多点拟合温度补偿
        /// <summary>
        /// 温度补偿-----多点拟合-----安全地开始温度补偿
        /// </summary>
        /// <returns></returns>
        public virtual bool MulT_SafeStartTemperatureCompensate()
        {
            return DPSEXMulTCal.MT_SafeStart().IsCorrect;
        }
        /// <summary>
        /// 温度补偿-----多点拟合-----设置温度补偿压力阶数与温度阶数
        /// </summary>
        /// <param name="pOrder">压力阶数</param>
        /// <param name="tOrder">温度阶数</param>
        /// <returns></returns>
        public virtual bool MulT_SetTemperatureCompensateOrder(int pOrder, int tOrder)
        {
            return DPSEXMulTCal.MT_SetMTOrder(pOrder, tOrder).IsCorrect;
        }
        /// <summary>
        /// 温度补偿-----多点拟合-----获取温度补偿压力阶数
        /// </summary>
        /// <returns></returns>
        public virtual int MulT_GetPressureOrder()
        {
            return DPSEXMulTCal.MT_GetPressurePow().Result;
        }
        /// <summary>
        /// 温度补偿-----多点拟合-----获取温度补偿温度阶数
        /// </summary>
        /// <returns></returns>
        public virtual int MulT_GetTemperatureOrder()
        {
            return DPSEXMulTCal.MT_GetTemperaturePow().Result;
        }
        /// <summary>
        /// 温度补偿-----多点拟合-----设置温度补偿矩阵数据
        /// </summary>
        /// <param name="dataArray"></param>
        /// <returns></returns>
        public virtual bool MulT_SetTemperatureCompensateMatrixData(string[] dataArray)
        {
            return DPSEXMulTCal.MT_SetMatrixData(dataArray).IsCorrect;
        }
        /// <summary>
        /// 温度补偿-----多点拟合-----获取温度补偿矩阵数据
        /// </summary>
        /// <returns></returns>
        public virtual string[] MulT_GetTemperatureCompensateMatrixData(int count)
        {
            return DPSEXMulTCal.MT_GetMatrixData(count).Result;
        }
        /// <summary>
        /// 温度补偿-----多点拟合-----安全地停止温度补偿
        /// </summary>
        /// <returns></returns>
        public virtual bool MulT_SelfStopTemperatureCompensate()
        {
            return DPSEXMulTCal.MT_SafeStopAndSave().IsCorrect;
        }
        /// <summary>
        /// 温度补偿-----多点拟合-----取消温度补偿
        /// </summary>
        /// <returns></returns>
        public virtual bool MulT_CalcelTemperatureCompensate()
        {
            return DPSEXMulTCal.MT_Cancel().IsCorrect;
        }
        #endregion

        #region 线性差值温度补偿
        /// <summary>
        /// 温度补偿-----线性差值-----是否包含温度补偿数据
        /// </summary>
        /// <returns></returns>
        public virtual bool Trad_IsContainsTemperatureCompensateData()
        {
            return DPSEXTradCal.TC_IsContainsLCData().IsCorrect;
        }
        /// <summary>
        /// 温度补偿-----线性差值-----安全地开始温度补偿
        /// </summary>
        /// <returns></returns>
        public virtual bool Trad_SafeStartTemperatureCompensate()
        {
            return DPSEXTradCal.TC_SafeStart().IsCorrect;
        }
        // <summary>
        /// 温度补偿-----线性差值-----设置温度补偿数据
        /// </summary>
        /// <param name="dataList">数据列表</param>
        /// <param name="referenceNum">温度参考点序号</param>
        /// <returns></returns>
        public virtual bool Trad_SetTemperatureCompensateData(List<TemperatureCompensationData> dataList, int referenceNum)
        {
            return DPSEXTradCal.TC_SetAllData(dataList, referenceNum).IsCorrect;
        }
        /// <summary>
        /// 温度补偿-----线性差值-----获取温度补偿数据集合
        /// </summary>
        /// <returns></returns>
        public virtual List<TemperatureCompensationData> Trad_GetTemperatureCompensateDataList()
        {
            return DPSEXTradCal.TC_GetAllData().Result;
        }

        /// <summary>
        /// 温度补偿-----线性差值-----安全地停止温度补偿
        /// </summary>
        /// <returns></returns>
        public virtual bool Trad_SafeStopTemperatureCompensate()
        {
            return DPSEXTradCal.TC_Stop(true).IsCorrect;
        }
        #endregion

        #region 线性差值修正
        /// <summary>
        /// 开始线性差值修正
        /// </summary>
        /// <returns></returns>
        public virtual bool StartLinearDifferenceCorrection()
        {
            return DPSEXTradCal.LC_SafeStart().IsCorrect;
        }
        /// <summary>
        /// 设置线性差值修正点总数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool SetLinearDifferenceCorrectionCount(double value)
        {
            return DPSEXTradCal.LC_SetCount(Convert.ToInt32(value)).IsCorrect;
        }
        /// <summary>
        /// 设置线性差值修正数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual bool SetLinearDifferenceCorrectionData(List<LinearCorrectionData> data)
        {
            return DPSEXTradCal.LC_SetAllData(data).IsCorrect;
        }
        /// <summary>
        /// 获取线性差值修正数据
        /// </summary>
        /// <returns></returns>
        public virtual List<LinearCorrectionData> GetLinearDifferenceCorrectionData()
        {
            return DPSEXTradCal.LC_GetAllData().Result;
        }
        /// <summary>
        /// 保存并停止线性差值修正
        /// </summary>
        /// <returns></returns>
        public virtual bool SaveAndStopLinearDifferenceCorrection()
        {
            return DPSEXTradCal.LC_Stop(true).IsCorrect;
        }
        /// <summary>
        /// 设置工作模式
        /// </summary>
        /// <param name="workMode"></param>
        /// <returns></returns>
        #endregion

        #region 压力校准
        public virtual bool SetWorkMode(PressureWorkMode workMode)
        {
            try
            {
                return DPSEXTradCal.SetPressureWorkMode(workMode).IsCorrect;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        /// <summary>
        /// 获取原始压力值
        /// </summary>
        /// <returns></returns>
        public virtual Xmas11.Domain.Mechanics.Pressure GetOriginalPressureValue()
        {
            return DPSEXBase.GetOriginalPressure().Result;
        }
        /// <summary>
        /// 压力下限校准
        /// </summary>
        /// <param name="pressure">压力值</param>
        /// <returns></returns>
        public virtual bool PressureLower_Calibration(Xmas11.Domain.Mechanics.Pressure pressure)
        {
            return DPSEXBase.PCal_CalLower(pressure).IsCorrect;
        }
        /// <summary>
        /// 压力中间点校准
        /// </summary>
        /// <param name="pressure"></param>
        /// <returns></returns>
        public virtual bool PressureMiddle_Calibration(Xmas11.Domain.Mechanics.Pressure pressure)
        {
            return DPSEXBase.PCal_CalMiddle(pressure).IsCorrect;
        }
        /// <summary>
        /// 压力上限校准
        /// </summary>
        /// <param name="pressure"></param>
        /// <returns></returns>
        public virtual bool PressureUpper_Calibration(Xmas11.Domain.Mechanics.Pressure pressure)
        {
            return DPSEXBase.PCal_CalUpper(pressure).IsCorrect;
        }
        /// <summary>
        /// 获取校准数据
        /// </summary>
        /// <param name="calItem">下限、中间点、上限</param>
        /// <returns></returns>
        public virtual PressureCalData GetCalibrationData(PressureCalItem calItem)
        {
            return DPSEXBase.PCal_GetOneData(calItem).Result;
        }
        #endregion

        #region 初始化设备及信息
        /// <summary>
        /// 初始化设备通讯
        /// </summary>
        public virtual void InitDevice()
        {
            CommSettingInstance = new SerialPortCommSettings("COM1", 9600, 8, StopBits.Two, Parity.None);
            if (_CommSetting is SocketCommSetting)
            {

                CommSettingInstance = new SocketCommSettings(IPAddress.Parse(((SocketCommSetting)_CommSetting).IpAddress), ((SocketCommSetting)_CommSetting).Port, true);
            }
            else if (_CommSetting is SerialPortCommSetting)
            {
                CommSettingInstance = new SerialPortCommSettings(((SerialPortCommSetting)_CommSetting).SerialPortName, ((SerialPortCommSetting)_CommSetting).Baudrate, ((SerialPortCommSetting)_CommSetting).DataBits, ((SerialPortCommSetting)_CommSetting).StopBits, ((SerialPortCommSetting)_CommSetting).Parity);
            }
            else if (_CommSetting is UsbCommSetting)
            {
                CommSettingInstance = new UsbCommSettings(((UsbCommSetting)_CommSetting).Vid, ((UsbCommSetting)_CommSetting).Pid, ((UsbCommSetting)_CommSetting).Location);
            }
            CommInstance = new DPSEXTradCalBase(CommSettingInstance);
            CommInstance.Policy = new Xmas11.Comm.Commander.iPolicy(3000, 100, 3);
            if (Role==null)
            {
                Role = new List<DeviceRole>();
            }
        }
        /// <summary>
        /// 初始化设备基本信息
        /// </summary>
        public virtual void GetBaseInfo()
        {
            this.SerialNumber = GetSerialNumber();
            this.DeviceType = GetDeviceType();
            this.DeviceVersion = GetVersion();
            this.Name = GetName();
            this.PressureType = GetPressureType();
            this.Range = GetPressureRange();
        }
        #endregion

        #region 校准相关
        public virtual bool ValidCalComExists()
        {
            lock (_lock)
            {
                bool result = true;
                for (int i = 0; i < 3; i++)
                {
                    //原版本有读取厂家校准数据的指令，所以验证需要进行写入指令的验证
                    PressureCalData calData = new PressureCalData(Xmas11.Comm.Data.Common.PressureCalItem.Z, 0, 0, PressureUnit.kPa);
                    iResponse getv = DPSEXTradCal.PCal_SetOneCal(calData);
                    if (!getv.IsCorrect)
                    {
                        if (getv.ErrorCode.Code == "1018")
                        {
                            result = false;
                            break;
                        }
                        else
                        {
                            Thread.Sleep(500);
                        }
                    }
                }
                return result;
            }
        }
        /// <summary>
        /// 退出校准
        /// </summary>
        /// <returns></returns>
        public virtual bool PressureCal_1_Exit()
        {
            lock (_lock)
            {
                bool isSucess = false;
                for (int i = 0; i < 5; i++)
                {
                    iResponse getv = DPSEXTradCal.PCal_Stop(false);

                    if (getv.ErrorCode.Code == "1001")
                    {
                        isSucess = true;
                        break;
                    }
                    else
                    {
                        Thread.Sleep(2000);
                    }
                }
                return isSucess;
            }
        }

        /// <summary>
        /// 开始校准
        /// </summary>
        /// <returns></returns>
        public virtual bool PressureCal_2_Start()
        {
            lock (_lock)
            {
                bool isSucess = false;
                for (int i = 0; i < 5; i++)
                {
                    iResponse getv = DPSEXTradCal.PCal_Start();

                    if (getv.IsCorrect)
                    {
                        isSucess = true;
                        break;
                    }
                    else
                    {
                        Thread.Sleep(2000);
                    }
                }
                return isSucess;
            }
        }

        /// <summary>
        /// 执行校准
        /// </summary>
        /// <param name="item"></param>
        /// <param name="pressure"></param>
        /// <returns></returns>
        public virtual bool PressureCal_3_Cal(Xmas11.Comm.Data.Common.PressureCalItem item, Xmas11.Domain.Mechanics.Pressure pressure)
        {
            lock (_lock)
            {
                bool isSucess = false;
                for (int i = 0; i < 5; i++)
                {
                    iResponse getv = DPSEXTradCal.PCal_Cal(item, pressure);

                    if (getv.IsCorrect)
                    {
                        isSucess = true;
                        break;
                    }
                    else
                    {
                        Thread.Sleep(2000);
                    }
                }
                return isSucess;
            }
        }

        /// <summary>
        /// 完成校准
        /// </summary>
        /// <returns></returns>
        public virtual bool PressureCal_4_Finish()
        {
            lock (_lock)
            {
                bool isSucess = false;
                for (int i = 0; i < 5; i++)
                {
                    iResponse getv = DPSEXTradCal.PCal_Stop(true);

                    if (getv.IsCorrect)
                    {
                        isSucess = true;
                        break;
                    }
                    else
                    {
                        Thread.Sleep(2000);
                    }
                }
                return isSucess;
            }
        }

        /// <summary>
        /// 回读校准数据
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual PressureCalData GetCalOneData(PressureCalItem item, out bool isContain)
        {
            isContain = true;
            lock (_lock)
            {
                PressureCalData result = PressureCalData.Empty;
                for (int i = 0; i < 5; i++)
                {
                    iResponse<PressureCalData> getv = DPSEXTradCal.PCal_GetOneData(item);
                    if (getv.IsCorrect)
                    {
                        result = getv.Result;
                        break;
                    }
                    else
                    {
                        if (getv.ErrorCode.Code == "1018")
                        {
                            isContain = false;
                        }
                        Thread.Sleep(2000);
                    }
                }
                return result;
            }
        }


        /// <summary>
        /// 写入全部校准数据
        /// </summary>
        /// <param name="calDataList"></param>
        /// <returns></returns>
        public virtual bool SetCalAllData(List<PressureCalData> calDataList)
        {
            lock (_lock)
            {
                bool isSucess = false;
                for (int i = 0; i < 5; i++)
                {
                    iResponse getv = DPSEXTradCal.PCal_SetAllCal(calDataList);

                    if (getv.IsCorrect)
                    {
                        isSucess = true;
                        break;
                    }
                    else
                    {
                        Thread.Sleep(2000);
                    }
                }
                return isSucess;
            }
        }

        public virtual bool CalibrateCancel()
        {
            lock (_lock)
            {
                bool isSucess = false;
                for (int i = 0; i < 5; i++)
                {
                    iResponse getv = DPSEXTradCal.PCal_CancelCal();
                    if (getv.IsCorrect)
                    {
                        isSucess = true;
                        break;
                    }
                    else
                    {
                        Thread.Sleep(2000);
                    }
                }
                return isSucess;
            }
        }
        #endregion

        /// <summary>
        /// 恢复全部校准数据（发送的指令较多，执行完毕可能需要一定时间）
        /// </summary>
        /// <param name="originalData">全部原始校准数据</param>
        /// <param name="calType">校准类型</param>
        /// <returns></returns>
        public virtual bool ResumeAllPressureCalDataByOriginal(string originalData, Xmas11.Comm.Data.Common.CalibrationType calType)
        {
            return DPSEXTradCal.ResumeAllPressureCalDataByOriginal(originalData, calType).IsCorrect;
        }


        /// <summary>
        /// 设置回差修正
        /// </summary>
        /// <param name="workMode"></param>
        /// <returns></returns>
        public virtual bool SetOffset(double value)
        {
            return DPSEXTradCal.SetOffset(value).IsCorrect;
        }
        /// <summary>
        /// 关闭回差修正
        /// </summary>
        /// <returns></returns>
        public virtual bool CloseOffset()
        {
            return DPSEXTradCal.CloseOffset().IsCorrect;
        }
        #endregion
    }
}