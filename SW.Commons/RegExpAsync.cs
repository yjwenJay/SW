using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace SW
{
    /// <summary>
    /// 异步正则 4.5有matchTimeout属性
    /// new Regex(@"\d",RegexOptions.Compiled,TimeSpan.FromSeconds(1));
    /// </summary>
    public class RegExpAsync
    {
        //RegExpAsync rea = new RegExpAsync();
        //mat = rea.Matches(reg, str);

        private MatchCollection mc = null;
        private int _timeout = 1000;        // 最长休眠时间(超时),毫秒,默认1秒
        public RegExpAsync()
        { }

        public RegExpAsync(int timeout)
        {
            this._timeout = timeout;
            this.mc = null;
        }

        public MatchCollection Matches(Regex regex, string input)
        {
            RegMatch r = new RegMatch(regex, input);
            r.OnMatchComplete += new RegMatch.MatchCompleteHandler(this.MatchCompleteHandler);
            Thread t = new Thread(new ThreadStart(r.Matchs));
            t.Start();

            while (_timeout > 0 && t != null && t.IsAlive)
            {
                Thread.Sleep(100);
                _timeout -= 100;
            }

            try
            {
                if (t != null)
                    t.Abort();
            }
            catch { }
            t = null;

            return mc;
        }

        private void MatchCompleteHandler(MatchCollection mc)
        {
            this.mc = mc;
        }

        private class RegMatch
        {
            internal delegate void MatchCompleteHandler(MatchCollection mc);
            internal event MatchCompleteHandler OnMatchComplete;
            private string _input;
            private Regex _regex;

            public RegMatch(Regex regex, string input)
            {
                this._regex = regex;
                this._input = input;
            }
            public string Input
            {
                get { return this._input; }
                set { this._input = value; }
            }
            public Regex Regex
            {
                get { return this._regex; }
                set { this._regex = value; }
            }

            internal void Matchs()
            {
                MatchCollection mc = this._regex.Matches(this._input);
                if (mc != null && mc.Count > 0)    // 这里有可能造成cpu资源耗尽
                    OnMatchComplete(mc);
                else
                    OnMatchComplete(null);
            }
        }


        //private int sleepCounter;
        //private int sleepInterval;    // 休眠间隔,毫秒
        //private bool _isTimeout;
        //public bool IsTimeout
        //{
        //    get { return this._isTimeout; }
        //}
        //public RegExpAsync(int timeout)
        //{
        //    this._timeout = timeout;
        //    this.sleepCounter = 0;
        //    this.sleepInterval = 100;
        //    this._isTimeout = false;
        //    this.mc = null;
        //}
        //private void Sleep(Thread t)
        //{
        //    if (t != null && t.IsAlive)
        //    {
        //        Thread.Sleep(TimeSpan.FromMilliseconds(this.sleepInterval));
        //        this.sleepCounter++;
        //        if (this.sleepCounter * this.sleepInterval >= this._timeout)
        //        {
        //            t.Abort();
        //            this._isTimeout = true;
        //        }
        //        else
        //        {
        //            this.Sleep(t);
        //        }
        //    }
        //}
    }
}
