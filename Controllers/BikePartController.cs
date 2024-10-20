using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using web_rest_hudz_kp21.Models;

namespace web_rest_hudz_kp21.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BikePartController : ControllerBase
    {
        private static List<BikePart> bikeParts = [
            new BikePart(1, "Wheel", "Aluminum", "SRAM", 20.5f, 20),
            new BikePart(2, "Brake", "Steel", "Shimano", 10.8f, 50),
            new BikePart(3, "Handlebar", "Carbon", "Shimano", 13.2f, 30),
            new BikePart(4, "Saddle", "Leather", "Brooks England", 43.3f, 15),
            new BikePart(5, "Chain", "Steel", "SRAM", 19.5f, 100)
        ];

        /// <summary>
        /// Retrieves a list of all available bike parts.
        /// </summary>
        /// <returns>
        /// Returns a list of bike parts.
        /// </returns>
        /// <response code="200">
        /// Successfully retrieved the list of bike parts.
        /// </response>
        [HttpGet]
        public ActionResult<IEnumerable<BikePart>> GetAllBikeParts()
        {
            return Ok(bikeParts);
        }

        /// <summary>
        /// Retrieves a specific bike part by its ID.
        /// </summary>
        /// <param name="id">
        /// The ID of the bike part to retrieve.
        /// </param>
        /// <returns>
        /// A bike part object if found; otherwise, 404 Not Found.
        /// </returns>
        /// <response code="200">
        /// Successfully retrieved the bike part.
        /// </response>
        /// <response code="404">Bike part not found.</response>
        [HttpGet("{id}")]
        public ActionResult<BikePart> GetBikePartById(int id)
        {
            var bikePart = bikeParts.FirstOrDefault(bp => bp.PartId == id);
            if (bikePart is null)
            {
                return NotFound();
            }
            return Ok(bikePart);
        }

        /// <summary>
        /// Adds a new bike part to the collection.
        /// </summary>
        /// <param name="newBikePart">
        /// The new bike part object to add.
        /// </param>
        /// <returns>
        /// The created bike part object.
        /// </returns>
        /// <response code="201">Successfully created the bike part.</response>
        [HttpPost]
        public ActionResult<BikePart> AddBikePart(BikePart newBikePart)
        {
            bikeParts.Add(newBikePart);
            return CreatedAtAction(
                nameof(GetBikePartById),
                new { id = newBikePart.PartId },
                newBikePart
            );
        }

        /// <summary>
        /// Updates an existing bike part by its ID.
        /// </summary>
        /// <param name="id">
        /// The ID of the bike part to update.
        /// </param>
        /// <param name="updatedBikePart">
        /// The updated bike part data.
        /// </param>
        /// <returns>
        /// No content if the update is successful; otherwise, 404 Not Found.
        /// </returns>
        /// <response code="204">Successfully updated the bike part.</response>
        /// <response code="404">Bike part not found.</response>
        [HttpPut("{id}")]
        public IActionResult UpdateBikePart(int id, BikePart updatedBikePart)
        {
            var bikePart = bikeParts.FirstOrDefault(bp => bp.PartId == id);
            if (bikePart is null)
            {
                return NotFound();
            }

            bikePart.PartType = updatedBikePart.PartType;
            bikePart.Description = updatedBikePart.Description;
            bikePart.Manufacturer = updatedBikePart.Manufacturer;
            bikePart.Price = updatedBikePart.Price;
            bikePart.StockQuantity = updatedBikePart.StockQuantity;

            return NoContent();
        }

        /// <summary>
        /// Deletes a bike part by its ID.
        /// </summary>
        /// <param name="id">
        /// The ID of the bike part to delete.
        /// </param>
        /// <returns>
        /// No content if the delete is successful; otherwise, 404 Not Found.
        /// </returns>
        /// <response code="204">Successfully deleted the bike part.</response>
        /// <response code="404">Bike part not found.</response>
        [HttpDelete("{id}")]
        public IActionResult DeleteBikePart(int id)
        {
            var bikePart = bikeParts.FirstOrDefault(bp => bp.PartId == id);
            if (bikePart is null)
            {
                return NotFound();
            }

            bikeParts.Remove(bikePart);
            return NoContent();
        }
    }
}
