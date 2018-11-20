namespace Easpnet.UrlRewrite.Configuration
{
    using Easpnet.UrlRewrite;
    using Easpnet.UrlRewrite.Utilities;
    using System;
    using System.Collections;

    public class ActionParserFactory
    {
        private Hashtable _parsers = new Hashtable();

        public void AddParser(IRewriteActionParser parser)
        {
            ArrayList list;
            if (parser == null)
            {
                throw new ArgumentNullException("parser");
            }
            if (this._parsers.ContainsKey(parser.Name))
            {
                list = (ArrayList) this._parsers[parser.Name];
            }
            else
            {
                list = new ArrayList();
                this._parsers.Add(parser.Name, list);
            }
            list.Add(parser);
        }

        public void AddParser(string parserType)
        {
            this.AddParser((IRewriteActionParser) TypeHelper.Activate(parserType, null));
        }

        public IList GetParsers(string verb)
        {
            if (this._parsers.ContainsKey(verb))
            {
                return (ArrayList) this._parsers[verb];
            }
            return null;
        }
    }
}
