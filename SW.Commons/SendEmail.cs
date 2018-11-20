/*
 * 文件名：SendEmail.cs
 * 功能：实现邮件的发送
 * 制作人：吉桂昕
 * 时间：2009-3-23
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net.Mail;

namespace SW.Commons
{
    /// <summary>
    /// 发送邮件
    /// </summary>
    //public class SendEmail
    //{
    //    //private string sendToAddress;
    //    //private string subject;
    //    //private string body;
    //    //private readonly string fromUserName = ConfigurationManager.AppSettings["fromUserName"];
    //    //private readonly string fromPassword = ConfigurationManager.AppSettings["fromPassword"];
    //    //private readonly string fromHost = ConfigurationManager.AppSettings["fromHost"];
    //    //private readonly int fromPort = Convert.ToInt32(ConfigurationManager.AppSettings["fromPort"]);

    //    ///// <summary>
    //    ///// 读取或设置收件人的地址
    //    ///// </summary>
    //    //public string SendToAddress
    //    //{
    //    //    get
    //    //    {
    //    //        return sendToAddress;
    //    //    }
    //    //    set
    //    //    {
    //    //        sendToAddress = value;
    //    //    }
    //    //}

    //    ///// <summary>
    //    ///// 读取或设置邮件的主题
    //    ///// </summary>
    //    //public string Subject
    //    //{
    //    //    get
    //    //    {
    //    //        return subject;
    //    //    }
    //    //    set
    //    //    {
    //    //        subject = value;
    //    //    }
    //    //}

    //    ///// <summary>
    //    ///// 读取或设置邮件内容
    //    ///// </summary>
    //    //public string Body
    //    //{
    //    //    get
    //    //    {
    //    //        return body;
    //    //    }
    //    //    set
    //    //    {
    //    //        body = value;
    //    //    }
    //    //}

    //    ///// <summary>
    //    ///// 从那来的邮件地址
    //    ///// </summary>
    //    //public string From
    //    //{
    //    //    get;
    //    //    set;
    //    //}

    //    ///// <summary>
    //    ///// 显示的来源的名字
    //    ///// </summary>
    //    //public string DisplayName
    //    //{
    //    //    get;
    //    //    set;
    //    //}

    //    ///// <summary>
    //    ///// 无参构造函数
    //    ///// </summary>
    //    //public SendEmail()
    //    //{ }

    //    ///// <summary>
    //    ///// 有参构造函数
    //    ///// </summary>
    //    ///// <param name="sendToAddress"></param>
    //    ///// <param name="subject"></param>
    //    ///// <param name="body"></param>
    //    //public SendEmail(string sendToAddress, string subject, string body)
    //    //{
    //    //    this.sendToAddress = sendToAddress;
    //    //    this.subject = subject;
    //    //    this.body = body;
    //    //}

    //    //public SendEmail(string sendToAddress, string subject, string body, string From, string DisplayName)
    //    //{
    //    //    this.sendToAddress = sendToAddress;
    //    //    this.subject = subject;
    //    //    this.body = body;
    //    //    this.From = From;
    //    //    this.DisplayName = DisplayName;
    //    //}

    //    ///// <summary>
    //    ///// 发送一封或多封邮件
    //    ///// </summary>
    //    ///// <returns></returns>
    //    //public bool SendEmails()
    //    //{
    //    //    bool isSecurity = false;


    //    //    MailMessage msg = new MailMessage();
    //    //    try
    //    //    {
    //    //        if (From == null)
    //    //        {
    //    //            From = fromUserName;
    //    //        }
    //    //        if (DisplayName == null)
    //    //        {
    //    //            DisplayName = "德合商务公司";
    //    //        }
    //    //        msg.To.Add(new MailAddress(sendToAddress));                
    //    //        msg.From = new MailAddress(From, DisplayName, Encoding.UTF8);
    //    //        msg.Subject = subject;
    //    //        msg.Body = @body;
    //    //        msg.Priority = MailPriority.High;
    //    //        msg.IsBodyHtml = true;
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        throw new Exception(ex.Message);
    //    //    }
    //    //    SmtpClient client = new SmtpClient(fromHost, fromPort);
    //    //    client.Credentials = new System.Net.NetworkCredential(fromUserName, fromPassword);
    //    //    client.EnableSsl = true;

    //    //    try
    //    //    {
    //    //        client.SendAsync(msg, (object)msg);
    //    //        isSecurity = true;
    //    //    }
    //    //    catch
    //    //    {
    //    //        isSecurity = false;
    //    //    }
    //    //    return isSecurity;
    //    //}
        
    //}
}
