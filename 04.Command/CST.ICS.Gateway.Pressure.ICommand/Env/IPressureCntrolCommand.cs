using CST.ICS.Gateway.Pressure.Command;
using GDZ9.Model.ICS.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pressure1 = Xmas11.Domain.Mechanics.Pressure;

namespace CST.ICS.Gateway.Pressure.Command
{
    public interface IPressureCntrolCommand:IBaseCommand<Pressure1>
    {
        /// <summary>
        /// 排空
        /// </summary>
        /// <returns></returns>
        public CommandReturnData<string> Vent();

        /// <summary>
        /// 貌似没用
        /// </summary>
        /// <returns></returns>
        //public CommandReturnData<string> Measure();

        /// <summary>
        /// 设置量程
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public CommandReturnData<string> Range(string param);
        /// <summary>
        /// 设定目标值
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public CommandReturnData<string> TargetValue(string param);

        /// <summary>
        /// 设定目标单位
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public CommandReturnData<string> TargetUnit(string param);
    }
}
