using System.ComponentModel.DataAnnotations;
using RegistrationControl.Models.Enums;

namespace RegistrationControl.Models
{
    public class Enrollment : Base
    {
        public int LiveId { get; set; }

        [Required(ErrorMessage = "{0} Required.")]
        [Display(Name = "Live:")]
        public Live Live { get; set; }
        public int RegisteredId { get; set; }

        [Required(ErrorMessage = "{0} Required.")]
        [Display(Name = "Registered:")]
        public Registered Registered { get; set; }

        [Required(ErrorMessage = "{0} Required.")]
        [Display(Name = "Due Date:")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "{0} Required.")]
        [Display(Name = "Payment Status:")]
        [EnumDataType(typeof(PaymentStatus))]
        public PaymentStatus PaymentStatus { get; set; }

        public Enrollment() { }
        public Enrollment(DateTime dueDate, PaymentStatus paymentStatus)
        {
            DueDate = dueDate;
            PaymentStatus = paymentStatus;
        }
    }
}