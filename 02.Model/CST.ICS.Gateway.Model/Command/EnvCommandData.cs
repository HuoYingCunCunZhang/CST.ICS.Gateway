using GDZ9.Model.ICS.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xmas11.Domain.Mechanics;
using Xmas11.Domain.Thermology;
using Press = Xmas11.Domain.Mechanics.Pressure;

namespace CST.ICS.Gateway.Model
{
    public static class EnvCommandData
    {
        public static bool IsPressureControlScan = false;
        public static bool IsPressureSTDScan = false;
        public static bool IsTemperatureControlScan = false;
        public static bool IsTemperatureSTDScan = false;

        public static bool PressureControlScanFinish = false;
        public static bool PressureSTDScanFinish = false;
        public static bool TemperatureControlScanFinish = false;
        public static bool TemperatureSTDScanFinish = false;

        public static bool IsPressureControlLiveDataOpen = false;
        public static bool IsPressureSTDLiveDataOpen = false;
        public static bool IsTemperatureControlLiveDataOpen = false;
        public static bool IsTemperatureSTDLiveDataOpen = false;


        public static Dictionary<string, CommandRunState> PressureStableState = new Dictionary<string, CommandRunState>();
        public static Dictionary<string, BanlanceData<Press>> PressureStableResultPairs = new Dictionary<string, BanlanceData<Press>>();

        public static Dictionary<string, CommandRunState> TemperatureStableState = new Dictionary<string, CommandRunState>();
        public static Dictionary<string, BanlanceData<List<Temperature>>> TemperatureStableResultPairs = new Dictionary<string, BanlanceData<List<Temperature>>>();
    }
}
