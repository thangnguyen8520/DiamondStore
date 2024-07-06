using DiamondStoreService.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreService.Utils
{
    public class EmailSender : IEmailSender
    {
        string SmtpServer = "smtp.gmail.com";
        int SmtpPort = 587;
        string SenderName = "Diamond Store";
        string SenderEmail = "diamonstore@support";
        string Username = "minhtam250102@gmail.com";
        string Password = "ggsh agkj dmpm";
        bool UseSSL = false;

        public async Task SendEmailAsync(string toEmail, string subject, string htmlMessage)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(SenderName, SenderEmail));
            email.To.Add(new MailboxAddress(toEmail, toEmail));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = htmlMessage };

            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync(SmtpServer, SmtpPort, UseSSL);
                await smtp.AuthenticateAsync(Username, Password + " pyha");
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
        }
    }
}
