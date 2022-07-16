using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xmas11.Comm.Data.Common;
using Xmas11.Domain.Mechanics;
using Press = Xmas11.Domain.Mechanics.Pressure;
namespace CST.ICS.Gateway.Pressure.IDevice
{
    /// <summary>
    /// 压力控制接口
    /// </summary>
    public interface IPressureControlDevice : IBasePressureDevice
    {
        /// <summary>
        /// 当前目标压力值
        /// </summary>
        Press CurrentTargetPressure { get; set; }

        /// <summary>
        /// 当前控制状态
        /// </summary>
        PressureControlMode CurrentControlState { get; set; }

        /// <summary>
        /// 获取控制状态
        /// </summary>
        /// <returns></returns>
        PressureControlMode GetControlState();

        /// <summary>
        /// 设置控制状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        bool SetControlState(Xmas11.Comm.Data.Common.PressureControlMode state);

        /// <summary>
        /// 排空
        /// </summary>
        /// <returns></returns>
        bool Vent();

        /// <summary>
        /// 设置目标压力
        /// </summary>
        /// <param name="targetPressure">目标压力（值、单位）</param>
        /// <returns></returns>
        bool SetTargetPressure(Xmas11.Domain.Mechanics.Pressure targetPressure);

        /// <summary>
        /// 获取目标压力值
        /// </summary>
        /// <returns></returns>
        Press GetTargetPressure();
    }
}
