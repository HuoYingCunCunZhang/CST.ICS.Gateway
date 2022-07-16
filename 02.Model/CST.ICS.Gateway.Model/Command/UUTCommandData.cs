using GDZ9.Model.ICS.Business;
using Press = Xmas11.Domain.Mechanics.Pressure;

namespace CST.ICS.Gateway.Model
{
    public static class UUTCommandData
    {
        public static bool IsScan = false;
        public static bool ScanFinish = false;
        public static bool IsLiveDataOpen = false;

        public static Dictionary<string, CommandRunState> ADState = new Dictionary<string, CommandRunState>();
        public static Dictionary<string, CommandRunState> RSState = new Dictionary<string, CommandRunState>();
        public static Dictionary<string, CommandRunState> ResumeCalState = new Dictionary<string, CommandRunState>();
        public static Dictionary<string, CommandRunState> StableState = new Dictionary<string, CommandRunState>();
        public static Dictionary<string , int> ADResultPairs = new Dictionary<string , int>();    
        public static Dictionary<string , double> RSResultPairs = new Dictionary<string , double>();    
        public static Dictionary<string , string> ResumeCalResultPairs = new Dictionary<string , string>();    
        public static Dictionary<string , BanlanceData<Press>> StableResultPairs = new Dictionary<string , BanlanceData<Press>>();    
    }
}
