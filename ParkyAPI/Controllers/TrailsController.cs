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
    //[ApiExplorerSettings(GroupName = "ParkyOpenApiSpecTrails")]
    [ApiController]
    [Route("api/{version 1}/[controller]")]
    [ApiVersion("1.0")]

    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public class TrailsController : ControllerBase
    {
        private readonly ITrailRepository trailRepository;
        private readonly IMapper mapper;
        public TrailsController(ITrailRepository trailRepository, IMapper mapper)
        {
            this.trailRepository = trailRepository;
            this.mapper = mapper;
        }

        
        /// <summary>
        /// Get All Trails
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK,Type =typeof(List<TrailDTO>))]
        [ProducesDefaultResponseType]
       
        public IActionResult GetTrails()
        {
            var TrailList = trailRepository.GetTrails();
            var TrailDTOList = new List<TrailDTO>();

            foreach (var obj in TrailList)
            {
                TrailDTOList.Add(mapper.Map<TrailDTO>(obj));
            }

            return Ok(TrailDTOList);
        }


        [HttpGet("[action]/{nationalparkid:int}",Name = "GetTrailsInNationalPark")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TrailDTO>))]
        [ProducesDefaultResponseType]

        public IActionResult GetTrailsInNationalPark(int nationalparkid)
        {
            var TrailList = trailRepository.GetTrailsInNationalPark(nationalparkid);
            var TrailDTOList = new List<TrailDTO>();

            foreach (var obj in TrailList)
            {
                TrailDTOList.Add(mapper.Map<TrailDTO>(obj));
            }

            return Ok(TrailDTOList);
        }

        /// <summary>
        /// Get Trail By Id
        /// </summary>
        /// <param name="TrailId"></param>
        /// <returns></returns>

        [HttpGet("{TrailId:int}",Name = "GetTrail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TrailDTO))]
        [ProducesDefaultResponseType]
        public IActionResult GetTrail(int TrailId)
        {
            var Obj = trailRepository.GetTrail(TrailId);
            if (Obj == null)
            {
                return NotFound();
            }

            var ObjDTO = mapper.Map<TrailDTO>(Obj);
            return Ok(ObjDTO);
        }

        /// <summary>
        /// Add a New Trail
        /// </summary>
        /// <param name="TrailDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created,Type =typeof(TrailDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]

        public IActionResult CreateTrail([FromBody] TrailInsertDTO TrailDTO)
        {
            if(TrailDTO == null)
            {
                return BadRequest(ModelState);
            }
            if(trailRepository.CheckExistTrail(TrailDTO.Name))
            {
                ModelState.AddModelError(string.Empty, "Trail Exists");
                return StatusCode(404, ModelState);
            }

            var obj = mapper.Map<Trail>(TrailDTO);
            if(!trailRepository.CreateTrail(obj))
            {
                ModelState.AddModelError(string.Empty, $"Something Wrong{obj.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetTrail", new { TrailId = obj.Id }, obj);
           
        }

        /// <summary>
        /// Update Trail
        /// </summary>
        /// <param name="TrailId"></param>
        /// <param name="TrailDTO"></param>
        /// <returns></returns>
        [HttpPatch("{TrailId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
       public IActionResult UpdateTrail (int TrailId ,[FromBody] TrailUpdateDTO TrailDTO )
        {
            if (TrailDTO==null || TrailId!=TrailDTO.Id)
            {
                return BadRequest(ModelState);
            }

            var objName = trailRepository.GetTrail(TrailDTO.Name);
            if (objName !=null)
            {
                if (objName.Id != TrailId)
                {
                    ModelState.AddModelError(string.Empty, "Trail Exists");
                    return StatusCode(404, ModelState);
                }
            }

            var obj = mapper.Map<Trail>(TrailDTO);
            var objFromDB = trailRepository.GetTrail(obj.Id);

            objFromDB.Name = obj.Name;
            objFromDB.Distance = obj.Distance;
            objFromDB.Difficulty = obj.Difficulty;
            objFromDB.NationalParkId = obj.NationalParkId;

            if (!trailRepository.UpdateTrail(objFromDB))
            {
                ModelState.AddModelError(string.Empty, $"SomeThing Wrong{obj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        } 

        /// <summary>
        /// Delete Trail
        /// </summary>
        /// <param name="TrailId"></param>
        /// <returns></returns>
        [HttpDelete("{TrailId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesDefaultResponseType]
        public IActionResult DeleteTrail (int TrailId)
        {
            if (!trailRepository.CheckExistTrail(TrailId))
            {
                return NotFound();
            }

            var obj = trailRepository.GetTrail(TrailId);
            if (! trailRepository.DeleteTrail(obj)) 
            {
                ModelState.AddModelError(string.Empty, $"Something Wrong{obj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }















    }
}
