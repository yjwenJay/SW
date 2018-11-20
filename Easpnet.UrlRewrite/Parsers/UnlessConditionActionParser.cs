namespace Easpnet.UrlRewrite.Parsers
{
    using System;

    public class UnlessConditionActionParser : IfConditionActionParser
    {
        public override string Name
        {
            get
            {
                return "unless";
            }
        }
    }
}
