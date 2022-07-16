using CST.ICS.Gateway.Pressure.Command;
using GDZ9.Model.ICS.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xmas11.Domain.Thermology;
using Pressure1 = Xmas11.Domain.Mechanics.Pressure;

namespace CST.ICS.Gateway.Pressure.Command
{
    public interface ITemperatureCntrolCommand : IBaseCommand<Temperature>
    {
        public CommandReturnData<string> TargetTemp(string value);
        public CommandReturnData<string> StartControl();
        public CommandReturnData<string> StopControl();
    }
}
