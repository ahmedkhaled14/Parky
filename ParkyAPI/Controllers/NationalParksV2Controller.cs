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
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]

    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public class NationalParksV2Controller : ControllerBase
    {
        private readonly INationalParkRepository npRepository;
        private readonly IMapper mapper;
        public NationalParksV2Controller(INationalParkRepository npRepository, IMapper mapper)
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

      

    }
}
