using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RegistrationControl.Models.Enums
{
    public enum PaymentStatus : int
    {
        Pending = 0,

        [Display(Name = "Paid Out")]
        PaidOut = 1,
    }
}