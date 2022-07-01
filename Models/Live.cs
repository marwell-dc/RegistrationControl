using System.ComponentModel.DataAnnotations;

namespace RegistrationControl.Models
{
    public class Live : Base
    {
        [Required(ErrorMessage = "{0} Required.")]
        [Display(Name = "Name:")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} Required.")]
        [Display(Name = "Description:")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}.")]
        public string Description { get; set; }

        public int InstructorId { get; set; }

        [Required(ErrorMessage = "{0} Required.")]
        [Display(Name = "Instructor Name:")]
        public Instructor Instructor { get; set; }

        [Required(ErrorMessage = "{0} Required.")]
        [Display(Name = "Data and Hour:")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateAndHour { get; set; }

        [Required(ErrorMessage = "{0} Required.")]
        [Display(Name = "Duration Minutes:")]
        public int DurationMinutes { get; set; }

        [Required(ErrorMessage = "{0} Required.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Registration Fee:")]
        [Range(5.00, 100.00, ErrorMessage = "{0} must be from {1} to {2}")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public Decimal RegistrationFee { get; set; }

        public ICollection<Enrollment> Enrollments = new List<Enrollment>();

        public Live() { }

        public Live(string name, string description, DateTime dateAndHour, int durationMinutes, Decimal registrationFee)
        {
            Name = name;
            Description = description;
            DateAndHour = dateAndHour;
            DurationMinutes = durationMinutes;
            RegistrationFee = registrationFee;
        }
    }
}
