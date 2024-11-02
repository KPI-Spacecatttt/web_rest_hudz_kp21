using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using web_rest_hudz_kp21.Database;
using web_rest_hudz_kp21.Models;
using web_rest_hudz_kp21.Models.DTOs;

namespace web_rest_hudz_kp21.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BicycleController : ControllerBase
    {
        private readonly IRepository<Bicycle> _bicycleRepository;
        private readonly IMapper _mapper;

        public BicycleController(
            IRepository<Bicycle> bicycleRepository,
            IMapper mapper
        )
        {
            _bicycleRepository = bicycleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a list of all available bicycles.
        /// </summary>
        /// <response code="200">
        /// Successfully retrieved the list of bicycles.
        /// </response>
        [HttpGet]
        public ActionResult<IEnumerable<BicycleSummaryDTO>> GetAllBicycles()
        {
            var bicycles = _bicycleRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<BicycleSummaryDTO>>(bicycles));
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
            var bicycle = _bicycleRepository.GetById(id);
            if (bicycle is null)
            {
                return NotFound();
            }
            return Ok(bicycle);
        }

        /// <summary>
        /// Adds a new bicycle to the collection.
        /// </summary>
        /// <param name="bicycleDTO">
        /// The new bicycle object to add.
        /// </param>
        /// <returns>
        /// The created bicycle object.
        /// </returns>
        /// <response code="201">Successfully created the bicycle.</response>
        [HttpPost]
        public ActionResult<Bicycle> AddBicycle(BicycleDTO bicycleDTO)
        {
            Bicycle bicycle = _mapper.Map<Bicycle>(bicycleDTO);

            _bicycleRepository.Add(bicycle);
            return CreatedAtAction(
               nameof(GetBicycleById),
               new { id = bicycle.Id },
               bicycleDTO);
        }

        /// <summary>
        /// Updates an existing bicycle by its ID.
        /// </summary>
        /// <param name="id">
        /// The ID of the bicycle to update.
        /// </param>
        /// <param name="bicycleDTO">
        /// The updated bicycle data.
        /// </param>
        /// <returns>
        /// No content if the update is successful; otherwise, 404 Not Found.
        /// </returns>
        /// <response code="204">Successfully updated the bicycle.</response>
        /// <response code="404">Bicycle not found.</response>
        [HttpPut("{id}")]
        public IActionResult UpdateBicycle(int id, BicycleDTO bicycleDTO)
        {
            var bicycle = _bicycleRepository.GetById(id);
            if (bicycle is null)
            {
                return NotFound();
            }

            bicycle = _mapper.Map(bicycleDTO, bicycle);
            _bicycleRepository.Update(bicycle);
            return Ok(bicycle);
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
            var bicycle = _bicycleRepository.GetById(id);
            if (bicycle is null)
            {
                return NotFound();
            }

            _bicycleRepository.Remove(bicycle);
            return NoContent();
        }

    }
}
