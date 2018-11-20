namespace Easpnet.UrlRewrite.Transforms
{
    using Easpnet.UrlRewrite;
    using System;
    using System.Collections.Specialized;

    public sealed class StaticMappingTransform : IRewriteTransform
    {
        private StringDictionary _map;
        private string _name;

        public StaticMappingTransform(string name, StringDictionary map)
        {
            this._name = name;
            this._map = map;
        }

        public string ApplyTransform(string input)
        {
            return this._map[input];
        }

        public string Name
        {
            get
            {
                return this._name;
            }
        }
    }
}
