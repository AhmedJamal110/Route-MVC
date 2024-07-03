using System.Net;
using System.Net.Mail;

namespace Demo.PL.Helper
{
    public static class EmailSetting
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp@gmail.com", 587);

            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("ahmed.jimmy@gmail.com", "");
            client.Send("ahmed.jimmy@gmail.com", email.To, email.Subject, email.Body);


        }
    }
}
