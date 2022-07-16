using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xmas11.Comm.Data.Common;
using Xmas11.Domain.Thermology;

namespace CST.ICS.Gateway.Pressure.IDevice
{
    /// <summary>
    /// (温控器)温度工装
    /// </summary>
    public interface ITemperatureDevice : IBaseDevice
    {
        #region 属性

        /// <summary>
        /// 温度量程
        /// </summary>
        TemperatureRange Range { get; }

        /// <summary>
        /// 当前温度值
        /// </summary>
        Temperature CurrentTemperature { get; set; }

        /// <summary>
        /// 当前高低温箱目标温度值
        /// </summary>
        Temperature CHTargetTemperature { get; set; }


        #endregion

        #region 方法
        /// <summary>
        /// 设置目标温度值
        /// </summary>
        /// <param name="targetTemperature">目标温度</param>
        /// <returns></returns>
        bool SetTargetTemperature(Temperature targetTemperature);

        /// <summary>
        /// 获取目标温度
        /// </summary>
        /// <returns></returns>
        Temperature GetTargetTemperature();

        /// <summary>
        /// 获取温度量程
        /// </summary>
        /// <returns></returns>
        TemperatureRange GetTemperatureRange();

        /// <summary>
        /// 获取当前温度值
        /// </summary>
        /// <returns></returns>
        Temperature GetCurrentTemperature();

        /// <summary>
        /// 设置压缩机状态
        /// </summary>
        /// <param name="state">状态</param>
        /// <returns></returns>
        bool SetCompressorState(Xmas11.Comm.Data.Common.OpenCloseState state);

        /// <summary>
        /// 获取压缩机状态
        /// </summary>
        /// <returns></returns>
        Xmas11.Comm.Data.Common.OpenCloseState GetCompressorState();

        /// <summary>
        /// 设置送风机状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        bool SetBlowerState(Xmas11.Comm.Data.Common.OpenCloseState state);

        /// <summary>
        /// 获取送风机状态
        /// </summary>
        /// <returns></returns>
        Xmas11.Comm.Data.Common.OpenCloseState GetBlowerState();

        /// <summary>
        /// 运行
        /// </summary>
        /// <returns></returns>
        bool Operate();

        /// <summary>
        /// 待机
        /// </summary>
        /// <returns></returns>
        bool Standby();

        /// <summary>
        /// 获取运行状态
        /// </summary>
        /// <returns></returns>
        OperateStandbyState GetOperationState();

        #endregion
    }
}
