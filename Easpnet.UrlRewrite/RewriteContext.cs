namespace Easpnet.UrlRewrite
{
    using Easpnet.UrlRewrite.Utilities;
    using System;
    using System.Collections.Specialized;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Web;

    public sealed class RewriteContext
    {
        private HttpCookieCollection _cookies = new HttpCookieCollection();
        private RewriterEngine _engine;
        private NameValueCollection _headers = new NameValueCollection();
        private Match _lastMatch;
        private string _location;
        private Easpnet.UrlRewrite.Utilities.MapPath _mapPath;
        private string _method = string.Empty;
        private NameValueCollection _properties = new NameValueCollection();
        private HttpStatusCode _statusCode = HttpStatusCode.OK;

        internal RewriteContext(RewriterEngine engine, string rawUrl, string httpMethod, Easpnet.UrlRewrite.Utilities.MapPath mapPath, NameValueCollection serverVariables, NameValueCollection headers, HttpCookieCollection cookies)
        {
            this._engine = engine;
            this._location = rawUrl;
            this._method = httpMethod;
            this._mapPath = mapPath;
            foreach (string str in serverVariables)
            {
                this._properties.Add(str, serverVariables[str]);
            }
            foreach (string str2 in headers)
            {
                this._properties.Add(str2, headers[str2]);
            }
            foreach (string str3 in cookies)
            {
                this._properties.Add(str3, cookies[str3].Value);
            }
        }

        public string Expand(string input)
        {
            return this._engine.Expand(this, input);
        }

        public string MapPath(string url)
        {
            return this._mapPath(url);
        }

        public string ResolveLocation(string location)
        {
            return this._engine.ResolveLocation(location);
        }

        public HttpCookieCollection Cookies
        {
            get
            {
                return this._cookies;
            }
        }

        public NameValueCollection Headers
        {
            get
            {
                return this._headers;
            }
        }

        public Match LastMatch
        {
            get
            {
                return this._lastMatch;
            }
            set
            {
                this._lastMatch = value;
            }
        }

        public string Location
        {
            get
            {
                return this._location;
            }
            set
            {
                this._location = value;
            }
        }

        public string Method
        {
            get
            {
                return this._method;
            }
        }

        public NameValueCollection Properties
        {
            get
            {
                return this._properties;
            }
        }

        public HttpStatusCode StatusCode
        {
            get
            {
                return this._statusCode;
            }
            set
            {
                this._statusCode = value;
            }
        }
    }
}
