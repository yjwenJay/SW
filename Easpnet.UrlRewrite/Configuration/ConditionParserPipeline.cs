namespace Easpnet.UrlRewrite.Configuration
{
    using Easpnet.UrlRewrite;
    using Easpnet.UrlRewrite.Utilities;
    using System;
    using System.Collections;

    public class ConditionParserPipeline : CollectionBase
    {
        public void AddParser(IRewriteConditionParser parser)
        {
            base.InnerList.Add(parser);
        }

        public void AddParser(string parserType)
        {
            this.AddParser((IRewriteConditionParser) TypeHelper.Activate(parserType, null));
        }
    }
}
