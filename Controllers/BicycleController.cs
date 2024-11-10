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
    public class BicycleController : ControllerBase
    {
        private readonly IRepository<Bicycle> _bicycleRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger<BicycleController> _logger;

        public BicycleController(
            IRepository<Bicycle> bicycleRepository,
            IMapper mapper,
            IConfiguration configuration,
            ILogger<BicycleController> logger

        )
        {
            _bicycleRepository = bicycleRepository;
            _mapper = mapper;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves a list of all available bicycles.
        /// </summary>
        /// <response code="200">
        /// Successfully retrieved the list of bicycles.
        /// </response>
        [HttpGet]
        public ActionResult<IEnumerable<object>> GetAllBicycles()
        {
            // Log the request and client IP
            var clientIp = HttpContext.Connection.RemoteIpAddress?.ToString();
            _logger.LogDebug("[{clientIp}] Getting all bicycles.", clientIp);

            var bicycles = _bicycleRepository.GetAll();

            bool showAvailableOnly = _configuration.GetValue<bool>(
                "BicycleApiSettings:Bicycle:ShowAvailableOnly");
            if (showAvailableOnly)
                bicycles = bicycles.Where(x => x.StockQuantity > 0);

            bool showFullInformation = _configuration.GetValue<bool>(
                "BicycleApiSettings:Bicycle:ShowFullInformation");

            object data = bicycles;
            if (!showFullInformation)
                data = _mapper.Map<IEnumerable<BicycleSummaryDTO>>(bicycles);
            return Ok(data);
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
            // Log the request and client IP
            var clientIp = HttpContext.Connection.RemoteIpAddress?.ToString();
            _logger.LogDebug("[{clientIp}] Getting bicycle by id: {id}.", clientIp, id);

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
            // Log the request and client IP
            var clientIp = HttpContext.Connection.RemoteIpAddress?.ToString();
            _logger.LogDebug("[{clientIp}] Adding bicycle.", clientIp);

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
            // Log the request and client IP
            var clientIp = HttpContext.Connection.RemoteIpAddress?.ToString();
            _logger.LogDebug("[{clientIp}] Updating the bicycle with id: {id}.", clientIp, id);

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
            // Log the request and client IP
            var clientIp = HttpContext.Connection.RemoteIpAddress?.ToString();
            _logger.LogDebug("[{clientIp}] Deleting the bicycle with id: {id}.", clientIp, id);

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
