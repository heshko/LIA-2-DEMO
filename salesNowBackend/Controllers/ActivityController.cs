using AutoMapper;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using salesNowBackend.Contracts;
using salesNowBackend.DTO.ActivityDto;
using salesNowBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace salesNowBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityFirestore _activityFirestore;
        private readonly IMapper _mapper;
        public ActivityController(IActivityFirestore activityFirestore, IMapper mapper)
        {
            _activityFirestore = activityFirestore;
            _mapper = mapper;
        }   

        [HttpGet]
        public async Task<IActionResult> GetAllActivites()
        {
            var activitiesDTO = await _activityFirestore.GetAllActivites("Activities");

            if (activitiesDTO is null)
            {
                return StatusCode(500, "Internal Server Error");
            }
          
            return Ok(activitiesDTO);
        }

        [HttpGet("{activityId}")]
        public async Task<IActionResult> GetActivity(string activityId)
        {
            var activityDTO = await _activityFirestore.GetActivityById("Activities", activityId);

            if (activityDTO is null)
            {
                return NotFound($"Activity with Id {activityId} not existes in Database");
            }
        
            return Ok(activityDTO);
        }

    }
}
