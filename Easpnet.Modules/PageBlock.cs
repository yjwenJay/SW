using System.Collections.Generic;

namespace Easpnet.Modules
{
    /// <summary>
    /// 页面区块
    /// </summary>
    public class PageBlock
    {
        /// <summary>
        /// 唯一Id
        /// </summary>
        public int PageBlockId { get; set; }
        /// <summary>
        /// 区块id
        /// </summary>
        public long BlockId { get; set; }
        /// <summary>
        /// 区块名称
        /// </summary>
        public string BlockName { get; set; }
        /// <summary>
        /// 是否使用默认区块框架
        /// </summary>
        public bool UseDefaultFrame { get; set; }
        /// <summary>
        /// 区块标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 选项值集合
        /// </summary>
        public List<NameValue> Options { get; set; }
    }


    /// <summary>
    /// 页面区块集合
    /// </summary>
    public class PageBlockList : List<PageBlock>
    {
        
    }
}
