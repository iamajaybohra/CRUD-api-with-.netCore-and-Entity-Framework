using Microsoft.AspNetCore.Mvc;
using MagicVillia_VillaAPI.Model;
using MagicVillia_VillaAPI.Model.DTO;
using MagicVillia_VillaAPI.Data;
using Microsoft.AspNetCore.JsonPatch;
using MagicVillia_VillaAPI.Logging;
using Microsoft.EntityFrameworkCore;

namespace MagicVillia_VillaAPI.Controllers
{
    //Another way to route Route("api/[controller]")
    [Route("api/VillaAPI")]
    //controller class should be treated as an API controller
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        //private readonly ILogger<VillaAPIController> _logger;

        // as the logger is build-in .net , here we are asking for the implementation with 
        // help of dependency injection.
        /*
        public VillaAPIController(ILogger<VillaAPIController> logger)
        {
           _logger = logger;
        }
        */

        private readonly ApplicationDbContext _db;
        private readonly ILogging _logger;
        public VillaAPIController(ILogging logger,ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.Log("Getting all villas", " ");
            return Ok(_db.Villas.ToList());
        }

        // Name = "XYZ", is being used to give explicit name to the end point.
        [HttpGet("{id:int}",Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        /*
         Another way is 
            [ProducesResponseType(200,Type = typeof(VillaDTO)] 
         */
        public ActionResult<VillaDTO> GetVilllas(int id)
        {
            if (id == 0)
            {
                _logger.Log("Get Villa Error with Id" + id,"error");
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(x => x.Id == id);    
            if(villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villaDTO)
        {
            /*
             * another way to check the model validation
             * if(!ModelState.IsValid)
             * {
             *   return BadRequest(ModelState);
             * }
             */
            if(villaDTO == null)
            {
                return BadRequest(villaDTO);
            }
            /*
              Add custom validation such that the villa name should be unique
             */
            if(_db.Villas.FirstOrDefault(x=>x.Name.ToLower() == villaDTO.Name.ToLower())!=null)
            {
                ModelState.AddModelError("CustomError", "Villa already Exists!");
                return BadRequest(ModelState);
            }
            if(villaDTO.Id > 0)
            {
                //to return the status code directly
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            Villa model = new()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Id = villaDTO.Id,
                ImageUrl = villaDTO.ImageUrl,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft
            };
            _db.Villas.Add(model);
            _db.SaveChanges();
            // to tell the client the url where the villa has been created
            return CreatedAtRoute("GetVilla",new {id  = villaDTO.Id}, villaDTO);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
        {
            //if id ==0
            if(id==0)
            {
                return BadRequest();
            }
            //check if the villa exit or not
            var villa = _db.Villas.FirstOrDefault(x => x.Id == id);
            if (villa == null)
            {
                ModelState.AddModelError("CustomError", "The villa does not exit");
                return NotFound(ModelState);
            }        
            _db.Villas.Remove(villa);
            _db.SaveChanges();          
            return NoContent();
        }

        //this is used to update the whole model
        [HttpPut("{id:int}",Name ="UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateVilla(int id, [FromBody]VillaDTO villaDTO)
        {
            // checking the edge case
            if(villaDTO == null || id != villaDTO.Id)
            {
                return BadRequest();
            }
            Villa model = new()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Id = villaDTO.Id,
                ImageUrl = villaDTO.ImageUrl,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft
            };
            _db.Villas.Update(model);
            _db.SaveChanges();
            return NoContent();

        }

        // to update the villa partially
        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdatePartialVilla(int id,JsonPatchDocument<VillaDTO> patchDTO)
        {
            if(patchDTO == null || id ==0)
            {
                return BadRequest();
            }

            var villa = _db.Villas.AsNoTracking().FirstOrDefault(x=>x.Id==id);
            if(villa == null)
            {
                return BadRequest();    
            }
            VillaDTO villaDTO = new()
            {
                Amenity = villa.Amenity,
                Details = villa.Details,
                Id = villa.Id,
                ImageUrl = villa.ImageUrl,
                Name = villa.Name,
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                Sqft = villa.Sqft
            };

            patchDTO.ApplyTo(villaDTO, ModelState);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Villa model = new()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Id = villaDTO.Id,
                ImageUrl = villaDTO.ImageUrl,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft
            };
            _db.Villas.Update(model);
            _db.SaveChanges();
            return NoContent();
        }


    }
}
