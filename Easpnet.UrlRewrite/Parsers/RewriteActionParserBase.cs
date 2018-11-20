namespace Easpnet.UrlRewrite.Parsers
{
    using Easpnet.UrlRewrite;
    using Easpnet.UrlRewrite.Conditions;
    using Easpnet.UrlRewrite.Configuration;
    using System;
    using System.Collections;
    using System.Xml;

    public abstract class RewriteActionParserBase : IRewriteActionParser
    {
        protected RewriteActionParserBase()
        {
        }

        public abstract IRewriteAction Parse(XmlNode node, object config);
        protected void ParseConditions(XmlNode node, IList conditions, bool negative, object config)
        {
            if (config != null)
            {
                if (node == null)
                {
                    throw new ArgumentNullException("node");
                }
                if (conditions == null)
                {
                    throw new ArgumentNullException("conditions");
                }
                RewriterConfiguration configuration = config as RewriterConfiguration;
                foreach (IRewriteConditionParser parser in configuration.ConditionParserPipeline)
                {
                    IRewriteCondition chainedCondition = parser.Parse(node);
                    if (chainedCondition != null)
                    {
                        if (negative)
                        {
                            chainedCondition = new NegativeCondition(chainedCondition);
                        }
                        conditions.Add(chainedCondition);
                    }
                }
                XmlNode firstChild = node.FirstChild;
                while (firstChild != null)
                {
                    if ((firstChild.NodeType == XmlNodeType.Element) && (firstChild.LocalName == "and"))
                    {
                        this.ParseConditions(firstChild, conditions, negative, config);
                        XmlNode nextSibling = firstChild.NextSibling;
                        node.RemoveChild(firstChild);
                        firstChild = nextSibling;
                    }
                    else
                    {
                        firstChild = firstChild.NextSibling;
                    }
                }
            }
        }

        public abstract bool AllowsAttributes { get; }

        public abstract bool AllowsNestedActions { get; }

        public abstract string Name { get; }
    }
}
