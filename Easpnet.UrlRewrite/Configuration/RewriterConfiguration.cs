namespace Easpnet.UrlRewrite.Configuration
{
    using Easpnet.UrlRewrite.Logging;
    using Easpnet.UrlRewrite.Parsers;
    using Easpnet.UrlRewrite.Transforms;
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.IO;
    using System.Web;
    using System.Web.Caching;
    using System.Xml;
    using Easpnet.UrlRewrite.Utilities;
    using System.Reflection;
    using System.Collections.Generic;
    using Easpnet.Modules;
    using System.Data;

    public class RewriterConfiguration
    {
        private Easpnet.UrlRewrite.Configuration.ActionParserFactory _actionParserFactory = new Easpnet.UrlRewrite.Configuration.ActionParserFactory();
        private static string _cacheName = typeof(RewriterConfiguration).AssemblyQualifiedName;
        private Easpnet.UrlRewrite.Configuration.ConditionParserPipeline _conditionParserPipeline;
        private StringCollection _defaultDocuments;
        private Hashtable _errorHandlers = new Hashtable();
        private IRewriteLogger _logger = new NullLogger();
        private ArrayList _rules = new ArrayList();
        private Easpnet.UrlRewrite.Configuration.TransformFactory _transformFactory;
        private string _xPoweredBy = MessageProvider.FormatString(Message.ProductName, new object[] { Assembly.GetExecutingAssembly().GetName().Version.ToString(3) });
        private static object SyncObject = new object();
        private static Modules.Models.Module module = new Easpnet.Modules.Models.Module();

        internal RewriterConfiguration()
        {
            this._actionParserFactory.AddParser(new IfConditionActionParser());
            this._actionParserFactory.AddParser(new UnlessConditionActionParser());
            this._actionParserFactory.AddParser(new AddHeaderActionParser());
            this._actionParserFactory.AddParser(new SetCookieActionParser());
            this._actionParserFactory.AddParser(new SetPropertyActionParser());
            this._actionParserFactory.AddParser(new RewriteActionParser());
            this._actionParserFactory.AddParser(new RedirectActionParser());
            this._actionParserFactory.AddParser(new SetStatusActionParser());
            this._actionParserFactory.AddParser(new ForbiddenActionParser());
            this._actionParserFactory.AddParser(new GoneActionParser());
            this._actionParserFactory.AddParser(new NotAllowedActionParser());
            this._actionParserFactory.AddParser(new NotFoundActionParser());
            this._actionParserFactory.AddParser(new NotImplementedActionParser());
            this._conditionParserPipeline = new Easpnet.UrlRewrite.Configuration.ConditionParserPipeline();
            this._conditionParserPipeline.AddParser(new AddressConditionParser());
            this._conditionParserPipeline.AddParser(new HeaderMatchConditionParser());
            this._conditionParserPipeline.AddParser(new MethodConditionParser());
            this._conditionParserPipeline.AddParser(new PropertyMatchConditionParser());
            this._conditionParserPipeline.AddParser(new ExistsConditionParser());
            this._conditionParserPipeline.AddParser(new UrlMatchConditionParser());
            this._transformFactory = new Easpnet.UrlRewrite.Configuration.TransformFactory();
            this._transformFactory.AddTransform(new DecodeTransform());
            this._transformFactory.AddTransform(new EncodeTransform());
            this._transformFactory.AddTransform(new LowerTransform());
            this._transformFactory.AddTransform(new UpperTransform());
            this._transformFactory.AddTransform(new Base64Transform());
            this._transformFactory.AddTransform(new Base64DecodeTransform());
            this._defaultDocuments = new StringCollection();
        }

        public static RewriterConfiguration Create()
        {
            return new RewriterConfiguration();
        }

        public static RewriterConfiguration Load()
        {
            XmlNode section = ConfigurationManager.GetSection("rewriter") as XmlNode;
            RewriterConfiguration configuration = null;
            XmlNode namedItem = section.Attributes.GetNamedItem("file");
            if (namedItem != null)
            {
                string filename = HttpContext.Current.Server.MapPath(namedItem.Value);
                ArrayList dependencyFiles;
                configuration = LoadFromFile(filename, out dependencyFiles);
                if (configuration != null)
                {
                    Array arr = dependencyFiles.ToArray();
                    string[] str = new string[arr.Length];
                    for (int i = 0; i < arr.Length; i++)
                    {
                        str[i] = arr.GetValue(i).ToString();
                    }

                    CacheDependency dependencies = new CacheDependency(str);
                    HttpRuntime.Cache.Add(_cacheName, configuration, dependencies, DateTime.Now.AddHours(1.0), TimeSpan.Zero, CacheItemPriority.Normal, null);
                }
            }
            if (configuration == null)
            {
                configuration = LoadFromNode(section);
                HttpRuntime.Cache.Add(_cacheName, configuration, null, DateTime.Now.AddHours(1.0), TimeSpan.Zero, CacheItemPriority.Normal, null);
            }
            return configuration;
        }

        public static RewriterConfiguration LoadFromFile(string filename, out ArrayList dependencyFiles)
        {
            dependencyFiles = new ArrayList();
            
            if (File.Exists(filename))
            {
                dependencyFiles.Add(filename);
                //
                XmlDocument document = new XmlDocument();
                document.Load(filename);

                string path = "";
                //
                List<Modules.Models.Module> lst_modules = module.GetAllModules();
                foreach (Modules.Models.Module md in lst_modules)
                {
                    if (string.IsNullOrEmpty(md.UrlRewriteFile))
                    {
                        path = "~/UrlRewrite/Modules/" + md.ModuleName + ".xml";
                    }
                    else
                    {
                        path = "~/UrlRewrite/Modules/" + md.UrlRewriteFile;
                    }

                    path = HttpContext.Current.Server.MapPath(path);
                    //
                    if (File.Exists(path))
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(path);
                        foreach (XmlNode n in doc.DocumentElement.ChildNodes)
                        {
                            XmlNode n1 = document.ImportNode(n, true);
                            document.DocumentElement.AppendChild(n1);
                        }                
        
                        //
                        dependencyFiles.Add(path);
                    }
                }

                //
                path = HttpContext.Current.Server.MapPath("~/UrlRewrite/CommonUrlRewrite.xml");
                //
                if (File.Exists(path))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(path);
                    foreach (XmlNode n in doc.DocumentElement.ChildNodes)
                    {
                        XmlNode n1 = document.ImportNode(n, true);
                        document.DocumentElement.AppendChild(n1);
                    }

                    //
                    dependencyFiles.Add(path);
                }

                return LoadFromNode(document.DocumentElement);
            }
            return null;
        }

        public static RewriterConfiguration LoadFromNode(XmlNode node)
        {
            return (RewriterConfiguration) RewriterConfigurationReader.Read(node);
        }

        public Easpnet.UrlRewrite.Configuration.ActionParserFactory ActionParserFactory
        {
            get
            {
                return this._actionParserFactory;
            }
        }

        public Easpnet.UrlRewrite.Configuration.ConditionParserPipeline ConditionParserPipeline
        {
            get
            {
                return this._conditionParserPipeline;
            }
        }

        public static RewriterConfiguration Current
        {
            get
            {
                RewriterConfiguration configuration = HttpRuntime.Cache.Get(_cacheName) as RewriterConfiguration;
                if (configuration == null)
                {
                    lock (SyncObject)
                    {
                        configuration = HttpRuntime.Cache.Get(_cacheName) as RewriterConfiguration;
                        if (configuration == null)
                        {
                            configuration = Load();
                        }
                    }
                }
                return configuration;
            }
        }

        public StringCollection DefaultDocuments
        {
            get
            {
                return this._defaultDocuments;
            }
        }

        public IDictionary ErrorHandlers
        {
            get
            {
                return this._errorHandlers;
            }
        }

        public IRewriteLogger Logger
        {
            get
            {
                return this._logger;
            }
            set
            {
                this._logger = value;
            }
        }

        public IList Rules
        {
            get
            {
                return this._rules;
            }
        }

        public Easpnet.UrlRewrite.Configuration.TransformFactory TransformFactory
        {
            get
            {
                return this._transformFactory;
            }
        }

        internal string XPoweredBy
        {
            get
            {
                return this._xPoweredBy;
            }
        }
    }
}
