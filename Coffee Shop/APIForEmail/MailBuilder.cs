using MailKit.Net.Smtp;
using MimeKit;
using Org.BouncyCastle.Asn1.X509.SigI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Xml.Linq;

namespace APIForEmail
{
    public enum TypeOfLetter : byte
    {
        NewNews,
        NewProduct,
        NewFunctui
    }

    public class MailBuilder
    {
        [RegularExpression(@"^((\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*)\\s*[;]{0,1}\\s*)+$")]
        [Required] public string FromMail { get; set; } = string.Empty;
        
        [RegularExpression(@"^((\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*)\\s*[;]{0,1}\\s*)+$")]
        [Required] public IEnumerable<String>? ToMail { get; set; } 
        [Required] public string Subject { get; set; } = string.Empty;
        [Required] public string FromWhom { get; set; } = string.Empty;
        [Required] public string AuthenticateKey { get; set; } = string.Empty;
        public TypeOfLetter Type { get; set; }

        

        // "coffeeshop_cafe@list.ru"
        // "3MTnGBVgwXz43eAs1TqV"

        public void Send()
        {
            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(this.FromWhom, this.FromMail));
            //emailMessage.To.Add(new MailboxAddress("", this.ToMail));
            ToMail?.ToList().ForEach(x => emailMessage.To.Add(new MailboxAddress("", x)));
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
            switch (this.Type) 
            {
                case TypeOfLetter.NewNews: return File.ReadAllText(@"../../../../APIForEmail/Templates/News.htm");
                case TypeOfLetter.NewFunctui: return File.ReadAllText(@"../../../../APIForEmail/Templates/.htm");
                case TypeOfLetter.NewProduct: return File.ReadAllText(@"../../../../APIForEmail/Templates/Products.htm");
            }
            return string.Empty;
        }
    }
}

