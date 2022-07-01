namespace RegistrationControl.Models
{
    public class Instructor : User
    {
        public ICollection<Live> Lives { get; set; } = new List<Live>();
        public Instructor() : base() { }
        public Instructor(string name, DateTime birthDate, string email, string instagramAddress) : base(name, birthDate, email, instagramAddress) { }

        public void AddLive(Live live)
        {
            Lives.Add(live);
        }

        public void RemoveLive(Live live)
        {
            Lives.Remove(live);
        }
    }
}