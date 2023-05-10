using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIForEmail
{
    public class EmailService
    {
        private string HtmlContent { get; set; } = "<div>\r\n    <style>\r\n        @import url('https://fonts.googleapis.com/css2?family=Montserrat:wght@400;900&display=swap');\r\n        * {\r\n            margin: 0;\r\n            padding: 0;\r\n            font-family: 'Montserrat', sans-serif;\r\n        }\r\n    </style>\r\n    <div style=\"background-color: yellow; padding: 15px 20px; display: flex; flex-wrap: wrap; flex-direction: column;\">\r\n        <h1 style=\"font-weight: 900; font-size: 45px;\">15% OFF* FOR YOU</h1>\r\n        <p style=\"font-weight: 300; font-size: 20px; margin: 15px 0;\">To redeem offer, enter promo code below during online checkout or present this email at your local Coffee Shop.\r\nLimited time offer valid until September 30, 2020.</p>\r\n        <a onmouseover=\"(this.style.color='black')(this.style.background='white')\" \r\n           onmouseleave=\"(this.style.color='white')(this.style.background='black')\" \r\n        style=\"padding: 10px; background-color: black; color: white; transition: .5s; text-decoration: none; width: 150px; margin: 15px 0; text-align: center;\" href=\"https://zephyrxxx.github.io/private-coffee-shop/index.htm\">REDEEM ONLINE</a>\r\n    </div>\r\n    <div class=\"company\" style=\"display: flex; justify-content: center; padding: 20px 0;\">\r\n        <img src=\"https://zephyrxxx.github.io/private-coffee-shop/image%20svg/LOGO.svg\"/>\r\n    </div>    \r\n</div>";
        
        public EmailService() 
        {
            // делать ли чтение из контетна из файла или оставить строкой ? 

            //Task GetHtmlContent = new Task(InitializeHtmlContent);
            //GetHtmlContent.Start();

            //InitOfCollectionemail();
            SendEmail("alex_puzikov@list.ru", "test");
        }

        private void InitOfCollectionemail()
        {
            
        }

        //private void InitializeHtmlContent()
        //{
        //    //HtmlContent = File.
        //}

        public static void Send()
        {

        }

        public async Task SendEmail(string email, string subject)
        {
            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "coffeeshop_cafe@list.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = HtmlContent
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.mail.ru", 465, true);
                await client.AuthenticateAsync("coffeeshop_cafe@list.ru", "3MTnGBVgwXz43eAs1TqV");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }

        // @2o!&69@2o!&69
        // 3MTnGBVgwXz43eAs1TqV
        // coffeeshop_cafe@list.ru
        // Аутлук на домашнем компьютере@2o!&69
    }
}
