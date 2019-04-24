using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace XM.Comm
{
    public class EmailHelper
    {
        static string Sender = "17875628610@qq.com";
        static string AuthCode = "nehiijlytktuhgjc";
        //单独发送一个邮件
        public static bool send(string Receiver, string Subject, string Body)
        {

            //邮箱信息
            MailMessage mail = new MailMessage(Sender, Receiver);
            mail.Subject = Subject;
            mail.SubjectEncoding = Encoding.UTF8;
            mail.Body = Body;
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;

            //邮箱发送端
            SmtpClient client = new SmtpClient("smtp.qq.com");
            //是否启动SSL加密 ，也就是SSL证书
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(Sender, AuthCode);
            //发送
            try
            {
                client.Send(mail);
            }
            catch
            {
                return false;
            }
            

            return true;
        }

        public static bool Allsend(List<string> Receivers,string Subject,string Body)
        {
            bool f = false;
            foreach (string r in Receivers)
            {
                f=send(r, Subject, Body);
            }
            return f;
        }

    }
}
