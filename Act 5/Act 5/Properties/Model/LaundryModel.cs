using System.ComponentModel.DataAnnotations;

namespace Act_5.Properties.Laundry
{
    public class LaundryModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }

        public string ContactNumber { get; set; }

        public string Address { get; set; }

        public string ServiceType { get; set; } // e.g., "Wash and Fold", "Dry Cleaning", "Ironing"

        public double WeightInKg { get; set; } // Weight of laundry in kilograms

        public DateTime DropOffDate { get; set; }

        public DateTime? PickupDate { get; set; } // Optional if the laundry has not yet been picked up

        public string SpecialInstructions { get; set; } // Any special handling instructions for the laundry

        public decimal TotalCost { get; set; }
        public string Status { get; set; } // e.g., "Pending", "In Process", "Completed", "Delivered"
    }
}
