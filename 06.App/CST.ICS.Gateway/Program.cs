using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using MQTTnet;
using MQTTnet.Client;
using Serilog;
using Serilog.Events;
using System.Reflection;
using System.Runtime.Loader;

namespace CST.ICS.Gateway
{
    public class Program
    {

        /// <summary>
        /// Serilog日志模板
        /// </summary>
        static string serilogDebug = System.Environment.CurrentDirectory + "\\Log\\Debug\\.log";
        static string serilogInfo = System.Environment.CurrentDirectory + "\\Log\\Info\\.log";
        static string serilogWarn = System.Environment.CurrentDirectory + "\\Log\\Warning\\.log";
        static string serilogError = System.Environment.CurrentDirectory + "\\Log\\Error\\.log";
        static string serilogFatal = System.Environment.CurrentDirectory + "\\Log\\Fatal\\.log";

        static string SerilogOutputTemplate = "{NewLine}时间:{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}日志等级:{Level}{NewLine}所在类:{SourceContext}{NewLine}日志信息:{Message}{NewLine}{Exception}";
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureServices((contex, services) =>
                {
                    services.Configure<Topic>(contex.Configuration.GetRequiredSection("Topic"));
                    
                })
                .ConfigureContainer<ContainerBuilder>((context, builder) =>
                {
                    Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName("CST.ICS.Gateway"));
                    builder.RegisterAssemblyTypes(assembly)
                    .Where(cc => cc.Name.EndsWith("Engine"))
                    .SingleInstance()
                    .AsImplementedInterfaces();
                    builder.RegisterType<PhysicalFileProvider>().SingleInstance().As<IFileProvider>().WithParameter(new TypedParameter(typeof(string), AppDomain.CurrentDomain.BaseDirectory)); ;

                    
                })
                .UseSerilog((context, logger) =>//注册Serilog
                {
                    logger.ReadFrom.Configuration(context.Configuration);
                    logger.Enrich.FromLogContext();
                    //logger.WriteTo.Console();  // 输出到Console控制台
                    //// 输出到配置的文件日志目录
                    //logger.WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Debug).WriteTo.Async(a => a.File(serilogDebug, rollingInterval: RollingInterval.Hour, outputTemplate: SerilogOutputTemplate)))
                    //    .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Information).WriteTo.Async(a => a.File(serilogInfo, rollingInterval: RollingInterval.Hour, outputTemplate: SerilogOutputTemplate)))
                    //    .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Warning).WriteTo.Async(a => a.File(serilogWarn, rollingInterval: RollingInterval.Hour, outputTemplate: SerilogOutputTemplate)))
                    //    .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Error).WriteTo.Async(a => a.File(serilogError, rollingInterval: RollingInterval.Hour, outputTemplate: SerilogOutputTemplate)))
                    //    .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Fatal).WriteTo.Async(a => a.File(serilogFatal, rollingInterval: RollingInterval.Hour, outputTemplate: SerilogOutputTemplate)));
                })
                .Build();

            host.Run();
        }
    }
}