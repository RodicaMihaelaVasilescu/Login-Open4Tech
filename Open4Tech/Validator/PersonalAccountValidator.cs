using System;
using System.Linq;
using System.Net.Mail;
using Open4Tech.Validator.Interfaces;

namespace Open4Tech.Validator
{
    class PersonalAccountValidator : IPersonalAccountValidator<PersonalAccountValidator>
    {
        public string ValidationMessage;
        public bool IsValid;
        public PersonalAccountValidator ValidatePassword()
        {
            return null;
        }
        public PersonalAccountValidator ValidatePassword(string password)
        {
            IsValid = true;
            ValidationMessage = string.Empty;

            if (string.IsNullOrEmpty(password))
            {
                ValidationMessage = "The password cannot be null.";
                IsValid = false;
                return this;
            }
            if (password.Count() < 8)
            {
                ValidationMessage = "The password should have at least 8 characters.";
                IsValid = false;
            }

            if (password.Any(ch => ch == ' '))
            {
                ValidationMessage = "The password can not contain spaces.";
                IsValid = false;
            }
            return this;
        }

        public PersonalAccountValidator ValidateEmail(string email, string emailSubject, string emailContent)
        {
            if(email == null)
            {
                IsValid = false;
                ValidationMessage = "The email cannot be null";
                return this;
            }
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("ArtClub.App@gmail.com");
                mail.To.Add(email);
                mail.Subject = emailSubject;
                mail.Body = emailContent;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("ArtClub.App@gmail.com", "ArtClub.App@gmail.com");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                IsValid = true;
            }
            catch (Exception)
            {
                IsValid = false;
                ValidationMessage = "The email you entered is not valid. Please re-enter your email.";
            }
            return this;
        }
    }
}
