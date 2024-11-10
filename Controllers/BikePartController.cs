using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using web_rest_hudz_kp21.Database;
using web_rest_hudz_kp21.Models;
using web_rest_hudz_kp21.Models.DTOs;

namespace web_rest_hudz_kp21.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ResponseCache(CacheProfileName = "Default20")]
    public class BikePartController : ControllerBase
    {
        private readonly IRepository<BikePart> _bikePartRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger<BicycleController> _logger;

        public BikePartController(
            IRepository<BikePart> bikePartRepository,
            IMapper mapper,
            IConfiguration configuration,
            ILogger<BicycleController> logger)
        {
            _bikePartRepository = bikePartRepository;
            _mapper = mapper;
            _configuration = configuration;
            _logger = logger;
        }

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
        public ActionResult<IEnumerable<BikePartSummaryDTO>> GetAllBikeParts()
        {
            var bikeParts = _bikePartRepository.GetAll();

            bool showAvailableOnly = _configuration.GetValue<bool>(
                "BicycleApiSettings:BikePart:ShowAvailableOnly");
            if (showAvailableOnly)
                bikeParts = bikeParts.Where(x => x.StockQuantity > 0);

            return Ok(_mapper.Map<IEnumerable<BikePartSummaryDTO>>(bikeParts));
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
            var bikePart = _bikePartRepository.GetById(id);
            if (bikePart is null)
            {
                return NotFound();
            }
            return Ok(bikePart);
        }

        /// <summary>
        /// Adds a new bike part to the collection.
        /// </summary>
        /// <param name="bikePartDTO">
        /// The new bike part object to add.
        /// </param>
        /// <returns>
        /// The created bike part object.
        /// </returns>
        /// <response code="201">Successfully created the bike part.</response>
        [HttpPost]
        public ActionResult<BikePart> AddBikePart(BikePartDTO bikePartDTO)
        {
            BikePart bikePart = _mapper.Map<BikePart>(bikePartDTO);

            _bikePartRepository.Add(bikePart);
            return CreatedAtAction(
                nameof(GetBikePartById),
                new { id = bikePart.Id },
                bikePartDTO
            );
        }

        /// <summary>
        /// Updates an existing bike part by its ID.
        /// </summary>
        /// <param name="id">
        /// The ID of the bike part to update.
        /// </param>
        /// <param name="bikePartDTO">
        /// The updated bike part data.
        /// </param>
        /// <returns>
        /// No content if the update is successful; otherwise, 404 Not Found.
        /// </returns>
        /// <response code="204">Successfully updated the bike part.</response>
        /// <response code="404">Bike part not found.</response>
        [HttpPut("{id}")]
        public IActionResult UpdateBikePart(int id, BikePartDTO bikePartDTO)
        {
            var bikePart = _bikePartRepository.GetById(id);
            if (bikePart is null)
            {
                return NotFound();
            }
            bikePart = _mapper.Map(bikePartDTO, bikePart);
            _bikePartRepository.Update(bikePart);
            return Ok(bikePart);
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
            var bikePart = _bikePartRepository.GetById(id);
            if (bikePart is null)
            {
                return NotFound();
            }

            _bikePartRepository.Remove(bikePart);
            return NoContent();
        }
    }
}
