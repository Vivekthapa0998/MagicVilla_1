using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Logging;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Runtime.Serialization.Json;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]//coz of this it wont hit the modelstate.isvalid and wont enter the action method
    public class VillaAPIController : ControllerBase
    {
        //private readonly ILogging _logger;

        //public VillaAPIController(ILogging logger)
        //{
        //    _logger = logger;
        //}
        //private readonly ILogger<VillaAPIController> _logger;

        //public VillaAPIController(ILogger<VillaAPIController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public VillaAPIController(IVillaRepository dbVilla, IMapper mapper)
        {

            _dbVilla= dbVilla;
            _mapper = mapper;
            this._response = new();
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles ="admin")]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var villa = await _dbVilla.GetAsync(u => u.Id == id);
                if (villa == null)
                {
                    return NotFound();
                }
                await _dbVilla.RemoveAsync(villa);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet]
        [ResponseCache(CacheProfileName ="Default30")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task <ActionResult<APIResponse>> GetVillas([FromQuery(Name ="FilterOccupancy")] int? occupancy ,
            [FromQuery] string? search, int pageSize=0,int pageNumber=1)
        {
            //_logger.LogInformation("Getting all villas");
            //_logger.Log("Getting all villas","");
            try
            {
                IEnumerable<Villa> villaList;
                if(occupancy>0)
                {
                    villaList= await _dbVilla.GetAllAsync(u=> u.Occupancy == occupancy,pageSize:pageSize,pageNumber:pageNumber);
                }
                else
                {
                    villaList = await _dbVilla.GetAllAsync( pageSize: pageSize, pageNumber: pageNumber);
                }

                if (!string.IsNullOrEmpty(search))
                {
                    villaList= villaList.Where(u=> u.Name.ToLower().Contains(search));
                }

                Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.Result = _mapper.Map<List<VillaDto>>(villaList);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex) 
            {
                _response.IsSuccess = false;
                _response.ErrorMessages= new List<string>() { ex.ToString()};
            }
            return _response;
        }

        [HttpGet("{id:int}",Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(200, Type = typeof(VillaDto))]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        
        public async Task <ActionResult<APIResponse>> GetVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    //_logger.Log("Get villa Error with id " + id, "error");
                    //_logger.LogError("Get villa Error with id " + id);
                    _response.IsSuccess= false;
                    _response.StatusCode= HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var villa = await _dbVilla.GetAsync(u => u.Id == id);
                if (villa == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<VillaDto>(villa);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
       // [Authorize(Roles ="manager")]
        public async Task <ActionResult<APIResponse>> CreateVilla([FromBody] VillaCreateDto createDto)
        {
            try
            {
                if (await _dbVilla.GetAsync(u => u.Name.ToLower() == createDto.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Villa already exist");
                    return BadRequest(ModelState);
                }
                if (createDto == null)
                {
                    return BadRequest();
                }

                //villaDto.Id= DataStore.VillaList.OrderByDescending(u=>u.Id).FirstOrDefault().Id + 1;
                //Villa model = new()
                //{

                //    Name = createDto.Name,
                //    Details= createDto.Details,
                //    ImageUrl= createDto.ImageUrl,
                //    Occupancy= createDto.Occupancy,
                //    Rate= createDto.Rate,
                //    Sqft= createDto.Sqft,
                //    Amenity= createDto.Amenity
                //};
                Villa villa = _mapper.Map<Villa>(createDto);
                await _dbVilla.CreateAsync(villa);
                _response.Result = _mapper.Map<VillaDto>(villa);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetVilla", new { id = villa.Id }, _response);
                // return Ok(villaDto);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        
        [HttpPut("{id:int}", Name ="UpdateVilla")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task <ActionResult<APIResponse>> UpdateVilla(int id,[FromBody] VillaUpdateDto updateDto)
        {
            try
            {
                if (updateDto == null || updateDto.Id != id)
                {
                    return BadRequest();
                }
                //var villa= DataStore.VillaList.FirstOrDefault(u=> u.Id==id);
                //villa.Name=villaDto.Name;
                //villa.Occupancy=villaDto.Occupancy;
                //villa.Sqft=villaDto.Sqft;
                //Villa model = new()
                //{
                //    Id = updateDto.Id,
                //    Name = updateDto.Name,
                //    Details = updateDto.Details,
                //    ImageUrl = updateDto.ImageUrl,
                //    Occupancy = updateDto.Occupancy,
                //    Rate = updateDto.Rate,
                //    Sqft = updateDto.Sqft,
                //    Amenity = updateDto.Amenity
                //};
                Villa model = _mapper.Map<Villa>(updateDto);
                await _dbVilla.UpdateAsync(model);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPatch("{id:int}", Name ="UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDto> patchDTO)
        {
            if(patchDTO==null || id==0)
            {
                return BadRequest();
            }
            var villa= await _dbVilla.GetAsync(u=>u.Id==id,tracked: false);

            VillaUpdateDto villaDTO = _mapper.Map<VillaUpdateDto>(villa);
            //VillaUpdateDto villaDTO = new()
            //{
            //    Amenity = villa.Amenity,
            //    Rate = villa.Rate,
            //    Sqft = villa.Sqft,
            //    Details = villa.Details,
            //    Id = villa.Id,
            //    ImageUrl = villa.ImageUrl,
            //    Occupancy = villa.Occupancy,
            //    Name = villa.Name
            //};
            if (villa == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(villaDTO,ModelState);
            Villa model= _mapper.Map<Villa>(villaDTO);
            //Villa model = new()
            //{
            //    Id = villaDTO.Id,
            //    Name = villaDTO.Name,
            //    Details = villaDTO.Details,
            //    ImageUrl = villaDTO.ImageUrl,
            //    Occupancy = villaDTO.Occupancy,
            //    Rate = villaDTO.Rate,
            //    Sqft = villaDTO.Sqft,
            //    Amenity = villaDTO.Amenity
            //};
            _dbVilla.UpdateAsync(model); 

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            return NoContent(); 
        }
    }
}
