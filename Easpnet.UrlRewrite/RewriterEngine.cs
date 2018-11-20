namespace Easpnet.UrlRewrite
{
    using Easpnet.UrlRewrite.Configuration;
    using Easpnet.UrlRewrite.Utilities;
    using System;
    using System.Collections;
    using System.IO;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Web;

    public class RewriterEngine
    {
        private RewriterConfiguration _configuration;
        private IContextFacade ContextFacade;
        private const string ContextOriginalQueryString = "UrlRewriter.NET.OriginalQueryString";
        private const string ContextQueryString = "UrlRewriter.NET.QueryString";
        private const string ContextRawUrl = "UrlRewriter.NET.RawUrl";

        public RewriterEngine(IContextFacade contextFacade, RewriterConfiguration configuration)
        {
            if (contextFacade == null)
            {
                throw new ArgumentNullException("contextFacade");
            }
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }
            this.ContextFacade = contextFacade;
            this._configuration = configuration;
        }

        private void AppendCookies(RewriteContext context)
        {
            for (int i = 0; i < context.Cookies.Count; i++)
            {
                HttpCookie cookie = context.Cookies[i];
                this.ContextFacade.AppendCookie(cookie);
            }
        }

        private void AppendHeaders(RewriteContext context)
        {
            foreach (string str in context.Headers)
            {
                this.ContextFacade.AppendHeader(str, context.Headers[str]);
            }
        }

        public string Expand(RewriteContext context, string input)
        {
            string str;
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }
            using (StringReader reader = new StringReader(input))
            {
                using (StringWriter writer = new StringWriter())
                {
                    for (char ch = (char) reader.Read(); ch != 0xffff; ch = (char) reader.Read())
                    {
                        if (ch == '$')
                        {
                            writer.Write(this.Reduce(context, reader));
                        }
                        else
                        {
                            writer.Write(ch);
                        }
                    }
                    str = writer.GetStringBuilder().ToString();
                }
            }
            return str;
        }

        private bool HandleDefaultDocument(RewriteContext context)
        {
            Uri uri = new Uri(this.ContextFacade.GetRequestUrl(), context.Location);
            UriBuilder builder = new UriBuilder(uri);
            builder.Path = builder.Path + "/";
            uri = builder.Uri;
            if (uri.Host == this.ContextFacade.GetRequestUrl().Host)
            {
                string path = this.ContextFacade.MapPath(uri.AbsolutePath);
                if (Directory.Exists(path))
                {
                    foreach (string str2 in RewriterConfiguration.Current.DefaultDocuments)
                    {
                        if (System.IO.File.Exists(Path.Combine(path, str2)))
                        {
                            context.Location = new Uri(uri, str2).AbsolutePath;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void HandleError(RewriteContext context)
        {
            this.ContextFacade.SetStatusCode((int) context.StatusCode);
            IRewriteErrorHandler handler = this._configuration.ErrorHandlers[(int) context.StatusCode] as IRewriteErrorHandler;
            if (handler != null)
            {
                try
                {
                    this._configuration.Logger.Debug(MessageProvider.FormatString(Message.CallingErrorHandler, new object[0]));
                    this.ContextFacade.HandleError(handler);
                    return;
                }
                catch (HttpException)
                {
                    throw;
                }
                catch (Exception exception)
                {
                    this._configuration.Logger.Fatal(exception.Message, exception);
                    throw new HttpException(500, HttpStatusCode.InternalServerError.ToString());
                }
            }
            throw new HttpException((int) context.StatusCode, context.StatusCode.ToString());
        }

        private void ProcessRules(RewriteContext context)
        {
            IList rules = this._configuration.Rules;
            int num = 0;
            for (int i = 0; i < rules.Count; i++)
            {
                IRewriteCondition condition = rules[i] as IRewriteCondition;
                if ((condition == null) || condition.IsMatch(context))
                {
                    IRewriteAction action = rules[i] as IRewriteAction;
                    switch (action.Execute(context))
                    {
                        case RewriteProcessing.StopProcessing:
                            this._configuration.Logger.Debug(MessageProvider.FormatString(Message.StoppingBecauseOfRule, new object[0]));
                            return;

                        case RewriteProcessing.RestartProcessing:
                            this._configuration.Logger.Debug(MessageProvider.FormatString(Message.RestartingBecauseOfRule, new object[0]));
                            i = 0;
                            if (++num > 10)
                            {
                                throw new InvalidOperationException(MessageProvider.FormatString(Message.TooManyRestarts, new object[0]));
                            }
                            break;
                    }
                }
            }
        }

        private string Reduce(RewriteContext context, StringReader reader)
        {
            char c = (char) reader.Read();
            if (char.IsDigit(c))
            {
                string str2 = c.ToString();
                if (char.IsDigit((char) reader.Peek()))
                {
                    c = (char) reader.Read();
                    str2 = str2 + c.ToString();
                }
                if (context.LastMatch != null)
                {
                    Group group = context.LastMatch.Groups[Convert.ToInt32(str2)];
                    if (group != null)
                    {
                        return group.Value;
                    }
                    return string.Empty;
                }
                return string.Empty;
            }
            if (c == '<')
            {
                string str3;
                using (StringWriter writer = new StringWriter())
                {
                    c = (char) reader.Read();
                    while ((c != '>') && (c != 0xffff))
                    {
                        if (c == '$')
                        {
                            writer.Write(this.Reduce(context, reader));
                        }
                        else
                        {
                            writer.Write(c);
                        }
                        c = (char) reader.Read();
                    }
                    str3 = writer.GetStringBuilder().ToString();
                }
                if (context.LastMatch != null)
                {
                    Group group2 = context.LastMatch.Groups[str3];
                    if (group2 != null)
                    {
                        return group2.Value;
                    }
                    return string.Empty;
                }
                return string.Empty;
            }
            if (c == '{')
            {
                string str4;
                bool flag = false;
                bool flag2 = false;
                using (StringWriter writer2 = new StringWriter())
                {
                    c = (char) reader.Read();
                    while ((c != '}') && (c != 0xffff))
                    {
                        switch (c)
                        {
                            case '$':
                                writer2.Write(this.Reduce(context, reader));
                                goto Label_017A;

                            case ':':
                                flag = true;
                                break;

                            case '(':
                                flag2 = true;
                                break;
                        }
                        writer2.Write(c);
                    Label_017A:
                        c = (char) reader.Read();
                    }
                    str4 = writer2.GetStringBuilder().ToString();
                }
                if (flag)
                {
                    Match match = Regex.Match(str4, @"^([^\:]+)\:([^\|]+)(\|(.+))?$");
                    string name = match.Groups[1].Value;
                    string input = match.Groups[2].Value;
                    string str7 = match.Groups[4].Value;
                    string str = this._configuration.TransformFactory.GetTransform(name).ApplyTransform(input);
                    if (str == null)
                    {
                        str = str7;
                    }
                    return str;
                }
                if (flag2)
                {
                    Match match2 = Regex.Match(str4, @"^([^\(]+)\((.+)\)$");
                    string str8 = match2.Groups[1].Value;
                    string str9 = match2.Groups[2].Value;
                    IRewriteTransform transform = this._configuration.TransformFactory.GetTransform(str8);
                    if (transform != null)
                    {
                        return transform.ApplyTransform(str9);
                    }
                    return str4;
                }
                return context.Properties[str4];
            }
            return c.ToString();
        }

        public string ResolveLocation(string location)
        {
            if (location == null)
            {
                throw new ArgumentNullException("location");
            }
            string applicationPath = this.ContextFacade.GetApplicationPath();
            if (applicationPath.Length > 1)
            {
                applicationPath = applicationPath + "/";
            }
            return location.Replace("~/", applicationPath);
        }

        public void Rewrite()
        {
            string rawUrl = this.ContextFacade.GetRawUrl().Replace("+", " ");
            this.RawUrl = rawUrl;

            //HttpContext.Current.Response.Write(rawUrl);
            //HttpContext.Current.Response.End();

            RewriteContext context = new RewriteContext(this, rawUrl, this.ContextFacade.GetHttpMethod(), this.ContextFacade.MapPath, this.ContextFacade.GetServerVariables(), this.ContextFacade.GetHeaders(), this.ContextFacade.GetCookies());
            this.ProcessRules(context);
            this.AppendHeaders(context);
            this.AppendCookies(context);
            this.ContextFacade.SetStatusCode((int) context.StatusCode);
            if ((context.Location != rawUrl) && (context.StatusCode < HttpStatusCode.BadRequest))
            {
                if (context.StatusCode < HttpStatusCode.MultipleChoices)
                {
                    this._configuration.Logger.Info(MessageProvider.FormatString(Message.RewritingXtoY, new object[] { this.ContextFacade.GetRawUrl(), context.Location }));
                    this.HandleDefaultDocument(context);
                    this.ContextFacade.RewritePath(context.Location);
                }
                else
                {
                    this._configuration.Logger.Info(MessageProvider.FormatString(Message.RedirectingXtoY, new object[] { this.ContextFacade.GetRawUrl(), context.Location }));
                    this.ContextFacade.SetRedirectLocation(context.Location);
                }
            }
            else if (context.StatusCode >= HttpStatusCode.BadRequest)
            {
                this.HandleError(context);
            }
            else if (this.HandleDefaultDocument(context))
            {
                this.ContextFacade.RewritePath(context.Location);
            }
            this.SetContextItems(context);
        }

        private void SetContextItems(RewriteContext context)
        {
            this.OriginalQueryString = new Uri(this.ContextFacade.GetRequestUrl(), this.ContextFacade.GetRawUrl()).Query.Replace("?", "");
            this.QueryString = new Uri(this.ContextFacade.GetRequestUrl(), context.Location).Query.Replace("?", "");
            foreach (string str in context.Properties.Keys)
            {
                this.ContextFacade.SetItem(string.Format("Rewriter.{0}", str), context.Properties[str]);
            }
        }

        private void VerifyResultExists(RewriteContext context)
        {
            if ((string.Compare(context.Location, this.ContextFacade.GetRawUrl()) != 0) && (context.StatusCode < HttpStatusCode.MultipleChoices))
            {
                Uri uri = new Uri(this.ContextFacade.GetRequestUrl(), context.Location);
                if (uri.Host == this.ContextFacade.GetRequestUrl().Host)
                {
                    string path = this.ContextFacade.MapPath(uri.AbsolutePath);
                    if (!System.IO.File.Exists(path))
                    {
                        this._configuration.Logger.Debug(MessageProvider.FormatString(Message.ResultNotFound, new object[] { path }));
                        context.StatusCode = HttpStatusCode.NotFound;
                    }
                    else
                    {
                        this.HandleDefaultDocument(context);
                    }
                }
            }
        }

        public string OriginalQueryString
        {
            get
            {
                return (string) this.ContextFacade.GetItem("UrlRewriter.NET.OriginalQueryString");
            }
            set
            {
                this.ContextFacade.SetItem("UrlRewriter.NET.OriginalQueryString", value);
            }
        }

        public string QueryString
        {
            get
            {
                return (string) this.ContextFacade.GetItem("UrlRewriter.NET.QueryString");
            }
            set
            {
                this.ContextFacade.SetItem("UrlRewriter.NET.QueryString", value);
            }
        }

        public string RawUrl
        {
            get
            {
                return (string) this.ContextFacade.GetItem("UrlRewriter.NET.RawUrl");
            }
            set
            {
                this.ContextFacade.SetItem("UrlRewriter.NET.RawUrl", value);
            }
        }
    }
}
