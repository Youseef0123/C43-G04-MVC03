using System.Net;
using System.Net.Mail;
using System.Security.Policy;

namespace Demo.presntation.Utilities
{
    public class EmailSetting
    {
        public static void SendEmail(Email email)
        {
            //Sending Email
            try
            {
                var Client = new SmtpClient("smtp.gmail.com", 587);
                Client.EnableSsl = true;
                Client.Credentials = new NetworkCredential("youoseefmemo2016@gmail.com", "wmogkpevwfjdtokx");
                Client.Send("youoseefmemo2016@gmail.com", email.To, email.Subject, email.Body);

            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
