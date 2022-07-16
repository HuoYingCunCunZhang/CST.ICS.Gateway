using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xmas11.Comm.Data.Common;
using Xmas11.Domain.Electricity;
using Xmas11.Domain.Mechanics;
using Xmas11.Domain.Thermology;

namespace CST.ICS.Gateway.Pressure.IDevice
{
    public interface IPressureStdDevice : IBasePressureDevice
    {
        #region 放大倍数标定

        /// <summary>
        /// 开始标定放大倍数
        /// </summary>
        /// <returns></returns>
        bool StartCalibrationAD();

        /// <summary>
        /// 设置放大倍数值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool SetADValue(int value);

        /// <summary>
        /// 获取放大倍数原始标识值
        /// </summary>
        /// <returns></returns>
        double GetADOriginalFlagValue();

        /// <summary>
        /// 保存放大倍数值
        /// </summary>
        /// <returns></returns>
        bool SaveADValue();

        /// <summary>
        /// 获取放大倍数值
        /// </summary>
        /// <returns></returns>
        ADData GetADData();

        /// <summary>
        /// 放大倍数——恢复放大倍数（此操作包含写入数据、验证数据，过程中发送的指令较多，执行完毕可能需要一定时间）
        /// </summary>
        /// <param name="data">放大倍数</param>
        /// <returns></returns>
        bool ResumeAD(ADData data);

        #endregion

        #region 量程标定

        /// <summary>
        /// 开始标定量程
        /// </summary>
        /// <returns></returns>
        bool StartCalibrationRange();

        /// <summary>
        /// 设置标定量程
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        bool SetCalibrationRange(PressureRange range, VoltageRange voltageRange);

        /// <summary>
        /// 标定量程上限
        /// </summary>
        /// <returns></returns>
        bool CalibrationRangeUpper(double pressure);

        /// <summary>
        /// 标定量程下限
        /// </summary>
        /// <returns></returns>
        bool CalibrationRangeLower(double pressure);

        /// <summary>
        /// 停止并保存标定量程
        /// </summary>
        /// <returns></returns>
        bool StopAndSaveCalibrationRange();

        /// <summary>
        /// 获取标定量程数据
        /// </summary>
        /// <returns></returns>
        RangeSettingData GetCalibrationRangeData();

        /// <summary>
        /// 量程设置——恢复量程设置数据（此操作包含进入模式、写入数据、安全退出、验证数据，过程中发送的指令较多，执行完毕可能需要一定时间）
        /// </summary>
        /// <param name="rsData"></param>
        /// <returns></returns>
        bool ResumeRangeSettingData(RangeSettingData rsData);

        #endregion

        #region 多点拟合温度补偿

        /// <summary>
        /// 温度补偿-----多点拟合-----安全地开始温度补偿
        /// </summary>
        /// <returns></returns>
        bool MulT_SafeStartTemperatureCompensate();

        /// <summary>
        /// 温度补偿-----多点拟合-----设置温度补偿压力阶数与温度阶数
        /// </summary>
        /// <param name="pOrder">压力阶数</param>
        /// <param name="tOrder">温度阶数</param>
        /// <returns></returns>
        bool MulT_SetTemperatureCompensateOrder(int pOrder, int tOrder);

        /// <summary>
        /// 温度补偿-----多点拟合-----获取温度补偿压力阶数
        /// </summary>
        /// <returns></returns>
        int MulT_GetPressureOrder();

        /// <summary>
        /// 温度补偿-----多点拟合-----获取温度补偿温度阶数
        /// </summary>
        /// <returns></returns>
        int MulT_GetTemperatureOrder();

        /// <summary>
        /// 温度补偿-----多点拟合-----设置温度补偿矩阵数据
        /// </summary>
        /// <param name="dataArray"></param>
        /// <returns></returns>
        bool MulT_SetTemperatureCompensateMatrixData(string[] dataArray);

        /// <summary>
        /// 温度补偿-----多点拟合-----获取温度补偿矩阵数据
        /// </summary>
        /// <returns></returns>
        string[] MulT_GetTemperatureCompensateMatrixData(int count);

        /// <summary>
        /// 温度补偿-----多点拟合-----安全地停止温度补偿
        /// </summary>
        /// <returns></returns>
        bool MulT_SelfStopTemperatureCompensate();

        /// <summary>
        /// 温度补偿-----多点拟合-----取消温度补偿
        /// </summary>
        /// <returns></returns>
        bool MulT_CalcelTemperatureCompensate();

        #endregion

        #region 其他
        /// <summary>
        /// 获取激励值
        /// </summary>
        /// <returns></returns>
        double GetORIV();

        /// <summary>
        /// 获取温度值
        /// </summary>
        /// <returns></returns>
        double GetTemperature();
        /// <summary>
        /// 当前温度值
        /// </summary>
        Temperature CurrentTemperature { get; set; }
        /// <summary>
        /// 设置工作模式
        /// </summary>
        /// <param name="workMode"></param>
        /// <returns></returns>
        bool SetWorkMode(Xmas11.Comm.Data.Common.PressureWorkMode workMode);
        /// <summary>
        /// 设置回差修正
        /// </summary>
        /// <param name="workMode"></param>
        /// <returns></returns>
        bool SetOffset(double value);
        /// <summary>
        /// 关闭回差修正
        /// </summary>
        /// <returns></returns>
        bool CloseOffset();
        /// <summary>
        /// 开始数据下载
        /// </summary>
        /// <param name="calibrationData">校准数据</param>
        /// <param name="calibrationType">校准类型</param>
        /// <returns></returns>
        bool StartDownloadCalibrationData(string calibrationData, Xmas11.Comm.Data.Common.CalibrationType calibrationType);
        #endregion
    }
}
