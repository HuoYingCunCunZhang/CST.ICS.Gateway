using CST.ICS.Gateway.Model;
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
    public class TemperatureStdCommand : ITemperatureStdCommand
    {
        public List<DeviceUnit<UUTLiveData>> Devices { get; set; }
        public CommandReturnData<Dictionary<string, Temperature>> CurrentValueQuery()
        {
            throw new NotImplementedException();
        }

        public CommandReturnData<Dictionary<string, bool>> Disable(string parameter)
        {
            throw new NotImplementedException();
        }

        public CommandReturnData<string> LiveData(string parameter)
        {
            throw new NotImplementedException();
        }

        public CommandReturnData<string> ScanDevices(string parameter)
        {
            throw new NotImplementedException();
        }

        public CommandReturnData<string> SentencedToSteady(string parameter)
        {
            throw new NotImplementedException();
        }

        public CommandReturnData<BalanceResult<Temperature>> SentencedToSteadyQuery()
        {
            throw new NotImplementedException();
        }

        public CommandReturnData<string> Zero()
        {
            throw new NotImplementedException();
        }
    }
}
