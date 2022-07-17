namespace CST.ICS.Gateway.IDevice
{
    public interface IPressureSourceDevice
    {
        public string DeviceName { get; set; }
        public bool SetParam(string param);
    }
}