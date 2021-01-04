using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Models;
using ParkyAPI.Models.DTO;
using ParkyAPI.Repository;
using ParkyAPI.Repository.IRepository;

namespace ParkyAPI.Controllers
{
    //[Route("api/[controller]")]
    //[ApiExplorerSettings(GroupName = "ParkyOpenApiSpecNationalPark")]
    [ApiController]
    [Route("api/v{version:apiVersion}/NationalPark")]
    [ApiVersion("1.0")]

    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class NationalParksController : ControllerBase
    {
        private readonly INationalParkRepository npRepository;
        private readonly IMapper mapper;
        public NationalParksController(INationalParkRepository npRepository, IMapper mapper)
        {
            this.npRepository = npRepository;
            this.mapper = mapper;
        }

        
        /// <summary>
        /// Get All National Parks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK,Type =typeof(List<NationalParkDTO>))]
        [ProducesDefaultResponseType]
       
        public IActionResult GetNationalParks()
        {
            var nationalParkList = npRepository.GetNationalParks();
            var nationalParkDTOList = new List<NationalParkDTO>();

            foreach (var obj in nationalParkList)
            {
                nationalParkDTOList.Add(mapper.Map<NationalParkDTO>(obj));
            }

            return Ok(nationalParkDTOList);
        }

        /// <summary>
        /// Get National Park By Id
        /// </summary>
        /// <param name="nationalparkId"></param>
        /// <returns></returns>

        [HttpGet("{nationalparkId:int}",Name = "GetNationalPark")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NationalParkDTO))]
        [ProducesDefaultResponseType]
        public IActionResult GetNationalPark(int nationalparkId)
        {
            var Obj = npRepository.GetNationalPark(nationalparkId);
            if (Obj == null)
            {
                return NotFound();
            }

            var ObjDTO = mapper.Map<NationalParkDTO>(Obj);
            return Ok(ObjDTO);
        }

        /// <summary>
        /// Add a New National Park
        /// </summary>
        /// <param name="nationalParkDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created,Type =typeof(NationalParkDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]

        public IActionResult CreateNationalPark([FromBody] NationalParkDTO nationalParkDTO)
        {
            if(nationalParkDTO == null)
            {
                return BadRequest(ModelState);
            }
            if(npRepository.CheckExistNationalPark(nationalParkDTO.Name))
            {
                ModelState.AddModelError(string.Empty, "NationalPark Exists");
                return StatusCode(404, ModelState);
            }

            var obj = mapper.Map<NationalPark>(nationalParkDTO);
            if(!npRepository.CreateNationalPark(obj))
            {
                ModelState.AddModelError(string.Empty, $"Something Wrong{obj.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetNationalPark", new { nationalParkId = obj.Id , Version=HttpContext.GetRequestedApiVersion().ToString() }, obj);
           
        }

        /// <summary>
        /// Update National Park
        /// </summary>
        /// <param name="nationalParkId"></param>
        /// <param name="nationalParkDTO"></param>
        /// <returns></returns>
        [HttpPatch("{nationalParkId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
       public IActionResult UpdateNationalPark (int nationalParkId ,[FromBody] NationalParkDTO nationalParkDTO )
        {
            if (nationalParkDTO==null || nationalParkId!=nationalParkDTO.Id)
            {
                return BadRequest(ModelState);
            }

            var objName = npRepository.GetNationalPark(nationalParkDTO.Name);
            if (objName !=null)
            {
                if (objName.Id != nationalParkId)
                {
                    ModelState.AddModelError(string.Empty, "NationalPark Exists");
                    return StatusCode(404, ModelState);
                }
            }

            var obj = mapper.Map<NationalPark>(nationalParkDTO);
            var objFromDB = npRepository.GetNationalPark(obj.Id);

            objFromDB.Name = obj.Name;
            objFromDB.Id = obj.Id;
            objFromDB.State = obj.State;
            objFromDB.Established = obj.Established;
            objFromDB.Created = obj.Created;

            if (!npRepository.UpdateNationalPark(objFromDB))
            {
                ModelState.AddModelError(string.Empty, $"SomeThing Wrong{obj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        } 

        /// <summary>
        /// Delete National Park
        /// </summary>
        /// <param name="nationalParkId"></param>
        /// <returns></returns>
        [HttpDelete("{nationalParkId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesDefaultResponseType]
        public IActionResult DeleteNationalPark (int nationalParkId)
        {
            if (!npRepository.CheckExistNationalPark(nationalParkId))
            {
                return NotFound();
            }

            var obj = npRepository.GetNationalPark(nationalParkId);
            if (! npRepository.DeleteNationalPark(obj)) 
            {
                ModelState.AddModelError(string.Empty, $"Something Wrong{obj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }















    }
}
