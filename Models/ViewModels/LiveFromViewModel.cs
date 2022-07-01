namespace RegistrationControl.Models.ViewModels
{
    public class LiveFromViewModel
    {
        public Live Live { get; set; }
        public ICollection<Instructor> Instructors { get; set; }
    }
}