using CST.ICS.Gateway.Pressure.IDevice;
using Newtonsoft.Json;

namespace CST.ICS.Gateway.Model
{
    /// <summary>
    /// 设备单元（主要用于LiveData及指令执行时调用）
    /// </summary>
    public class DeviceUnit<T>
    {
        public IBaseDevice DeviceItem { get; set; }
        public T LiveData { get; set; }
        public GDZ9.Dto.ICS.Param.WorkStation Station { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(LiveData);
        }
    }
}
