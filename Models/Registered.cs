namespace RegistrationControl.Models
{
    public class Registered : User
    {
        public ICollection<Enrollment> Enrollments = new List<Enrollment>();
        public Registered() : base() { }
        public Registered(string name, DateTime birthDate, string email, string instagramAddress) : base(name, birthDate, email, instagramAddress) { }
    }
}