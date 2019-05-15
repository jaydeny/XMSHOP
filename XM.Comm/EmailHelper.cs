using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace XM.Comm
{
    /// <summary>
    /// 创建人：朱茂琛
    /// 创建时间：2019/04/25
    /// 邮箱
    /// </summary>
    public class EmailHelper
    {
        //发送人邮箱地址
        private static string Sender = "17875628610@qq.com";
        //邮箱授权码
        private static string AuthCode = "nehiijlytktuhgjc";
        #region  发送邮件（单个）
        public static bool send(string Receiver, string Subject, string Body)
        {
            try
            {
                //邮箱信息
                MailMessage mail = new MailMessage(Sender, Receiver);
                mail.Subject = Subject;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.Body = Body;
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = true;

                //实例化一个SmtpClient类。
                SmtpClient client = new SmtpClient();

                #region 设置邮件服务器地址

                if (Sender.Length != 0)
                {
                    //根据发件人的邮件地址判断发件服务器地址   默认端口一般是25
                    string[] addressor = Sender.Trim().Split(new Char[] { '@', '.' });
                    switch (addressor[1])
                    {
                        case "163":
                            client.Host = "smtp.163.com";
                            break;
                        case "126":
                            client.Host = "smtp.126.com";
                            break;
                        case "qq":
                            client.Host = "smtp.qq.com";
                            break;
                        case "gmail":
                            client.Host = "smtp.gmail.com";
                            break;
                        case "hotmail":
                            client.Host = "smtp.live.com";//outlook邮箱
                            //client.Port = 587;
                            break;
                        case "foxmail":
                            client.Host = "smtp.foxmail.com";
                            break;
                        case "sina":
                            client.Host = "smtp.sina.com.cn";
                            break;
                        default:
                            client.Host = "smtp.exmail.qq.com";//qq企业邮箱
                            break;
                    }
                }
                #endregion

                //使用安全加密连接。
                client.EnableSsl = true;
                //不和请求一块发送。
                client.UseDefaultCredentials = false;

                //验证发件人身份(发件人的邮箱，邮箱里的生成授权码);
                client.Credentials = new NetworkCredential(Sender, AuthCode);

                //如果发送失败，SMTP 服务器将发送 失败邮件告诉我  
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                //发送
                client.Send(mail);

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
        #region  发送邮件（群发）
        public static bool Allsend(List<string> Receivers,string Subject,string Body)
        {
            bool f = false;
            foreach (string r in Receivers)
            {
                f=send(r, Subject, Body);
            }
            return f;
        }
        #endregion
        #region 发送邮件加附件方法
        /// <param name="Receiver">接收邮箱地址</param>
        /// <param name="Subject">邮件标题</param>
        /// <param name="Body">邮件内容</param>
        /// <param name="File_Path">附件</param>
        /// <returns></returns>
        public static bool SendMail(string Receiver, string Subject, string Body, string File_Path)
        {
            try
            {
                //实例化一个发送邮件类。
                MailMessage mail = new MailMessage(Sender, Receiver);
                mail.Subject = Subject;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.Body = Body;
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = true;

                //邮件的优先级，分为 Low, Normal, High，通常用 Normal即可
                mail.Priority = MailPriority.Normal;

                //设置邮件的附件，将在客户端选择的附件先上传到服务器保存一个，然后加入到mail中  
                if (File_Path != "" && File_Path != null)
                {
                    //将附件添加到邮件
                    mail.Attachments.Add(new Attachment(File_Path));
                    //获取或设置此电子邮件的发送通知。
                    mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                }

                //实例化一个SmtpClient类。
                SmtpClient client = new SmtpClient();

                #region 设置邮件服务器地址

                if (Sender.Length != 0)
                {
                    //根据发件人的邮件地址判断发件服务器地址   默认端口一般是25
                    string[] addressor = Sender.Trim().Split(new Char[] { '@', '.' });
                    switch (addressor[1])
                    {
                        case "163":
                            client.Host = "smtp.163.com";
                            break;
                        case "126":
                            client.Host = "smtp.126.com";
                            break;
                        case "qq":
                            client.Host = "smtp.qq.com";
                            break;
                        case "gmail":
                            client.Host = "smtp.gmail.com";
                            break;
                        case "hotmail":
                            client.Host = "smtp.live.com";//outlook邮箱
                            //client.Port = 587;
                            break;
                        case "foxmail":
                            client.Host = "smtp.foxmail.com";
                            break;
                        case "sina":
                            client.Host = "smtp.sina.com.cn";
                            break;
                        default:
                            client.Host = "smtp.exmail.qq.com";//qq企业邮箱
                            break;
                    }
                }
                #endregion

                //使用安全加密连接。
                client.EnableSsl = true;
                //不和请求一块发送。
                client.UseDefaultCredentials = false;

                //验证发件人身份(发件人的邮箱，邮箱里的生成授权码);
                client.Credentials = new NetworkCredential(Sender, AuthCode);

                //如果发送失败，SMTP 服务器将发送 失败邮件告诉我  
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                //发送
                client.Send(mail);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        #endregion
    }
}
