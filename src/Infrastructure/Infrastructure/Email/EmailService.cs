// <copyright file="EmailService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AspNetCoreAngular.Infrastructure.Email
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Mail;
    using System.Reflection;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using AspNetCoreAngular.Application.Abstractions;
    using AspNetCoreAngular.Application.Features.Notifications.Models;
    using AspNetCoreAngular.Application.Settings;
    using Microsoft.AspNetCore.Routing.Template;
    using Microsoft.Extensions.Logging;

    public class EmailService : IEmailService
    {
        private readonly EmailSettings emailSettings;
        private readonly ILogger<EmailService> logger;

        public EmailService(EmailSettings emailSettings, ILogger<EmailService> logger)
        {
            this.emailSettings = emailSettings;
            this.logger = logger;
        }

        public Task RegistrationConfirmationEmail(string to, string link)
        {
            var registrationTemplate = GetBaseTemplate("RegistrationTemplate");

            registrationTemplate = registrationTemplate
                .Replace("@user@", to)
                .Replace("@linktext@", "Activate account")
                .Replace("@link@", link);
            var emailMessage = new EmailMessage
            {
                To = to,
                Body = registrationTemplate,
                Subject = "Confirm your registration",
                From = this.emailSettings.SmtpSenderAddress,
            };
            this.SendAsync(emailMessage);
            return Task.CompletedTask;
        }

        public Task ForgottentPasswordEmail(string to, string link)
        {
            var registrationTemplate = GetBaseTemplate("RegistrationTemplate");

            registrationTemplate = registrationTemplate
                .Replace("@user@", to)
                .Replace("@linktext@", "Reset password")
                .Replace("@link@", link);
            var emailMessage = new EmailMessage
            {
                To = to,
                Body = registrationTemplate,
                Subject = "Reset password",
                From = this.emailSettings.SmtpSenderAddress,
            };
            this.SendAsync(emailMessage);
            return Task.CompletedTask;
        }

        public Task SendCustomerCreatedEmail(EmailMessage emailMessage)
        {
            return Task.CompletedTask;
        }

        private static string GetBaseTemplate(string bodyTemplateName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var baseTemplateStream = assembly.GetManifestResourceStream(
                "AspNetCoreAngular.Infrastructure.Email.Templates.BaseTemplate.html");
            using var baseReader = new StreamReader(baseTemplateStream, Encoding.UTF8);
            var baseTemplate = baseReader.ReadToEnd();

            var bodyTemplateStream = assembly.GetManifestResourceStream(
                $"AspNetCoreAngular.Infrastructure.Email.Templates.{bodyTemplateName}.html");
            using var bodyReader = new StreamReader(bodyTemplateStream, Encoding.UTF8);
            var bodyTemplate = bodyReader.ReadToEnd();

            var template = baseTemplate.Replace("@content@", bodyTemplate);

            return template;
        }

        private Task SendAsync(EmailMessage emailMessage)
        {
            try
            {
                var mailMessage = new MailMessage(
                    this.emailSettings.SmtpSenderAddress,
                    emailMessage.To,
                    emailMessage.Subject,
                    emailMessage.Body)
                {
                    From = new MailAddress(
                        this.emailSettings.SmtpSenderAddress,
                        this.emailSettings.SmtpSenderName),
                    IsBodyHtml = true,
                };

                foreach (var attachment in emailMessage.Attachments)
                {
                    mailMessage.Attachments.Add(
                        new Attachment(
                            new MemoryStream(attachment.Content),
                            attachment.Name,
                            attachment.ContentType));
                }

                using var client = new SmtpClient(this.emailSettings.SmtpHost, this.emailSettings.SmtpPort)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(
                        this.emailSettings.SmtpUsername,
                        this.emailSettings.SmtpPassword),
                };

                client.Send(mailMessage);

                var emailMessageTo = emailMessage.To;
                var eMailMessageSubject = emailMessage.Subject;
                this.logger.LogInformation(
                    "Email sent successfully to: {EmailMessageTo}. Subject: {EMailMessageSubject}", emailMessageTo, eMailMessageSubject);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                var emailMessageTo = emailMessage.To;
                this.logger.LogError(ex, "Email failed to send to {EmailMessageTo}", emailMessageTo);
                throw;
            }
        }
    }
}
