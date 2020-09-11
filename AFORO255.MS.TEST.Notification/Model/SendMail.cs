using System.ComponentModel.DataAnnotations;

namespace AFORO255.MS.TEST.Notification.Model
{
    public class SendMail
    {
        [Key]
        public int SendMailId { get; set; }
        public string SendDate { get; set; }
        public int InvoiceId { get; set; }
    }
}
