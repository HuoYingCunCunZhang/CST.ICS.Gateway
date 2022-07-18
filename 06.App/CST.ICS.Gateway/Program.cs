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
                    
                    builder.RegisterModule(new AutofacModule());

                })
                .UseSerilog((context, logger) =>//×¢²áSerilog
                {
                    logger.ReadFrom.Configuration(context.Configuration);
                    logger.Enrich.FromLogContext();
                })
                .Build();

            host.Run();
        }
    }
}