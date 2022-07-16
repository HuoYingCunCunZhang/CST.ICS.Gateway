using GDZ9.Dto.ICS.Param;
using GDZ9.Model.ICS.Common;
using Xmas11.Comm.Core;
using Xmas11.Comm.Devices;

namespace CST.ICS.Gateway.Pressure.IDevice
{
    /// <summary>
    /// 设备基类接口
    /// </summary>
    public interface IBaseDevice
    {
        #region 属性
        /// <summary>
        /// 设备型号
        /// </summary>
        string DeviceMode { get; set; }
        /// <summary>
        /// 设备角色
        /// </summary>
        List<DeviceRole> Role { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        int SequenceNumber { get; set; }
        /// <summary>
        /// 标识设备在线状态
        /// </summary>
        bool IsOnline { get; set; }
        /// <summary>
        /// 标识设备的启用状态
        /// </summary>
        bool IsEnable { get; set; }
        /// <summary>
        /// 标识设备是否正被扫描
        /// </summary>
        bool IsNotScaning { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        string SerialNumber { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        string DeviceType { get; set; }

        /// <summary>
        /// 设备固件版本
        /// </summary>
        string DeviceVersion { get; set; }

        /// <summary>
        /// 通讯实例
        /// </summary>
        BaseDevice CommInstance { get; set; }

        /// <summary>
        /// 通讯配置
        /// </summary>
        CommSettings CommSettingInstance { get; set; }

        /// <summary>
        /// 管理端通讯配置
        /// </summary>
        CommSetting CommSetting { get; set; }
        #endregion

        #region 方法

        /// <summary>
        /// 设备初始化
        /// </summary>
        void InitDevice();
        /// <summary>
        /// 获取设备基本信息
        /// </summary>
        void GetBaseInfo();
        /// <summary>
        /// 打开通讯
        /// </summary>
        /// <returns></returns>
        bool Open();

        /// <summary>
        /// 关闭通讯
        /// </summary>
        /// <returns></returns>
        bool Close();

        /// <summary>
        /// 获取序列号
        /// </summary>
        /// <returns></returns>
        string GetSerialNumber();

        /// <summary>
        /// 获取设备名称
        /// </summary>
        /// <returns></returns>
        string GetName();

        /// <summary>
        /// 设备是否存在
        /// </summary>
        /// <returns></returns>
        bool IsExist();

        /// <summary>
        /// 获取设备固件版本
        /// </summary>
        /// <returns></returns>
        string GetVersion();

        /// <summary>
        /// 获取设备类型
        /// </summary>
        /// <returns></returns>
        string GetDeviceType();

        #endregion
    }
}