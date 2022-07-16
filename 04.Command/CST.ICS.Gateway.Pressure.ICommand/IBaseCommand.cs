using GDZ9.Model.ICS.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pressure1 = Xmas11.Domain.Mechanics.Pressure;
namespace CST.ICS.Gateway.Pressure.Command
{
    public interface IBaseCommand<T>
    {

        /// <summary>
        /// 切换数据上报开关
        /// </summary>
        /// <param name="parameter">true/false</param>
        /// <returns></returns>
        public CommandReturnData<string> LiveData(string parameter);

        /// <summary>
        /// 切换设备扫描开关
        /// </summary>
        /// <param name="parameter">true/false</param>
        /// <returns></returns>
        public CommandReturnData<string> ScanDevices(string parameter);


        /// <summary>
        /// 禁用设备
        /// </summary>
        /// <returns></returns>
        public CommandReturnData<Dictionary<string, bool>> Disable(string parameter);


        /// <summary>
        /// 清零
        /// </summary>
        /// <returns></returns>
        public CommandReturnData<string> Zero();

        /// <summary>
        /// 查询当前所有设备压力/温度值
        /// </summary>
        /// <returns></returns>
        public CommandReturnData<Dictionary<string, T>> CurrentValueQuery();

        #region 判稳指令
        /// <summary>
        /// 开始判稳
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public CommandReturnData<string> SentencedToSteady(string parameter);

        /// <summary>
        /// 读判稳结果
        /// </summary>
        /// <returns></returns>
        public CommandReturnData<BalanceResult<T>> SentencedToSteadyQuery();
        #endregion

    }
}
