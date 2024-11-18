using System.ComponentModel.DataAnnotations;

namespace Act_5.Properties.Laundry
{
    public class LaundryManagerModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string ContactNumber { get; set; }

        public string Address { get; set; }

        public string Role { get; set; } = "Manager"; // Default role is "Manager"

    }
}
