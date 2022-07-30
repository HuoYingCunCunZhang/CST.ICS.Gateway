using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST.ICS.Gateway.Common
{
    public class ConstValue
    {
        #region SerilLog模板相关
        public static string serilogDebug = System.Environment.CurrentDirectory + "\\Log\\Debug\\.log";
        public static string serilogInfo = System.Environment.CurrentDirectory + "\\Log\\Info\\.log";
        public static string serilogWarn = System.Environment.CurrentDirectory + "\\Log\\Warning\\.log";
        public static string serilogError = System.Environment.CurrentDirectory + "\\Log\\Error\\.log";
        public static string serilogFatal = System.Environment.CurrentDirectory + "\\Log\\Fatal\\.log";
        public static string SerilogOutputTemplate = "{NewLine}时间:{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}日志等级:{Level}{NewLine}所在类:{SourceContext}{NewLine}日志信息:{Message}{NewLine}{Exception}";
        #endregion


        //logger.WriteTo.Console();  // 输出到Console控制台
        //// 输出到配置的文件日志目录
        //logger.WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Debug).WriteTo.Async(a => a.File(serilogDebug, rollingInterval: RollingInterval.Hour, outputTemplate: SerilogOutputTemplate)))
        //    .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Information).WriteTo.Async(a => a.File(serilogInfo, rollingInterval: RollingInterval.Hour, outputTemplate: SerilogOutputTemplate)))
        //    .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Warning).WriteTo.Async(a => a.File(serilogWarn, rollingInterval: RollingInterval.Hour, outputTemplate: SerilogOutputTemplate)))
        //    .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Error).WriteTo.Async(a => a.File(serilogError, rollingInterval: RollingInterval.Hour, outputTemplate: SerilogOutputTemplate)))
        //    .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Fatal).WriteTo.Async(a => a.File(serilogFatal, rollingInterval: RollingInterval.Hour, outputTemplate: SerilogOutputTemplate)));


        #region 静态字符串常量
        /// <summary>
        /// 公共程序集前缀
        /// </summary>
        public const string AssemblyPrefix = "CST.ICS";

        /// <summary>
        /// APP启动程序级后缀
        /// </summary>
        public const string AssemblySuffixOfApp = ".Gateway";

        /// <summary>
        /// 服务层启动程序级后缀
        /// </summary>
        public const string AssemblySuffixOfService = ".Service";

        /// <summary>
        /// 命令层启动程序级后缀
        /// </summary>
        public const string AssemblySuffixOfCommand = ".Command";

        /// <summary>
        /// 命令层启动程序级后缀
        /// </summary>
        public const string AssemblySuffixOfDevice = ".Device";

        /// <summary>
        /// 引擎类名后缀
        /// </summary>
        public const string ClassSuffixOfEngine = "Engine";

        /// <summary>
        /// 服务类名后缀
        /// </summary>
        public const string ClassSuffixOfService = "Service";

        /// <summary>
        /// 设备类名后缀
        /// </summary>
        public const string ClassSuffixOfCommand = "Command";

        /// <summary>
        /// 设备类名后缀
        /// </summary>
        public const string ClassSuffixOfDevice = "Device";
        #endregion
    }
}
