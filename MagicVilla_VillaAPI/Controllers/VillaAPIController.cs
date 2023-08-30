using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]//coz of this it wont hit the modelstate.isvalid and wont enter the action method
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogger<VillaAPIController> _logger;

        public VillaAPIController(ILogger<VillaAPIController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            _logger.LogInformation("Getting all villas");
            return DataStore.VillaList;
        }

        [HttpGet("{id:int}",Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(200, Type = typeof(VillaDto))]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        public ActionResult<VillaDto> GetVilla(int id)
        {
            if(id==0)
            {
                _logger.LogError("Get villa Error with id " + id);
                return BadRequest();
            }
            var villa= DataStore.VillaList.FirstOrDefault(u => u.Id == id);
            if(villa==null)
            {
                return NotFound();
            }
            return Ok(villa);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VillaDto> Create([FromBody] VillaDto villaDto)
        {
            if(DataStore.VillaList.FirstOrDefault(u=> u.Name.ToLower() == villaDto.Name.ToLower())!=null)
            {
                ModelState.AddModelError("customError", "Villa already exist");
                return BadRequest(ModelState);
            }
            if(villaDto == null) 
            {
                return BadRequest();
            }
            if(villaDto.Id>0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            villaDto.Id= DataStore.VillaList.OrderByDescending(u=>u.Id).FirstOrDefault().Id + 1;
            DataStore.VillaList.Add(villaDto);
            return CreatedAtRoute("GetVilla",new { id= villaDto.Id}, villaDto);
           // return Ok(villaDto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteVilla(int id) 
        { 
            if(id==0)
            {
                return BadRequest();
            }
            var villa=DataStore.VillaList.FirstOrDefault(u=>u.Id==id);
            if(villa == null)
            {
                return NotFound();
            }
            DataStore.VillaList.Remove(villa);
            return NoContent();
        }
        [HttpPut("{id:int}", Name ="UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateVilla(int id,[FromBody] VillaDto villaDto) 
        { 
            if(villaDto ==null || villaDto.Id!=id)
            {
                return BadRequest();
            }
            var villa= DataStore.VillaList.FirstOrDefault(u=> u.Id==id);
            villa.Name=villaDto.Name;
            villa.Occupancy=villaDto.Occupancy;
            villa.Sqft=villaDto.Sqft;
            return NoContent();
        }
        [HttpPatch("{id:int}", Name ="UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDto> patchDTO)
        {
            if(patchDTO==null || id==0)
            {
                return BadRequest();
            }
            var villa= DataStore.VillaList.FirstOrDefault(u=>u.Id==id);
            if (villa == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(villa,ModelState);
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            return NoContent(); 
        }
    }
}
