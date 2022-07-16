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
    public interface IPressureStdCommand : IBaseCommand<Pressure1>
    {

        /// <summary>
        /// 设定目标单位
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public CommandReturnData<string> TargetUnit(string param);
    }
}
