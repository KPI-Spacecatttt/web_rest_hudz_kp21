using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using web_rest_hudz_kp21.Models;

namespace web_rest_hudz_kp21.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BicycleController : ControllerBase
    {
        private static List<Bicycle> bicycles = [
            new(1, "Big Trail", "Road", "Merida", 2023, 11.5, 1200.99f, 10),
            new(2, "Escape", "Mountain", "Giant", 2024, 8.2, 950.00f, 4),
            new(3, "Range C1", "Enduro", "Norco", 2022, 13.2, 9999.00f, 5),
            new(4, "V 10 CC", "Mountain", "Santa Cruz", 2023, 15.2,
                10750.00f, 2)
        ];

        /// <summary>
        /// Retrieves a list of all available bicycles.
        /// </summary>
        /// <response code="200">
        /// Successfully retrieved the list of bicycles.
        /// </response>
        [HttpGet]
        public ActionResult<IEnumerable<Bicycle>> GetAllBicycles()
        {
            return Ok(bicycles);
        }

        /// <summary>
        /// Retrieves a specific bicycle by its ID.
        /// </summary>
        /// <param name="id">
        /// The ID of the bicycle to retrieve.
        /// </param>
        /// <returns>
        /// A bicycle object if found; otherwise, 404 Not Found.
        /// </returns>
        /// <response code="200">Successfully retrieved the bicycle.</response>
        /// <response code="404">Bicycle not found.</response>
        [HttpGet("{id}")]
        public ActionResult<Bicycle> GetBicycleById(int id)
        {
            var bicycle = bicycles.FirstOrDefault(b => b.BicycleId == id);
            if (bicycle is null)
            {
                return NotFound();
            }
            return Ok(bicycle);
        }

        /// <summary>
        /// Adds a new bicycle to the collection.
        /// </summary>
        /// <param name="newBicycle">
        /// The new bicycle object to add.
        /// </param>
        /// <returns>
        /// The created bicycle object.
        /// </returns>
        /// <response code="201">Successfully created the bicycle.</response>
        [HttpPost]
        public ActionResult<Bicycle> AddBicycle(Bicycle newBicycle)
        {
            bicycles.Add(newBicycle);
            return CreatedAtAction(
               nameof(GetBicycleById),
               new { id = newBicycle.BicycleId },
               newBicycle);
        }

        /// <summary>
        /// Updates an existing bicycle by its ID.
        /// </summary>
        /// <param name="id">
        /// The ID of the bicycle to update.
        /// </param>
        /// <param name="updatedBicycle">
        /// The updated bicycle data.
        /// </param>
        /// <returns>
        /// No content if the update is successful; otherwise, 404 Not Found.
        /// </returns>
        /// <response code="204">Successfully updated the bicycle.</response>
        /// <response code="404">Bicycle not found.</response>
        [HttpPut("{id}")]
        public IActionResult UpdateBicycle(int id, Bicycle updatedBicycle)
        {
            var bicycle = bicycles.FirstOrDefault(b => b.BicycleId == id);
            if (bicycle is null)
            {
                return NotFound();
            }

            bicycle.Model = updatedBicycle.Model;
            bicycle.Type = updatedBicycle.Type;
            bicycle.Manufacturer = updatedBicycle.Manufacturer;
            bicycle.ReleaseYear = updatedBicycle.ReleaseYear;
            bicycle.Weight = updatedBicycle.Weight;
            bicycle.Price = updatedBicycle.Price;
            bicycle.StockQuantity = updatedBicycle.StockQuantity;

            return NoContent();
        }

        /// <summary>
        /// Deletes a bicycle by its ID.
        /// </summary>
        /// <param name="id">
        /// The ID of the bicycle to delete.
        /// </param>
        /// <returns>
        /// No content if the delete is successful; otherwise, 404 Not Found.
        /// </returns>
        /// <response code="204">Successfully deleted the bicycle.</response>
        /// <response code="404">Bicycle not found.</response>
        [HttpDelete("{id}")]
        public IActionResult DeleteBicycle(int id)
        {
            var bicycle = bicycles.FirstOrDefault(b => b.BicycleId == id);
            if (bicycle is null)
            {
                return NotFound();
            }

            bicycles.Remove(bicycle);
            return NoContent();
        }

    }
}
