using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST.ICS.Gateway.Model
{

    /// <summary>
    /// 主题集合
    /// </summary>
    public class TopicSet
    {
        /// <summary>
        /// 订阅主题
        /// </summary>
        public SubAndPubTopic UUTTopic { get; set; } = default!;

        /// <summary>
        /// 发布主题
        /// </summary>
        public SubAndPubTopic ENVTopic { get; set; } = default!;
    }

    /// <summary>
    /// 发布订阅主题列表
    /// </summary>
    public class SubAndPubTopic
    {
        /// <summary>
        /// 订阅主题
        /// </summary>
        public List<Topic> SubTopics { get; set; } = default!;

        /// <summary>
        /// 发布主题
        /// </summary>
        public List<Topic> PubTopics { get; set; } = default!;
    }

    /// <summary>
    /// 主题对象
    /// </summary>
    public class Topic
    {
        /// <summary>
        /// 主题分类
        /// </summary>
        public string TopicClass { get; set; } = default!;

        /// <summary>
        /// 主题内容
        /// </summary>
        public string TopicValue { get; set; } = default!;
    }

}
