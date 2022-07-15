namespace CST.ICS.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddHostedService<ScanDeviceEngine>();
                })
                .Build();

            host.Run();
        }
    }
}