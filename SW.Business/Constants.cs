using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW.Business
{
    public static class Constants
    {
        public const string ARCTICLEHTMLSTRING = @"<h2>$htmlformat[3]</h2>
                                                    <p class='dateview'><span>发布时间：$htmlformat[4]</span><span>作者：$htmlformat[5]</span><span>分类：[<a href='/news/life/'>$htmlformat[2]</a>]</span></p>
                                                    <figure><img src='images/001.png'></figure>
                                                    <ul class='nlist'>
                                                        $htmlformat[6]
                                                        <a title='$htmlformat[7]' href='$htmlformat[8]' target='_blank' class='readmore'>阅读全文>></a>
                                                    </ul>
                                                    <div class='line'></div> ";
        public const string ARCTICLEHTMLFOOTERSTRING = "<div class='blank'></div><div class='ad'> <img src='images/ad.png'> </div>";
        public const string PAGESTRING = @"<div class='page'>
                                                <a title='Total record'><b>$htmlformat[9]</b></a><b>$htmlformat[10]</b>
                                                <a href='$htmlformat[11]'>2</a><a href='$htmlformat[12]'></a>
                                                <a href='/news/s/index_2.html'></a>
                                            </div>"; 
    }
}
