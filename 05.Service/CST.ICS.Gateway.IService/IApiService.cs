using GDZ9.Dto.ICS.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST.ICS.Gateway.IService
{
    public interface IApiService
    {
        public bool IsGatewayInfoComplete { get; }
        /// <summary>
        /// 获取网关的基本信息
        /// </summary>
        /// <returns></returns>
        public Task GetGatewayInfo();
    }
}
