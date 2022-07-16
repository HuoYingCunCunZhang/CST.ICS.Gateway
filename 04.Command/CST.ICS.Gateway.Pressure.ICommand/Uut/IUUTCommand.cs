using GDZ9.Model.ICS.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pressure1 = Xmas11.Domain.Mechanics.Pressure;

namespace CST.ICS.Gateway.Pressure.Command
{
    /// <summary>
    /// 被检设备指令
    /// </summary>
    public interface IUUTCommand:IBaseCommand<Pressure1>
    {
        #region 基础指令
        
        /// <summary>
        /// 设置工作模式
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public CommandReturnData<string> WorkMode(string parameter);

        /// <summary>
        /// 设置回差修正系数
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public CommandReturnData<string> OffSet(string parameter);
        #endregion


        #region AD任务相关指令
        /// <summary>
        /// 设置AD放大倍数
        /// </summary>
        /// <param name="parameter">放大倍数</param>
        /// <returns></returns>
        public CommandReturnData<string> AD(string parameter);

        /// <summary>
        /// 读AD放大倍数结果
        /// </summary>
        /// <returns></returns>
        public CommandReturnData<ADResult> ADQuery();

        #endregion

        #region RS任务相关指令
        /// <summary>
        /// 量程标定
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public CommandReturnData<string> RS(string parameter);

        /// <summary>
        /// 读取量程标定结果
        /// </summary>
        /// <returns></returns>
        public CommandReturnData<RSResult> RSQuery();
        #endregion

        #region 数据下载业务相关指令
        /// <summary>
        /// 数据下载
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public CommandReturnData<string> ResumeCalData(string parameter);

        /// <summary>
        /// 读下载状态
        /// </summary>
        /// <returns></returns>
        public CommandReturnData<ResumeCalDataResult> ResumeCalDataQuery();
        #endregion


    }
}
