namespace RegistrationControl.Models.ViewModels
{
    public class EnrollmentFromViewModel
    {
        public Enrollment Enrollment { get; set; }
        public ICollection<Live> Lives { get; set; }
        public ICollection<Registered> Registereds { get; set; }
    }
}