using MailKit.Net.Smtp;
using MimeKit;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace APIForEmail
{
    public class MailBuilder
    {
        [RegularExpression(@"^((\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*)\\s*[;]{0,1}\\s*)+$")]
        [Required] public string FromMail { get; set; } = string.Empty;

        [RegularExpression(@"^((\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*)\\s*[;]{0,1}\\s*)+$")]
        [Required] public string ToMail { get; set; }
        [Required] public string Subject { get; set; } = string.Empty;
        [Required] public string FromWhom { get; set; } = string.Empty;
        [Required] public string AuthenticateKey { get; set; } = string.Empty;
        [Required] public string Body { get; set; } = string.Empty;
        public TypeOfLetter Type { get; set; }

        public MailBuilder(string subject, TypeOfLetter type,
            string toMail, string body,
            string fromMail = "coffeeshop_cafe@list.ru",
            string fromWhom = "Администрация сайта",
            string authenticateKey = "3MTnGBVgwXz43eAs1TqV")
        {
            this.Body = body;
            this.ToMail = toMail;
            this.Type = type;
            this.FromMail = fromMail;
            this.Subject = subject;
            this.FromWhom = fromWhom;
            this.AuthenticateKey = authenticateKey;
        }

        public void Send()
        {
            new Task(SendMessage).Start();
        }
        private void SendMessage()
        {
            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(this.FromWhom, this.FromMail));
            emailMessage.To.Add(new MailboxAddress("", ToMail));
            emailMessage.Subject = this.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = GetContentForType()
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.mail.ru", 465, true);
                client.Authenticate(this.FromMail, this.AuthenticateKey);
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }

        private string GetContentForType()
        {
            string title = string.Empty;
            switch (this.Type)
            {
                case TypeOfLetter.NewFunc: title = "Мы добавили новую фишку!"; break;
                case TypeOfLetter.NewNews: title = "Похоже у нас новости!"; break;
                case TypeOfLetter.Welcome: title = "Доброе пожаловать к нам!"; break;
                case TypeOfLetter.NewProduct: title = "У нас для тебя новинка!"; break;
            }
            return $"<div>\r\n    <style>\r\n        @import url('https://fonts.googleapis.com/css2?family=Montserrat:wght@400;900&display=swap');\r\n        * {{\r\n            margin: 0;\r\n            padding: 0;\r\n            font-family: 'Montserrat', sans-serif;\r\n        }}\r\n    </style>\r\n    <div style=\"background-color: yellow; padding: 15px 20px; display: flex; flex-wrap: wrap; flex-direction: column;\">\r\n        <h1 style=\"font-weight: 900; font-size: 45px;\">{title}</h1>\r\n        <p style=\"font-weight: 300; font-size: 20px; margin: 15px 0;\">{Body}</p>\r\n        <a onmouseover=\"(this.style.color='black')(this.style.background='white')\" \r\n           onmouseleave=\"(this.style.color='white')(this.style.background='black')\" \r\n        style=\"padding: 10px; background-color: black; color: white; transition: .5s; text-decoration: none; width: 150px; margin: 15px 0; text-align: center;\" href=\"https://zephyrxxx.github.io/private-coffee-shop/index.htm\">Наш сайт</a>\r\n    </div>\r\n    <div class=\"company\" style=\"display: flex; justify-content: center; padding: 20px 0;\">\r\n        <img src=\"https://zephyrxxx.github.io/private-coffee-shop/image%20svg/LOGO.svg\"/>\r\n    </div>    \r\n</div>";
        }
    }
}

