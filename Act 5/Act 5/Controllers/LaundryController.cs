using Act_5.Properties.Laundry;
using Microsoft.AspNetCore.Mvc;

namespace Act_5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LaundryController : ControllerBase
    {
        private static readonly List<UserModel> Users = new List<UserModel>
        {
            // Demo user with a hashed password
            new UserModel { Username = "admin", PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"), Role = "Admin" }
        };
        // Sample in-memory data storage for laundry orders
        private static readonly List<LaundryModel> LaundryOrders = new List<LaundryModel>
        {
            new LaundryModel { Id = 1, CustomerName = "Raniel Fajardo", ContactNumber = "09123456789", Address = "Sta. Cruz, San Juan, So. Leyte", ServiceType = "Wash and Fold", WeightInKg = 5.5, DropOffDate = DateTime.Now.AddDays(-1), PickupDate = null, SpecialInstructions = "Use hypoallergenic detergent.", TotalCost = 250.00m, Status = "Pending" },
            new LaundryModel { Id = 2, CustomerName = "Jessa Mae Villanueva", ContactNumber = "09198765432", Address = "Brgy. Poblacion, Tacloban City", ServiceType = "Dry Cleaning", WeightInKg = 3.2, DropOffDate = DateTime.Now.AddDays(-2), PickupDate = DateTime.Now, SpecialInstructions = "", TotalCost = 300.00m, Status = "Completed" },
            new LaundryModel { Id = 3, CustomerName = "Carlos Santos", ContactNumber = "09187654321", Address = "Malate, Manila", ServiceType = "Ironing", WeightInKg = 4.0, DropOffDate = DateTime.Now.AddDays(-3), PickupDate = null, SpecialInstructions = "Handle with care.", TotalCost = 200.00m, Status = "In Process" },
            new LaundryModel { Id = 4, CustomerName = "Ana Maria Lopez", ContactNumber = "09223334444", Address = "Brgy. Mabini, Cebu City", ServiceType = "Wash and Fold", WeightInKg = 6.0, DropOffDate = DateTime.Now.AddDays(-4), PickupDate = DateTime.Now, SpecialInstructions = "Use fabric softener.", TotalCost = 320.00m, Status = "Delivered" },
            new LaundryModel { Id = 5, CustomerName = "Miguel de la Cruz", ContactNumber = "09128887777", Address = "Intramuros, Manila", ServiceType = "Wash and Fold", WeightInKg = 2.5, DropOffDate = DateTime.Now.AddDays(-1), PickupDate = null, SpecialInstructions = "Separate whites.", TotalCost = 150.00m, Status = "Pending" },
            new LaundryModel { Id = 6, CustomerName = "Marites Reyes", ContactNumber = "09219998888", Address = "Brgy. Sogod, Sogod, So. Leyte", ServiceType = "Dry Cleaning", WeightInKg = 3.8, DropOffDate = DateTime.Now.AddDays(-2), PickupDate = DateTime.Now, SpecialInstructions = "Use mild detergent.", TotalCost = 280.00m, Status = "Completed" },
            new LaundryModel { Id = 7, CustomerName = "Josefina Garcia", ContactNumber = "09331112222", Address = "Brgy. 5, Laoag City", ServiceType = "Ironing", WeightInKg = 4.5, DropOffDate = DateTime.Now.AddDays(-3), PickupDate = null, SpecialInstructions = "", TotalCost = 250.00m, Status = "In Process" },
            new LaundryModel { Id = 8, CustomerName = "Ricardo Mendoza", ContactNumber = "09342223333", Address = "Brgy. Ligid, Davao City", ServiceType = "Wash and Fold", WeightInKg = 5.0, DropOffDate = DateTime.Now.AddDays(-1), PickupDate = null, SpecialInstructions = "No bleach.", TotalCost = 300.00m, Status = "Pending" },
            new LaundryModel { Id = 9, CustomerName = "Bea Alonzo", ContactNumber = "09454445555", Address = "Brgy. Maginhawa, Quezon City", ServiceType = "Dry Cleaning", WeightInKg = 3.6, DropOffDate = DateTime.Now.AddDays(-2), PickupDate = DateTime.Now, SpecialInstructions = "Delicate fabrics.", TotalCost = 350.00m, Status = "Completed" },
            new LaundryModel { Id = 10, CustomerName = "Paolo Ballesteros", ContactNumber = "09556667777", Address = "Brgy. Tambo, Paranaque", ServiceType = "Ironing", WeightInKg = 4.2, DropOffDate = DateTime.Now.AddDays(-3), PickupDate = null, SpecialInstructions = "Light steam only.", TotalCost = 220.00m, Status = "In Process" },
            new LaundryModel { Id = 11, CustomerName = "Elena Cruz", ContactNumber = "09667778888", Address = "Brgy. Lapaz, Iloilo City", ServiceType = "Wash and Fold", WeightInKg = 5.2, DropOffDate = DateTime.Now.AddDays(-1), PickupDate = null, SpecialInstructions = "No fabric softener.", TotalCost = 270.00m, Status = "Pending" },
            new LaundryModel { Id = 12, CustomerName = "Arvin Villanueva", ContactNumber = "09778889999", Address = "Brgy. Manggahan, Pasig City", ServiceType = "Dry Cleaning", WeightInKg = 4.0, DropOffDate = DateTime.Now.AddDays(-2), PickupDate = DateTime.Now, SpecialInstructions = "", TotalCost = 400.00m, Status = "Completed" },
            new LaundryModel { Id = 13, CustomerName = "Kristina Valdez", ContactNumber = "09889991111", Address = "Brgy. San Miguel, Dagupan City", ServiceType = "Ironing", WeightInKg = 4.8, DropOffDate = DateTime.Now.AddDays(-3), PickupDate = null, SpecialInstructions = "Press collars well.", TotalCost = 230.00m, Status = "In Process" },
            new LaundryModel { Id = 14, CustomerName = "Jonathan Padilla", ContactNumber = "09990001234", Address = "Brgy. Kalunasan, Cebu City", ServiceType = "Wash and Fold", WeightInKg = 6.3, DropOffDate = DateTime.Now.AddDays(-1), PickupDate = null, SpecialInstructions = "Separate colored clothes.", TotalCost = 320.00m, Status = "Pending" },
            new LaundryModel { Id = 15, CustomerName = "Nina Perez", ContactNumber = "09001112222", Address = "Brgy. Bonuan, Pangasinan", ServiceType = "Dry Cleaning", WeightInKg = 3.1, DropOffDate = DateTime.Now.AddDays(-2), PickupDate = DateTime.Now, SpecialInstructions = "No starch.", TotalCost = 340.00m, Status = "Completed" },
            new LaundryModel { Id = 16, CustomerName = "Enrique Gil", ContactNumber = "09102223333", Address = "Brgy. Kamuning, Quezon City", ServiceType = "Ironing", WeightInKg = 5.0, DropOffDate = DateTime.Now.AddDays(-3), PickupDate = null, SpecialInstructions = "Low heat only.", TotalCost = 200.00m, Status = "In Process" },
            new LaundryModel { Id = 17, CustomerName = "Lea Salonga", ContactNumber = "09213334444", Address = "Brgy. Capitol Site, Cebu City", ServiceType = "Wash and Fold", WeightInKg = 5.7, DropOffDate = DateTime.Now.AddDays(-1), PickupDate = null, SpecialInstructions = "Use scented detergent.", TotalCost = 290.00m, Status = "Pending" },
            new LaundryModel { Id = 18, CustomerName = "Gabriel Mercado", ContactNumber = "09334445555", Address = "Brgy. Talomo, Davao City", ServiceType = "Dry Cleaning", WeightInKg = 4.5, DropOffDate = DateTime.Now.AddDays(-2), PickupDate = DateTime.Now, SpecialInstructions = "Gentle cycle.", TotalCost = 360.00m, Status = "Completed" },
            new LaundryModel { Id = 19, CustomerName = "Angela Bautista", ContactNumber = "09445556666", Address = "Brgy. Divisoria, Zamboanga City", ServiceType = "Ironing", WeightInKg = 3.9, DropOffDate = DateTime.Now.AddDays(-3), PickupDate = null, SpecialInstructions = "", TotalCost = 210.00m, Status = "In Process" },
            new LaundryModel { Id = 20, CustomerName = "Francisco Ybañez", ContactNumber = "09556667777", Address = "Brgy. Gusa, Cagayan de Oro", ServiceType = "Wash and Fold", WeightInKg = 4.2, DropOffDate = DateTime.Now.AddDays(-1), PickupDate = null, SpecialInstructions = "Quick wash.", TotalCost = 260.00m, Status = "Pending" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<LaundryModel>> Get()
        {
            return Ok(LaundryOrders);
        }

        [HttpGet("{id}")]
        public ActionResult<LaundryModel> Get(int id)
        {
            var order = LaundryOrders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public ActionResult Post([FromBody] LaundryModel laundryOrder)
        {
            if (laundryOrder == null)
            {
                return BadRequest("Laundry order data is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Generate a new ID based on the existing max ID
            int newId = LaundryOrders.Count > 0 ? LaundryOrders.Max(o => o.Id) + 1 : 1;
            laundryOrder.Id = newId;

            LaundryOrders.Add(laundryOrder);
            return CreatedAtAction(nameof(Get), new { id = laundryOrder.Id }, laundryOrder);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] LaundryModel updatedOrder)
        {
            if (updatedOrder == null)
            {
                return BadRequest("Laundry order data is required.");
            }

            var existingOrder = LaundryOrders.FirstOrDefault(o => o.Id == id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            // Update properties of the existing order
            existingOrder.CustomerName = updatedOrder.CustomerName;
            existingOrder.ContactNumber = updatedOrder.ContactNumber;
            existingOrder.Address = updatedOrder.Address;
            existingOrder.ServiceType = updatedOrder.ServiceType;
            existingOrder.WeightInKg = updatedOrder.WeightInKg;
            existingOrder.DropOffDate = updatedOrder.DropOffDate;
            existingOrder.PickupDate = updatedOrder.PickupDate;
            existingOrder.SpecialInstructions = updatedOrder.SpecialInstructions;
            existingOrder.TotalCost = updatedOrder.TotalCost;
            existingOrder.Status = updatedOrder.Status;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var order = LaundryOrders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            LaundryOrders.Remove(order);
            return NoContent();
        }
    }
}
