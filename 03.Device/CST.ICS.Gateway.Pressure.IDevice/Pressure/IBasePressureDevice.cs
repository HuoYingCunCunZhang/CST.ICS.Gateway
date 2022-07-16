using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xmas11.Comm.Data.Common;
using Xmas11.Domain;
using Xmas11.Domain.Mechanics;
using Press = Xmas11.Domain.Mechanics.Pressure;

namespace CST.ICS.Gateway.Pressure.IDevice
{
    /// <summary>
    /// 压力基类接口
    /// </summary>
    public interface IBasePressureDevice : IBaseDevice
    {
        #region 属性

        /// <summary>
        /// 压力量程
        /// </summary>
        PressureRange Range { get; set; }

        /// <summary>
        /// 压力类型
        /// </summary>
        PressureType PressureType { get; set; }

        /// <summary>
        /// 当前压力值
        /// </summary>
        Press CurrentPressure { get; set; }

        /// <summary>
        /// 当前单位
        /// </summary>
        string Unit { get; set; }

        /// <summary>
        /// 显示位
        /// </summary>
        int Digit { get; set; }
        #endregion

        #region 方法

        /// <summary>
        /// 压力清零
        /// </summary>
        /// <returns></returns>
        bool PressureZero();

        /// <summary>
        /// 获取压力值
        /// </summary>
        /// <returns></returns>
        Press GetPressure();

        /// <summary>
        /// 设置压力单位
        /// </summary>
        /// <param name="unit">压力单位</param>
        /// <returns></returns>
        bool SetPressureUnit(Xmas11.Domain.Unit unit);

        /// <summary>
        /// 获取压力单位
        /// </summary>
        /// <returns></returns>
        Unit GetPressureUnit();

        /// <summary>
        /// 设置压力类型
        /// </summary>
        /// <param name="pressureType">压力类型</param>
        /// <returns></returns>
        bool SetPressureType(PressureType pressureType);

        /// <summary>
        /// 获取压力类型
        /// </summary>
        /// <returns></returns>
        PressureType GetPressureType();

        /// <summary>
        /// 获取压力量程
        /// </summary>
        /// <returns></returns>
        Xmas11.Domain.Mechanics.PressureRange GetPressureRange();

        #endregion
    }
}
