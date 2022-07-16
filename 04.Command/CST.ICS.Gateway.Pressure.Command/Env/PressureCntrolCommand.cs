using CST.ICS.Gateway.Model;
using CST.ICS.Gateway.Pressure.Command;
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
    public class PressureCntrolCommand:IPressureCntrolCommand
    {
        public List<DeviceUnit<UUTLiveData>> Devices { get; set; }
        public CommandReturnData<string> LiveData(string parameter)
        {
            throw new NotImplementedException();
        }

        public CommandReturnData<string> ScanDevices(string parameter)
        {
            throw new NotImplementedException();
        }

        public CommandReturnData<Dictionary<string, bool>> Disable(string parameter)
        {
            throw new NotImplementedException();
        }

        public CommandReturnData<string> Zero()
        {
            throw new NotImplementedException();
        }

        public CommandReturnData<Dictionary<string, Pressure1>> CurrentValueQuery()
        {
            throw new NotImplementedException();
        }

        public CommandReturnData<string> SentencedToSteady(string parameter)
        {
            throw new NotImplementedException();
        }

        public CommandReturnData<BalanceResult<Pressure1>> SentencedToSteadyQuery()
        {
            throw new NotImplementedException();
        }

        public CommandReturnData<string> Vent()
        {
            throw new NotImplementedException();
        }

        public CommandReturnData<string> Range(string param)
        {
            throw new NotImplementedException();
        }

        public CommandReturnData<string> TargetValue(string param)
        {
            throw new NotImplementedException();
        }

        public CommandReturnData<string> TargetUnit(string param)
        {
            throw new NotImplementedException();
        }
    }
}
