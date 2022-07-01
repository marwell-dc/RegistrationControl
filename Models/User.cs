using System.ComponentModel.DataAnnotations;

namespace RegistrationControl.Models
{
    public class User : Base
    {
        [Required(ErrorMessage = "{0} Required.")]
        [Display(Name = "Name:")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} Required.")]
        [Display(Name = "Birth Date:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} Required.")]
        [Display(Name = "E-mail Address:")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} Required.")]
        [Display(Name = "Instagram Address:")]
        [DataType(DataType.Url)]
        public string InstagramAddress { get; set; }

        public User() { }

        public User(string name, DateTime birthDate, string email, string instagramAddress)
        {
            Name = name;
            BirthDate = birthDate;
            Email = email;
            InstagramAddress = instagramAddress;
        }
    }
}