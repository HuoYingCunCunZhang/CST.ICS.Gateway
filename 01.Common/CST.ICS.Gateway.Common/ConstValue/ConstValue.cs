using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST.ICS.Gateway.Common.ConstValue
{
    public class ConstValue
    {
        /// <summary>
        /// Serilog日志模板
        /// </summary>
        public static string serilogDebug = System.Environment.CurrentDirectory + "\\Log\\Debug\\.log";
        public static string serilogInfo = System.Environment.CurrentDirectory + "\\Log\\Info\\.log";
        public static string serilogWarn = System.Environment.CurrentDirectory + "\\Log\\Warning\\.log";
        public static string serilogError = System.Environment.CurrentDirectory + "\\Log\\Error\\.log";
        public static string serilogFatal = System.Environment.CurrentDirectory + "\\Log\\Fatal\\.log";
        public static string SerilogOutputTemplate = "{NewLine}时间:{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}日志等级:{Level}{NewLine}所在类:{SourceContext}{NewLine}日志信息:{Message}{NewLine}{Exception}";


        //logger.WriteTo.Console();  // 输出到Console控制台
        //// 输出到配置的文件日志目录
        //logger.WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Debug).WriteTo.Async(a => a.File(serilogDebug, rollingInterval: RollingInterval.Hour, outputTemplate: SerilogOutputTemplate)))
        //    .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Information).WriteTo.Async(a => a.File(serilogInfo, rollingInterval: RollingInterval.Hour, outputTemplate: SerilogOutputTemplate)))
        //    .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Warning).WriteTo.Async(a => a.File(serilogWarn, rollingInterval: RollingInterval.Hour, outputTemplate: SerilogOutputTemplate)))
        //    .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Error).WriteTo.Async(a => a.File(serilogError, rollingInterval: RollingInterval.Hour, outputTemplate: SerilogOutputTemplate)))
        //    .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Fatal).WriteTo.Async(a => a.File(serilogFatal, rollingInterval: RollingInterval.Hour, outputTemplate: SerilogOutputTemplate)));
    }
}
