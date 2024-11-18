using Act_5.Properties.Laundry;
using Microsoft.AspNetCore.Mvc;

namespace Act_5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LaundryManagerController : ControllerBase
    {
        // Sample in-memory data storage for laundry managers
        private static readonly List<LaundryManagerModel> LaundryManagers = new List<LaundryManagerModel>
        {
            new LaundryManagerModel
            {
                Id = 1,
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@example.com",
                ContactNumber = "09112223333",
                Address = "123 Main St, Cityville"
            },
            new LaundryManagerModel
            {
                Id = 2,
                FirstName = "John",
                LastName = "Smith",
                Email = "john.smith@example.com",
                ContactNumber = "09223334444",
                Address = "456 Side Ave, Townsville"
            }
        };

        [HttpGet]
        public ActionResult<IEnumerable<LaundryManagerModel>> Get()
        {
            return Ok(LaundryManagers);
        }

        [HttpGet("{id}")]
        public ActionResult<LaundryManagerModel> Get(int id)
        {
            var manager = LaundryManagers.FirstOrDefault(m => m.Id == id);
            if (manager == null)
            {
                return NotFound();
            }
            return Ok(manager);
        }

        [HttpPost]
        public ActionResult Post([FromBody] LaundryManagerModel manager)
        {
            if (manager == null)
            {
                return BadRequest("Manager data is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Generate a new ID based on the existing max ID
            int newId = LaundryManagers.Count > 0 ? LaundryManagers.Max(m => m.Id) + 1 : 1;
            manager.Id = newId;

            LaundryManagers.Add(manager);
            return CreatedAtAction(nameof(Get), new { id = manager.Id }, manager);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] LaundryManagerModel updatedManager)
        {
            if (updatedManager == null)
            {
                return BadRequest("Manager data is required.");
            }

            var existingManager = LaundryManagers.FirstOrDefault(m => m.Id == id);
            if (existingManager == null)
            {
                return NotFound();
            }

            // Update properties of the existing manager
            existingManager.FirstName = updatedManager.FirstName;
            existingManager.LastName = updatedManager.LastName;
            existingManager.Email = updatedManager.Email;
            existingManager.ContactNumber = updatedManager.ContactNumber;
            existingManager.Address = updatedManager.Address;
            existingManager.Role = updatedManager.Role;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var manager = LaundryManagers.FirstOrDefault(m => m.Id == id);
            if (manager == null)
            {
                return NotFound();
            }

            LaundryManagers.Remove(manager);
            return NoContent();
        }
    }
}
