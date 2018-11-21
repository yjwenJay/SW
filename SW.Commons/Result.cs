namespace SW
{
    /// <summary>
    /// 操作结果类，用来返回操作的结果，以及返回消息和相关数据
    /// </summary>
    public struct Result
    {
        /// <summary>
        /// 操作是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 相关的int数据，如如果是插入数据，则可以返回刚插入的Id
        /// </summary>
        public int IntData { get; set; }

        /// <summary>
        /// 相关的Guid数据
        /// </summary>
        public System.Guid GuidData { get; set; }

        /// <summary>
        /// 对象数据
        /// </summary>
        public object ObjectData { get; set; }


        /// <summary>
        /// 根据指定的参数创建实例
        /// </summary>
        /// <param name="success"></param>
        /// <param name="message"></param>
        public Result(bool success, string message)
        {
            this = new Result();
            this.Success = success;
            this.Message = message;
        }
    }
}
